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