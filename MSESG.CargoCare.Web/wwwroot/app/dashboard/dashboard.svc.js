var dashboardService = angular.module('dashboardService', ['ngResource']);

// create the factory
dashboardService.factory('DashSvc', ['$resource','$rootScope', function ($resource, $rootScope) {
    return $resource(':url/:action', { action: "@action" }, {
      
    });
}]);