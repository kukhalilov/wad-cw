angular.module('app').directive('appFooter', appFooter);

function appFooter() {
  return {
    restrict: 'E',
    controller: FooterController,
    controllerAs: 'vm',
    bindToController: true,
    templateUrl: 'common/footer.html',
  };
}

class FooterController {
    constructor() {
        var vm = this;

        vm.year = new Date().getFullYear();
    }
}
