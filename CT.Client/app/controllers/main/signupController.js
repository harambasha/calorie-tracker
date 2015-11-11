'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', 'navigationService', function ($scope, $location, $timeout, authService, navigationService) {
    navigationService.currentPath = ['signup'];
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.errorMessages = [];

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function () {
            $scope.errorMessages = [];
            $scope.savedSuccessfully = true;
            $scope.errorMessages.push("User has been registered successfully, you will be redicted to login page in 2 seconds.");
            $scope.startTimer();

        },
         function (response) {
             $scope.errorMessages = [];
             for (var key in response.data.modelState) {
                 if (response.data.modelState.hasOwnProperty(key)) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         $scope.errorMessages.push(response.data.modelState[key][i]);
                     }
                 }
             }
         });
    };

    $scope.startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 1500);
    }

}]);