angular
    .module('app')
    .controller('selloEditCtrl', ['$scope', '$rootScope', '$http', 'selloId', function ($scope, $rootScope, $http, selloId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.sello = { inicial: '', final: '', espacios: 6, lote: ''}
        $scope.selloId = selloId
        $scope.getSello = function () {
            $scope.myPromise = $http.get($rootScope.config.apiBase + '/sello/GetCreate',
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.sello = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
        }
        $scope.titleMsg = $scope.selloId > 0 ? "Editando sello" : "Agregando sello";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.selloId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/sello/' + $scope.selloId, $scope.sello, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('sello', 'sello guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/sello/', $scope.sello, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('sellos', 'sello guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getSello();
    }]);