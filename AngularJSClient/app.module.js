angular.module('app', ['ui.router']).config(function ($stateProvider) {
  // Set up the states
  $stateProvider
    .state('home', {
      url: '',
      templateUrl: 'home/home.html',
    })
    .state('homeSlash', {
      url: '/',
      templateUrl: 'home/home.html',
    })
    .state('books', {
      url: '/books',
      templateUrl: 'books/book-list.html',
      controller: 'BookListController',
      controllerAs: 'vm',
    })
    .state('addBook', {
      url: '/books/add',
      templateUrl: 'books/book-add.html',
      controller: 'BookAddController',
      controllerAs: 'vm',
    })
    .state('editBook', {
      url: '/books/:id/edit',
      templateUrl: 'books/book-edit.html',
      controller: 'BookEditController',
      controllerAs: 'vm',
    })
    .state('bookDetails', {
      url: '/books/:id',
      templateUrl: 'books/book-details.html',
      controller: 'BookDetailsController',
      controllerAs: 'vm',
    })
    .state('authors', {
      url: '/authors',
      templateUrl: 'authors/author-list.html',
      controller: 'AuthorListController',
      controllerAs: 'vm',
    })
    .state('addAuthor', {
      url: '/authors/add',
      templateUrl: 'authors/author-add.html',
      controller: 'AuthorAddController',
      controllerAs: 'vm',
    })
    .state('editAuthor', {
      url: '/authors/:id/edit',
      templateUrl: 'authors/author-edit.html',
      controller: 'AuthorEditController',
      controllerAs: 'vm',
    })
    .state('authorDetails', {
      url: '/authors/:id',
      templateUrl: 'authors/author-details.html',
      controller: 'AuthorDetailsController',
      controllerAs: 'vm',
    });
});
