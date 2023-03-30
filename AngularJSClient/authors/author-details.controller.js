angular
  .module('app')
  .controller('AuthorDetailsController', AuthorDetailsController);

AuthorDetailsController.$inject = ['$state', '$stateParams', 'AuthorService'];

function AuthorDetailsController($state, $stateParams, AuthorService) {
  var vm = this;
  vm.delete = deleteAuthor;

  // Get the author ID from the URL parameter
  var authorId = $stateParams.id;

  // Retrieve the author details from the AuthorService
  AuthorService.getById(authorId)
    .then(function (author) {
      vm.author = author.data;
    })
    .catch(function (error) {
      console.log(error);
    });

  function deleteAuthor() {
    AuthorService.removeAuthor(authorId)
      .then(function (response) {
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        $state.go('authors');
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}
