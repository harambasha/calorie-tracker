'use strict';

app.factory('userSettingsService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/user-settings/:id', null, {
        'update': {
            method: 'PUT'
        }
    });
});