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