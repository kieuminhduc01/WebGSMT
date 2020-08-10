$('#my_datatable_CatalogData').on('click', '.edit-catalogdata-form', function () {
    var url = "/Users/CatalogData/Edit";

    var id = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            TagName: id
        },
        success: function (data) {
            $('#formModal').html(data);
            $('#formModal').modal('show');
            $('#formModal').modal({
                backdrop: false
            });
        },
        error: function (data) {
            showMessage("Lỗi tải trang!", false);
        }
    });
});

$('.create-catalogdata-form').on('click', function () {
    var url = "/Users/CatalogData/Create";
    var devicename = $(this).attr('data-id');

    if (devicename.length == 0) {
        showMessage("Vui lòng chọn một thiết bị ở phần danh sách hiết bị và giao thức!", false);
        return;
    };

    $.ajax({
        url: url,
        type: 'GET',
        data: {
            DeviceName: devicename
        },
        success: function (data) {
            $('#formModal').html(data);
            $('#formModal').modal('show');
            $('#formModal').modal({
                backdrop: false
            });
        },
        error: function (data) {
            showMessage("Lỗi tải trang!", false);
        }
    });


});

$('#my_datatable_CatalogData').on('click', '.delete-catalogdata-form', function () {

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
    var url = "Users/CatalogData/DeleteCatalog";
    var name = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            TagName: name
        },
        success: function (data) {
            if (!data) {
                showMessage("Lỗi xóa!", false);
            } else {
                showMessage("Xóa thành công!", true);
                $('#my_datatable_CatalogData').DataTable().ajax.reload(function () { loadPermissionDanhMucDuLieu(); }, false);
            }
        },
        error: function (data) {
            showMessage("Lỗi tải trang!", false);
        }
    });
});


"use strict";
var KTDatatablesDataSourceAjaxServer2 = function () {

    var name = document.getElementById("devicename").value;
    var tableDevices = function () {
        var table = $('#my_datatable_CatalogData');
        // begin first table
        table.DataTable({
            initComplete: function (settings, json) {
                loadPermissionDanhMucDuLieu();
            },
            responsive: true,
            searchDelay: 500,
            processing: true,
            serverSide: true,
            info: false,

            language: {
                "processing": "Đang sử lý..."
            },
            ajax: {
                url: "/Users/CatalogData/GetAllCatalogData",
                type: 'GET',
                data: {
                    name: name
                },
            },
            columns: [
                { data: 'deviceName', name: "DeviceName" },
                { data: 'tagName', name: "TagName" },
                { data: 'address', name: "Address" },
                { data: 'unit', name: "Unit" },
                { data: 'warningMin', name: "WarningMin" },
                { data: 'warningMax', name: "WarningMax" },
                {
                    data: 'tagName', name: "Actions",
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
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon edit-catalogdata-form" title="Edit" data-id="'+ data + '" style="display:none"  >\
								<i class="la la-edit"></i>\
							</a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon delete-catalogdata-form" title="Delete" data-id="'+ data + '" style="display:none"  >\
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
            tableDevices();
        },
    };
}();

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer2.init();
});

