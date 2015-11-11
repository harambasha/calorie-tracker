'use strict';

angular.module('calorieTrackingApp').directive('formGroupValidate', [function () {
    return {
        restrict: 'A',
        link: function (scope, element) {
            var input = element.find('[ng-model]');
            if (input) {
                scope.$watch(function () {
                    return input.hasClass('ng-invalid');
                }, function (isInvalid) {
                    element.toggleClass('has-error', isInvalid);
                });
            }
        }
    };
}]);