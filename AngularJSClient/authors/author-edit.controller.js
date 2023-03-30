angular
  .module('app')
  .controller(
    'AuthorEditController',
    function (AuthorService, $state, $stateParams) {
      var vm = this;

      vm.authorId = $stateParams.id;
      vm.author = null;
      vm.error = null;

      vm.getAuthor = function () {
        AuthorService.getById(vm.authorId)
          .then(function (response) {
            vm.author = response.data;
          })
          .catch(function (error) {
            vm.error = error;
          });
      };

      vm.saveAuthor = function () {
        AuthorService.updateAuthor(vm.author)
          .then(function (response) {
            $state.go('authors');
            vm.author = null;
          })
          .catch(function (error) {
            vm.error = error;
            console.log(error);
          });
      };

      vm.getAuthor();
    }
  );
