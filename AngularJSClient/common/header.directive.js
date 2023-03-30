angular.module('app').directive('appHeader', function () {
  return {
    restrict: 'E',
    templateUrl: 'common/header.html',
    controller: function () {},
    controllerAs: 'vm',
    bindToController: true,
  };
});
