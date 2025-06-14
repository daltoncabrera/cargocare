angular
    .module('app')
    .controller('inspeccionPrintCtrl', ['$scope', '$rootScope', '$http', '$sce', 'url', function ($scope, $rootScope, $http, $sce, url) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Loading...";
      
        $scope.url = $sce.trustAsResourceUrl(url);
      
    }]);