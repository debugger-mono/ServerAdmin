'use strict';

var app = angular.module('tbl.serveradmin.app', ['ngRoute', 'ngResource', 'ngSanitize', 'ui.router', 'angular-loading-bar', 'tbl.serveradmin.directives', 'tbl.serveradmin.services']);

app.config(['$urlRouterProvider', '$stateProvider', '$locationProvider', function ($urlRouterProvider, $stateProvider, $locationProvider) {

    $urlRouterProvider.when('', '/');

    $stateProvider
        .state('/', { url: '/', templateUrl: '/Templates/Login', controller: 'LoginController' })
        .state('dashboard', { url: '/main', templateUrl: '/Templates/Main', controller: 'MainController' })
        .state('dashboard.home', { url: '/dashboard', templateUrl: '/Templates/Dashboard', controller: 'DashboardController' })
        .state('dashboard.users', { url: '/users', templateUrl: '/Templates/Users', controller: 'UsersController' })
        .state('otherwise', { url: '*path', templateUrl: '/Error/NotFound'});
}]);