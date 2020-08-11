$('#my_datatable_role').on('click', '.bt-open-edit-account-form', function () {
    var url = "/Admin/Role/Edit";
    var id = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
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
            showMessage("Lỗi tải trang!", false);
        }
    });
});

$('.bt-open-create-account-form').on('click', function () {
    var url = "/Admin/Role/Create";
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
            showMessage("Lỗi tải trang", false);
        }
    });


});

$('#my_datatable_role').on('click', '.bt-delete-role', function () {
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
    var url = "Admin/Role/DeleteRole";
    var rolename = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            rolename: rolename
        },
        success: function (data) {
            if (!data) {
                showMessage("Lỗi xóa vai trò!", false);
            } else {
                showMessage("Xóa vai trò thành công!", true);
                $('#my_datatable_role').DataTable().ajax.reload();
            }
        },
        error: function (data) {
            showMessage("Lỗi tải trang", false);
        }
    });
});


"use strict";
var KTDatatablesDataSourceAjaxServer = function () {

    var initTable1 = function () {
        var table = $('#my_datatable_role');

        // begin first table
        table.DataTable({
            drawCallback: function (settings, json) {
                loadPermissionDanhSachVaiTro();
            },
            responsive: true,
            searchDelay: 500,
            processing: true,
            serverSide: true,
            info: false,
            language: {

                "processing": "Đang xử lý...",
                "search": "Tìm kiếm",
                "lengthMenu": "Hiển thị _MENU_ dữ liệu trên một trang",
                "infoEmpty": "Không có dữ liệu",
                "zeroRecords": "Không có dữ liệu",
                "info": "Trang thứ _PAGE_ Trên tổng số _PAGES_",
                "infoFiltered": "(filtered from _MAX_ total records)"
            },
            ordering: false,
            ajax: {
                url: "/Admin/Role/GetAllRole",
                type: 'GET'
            },
            columns: [
                { data: 'roleName', name: "RoleName" },
                { data: 'description', name: "Description" },
                {
                    data: 'roleName', name: "Actions",
                    responsivePriority: -1
                },
            ],
            columnDefs: [
                {
                    targets: -1,
                    title: 'Actions',
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return '\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-open-edit-account-form" title="Edit" data-id="'+ data + '" style="display:none">\
								<i class="la la-edit"></i>\
							</a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-delete-role" title="Delete" data-id="'+ data + '" style="display:none">\
								<i class="la la-trash"></i>\
							</a>\
						';
                    },
                },
            ],
        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTable1();
        },

    };

}();

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
});
