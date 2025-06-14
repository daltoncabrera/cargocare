angular
    .module('app').factory('responseInterceptor', ['$q', '$location', 'toasty', '$rootScope', function ($q, $location, toasty, $rootScope) {
    return {
        response: function (response) {
            if (response.status === 401) {
                console.log("Response 401");
            }
            return response || $q.when(response);
        },

        responseError: function (rejection) {

            if (rejection.status === 401) {
                console.log("Response Error 401", rejection);
               $location.path('/');
            }

            var result = rejection.statusText;
            if (rejection.data)
                result = rejection.data;
            $rootScope.error('Error ' + rejection.status + '!', result);

         
            return $q.reject(rejection);
        }
    }
}])