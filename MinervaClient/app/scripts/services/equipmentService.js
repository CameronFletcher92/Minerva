'use strict';
angular.module('minervaApp').service('EquipmentSvc', function () {

    // equipment data source
    var baseUrl = 'http://MinervaService.cloudapp.net/api/Equipment';
    var dataSource = new kendo.data.HierarchicalDataSource({
        type: 'odata',
        // connect to the odata controller
        transport: {
            read: {
                // show top level equipment
                url: baseUrl + '?$expand=Children&$filter=ParentId eq null',
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
                children: 'Children',
                hasChildren: function(item) {
                    return item["Children"] && item["Children"].length > 0;
                },
                fields: {
                    Id : {type: 'number', editable: false},
                    Code : {type: 'string', editable: false},
                    ParentId : {type: 'number', editable: false},
                    Description : {type: 'string', editable: false}
                }
            }
        }
    });

    this.getDataSource = function() {
        return dataSource;
    }
});
