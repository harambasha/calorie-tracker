'use strict';
app.factory('navigationService', function () {
    return {
        //Treated as an array to allow nesting. Example: Content -> Widget -> Awesome can be represented as ['Content', 'Widget', 'Awesome']
        currentPath: []
    };
});