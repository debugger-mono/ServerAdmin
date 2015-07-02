'use strict';

var app = angular.module('tbl.serveradmin.app');

app.controller('LoginController', ['$scope', '$state', function ($scope, $state) {

    console.log('Login Controller Initialized');

    $scope.submitLogin = function () {
        console.log('Submit Login');
        $state.go('dashboard.home');
    };
}]);