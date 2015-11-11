'use strict';
app.config(function ($stateProvider) {
    $stateProvider.state('meals', {
        url: '/meals',
        abstract: true,
        template: '<div ui-view></div>'
    });

    $stateProvider.state('meals.list', {
        url: '',
        controller: 'mealsListController',
        templateUrl: 'app/views/meals/meal-list.html',
        resolve: {
            meals: ['mealsService', function (mealsService) {
                return mealsService.query();
            }],
            userSettings: ['userSettingsService', function (userSettingsService) {
                return userSettingsService.query();
            }]
        }
    });
});
