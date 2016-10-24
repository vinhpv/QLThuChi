'use strict';
app.factory('authService', ['$http', '$q', '$localStorage', 'ngAuthSettings',
    function ($http, $q, $localStorage, ngAuthSettings) {
        var factory = {};
        var _authentication = {
            isAuth: false,
            userName: ""
        };

        factory.
            loGin = function (loginData) {
                var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.passWord;
                var deferred = $q.defer();
                $http.post(ngAuthSettings.apiServiceBaseUri + 'oauth/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                    $localStorage.authorizationData =
                    { token: response.access_token, userName: loginData.userName };

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;

                    deferred.resolve(response);

                }).error(function (err, status) {
                    deferred.reject(err);
                });

                return deferred.promise;
            };
        factory.
            logOut = function () {
                $localStorage.authorizationData = null;
                _authentication.isAuth = false;
                _authentication.userName = "";
            };
        factory.
        authentication = _authentication
        return factory;
    }
]
)