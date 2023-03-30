angular
  .module('app')
  .controller('AuthorAddController', AuthorAddController);

AuthorAddController.$inject = ['$state', 'AuthorService'];

function AuthorAddController($state, AuthorService) {
  var vm = this;

  // function to create a new author
  vm.addAuthor = function () {
    var author = {
      name: vm.author.name,
      surname: vm.author.surname,
    };

    AuthorService.addAuthor(author)
      .then(function (response) {
        // redirect to the author list page after successful creation
        $state.go('authors');
      })
      .catch(function (error) {
        console.log(error);
        console.log(author)
      });
  };
}
