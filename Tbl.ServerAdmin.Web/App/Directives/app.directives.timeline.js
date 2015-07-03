'use strict';

angular.module('tbl.serveradmin.directives')
	.directive('timeline', function () {
	    return {
	        templateUrl: 'Templates/Timeline',
	        restrict: 'E',
	        replace: true,
	    }
	});