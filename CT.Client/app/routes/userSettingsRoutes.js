'use strict';
app.config(function ($stateProvider) {
    $stateProvider.state('userSettings', {
        url: '/userSettings',
        abstract: true,
        template: '<div ui-view></div>'
    });

    $stateProvider.state('userSettings.list', {
        url: '',
        controller: 'userSettingListController',
        templateUrl: 'app/views/user-settings/user-settings.html',
        resolve: {
            userSettings: ['userSettingsService', function (userSettingsService) {
                return userSettingsService.query();
            }]
        }
    });
});
