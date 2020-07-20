"use strict"
var KTDatatablesDataSourceAjaxServer = function () {

    var initTable1 = function () {
        var table = $('#kt_datatable');

        // begin first table
        table.DataTable({
            responsive: true,
            searchDelay: 500,
            processing: true,
            serverSide: true,
            ajax: {
                url: "/Admin/Account/getAllUser",
                type: 'GET'
            },
            columns: [
                { data: 'userName', name: "UserName" },
                { data: 'fullName', name: "FullName" },
                {
                    data: 'dob', name: "DOB",
                    render: function (data, type, full, meta) {
                        return data.substr(0, 10);
                    }
                },
                { data: 'email', name: "Email" },
                { data: 'phoneNumber', name: "PhoneNumber" },
                /*{ data: 'Status' },*/
                {
                    data: {
                        active: 'active',
                        userName: 'userName',
                        classCheck : 'classCheck'
                    },
                    name: "Actions",
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return '\
                           	<a href="javascript:;" class="btn btn-sm btn-clean btn-icon btnEditAccount" title="Edit details" data-id ="'+ data.userName +'">\
								<i class="la la-edit"></i>\
							</a>\
                            <a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-update-active" title="Active User" data-id="'+ data.userName + '">\
                                <i class="fas '+ data.classCheck + '" name="active" id="active" data-active=' + data.active + '></i>\
                            </a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\
						';
                    }
                },
            ]

            /*{
                width: '75px',
                targets: -3,
                render: function (data, type, full, meta) {
                    var status = {
                        1: { 'title': 'Pending', 'class': 'label-light-primary' },
                        2: { 'title': 'Delivered', 'class': ' label-light-danger' },
                        3: { 'title': 'Canceled', 'class': ' label-light-primary' },
                        4: { 'title': 'Success', 'class': ' label-light-success' },
                        5: { 'title': 'Info', 'class': ' label-light-info' },
                        6: { 'title': 'Danger', 'class': ' label-light-danger' },
                        7: { 'title': 'Warning', 'class': ' label-light-warning' },
                    };
                    if (typeof status[data] === 'undefined') {
                        return data;
                    }
                    return '<span class="label label-lg font-weight-bold' + status[data].class + ' label-inline">' + status[data].title + '</span>';
                },
            },*/

        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTable1();
        },

    };

}();

$('#kt_datatable').on('click', '.bt-update-active',function () {
    var id = $(this).attr("data-id");
    $.ajax({
        url: "/Admin/Account/activeOrNot",
        type: 'Get',
        data: {
            id:id
        },
        success: function (data) {
            showMessage("Change Active Success!",true);
            reloadDataTable();
        },
        error: function (data, jqXHR, textStatus, errorThrown) {}
    });

})

function reloadDataTable() {
    $('#kt_datatable').DataTable().ajax.reload(null, false);
};

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
});

$('#btnCreateAccount').on('click', function () {
    var url = "/Admin/Account/Create";
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $('#formEditRole').html(data);
            $('#formEditRole').modal('show');
            $('#formEditRole').modal({
                backdrop: false
            });
        },
        error: function (data) {
            alert("Error load ajax create account");
        }
    });


});

$('#kt_datatable').on('click', '.btnEditAccount', function () {
    var id = $(this).attr("data-id");
    var url = "/Admin/Account/Edit";
    $.ajax({
        url: url,
        type: 'GET',
        data: {
            id: id
        },
        success: function (data) {
            $('#formEditRole').html(data);
            $('#formEditRole').modal('show');
            $('#formEditRole').modal({
                backdrop: false
            });
        },
        error: function (data) {
            alert("Error load ajax edit account");
        }
    });


});