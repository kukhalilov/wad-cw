angular.module('app').factory('BaseService', function ($http) {
  var service = {
    getAll: getAll,
    getById: getById,
    add: add,
    update: update,
    remove: remove,
  };
  return service;

  function getAll(endpoint, searchTerm, sortBy, sortAsc, page, pageSize) {
    return $http.get(endpoint, {
      params: {
        searchTerm: searchTerm,
        sortBy: sortBy,
        sortAsc: sortAsc,
        page: page,
        pageSize: pageSize,
      },
    });
  }

  function getById(endpoint, id) {
    return $http.get(endpoint + '/' + id);
  }

  function add(endpoint, data) {
    return $http.post(endpoint, data);
  }

  function update(endpoint, id, data) {
    return $http.put(endpoint + '/' + id, data);
  }

  function remove(endpoint, id) {
    return $http.delete(endpoint + '/' + id);
  }
});
