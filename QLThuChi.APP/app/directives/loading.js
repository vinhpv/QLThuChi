angular.module('app')
  .directive('loadingContainer', function () {
      return {
          restrict: 'AC',
          link: function (scope, el, attrs) {
              el.removeClass('app-loading');
              scope.$on('$stateChangeStart', function (event) {
                  el.addClass('app-loading');
              });
              scope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState) {
                  event.targetScope.$watch('$viewContentLoaded', function() {
                      el.removeClass('app-loading ');
                  });
              });
          }
      };
  })
.directive('loading', ['$http', '$rootScope', function ($http, $rootScope) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    $rootScope.loadingAction = true;
                    elm.show();
                } else {
                    $rootScope.loadingAction = false;
                    elm.hide();
                }
            });
        }
    };

}]);