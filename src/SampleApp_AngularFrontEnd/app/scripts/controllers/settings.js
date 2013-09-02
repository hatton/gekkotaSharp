'use strict';

angular.module('SampleApp')
  .controller('SettingsCtrl', function ($scope, dialog) {

        $scope.save = function() {
            dialog.close($scope.book);
        };

        $scope.close = function(){
            dialog.close(undefined);
        };
  });
