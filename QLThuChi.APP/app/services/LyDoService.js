'use strict';
app.factory('lydoService', ['$http', '$q', '$localStorage', 'ngAuthSettings',
    function ($http, $q, $localStorage, ngAuthSettings) {
        var factory = {};
        factory.GetAll = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Lydoes').then(function (response) {
                return response;
            });
        };

        factory.Get = function (id) {
            var deferred = $q.defer();
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Lydoes/' + id).then(function (response) {
                return response;
            });
        };

        factory.Insert = function (lydoData) {
            var deferred = $q.defer();
            $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Lydoes', lydoData)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.Update = function (id, lydoData) {
            var deferred = $q.defer();
            $http.put(ngAuthSettings.apiServiceBaseUri + 'api/Lydoes/' + id, lydoData)
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
            $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/Lydoes/' + id)
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