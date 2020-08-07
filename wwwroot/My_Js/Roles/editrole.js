$('#submit').on('click', function () {
    if ($('#role-description').val() == "") {
        $('#validationMota').text('Nhập Mô tả');
        return;
    }
    var url = "/Admin/Role/UpdateRole";
    var description = document.getElementById("role-description").value;
    var rolename = document.getElementById("role-name").value;

    var checked_ids = [];
    var selectedNodes = $('#jstree').jstree("get_selected", true);
    $.each(selectedNodes, function () {
        checked_ids.push(this.id);
    });
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            RoleName: rolename,
            Description: description,
            PermissionRole: checked_ids
        },
        success: function (data) {
            if (data != "success") {
            } else {
                $('#my_datatable_role').DataTable().ajax.reload(null, false);
                showMessage("Sửa thành công!", true);
            }
        },
        error: function (data) {
            showMessage(" Edit Fail!", false);
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
            showMessage("Load Permission Fail!", false);
        }
    });
});
