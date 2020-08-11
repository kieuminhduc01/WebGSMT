$('#btnCreateAccount').on('click', function () {
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
            showMessage("Lỗi tải form Tạo mới!",false);
        }
    });
});
$('.btnCreateAccount').on('click', function () {
    if (!validateFormCreate()) {
        return;
    }
    var username = $('#UserName').val();
    var FullName = $('#FullName').val();
    var DOB = $('#DOB').val();
    var Email = $('#Email').val();
    var PhoneNumber = $('#PhoneNumber').val();
    var Active;
    var CheckRole = [];
    $('#checkBoxRole input:checked').each(function () {
        CheckRole.push($(this).val());
    });

    if ($('#Active').is(":checked")) {
        Active = 'true';
    }
    else
        Active = 'false';
    $.ajax({
        url: '/Admin/Account/Create',
        type: 'POST',
        data: {

            UserName: username,
            FullName: FullName,
            DOB: DOB,
            Email: Email,
            PhoneNumber: PhoneNumber,
            Active: Active,
            RoleName: CheckRole
        },
        success: function (data) {
            if (data == "success") {
                showMessage("Tạo mới tài khoản thành công!", true);
                reloadDataTable();
            } else {
                showMessage("Lỗi tạo mới tài khoản!", false);
            }

        },
        error: function (data, jqXHR, textStatus, errorThrown) {

        }
    });

});
