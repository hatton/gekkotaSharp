'use strict';

angular.module('SampleApp')
  .controller('MainCtrl', function ($scope, Books) {
      $scope.books = Books.query();

      $scope.NewBook = function () {
          var x = new Books();
          x.Name = 'newbie';
          x.$save(function(){
            $scope.books = Books.query();
          });
      }

      $scope.delete = function (book) {
              var bookId = this.book.Id;
              Books.delete({ Id: bookId }, function () {
                    $("#item_" + bookId).fadeOut();
                    $scope.books = Books.query();
              });
      }

        $scope.hello = function (book) {
            alert(book.Name);
        }
  });
