angular
    .module('app')
    .controller('rolEditCtrl', ['$scope', '$rootScope', '$http', 'rolId', function ($scope, $rootScope, $http, rolId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.rol = { name: '', description: ''}
        $scope.rolId = rolId
        $scope.getRol = function () {
            if ($scope.rolId > 0) {
                $scope.myPromise =  $http.get($rootScope.config.apiBase + '/Permisos/Rol/' + $scope.rolId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.rol = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.rolId > 0 ? "Editando Rol" : "Agregando Rol";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {

            if (!valid) {

                return;

            }

            if ($scope.rolId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Permisos/Rol/' + $scope.rolId, $scope.rol, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Rols', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Permisos/Rol/', $scope.rol, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Roles', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getRol();
    }]);