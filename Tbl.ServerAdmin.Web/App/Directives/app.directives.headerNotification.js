'use strict';

angular.module('tbl.serveradmin.directives').
    directive('headerNotification', function () {
        return {
            templateUrl: 'Templates/HeaderNotification',
            restrict: 'E',
            replace: true,
        }
    });