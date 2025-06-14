angular
    .module('app')
    .controller('productoCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.productoes = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";

        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/producto', { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    $scope.productoes = result.data;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.deleteproducto = function(productoId)
        {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar esta producto!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/producto/' + productoId, $scope.producto, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "producto Eliminada.", "success")
                            })
                      
                    } else {
                       // SweetAlert.swal("Cancelado", "producto no eliminada", "error");
                    }
                });
        }

        $scope.editproducto = function (productoId) {

            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "producto/producto.edit.ctrl.html",
                controller: "productoEditCtrl",
                resolve: {
                    productoId: function () {
                        return productoId;
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