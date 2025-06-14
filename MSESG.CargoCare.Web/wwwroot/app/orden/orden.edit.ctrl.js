angular
    .module('app')
    .controller('ordenEditCtrl', ['$scope', '$rootScope', '$http', '$location', '$routeParams', function ($scope, $rootScope, $http, $location, $routeParams) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = { ordern: {}, detalle: [] }
        $scope.fecha2 = false;
        $scope.ordenId = $routeParams.id;
        $scope.planificacionId = $routeParams.planificacionId;
        $scope.orden = [];
        $scope.detalle = [];
        $scope.choferes = [];
        $scope.camiones = [];
        $scope.cisternas = [];
        $scope.productos = [];
        $scope.terminales = [];
        $scope.sellos = [];
        $scope.cisternaDetalles = []
        $scope.choosedProducto = false;
        $scope.curCisterna = {};
        $scope.hstep = 1;
        $scope.mstep = 10;
        $scope.ismeridian = false;
        $scope.$watch('model.orden.camionId', function (n, o) { $scope.camionChange(n, o) }, true);
        $scope.$watch('model.orden.cisternaId', function (n, o) { $scope.cisternaChange(n, o) }, true);

        $scope.camionChange = function (n, o) {
            if (n != o && $scope.ordenForm.$dirty) {
                for (var i = 0; i < $scope.camiones.length; i++) {
                    if ($scope.camiones[i].key == n) {
                        $scope.model.orden.cisternaId = $scope.camiones[i].cisternaId
                        $scope.model.orden.choferId = $scope.camiones[i].choferId
                        break
                    }
                }

            }
        }

        $scope.cisternaChange = function (n, o) {
            if (n != o && $scope.ordenForm.$dirty) {
                $scope.model.detalle = []
                var tmp = []
                $scope.choosedProducto = false;
                for (var i = 0; i < $scope.cisternaDetalles.length; i++) {
                    if ($scope.cisternaDetalles[i].cisternaId == n) {

                        var detalle = $scope.cisternaDetalles[i];
                        tmp.push({
                            compartimento: detalle.compartimento,
                            cisternaDetalleId: detalle.id,
                            cantidadConduce: detalle.capacidad,
                            selloChapaManholeId: 0,
                            selloChapaManhole: '',
                            selloBocaCarga: '',
                            selloBocaCargaId: 0,
                            selloBocaDescargaId: 0,
                            selloBocaDescarga: '',
                            productoId: 0,
                            enUso: true
                        })
                    }
                }

                $scope.model.detalle = tmp.sort(function (a, b) { return a.compartimento - b.compartimento; })

                /// $scope.model.detalle = $filter('orderBy')($scope.model.detalle, 'compartimento')

            }
        }


        $scope.detalleChecked = function (d) {
            if (!d.enUso) {
                d.productoId = null;
                d.producto = null;

                d.cantidadConduce = null;
                d.selloChapaManholeId = null;
                d.selloChapaManhole = null;

                d.selloBocaCargaId = null;
                d.selloBocaCarga = null;

                d.selloBocaDesCargaId = null;
                d.selloBocaDescarga = null;
            }
        }

        $scope.selectSelloCallBack = function ($item, $model, detalle, target) {

            if (target === 0) {
                detalle.selloChapaManholeId = $item.id;
                var i = 0;
                if (detalle.compartimento == 1) {
                    $http.get($rootScope.config.apiBase + '/sello/GetSellosAfter', {
                        params: {
                            intSello: $item.intSello,

                        }
                    }).then(function (response) {

                        var data = response.data;
                        if (data && data.length > 0) {
                            var i = 0;
                            for (var j = 0; j < $scope.model.detalle.length; j++) {
                                console.log($scope.model.detalle[j])
                                if (j == 0) {
                                    if ($scope.model.detalle[j].selloBocaCargaId <= 0) {
                                        $scope.model.detalle[j].selloBocaCargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaCarga = data[i].codSello
                                        i++;
                                    }
                                    if ($scope.model.detalle[j].selloBocaDescargaId <= 0) {
                                        $scope.model.detalle[j].selloBocaDescargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaDescarga = data[i].codSello
                                        i++;
                                    }
                                } else {
                                    if ($scope.model.detalle[j].selloChapaManholeId <= 0) {
                                        $scope.model.detalle[j].selloChapaManholeId = data[i].id;
                                        $scope.model.detalle[j].selloChapaManhole = data[i].codSello
                                        i++;
                                    }
                                    if ($scope.model.detalle[j].selloBocaCargaId <= 0) {
                                        $scope.model.detalle[j].selloBocaCargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaCarga = data[i].codSello

                                        i++;
                                    }

                                    if ($scope.model.detalle[j].selloBocaDescargaId <= 0) {
                                        $scope.model.detalle[j].selloBocaDescargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaDescarga = data[i].codSello
                                        i++;
                                    }
                                }
                            }
                        }

                    });


                }


            } else if (target === 1) {
                detalle.selloBocaCargaId = $item.id;
            } else if (target === 2) {
                detalle.selloBocaDesCargaId = $item.id;
            }



        }

        $scope.getSellos = function (filter) {
            return $http.get($rootScope.config.apiBase + '/sello/Search', {
                params: {
                    filter: filter,
                    ordenId: $scope.ordenId
                }
            }).then(function (response) {
                return response.data
            });
        };

        $scope.ngModelOptionsSelected = function (value) {
            if (arguments.length) {
                _selected = value;
            } else {
                return _selected;
            }
        };

        $scope.productoChanged = function (detalle) {
            var nombre = ''
            for (var j = 0; j < $scope.productos.length; j++) {
                if ($scope.productos[j].key === detalle.productoId) {
                    nombre = $scope.productos[j].value;
                    break;
                }
            }
            detalle.producto = nombre

            //if (!$scope.choosedProducto) {
            //    $scope.choosedProducto = true;

            //    for (var i = 0; i < $scope.model.detalle.length; i++) {
            //        $scope.model.detalle[i].productoId = detalle.productoId
            //        $scope.model.detalle[i].producto =  nombre
            //    }
            //}
        }

        $scope.getOrden = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/orden/' + $scope.ordenId + '/' + $scope.planificacionId,
                { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    if (result.data.model.orden && result.data.model.orden.fecha) {
                        result.data.model.orden.fecha = moment.utc(result.data.model.orden.fecha).toDate();
                    }

                    $scope.model = result.data.model;
                    $scope.detalle = result.data.detalle;
                    $scope.choferes = result.data.choferes;
                    $scope.camiones = result.data.camiones;
                    $scope.cisternas = result.data.cisternas;
                    $scope.productos = result.data.productos;
                    $scope.sellos = result.data.sellos;
                    $scope.cisternaDetalles = result.data.cisternaDetalles;
                    $scope.terminales = result.data.terminales;

                },
                function error(err) {
                    console.log(err);
                });
        }
        $scope.titleMsg = $scope.ordenId > 0 ? "Editando orden" : "Agregando orden";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            if (!valid) {
                return;
            }

            if ($scope.model.orden.camionId <= 0 ||
                $scope.model.orden.choferId <= 0 ||
                $scope.model.orden.cisternaId <= 0) {
                $rootScope.error("Favor llenar camion, chofer y cisterna")
                return;
            }

            for (var i = 0; i < $scope.model.detalle.length; i++) {
                if ($scope.model.detalle[i].enUso &&
                    ($scope.model.detalle[i].productoId <= 0 || $scope.model.detalle[i].cantidad <= 0)) {
                    $rootScope.error("Favor indicar cantidad y producto para los compartimentos en uso")
                    return;
                }
            }

            if ($scope.ordenId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/orden/' + $scope.ordenId, $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('orden', 'orden guardado')
                        $rootScope.goTo('/ordenes')
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/orden/', $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('ordens', 'orden guardado')
                        $rootScope.goTo('/ordenes')
                    },
                    function error(err) {
                    });
            }
        }

        $scope.getOrden();
    }]);