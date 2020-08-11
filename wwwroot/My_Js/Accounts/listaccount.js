"use strict"
var KTDatatablesDataSourceAjaxServer = function () {

    var initTable1 = function () {
        var table = $('#kt_datatable');
        var dateTimeFormat = new Intl.DateTimeFormat('en', { year: 'numeric', month: 'short', day: '2-digit' });
        // begin first table
        table.DataTable({
            initComplete: function (settings, json) {
                loadPermissionDanhNguoiDung();
            },
            responsive: true,
            searchDelay: 500,
            processing: true,
            serverSide: true,
            info: false,
            ajax: {
                url: "/Admin/Account/getalluser",
                type: 'GET'
            },
            columns: [
                { data: 'userName', name: "UserName" },
                { data: 'fullName', name: "FullName" },
                {
                    data: 'dob', name: "DOB",
                    render: function (data, type, full, meta) {
                        var d = new Date(data),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();

                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;

                        return [day, month, year].join('/');
                    }
                },
                { data: 'email', name: "Email" },
                { data: 'phoneNumber', name: "PhoneNumber" },
                {
                    data: "role", name: "Role",
                    orderable: false,
                    render: function (Role) {
                        var res = Role.split(",");
                        var status = {
                            'Admin': 'label-light-primary',
                            'User': 'label-light-info',
                            'test 1': 'label-light-success'
                            //label-light-danger //label-light-warning
                        };
                        var re ="";
                        for (var i = 0; i < res.length; i++) {
                            re += '<span class="label label-lg font-weight-bold ' + status[res[i]] + ' label-inline">' + res[i] + '</span>';
                        }
                        return re;
                    }
                },
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
                           	<a href="javascript:;" class="btn btn-sm btn-clean btn-icon btnEditAccount" title="Edit details" data-id ="'+ data.userName +'" style="display:none" >\
								<i class="la la-edit"></i>\
							</a>\
                            <a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-update-active" title="Active User" data-id="'+ data.userName + '" style="display:none">\
                                <i class="fas '+ data.classCheck + '" name="active" id="active" data-active=' + data.active + '></i>\
                            </a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-delete-account" title="Delete" data-id="'+ data.userName + '" style="display:none">\
								<i class="la la-trash"></i>\
							</a>\
						';
                    }
                },
            ]
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
        type: 'GET',
        data: {
            id:id
        },
        success: function (data) {
            showMessage("Đổi thành công!",true);
            reloadDataTable();
        },
        error: function (data, jqXHR, textStatus, errorThrown) {
            showMessage("Lỗi!", false);
        }
    });

})

function reloadDataTable() {
    $('#kt_datatable').DataTable().ajax.reload(function () {
        loadPermissionDanhNguoiDung();
    }, false);
};

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
});


$('.bt-open-create-account-form').on('click', function () {
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
            showMessage("Lỗi tải form Tạo mới", false);
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
        }
    });


});

$('#kt_datatable').on('click', '.bt-delete-account', function () {
    //add xac thuc trc khi xoa
    var id = $(this).attr("data-id");
    $('#btnDelteYes').attr("data-id", id);
    $('#confirmDelete').modal('show');
    $('#confirmDelete').modal({
        backdrop: true
    });
});

$('#btnDelteYes').on('click', function (e) {
    e.preventDefault();
    var url = "Admin/Account/DeleteAccount";
    var username = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            userName: username
        },
        success: function (data) {
            if (!data) {
                showMessage("Lỗi xóa tài khoản!", false);
            } else {
                showMessage("Xóa tài khoản thành công!", true);
                reloadDataTable();
            }
        },
        error: function (data) {
            showMessage("Lỗi tải form xóa!", false);
        }
    });
});