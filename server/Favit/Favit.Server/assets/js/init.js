var in_motion = false;
var showing_countdown = true;
var ytPlayer = null;
var contact_form_open = false;
var prevWidth = 0;
var firstLoad = false;
var nbOfTicks = 0;
var videoDefined = false;
	
$(document).ready(function() {
                
	_startLoading();

	// fade in backgrounds, countdown
	$('#bg-image').animate({opacity: 1}, 1000);
	
	if(_isDesktop() && !_isIE8Less()) {
		
		var video_id = $("#bg-video").data('videoid');
		var mute = $("#bg-video").data('mute');
		var start = $("#bg-video").data('start');
		var repeat = $("#bg-video").data('repeat');
		var volume = $("#bg-video").data('volume');

		var hide_video = false;
		if(typeof(video_id) == "undefined" || video_id == "") {
			video_id = '1xxXp6utSB8';
			videoDefined = false;
			hide_video = true;
		}else{
			videoDefined = true;
			$('#bg-image').css('background-image', 'none');							
		}

		if(typeof(mute) == "undefined")
			mute = true;
			
		if(typeof(repeat) == "undefined")
			repeat = true;
						
		if(typeof(start) == "undefined")
			start = 2;

		if(typeof(volume) == "undefined")
			volume = 60;
	
		$('#bg-video').tubular({
			videoId: video_id, 
			mute: mute,
			start: start,
			repeat: repeat,
			wrapperZIndex: '1', 
		});
		

		if(mute == false) {
			var interval = setInterval(function() {
	
				if(typeof(player) != "undefined" && typeof(player.setVolume) != "undefined") {
					player.setVolume(volume);
					clearInterval(interval);
				}	
				
			}, 500);
		}
			
		
		if(hide_video)
			_hideVideo();
	
	}	


	// Logo Click
	$('#logo').click(function() {
		_scrollUp();
	});
	
	
	// init countdown
	var countdown_time = $("#countdown-widget").data('time');
	var countdown_timezone = $("#countdown-widget").data('timezone');
	
	if(countdown_time != '') {
		launchTime = new Date(Date.parse(countdown_time));
	}else{
		launchTime = new Date(); // Set launch: [year], [month], [day], [hour]...
		launchTime.setDate(launchTime.getDate() + 15); // Add 15 days
	}
	if(countdown_timezone == '')
		countdown_timezone = null;
			
	$("#countdown-widget").countdown({
		until: launchTime, 
		format: "dHMS",
		labels: ['','','','','','',''],
		digits:['0','1','2','3','4','5','6','7','8','9'],
		timezone: countdown_timezone,
		onTick: _onTick
	});
	

	// style checkboxes
	$('input:checkbox.screw').screwDefaultButtons({
	    image: 'url(assets/img/sprite_checkbox.png)',
	    width: 35,
	    height: 32
	});

	// Widgets Panel Close
	$("#widgets > span.close").click(function(event) {
		event.preventDefault();
		_scrollUp();
	});
	

	// init subscribe form
	$(".newsletter form").submit(function (event) {
		event.preventDefault();
		var postData = $(this).serialize();
		var status = $(".newsletter p");
		status.removeClass('shake');
		$.ajax({
			type: "POST",
			url: "home/subscription",
			data: postData,
			success: function(data) {

				if (data == "success")
					status.html("Thanks for your interest! We will let you know.").slideDown();
				
				else if (data == "subscribed")
					status.toggleClass('shake').html("This email is already subscribed.").slideDown();
				 
				else if (data == "invalid")
					status.toggleClass('shake').html("This email is invalid.").slideDown();
				
				else
					status.toggleClass('shake').html("Oups, something went wrong!").slideDown();	
				
			},
			error: function () {
				status.toggleClass('shake').html("Oups there was an error :/").slideDown();
			}
		});
	});
	
	if(!_isMobile()) {
	
		// init write us button - toggle contact form
		$(".writeus .toggle-button").toggle(function(event) {
			event.preventDefault();
			$(this).fadeOut();
			if(_isIE8Less()) {
				$('#widgets').find('div.section').not('.writeus').fadeOut("slow", function() {
					$('#contact_form').css({height:$(window).height() - $('div#header').height()}).fadeIn("slow", function() {
						$('#widgets').css('overflow-y', 'hidden');
						contact_form_open = true;
					});
				});

			}else{
				$('#contact_form').animate({height:$(window).height() - $('div#header').height()},800, function() {
					$('html,body').animate({scrollTop: $("#widgets").offset().top},'slow');
					$('#widgets').css('overflow-y', 'hidden');
					contact_form_open = true;
				});
			}
				
		},function(event) {
			event.preventDefault();
			var toggleButton = $(this);

			if(_isIE8Less()) {

				$('#contact_form').fadeOut("slow", function() {
					toggleButton.show();
					contact_form_open = false;
					$('#widgets').css('overflow-y', 'auto');
					$('#widgets').find('div.section').not('.writeus').fadeIn("slow");
				});

			}else{

				$('#contact_form').animate({height:'0'},800, function() {
					toggleButton.show();
					$('html,body').animate({scrollTop: $(".writeus").offset().top},'slow', function() {
						contact_form_open = false;
					});
				});
			}
			
		});
	}else{
	
		var contactFormHeight = $("#contact_form").height();
		var widgetsPaddingBottom = parseInt($("#widgets").css('padding-bottom').replace('px', ''));
		$("#contact_form").hide();
		$(".writeus .toggle-button").toggle(function(event) {

			$("#widgets").animate({'padding-bottom': widgetsPaddingBottom+contactFormHeight}, 400);
			$("#contact_form").slideDown();
			$('html,body').animate({scrollTop: $(".writeus").offset().top - $(".toggle-button").height()},'slow');
			
		},function(event) {
		
			event.preventDefault();
			
			$("#contact_form").slideUp("fast", function () {
				$("#widgets").animate({'padding-bottom': 65});
			});
			$('html,body').animate({scrollTop: $(".writeus").offset().top},'slow');

		});
		
		
	}
	
	$("#contact_form .wrap > span.close").click(function() {
		$(".writeus .toggle-button").click();
	});
	
	$("#contact_form .wrap .form .success > span.close").click(function() {
		$("#contact_form form")[0].reset();
		$("#contact_form .success").fadeOut();
	});

	
	// init contact form
	$("#contact_form form").submit(function (event) {
		event.preventDefault();
		var form = $(this);
		var postData = form.serialize();
		var status = form.parent().find(".status");
		var success = form.parent().find(".success");
		success.removeClass('shake')
		$.ajax({
			type: "POST",
			url: "home/writeus",
			data: postData,
			success: function(data) {
				if (data == "success") {
					status.html("").hide();
					success.fadeIn();
				} else if (data == "invalid") {
					status.toggleClass('shake').html("This email is invalid.").slideDown();
				} else {
					status.toggleClass('shake').html("Oups, something went wrong!").slideDown();	
				}
			},
			error: function () {
				status.toggleClass('shake').html("Oups, something went wrong!").slideDown();
			}
		});
	});
	

	$("#tweet").tweet({		
		modpath: './assets/js/twitter/',	
		username: _getTwitterUser(), // Your Twitter username
		count: 1, // Number of tweets to show up
		loading_text: "Loading tweets...",
		fetch: 10
	});
	$(".tweet_list").fadeIn();
	
	if(!_isMobile()) {
		setTimeout(function() {
			_resize();
			$('#countdown-widget').animate({opacity: 0.8,'margin-top':0}, 1000);
		},2000);	
	}else{
		$('#countdown-widget').css({opacity: 0.8,'margin-top':0});
	}	
		
		
	// init scroll
	if( _isMobile() ) {
		
		_initMobile();

	}else if( _isIpad() ) {

		_initIpad();	

	}else{
		
		_initDesktop();
	}	
	
	_stopLoading();

	$(window).bind('orientationchange resize', function(event){
		_resize();
	});
	
	// init nicescroll
	$("#widgets, #contact_form").niceScroll({
		'hidecursordelay' : 1000 // Default 400
	});
	_resize();

});

	
function _onTick() {
	var amount;
	$('.countdown_amount').each(function() {
		amount = parseInt($(this).text());
		if(amount < 10) {
			$(this).text('0'+amount);
		}
	});
	 
	if(_isMobile() && nbOfTicks < 3) {
		_resize();
	}	
	nbOfTicks++;
}
      
function _isMobile() {

	return (( /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent) ) || ($(window).width() <= 568));
}

function _isIpad() {

	return ( /iPad/i.test(navigator.userAgent));
}


function _isDesktop() {

	return (!_isIpad() && !_isMobile());
}

function _isIE() {

	//if(typeof($.browser.msie) != "undefined")
	//	return $.browser.msie;

	return false;
}

function _isIE8Less() {
	return _isIE() && (parseInt($.browser.version) <= 8)
}
function _isIE9Less() {
	return _isIE() && (parseInt($.browser.version) <= 9)
}

function _resize() {

	var winWidth = $(window).width();
	var winHeight = $(window).height();
	var headerHeight = $('div#header').height();
	var contentHeight = winHeight-headerHeight;
	var countdownHeight = $('#countdown-widget').height();

	if(!_isIpad()) {
		var prevWidth = $.cookie('prevWidth');
		if( (winWidth <= 568 && prevWidth > 568) || (winWidth > 568 && prevWidth <= 568)) {
			$.cookie('prevWidth', winWidth);
			prevWidth = $.cookie('prevWidth');
			if(prevWidth)
				location.reload(true);
		}
	}	

	var countdownPaddingTop = (contentHeight / 2) - (countdownHeight / 2) - headerHeight - 30;
	
	var realCountdownWidth = 0;
	$('#countdown-widget .countdown_section').each(function() {
		realCountdownWidth = realCountdownWidth + $(this).width();
	});
	var countdownLeft = ((winWidth - realCountdownWidth) / 2);
	
	if(!_isMobile()) {
		$('#content, div.section.countdown, #widgets').height(contentHeight);
		$('#countdown-widget').css({'left':countdownLeft, 'padding-top':countdownPaddingTop});
		if(contact_form_open) {
			$('#widgets').css('overflow-y', 'hidden');
			$('#contact_form').css({height:contentHeight});
		}else{
			$('#widgets').css('overflow-y', 'hidden');
		}	
	}else{

		$('#countdown-widget').css({'left':countdownLeft});
	}
	
	$("#widgets").getNiceScroll().resize();
	
	if(!showing_countdown)
		_scrollDown();
}

function _initMobile() {

	$("#content").removeAttr('style');

}

function _initIpad() {

	$("#content").removeAttr('style');
	$("div.section.countdown").unbind("touchstart");
	
	$("div.section.countdown").bind("touchstart", function() {
		_scrollDown();
	});
	
}

function _initDesktop() {
		
	$("#content").removeAttr('style');
	$("div.section.countdown").unbind("click");
	$("div.section.countdown").bind("click", function() {
		_scrollDown();
	});

	$("#content").unbind("mousewheel");
	$("#content").bind("mousewheel", function(event, delta, deltaX, deltaY) {

		event.preventDefault();
		
		if(in_motion == false) {
			
			in_motion = true;

			if(deltaY < 0) {
				_scrollDown();	
			}else{
				_scrollUp();	
			}
			
		}else{
			return false;
		}
			
	});

}

function _scrollDown() {

	$('#bg-overlay').stop().animate({'top':0}, 800);

	$('.scrolldown').stop().animate({opacity:0}, 400);
	
	var winHeight = $(window).height();
	$('#content').stop().animate({scrollTop: winHeight}, 800,function(){
		in_motion = false;	
		showing_countdown = false;
		$('#widgets').addClass('opened');		
		$('#content').removeClass('cursor');
	});
	
	if(_isIE8Less()) {
		$('#widgets div.section').fadeIn(1000);
	}else{
		$('#widgets div.section').stop().animate({'opacity':1}, 1800);
	}

	$('#countdown-widget').stop().animate({'margin-top':'100px', opacity:0}, 800);

}

function _scrollUp() {

	if(contact_form_open)
		$("#contact_form .close").click();
	
	$('#widgets').removeClass('opened');
	$('#bg-overlay').stop().animate({'top':'100%'}, 800);
	if(_isIE8Less()) {
		$('#widgets div.section').fadeOut(1000);
	}else{
		$('#widgets div.section').stop().animate({'opacity':0}, 800);
	}
	$('.scrolldown').stop().animate({opacity:0.8}, 400);
		
	$('#content').stop().animate({scrollTop: 0}, 800,function(){
		in_motion = false;
		showing_countdown = true;	
		$('#widgets').removeClass('opened');
		$('#content').addClass('cursor');
	});
	
	$('#countdown-widget').stop().animate({'margin-top':'0', opacity:0.8}, 800);	
}

function _startLoading() {

	$("#bg-loading").show().animate({opacity:1}, 500);
}

function _stopLoading() {

	timeout = 500;
	if(videoDefined && !firstLoad) {
		timeout = 4000;
		firstLoad = true;
	}
	$(window).load(function() {
		$("#bg-loading").delay(timeout).animate({opacity:0}, 500, function() {
		
			$(this).hide();
		});
	});	
	
}
function _tubularLoaded() {
	return ($('body').find('#tubular-container').length > 0);
}

function _hideVideo() {
	
	if(typeof(player) != "undefined")
		player.stopVideo();
		
	$('#tubular-container').hide();
	$('#tubular-shield').hide();
	
}

function _getTwitterUser() {

	var twitter_user = '';
	
	// init twitter feed - change to your username
	if($("#tweet").data('tweetuser') && $("#tweet").data('tweetuser') != '') {
		twitter_user = $("#tweet").data('tweetuser');
	}else{
		twitter_user = 'envato';
	}

	return twitter_user;
	
}