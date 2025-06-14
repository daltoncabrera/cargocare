angular
    .module('app')
    .controller('camionCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.camions = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/camion', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.camions = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deletecamion = function(camionId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este camion!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/camion/' + camionId, $scope.camion, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "camion Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "camion no eliminada", "error");
                    }
                });
        }

        $scope.editcamion = function (camionId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "camion/camion.edit.ctrl.html",
                controller: "camionEditCtrl",
                resolve: {
                    camionId: function () {
                        return camionId;
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