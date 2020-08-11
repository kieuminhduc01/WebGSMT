$('#btnSaveChange').on('click', function () {

    if (ValidateEditForm()) {
        var FullName = $('#fullName').val();
        var PassWord = $('#password').val();
        var DOB = $('#datepicker').val();
        var Email = $('#email').val();
        var PhoneNumber = $('#phone').val();
        $.ajax({
            url: '/Users/Information/edit',
            type: 'POST',
            data: {
                FullName: FullName,
                Password : PassWord,
                Dob: DOB,
                Email: Email,
                phoneNumber: PhoneNumber
            },
            success: function (data) {
                if (data == "success") {
                    showMessage("Sửa tài khoản thành công!", true);
                    $('#PF_FullName').load(" #PF_FullName");
                } else {
                    showMessage("Sửa tài khoản lỗi!", false);
                }
            },
            error: function (data, jqXHR, textStatus, errorThrown) { }
        });
    }
});


