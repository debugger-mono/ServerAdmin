'use strict';

angular.module('tbl.serveradmin.directives')
	.directive('chat', function () {
	    return {
	        templateUrl: 'Templates/Chat',
	        restrict: 'E',
	        replace: true,
	    }
	});


