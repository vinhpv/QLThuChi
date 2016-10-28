﻿'use strict';

app.controller('thuchiCtrl', ['$scope', '$injector', function ($scope, $injector) {
    var KhoiTao = function () {
        $scope.Thuchi = {};
        $scope.message = "";
        $scope.isInsert = true;
    }

    var sv = $injector.get('thuchiService');
    var svNguoiThuChi = $injector.get('nguoithuchiService');
    var svlydo = $injector.get('lydoService');
    var focus = $injector.get('focus');

    $scope.LyDoes = function () {
        svlydo.GetAll().then(
            function (data) {
                return data.data;
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
                return {};
            }
            )
    };
    $scope.NguoiThuChis = function () {
        svNguoiThuChi.GetAll().then(
            function (data) {
                return data.data;
            },
            function (err, stt) {
                $scope.message = "Có lỗi xảy ra!";
                return {};
            }
            )
    };
    $scope.getAll = function () {
        sv.GetAll().then(
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
        focus('txtTen');
    }

    $scope.Commit = function () {
        if ($scope.isInsert) {
            var data = {
                "NguoiThuchiId": 0,
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