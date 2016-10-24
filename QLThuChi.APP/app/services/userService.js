'use strict';
app.factory('userService', ['$http', '$q', '$localStorage', 'ngAuthSettings',
    function ($http, $q, $localStorage, ngAuthSettings) {
        var factory = {};
        factory.GetAllUsers = function () {
            var deferred = $q.defer();
            $http.get(ngAuthSettings.apiServiceBaseUri + 'api/accounts/users')
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.GetUser = function (username) {
            var deferred = $q.defer();
            $http.get(ngAuthSettings.apiServiceBaseUri + 'api/accounts/user/' + username)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.CreateUser = function (userData) {
            var deferred = $q.defer();
            $http.post(ngAuthSettings.apiServiceBaseUri + 'api/accounts/create', userData)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.ChangePassword = function (changePassword) {
            var deferred = $q.defer();
            $http.post(ngAuthSettings.apiServiceBaseUri + 'api/accounts/ChangePassword', changePassword)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.DeleteUser = function (userId) {
            var deferred = $q.defer();
            $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/accounts/user/' + userId)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        return factory;
    }
]
)