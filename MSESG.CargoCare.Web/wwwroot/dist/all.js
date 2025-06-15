angular
    .module('app')
    .run(['$location', '$rootScope', '$window', 'toasty', '$timeout', 'Upload', function ($location, $rootScope, $window, toasty, $timeout,Upload) {

        $rootScope.errorTriggered = false;
        $rootScope.loadingMsg = "";
        $rootScope.config = { apiBase: location.origin + '/api' };
        $rootScope.title = '';
        $rootScope.navName = '';
        $rootScope.parentName = '';
        $rootScope.parentTitle = '';

        $rootScope.init = function (defaults) {

        }


        $rootScope.timePickerOptions = {
            step: 15,
            minTime: '7:00am',
            maxTime: '5:00pm',
            timeFormat: 'g:ia',
            appendTo: 'body'
        };

        $rootScope.hasClaim = function () {
            var resutl = false;
            for (var i = 0; i < arguments.length; i++) {
                if ($rootScope.info.claims.indexOf(arguments[i]) >= 0) {
                    resutl = true;
                    break;
                }
            }

            return resutl;
        }

        $rootScope.goTo = function (path) {
            $location.path(path);
        }

        $rootScope.goToRoot = function (path) {
            var url = path;
            window.location = url;
        }
        $rootScope.$on("$routeChangeSuccess", function (event, currentRoute, previousRoute) {
            $rootScope.title = currentRoute.title;
            $rootScope.navName = currentRoute.navName;
            $rootScope.parentName = currentRoute.parentName;
            $rootScope.parentTitle = currentRoute.parentTitle;

        });

        $rootScope.uploadFile = function (file, url, callback) {
            file.upload = Upload.upload({
                url: url,
                data: { file: file },
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                    if (callback)
                        callback(response.data);
                });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    $rootScope.error(":(", errorMsg)
                }
            }, function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }


        $rootScope.success = function (title, msg) {
            toasty["success"]({
                title: title,
                msg: msg,
                showClose: true,
                clickToClose: false,
                sound: true,
                html: false,
                shake: false,
                onRemove: function () {

                }
            });
        }

        $rootScope.error = function (title, msg) {

            toasty["error"]({
                title: title,
                msg: msg,
                showClose: true,
                clickToClose: false,
                sound: true,
                html: false,
                shake: false,
                onRemove: function () {

                }
            });




        };

    }]);


angular.module('app').config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push("responseInterceptor");
}]);

angular.module('app').config(['toastyConfigProvider', 'calendarConfig', function (toastyConfigProvider, calendarConfig) {
    toastyConfigProvider.setConfig({
        sound: true,
        position: 'top-right',
        shake: true,
        timeout: 5000
    });

    // Use either moment or angular to format dates on the calendar. Default angular. Setting this will override any date formats you have already set.
    calendarConfig.dateFormatter = 'moment';

    // This will configure times on the day view to display in 24 hour format rather than the default of 12 hour
    //calendarConfig.allDateFormats.moment.date.hour = 'HH:mm a';

    // This will configure the day view title to be shorter
    // calendarConfig.allDateFormats.moment.title.day = 'ddd D MMM';
    calendarConfig.allDateFormats.moment.date.time = 'hh:mma';

    // This will set the week number hover label on the month view
    calendarConfig.i18nStrings.weekNumber = 'Week {week}';


    // Make the week view more like the day view, ***with the caveat that event end times are ignored***.
    calendarConfig.showTimesOnWeekView = true;

}]);




angular
    .module('app')
    .config(['$routeProvider', '$httpProvider', '$locationProvider', function ($routeProvider, $httpProvider, $locationProvider) {

        //initialize get if not there
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }

        //disable IE ajax request caching
        $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
        // extra
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
        $httpProvider.defaults.headers.get["X-Requested-With"] = "XMLHttpRequest"

        $routeProvider
            // home
            .when("/", { templateUrl: "dashboard/dashboard.html", controller: "DashboardCtrl", title: 'Dashboard', navName: 'dashboard', parentName: '' })
            .when("/empresas", { templateUrl: "empresa/empresa.ctrl.html", controller: "empresaCtrl", parentTitle:'Configuracion',  title: 'Empresas', navName: 'empresas', parentName: 'configuracion' })
            .when("/empresa/:id", { templateUrl: "empresa/empresa.edit.ctrl.html", controller: "empresaEditCtrl", parentTitle: 'Configuracion', title: 'Empresas', navName: 'empresa', parentName: 'configuracion' })
            .when("/clientes", { templateUrl: "cliente/cliente.ctrl.html", controller: "clienteCtrl", parentTitle: 'Configuracion', title: 'Clientes', navName: 'clientes', parentName: 'configuracion' })
            .when("/cliente/:id", { templateUrl: "cliente/cliente.edit.ctrl.html", controller: "clienteEditCtrl", parentTitle: 'Configuracion', title: 'Creacion/Edicion Cliente', navName: 'cliente', parentName: 'configuracion' })
            .when("/terminales", { templateUrl: "terminal/terminal.ctrl.html", controller: "terminalCtrl", parentTitle: 'Configuracion',  title: 'Terminales', navName: 'terminal', parentName: 'configuracion' })
            .when("/productos", { templateUrl: "producto/producto.ctrl.html", controller: "productoCtrl", parentTitle: 'Configuracion',  title: 'Productos', navName: 'producto', parentName: 'configuracion' })
            .when("/sellos", { templateUrl: "sello/sello.ctrl.html", controller: "selloCtrl", parentTitle: 'Clientes', title: 'Sellos', navName: 'sello', parentName: 'sello' })
            .when("/camiones", { templateUrl: "camion/camion.ctrl.html", controller: "camionCtrl", parentTitle: 'Clientes',  title: 'Camiones', navName: 'camion', parentName: 'camion' })
            .when("/cisternas", { templateUrl: "cisterna/cisterna.ctrl.html", controller: "cisternaCtrl", parentTitle: 'Clientes',  title: 'Cisternas', navName: 'cisterna', parentName: 'cisterna' })
            .when("/choferes", { templateUrl: "chofer/chofer.ctrl.html", controller: "choferCtrl", parentTitle: 'Clientes', title: 'Choferes', navName: 'chofer', parentName: 'chofer' })
            //.when("/ordenes", { templateUrl: "orden/orden.ctrl.html", controller: "ordenCtrl", parentTitle: 'Clientes',  title: 'Ordenes', navName: 'orden', parentName: 'orden' })
            //.when("/orden/:id/:planificacionId?", { templateUrl: "orden/orden.edit.ctrl.html", controller: "ordenEditCtrl", parentTitle: 'Clientes', title: 'Creacion/Edicion Orden', navName: 'editorden', parentName: 'configuracion' })
            .when("/precargas", { templateUrl: "precarga/precarga.ctrl.html", controller: "precargaCtrl", parentTitle: 'Operaciones', title: 'Precargas', navName: 'precarga', parentName: 'operaciones' })
            .when("/precarga/:id/:inspeccionId?", { templateUrl: "precarga/precarga.edit.ctrl.html", controller: "precargaEditCtrl", parentTitle: 'Operaciones', title: 'Creacion/Edicion precarga', navName: 'editprecarga', parentName: 'operaciones' })
            .when("/inspecciones", { templateUrl: "inspeccion/inspeccion.ctrl.html", controller: "inspeccionCtrl", parentTitle: 'Operaciones',  title: 'Inspecciones', navName: 'inspeccion', parentName: 'inspeccion' })
            .when("/inspeccion/:id/:precargaId?", { templateUrl: "inspeccion/inspeccion.edit.ctrl.html", controller: "inspeccionEditCtrl", parentTitle: 'Operaciones',  title: 'Creacion/Edicion Inspeccion', navName: 'editinspeccion', parentName: 'configuracion' })
            .when("/planificaciones", { templateUrl: "planificacion/planificacion.ctrl.html", controller: "planificacionCtrl", parentTitle: 'Clientes',  title: 'Planificaciones', navName: 'planificacion', parentName: 'planificacion' })
            .when("/planificacion/:id", { templateUrl: "planificacion/planificacion.edit.ctrl.html", controller: "planificacionEditCtrl", parentTitle: 'Clientes', title: 'Creacion/Edicion Planificacion', navName: 'editplanificacion', parentName: 'configuracion' })
            .when("/usuarios", { templateUrl: "usuario/usuario.ctrl.html", controller: "usuarioCtrl", parentTitle: 'Configuracion',  title: 'Usuarios', navName: 'usuario', parentName: 'configuracion' })
            .when("/usuario/:id", { templateUrl: "usuario/usuario.edit.ctrl.html", controller: "usuarioEditCtrl", parentTitle: 'Configuracion', title: 'Creacion/Edicion Usuario', navName: 'editusuario', parentName: 'configuracion' })
            .when("/actividades", { templateUrl: "reportes/actividades.ctrl.html", controller: "actividadesCtrl", parentTitle: 'Reportes', title: 'Reporte actvidades', navName: 'actvidades', parentName: 'reportes' })
            .when("/verificaciones", { templateUrl: "verificacion/verificacion.ctrl.html", controller: "verificacionCtrl", parentTitle: 'Verficiacion', title: 'Listado Verificacion', navName: 'verificaciones', parentName: 'Operaciones' })
            .when("/verificacion/:id", { templateUrl: "verificacion/verificacion.edit.ctrl.html", controller: "verificacionEditCtrl", parentTitle: 'Verificacion', title: 'Creacion/Edicion Verificacion', navName: 'verificacionedit', parentName: 'reportes' })
            .when("/reporteAct", { templateUrl: "reportes/reporteAct.ctrl.html", controller: "reporteActCtrl", parentTitle: 'Reportes', title: 'Reporte Actividades', navName: 'reporteAct', parentName: 'clientes' })
            .when("/reporteGeneral", { templateUrl: "reportes/reporteGeneral.ctrl.html", controller: "reporteGeneralCtrl", parentTitle: 'Reportes', title: 'Reporte General', navName: 'reporteGeneral', parentName: 'reportes' })
            //.when("/permisos", { templateUrl: "rol/rol.ctrl.html", controller: "rolCtrl", title: 'Roles/Permisos', navName: 'permiso', parentName: 'configuracion' })
            .when("/401", { templateUrl: "error/noaccess.html", controller: "NoAccessCtrl", title: '401 - Access Denied' })
            // 404
            .when("/404", { templateUrl: "error/notfound.html", controller: "NotFoundCtrl", title: '404 - Not Found' })
            // otherwise
            .otherwise({ redirectTo: "404" });           


    }]); 
angular
    .module('app')
    .controller('camionCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.camions = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/camion', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.camions = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deletecamion = function(camionId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este camion!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/camion/' + camionId, $scope.camion, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "camion Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "camion no eliminada", "error");
                    }
                });
        }

        $scope.editcamion = function (camionId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "camion/camion.edit.ctrl.html",
                controller: "camionEditCtrl",
                resolve: {
                    camionId: function () {
                        return camionId;
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

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('cisternaCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.cisternas = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/cisterna', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.cisternas = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deletecisterna = function(cisternaId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este cisterna!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/cisterna/' + cisternaId, $scope.cisterna, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "cisterna Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "cisterna no eliminada", "error");
                    }
                });
        }

        $scope.editcisterna = function (cisternaId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "cisterna/cisterna.edit.ctrl.html",
                controller: "cisternaEditCtrl",
                resolve: {
                    cisternaId: function () {
                        return cisternaId;
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

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('cisternaEditCtrl', ['$scope', '$rootScope', '$http', 'cisternaId', function ($scope, $rootScope, $http, cisternaId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = { cisterna: { compartimentos: 0 }, detalle: [] }
        $scope.medidas = ["1/8", "2/8", "3/8", "4/8", "5/8", "6/8", "7/8", "1/16", "3/16", "5/16", "7/16", "9/16", "10/16", "11/16", "9/16", "13/16"]
        $scope.$watch('model.cisterna.compartimentos', function (n, o) { $scope.changeCompartimentos(n, o) }, true);

        $scope.cisternaId = cisternaId
        $scope.compartimentosArray = [{key:1, value:'Uno'},{key:2, value:'Dos'},{key:3, value:'Tres'},{key:4, value:'Cuatro'},{key:5, value:'Cinco'},{key:6, value:'Seis'},]
        $scope.getCisterna = function () {
            if ($scope.cisternaId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/cisterna/' + $scope.cisternaId,
                        { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        result.data.detalle = result.data.detalle.sort(function (a, b) { return a.compartimento - b.compartimento; })
                            $scope.model = result.data;
                        },
                        function error(err) {
                            console.log(err);
                        });
            } 
        }
        $scope.titleMsg = $scope.cisternaId > 0 ? "Editando cisterna" : "Agregando cisterna";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.changeCompartimentos = function (n, o) {
            //console.log($scope.cisternaForm)
            if (!$scope.cisternaForm.$dirty)
                return;
            $scope.model.detalle = []
            if (n != o) {
                var deta = []
                for (var i = 1; i <= n; i++) {
                    var cap = 3000;
                   var e = { compartimento: i, capacidad: cap, hasChapa: true, hasBoca: true, hasDescarga: true }
                   deta.push(e);
                }
                $scope.model.detalle =  deta.sort(function (a, b) { return a.compartimento - b.compartimento; })
           }
        }

        $scope.saveData = function (valid) {
            if (!valid) {
                return;
            }

            if ($scope.cisternaId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/cisterna/' + $scope.cisternaId, $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('cisterna', 'cisterna guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/cisterna/', $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('cisternas', 'cisterna guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }

        $scope.getCisterna();
    }]);
angular
    .module('app')
    .controller('choferCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.chofers = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/chofer', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.chofers = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deletechofer = function(choferId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este chofer!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/chofer/' + choferId, $scope.chofer, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "chofer Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "chofer no eliminada", "error");
                    }
                });
        }

        $scope.editchofer = function (choferId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "chofer/chofer.edit.ctrl.html",
                controller: "choferEditCtrl",
                resolve: {
                    choferId: function () {
                        return choferId;
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

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('clienteCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.clientes = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Cliente', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.clientes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteCliente = function(clienteId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este cliente!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/Cliente/' + clienteId, $scope.cliente, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Cliente Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "Cliente no eliminada", "error");
                    }
                });
        }

        $scope.editCliente = function (clienteId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "cliente/cliente.edit.ctrl.html",
                controller: "clienteEditCtrl",
                resolve: {
                    clienteId: function () {
                        return clienteId;
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

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('clienteEditCtrl', ['$scope', '$rootScope', '$http', '$routeParams', 'Upload', function ($scope, $rootScope, $http, $routeParams, Upload) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.cliente = { id: 0, nombre: '', email: '', contacto: '', telefono: '', fax: '', logo: '' }
        $scope.clienteId = $routeParams.id
        $scope.empresas = []
        $scope.terminales = []
        $scope.correos = []
        $scope.productos = []
        $scope.curCorreo = null;
        $scope.curTerminal = null;
        $scope.productoId = null;
        $scope.codigoProducto = "";
        $scope.productosCliente = [];
        $scope.terminalPromise = false;
        $scope.yesno = [{ key: true, value: 'Yes' }, { eky: false, value: 'No' }]
        $scope.conducesTpl = [];
        $scope.getCliente = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Cliente/' + $scope.clienteId, { 'Content-Type': 'application/json' })
                .then(function success(result) {

                    if (result.data.cliente.inicioLabor)
                        result.data.cliente.inicioLabor = moment.utc(result.data.cliente.inicioLabor).toDate();

                    if (result.data.cliente.finLabor)
                        result.data.cliente.finLabor = moment.utc(result.data.cliente.finLabor).toDate();

                    $scope.cliente = result.data.cliente;
                    $scope.empresas = result.data.empresas;
                    $scope.productosCliente = result.data.productosCliente;
                    $scope.productos = result.data.productos;
                    $scope.correos = result.data.correos;
                    $scope.cliente.empresaId = $scope.cliente.empresaId <= 0 ? "" : $scope.cliente.empresaId;
                    $scope.conducesTpl = result.data.conducesTpl;
                    $scope.getTerminales();
                },
                function error(err) {
                    console.log(err);
                });
        }

        $scope.productoChanged = function (arg) {
            $scope.productoId = arg;
        }
        $scope.changeCodigo = function (arg) {
            $scope.codigoProducto = arg;
        }

        $scope.addProducto = function () {
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Cliente/AddToClient', { productoId: $scope.productoId, clienteId: $scope.cliente.id, codigoProducto: $scope.codigoProducto }, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $rootScope.success('Clientes', 'Registro guardado')
                    $scope.productosCliente = result.data;
                    $scope.productoId = null;
                },
                    function error(err) { });
        }

        $scope.removeProducto = function (id) {
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Cliente/removeFromClient', { productoId: id, clienteId: $scope.cliente.id }, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $rootScope.success('Clientes', 'Registro removido')
                    $scope.productosCliente = result.data;
                },
                    function error(err) { });
        }


        $scope.getTerminales = function () {
            $scope.terminalPromise = $http.get($rootScope.config.apiBase + '/Terminal/GetByCliente/' + $scope.clienteId, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.terminales = result.data;
                },
                function error(err) {
                    console.log(err);
                });
        }


        $scope.curfile = false;

        $scope.titleMsg = $scope.clienteId > 0 ? "Editando Cliente" : "Agregando Cliente";


        $scope.editMail = function (mail) {

            if (!mail)
                mail = { clienteId: $scope.cliente.id, email: '', nombre: '', notificaPlanificacion: false, notifcaOrden: false, notificaInspeccion: false }

            $scope.curCorreo = mail;
        }


        $scope.editTerminal = function (terminal) {

            if (!terminal)
                terminal = { clienteId: $scope.cliente.id, esDestino: true, destinoOrd: 0, conduceCaption: "" }

            $scope.curTerminal = terminal;
        }

        $scope.saveTerminal = function () {
            if ($scope.curTerminal.id > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Terminal/' + $scope.curTerminal.id, $scope.curTerminal, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Clientes', 'Registro guardado')
                        $scope.curTerminal = null;
                    },
                    function error(err) { });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Terminal',
                    $scope.curTerminal,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Clientes', 'Registro guardado')
                        $scope.getTerminales();
                        $scope.curTerminal = null;
                    },
                    function error(err) { });
            }
        }

        $scope.cancelMail = function () {
            $scope.curCorreo = null;
        }

        $scope.cancelTerminal = function () {
            $scope.curTerminal = null;
        }


        $scope.removeTerminal = function (terminal) {
            $scope.myPromise = $http.delete($rootScope.config.apiBase + '/Terminal/' + terminal.id, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $rootScope.success('Clientes', 'Registro eliminado')
                    $scope.getTerminales();
                },
                function error(err) { });
        }

        $scope.saveMail = function () {
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Cliente/UpdateMail/', $scope.curCorreo, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $rootScope.success('Clientes', 'Correo guardado')
                    $scope.correos = result.data;
                    $scope.curCorreo = null;
                },
                function error(err) { });
        }

        $scope.deleteMail = function (c) {
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Cliente/DeleteMail/', c, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $rootScope.success('Clientes', 'Correo eliminado')
                    $scope.correos = result.data;
                },
                function error(err) { });
        }

        $scope.changeLogo = function (file, errFiles) {
            if (file) {
                $scope.f = file;
                $scope.errFile = errFiles && errFiles[0];
                var url = "api/Cliente/Upload/?clienteId=" + $scope.cliente.id;
                $rootScope.uploadFile(file,
                    url,
                    function(data) {
                        $scope.cliente.logo = data;
                    });
            }
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.clienteId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Cliente/' + $scope.clienteId, $scope.cliente, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Cliente', 'Cliente guardado')
                        $scope.$close()
                    },
                    function error(err) { });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Cliente/', $scope.cliente, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Clientes', 'Cliente guardado')
                        $scope.cliente = result.data;
                    },
                    function error(err) { });
            }
        }


        $scope.getCliente();
    }]);
angular
    .module('app').factory('responseInterceptor', ['$q', '$location', 'toasty', '$rootScope', function ($q, $location, toasty, $rootScope) {
    return {
        response: function (response) {
            if (response.status === 401) {
                console.log("Response 401");
            }
            return response || $q.when(response);
        },

        responseError: function (rejection) {

            if (rejection.status === 401) {
                console.log("Response Error 401", rejection);
               $location.path('/');
            }

            var result = rejection.statusText;
            if (rejection.data)
                result = rejection.data;
            $rootScope.error('Error ' + rejection.status + '!', result);

         
            return $q.reject(rejection);
        }
    }
}])
angular
    .module('app')
    .filter('utcToLocal', Filter);

function Filter($filter) {
    return function (utcDateString, format) {
        // return if input date is null or undefined
        if (!utcDateString) {
            return;
        }

        // append 'Z' to the date string to indicate UTC time if the timezone isn't already specified
        if (utcDateString.indexOf('Z') === -1 && utcDateString.indexOf('+') === -1) {
            utcDateString += 'Z';
        }

        // convert and format date using the built in angularjs date filter
        return $filter('date')(utcDateString, format);
    };
}

angular
  .module('storageService', [])
  .factory('StorageSvc', ['$window', function ($window) {
  	return {
  		setLocal: function (key, value) {
  			try {
  				if ($window.Storage) {
  					$window.localStorage.setItem(key, $window.JSON.stringify(value));
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getLocal: function (key) {
  			try {
  				if ($window.Storage) {
  					return $window.JSON.parse($window.localStorage.getItem(key));
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getLocalAll: function () {
  			try {
  				if ($window.Storage) {
  					var _json = '{"Data": [';
  					for (var i = 0; i < $window.localStorage.length; i++) {
  						_json += $window.localStorage.getItem($window.localStorage.key(i));
  						_json += ',';
  					}

  					if($window.localStorage.length > 0){ _json = _json.substring(0, _json.length - 1); }
  	
  					_json += ']}'

  					return $window.JSON.parse(_json);
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		removeLocal: function (key) {
  			try {
  				if ($window.Storage) {
  					$window.localStorage.removeItem(key);
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		clearLocal: function()
  		{
  			try {
  				if ($window.Storage) {
  					$window.localStorage.clear();
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		setSession: function (key, value) {
  			try {
  				if ($window.Storage) {
  					$window.sessionStorage.setItem(key, $window.JSON.stringify(value));
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getSession: function (key) {
  			try {
  				if ($window.Storage) {
  					return $window.JSON.parse($window.sessionStorage.getItem(key));
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		getSessionAll: function () {
  			try {
  				if ($window.Storage) {
  					var _json = '{"Data": [';
  					for (var i = 0; i < sessionStorage.length; i++) {
  						_json += $window.JSON.parse($window.sessionStorage.getItem(i));
  						_json += ',';
  					}

  					if (localStorage.length > 0) { _json.slice(0, -1); }

  					_json += ']}'

  					return _json;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		removeSession: function (key) {
  			try {
  				if ($window.Storage) {
  					$window.sessionStorage.removeItem(key);
  					return true;
  				} else {
  					return false;
  				}
  			} catch (error) {
  				console.error(error, error.message);
  			}
  		},
  		clearSession: function () {
			try{
  				if ($window.Storage) {
  					$window.sessionStorage.clear();
  				} else {
  					return false;
  				}
			} catch (error) {
		  		console.error(error, error.message);
			}
  		}
  	}
  }]);
//by Cacodaimon
//https://gist.github.com/Cacodaimon/7309268
angular.module('app')
    .filter('sumByKey', function () {
        return function (data, key) {
            if (typeof (data) === 'undefined' || typeof (key) === 'undefined') {
                return 0;
            }

            var sum = 0;
            for (var i = data.length - 1; i >= 0; i--) {
                sum += parseInt(data[i][key]);
            }

            return sum;
        };
    });

angular
    .module('app')
    .controller('DashboardCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'calendarConfig', '$timeout', 'calendarTitle', '$location', function DashboardCtrl($scope, $rootScope, $http, Popeye, calendarConfig, $timeout, calendarTitle, $location) {
        $scope.loadingMsg = "cargando...";
        $scope.clientes = [];
        $scope.events = [];
        $scope.ordenes = [];
        $scope.terminales = [];
        $scope.productos = [];
        $scope.usuarios = [];
        $scope.camiones = [];
        $scope.choferes = [];
        $scope.cisternas = [];
        $scope.filterSello = {};
        $scope.sellos = [];
        //promises
        $scope.clientesPromise = false;
        $scope.eventsPromise = false;
        $scope.ordenesPromise = false;
        $scope.terminalesPromise = false;
        $scope.productosPromise = false;
        $scope.usuariosPromise = false;
        $scope.camionesPromise = false;
        $scope.cisternasPromise = false;
        $scope.choferesPromise = false;
        $scope.sellosPromise = false;

        $scope.calendarView = 'month';
        $scope.viewDate = moment().startOf('month').toDate();
        $scope.excludedDays = [0, 6];
        $scope.filter = { clinicId: null }
        $scope.msg = "";
        $scope.getTitle = function () {

            var d = moment($scope.viewDate);

            var result = "";
            if ($scope.calendarView == 'day')
                result = d.format('MMMM Do YYYY');
            if ($scope.calendarView == 'month')
                result = d.format('MMMM YYYY');
            if ($scope.calendarView == 'year')
                result = d.format('YYYY');
            if ($scope.calendarView == 'week')
                result = d.format('[Week #]W [of] YYYY')

            return result;
        }

        $scope.eventClicked = function (event) {
            $rootScope.goToRoot('/App/' + event.empresaSlug + '/' + event.clienteSlug + '/#!/inspeccion/' + event.id);
        };

        $scope.clearFilter = function () {
            $scope.filter.clinicId = null;
            $scope.updateMsg();
            $scope.loadEvents();
        };



        $scope.getClientes = function () {
            $scope.clientesPromise = $http.get($rootScope.config.apiBase + '/DashBoard/GetClientes', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.clientes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.getEvents = function () {
            $scope.eventsPromise = $http.get($rootScope.config.apiBase + '/DashBoard/GetPenddingOrders', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.events = [];
                    if (result.data) {
                        for (var i = 0; i < result.data.length; i++) {
                            var e = result.data[i];
                            $scope.events.push({
                                id: e.ordenId,
                                empresaNombre: e.empresaNombre,
                                empresaSlug: e.empresaSlug,
                                clienteNombre: e.clienteNombre,
                                clienteSlug: e.clienteSlug,
                                choferNombre: e.choferNombre,
                                fichaCamion: e.fichaCamion,
                                fichaCisterna: e.fichaCisterna,
                                terminal: e.terminal,
                                title: e.clienteNombre,
                                color: calendarConfig.colorTypes.info,
                                startsAt: moment.utc(e.fecha).toDate()
                            })
                        }
                    }


                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.getOrdenes = function () {
            $scope.ordenesPromise = $http.get($rootScope.config.apiBase + '/DashBoard/GetOrdenes', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.ordenes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }


        $scope.curId = 0;
        $scope.print = function (id) {
            $scope.curId = id;
            console.log(id);
            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Inspeccion/' + $scope.curId;
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



        if ($rootScope.hasClaim('sadmin', 'supervisor', 'inspector')) {
            $scope.getClientes();
            //$scope.getUsuarios();
            //$scope.getTerminales();
            //$scope.getProductos();

            //$scope.getEvents();
            $scope.getOrdenes();

        } else if($rootScope.info.clienteId > 0) {

           // $scope.getEvents();
            $scope.getOrdenes();
        }
        
        $scope.editinspeccion = function (inspeccionId) {
            $location.path('/inspeccion/0/' + inspeccionId);
        };



    }]);
var dashboardService = angular.module('dashboardService', ['ngResource']);

// create the factory
dashboardService.factory('DashSvc', ['$resource','$rootScope', function ($resource, $rootScope) {
    return $resource(':url/:action', { action: "@action" }, {
      
    });
}]);
angular
    .module('app')
    .controller('empresaCtrl', ['$scope', '$rootScope', '$http', '$location', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, $location, Popeye, SweetAlert) {
        $scope.empresas = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Empresa', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.empresas = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteEmpresa = function (empresaId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta empresa!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/Empresa/' + empresaId, $scope.empresa, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Empresa Eliminada.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "Empresa no eliminada", "error");
                    }
                });
        }

        $scope.editEmpresa = function (empresaId) {
            $location.path('empresa/' + empresaId);
        };

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('empresaEditCtrl', ['$scope', '$rootScope', '$http', '$location', '$routeParams', function ($scope, $rootScope, $http, $location, $routeParams) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando..."
        $scope.empresa = { id: 0 }
        $scope.empresaId = $routeParams.id
        $scope.settings = {}
        $scope.getEmpresa = function () {
            if ($scope.empresaId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/Empresa/' + $scope.empresaId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.empresa = result.data.empresa;
                        if (result.data.settings)
                            $scope.settings = result.data.settings;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }

        $scope.titleMsg = $scope.empresaId > 0 ? "Editando Empresa" : "Agregando Empresa";


        $scope.curfile = false;

        $scope.changeLogo = function (file, errFiles) {
            if (file) {
                $scope.f = file;
                $scope.errFile = errFiles && errFiles[0];
                var url = "api/Empresa/Upload/?empresaId=" + $scope.empresa.id;
                $rootScope.uploadFile(file,
                    url,
                    function(data) {
                        $scope.empresa.logo = data;
                    });
            }
        }

        $scope.saveSettings = function () {
            $scope.settings.empresaId = $scope.empresa.id;
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Empresa/SaveSettings/', $scope.settings, { headers: { 'Content-Type': 'application/json' } })
                .then(function success(result) {
                    $rootScope.success('Empresas', 'Registro guardado')
                    $scope.settings = result.data;
                },
                    function error(err) { });
        }


        $scope.sendTest = function () {
            $scope.settings.empresaId = $scope.empresa.id;
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Empresa/SendTest/' + $scope.empresa.id, null, { headers: { 'Content-Type': 'application/json' } })
                .then(function success(result) {
                    $rootScope.success('Empresas', 'Correo de prueba enviado')

                },
                    function error(err) { });
        }


        $scope.cancel = function () {
            $scope.$close()
        }


        $scope.saveData = function (valid) {

            if (!valid) {

                return;

            }

            if ($scope.empresaId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Empresa/' + $scope.empresaId, $scope.empresa, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $rootScope.success('Empresas', 'Registro guardado');
                        $scope.empresa = result.data;

                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Empresa/', $scope.empresa, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $rootScope.success('Empresas', 'Registro guardado')
                        $scope.empresa = result.data;
                    },
                    function error(err) {
                    });
            }
        }

        $scope.getEmpresa();
    }]);
(function () {
    'use strict';

    angular
        .module('app')
        .filter('utcToLocal', Filter);

    function Filter($filter) {
        return function (utcDateString, format) {
            // return if input date is null or undefined
            if (!utcDateString) {
                return;
            }

            // append 'Z' to the date string to indicate UTC time if the timezone isn't already specified
            if (utcDateString.indexOf('Z') === -1 && utcDateString.indexOf('+') === -1) {
                utcDateString += 'Z';
            }

            // convert and format date using the built in angularjs date filter
            return $filter('date')(utcDateString, format);
        };
    }
})();
angular
	.module('app')
	.controller('NoAccessCtrl', ['$scope', function NoAccessCtrl($scope) {
	}]);
angular
	.module('app')
	.controller('NotFoundCtrl', ['$scope', function NotFoundCtrl($scope) {
	}]);
angular
    .module('app')
    .controller('inspeccionCtrl', ['$scope', '$rootScope', '$location', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope,  $location, $http, Popeye, SweetAlert) {
        $scope.inspecciones = [];
        $scope.ordenes = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.togleDetail = function(o) {
            o.showDetail = !o.showDetail
        }

        $scope.refreshData = function () {

            $scope.myPromise2 = $http.get($rootScope.config.apiBase + '/inspeccion', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.inspecciones = result.data;
                }, function error(err) {
                    console.log(err);
                });

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/inspeccion/ordenes', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.ordenes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteinspeccion = function (inspeccionId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este inspeccion!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/inspeccion/' + inspeccionId, $scope.inspeccion, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "inspeccion Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "inspeccion no eliminada", "error");
                    }
                });
        }

        $scope.print = function (id) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Inspeccion/' + id;
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



        $scope.editinspeccion = function (inspeccionId) {
            $location.path('/inspeccion/0/' + inspeccionId);
        };

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('inspeccionPrintCtrl', ['$scope', '$rootScope', '$http', '$sce', 'url', function ($scope, $rootScope, $http, $sce, url) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Loading...";
      
        $scope.url = $sce.trustAsResourceUrl(url);
      
    }]);
angular
    .module('app')
    .controller('ordenCtrl', ['$scope', '$rootScope', '$location', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope,  $location, $http, Popeye, SweetAlert) {
        $scope.ordenes = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/orden', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.ordenes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteorden = function (ordenId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este orden!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/orden/' + ordenId, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "orden Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "orden no eliminada", "error");
                    }
                });
        }

        $scope.editorden = function (ordenId) {

            $location.path('/orden/' + ordenId);
        };

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('precargaCtrl', ['$scope', '$rootScope', '$location', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $location, $http, Popeye, SweetAlert) {
        $scope.precargas = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/precarga', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.precargas = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

 

        $scope.deleteprecarga = function (precargaId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este precarga!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/precarga/' + precargaId, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "precarga Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "precarga no eliminada", "error");
                    }
                });
        }

        $scope.editprecarga = function (precargaId) {
            $location.path('/precarga/' + precargaId);
        };


        $scope.print = function (id) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Precarga/' + id;
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

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('planificacionCtrl', ['$scope', '$rootScope', '$http', '$location', 'Popeye', 'SweetAlert', function($scope, $rootScope, $http, $location, Popeye, SweetAlert) {
        $scope.planificacions = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";


        $scope.refreshData = function() {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/planificacion', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.planificacions = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteplanificacion = function(planificacionId) {
            SweetAlert.swal({
                    title: "Esta seguro?",
                    text: "Esta seguro de quere elimnar este planificacion!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Eliminar!",
                    cancelButtonText: "No, cancelar!",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                function(isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/planificacion/' + planificacionId, $scope.planificacion, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "planificacion Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "planificacion no eliminada", "error");
                    }
                });
        }

        $scope.editplanificacion = function(planificacionId) {
            if (!planificacionId)
                planificacionId = 0;

            $location.path('planificacion/' + planificacionId);
        };

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('planificacionEditCtrl', ['$scope', '$rootScope', '$http', '$location', '$routeParams', '$sce', '$interpolate', 'Popeye', function ($scope, $rootScope, $http, $location, $routeParams, $sce, $interpolate, Popeye) {
        $scope.myPromise = false;
        var urlParams = $location.search();
        $scope.loadingMsg = "Cargando...";
        $scope.model = { planificacion: { id: 0 }, planificacionDespacho: [] }
        $scope.terminales = []
        $scope.destinos = []
        $scope.terminalId = 0;
        $scope.planificacionId = $routeParams.id;

        //$scope.$watch(function() { return $scope.planificacion.fechaInicio }, $scope.timeWatcher, true)
        //$scope.$watch("planificacion.fechaFin", $scope.timeWatcher, true);



        $scope.removeTerminal = function (index) {
            $scope.model.planificacionDespacho.splice(index, 1);
        }


        $scope.sendMail = function () {
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Planificacion/SendMail/' + $scope.model.planificacion.id, null, { headers: { 'Content-Type': 'application/json' } })
                .then(function success(result) {
                    $rootScope.success('Planificacion', 'Correo enviado')
                    $scope.settings = result.data;
                },
                    function error(err) { });
        }

        $scope.addTerminal = function () {
            var despacho = {
                despacho: {
                    terminalId: 0,
                    clienteId: $scope.model.planificacion.clienteId,
                    planificacionId: $scope.model.planificacion.id
                }, planificacionDetalle: []
            }
            $scope.model.planificacionDespacho.push(despacho)
            $scope.timeWatcher()
        }

        $scope.timeWatcher = function (n, o) {

            var r = $scope.daysBetweenDates($scope.model.planificacion.fechaInicio, $scope.model.planificacion.fechaFin)
            for (var i = 0; i < $scope.model.planificacionDespacho.length; i++) {
                for (var j = 0; j < r.length; j++) {
                    $scope.model.planificacionDespacho[i].planificacionDetalle[j] = { detalle: { fecha: r[j].fecha }, destinos: [] }
                    for (var k = 0; k < $scope.destinos.length; k++) {
                        $scope.model.planificacionDespacho[i].planificacionDetalle[j].destinos.push({ terminalId: $scope.destinos[k].key });
                    }
                }
            }

        }

        $scope.getTable = function (d) {
            var table = '';
            for (var i = 0; i < $scope.model.planificacionDespacho.length; i++) {
                table += '<table class="table1" style=""><thead><tr><th style="width:90px"></th><th class="thead" colspan="3">Despacho&nbsp;<a href="javascript:;" ng-click="removeTerminal(' + i + ')"  class="text-danger"><li class="fa fa-remove"></li></a></th> <th class="thead" colspan="{{destinos.length * 3 }}">Destino</th></tr>';

                table += '<tr><th></th><th colspan="3"><select style="width:100%" ng-change="" ng-options="item.key as item.value for item in terminales " id="terminal" name="terminal" ng-model="model.planificacionDespacho[' + i + '].despacho.terminalId"><option value="">--select terminal--</option></select></th>'
                table += '<th colspan="3" style="text-align: center;border:1px solid #AAAAAA"  ng-repeat="dest in destinos track by $index">{{dest.value}}</th></tr>'
                table += '<tr><th></th><th>C. Qty</th><th>H. Inicio</th><th>H. Fin</th>';

                for (var k = 0; k < $scope.destinos.length; k++) {
                    table += '<th>C. Qty</th><th>H. Inicio</th><th>H. Fin</th>';
                }
                table += '</tr></thead><tbody>'

                for (var j = 0; j < $scope.model.planificacionDespacho[i].planificacionDetalle.length; j++) {
                    //var fecha = $scope.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fecha.format("dddd") + " " + $scope.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fecha.format("DD") + " de " + $scope.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fecha.format("MMMM")
                    var fecha = $scope.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fecha.format("DD/MM/YYYY");
                    table += "<tr>"
                    table += '<td>' + fecha + '</td>'
                    table += '<td><input type="number" ng-model="model.planificacionDespacho[' + i + '].planificacionDetalle[' + j + '].detalle.camionesQty"/></td>'
                    table += ' <td><input ui-timepicker="$root.timePickerOptions" ng-model="model.planificacionDespacho[' + i + '].planificacionDetalle[' + j + '].detalle.fechaInicio"/></td>'
                    table += '<td><input ui-timepicker="$root.timePickerOptions"  ng-model="model.planificacionDespacho[' + i + '].planificacionDetalle[' + j + '].detalle.fechaFin"/></td>'


                    for (var l = 0; l < $scope.destinos.length; l++) {
                        var d = $scope.getCurTerminal($scope.destinos[l].key, $scope.model.planificacionDespacho[i].planificacionDetalle[j].destinos);
                        table += '<td><input type="number" ng-model="model.planificacionDespacho[' + i + '].planificacionDetalle[' + j + '].destinos[' + d + '].camionesQty"/></td>';
                        table += '<td><input  ui-timepicker="$root.timePickerOptions" ng-model="model.planificacionDespacho[' + i + '].planificacionDetalle[' + j + '].destinos[' + d + '].fechaInicio"/></td>';
                        table += '<td><input  ui-timepicker="$root.timePickerOptions"  ng-model="model.planificacionDespacho[' + i + '].planificacionDetalle[' + j + '].destinos[' + d + '].fechaFin"/></td>';

                    }
                    table += "</tr>"
                }
                table += '</tbody></table>'
            }

            return table;
        }

        $scope.getCurTerminal = function (terminalId, de) {
            for (var i = 0; i < de.length; i++) {
                if (de[i].terminalId == terminalId) {
                    return i;
                }
            }
        }

        $scope.daysBetweenDates = function daysBetweenDates(startDate, endDate) {
            startDate = moment(startDate);
            endDate = moment(endDate);

            var now = startDate,
                dates = [];

            while (now.isBefore(endDate) || now.isSame(endDate)) {
                var d = now.format('dddd') + ' ' + now.format('DD') + ' de ' + now.format('MMMM')
                dates.push({ fecha: now.clone(), despachoDetalle: [] });
                now.add(1, 'days');
            }

            return dates;
        };

        $scope.getPlanificacion = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Planificacion/' + $scope.planificacionId, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    result.data.model.planificacion.fechaInicio = moment.utc(result.data.model.planificacion.fechaInicio).toDate()
                    result.data.model.planificacion.fechaFin = moment.utc(result.data.model.planificacion.fechaFin).toDate()
                    for (var i = 0; i < result.data.model.planificacionDespacho.length; i++) {
                        for (var j = 0; j < result.data.model.planificacionDespacho[i].planificacionDetalle.length; j++) {

                            result.data.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fecha =
                                moment.utc(result.data.model.planificacionDespacho[i].planificacionDetalle[j].detalle
                                    .fecha);

                            result.data.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fechaInicio =
                                moment.utc(result.data.model.planificacionDespacho[i].planificacionDetalle[j]
                                    .detalle.fechaInicio);

                            result.data.model.planificacionDespacho[i].planificacionDetalle[j].detalle.fechaFin =
                                moment.utc(result.data.model.planificacionDespacho[i].planificacionDetalle[j]
                                    .detalle.fechaFin);

                            for (var k = 0; k < result.data.model.planificacionDespacho[i].planificacionDetalle[j].destinos.length; k++) {

                                result.data.model.planificacionDespacho[i].planificacionDetalle[j].destinos[k]
                                    .fechaInicio = moment.utc(result.data.model.planificacionDespacho[i].planificacionDetalle[j].destinos[k]
                                        .fechaInicio);

                                result.data.model.planificacionDespacho[i].planificacionDetalle[j].destinos[k]
                                    .fechaFin = moment.utc(result.data.model.planificacionDespacho[i].planificacionDetalle[j].destinos[k]
                                        .fechaFin);
                            }
                        }
                    }

                    $scope.model = result.data.model;
                    $scope.terminales = result.data.terminales;
                    $scope.destinos = result.data.destinos;
                    $scope.getprecargas();
                },
                function error(err) {
                    console.log(err)
                });
        }

        $scope.titleMsg = $scope.planificacionId > 0 ? "Editando planificacion" : "Agregando planificacion";

   
        $scope.saveData = function (valid) {


            //if (!valid) {
            //    $rootScope.error('Datos invalidos')
            //    return;
            //}

            if ($scope.planificacionId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/planificacion/' + $scope.planificacionId, JSON.stringify($scope.model), { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('planificacion', 'planificacion guardado')

                    },
                    function error(err) { });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/planificacion/', $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('planificacions', 'planificacion guardado')

                    },
                    function error(err) { });
            }
        }


        $scope.getprecargas = function () {

            $scope.prePromise = $http.get($rootScope.config.apiBase + '/planificacion/GetPrecargas/' + $scope.planificacionId, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.precargas = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.editPrecarga = function (id) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "precarga/precarga.edit.ctrl.html",
                'modalClass': 'modal-precarga',
                controller: "precargaEditCtrl",
                resolve: {
                    $routeParams: function () {
                        return { planificacionId: $scope.planificacionId, id: id }
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
                $scope.getprecargas();
            });
        };

        $scope.getPlanificacion();
    }]);
angular
    .module('app')
    .controller('productoCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.productoes = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/producto', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.productoes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteproducto = function(productoId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta producto!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/producto/' + productoId, $scope.producto, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "producto Eliminada.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "producto no eliminada", "error");
                    }
                });
        }

        $scope.editproducto = function (productoId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "producto/producto.edit.ctrl.html",
                controller: "productoEditCtrl",
                resolve: {
                    productoId: function () {
                        return productoId;
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

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('actividadEditCtrl', ['$scope', '$rootScope', '$http', 'actividadId', function ($scope, $rootScope, $http, actividadId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.actividad = { nombre: '', telefono: '', direccion: '', multipleInstances: false}
        $scope.actividadId = actividadId
        $scope.getactividad = function () {
            if ($scope.actividadId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/actividad/' + $scope.actividadId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.actividad = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.actividadId > 0 ? "Editando actividad" : "Agregando actividad";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.actividadId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/actividad/' + $scope.actividadId, $scope.actividad, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('actividades', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/actividad/', $scope.actividad, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('actividades', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getactividad();
    }]);
angular
    .module('app')
    .controller('actividadesCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', '$uibModal', function ($scope, $rootScope, $http, Popeye, SweetAlert, $uibModal) {
        $scope.actividades = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = {};
        $scope.curIndex = null;
        $scope.isOpen = false;
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.getTitle = function () {
            if ($scope.model.dateFilter)
                return moment($scope.model.dateFilter).format("dddd D, MMMM YYYY");
        }

        $scope.monthTitle = function () {
            if ($scope.model.dateFilterMonth)
                return moment($scope.model.dateFilterMonth).format("MMMM YYYY");
        }

        $scope.yearTitle = function () {
            if ($scope.model.dateFilter)
                return moment($scope.model.dateFilter).format("YYYY");
        }

        $scope.dateChanged = function (val, dir) {
            $scope.model.dir = dir;
            $scope.refreshData();
        }

        $scope.monthChanged = function (val, dir) {
            $scope.model.dirMonth = dir;
            $scope.refreshMonth();
        }

        $scope.dateOptions = {
            maxDate: new Date()
        };

        $scope.monthOptions = {
            maxDate: new Date(),
            minMode: 'month'
        };

        $scope.yearOptions = {
            maxDate: new Date(),
            minMode: 'year'
        };

        $scope.addNote = function () {

            var modal = $uibModal.open({
                animation: true,
                templateUrl: 'observacion.html',
                controller: function ($scope) {
                    $scope.note = null;
                    $scope.cancel = function () {
                        $scope.$close(null)
                    }
                    $scope.ok = function () {
                        $scope.$close($scope.note)
                    }
                },
                size: 'md',
                resolve: {
                    result: function () {
                        return $scope.note;
                    }
                }
            });

            modal.result.then(function (note) {

                if (!note || note == "")
                    return;

                var observacion = { nota: note, fecha: $scope.model.dateFilter, actividadId: $scope.actividades.actividadId, type: $scope.actividades.type }
                $http.post($rootScope.config.apiBase + '/actividad/AddObservation', observacion, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $scope.refreshData()
                        SweetAlert.swal("Guardado!", "Observación/Comentario Guaradado", "success")
                    })
            }, function () {

            });
        }

        $scope.addNoteMonth = function () {

            var modal = $uibModal.open({
                animation: true,
                templateUrl: 'observacion.html',
                controller: function ($scope) {
                    $scope.note = null;
                    $scope.cancel = function () {
                        $scope.$close(null)
                    }
                    $scope.ok = function () {
                        $scope.$close($scope.note)
                    }
                },
                size: 'md',
                resolve: {
                    result: function () {
                        return $scope.note;
                    }
                }
            });

            modal.result.then(function (note) {

                if (!note || note == "")
                    return;

                var observacion = { nota: note, fecha: $scope.model.dateFilterMonth, actividadId: $scope.actividadesMonth.actividadId, type: $scope.actividadesMonth.type }
                $http.post($rootScope.config.apiBase + '/actividad/AddObservation', observacion, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $scope.refreshMonth()
                        SweetAlert.swal("Guardado!", "Observación/Comentario Guaradado", "success")
                    })
            }, function () {

            });
        }

        $scope.refreshData = function () {
            $scope.myPromise =
                $http.get($rootScope.config.apiBase + '/actividad/GetDay/', {
                    params: { dateInit: $scope.model.dateFilter, dir: $scope.model.dir }
                })
                .then(function success(result) {
                    $scope.model.dir = null;
                    $scope.actividades = result.data;
                    if ($scope.actividades.dateFilterInit)
                        $scope.model.dateFilter = moment.utc($scope.actividades.dateFilterInit).toDate();

                    if ($scope.actividades.message)
                        $rootScope.success("Info", $scope.actividades.message);

                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.refreshMonth = function () {
            $scope.myPromise =
                $http.get($rootScope.config.apiBase + '/actividad/GetMonth/', {
                    params: { dateInit: $scope.model.dateFilterMonth, dir: $scope.model.dirMonth }
                })
                .then(function success(result) {
                    $scope.model.dirMonth = null;
                    $scope.actividadesMonth = result.data;
                    if ($scope.actividadesMonth.dateFilterInit)
                        $scope.model.dateFilterMonth = moment.utc($scope.actividadesMonth.dateFilterInit).toDate();

                    if ($scope.actividadesMonth.message)
                        $rootScope.success("Info", $scope.actividades.message);

                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.delNote = function (observacion) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de querer eliminar esta observación/comentario!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.post($rootScope.config.apiBase + '/actividad/DeleteObservation', observacion, { headers: { 'Content-Type': 'application/json' } })
                            .then(function success(result) {
                                $scope.refreshMonth()
                                SweetAlert.swal("Eliminado!", "registro Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "actividad no eliminada", "error");
                    }
                });

        }

        $scope.delNoteMonth = function (observacion) {
            SweetAlert.swal({
                    title: "Esta seguro?",
                    text: "Esta seguro de querer eliminar esta observación/comentario!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                    cancelButtonText: "No, cancelar!",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.post($rootScope.config.apiBase + '/actividad/DeleteObservation', observacion, { headers: { 'Content-Type': 'application/json' } })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "registro Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "actividad no eliminada", "error");
                    }
                });

        }

        $scope.checkActividad = function (visible) {
            var msg = "Seguro que desea hacer visible este reporte";
            var msg2 = "Mostrar";
            var msg3 = "Registro ahora esta visible";
            if (!visible) {
                msg = "Seguro que desea ocultar este reporte";
                msg2 = "Ocultar";
                msg3 = "Registro ahora esta oculto";
            }


            SweetAlert.swal({
                    title: msg,
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55", confirmButtonText: msg2,
                    cancelButtonText: "No, cancelar!",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.post($rootScope.config.apiBase + '/actividad/checkActivity?flagVisible=' + visible, $scope.actividades, { headers: { 'Content-Type': 'application/json' } })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Modificado!", msg3, "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "actividad no eliminada", "error");
                    }
                });

        }

        $scope.checkActividadMonth = function (visible) {
            var msg = "Seguro que desea hacer visible este reporte";
            var msg2 = "Mostrar";
            var msg3 = "Registro ahora esta visible";
            if (!visible) {
                msg = "Seguro que desea ocultar este reporte";
                msg2 = "Ocultar";
                msg3 = "Registro ahora esta oculto";
            }


            SweetAlert.swal({
                    title: msg,
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55", confirmButtonText: msg2,
                    cancelButtonText: "No, cancelar!",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.post($rootScope.config.apiBase + '/actividad/checkActivity?flagVisible=' + visible, $scope.actividadesMonth, { headers: { 'Content-Type': 'application/json' } })
                            .then(function success(result) {
                                $scope.refreshMonth()
                                SweetAlert.swal("Modificado!", msg3, "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "actividad no eliminada", "error");
                    }
                });   

        }

        $scope.resumen = function(){
            if (!$scope.actividadesMonth) {
                $scope.refreshMonth()
            }
        }

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('reporteActCtrl', ['$scope', '$rootScope', '$http', '$sce', function ($scope, $rootScope, $http, $sce) {
        $scope.reporteAct = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = {};
        $scope.curIndex = null;
        $scope.isOpen = false;
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.getTitle = function () {
            if ($scope.model.dateFilter)
                return moment($scope.model.dateFilter).format("dddd D, MMMM YYYY");
        }

        $scope.monthTitle = function () {
            if ($scope.model.dateFilterMonth)
                return moment($scope.model.dateFilterMonth).format("MMMM YYYY");
        }

        $scope.yearTitle = function () {
            if ($scope.model.dateFilter)
                return moment($scope.model.dateFilter).format("YYYY");
        }

        $scope.dateChanged = function (val, dir) {
            $scope.myPromise =
                $http.get($rootScope.config.apiBase + '/actividad/getDateInfo/', {
                    params: { dateInit: $scope.model.dateFilter, dir: dir }
                })
                    .then(function success(result) {

                        console.log(result)
                        $scope.model.dateFilter = moment.utc(result.data.dateFilterInit).toDate();

                        if (result.data.message)
                            $rootScope.success("Info", result.data.message);

                        $scope.model.dir = dir;
                        $scope.url = $sce.getTrustedResourceUrl('report/ActividadesReport/?dateInit=' + $scope.model.dateFilter.toLocaleDateString());

                    }, function error(err) {
                        console.log(err);
                    });

        }

        $scope.monthChanged = function (val, dir) {
            $scope.model.dirMonth = dir;
            $scope.refreshMonth();
        }

        $scope.dateOptions = {
            maxDate: new Date()
        };

        $scope.monthOptions = {
            maxDate: new Date(),
            minMode: 'month'
        };

        $scope.yearOptions = {
            maxDate: new Date(),
            minMode: 'year'
        };

        $scope.refreshData = function () {
            $scope.dateChanged();
        }

        $scope.refreshMonth = function () {
            $scope.myPromise =
                $http.get($rootScope.config.apiBase + '/actividad/GetMonth/', {
                    params: { dateInit: $scope.model.dateFilterMonth, dir: $scope.model.dirMonth }
                })
                    .then(function success(result) {
                        $scope.model.dirMonth = null;
                        $scope.reporteActMonth = result.data;
                        if ($scope.reporteActMonth.dateFilterInit)
                            $scope.model.dateFilterMonth = moment.utc($scope.reporteActMonth.dateFilterInit).toDate();

                        if ($scope.reporteActMonth.message)
                            $rootScope.success("Info", $scope.reporteAct.message);

                    }, function error(err) {
                        console.log(err);
                    });
        }

        $scope.resumen = function () {
            if (!$scope.reporteActMonth) {
                $scope.refreshMonth()
            }
        }

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('actividadEditCtrl', ['$scope', '$rootScope', '$http', 'actividadId', function ($scope, $rootScope, $http, actividadId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.actividad = { nombre: '', telefono: '', direccion: '', multipleInstances: false}
        $scope.actividadId = actividadId
        $scope.getactividad = function () {
            if ($scope.actividadId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/actividad/' + $scope.actividadId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.actividad = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.actividadId > 0 ? "Editando actividad" : "Agregando actividad";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.actividadId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/actividad/' + $scope.actividadId, $scope.actividad, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('reporteAct', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/actividad/', $scope.actividad, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('reporteAct', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getactividad();
    }]);
angular
    .module('app')
    .controller('reporteGeneralCtrl', ['$scope', '$rootScope', '$http', '$sce', function ($scope, $rootScope, $http, $sce) {
        $scope.reporteGeneral = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = {};
        $scope.model.dateInit = new Date();
        $scope.model.dateEnd = new Date();
        $scope.model.reportName = null;
        $scope.model.grupo = null;
        $scope.model.periodo = null;
        $scope.model.destino = null;
        $scope.model.tipo = null;

        $scope.curIndex = null;
        $scope.isOpen = false;
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.periods = [{ key: "dia", value: "Diario" },
        { key: "mes", value: "Mensual" },
        { key: "anio", value: "Anual" }]

        $scope.tipos = [{ key: "listado", value: "Listado" },
        { key: "grafico", value: "Graficos" },]


        $scope.groups = [{ key: "producto", value: "Producto" },
        { key: "destino", value: "Destino" },
        { key: "fecha", value: "fecha" }]


        $scope.reports = [{ key: "GetProductsReport", value: "Movimiento de Productos" },
        { key: "GetCamionReport", value: "Reporte de Camiones" },
        { key: "GetChoferReport", value: "Reporte de Choferes" }]

        $scope.getTitle = function () {

        }

        $scope.isValid = function () {
            return $scope.model.dateInit &&
                $scope.model.dateEnd &&
                $scope.model.tipo &&
                $scope.model.periodo &&
                $scope.model.grupo;
        }

        $scope.showReport = function () {
            $scope.url = $sce.getTrustedResourceUrl('AllReport/General/?name=' + $scope.model.reportName
                + '&dateInit=' + $scope.model.dateInit.toLocaleDateString()
                + '&dateEnd=' + $scope.model.dateEnd.toLocaleDateString()
                + '&tipo=' + $scope.model.tipo
                + '&periodo=' + $scope.model.periodo
                + '&grupo=' + $scope.model.grupo)

        }



        $scope.dateOptions = {
            maxDate: new Date()
        };

        $scope.monthOptions = {
            maxDate: new Date(),
            minMode: 'month'
        };

        $scope.yearOptions = {
            maxDate: new Date(),
            minMode: 'year'
        };

        $scope.refreshData = function () {
            $scope.showReport();
        }


    }]);
angular
    .module('app')
    .controller('rolCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.roles = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Permisos/Rol', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.roles = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteRol = function (rolId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este rol!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/Permisos/Rol/' + rolId, $scope.rol, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Usuario Eliminada.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "Usuario no eliminada", "error");
                    }
                });
        }

        $scope.editRol = function (rolId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "app/rol/rol.edit.ctrl.html",
                controller: "rolEditCtrl",
                resolve: {
                    rolId: function () {
                        return rolId;
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

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('usuarioCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.usuarios = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Permisos/Usuario', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.usuarios = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteUsuario = function (usuarioId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta usuario!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/Permisos/Usuario/' + usuarioId, $scope.usuario, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Usuario Eliminada.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "Usuario no eliminada", "error");
                    }
                });
        }

        $scope.editUsuario = function (usuarioId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "usuario/usuario.edit.ctrl.html",
                controller: "usuarioEditCtrl",
                resolve: {
                    usuarioId: function () {
                        return usuarioId;
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

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('selloCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.sellos = []
        $scope.filter = {};
        $scope.search = ""
        $scope.myPromise = false;
        $scope.lotes = []
        $scope.estatus = []
        $scope.curEstatus = '';
        $scope.curPage = 0;
        $scope.loadingMsg = "Cargando...";
        $scope.curDetalleId = 0;

        $scope.update = function (s) {
            $scope.myPromise = $http.put($rootScope.config.apiBase + '/sello/' + s.id, s, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    console.log(result)
                    $rootScope.success('sello', 'sello guardado')
                    for (var i = 0; i < $scope.sellos.length; i++) {
                        if ($scope.sellos[i].id === result.data.id) {
                            $scope.sellos[i].selloStatus = result.data.selloStatus
                            $scope.sellos[i].selloStatusId = result.data.selloStatusId
                            break
                        }
                    }
                    $scope.curDetalleId = 0;

                },
                function error(err) {
                });
        }

        $scope.cancel = function () {
            $scope.curDetalleId = 0;
        }

        $scope.editsello = function (s) {
            $scope.curDetalleId = (s.id > 0) ? s.id : 0;
        }

        $scope.addsello = function () {
            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "sello/sello.edit.ctrl.html",
                controller: "selloEditCtrl",
                resolve: {
                    selloId: function () {
                        return 0;
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


        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/sello',
                { 'params': $scope.filter },
                { 'Content-Type': 'application/json' }
            )
                .then(function success(result) {
                    $scope.sellos = result.data.sellos;
                    $scope.filter = result.data.filter;
                    if (result.data.lotes)
                        $scope.lotes = result.data.lotes;
                    if (result.data.estatus)
                        $scope.estatus = result.data.estatus;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.changeLote = function () {
            $scope.filter.filter = $scope.search;
            $scope.filter.column = 1;
            $scope.filter.page = 1;
            $scope.refreshData();
        }

        $scope.changeEstatus = function () {
            $scope.filter.estatus = $scope.curEstatus;
            $scope.refreshData();
        }

        $scope.totalPages = function () {
            var nume = $scope.filter.totalItems
            var deno = $scope.filter.pageSize > 0 ? $scope.filter.pageSize : 1
            return Math.ceil(nume / deno)
        }

        $scope.movePage = function (dir) {
            if (dir === "right" && $scope.filter.page < $scope.filter.totalItems) {
                $scope.filter.page++;
            }

            if (dir === "left" && $scope.filter.page > 0) {
                $scope.filter.page--;
            }

            $scope.refreshData();
        }

        $scope.deletesello = function (lote) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este lote (" + lote + ")!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/sello/DeleteLote/?id=' + lote, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Lote Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "sello no eliminada", "error");
                    }
                });
        }


        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('selloEditCtrl', ['$scope', '$rootScope', '$http', 'selloId', function ($scope, $rootScope, $http, selloId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.sello = { inicial: '', final: '', espacios: 6, lote: ''}
        $scope.selloId = selloId
        $scope.getSello = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/sello/GetCreate',
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.sello = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
        }
        $scope.titleMsg = $scope.selloId > 0 ? "Editando sello" : "Agregando sello";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.selloId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/sello/' + $scope.selloId, $scope.sello, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('sello', 'sello guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/sello/', $scope.sello, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('sellos', 'sello guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getSello();
    }]);
angular
    .module('app')
    .controller('verificacionCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.verificaciones = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/verificacion', { headers: { 'Content-Type': 'application/json' } })
                .then(function success(result) {
                    $scope.verificaciones = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteverificacion = function (verificacionId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta verificacion!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/verificacion/' + verificacionId, $scope.verificacion, { headers: { 'Content-Type': 'application/json' } })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "verificacion Eliminada.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "verificacion no eliminada", "error");
                    }
                });
        }

        $scope.editVerificacion = function (verificacionId) {
            $rootScope.goTo("/verificacion/" + verificacionId)
        };

        $scope.refreshData()
    }]);
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
angular
    .module('app')
    .controller('terminalCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.terminales = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Terminal', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.terminales = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteTerminal = function(terminalId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta terminal!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/Terminal/' + terminalId, $scope.terminal, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Terminal Eliminada.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "Terminal no eliminada", "error");
                    }
                });
        }

        $scope.editTerminal = function (terminalId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "terminal/terminal.edit.ctrl.html",
                controller: "terminalEditCtrl",
                resolve: {
                    terminalId: function () {
                        return terminalId;
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

        $scope.refreshData()
    }]);
angular
    .module('app')
    .controller('terminalEditCtrl', ['$scope', '$rootScope', '$http', 'terminalId', function ($scope, $rootScope, $http, terminalId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.terminal = { nombre: '', telefono: '', direccion: '', multipleInstances: false}
        $scope.terminalId = terminalId
        $scope.getTerminal = function () {
            if ($scope.terminalId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/Terminal/' + $scope.terminalId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.terminal = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.terminalId > 0 ? "Editando Terminal" : "Agregando Terminal";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.terminalId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Terminal/' + $scope.terminalId, $scope.terminal, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Terminales', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Terminal/', $scope.terminal, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Terminales', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getTerminal();
    }]);
angular.module('app')
    .directive('dynamic', function($compile) {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                scope.$watch(attrs.dynamic, function(html) {
                    element[0].innerHTML = html;
                    $compile(element.contents())(scope);
                });
            }
        };
    });

angular.module('app')
    .directive('inputform', function () {
        return {

            restrict: 'E',
            link: function (scope, element, attrs) {

                scope.display = attrs.display
                scope.collabel = attrs.collabel ? attrs.collabel : 'col-md-2'
                scope.collinput = attrs.collinput ? attrs.collinput : 'col-md-10'
                scope.name = attrs.name
                scope.disabled = attrs.disabled == 'true'

                scope.ngRequired = attrs.ngRequired
                scope.type = attrs.type ? attrs.type : 'text'
                scope.pat = '';
                if (attrs.type == 'email')
                    scope.pat = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/

             

            },
            scope: {
                ngModel: '=',
                field: '=field',
                ngChange: '='
            },
            templateUrl: function ($state, $e) {
              
                var  tpl = 'directives/input.form/input.directive.t.h.html';
                if ($e.dir == 'v')
                    tpl = 'directives/input.form/input.directive.t.v.html';;

                return tpl;
            },
        };
    });


angular.module('app')
    .directive('selectform', function () {
        return {
            restrict: 'E',
            replace: true,
            link: function (scope, element, attrs) {

                scope.display = attrs.display
                scope.collabel = attrs.collabel ? attrs.collabel : 'col-md-2'
                scope.collinput = attrs.collinput ? attrs.collinput : 'col-md-10'
                scope.name = attrs.name
                scope.ngRequired = attrs.ngRequired == 'true'
                scope.disabled = attrs.disabled == 'true'
                scope.pat = '';
          
            },
            scope: {
                ngModel: '=',
                field: '=field',
                option: '=option',
                ngChange: '='
            },
            templateUrl: function ($state, $e) {

                var tpl = 'directives/input.form/input.directive.s.h.html';
                if ($e.dir == 'v')
                    tpl = 'directives/input.form/input.directive.s.v.html';;

                return tpl;
            },
        };
    });


angular.module('app')
    .directive('dateform', function () {
        return {
            restrict: 'E',
            replace: true,
            link: function (scope, element, attrs) {
                scope.opened = false;
                scope.dateOptions = {
                };
                scope.eventSources = []
                scope.change = function (value) {

                 
                }

                scope.dateSelection = function (value) {

                
                }
                scope.formats = ['MM/dd/yyyy', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
                scope.format = scope.formats[0];
                scope.altInputFormats = ['M!/d!/yyyy'];
                scope.display = attrs.display
                scope.collabel = attrs.collabel ? attrs.collabel : 'col-md-2'
                scope.collinput = attrs.collinput ? attrs.collinput : 'col-md-10'
                scope.name = attrs.name
                scope.ngRequired = attrs.ngRequired == 'true'
                scope.type = attrs.type ? attrs.type : 'text'
                scope.pat = '';
                scope.disabled = attrs.disabled == 'true'
                if (attrs.type == 'email')
                    scope.pat = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/

              
            },
            scope: {
                ngModel: '=ngModel',
                field: '=field',
                ngChange: '='
            },
            templateUrl: function ($state, $e) {
                var tpl = 'directives/input.form/input.directive.d.h.html';
                if ($e.dir == 'v')
                    tpl = 'directives/input.form/input.directive.d.v.html';;

                return tpl;
            },
        };
    });
