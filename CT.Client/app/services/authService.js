'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', 'localUserInfoService', function ($http, $q, localStorageService, ngAuthSettings, localUserInfoService) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };

    var saveRegistration = function (registration) {
        logOut();
        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        var deferred = $q.defer();

        $http.post(serviceBase + 'oauth/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, isAdmin: false, isUserManger: false });

            authentication.isAuth = true;
            authentication.userName = loginData.userName;
            fillAuthData();
            deferred.resolve(response);

        }).error(function (err, status) {
            logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var logOut = function () {
        localStorageService.remove('authorizationData');

        authentication.isAuth = false;
        authentication.userName = "";
        authentication.isUserManger = false;
        authentication.isAdmin = false;

    };

    var fillAuthData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            authentication.isAuth = true;
            authentication.userName = authData.userName;
            var userInfo = localUserInfoService.get();
            if (userInfo.$promise != null) {
                userInfo.$promise.then(function () {
                    authentication.isAdmin = _.contains(userInfo.roles, 'Admin');
                    authentication.isUserManger = _.contains(userInfo.roles, 'UserManager');
                });
            }
        }
    };

    authServiceFactory.saveRegistration = saveRegistration;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
}]);