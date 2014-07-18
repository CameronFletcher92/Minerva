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
                    //read: 'http://demos.telerik.com/kendo-ui/service/Northwind.svc/Orders'
                    read: {
                        url: 'http://MinervaService.cloudapp.net/api/DowntimeEvent?$expand=Equipment',
                        dataType: 'json'
                    }
                },

                // the schema allows for the drop-down filters to have types
                schema: {
                    data: function(data) {
                        return data;
                    },
                    total: function(data) {
                        return data['odata.count'];
                    },
                    model: {
                        fields: {
                            Start : {type: 'date'}
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
                    field: 'Start'
                }
            ]
        };

    });
