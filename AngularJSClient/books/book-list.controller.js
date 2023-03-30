angular.module('app').controller('BookListController', BookListController);

BookListController.$inject = ['BookService'];

function BookListController(BookService) {
  var vm = this;

  vm.searchTerm = '';
  vm.sortBy = 'title';
  vm.sortAsc = true;
  vm.pageSize = 5;
  vm.currentPage = 1;

  vm.books = [];
  vm.delete = deleteBook;
  vm.bookToDeleteId = null;
  vm.totalItems = 0;
  vm.pages = [];
  vm.totalPages = 1;

  vm.loadBooks = loadBooks;

  vm.searchTermForHighlight = '';

  vm.setBookToDeleteId = function (bookId) {
    vm.bookToDeleteId = bookId;
  };

  vm.goToFirstPage = function () {
    vm.currentPage = 1;
    loadBooks();
  };

  vm.goToLastPage = function () {
    vm.currentPage = vm.totalPages;
    loadBooks();
  };

  vm.goToPreviousPage = function () {
    if (vm.currentPage > 1) {
      vm.currentPage--;
      loadBooks();
    }
  };

  vm.goToNextPage = function () {
    if (vm.currentPage < vm.totalPages) {
      vm.currentPage++;
      loadBooks();
    }
  };

  loadBooks();

  function loadBooks() {
    BookService.getAll(
      vm.searchTerm,
      vm.sortBy,
      vm.sortAsc,
      vm.currentPage,
      vm.pageSize
    )
      .then(function (response) {
        vm.books = response.data;
        vm.totalItems = response.headers('Books-Total-Count');
        vm.totalPages = Math.ceil(vm.totalItems / vm.pageSize);
        vm.pages = getPaginationPages(vm.currentPage, vm.totalPages);

        if (vm.totalPages === 0) {
          vm.totalPages = 1;
        }

        if (vm.currentPage > vm.totalPages) {
          vm.currentPage = 1;
          loadBooks();
        }

        if (vm.searchTerm) {
          vm.searchTermForHighlight = vm.searchTerm;
        } else {
          vm.searchTermForHighlight = '';
        }
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  function deleteBook() {
    BookService.removeBook(vm.bookToDeleteId)
      .then(function (response) {
        loadBooks();
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}
