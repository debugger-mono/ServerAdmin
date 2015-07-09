'use strict';

angular.module('tbl.serveradmin.directives')
    .directive('stats', function () {
        return {
            templateUrl: 'Templates/Stat',
            restrict: 'E',
            replace: true,
            scope: {
                'model': '=',
                'comments': '@',
                'number': '@',
                'name': '@',
                'colour': '@',
                'details': '@',
                'type': '@',
                'goto': '@'
            }

        }
    });