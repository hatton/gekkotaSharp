'use strict';

var SampleApp = angular.module('SampleApp', ["ngResource", "ui.bootstrap"])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .when('/settings', {
        templateUrl: 'views/settings.html',
        controller: 'SettingsCtrl'
      })
      .when('/EditBook', {
        templateUrl: 'views/EditBook.html',
        controller: 'EditBookCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  });

SampleApp.factory('Books', function ($resource) {
    return $resource('/api/books/:id', //review posting (but not update/putting) worked without the /:id as well
        {}, { 'update': { method: 'PUT' } });
});

SampleApp.directive('rightClick', function ($parse) { //nb: the actual attribute must be "right-click". I can't explain the discrepency between that and the needed "rightClick" here.
    return function (scope, element, attr) {
        element.bind('contextmenu', function (event) {
            event.preventDefault();
            var fn = $parse(attr['rightClick']);
            scope.$apply(function () {
                fn(scope, {
                    $event: event
                });
            });
            return false;
        });
    }
});

//Note: this just shows the menu. The actual commands are run from the links of the menu items
SampleApp.directive('context', [function() {
    return {
        restrict    : 'A',
        scope       : '@&',
        compile: function compile(tElement, tAttrs, transclude) {
            return {
                post: function postLink(scope, iElement, iAttrs, controller) {
                    var ul = $('#' + iAttrs.context),
                        last = null;

                    ul.css({ 'display' : 'none'});

                    $(iElement).bind('contextmenu', function (event) {
                        event.preventDefault();//don't show the normal browser context menu
                        ul.css({
                            position: "fixed",
                            display: "block",
                            left: event.clientX + 'px',
                            top:  event.clientY + 'px'
                        });
                        last = event.timeStamp;
                    });

                    $(document).click(function(event) {
                        var target = $(event.target);
                        //Are we clicking on the menu? If not, hide the context menu
                        if(!target.is(".popover") && !target.parents().is(".popover")) {
                            if(last === event.timeStamp)
                                return;
                            ul.css({
                                'display' : 'none'
                            });

                        }
                    });
                }
            };
        }
    };
}]);

//review: adding functions here is probably not angularjs best practice (but I haven't learned what the correct way would be, just yet)
SampleApp.run(function ($rootScope) {

    //lets you write ng-click="log('testing')"
    $rootScope.log = function (variable) {
        console.log(variable);
    };

    //lets you write ng-click="alert('testing')"
    $rootScope.alert = function (text) {
        alert(text);
    }
});


