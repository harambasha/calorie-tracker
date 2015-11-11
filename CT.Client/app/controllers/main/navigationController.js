'use strict';
app.controller('navigationController', ['$scope', '$location', 'authService', 'navigationService',
    function ($scope, $location, authService, navigationService) {
        authService.fillAuthData();
        $scope.authentication = authService.authentication;
        $scope.navigationStyle = function(pagePath) {
            if (pagePath.length > navigationService.currentPath.length) {
                return '';
            }
            for (var i = 0; i < pagePath.length; i++) {
                if (navigationService.currentPath[i] !== pagePath[i]) {
                    return '';
                }
            }
            return 'active';
        };

        $scope.logOut = function() {
            authService.logOut();
            $location.path('/home');
        }

        $scope.showUserEditing = function() {
            if ($scope.authentication.isAuth) {
                if ($scope.authentication.isAdmin || $scope.authentication.isUserManger) {
                    return true;
                }
            }
            return false;
        }
    }
]);
