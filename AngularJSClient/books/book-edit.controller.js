angular
  .module('app')
  .controller(
    'BookEditController',
    function (BookService, AuthorService, $state, $stateParams) {
      var vm = this;

      vm.bookId = $stateParams.id;
      vm.book = null;
      vm.authors = [];
      vm.error = null;
      vm.availabilityOptions = [
        { label: 'Available', value: true },
        { label: 'Not available', value: false }
      ];

      vm.getBook = function () {
        BookService.getById(vm.bookId)
          .then(function (response) {
            vm.book = response.data;
            vm.book.publishDate = new Date(vm.book.publishDate);
          })
          .catch(function (error) {
            vm.error = error;
          });
        AuthorService.getAll()
          .then(function (response) {
            vm.authors = response.data;
          })
          .catch(function (error) {
            vm.error = error;
          });
      };

      vm.saveBook = function () {
        BookService.updateBook(vm.book)
          .then(function (response) {
            $state.go('books');
            vm.book = null;
          })
          .catch(function (error) {
            vm.error = error;
            console.log(error);
          });
      };

      vm.getBook();
    }
  );
