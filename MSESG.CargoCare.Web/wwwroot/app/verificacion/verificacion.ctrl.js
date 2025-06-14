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