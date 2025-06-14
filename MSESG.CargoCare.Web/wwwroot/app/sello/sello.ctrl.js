angular
    .module('app')
    .controller('selloCtrl', ['$scope', '$rootScope', '$http', 'Popeye', 'SweetAlert', function ($scope, $rootScope, $http, Popeye, SweetAlert) {
        $scope.sellos = []
        $scope.filter = {};
        $scope.search = ""
        $scope.myPromise = false;
        $scope.lotes = []
        $scope.estatus = []
        $scope.curEstatus = '';
        $scope.curPage = 0;
        $scope.loadingMsg = "Cargando...";
        $scope.curDetalleId = 0;

        $scope.update = function (s) {
            $scope.myPromise = $http.put($rootScope.config.apiBase + '/sello/' + s.id, s, { 'Content-Type': 'application/json' })
                .then(function success(result) {
                    console.log(result)
                    $rootScope.success('sello', 'sello guardado')
                    for (var i = 0; i < $scope.sellos.length; i++) {
                        if ($scope.sellos[i].id === result.data.id) {
                            $scope.sellos[i].selloStatus = result.data.selloStatus
                            $scope.sellos[i].selloStatusId = result.data.selloStatusId
                            break
                        }
                    }
                    $scope.curDetalleId = 0;

                },
                function error(err) {
                });
        }

        $scope.cancel = function () {
            $scope.curDetalleId = 0;
        }

        $scope.editsello = function (s) {
            $scope.curDetalleId = (s.id > 0) ? s.id : 0;
        }

        $scope.addsello = function () {
            // Open a modal to show the selected user profile
            var modal = Popeye.openModal({
                templateUrl: "sello/sello.edit.ctrl.html",
                controller: "selloEditCtrl",
                resolve: {
                    selloId: function () {
                        return 0;
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


        $scope.refreshData = function () {

            $scope.myPromise = $http.get($rootScope.config.apiBase + '/sello',
                { 'params': $scope.filter },
                { 'Content-Type': 'application/json' }
            )
                .then(function success(result) {
                    $scope.sellos = result.data.sellos;
                    $scope.filter = result.data.filter;
                    if (result.data.lotes)
                        $scope.lotes = result.data.lotes;
                    if (result.data.estatus)
                        $scope.estatus = result.data.estatus;
                }, function error(err) {
                    console.log(err);
                });
        }

        $scope.changeLote = function () {
            $scope.filter.filter = $scope.search;
            $scope.filter.column = 1;
            $scope.filter.page = 1;
            $scope.refreshData();
        }

        $scope.changeEstatus = function () {
            $scope.filter.estatus = $scope.curEstatus;
            $scope.refreshData();
        }

        $scope.totalPages = function () {
            var nume = $scope.filter.totalItems
            var deno = $scope.filter.pageSize > 0 ? $scope.filter.pageSize : 1
            return Math.ceil(nume / deno)
        }

        $scope.movePage = function (dir) {
            if (dir === "right" && $scope.filter.page < $scope.filter.totalItems) {
                $scope.filter.page++;
            }

            if (dir === "left" && $scope.filter.page > 0) {
                $scope.filter.page--;
            }

            $scope.refreshData();
        }

        $scope.deletesello = function (lote) {
            SweetAlert.swal({
                title: "Esta seguro?",
                text: "Esta seguro de quere elimnar este lote (" + lote + ")!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Eliminar!",
                cancelButtonText: "No, cancelar!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $http.delete($rootScope.config.apiBase + '/sello/DeleteLote/?id=' + lote, { 'Content-Type': 'application/json' })
                            .then(function success(result) {
                                $scope.refreshData()
                                SweetAlert.swal("Eliminado!", "Lote Eliminado.", "success")
                            })

                    } else {
                        // SweetAlert.swal("Cancelado", "sello no eliminada", "error");
                    }
                });
        }


        $scope.refreshData()
    }]);