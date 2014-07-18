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
        // the downtime event url
        var dtUrl = 'http://MinervaService.cloudapp.net/api/DowntimeEvent';
        var equipUrl = 'http://MinervaService.cloudapp.net/api/Equipment';

        // the kendo data source for downtime events
        var eventDataSource = new kendo.data.DataSource({
            type: 'odata',
            // connect to the odata controller
            transport: {
                read: {
                    url: dtUrl + '?$expand=Equipment',
                    dataType: 'json'
                },
                create: {
                    url: dtUrl,
                    type: "POST",
                    dataType: "json"
                },
                update: {
                    url: function (data) {
                        return dtUrl + '(' + data['Id'] + ')';
                    },
                    type: "PUT",
                    dataType: "json"
                },
                destroy: {
                    url: function (data) {
                        return dtUrl + '(' + data['Id'] + ')';
                    },
                    type: "DELETE",
                    dataType: "json"
                }
            },

            // the schema allows for the drop-down filters to have types
            schema: {
                data: function(data) {
                    return data.value;
                },
                total: function(data) {
                    return data['odata.count'];
                },
                model: {
                    id : "Id",
                    fields: {
                        "Equipment.Code" : {type:'string', editable: false},
                        Start : {type: 'date'},
                        End : {type: 'date'}
                    }
                }
            },

            // server side operations
            pageSize: 20,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        });

        // the kendo hierarchical data source for equipments
        var equipDataSource = new kendo.data.HierarchicalDataSource({
            type: 'odata',
            // connect to the odata controller
            transport: {
                read: {
                    url: equipUrl,
                    dataType: 'json'
                }
            },
            schema: {
                data: function(data) {
                    return data.value;
                },
                total: function(data) {
                    return data['odata.count'];
                },
                model: {
                    id : "Id",
                    fields: {
                        Code : {type:'string', editable: false},
                        Description : {type: 'string'}
                    }
                }
            }
        });

        // the options of the main grid
        $scope.gridOptions = {
            dataSource: eventDataSource,

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
                    title: 'Start'
                },
                {
                    field: 'End',
                    title: 'End'
                },

                // the controls
                { command: ["edit", "destroy"], title: "&nbsp;", width: "200px" }
            ],

            // edit style
            editable: "popup"
        };

        $scope.treeOptions = {
            dataSource: equipDataSource,
            template: '{{dataItem.Code}}',
            change: function(e) {
                console.log("Change", this.select());
            }
        };


    });
