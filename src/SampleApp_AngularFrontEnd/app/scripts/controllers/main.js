'use strict';

angular.module('SampleApp')
  .controller('MainCtrl', function ($scope, $dialog, Books) {
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

        $scope.showSettings = function(){ //TODO add some settings data
            var options = {
                templateUrl: 'views/settings.html',
                controller: 'SettingsCtrl'
            };
            var d = $dialog.dialog(options);
            d.open().then(function(result){
                if(result)
                {
                    alert('dialog closed with result: ' + result);
                }
            });
        };

        $scope.showEditor = function (book) {
            var staticDialogOptions = {
                templateUrl: 'views/EditBook.html',
                controller: 'EditBookCtrl'
            };

            var dialogOptions  = angular.extend(staticDialogOptions, {
                //resolve: members that will be resolved and passed to the controller as locals
                resolve: {
                    book: function() {
                        //make a copy in case they hit cancel
                        return angular.copy(book);
                    }
                }
            });


            $dialog.dialog(dialogOptions)
                .open()
                .then(function(result) {
                    //the dialog returns the edited item if the user clicks OK
                    if(result) {
                        angular.copy(result, book);
                        book.$update();//book.$save();
                    }
                });
        }
  });


/* For later. This example shows getting at the current list item and cancelling changes:      http://plnkr.co/edit/MsXGuM?p=preview

$dialog.dialog(angular.extend(dialogOptions, {
 resolve: {item: function() {return angular.copy(itemToEdit);}}
 }))
 .open()
 .then(function(result) {
 if(result) {
 angular.copy(result, itemToEdit);
 }
 itemToEdit = undefined;
 });
 };*/