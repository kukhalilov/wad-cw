angular
  .module('app')
  .controller('BookDetailsController', BookDetailsController);

BookDetailsController.$inject = ['$state', '$stateParams', 'BookService'];

function BookDetailsController($state, $stateParams, BookService) {
  var vm = this;
  vm.delete = deleteBook;

  // Get the book ID from the URL parameter
  var bookId = $stateParams.id;

  // Retrieve the book details from the BookService
  BookService.getById(bookId)
    .then(function (book) {
      vm.book = book.data;
    })
    .catch(function (error) {
      console.log(error);
    });

  function deleteBook() {
    BookService.removeBook(bookId)
      .then(function (response) {
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        $state.go('books');
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}
