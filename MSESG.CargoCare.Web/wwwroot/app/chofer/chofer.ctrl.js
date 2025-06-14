angular
    .module('app')
    .controller('choferCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.chofers = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/chofer', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.chofers = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deletechofer = function(choferId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este chofer!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/chofer/' + choferId, $scope.chofer, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "chofer Eliminado.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "chofer no eliminada", "error");
                    }
                });
        }

        $scope.editchofer = function (choferId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "chofer/chofer.edit.ctrl.html",
                controller: "choferEditCtrl",
                resolve: {
                    choferId: function () {
                        return choferId;
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