'use strict';
app.controller('userSettingListController', ['$scope', 'userSettingsService', 'userSettings', 'navigationService',
    function ($scope, userSettingsService, userSettings, navigationService) {

        navigationService.currentPath = ['userSettings'];
        $scope.userSettings = userSettings;
        $scope.newUserSetting = {};
        $scope.newUserSetting.id = null;
        $scope.addOrEdit = false;
        $scope.errorMessages = [];

        $scope.edit = function (userSetting) {
            $scope.addOrEdit = true;
            $scope.newUserSetting = userSetting;
        };

        $scope.cancelEdit = function () {
            $scope.addOrEdit = false;
        };

        $scope.new = function () {
            $scope.newUserSetting = {};
            $scope.newUserSetting.id = null;
            $scope.addOrEdit = true;
        };

        $scope.save = function () {
            var savePromise = null;

            if ($scope.newUserSetting.id !== null) {
                savePromise = userSettingsService.update({
                    id: $scope.newUserSetting.id
                }, $scope.newUserSetting).$promise;
            } else {
                savePromise = userSettingsService.save($scope.newUserSetting).$promise;
            }

            savePromise.then(function (response) {
                $scope.userSettings = userSettingsService.query();
                $scope.addOrEdit = false;
            },
             function (error) {
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
    }]);