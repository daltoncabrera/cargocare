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