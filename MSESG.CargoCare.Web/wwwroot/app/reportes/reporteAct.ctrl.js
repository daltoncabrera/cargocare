angular
    .module('app')
    .controller('reporteActCtrl', ['$scope', '$rootScope', '$http', '$sce', function ($scope, $rootScope, $http, $sce) {
        $scope.reporteAct = [];
        $scope.myPromise = false;
        $scope.loadingMsg = "Cargando...";
        $scope.model = {};
        $scope.curIndex = null;
        $scope.isOpen = false;
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.getTitle = function () {
            if ($scope.model.dateFilter)
                return moment($scope.model.dateFilter).format("dddd D, MMMM YYYY");
        }

        $scope.monthTitle = function () {
            if ($scope.model.dateFilterMonth)
                return moment($scope.model.dateFilterMonth).format("MMMM YYYY");
        }

        $scope.yearTitle = function () {
            if ($scope.model.dateFilter)
                return moment($scope.model.dateFilter).format("YYYY");
        }

        $scope.dateChanged = function (val, dir) {
            $scope.myPromise =
                $http.get($rootScope.config.apiBase + '/actividad/getDateInfo/', {
                    params: { dateInit: $scope.model.dateFilter, dir: dir }
                })
                    .then(function success(result) {

                        console.log(result)
                        $scope.model.dateFilter = moment.utc(result.data.dateFilterInit).toDate();

                        if (result.data.message)
                            $rootScope.success("Info", result.data.message);

                        $scope.model.dir = dir;
                        $scope.url = $sce.getTrustedResourceUrl('report/ActividadesReport/?dateInit=' + $scope.model.dateFilter.toLocaleDateString());

                    }, function error(err) {
                        console.log(err);
                    });

        }

        $scope.monthChanged = function (val, dir) {
            $scope.model.dirMonth = dir;
            $scope.refreshMonth();
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
            $scope.dateChanged();
        }

        $scope.refreshMonth = function () {
            $scope.myPromise =
                $http.get($rootScope.config.apiBase + '/actividad/GetMonth/', {
                    params: { dateInit: $scope.model.dateFilterMonth, dir: $scope.model.dirMonth }
                })
                    .then(function success(result) {
                        $scope.model.dirMonth = null;
                        $scope.reporteActMonth = result.data;
                        if ($scope.reporteActMonth.dateFilterInit)
                            $scope.model.dateFilterMonth = moment.utc($scope.reporteActMonth.dateFilterInit).toDate();

                        if ($scope.reporteActMonth.message)
                            $rootScope.success("Info", $scope.reporteAct.message);

                    }, function error(err) {
                        console.log(err);
                    });
        }

        $scope.resumen = function () {
            if (!$scope.reporteActMonth) {
                $scope.refreshMonth()
            }
        }

        $scope.refreshData()
    }]);