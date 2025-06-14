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