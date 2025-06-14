angular
    .module('app')
    .controller('productoEditCtrl', ['$scope', '$rootScope', '$http', 'productoId', function ($scope, $rootScope, $http, productoId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.producto = { nombre: '', telefono: '', direccion: '', multipleInstances: false}
        $scope.productoId = productoId
        $scope.getproducto = function () {
            if ($scope.productoId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/producto/' + $scope.productoId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.producto = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.productoId > 0 ? "Editando producto" : "Agregando producto";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.productoId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/producto/' + $scope.productoId, $scope.producto, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('productoes', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/producto/', $scope.producto, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('productoes', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getproducto();
    }]);