'use strict';

angular.module('tbl.serveradmin.directives').
    directive('header', function () {
        return {
            templateUrl: 'Templates/Header',
            restrict: 'E',
            replace: true,
        }
    });