'use strict';

app.controller('nguoithuchiCtrl', ['$scope', '$injector', '$location', function ($scope, $injector, $location) {
    var KhoiTao = function () {
        $scope.NguoiThuchiId = 0;
        $scope.HoTen = "";
        $scope.GhiChu = "";
        $scope.message = "";
        $scope.isInsert = true;
        $scope.loading = false;
    }
    var sv = $injector.get('nguoithuchiService');
    var focus = $injector.get('focus');
    $scope.getAll = function () {
        $scope.loading = true;
        sv.GetAll().then(
            function (data) {
                $scope.NguoiThuChis = data.data;
                KhoiTao();
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
            }
            );
    };
    $scope.Insert = function () {
        $scope.NguoiThuchiId = 0;
        $scope.HoTen = "";
        $scope.GhiChu = "";
        $scope.isInsert = true;
        focus('txtTen');
    }

    $scope.Commit = function () {
        if ($scope.isInsert) {
            var data = { HoTen: $scope.HoTen, GhiChu: $scope.GhiChu };
            $scope.loading = true;
            sv.Insert(data).then(
                    function (response) {
                        $scope.getAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        } else {
            var data = { NguoiThuchiId: $scope.NguoiThuchiId, HoTen: $scope.HoTen, GhiChu: $scope.GhiChu };
            sv.Update($scope.NguoiThuchiId, data).then(
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
        var s = Enumerable.from($scope.NguoiThuChis)
            .where(function (x) { return x.NguoiThuchiId == id })
            .singleOrDefault();

        $scope.NguoiThuchiId = id;
        $scope.HoTen = s.HoTen;
        $scope.GhiChu = s.GhiChu;
        $scope.isInsert = false;
        focus('txtTen');
    }

    $scope.Delete = function () {
        if ($scope.NguoiThuchiId > 0)
            sv.Delete($scope.NguoiThuchiId).then(
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