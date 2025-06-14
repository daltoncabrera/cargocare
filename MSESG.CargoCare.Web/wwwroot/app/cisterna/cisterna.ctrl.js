angular
    .module('app')
    .controller('cisternaCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.cisternas = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/cisterna', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.cisternas = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deletecisterna = function(cisternaId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este cisterna!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/cisterna/' + cisternaId, $scope.cisterna, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "cisterna Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "cisterna no eliminada", "error");
                    }
                });
        }

        $scope.editcisterna = function (cisternaId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "cisterna/cisterna.edit.ctrl.html",
                controller: "cisternaEditCtrl",
                resolve: {
                    cisternaId: function () {
                        return cisternaId;
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