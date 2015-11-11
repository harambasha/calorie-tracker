
var app = angular.module('calorieTrackingApp', ['ngResource', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ui.router', 'ui.timepicker']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider.state('home', {
        url: '/',
        templateUrl: 'app/views/home.html',
        controller: 'homeController'
    });

    $stateProvider.state('login', {
        url: '/login',
        templateUrl: 'app/views/login.html',
        controller: 'loginController'
    });

    $stateProvider.state('signup', {
        url: '/signup',
        templateUrl: 'app/views/signup.html',
        controller: 'signupController'
    });
});

var serviceBase = 'http://localhost:9000/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


