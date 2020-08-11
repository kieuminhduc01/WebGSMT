$('#my_datatable_Devices').on('click', '.bt-open-edit-devices-form', function () {
    var url = "/Users/Devices/Edit";
    var id = $('.bt-open-edit-devices-form').attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            id: id
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

$('.bt-open-create-device-form').on('click', function () {
    var url = "/Users/Devices/Create";
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $('#formModal').html(data);
            $('#formModal').modal('show');
            $('#formModal').modal({
                backdrop: false
            });
        },
        error: function (data) {
            showMessage("Lỗi tải trang", false);
        }
    });


});

$('#my_datatable_Devices').on('click', '.bt-delete-devices', function () {
    var id = $(this).attr("data-id");
    $('#btnDelteYes').attr("data-id", id);
    $('#confirmDeleteDevice').modal('show');
    $('#confirmDeleteDevice').modal({
        backdrop: true
    });
});

$('#btnDelteYes').on('click', function (e) {
    e.preventDefault();
    var url = "Users/Devices/DeleteDevices";
    var name = $('.bt-delete-devices').attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            name: name
        },
        success: function (data) {
            if (data) {
                showMessage("Xóa thành công!", true);
                $('#my_datatable_Devices').DataTable().ajax.reload(function (json) {
                    loadPermissionThietBiVaGiaoThuc();
                }, false);
            } else {
                showMessage("Lỗi xóa thiết bị ", false);
            }
        },
        error: function (data) {
            showMessage("Lỗi tải trang!", false);
        }
    });
});


"use strict";
var KTDatatablesDataSourceAjaxServer = function () {
    var tableDevices = function () {
        var table = $('#my_datatable_Devices');

        // begin first table
        table.DataTable({
            responsive: true,
            searchDelay: 500,
            serverSide: true,
            info: false,
            
            drawCallback: function (settings, json) {
                loadPermissionThietBiVaGiaoThuc(); 
            },
            processing: true,
            language: {

                "processing": "Đang xử lý...",
                "search": "Tìm kiếm",
                "lengthMenu": "Hiển thị _MENU_ dữ liệu trên một trang",
                "infoEmpty": "Không có dữ liệu",
                "zeroRecords": "Không có dữ liệu",
                "info": "Trang thứ _PAGE_ Trên tổng số _PAGES_",
                "infoFiltered": "(filtered from _MAX_ total records)"
            },
            ajax: {
                url: "/Users/Devices/GetAllDevices",
                type: 'GET'
            },
            columns: [
                { data: 'name', name: "name" },
                { data: 'nameShow', name: "NameShow" },
                { data: 'branchOrProtocol', name: "BranchOrProtocol" },
                {
                    data: 'name', name: "Actions",
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
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-open-edit-devices-form " title="Edit" data-id="'+ data + '" style="display:none">\
								<i class="la la-edit"></i>\
							</a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-delete-devices" title="Delete" data-id="'+ data + '" style="display:none" >\
								<i class="la la-trash"></i>\
							</a>\
                            <a href="javacript:;" class="btn btn-sm btn-clean btn-icon bt-show-dvn" title="Show Data" data-id="'+ data + '">\
								<i class="fa fa-eye" aria-hidden="true"></i>\
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

function loadTableCatalog(dvn) {
    $.ajax({
        url: "/Users/CatalogData/Index",
        type: 'GET',
        data: {
            DeviceName: dvn
        },
        success: function (data) {

            $('#loadTableCatalog').html(data);
        },
        error: function (jqXHR, error, errorThrown) {
            showMessage(jqXHR.responseText, false);
        }

    });
}

$('#my_datatable_Devices').on('click', '.bt-show-dvn', function () {
    //add xac thuc trc khi xoa
    var id = $(this).attr("data-id");
    loadTableCatalog(id);
});

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
    loadTableCatalog("");
});

