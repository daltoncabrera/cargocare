angular
    .module('app')
    .run(['$location', '$rootScope', '$window', 'toasty', '$timeout', 'Upload', function ($location, $rootScope, $window, toasty, $timeout,Upload) {

        $rootScope.errorTriggered = false;
        $rootScope.loadingMsg = "";
        $rootScope.config = { apiBase: location.origin + '/api' };
        $rootScope.title = '';
        $rootScope.navName = '';
        $rootScope.parentName = '';
        $rootScope.parentTitle = '';

        $rootScope.init = function (defaults) {

        }


        $rootScope.timePickerOptions = {
            step: 15,
            minTime: '7:00am',
            maxTime: '5:00pm',
            timeFormat: 'g:ia',
            appendTo: 'body'
        };

        $rootScope.hasClaim = function () {
            var resutl = false;
            for (var i = 0; i < arguments.length; i++) {
                if ($rootScope.info.claims.indexOf(arguments[i]) >= 0) {
                    resutl = true;
                    break;
                }
            }

            return resutl;
        }

        $rootScope.goTo = function (path) {
            $location.path(path);
        }

        $rootScope.goToRoot = function (path) {
            var url = path;
            window.location = url;
        }
        $rootScope.$on("$routeChangeSuccess", function (event, currentRoute, previousRoute) {
            $rootScope.title = currentRoute.title;
            $rootScope.navName = currentRoute.navName;
            $rootScope.parentName = currentRoute.parentName;
            $rootScope.parentTitle = currentRoute.parentTitle;

        });

        $rootScope.uploadFile = function (file, url, callback) {
            file.upload = Upload.upload({
                url: url,
                data: { file: file },
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                    if (callback)
                        callback(response.data);
                });
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    $rootScope.error(":(", errorMsg)
                }
            }, function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }


        $rootScope.success = function (title, msg) {
            toasty["success"]({
                title: title,
                msg: msg,
                showClose: true,
                clickToClose: false,
                sound: true,
                html: false,
                shake: false,
                onRemove: function () {

                }
            });
        }

        $rootScope.error = function (title, msg) {

            toasty["error"]({
                title: title,
                msg: msg,
                showClose: true,
                clickToClose: false,
                sound: true,
                html: false,
                shake: false,
                onRemove: function () {

                }
            });




        };

    }]);


angular.module('app').config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push("responseInterceptor");
}]);

angular.module('app').config(['toastyConfigProvider', 'calendarConfig', function (toastyConfigProvider, calendarConfig) {
    toastyConfigProvider.setConfig({
        sound: true,
        position: 'top-right',
        shake: true,
        timeout: 5000
    });

    // Use either moment or angular to format dates on the calendar. Default angular. Setting this will override any date formats you have already set.
    calendarConfig.dateFormatter = 'moment';

    // This will configure times on the day view to display in 24 hour format rather than the default of 12 hour
    //calendarConfig.allDateFormats.moment.date.hour = 'HH:mm a';

    // This will configure the day view title to be shorter
    // calendarConfig.allDateFormats.moment.title.day = 'ddd D MMM';
    calendarConfig.allDateFormats.moment.date.time = 'hh:mma';

    // This will set the week number hover label on the month view
    calendarConfig.i18nStrings.weekNumber = 'Week {week}';


    // Make the week view more like the day view, ***with the caveat that event end times are ignored***.
    calendarConfig.showTimesOnWeekView = true;

}]);



