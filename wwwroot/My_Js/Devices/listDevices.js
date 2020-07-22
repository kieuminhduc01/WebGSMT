$('#my_datatable_Devices').on('click', '.bt-open-edit-devices-form', function () {
    var url = "/Users/Devices/Edit";

    var id = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            id: id
        },
        success: function (data) {
            $('#formEditDevices').html(data);
            $('#formEditDevices').modal('show');
            $('#formEditDevices').modal({
                backdrop: false
            });
        },
        error: function (data) {
            showMessage("Lỗi load ajax edit", false);
        }
    });
});

$('.bt-open-create-device-form').on('click', function () {
    var url = "/Users/Devices/Create";
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $('#formEditDevices').html(data);
            $('#formEditDevices').modal('show');
            $('#formEditDevices').modal({
                backdrop: false
            });
        },
        error: function (data) {
            showMessage("Lỗi load ajax create Device", false);
        }
    });


});

$('#my_datatable_Devices').on('click', '.bt-delete-devices', function () {

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
    var url = "Users/Devices/DeleteDevices";
    var name = $(this).attr("data-id");
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            name: name
        },
        success: function (data) {
            if (!data) {
                showMessage("Lỗi xóa thiết bị ", false);
            } else {
                showMessage("Xóa thành công!", true);
                $('#my_datatable_Devices').DataTable().ajax.reload(null, false);
            }
        },
        error: function (data) {
            showMessage("Lỗi gọi ajax xóa device", false);
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
            processing: true,
            serverSide: true,
            language: {
                "processing": "Đang sử lý..."
            },
            ordering: false,
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
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-open-edit-devices-form" title="sửa" data-id="'+ data + '">\
								<i class="la la-edit"></i>\
							</a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-delete-devices" title="xóa" data-id="'+ data + '">\
								<i class="la la-trash"></i>\
							</a>\
                            <a href="javascript:;" class="btn btn-sm btn-clean btn-icon " title="dữ liệu" data-id="'+ data + '">\
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

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
});

