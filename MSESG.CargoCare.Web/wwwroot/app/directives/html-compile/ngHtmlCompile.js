angular.module('app')
    .directive('dynamic', function($compile) {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                scope.$watch(attrs.dynamic, function(html) {
                    element[0].innerHTML = html;
                    $compile(element.contents())(scope);
                });
            }
        };
    });
