'use strict';

app.factory('rolesService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/roles', null, {});
});