angular
    .module('app')
    .controller('usuarioEditCtrl', ['$scope', '$rootScope', '$routeParams', '$location', '$http', function ($scope, $rootScope, $routeParams, $location, $http) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.usuarioId = $routeParams.id;
        $scope.getUsuario = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Permisos/Usuario/' + $scope.usuarioId,
                { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.usuario = result.data;
                },
                function error(err) {
                    console.log(err);
                });
        }
        $scope.titleMsg = $scope.usuarioId > 0 ? "Editando Usuario" : "Agregando Usuario";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.resetPassword = function () {

            if ($scope.usuarioId > 0) {
                $scope.myPromise = $http.post('/Account/ResetUserPassword/', $scope.usuario, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                            $rootScope.success('Usuario', 'Datos guardados')
                            alert("Temporal Pass: " + result.data.tempPassword);
                        },
                        function error(err) {
                        });
            }
        }

        $scope.saveData = function (valid) {

            if (!valid) {

                return;

            }

            if ($scope.usuarioId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Permisos/Usuario/' + $scope.usuarioId, $scope.usuario, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Usuarios', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Permisos/Usuario/', $scope.usuario, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Usuarios', 'Registro guardado')
                        alert("Temporal Pass: " + result.data.password);
                        $rootScope.goTo("/usuario/" + result.data.user.id);
                        },
                    function error(err) {
                    });
            }
        }

        $scope.getUsuario();
    }]);