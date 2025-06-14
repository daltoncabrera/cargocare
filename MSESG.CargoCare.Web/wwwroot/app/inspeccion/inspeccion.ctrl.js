angular
    .module('app')
    .controller('inspeccionCtrl', ['$scope', '$rootScope', '$location', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope,  $location, $http, Popeye, SweetAlert) {
        $scope.inspecciones = [];
        $scope.ordenes = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.togleDetail = function(o) {
            o.showDetail = !o.showDetail
        }

        $scope.refreshData = function () {

            $scope.myPromise2 = $http.get($rootScope.config.apiBase + '/inspeccion', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.inspecciones = result.data;
                }, function error(err) {
                    console.log(err);
                });

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/inspeccion/ordenes', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.ordenes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteinspeccion = function (inspeccionId) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este inspeccion!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/inspeccion/' + inspeccionId, $scope.inspeccion, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "inspeccion Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "inspeccion no eliminada", "error");
                    }
                });
        }

        $scope.print = function (id) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "inspeccion/inspeccion.print.ctrl.html",
                containerClass: 'popprint',
                controller: "inspeccionPrintCtrl",
                resolve: {
                    url: function () {
                        return '/Report/Inspeccion/' + id;
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



        $scope.editinspeccion = function (inspeccionId) {
            $location.path('/inspeccion/0/' + inspeccionId);
        };

        $scope.refreshData()
    }]);