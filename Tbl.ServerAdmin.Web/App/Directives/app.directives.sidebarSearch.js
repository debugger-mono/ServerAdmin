'use strict';

angular.module('tbl.serveradmin.directives')
  .directive('sidebarSearch', function () {
      return {
          templateUrl: 'Templates/SidebarSearch',
          restrict: 'E',
          replace: true,
          scope: {
          },
          controller: function ($scope) {
              $scope.selectedMenu = 'home';
          }
      }
  });
