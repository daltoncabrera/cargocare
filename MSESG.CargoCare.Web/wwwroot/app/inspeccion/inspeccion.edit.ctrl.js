angular
    .module('app')
    .controller('inspeccionEditCtrl', ['$scope', '$rootScope', '$http', '$location', '$routeParams', '$filter', 'Popeye', function ($scope, $rootScope, $http, $location, $routeParams,$filter, Popeye) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = { inspeccion: {}, detalle: [] }
        $scope.fecha2 = false;
        $scope.fecha3 = false;
        $scope.inspeccionId = $routeParams.id;
        $scope.precargaId = $routeParams.precargaId;
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
        $scope.$watch('model.inspeccion.camionId', function (n, o) { $scope.camionChange(n, o) }, true);
        $scope.$watch('model.inspeccion.cisternaId', function (n, o) { $scope.cisternaChange(n, o) }, true);

        $scope.camionChange = function (n, o) {
            if (n != o && $scope.inspeccionForm.$dirty) {
                for (var i = 0; i < $scope.camiones.length; i++) {
                    if ($scope.camiones[i].key == n) {
                        $scope.model.inspeccion.cisternaId = $scope.camiones[i].cisternaId
                        $scope.model.inspeccion.choferId = $scope.camiones[i].choferId
                        break
                    }
                }

            }
        }

        $scope.cisternaChange = function (n, o) {
            if (n != o && $scope.inspeccionForm.$dirty) {
              
                $scope.model.detalle = []
                var tmp = []
                $scope.choosedProducto = false;
                console.log($scope.cisternaDetalles)
                for (var i = 0; i < $scope.cisternaDetalles.length; i++) {
                    console.log(n)
                    console.log($scope.cisternaDetalles[i].cisternaId)
                    if ($scope.cisternaDetalles[i].cisternaId == n) {
                        var detalle = $scope.cisternaDetalles[i];
                        tmp.push({
                            compartimentoId: detalle.compartimento,
                            cisternaDetalleId: detalle.id,
                            capacidad: detalle.capacidad,
                            selloChapaManholeId: null,
                            selloChapaManhole: null,
                            selloBocaCarga: null,
                            selloBocaCargaId: null,
                            selloBocaDescargaId: null,
                            selloBocaDescarga: null,
                            productoId: null,
                            cantidad: detalle.capacidad,
                            cantidadDespachada: detalle.capacidad,
                            enUso: true,
                            hasChapa: detalle.hasChapa,
                            hasBocaCarga: detalle.hasBoca,
                            hasBocaDescarga: detalle.hasDescarga,
                    })
                    }
                }

                $scope.model.detalle = tmp.sort(function (a, b) { return a.compartimento - b.compartimento; })
                $scope.model.detalle = $filter('orderBy')($scope.model.detalle, 'compartimento')

            }
        }

        $scope.selectSelloCallBack = function ($item, $model, detalle, target) {
            if (target === 0) {
                detalle.selloChapaManholeId = $item.id;
                var i = 0;
                if (detalle.compartimentoId == 1) {
                    $http.get($rootScope.config.apiBase + '/sello/GetSellosAfter', {
                        params: {
                            intSello: $item.intSello,

                        }
                    }).then(function (response) {
                        var data = response.data;
                        if (data && data.length > 0) {
                            var i = 0;
                            for (var j = 0; j < $scope.model.detalle.length; j++) {
                                if (j == 0) {
                                    if ($scope.model.detalle[j].enUso && $scope.model.detalle[j].selloBocaCargaId <= 0 && $scope.model.detalle[j].hasBocaCarga === true) {
                                        $scope.model.detalle[j].selloBocaCargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaCarga = data[i].codSello
                                        i++;
                                    }
                                    if ($scope.model.detalle[j].enUso && $scope.model.detalle[j].selloBocaDescargaId <= 0 && $scope.model.detalle[j].hasBocaDescarga === true) {
                                        $scope.model.detalle[j].selloBocaDescargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaDescarga = data[i].codSello
                                        i++;
                                    }
                                } else {
                                    if ($scope.model.detalle[j].enUso && $scope.model.detalle[j].selloChapaManholeId <= 0 && $scope.model.detalle[j].hasChapa === true) {
                                        $scope.model.detalle[j].selloChapaManholeId = data[i].id;
                                        $scope.model.detalle[j].selloChapaManhole = data[i].codSello
                                        i++;
                                    }
                                    if ($scope.model.detalle[j].enUso && $scope.model.detalle[j].selloBocaCargaId <= 0 && $scope.model.detalle[j].hasBocaCarga === true) {
                                        $scope.model.detalle[j].selloBocaCargaId = data[i].id;
                                        $scope.model.detalle[j].selloBocaCarga = data[i].codSello
                                        i++;
                                    }

                                    if ($scope.model.detalle[j].enUso && $scope.model.detalle[j].selloBocaDescargaId <= 0 && $scope.model.detalle[j].hasBocaDescarga === true) {
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

        $scope.ngModelOptionsSelected = function (value) {
            if (arguments.length) {
                _selected = value;
            } else {
                return _selected;
            }
        };

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

        $scope.productoChanged = function (productoId) {
            if (!$scope.choosedProducto) {
                $scope.choosedProducto = true;

                for (var i = 0; i < $scope.model.detalle.length; i++) {
                    $scope.model.detalle[i].productoId = productoId
                }
            }
        }

        $scope.getinspeccion = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/inspeccion/' + $scope.inspeccionId + '/' + $scope.precargaId,
                { 'Content-Type': 'application/json' })
                .then(function success(result) {
                        if (result.data.model.inspeccion && result.data.model.inspeccion.fechaInicio) {
                        result.data.model.inspeccion.fechaInicio =
                            moment(result.data.model.inspeccion.fechaInicio).toDate();
                    }

                    //bug aqui hay la fecha final tendra que ser el mismo dia de la fecha inicial nos interesa la hora por el momento
                    if (result.data.model.inspeccion && result.data.model.inspeccion.fechaFin) {
                        result.data.model.inspeccion.fechaFin =
                            moment(result.data.model.inspeccion.fechaFin).toDate();
                    }

                    $scope.model = result.data.model;
                    $scope.choferes = result.data.choferes;
                    $scope.camiones = result.data.camiones;
                    $scope.cisternas = result.data.cisternas;
                    $scope.cisternaDetalles = result.data.cisternaDetalles;
                    $scope.productos = result.data.productos;
                    $scope.sellos = result.data.sellos;
                    $scope.sellos = result.data.sellos;
                    $scope.terminales = result.data.terminales;
                    $scope.destinos = result.data.destinos;

                },
                function error(err) {
                    console.log(err);
                });
        }
        $scope.titleMsg = $scope.inspeccionId > 0 ? "Editando inspeccion" : "Agregando inspeccion";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            if (!valid) {
                $rootScope.error("Errores en el formulario, favor revisar datos")
                return;
            }
            
            if ($scope.model.inspeccion.camionId <= 0 ||
                $scope.model.inspeccion.choferId <= 0 ||
                $scope.model.inspeccion.cisternaId <= 0) {
                $rootScope.error("Favor llenar camion, chofer y cisterna")
                return;
            }


            if ($scope.model.inspeccion.llenaDetalle) {
                for (var i = 0; i < $scope.model.detalle.length; i++) {
                    if ($scope.model.detalle[i].enUso &&
                    ($scope.model.detalle[i].productoId <= 0 ||
                        $scope.model.detalle[i].cantidadConduce <= 0 ||
                        $scope.model.detalle[i].cantidadMedida <= 0)) {
                        $rootScope.error(
                            "Favor indicar cantidad/cantidad medida y producto para los compartimentos en uso")
                        return;
                    }
                }
            } else {
                if ($scope.model.inspeccion.cantidad <= 0 || $scope.model.inspeccion.productoId <= 0) {
                    $rootScope.error(
                        "Favor indicar producto y cantidad")
                    return;
                }
            }

            if ($scope.inspeccionId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/inspeccion/' + $scope.inspeccionId, $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('inspeccion', 'inspeccion guardado')
                        
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/inspeccion/', $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('inspeccions', 'inspeccion guardado')
                        $rootScope.goTo('/inspeccion/' + result.data.id)
                        $scope.inspeccionId = result.data.id;
                        
                        },
                    function error(err) {
                    });
            }
        }


        $scope.print = function () {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Inspeccion/' + $scope.inspeccionId;
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
                $scope.refreshData();
            });
        };



        

        $scope.getinspeccion();
    }]);