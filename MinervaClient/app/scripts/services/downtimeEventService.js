'use strict';
angular.module('minervaApp').service('DowntimeEventSvc', function () {

    // downtime event data source
    var baseUrl = 'http://local.minerva.com:39658/api/DowntimeEvent';
    var dataSource = new kendo.data.DataSource({
        type: 'odata',
        // connect to the odata controller
        transport: {
            read: {
                url: baseUrl + '?$expand=Equipment',
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

        // the schema allows for the drop-down filters to have types
        schema: {
            data: function (data) {
                return data.value;
            },
            total: function (data) {
                return data['odata.count'];
            },
            model: {
                id: "Id",
                fields: {
                    "Equipment.Code": {type: 'string', editable: false},
                    EquipmentId: {type: 'number', editable:false},
                    Start: {type: 'date'},
                    End: {type: 'date'}
                }
            }
        },

        // server side operations
        pageSize: 20,
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    });

    this.getDataSource = function() {
        return dataSource;
    }
});
