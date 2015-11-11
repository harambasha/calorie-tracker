'use strict';

app.factory('mealsService', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(serviceBase + 'api/meals/:id', null, {
        'update': {
            method: 'PUT'
        },
        'save': {
            method: 'POST'
        }
    });

}).factory('mealsFilterService', function ($resource, ngAuthSettings) {
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        return $resource(serviceBase + 'api/meals/filter?startDate=:startDate&endDate=:endDate&fromTime=:fromTime&toTime=:toTime', null, {});
    });