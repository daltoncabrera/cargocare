angular
    .module('app')
    .controller('planificacionCtrl', ['$scope', '$rootScope', '$http', '$location', 'Popeye', 'SweetAlert', function($scope, $rootScope, $http, $location, Popeye, SweetAlert) {
        $scope.planificacions = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";


        $scope.refreshData = function() {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/planificacion', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.planificacions = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteplanificacion = function(planificacionId) {
            SweetAlert.swal({
                    title: "Esta seguro?",
                    text: "Esta seguro de quere elimnar este planificacion!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Eliminar!",
                    cancelButtonText: "No, cancelar!",
                    closeOnConfirm: false,
                    closeOnCancel: true
                },
                function(isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/planificacion/' + planificacionId, $scope.planificacion, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "planificacion Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "planificacion no eliminada", "error");
                    }
                });
        }

        $scope.editplanificacion = function(planificacionId) {
            if (!planificacionId)
                planificacionId = 0;

            $location.path('planificacion/' + planificacionId);
        };

        $scope.refreshData()
    }]);