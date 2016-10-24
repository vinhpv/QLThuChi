'use strict';
app.factory('authService', ['$http', '$q', '$localStorage', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {
        var sefl = this;
        var _authentication = {
            isAuth: false,
            userName: ""
        };
        var serviceBase = ngAuthSettings.serviceBase;
        return {
            sefl.loGin = function(loginData)
            {
                var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
                var deferred = $q.defer();

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                    $localStorage.authorizationData =
                    { token: response.access_token, userName: loginData.userName };

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;
                    
                    deferred.resolve(response);

                }).error(function (err, status) {
                    logOut();
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            sefl.logOut = function()
            {
                $localStorage.authorizationData = null;
                _authentication.isAuth = false;
                _authentication.userName = "";
            },
            sefl.authentication = _authentication
        }
    }
]
)