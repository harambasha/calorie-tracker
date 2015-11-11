'use strict';
app.controller('homeController', ['$scope', 'authService', function ($scope, authService) {
    $scope.authentication = authService.authentication;
    $scope.showUserEditing = function () {
        if ($scope.authentication.isAuth) {
            if ($scope.authentication.isAdmin || $scope.authentication.isUserManger) {
                return true;
            }
        }
        return false;
    }
}]);