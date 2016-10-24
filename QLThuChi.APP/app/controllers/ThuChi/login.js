app.controller('loginCtrl', ['$scope','$injector','$location', function ($scope, $injector, $location) {
    $scope.loginData = {
        userName: "",
        passWord: ""
    };

    $scope.login = function () {
        if ($scope.loginData.userName != "") {
            var sv = $injector.get('authService');
            sv.loGin($scope.loginData).then(function (response) {
                $location.path('/ThuChi/nguoithuchi');
            },
         function (err) {
             sv.logOut();
             $scope.message = err.error_description;
         });
        }
        else
        {
            $scope.message = 'User name is null';
        }
    }

}]);