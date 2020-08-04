$('#kt_datatable').on('click', '.btnEditAccount', function () {
    var id = $(this).attr("data-id");
    var url = "/Admin/Account/Edit";
    $.ajax({
        url: url,
        type: 'GET',
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
            showMessage("Lỗi tải form Sửa tài khoản!", false);
        }
    });


});

$('#btnEditAccount').on('click', function () {

    if (validateFormEdit()) {
        var username = $('#UserName').val();
        var Password = $('#Password').val();
        var FullName = $('#FullName').val();
        var DOB = $('#DOB').val();
        var Email = $('#Email').val();
        var PhoneNumber = $('#PhoneNumber').val();
        var CheckRole = [];
        $('#checkBoxRole input:checked').each(function () {
            CheckRole.push($(this).val());
        });
        var Active;
        if ($('#Active').is(":checked")) {
            Active = 'true';
        }
        else
            Active = 'false';
        $.ajax({
            url: '/Admin/Account/edit',
            type: 'POST',
            data: {

                UserName: username,
                Password: Password,
                FullName: FullName,
                DOB: DOB,
                Email: Email,
                PhoneNumber: PhoneNumber,
                Active: Active,
                RoleName: CheckRole
            },
            success: function (data) {
                if (data == "success") {
                    showMessage("Sửa tài khoản thành công!", true);
                    reloadDataTable();
                } else {
                    showMessage("Lỗi sửa tài khoản!", false);
                }

            },
            error: function (data, jqXHR, textStatus, errorThrown) { }
        });

    } else {

    }

});

