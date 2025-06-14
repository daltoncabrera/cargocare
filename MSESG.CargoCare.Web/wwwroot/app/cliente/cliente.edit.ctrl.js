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