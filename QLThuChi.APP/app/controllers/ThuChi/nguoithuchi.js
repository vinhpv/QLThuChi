'use strict';

app.controller('nguoithuchiCtrl', ['$scope', '$injector', '$location', function ($scope, $injector, $location) {
    $scope.NguoiThuchiId = 0;
    $scope.HoTen = "";
    $scope.GhiChu = "";
    $scope.message = "";
    $scope.isInsert = true;
    var sv = $injector.get('nguoithuchiService');
    var focus = $injector.get('focus');
    $scope.getAll = function () {
        sv.GetAll().then(
            function (data) {
                $scope.NguoiThuChis = data.data;
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
            }
            )
    };
    $scope.Insert = function () {
        $scope.NguoiThuchiId = 0;
        $scope.HoTen = "";
        $scope.GhiChu = "";
        $scope.isInsert = true;
        focus('txtTen');
    }

    $scope.Commit = function () {
        var data = { HoTen: $scope.HoTen, GhiChu: $scope.GhiChu };
        if ($scope.isInsert) {
            sv.Insert(data).then(
                    function (response) {
                        $scope.getAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        } else {
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

    $scope.Delete = function (id) {
        sv.Delete(id).then(
                 function (response) {
                     $scope.getAll();
                 },
                 function (err) {
                     $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                 }
             );
    }

    $scope.getAll();

}]);