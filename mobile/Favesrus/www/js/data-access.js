FavesApp.dataAccess = (function () {

    //options input parameter will have all the data needed
    //to perform the ajax call
    function callService(options) {

        $.ajax({
            url: options.url,
            type: options.requestType,
            data: options.data,
            dataType: options.dataType,
            //Add HTTP headers if configured
            beforeSend: function (xhr) {
                if (typeof options.httpHeader !== 'undefined' && typeof options.headerValue !== 'undefined')
                    xhr.setRequestHeader(options.httpHeader, options.headerValue);
            },
            //on successful ajax call back
            success: function (resultData, status, xhr) {
                var result = {
                    data: resultData,
                    success: true
                };
                options.callBack(result);
            },
            //Callback function in case of an error
            error: function (xhr, status, errorThrown) {

                switch (xhr.status) {
                case '401':
                    alert('401 Unauthorized access detected. Please check the credentials you entered.' + errorThrown);
                    break;
                case '500':
                    alert('500 Intertnal Server Error. Please check the service code.' + errorThrown);
                    break;
                default:
                    alert('Unexpected error: ' + errorThrown);
                    break;
                }
                var result = {
                    success: false
                };
                options.callBack(result);
            }
        });
    }
    return {
        callService: callService
    }
})();