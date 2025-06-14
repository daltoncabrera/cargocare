angular
    .module('app')
    .controller('precargaCtrl', ['$scope', '$rootScope', '$location', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $location, $http, Popeye, SweetAlert) {
        $scope.precargas = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/precarga', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.precargas = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

 

        $scope.deleteprecarga = function (precargaId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este precarga!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/precarga/' + precargaId, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "precarga Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "precarga no eliminada", "error");
                    }
                });
        }

        $scope.editprecarga = function (precargaId) {
            $location.path('/precarga/' + precargaId);
        };


        $scope.print = function (id) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Precarga/' + id;
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
               
            });
        };

        $scope.refreshData()
    }]);