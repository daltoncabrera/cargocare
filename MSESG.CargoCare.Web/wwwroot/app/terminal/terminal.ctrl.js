angular
    .module('app')
    .controller('terminalCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.terminales = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/Terminal', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.terminales = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteTerminal = function(terminalId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta terminal!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/Terminal/' + terminalId, $scope.terminal, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Terminal Eliminada.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "Terminal no eliminada", "error");
                    }
                });
        }

        $scope.editTerminal = function (terminalId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "terminal/terminal.edit.ctrl.html",
                controller: "terminalEditCtrl",
                resolve: {
                    terminalId: function () {
                        return terminalId;
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