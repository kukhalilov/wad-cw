angular.module('app').controller('AuthorListController', AuthorListController);

AuthorListController.$inject = ['AuthorService'];

function AuthorListController(AuthorService) {
  var vm = this;

  vm.searchTerm = '';
  vm.sortBy = 'name';
  vm.sortAsc = true;
  vm.pageSize = 5;
  vm.currentPage = 1;

  vm.authors = [];
  vm.delete = deleteAuthor;
  vm.authorToDeleteId = null;
  vm.totalItems = 0;
  vm.pages = [];
  vm.totalPages = 1;

  vm.loadAuthors = loadAuthors;

  vm.searchTermForHighlight = '';

  vm.setAuthorToDeleteId = function (authorId) {
    vm.authorToDeleteId = authorId;
  };

  vm.goToFirstPage = function () {
    vm.currentPage = 1;
    loadAuthors();
  };

  vm.goToLastPage = function () {
    vm.currentPage = vm.totalPages;
    loadAuthors();
  };

  vm.goToPreviousPage = function () {
    if (vm.currentPage > 1) {
      vm.currentPage--;
      loadAuthors();
      console.log(vm.authors);
    }
  };

  vm.goToNextPage = function () {
    if (vm.currentPage < vm.totalPages) {
      vm.currentPage++;
      loadAuthors();
      console.log(vm.authors);
    }
  };

  loadAuthors();

  function loadAuthors() {
    AuthorService.getAll(
      vm.searchTerm,
      vm.sortBy,
      vm.sortAsc,
      vm.currentPage,
      vm.pageSize
    )
      .then(function (response) {
        vm.authors = response.data;
        vm.totalItems = response.headers('Authors-Total-Count');
        vm.totalPages = Math.ceil(vm.totalItems / vm.pageSize);
        vm.pages = getPaginationPages(vm.currentPage, vm.totalPages);

        if (vm.totalPages === 0) {
          vm.totalPages = 1;
        }

        if (vm.currentPage > vm.totalPages) {
          vm.currentPage = 1;
          loadAuthors();
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

  function deleteAuthor() {
    AuthorService.removeAuthor(vm.authorToDeleteId)
      .then(function (response) {
        loadAuthors();
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}
