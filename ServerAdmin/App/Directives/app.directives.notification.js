'use strict';

angular.module('tbl.serveradmin.directives')
	.directive('notifications', function () {
	    return {
	        templateUrl: 'Templates/Notification',
	        restrict: 'E',
	        replace: true,
	    }
	});