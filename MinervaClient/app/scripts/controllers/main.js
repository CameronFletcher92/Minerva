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
        // the base url
        var crudServiceBaseUrl = 'http://MinervaService.cloudapp.net/api/DowntimeEvent';

        // the options of the main grid
        $scope.gridOptions = {
            dataSource: {
                type: 'odata',
                // connect to the odata controller
                transport: {
                    read: {
                        url: crudServiceBaseUrl + '?$expand=Equipment',
                        dataType: 'json'
                    },
                    create: {
                        url: crudServiceBaseUrl,
                        type: "POST",
                        dataType: "json"
                    },
                    update: {
                        url: function (data) {
                            return crudServiceBaseUrl + '(' + data['Id'] + ')';
                        },
                        type: "PUT",
                        dataType: "json"
                    },
                    destroy: {
                        url: function (data) {
                            return crudServiceBaseUrl + '(' + data['Id'] + ')';
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
            },

            // enable virtual scrolling
            /*
            scrollable: {
                virtual: true
            },
            */

            // grid settings
            height: 550,
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

            // the controls
            //toolbar: ["create"],
            editable: "popup"
        };

    });
