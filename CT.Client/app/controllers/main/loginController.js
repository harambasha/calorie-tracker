'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'navigationService', function ($scope, $location, authService, navigationService) {
    navigationService.currentPath = ['login'];
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {

        authService.login($scope.loginData).then(function (response) {
            $location.path('/');
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };
}]);
