'use strict';

app.controller('lydoCtrl', ['$scope', '$injector', '$location', function ($scope, $injector, $location) {
    var KhoiTao = function () {
        $scope.LydoId = 0;
        $scope.TenLydo = "";
        $scope.Ghichu = "";
        $scope.message = "";
        $scope.isInsert = true;
    }
    var sv = $injector.get('lydoService');
    var focus = $injector.get('focus');
    $scope.getAll = function () {
        sv.GetAll().then(
            function (data) {
                $scope.LyDoes = data.data;
                KhoiTao();
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
            }
            )
    };
    $scope.Insert = function () {
        $scope.LydoId = 0;
        $scope.TenLydo = "";
        $scope.Ghichu = "";
        $scope.isInsert = true;
        focus('txtTen');
    }

    $scope.Commit = function () {
        if ($scope.isInsert) {
            var data = { TenLydo: $scope.TenLydo, Ghichu: $scope.Ghichu };
            sv.Insert(data).then(
                    function (response) {
                        $scope.getAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        } else {
            var data = { LydoId: $scope.LydoId, TenLydo: $scope.TenLydo, Ghichu: $scope.Ghichu };
            sv.Update($scope.LydoId, data).then(
                    function (response) {
                        $scope.getAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        }
        focus('txtTen');
    }

    $scope.Select = function (id) {
        var s = Enumerable.from($scope.LyDoes)
            .where(function (x) { return x.LydoId == id })
            .singleOrDefault();

        $scope.LydoId = id;
        $scope.TenLydo = s.TenLydo;
        $scope.Ghichu = s.Ghichu;
        $scope.isInsert = false;
        focus('txtTen');
    }

    $scope.Delete = function () {
        if ($scope.LydoId > 0)
            sv.Delete($scope.LydoId).then(
                     function (response) {
                         $scope.getAll();
                     },
                     function (err) {
                         $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                     }
                 );
    }
    KhoiTao();
    $scope.getAll();


}]);