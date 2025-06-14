angular
    .module('app')
    .controller('verificacionEditCtrl', ['$scope', '$rootScope', '$http', '$routeParams', function ($scope, $rootScope, $http, $routeParams) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = {}
        $scope.choferes = [];
        $scope.camiones = [];
        $scope.cisternas = []
        $scope.verificacionId = $routeParams.id

        $scope.itemChange = function(item, ord, isYesNo) {
            if (isYesNo) {
                item.si = item.si && ord === 1;
                item.no = item.no && ord === 2;
            } else {
                item.bueno = item.bueno && ord === 1;
                item.regular = item.regular && ord === 2;
                item.malo = item.malo && ord === 3;
            }
        }

        $scope.getverificacion = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/verificacion/' + $scope.verificacionId,
                { 'Content-Type': 'application/json' })
                .then(function success(result) {

                        if (result.data.model.verificacion.fecha) {
                            result.data.model.verificacion.fecha =
                                moment.utc(result.data.model.verificacion.fecha).toDate();
                        }

                        $scope.model = result.data.model;
                    $scope.choferes = result.data.choferes;
                    $scope.camiones = result.data.camiones;
                    $scope.cisternas = result.data.cisternas;
                },
                function error(err) {
                    console.log(err);
                });
        }
        $scope.titleMsg = $scope.verificacionId > 0 ? "Editando verificacion" : "Agregando verificacion";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.verificacionId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/verificacion/' + $scope.verificacionId, $scope.model, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $rootScope.success('verificaciones', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/verificacion/', $scope.model, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $rootScope.success('verificaciones', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getverificacion();
    }]);