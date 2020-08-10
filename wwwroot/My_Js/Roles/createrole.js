$('#submit').on('click', function () {
    if (Validate() && RoleCheck()) {

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
                    showMessage(data, false);
                } else {
                    $('#my_datatable_role').DataTable().ajax.reload(function () { loadPermissionDanhSachVaiTro();}, false);
                    showMessage("Tạo mới thành công!", true);
                }
            },
            error: function (data) {
                showMessage("Lỗi tạo mới!", false);
            }
        });
    }

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
            showMessage("tải trang \"quyền truy cập\" thành công", true);
        },
        error: function (data) {
            showMessage("Lỗi tải trang \"quyền truy cập\"!", false);
        }
    });
});
function Validate() {
    var roleName = $('#role-name').val();
    var roleDescription = $('#role-description').val();

    var roleNameValidation = $('#Validate-role-name');
    var roleDescriptionValidation = $('#Validate-role-description');

    roleNameValidation.text("");

    roleDescriptionValidation.text("");

    var isValidation = true;
    if (roleName == "") {
        InvalidMsg(document.getElementById('role-name'));
        $('#Validate-role-name').text("Nhập tên vai trò");
        isValidation = false;
    }
    if (!(/^(?!.*__.*)(?!.*\.\..*)[a-z0-9_.]+$/iu.test(roleName))) {
        $('#Validate-role-name').text("Tên vai trò sai định dạng");
        isValidation = false;
    }
    if (roleDescription == "") {
        $('#Validate-role-description').text("Nhập Mô tả");
        isValidation = false;
    }
    return isValidation;
}
function RoleCheck() {
    var validationCheck = true;
    $("#Status").html("Checking...");
    $.ajaxSetup({ async: false });
    $.post("/Admin/role/CheckExits",
        {
            userdata: $("#role-name").val()
        },
        function (data) {
            if (data == 0) {
                $("#Validate-role-name").html('<font color="Green">Tên tài khoản hợp lệ...</font>');
                $("#role-name").css("border-color", "Green");
            }
            else if (data == 1) {
                $("#Validate-role-name").html('<font color="Red">Tên tài khoản đã tồn tại...</font>');
                $("#role-name").css("border-color", "Red");
                validationCheck = false;
            }
        }
    );
    return validationCheck;
}
function InvalidMsg(textbox) {
debugger;
    if (textbox.value === '') {
        textbox.setCustomValidity('Entering an email-id is necessary!');
    } else {
        textbox.setCustomValidity('');
    }
    
    return true;
} 