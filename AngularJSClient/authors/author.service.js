angular.module('app').factory('AuthorService', function (BaseService) {
  var resourceUrl = 'http://localhost:24255/api/author';

  var service = angular.extend({}, BaseService, {
    getAll: getAll,
    getById: getById,
    addAuthor: addAuthor,
    updateAuthor: updateAuthor,
    removeAuthor: removeAuthor,
  });

  return service;

  function getAll(searchTerm, sortBy, sortAsc, page, pageSize) {
    return BaseService.getAll(resourceUrl, searchTerm, sortBy, sortAsc, page, pageSize);
  }

  function getById(id) {
    return BaseService.getById(resourceUrl, id);
  }

  function addAuthor(author) {
    return BaseService.add(resourceUrl, author);
  }

  function updateAuthor(author) {
    return BaseService.update(resourceUrl, author.authorId, author);
  }

  function removeAuthor(id) {
    return BaseService.remove(resourceUrl, id);
  }
});
