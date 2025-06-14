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