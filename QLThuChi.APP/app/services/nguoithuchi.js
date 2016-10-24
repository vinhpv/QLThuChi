'use strict';
app.factory('nguoithuchiService', ['$http', '$q', '$localStorage', 'ngAuthSettings',
    function ($http, $q, $localStorage, ngAuthSettings) {
        var factory = {};
        factory.GetAll = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Nguoithuchi').then(function (response) {
                return response;
            });
        };

        factory.Get = function (id) {
            var deferred = $q.defer();
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Nguoithuchi/' + id).then(function (response) {
                return response;
            });
        };

        factory.Insert = function (nguoithuchiData) {
            var deferred = $q.defer();
            $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Nguoithuchi', nguoithuchiData)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        factory.Update = function (id, nguoithuchiData) {
            var deferred = $q.defer();
            $http.put(ngAuthSettings.apiServiceBaseUri + 'api/Nguoithuchi/' + id, nguoithuchiData)
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
            $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/Nguoithuchi/' + id)
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