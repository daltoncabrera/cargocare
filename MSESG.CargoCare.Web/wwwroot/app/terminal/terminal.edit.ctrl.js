angular
    .module('app')
    .controller('terminalEditCtrl', ['$scope', '$rootScope', '$http', 'terminalId', function ($scope, $rootScope, $http, terminalId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.terminal = { nombre: '', telefono: '', direccion: '', multipleInstances: false}
        $scope.terminalId = terminalId
        $scope.getTerminal = function () {
            if ($scope.terminalId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/Terminal/' + $scope.terminalId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.terminal = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.terminalId > 0 ? "Editando Terminal" : "Agregando Terminal";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.terminalId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Terminal/' + $scope.terminalId, $scope.terminal, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Terminales', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Terminal/', $scope.terminal, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('Terminales', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getTerminal();
    }]);