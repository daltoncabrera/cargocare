angular
    .module('app')
    .controller('actividadEditCtrl', ['$scope', '$rootScope', '$http', 'actividadId', function ($scope, $rootScope, $http, actividadId) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.actividad = { nombre: '', telefono: '', direccion: '', multipleInstances: false}
        $scope.actividadId = actividadId
        $scope.getactividad = function () {
            if ($scope.actividadId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/actividad/' + $scope.actividadId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.actividad = result.data;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }
        $scope.titleMsg = $scope.actividadId > 0 ? "Editando actividad" : "Agregando actividad";

        $scope.cancel = function () {
            $scope.$close()
        }

        $scope.saveData = function (valid) {
            console.log(valid)
            if (!valid) {

                return;

            }

            if ($scope.actividadId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/actividad/' + $scope.actividadId, $scope.actividad, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('reporteAct', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/actividad/', $scope.actividad, { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $rootScope.success('reporteAct', 'Registro guardado')
                        $scope.$close()
                    },
                    function error(err) {
                    });
            }
        }



        $scope.getactividad();
    }]);