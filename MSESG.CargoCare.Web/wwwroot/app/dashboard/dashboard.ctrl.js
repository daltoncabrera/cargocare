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