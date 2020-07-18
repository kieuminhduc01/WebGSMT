
    $('#submit').on('click', function () {
        var url = "/Admin/Role/InsertRole";
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
        alert(data);
                } else {
        $('#my_datatable_role').DataTable().ajax.reload(null, false);
                    showMessage("Create Success!", true);
                }
            },
            error: function (data) {
        showMessage("Create Fail!", false);
            }
        });
    });
    //load jstree
     $(document).ready(function () {
        $.ajax({
            url: "/Admin/Role/GetAllPermission",
            type: 'GET',
            data: {
                RoleName: "0"
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
                showMessage("Load permission success", true);
            },
            error: function (data) {
                showMessage("Load permission fail!", false);
            }
        });
    });