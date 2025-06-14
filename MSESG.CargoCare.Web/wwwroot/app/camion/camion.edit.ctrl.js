angular
    .module('app')
    .controller('camionEditCtrl', ['$scope', '$rootScope', '$http', 'camionId', function ($scope, $rootScope, $http, camionId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.cisternas = []
        $scope.choferes = []
        $scope.camion = { }
        $scope.camionId = camionId
        $scope.getEmpresa = function () {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/camion/' + $scope.camionId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.camion = result.data.camion;
                        $scope.cisternas = result.data.cisternas;
                        $scope.choferes = result.data.choferes;
                    },
                    function error(err) {
                        console.log(err);
                    });
        }
        $scope.titleMsg = $scope.camionId > 0 ? "Editando camion" : "Agregando camion";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.camionId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/camion/' + $scope.camionId, $scope.camion, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('camion', 'camion guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/camion/', $scope.camion, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('camions', 'camion guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getEmpresa();
    }]);