angular
    .module('app')
    .controller('choferEditCtrl', ['$scope', '$rootScope', '$http', 'choferId', function ($scope, $rootScope, $http, choferId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.chofer = { nombre: '', email: '', contacto: '', telefono: '', fax: '', logo: '' }
        $scope.choferId = choferId
        $scope.getEmpresa = function () {
            if ($scope.choferId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/chofer/' + $scope.choferId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.chofer = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.choferId > 0 ? "Editando chofer" : "Agregando chofer";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.choferId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/chofer/' + $scope.choferId, $scope.chofer, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('chofer', 'chofer guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/chofer/', $scope.chofer, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('chofers', 'chofer guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getEmpresa();
    }]);