'use strict';

/**
 * @ngdoc function
 * @name minervaApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the minervaApp
 */
angular.module('minervaApp')
    .controller('MainCtrl', function ($scope) {
        $scope.gridOptions = {
            dataSource: {
                type: 'odata',
                transport: {
                    read: 'http://demos.telerik.com/kendo-ui/service/Northwind.svc/Orders'
                },

                // the schema allows for the drop-down filters to have types
                schema: {
                    /*
                    data: function(data) {
                        return data.value;
                    },
                    total: function(data) {
                        return data['odata.count'];
                    },
                    */
                    model: {
                        fields: {
                            OrderID: { type: 'number' },
                            Freight: { type: 'number' },
                            ShipName: { type: 'string' },
                            OrderDate: { type: 'date' },
                            ShipCity: { type: 'string' }
                        }
                    }
                },

                // server side operations
                pageSize: 20,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },

            // enable virtual scrolling
            scrollable: {
                virtual: true
            },

            // grid settings
            height: 550,
            filterable: true,
            sortable: true,
            pageable: true,
            groupable: true,

            // column definitions for other titles and properties
            columns: [
                {
                    field: 'OrderID',
                    filterable: false
                },
                {
                    field: 'Freight'
                },
                {
                    field: 'OrderDate',
                    title: 'Order Date',
                    format: '{0:MM/dd/yyyy}'
                },
                {
                    field: 'ShipName',
                    title: 'Ship Name'
                },
                {
                    field: 'ShipCity',
                    title: 'Ship City'
                }
            ]
        };

    });
