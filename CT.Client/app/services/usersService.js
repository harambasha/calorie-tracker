'use strict';

app.factory('usersService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/account/users/:id', null, {
        'update': {
            method: 'PUT'
        },
        'save': {
            method: 'POST'
        }
    });

}).factory('localUserInfoService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/account/local-user-info', null, {});

}).factory('registerService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/account/register', null, {});

}).factory('userRolesService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/account/users/:id/roles', null, {
        'update': {
            method: 'PUT'
        }
    });
});