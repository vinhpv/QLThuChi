'use strict';

app.controller('thuchiCtrl', ['$scope', '$injector', '$modal', function ($scope, $injector, $modal) {
    var KhoiTao = function () {
        $scope.Thuchi = {};
        $scope.message = "";
        $scope.isInsert = true;
    }

    var sv = $injector.get('thuchiService');
    var svNguoiThuChi = $injector.get('nguoithuchiService');
    var svlydo = $injector.get('lydoService');
    var focus = $injector.get('focus');
    var svModal = $injector.get('modalService');
    $scope.thang = new Date();


    $scope.getAll = function () {
        sv.GetListOfMonth($scope.thang.yyyymm()).then(
            function (data) {
                $scope.Thuchis = data.data;
                KhoiTao();

            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
            }
            )
    };
    $scope.Insert = function () {
        KhoiTao();

        var modalInstance = $modal.open({
            templateUrl: 'CapNhatThuChi.html',
            controller: 'thuchiEditCtrl',
            size: 'lg',
            resolve: {
                data: function () {
                    return $scope.Thuchi;

                },
                isInsert: function () {
                    return $scope.isInsert;
                }
            }
        });

        focus('txtTen');
    }

    $scope.Commit = function () {
        if ($scope.isInsert) {
            var data = {
                "NguoiThuchiId": 0,
                "NgayThuchi": $scope.Thuchi.NgayThuchi,
                "KieuThu": $scope.Thuchi.KieuThu,
                "LydoId": $scope.Thuchi.LydoId,
                "Tien": $scope.Thuchi.Tien,
                "GhiChu": $scope.Thuchi.GhiChu
            };
            sv.Insert(data).then(
                    function (response) {
                        $scope.getAll();
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        } else {
            var data = {
                "NguoiThuchiId": $scope.Thuchi.NguoiThuchiId,
                "NgayThuchi": $scope.Thuchi.NgayThuchi,
                "KieuThu": $scope.Thuchi.KieuThu,
                "LydoId": $scope.Thuchi.LydoId,
                "Tien": $scope.Thuchi.Tien,
                "GhiChu": $scope.Thuchi.GhiChu
            };
            sv.Update($scope.Thuchi.ThuchiId, data).then(
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
        $scope.Thuchi = Enumerable.from($scope.Thuchis)
            .where(function (x) { return x.ThuchiId == id })
            .singleOrDefault();

        $scope.isInsert = false;
        focus('txtTen');
    }

    $scope.Delete = function () {
        if ($scope.Thuchi.ThuchiId > 0)
            sv.Delete($scope.Thuchi.ThuchiId).then(
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

app.controller('thuchiEditCtrl', ['$scope', '$injector', '$modalInstance', 'data', 'isInsert',
    function ($scope, $injector, $modalInstance, data, isInsert) {
    $scope.Thuchi = data;
    $scope.isInsert = isInsert;
    var sv = $injector.get('thuchiService');
    var svNguoiThuChi = $injector.get('nguoithuchiService');
    var svlydo = $injector.get('lydoService');
    $scope.message = '';

    function khoiTaoDuLieu() {
        svlydo.GetAll().then(
            function (data) {
                $scope.LyDoes = data.data;
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
                $scope.LyDoes = [];
            }
            );
        svNguoiThuChi.GetAll().then(
            function (data) {
                $scope.NguoiThuChis = data.data;
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
                $scope.NguoiThuChis = [];
            }
            );
    }

    $scope.ok = function () {
        if ($scope.isInsert) {
            var data = {
                "NguoiThuchiId": $scope.Thuchi.NguoiThuchiId,
                "NgayThuchi": $scope.Thuchi.NgayThuchi,
                "KieuThu": $scope.Thuchi.KieuThu,
                "LydoId": $scope.Thuchi.LydoId,
                "Tien": $scope.Thuchi.Tien,
                "GhiChu": $scope.Thuchi.GhiChu
            };
            sv.Insert(data).then(
                    function (response) {
                        //$scope.getAll();
                        $modalInstance.close($scope.Thuchi);
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        }
        else
        {
            var data = {
                "ThuchiId": $scope.Thuchi.ThuchiId,
                "NguoiThuchiId": $scope.Thuchi.NguoiThuchiId,
                "NgayThuchi": $scope.Thuchi.NgayThuchi,
                "KieuThu": $scope.Thuchi.KieuThu,
                "LydoId": $scope.Thuchi.LydoId,
                "Tien": $scope.Thuchi.Tien,
                "GhiChu": $scope.Thuchi.GhiChu
            };
            sv.Update($scope.Thuchi.ThuchiId, data).then(
                    function (response) {
                        $modalInstance.close($scope.Thuchi);
                    },
                    function (err) {
                        $scope.message = "Có lỗi xảy ra trong quá trình thực hiện! ";
                    }
                );
        }

    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    khoiTaoDuLieu();

}]);