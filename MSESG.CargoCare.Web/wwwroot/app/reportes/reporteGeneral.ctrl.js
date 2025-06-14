angular
    .module('app')
    .controller('reporteGeneralCtrl', ['$scope', '$rootScope', '$http', '$sce', function ($scope, $rootScope, $http, $sce) {
        $scope.reporteGeneral = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = {};
        $scope.model.dateInit = new Date();
        $scope.model.dateEnd = new Date();
        $scope.model.reportName = null;
        $scope.model.grupo = null;
        $scope.model.periodo = null;
        $scope.model.destino = null;
        $scope.model.tipo = null;

        $scope.curIndex = null;
        $scope.isOpen = false;
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.periods = [{ key: "dia", value: "Diario" },
        { key: "mes", value: "Mensual" },
        { key: "anio", value: "Anual" }]

        $scope.tipos = [{ key: "listado", value: "Listado" },
        { key: "grafico", value: "Graficos" },]


        $scope.groups = [{ key: "producto", value: "Producto" },
        { key: "destino", value: "Destino" },
        { key: "fecha", value: "fecha" }]


        $scope.reports = [{ key: "GetProductsReport", value: "Movimiento de Productos" },
        { key: "GetCamionReport", value: "Reporte de Camiones" },
        { key: "GetChoferReport", value: "Reporte de Choferes" }]

        $scope.getTitle = function () {

        }

        $scope.isValid = function () {
            return $scope.model.dateInit &&
                $scope.model.dateEnd &&
                $scope.model.tipo &&
                $scope.model.periodo &&
                $scope.model.grupo;
        }

        $scope.showReport = function () {
            $scope.url = $sce.getTrustedResourceUrl('AllReport/General/?name=' + $scope.model.reportName
                + '&dateInit=' + $scope.model.dateInit.toLocaleDateString()
                + '&dateEnd=' + $scope.model.dateEnd.toLocaleDateString()
                + '&tipo=' + $scope.model.tipo
                + '&periodo=' + $scope.model.periodo
                + '&grupo=' + $scope.model.grupo)

        }



        $scope.dateOptions = {
            maxDate: new Date()
        };

        $scope.monthOptions = {
            maxDate: new Date(),
            minMode: 'month'
        };

        $scope.yearOptions = {
            maxDate: new Date(),
            minMode: 'year'
        };

        $scope.refreshData = function () {
            $scope.showReport();
        }


    }]);