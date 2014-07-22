'use strict';
angular.module('minervaApp').service('EquipmentSvc', function () {

    // equipment data source
    //var baseUrl = 'http://MinervaService.cloudapp.net/api/Equipment';
    var baseUrl = 'http://local.minerva.com:39658/api/Equipment';
    var dataSource = new kendo.data.DataSource({
        type: 'odata',
        // connect to the odata controller
        transport: {
            read: {
                // show top level equipment
                url: baseUrl,
                dataType: 'json'
            },
            create: {
                url: baseUrl,
                type: "POST",
                dataType: "json"
            },
            update: {
                url: function (data) {
                    return baseUrl + '(' + data['Id'] + ')';
                },
                type: "PUT",
                dataType: "json"
            },
            destroy: {
                url: function (data) {
                    return baseUrl + '(' + data['Id'] + ')';
                },
                type: "DELETE",
                dataType: "json"
            }
        },
        schema: {
            data: function (data) {
                return data.value;
            },
            total: function (data) {
                return data['odata.count'];
            },
            model: {
                id: 'Id',
                fields: {
                    Code : {type: 'string'},
                    ParentId : {type: 'number', editable: false},
                    Description : {type: 'string'}
                }
            }
        },

        // server side operations
        pageSize: 20,
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    });

    var hDataSource = new kendo.data.HierarchicalDataSource({
        type: 'odata',
        // connect to the odata controller
        transport: {
            read: {
                // show top level equipment
                url: baseUrl + '?$expand=Children&$filter=ParentId eq null',
                dataType: 'json'
            }
        },
        schema: {
            data: function (data) {
                return data.value;
            },
            total: function (data) {
                return data['odata.count'];
            },
            model: {
                id: 'Id',
                children: 'Children',
                hasChildren: function(item) {
                    return item["Children"] && item["Children"].length > 0;
                },
                fields: {
                    Code : {type: 'string'},
                    ParentId : {type: 'number', editable: false},
                    Description : {type: 'string'}
                }
            }
        }
    });


    this.getHierarchicalDataSource = function() {
        return hDataSource;
    };

    this.getDataSource = function() {
        return dataSource;
    };
});
