'use strict';

/**
 * @ngdoc function
 * @name minervaApp.controller:AboutCtrl
 * @description
 * # AboutCtrl
 * Controller of the minervaApp
 */
angular.module('minervaApp')
    .controller('AboutCtrl', function ($scope) {
        $scope.awesomeThings = [
            'HTML5 Boilerplate',
            'AngularJS',
            'Karma'
        ];
    });
