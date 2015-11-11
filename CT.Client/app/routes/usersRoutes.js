'use strict';
app.config(function ($stateProvider) {
    $stateProvider.state('users', {
        url: '/users',
        abstract: true,
        template: '<div ui-view></div>'
    });

    $stateProvider.state('users.list', {
        url: '',
        controller: 'usersListController',
        templateUrl: 'app/views/users/user-list.html',
        resolve: {
            users: ['usersService', function (usersService) {
                return usersService.query();
            }],
            roles: ['rolesService', function (rolesService) {
                return rolesService.query();
            }]
        }
    });
});
