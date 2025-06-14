angular.module('app')
    .directive('inputform', function () {
        return {

            restrict: 'E',
            link: function (scope, element, attrs) {

                scope.display = attrs.display
                scope.collabel = attrs.collabel ? attrs.collabel : 'col-md-2'
                scope.collinput = attrs.collinput ? attrs.collinput : 'col-md-10'
                scope.name = attrs.name
                scope.disabled = attrs.disabled == 'true'

                scope.ngRequired = attrs.ngRequired
                scope.type = attrs.type ? attrs.type : 'text'
                scope.pat = '';
                if (attrs.type == 'email')
                    scope.pat = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/

             

            },
            scope: {
                ngModel: '=',
                field: '=field',
                ngChange: '='
            },
            templateUrl: function ($state, $e) {
              
                var  tpl = 'directives/input.form/input.directive.t.h.html';
                if ($e.dir == 'v')
                    tpl = 'directives/input.form/input.directive.t.v.html';;

                return tpl;
            },
        };
    });


angular.module('app')
    .directive('selectform', function () {
        return {
            restrict: 'E',
            replace: true,
            link: function (scope, element, attrs) {

                scope.display = attrs.display
                scope.collabel = attrs.collabel ? attrs.collabel : 'col-md-2'
                scope.collinput = attrs.collinput ? attrs.collinput : 'col-md-10'
                scope.name = attrs.name
                scope.ngRequired = attrs.ngRequired == 'true'
                scope.disabled = attrs.disabled == 'true'
                scope.pat = '';
          
            },
            scope: {
                ngModel: '=',
                field: '=field',
                option: '=option',
                ngChange: '='
            },
            templateUrl: function ($state, $e) {

                var tpl = 'directives/input.form/input.directive.s.h.html';
                if ($e.dir == 'v')
                    tpl = 'directives/input.form/input.directive.s.v.html';;

                return tpl;
            },
        };
    });


angular.module('app')
    .directive('dateform', function () {
        return {
            restrict: 'E',
            replace: true,
            link: function (scope, element, attrs) {
                scope.opened = false;
                scope.dateOptions = {
                };
                scope.eventSources = []
                scope.change = function (value) {

                 
                }

                scope.dateSelection = function (value) {

                
                }
                scope.formats = ['MM/dd/yyyy', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
                scope.format = scope.formats[0];
                scope.altInputFormats = ['M!/d!/yyyy'];
                scope.display = attrs.display
                scope.collabel = attrs.collabel ? attrs.collabel : 'col-md-2'
                scope.collinput = attrs.collinput ? attrs.collinput : 'col-md-10'
                scope.name = attrs.name
                scope.ngRequired = attrs.ngRequired == 'true'
                scope.type = attrs.type ? attrs.type : 'text'
                scope.pat = '';
                scope.disabled = attrs.disabled == 'true'
                if (attrs.type == 'email')
                    scope.pat = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/

              
            },
            scope: {
                ngModel: '=ngModel',
                field: '=field',
                ngChange: '='
            },
            templateUrl: function ($state, $e) {
                var tpl = 'directives/input.form/input.directive.d.h.html';
                if ($e.dir == 'v')
                    tpl = 'directives/input.form/input.directive.d.v.html';;

                return tpl;
            },
        };
    });
