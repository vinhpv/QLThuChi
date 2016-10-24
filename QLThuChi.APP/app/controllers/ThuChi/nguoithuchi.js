'use strict';

app.controller('nguoithuchiCtrl', ['$scope', '$injector', '$location', function ($scope, $injector, $location) {
    var sv = $injector.get('nguoithuchiService');
    var focus = $injector.get('focus');
    $scope.NguoiThuChis = sv.GetAll();
    $scope.id = 0;
    $scope.HoTen = "";
    $scope.GhiChu = "";
    $scope.message = "";
    $scope.isInsert = true;

    $scope.Insert = function () {
        $scope.id = 0;
        $scope.HoTen = "";
        $scope.GhiChu = "";
        $scope.isInsert = true;
        focus('txtHoTen');
    }

    $scope.Commit = function () {
        var data = { HoTen: $scope.HoTen, GhiChu: $scope.GhiChu };
        if ($scope.isInsert) {
            sv.Insert(data).then(
                    function (response) {
                        $scope.NguoiThuChis = sv.GetAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        } else {
            sv.Update($scope.id, data).then(
                    function (response) {
                        $scope.NguoiThuChis = sv.GetAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        }
        focus('txtHoTen');
    }

    $scope.Select = function (id) {
        var s = Enumerable.From($scope.NguoiThuChis)
            .Where(function (x) { return x.NguoiThuchiId == id })
            .Select(function (x) { return { HoTen: x.HoTen, GhiChu: x.GhiChu } });
        $scope.id = id;
        $scope.HoTen = s.HoTen;
        $scope.GhiChu = s.GhiChu;
        $scope.isInsert = false;
        focus('txtHoTen');
    }

}]);