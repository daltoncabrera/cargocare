angular
    .module('app')
    .controller('cisternaEditCtrl', ['$scope', '$rootScope', '$http', 'cisternaId', function ($scope, $rootScope, $http, cisternaId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = { cisterna: { compartimentos: 0 }, detalle: [] }
        $scope.medidas = ["1/8", "2/8", "3/8", "4/8", "5/8", "6/8", "7/8", "1/16", "3/16", "5/16", "7/16", "9/16", "10/16", "11/16", "9/16", "13/16"]
        $scope.$watch('model.cisterna.compartimentos', function (n, o) { $scope.changeCompartimentos(n, o) }, true);

        $scope.cisternaId = cisternaId
        $scope.compartimentosArray = [{key:1, value:'Uno'},{key:2, value:'Dos'},{key:3, value:'Tres'},{key:4, value:'Cuatro'},{key:5, value:'Cinco'},{key:6, value:'Seis'},]
        $scope.getCisterna = function () {
            if ($scope.cisternaId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/cisterna/' + $scope.cisternaId,
                        { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        result.data.detalle = result.data.detalle.sort(function (a, b) { return a.compartimento - b.compartimento; })
                            $scope.model = result.data;
                        },
                        function error(err) {
                            console.log(err);
                        });
            } 
        }
        $scope.titleMsg = $scope.cisternaId > 0 ? "Editando cisterna" : "Agregando cisterna";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.changeCompartimentos = function (n, o) {
            //console.log($scope.cisternaForm)
            if (!$scope.cisternaForm.$dirty)
                return;
            $scope.model.detalle = []
            if (n != o) {
                var deta = []
                for (var i = 1; i <= n; i++) {
                    var cap = 3000;
                   var e = { compartimento: i, capacidad: cap, hasChapa: true, hasBoca: true, hasDescarga: true }
                   deta.push(e);
                }
                $scope.model.detalle =  deta.sort(function (a, b) { return a.compartimento - b.compartimento; })
           }
        }

        $scope.saveData = function (valid) {
            if (!valid) {
                return;
            }

            if ($scope.cisternaId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/cisterna/' + $scope.cisternaId, $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('cisterna', 'cisterna guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/cisterna/', $scope.model, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('cisternas', 'cisterna guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }

        $scope.getCisterna();
    }]);