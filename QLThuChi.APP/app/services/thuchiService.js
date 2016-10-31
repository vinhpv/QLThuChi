'use strict';
app.factory('thuchiService', ['$http', '$q', '$localStorage', 'ngAuthSettings',
    function ($http, $q, $localStorage, ngAuthSettings) {
        var factory = {};
        factory.GetAll = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/thuchi').then(function (response) {
                return response;
            });
        };

        factory.Get = function (id) {
            var deferred = $q.defer();
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/thuchi/' + id).then(function (response) {
                return response;
            });
        };

        factory.GetListOfMonth = function (thang) {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/thuchi/get/' + thang).then(function (response) {
                return response;
            });
        };

        factory.Insert = function (thuchiData) {
            var deferred = $q.defer();
            $http.post(ngAuthSettings.apiServiceBaseUri + 'api/thuchi', thuchiData)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.Update = function (id, thuchiData) {
            var deferred = $q.defer();
            $http.put(ngAuthSettings.apiServiceBaseUri + 'api/thuchi/' + id, thuchiData)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.Delete = function (id) {
            var deferred = $q.defer();
            $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/thuchi/' + id)
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