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