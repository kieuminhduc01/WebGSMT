$('#my_datatable_Devices').on('click', '.bt-open-edit-devices-form', function () {
    debugger;

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
            //showMessage("Error load ajax edit account", false);
        }
    });
});

//$('.bt-open-create-account-form').on('click', function () {
//    var url = "/Users/Devices/Create";
//    $.ajax({
//        url: url,
//        type: 'GET',
//        success: function (data) {
//            $('#formEditDevices').html(data);
//            $('#formEditDevices').modal('show');
//            $('#formEditDevices').modal({
//                backdrop: false
//            });
//        },
//        error: function (data) {
//            //showMessage("Error load ajax create account", false);
//        }
//    });


//});

//$('#my_datatable_Devices').on('click', '.bt-delete-role', function () {
//    debugger;

//    //add xac thuc trc khi xoa
//    var id = $(this).attr("data-id");
//    $('#btnDelteYes').attr("data-id", id);
//    $('#confirmDelete').modal('show');
//    $('#confirmDelete').modal({
//        backdrop: true
//    });
//});

//$('#btnDelteYes').on('click', function (e) {
//    debugger;
//    e.preventDefault();
//    var url = "Users/Devices/DeleteRole";
//    var rolename = $(this).attr("data-id");
//    $.ajax({
//        url: url,
//        type: 'POST',
//        data: {
//            rolename: rolename
//        },
//        success: function (data) {
//            if (!data) {
//                //showMessage("Error delete role ", false);
//            } else {
//                //showMessage("Delete role success!", true);
//                $('#my_datatable_Devices').DataTable().ajax.reload(null, false);
//            }
//        },
//        error: function (data) {
//            //showMessage("Error load ajax delete role", false);
//        }
//    });
//});


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
                { data: 'branchOrProtocol', name: "BranchOrProtocol" },
                { data: 'nameShow', name: "NameShow" },
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
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-open-edit-devices-form" title="Edit" data-id="'+ data + '">\
								<i class="la la-edit"></i>\
							</a>\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon bt-delete-role" title="Delete" data-id="'+ data + '">\
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
    KTDatatablesDataSourceAjaxServer.init();
});

