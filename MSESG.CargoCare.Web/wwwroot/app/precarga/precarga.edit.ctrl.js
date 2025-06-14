angular
    .module('app')
    .controller('precargaEditCtrl', ['$scope', '$rootScope', '$http', '$location', '$routeParams', 'Popeye', function ($scope, $rootScope, $http, $location, $routeParams, Popeye) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = { ordern: {}, detalle: [] }
        $scope.fecha2 = false;
        $scope.precargaId = $routeParams.id;
        $scope.planificacionId = $routeParams.planificacionId;
        $scope.precarga = {};
        $scope.detalle = [];
        $scope.choferes = [];
        $scope.camiones = [];
        $scope.cisternas = [];
        $scope.productos = [];
        $scope.terminales = [];
        $scope.destinos = [];
        $scope.sellos = [];
        $scope.cisternaDetalles = []
        $scope.choosedProducto = false;
        $scope.curCisterna = {};
        $scope.hstep = 1;
        $scope.mstep = 10;
        $scope.ismeridian = false;
        $scope.$watch('model.precarga.camionId', function (n, o) { $scope.camionChange(n, o) }, true);
        $scope.$watch('model.precarga.cisternaId', function (n, o) { $scope.cisternaChange(n, o) }, true);
        console.log($routeParams)
        $scope.camionChange = function (n, o) {
            if (n != o && $scope.precargaForm.$dirty) {
                for (var i = 0; i < $scope.camiones.length; i++) {
                    if ($scope.camiones[i].key == n) {
                        $scope.model.precarga.cisternaId = $scope.camiones[i].cisternaId
                        $scope.model.precarga.choferId = $scope.camiones[i].choferId
                        break
                    }
                }

            }
        }

        $scope.cisternaChange = function (n, o) {
            if (n != o && $scope.precargaForm.$dirty) {
                var tmp = []
                $scope.choosedProducto = false;
                for (var i = 0; i < $scope.cisternaDetalles.length; i++) {
                    if ($scope.cisternaDetalles[i].cisternaId == n) {

                        var detalle = $scope.cisternaDetalles[i];

                    }
                }

            }
        }

        $scope.cancel = function () {
            if ($scope.$close) {
                $scope.$close();
            } else {
                $rootScope.goTo('/precargas');
            }
        }

        $scope.sumaCompartimentos = function (deta, no) {
            console.log(deta)
            for (var i = 0; i < $scope.model.detalle.length; i++) {

                if (no == 1 && $scope.model.detalle[i].compartimento1 > 0 && $scope.model.detalle[i].productoId != deta.productoId) {
                    return true;
                }
               
                if (no == 2 && $scope.model.detalle[i].compartimento2 > 0 && $scope.model.detalle[i].productoId != deta.productoId) {
                    return true;
                }

                if (no == 3 && $scope.model.detalle[i].compartimento3 > 0 && $scope.model.detalle[i].productoId != deta.productoId) {
                    return true;
                }

                if (no == 4 && $scope.model.detalle[i].compartimento4 > 0 && $scope.model.detalle[i].productoId != deta.productoId) {
                    return true;
                }

                if (no == 5 && $scope.model.detalle[i].compartimento5 > 0 && $scope.model.detalle[i].productoId != deta.productoId) {
                    return true;
                }


            }
            console.log('NA');
            return false;

        }

        $scope.print = function () {
            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Precarga/' + $scope.precargaId;
                    }
                }
            });

            // Show a spinner while modal is resolving dependencies
            $scope.showLoading = true;
            modal.resolved.then(function () {
                $scope.showLoading = false;
            });

            // Update user after modal is closed
            modal.closed.then(function () {

            });
        };

        $scope.getprecarga = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/precarga/' + $scope.precargaId + '/' + $scope.planificacionId,
                { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    if (result.data.model.precarga && result.data.model.precarga.fecha) {
                        result.data.model.precarga.fecha = moment(result.data.model.precarga.fecha).toDate();
                    }

                    $scope.model = result.data.model;
                    $scope.detalle = result.data.detalle;
                    $scope.choferes = result.data.choferes;
                    $scope.camiones = result.data.camiones;
                    $scope.cisternas = result.data.cisternas;
                    $scope.cisternaDetalles = result.data.cisternaDetalles;
                    $scope.terminales = result.data.terminales;
                    $scope.destinos = result.data.destinos;

                },
                function error(err) {
                    console.log(err);
                });
        }
        $scope.titleMsg = $scope.precargaId > 0 ? "Editando precarga" : "Agregando precarga";

        $scope.lineChanged = function (line) {
            line.cantidad = line.compartimento1 +
                line.compartimento2 +
                line.compartimento3 +
                line.compartimento4 +
                line.compartimento5 +
                line.compartimento6;
        }

        $scope.saveData = function (valid) {
            if (!valid) {
                return;
            }

            if ($scope.model.precarga.camionId <= 0 ||
                $scope.model.precarga.choferId <= 0 ||
                $scope.model.precarga.cisternaId <= 0) {
                $rootScope.error("Favor llenar camion, chofer y cisterna")
                return;
            }

            for (var i = 0; i < $scope.model.detalle.length; i++) {

            }
            if ($scope.precargaId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/precarga/' + $scope.precargaId, $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('precarga', 'precarga guardado')

                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/precarga/', $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('precargas', 'precarga guardado')
                            $scope.model = result.data;
                        },
                    function error(err) {
                    });
            }
        }

        $scope.getprecarga();
    }]);