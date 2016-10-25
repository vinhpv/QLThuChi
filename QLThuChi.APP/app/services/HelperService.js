app
.factory('focus', function ($timeout, $window) {
    return function (id) {
        // timeout makes sure that it is invoked after any other event has been triggered.
        // e.g. click events that need to run before the focus or
        // inputs elements that are in a disabled state but are enabled when those events
        // are triggered.
        $timeout(function () {
            var element = $window.document.getElementById(id);
            if (element)
                element.focus();
        });
    };
})

.service('ConfirmService', function ($modal) {
    var service = {};
    service.open = function (text, onOk) {
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'ModalConfirmCtrl',
            resolve: {
                text: function () {
                    return text;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            onOk();
        }, function () {
        });
    };

    return service;
})