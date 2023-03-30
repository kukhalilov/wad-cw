angular.module('app').factory('BookService', function (BaseService) {
  var resourceUrl = 'http://localhost:24255/api/book';

  var service = angular.extend({}, BaseService, {
    getAll: getAll,
    getById: getById,
    addBook: addBook,
    updateBook: updateBook,
    removeBook: removeBook,
  });

  return service;

  function getAll(searchTerm, sortBy, sortAsc, page, pageSize) {
    return BaseService.getAll(resourceUrl, searchTerm, sortBy, sortAsc, page, pageSize);
  }

  function getById(id) {
    return BaseService.getById(resourceUrl, id);
  }

  function addBook(book) {
    return BaseService.add(resourceUrl, book);
  }

  function updateBook(book) {
    return BaseService.update(resourceUrl, book.bookId, book);
  }

  function removeBook(id) {
    return BaseService.remove(resourceUrl, id);
  }
});
