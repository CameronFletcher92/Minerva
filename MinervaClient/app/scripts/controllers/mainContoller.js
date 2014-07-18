'use strict';

/**
 * @ngdoc function
 * @name minervaApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the minervaApp
 */
angular.module('minervaApp').controller('MainCtrl', function ($scope, DowntimeEventSvc, EquipmentSvc) {
    $scope.downtimeEvents = DowntimeEventSvc.getDataSource();
    $scope.flatEquipments = EquipmentSvc.getDataSource();
    $scope.hierarchicalEquipments = EquipmentSvc.getHierarchicalDataSource();

    // downtime event grid
    $scope.gridOptions = {
        // grid settings
        height: 600,
        filterable: true,
        sortable: true,
        pageable: true,
        //groupable: true,

        // column definitions for other titles and properties
        columns: [
            {
                field: 'Equipment.Code',
                title: 'Equipment Code'
            },
            {
                field: 'Start',
                title: 'Start',
                format: '{0: yyyy-MM-dd HH:mm}'
            },
            {
                field: 'End',
                title: 'End',
                format: '{0: yyyy-MM-dd HH:mm}'
            },

            // the controls
            { command: ["edit", "destroy"], title: "&nbsp;", width: "200px" }
        ],

        // edit style
        editable: "popup"
    };


    // equipment tree
    $scope.treeOptions = {
        dataTextField: 'Code'
    };

    $scope.equipGridOptions = {
        height: 600,
        filterable: true,
        sortable: true,
        pageable: true,

        columns: [
            {
                field: 'Code',
                title: 'Equipment Code'
            },
            {
                field: 'Description',
                title: 'Description'
            },

            // the controls
            { command: ["edit", "destroy"], title: "&nbsp;", width: "200px" }
        ],

        editable: "popup"
    };


});
