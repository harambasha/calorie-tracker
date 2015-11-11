'use strict';
app.controller('usersListController', ['$scope', 'navigationService', 'users', 'authService', 'usersService', 'registerService', 'userRolesService', 'roles',
function ($scope, navigationService, users, authService, usersService, registerService, userRolesService, roles) {
        navigationService.currentPath = ['users'];
        $scope.authentication = authService.authentication;
        $scope.users = users;
        $scope.roles = roles;
        $scope.isError = false;
        $scope.isSuccess = false;
        $scope.addOrEdit = false;
        $scope.addingMessage = null;
        $scope.errorMessages = [];
        $scope.newUser = {};
        $scope.newUser.id = null;
        $scope.newRole = null;
        $scope.newUser.roles = [];


        $scope.new = function () {
            $scope.newUser = {};
            $scope.newUser.id = null;
            $scope.newUser.roles = [];
            $scope.isError = false;
            $scope.isSuccess = false;
            $scope.addOrEdit = true;
        };

        $scope.cancelEditOrAdd = function () {
            $scope.isError = false;
            $scope.isSuccess = false;
            $scope.addOrEdit = false;
            $scope.errorMessages = [];
        };


        $scope.save = function () {
            $scope.newUser.roles = null;
            var savePromise = null;

            var firstNameAndLastnameArray = $scope.newUser.fullName.split(" ", 2);
            var firstName = firstNameAndLastnameArray[0];
            var lastName = firstNameAndLastnameArray[1];

            $scope.newUser.firstName = firstName;
            $scope.newUser.lastName = lastName;

            if ($scope.newUser.id !== null) {
                savePromise = usersService.update({
                    id: $scope.newUser.id
                }, $scope.newUser).$promise;
            } else {
                savePromise = registerService.save($scope.newUser).$promise;
            }

            savePromise.then(function (response) {
                $scope.users = usersService.query();
                $scope.addOrEdit = false;
                $scope.isSuccess = true;
                $scope.isError = false;
            },
             function (error) {
                 $scope.isError = false;
                 $scope.errorMessages = [];
                 for (var key in error.data.modelState) {
                     if (error.data.modelState.hasOwnProperty(key)) {
                         for (var i = 0; i < error.data.modelState[key].length; i++) {
                             $scope.errorMessages.push(error.data.modelState[key][i]);
                         }
                     }
                 }
             });
        }

        $scope.edit = function (user) {
            $scope.isError = false;
            $scope.isSuccess = false;

            $scope.addOrEdit = true;
            $scope.newUser = user;
        };

        $scope.checkIfUserIsAdmin = function (user) {
            return _.contains(user.roles, 'Admin');
        };

        $scope.delete = function (user) {
            usersService.delete({ id: user.id }, function () {
                $scope.users = usersService.query();
            }, function () {
                $scope.message = 'Unable to delete user';
            });
        };
        $scope.addRole = function () {
            //Clicked tags are passed as objects, new tags are passed as strings.
            if ($scope.newRole !== null) {
                $scope.newUser.roles.push($scope.newRole.name);
                userRolesService.update({ id: $scope.newUser.id }, $scope.newUser.roles).$promise;
            }
            $scope.newRole = null;
        };
        $scope.removeRole = function (role) {
            $scope.newUser.roles.splice(_.indexOf($scope.newUser.roles, role), 1);
            userRolesService.update({ id: $scope.newUser.id }, $scope.newUser.roles).$promise;
        };
    }]);