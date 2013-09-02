'use strict';

angular.module('SampleApp')
  .controller('EditBookCtrl', function ($scope, book, dialog) {
        $scope.book = book;

        $scope.save = function() {
            dialog.close($scope.book);
        };

        $scope.close = function(){
            dialog.close(undefined);
        };
  });
