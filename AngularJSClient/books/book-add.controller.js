angular.module('app').controller('BookAddController', BookAddController);

BookAddController.$inject = ['$state', 'BookService', 'AuthorService'];

function BookAddController($state, BookService, AuthorService) {
  var vm = this;

  vm.book = {};
  vm.authors = [];
  vm.error = null;
  vm.availabilityOptions = [
    { label: 'Available', value: true },
    { label: 'Not available', value: false },
  ];

  vm.getAuthors = function () {
    AuthorService.getAll()
      .then(function (response) {
        vm.authors = response.data;
      })
      .catch(function (error) {
        vm.error = error;
      });
  };

  // function to add a new book
  vm.addBook = function () {
    BookService.addBook(vm.book)
      .then(function (response) {
        // redirect to the book list page after successful creation
        $state.go('books');
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  vm.getAuthors();
}
