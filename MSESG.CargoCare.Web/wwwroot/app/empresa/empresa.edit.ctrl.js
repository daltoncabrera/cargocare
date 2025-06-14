angular
    .module('app')
    .controller('empresaEditCtrl', ['$scope', '$rootScope', '$http', '$location', '$routeParams', function ($scope, $rootScope, $http, $location, $routeParams) {
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando..."
        $scope.empresa = { id: 0 }
        $scope.empresaId = $routeParams.id
        $scope.settings = {}
        $scope.getEmpresa = function () {
            if ($scope.empresaId > 0) {
                $scope.myPromise = $http.get($rootScope.config.apiBase + '/Empresa/' + $scope.empresaId,
                    { 'Content-Type': 'application/json' })
                    .then(function success(result) {
                        $scope.empresa = result.data.empresa;
                        if (result.data.settings)
                            $scope.settings = result.data.settings;
                    },
                    function error(err) {
                        console.log(err);
                    });
            }
        }

        $scope.titleMsg = $scope.empresaId > 0 ? "Editando Empresa" : "Agregando Empresa";


        $scope.curfile = false;

        $scope.changeLogo = function (file, errFiles) {
            if (file) {
                $scope.f = file;
                $scope.errFile = errFiles && errFiles[0];
                var url = "api/Empresa/Upload/?empresaId=" + $scope.empresa.id;
                $rootScope.uploadFile(file,
                    url,
                    function(data) {
                        $scope.empresa.logo = data;
                    });
            }
        }

        $scope.saveSettings = function () {
            $scope.settings.empresaId = $scope.empresa.id;
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Empresa/SaveSettings/', $scope.settings, { headers: { 'Content-Type': 'application/json' } })
                .then(function success(result) {
                    $rootScope.success('Empresas', 'Registro guardado')
                    $scope.settings = result.data;
                },
                    function error(err) { });
        }


        $scope.sendTest = function () {
            $scope.settings.empresaId = $scope.empresa.id;
            $scope.myPromise = $http.post($rootScope.config.apiBase + '/Empresa/SendTest/' + $scope.empresa.id, null, { headers: { 'Content-Type': 'application/json' } })
                .then(function success(result) {
                    $rootScope.success('Empresas', 'Correo de prueba enviado')

                },
                    function error(err) { });
        }


        $scope.cancel = function () {
            $scope.$close()
        }


        $scope.saveData = function (valid) {

            if (!valid) {

                return;

            }

            if ($scope.empresaId > 0) {
                $scope.myPromise = $http.put($rootScope.config.apiBase + '/Empresa/' + $scope.empresaId, $scope.empresa, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $rootScope.success('Empresas', 'Registro guardado');
                        $scope.empresa = result.data;

                    },
                    function error(err) {
                    });
            } else {
                $scope.myPromise = $http.post($rootScope.config.apiBase + '/Empresa/', $scope.empresa, { headers: { 'Content-Type': 'application/json' } })
                    .then(function success(result) {
                        $rootScope.success('Empresas', 'Registro guardado')
                        $scope.empresa = result.data;
                    },
                    function error(err) {
                    });
            }
        }

        $scope.getEmpresa();
    }]);