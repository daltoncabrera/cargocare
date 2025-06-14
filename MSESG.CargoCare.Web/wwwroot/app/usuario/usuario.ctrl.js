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