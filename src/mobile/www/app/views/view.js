define([], function () {
  
  var APP = window.APP = window.APP || {};
  
  var View = kendo.Class.extend({
    init: function (name, template, model, events) {
      
      // append the template to the DOM
      this.html = $(template).appendTo(document.body);

      // expose the model and events off the global scope
      APP[name] = { model: model || {}, events: events || {} };
    }
  });

  return View;

});