$('#submit').on('click', function () {
    var url = "/Users/Devices/UpdateDevice";
    var deviceName = document.getElementById("device-name").value;
    var deviceNameShow = document.getElementById("device-nameShow").value;
    var deviceBranchOrProtocol = document.getElementById("device-BranchOrProtocol").value;
    $.each(selectedNodes, function () {
        checked_ids.push(this.id);
    });
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            name: deviceName,
            nameShow: deviceNameShow,
            branchOrProtocol: deviceBranchOrProtocol
        },
        success: function (data) {
            if (data != "success") {
                alert(data);
            } else {
                $('#my_datatable_Devices').DataTable().ajax.reload(null, false);
                //showMessage("Edit successfully!", true);
            }
        },
        error: function (data) {
            //showMessage(" Edit Fail!", false);
        }
    });
});
$(document).ready(function () {
    var rolename = document.getElementById("role-name").value;
    $.ajax({
        url: "/Admin/Role/GetAllPermission",
        type: 'GET',
        data: {
            RoleName: rolename
        },
        success: function (data) {
            $('#jstree').jstree({
                plugins: ['checkbox'],
                'core': {
                    'data': data
                },
            }).on('ready.jstree', function () {
                $('#jstree').jstree("open_all");
            });
        },
        error: function (data) {
            //showMessage("Load Permission Fail!", false);
        }
    });
});