$('#btnSaveChange').on('click', function () {

    if (ValidateEditForm()) {
        var FullName = $('#fullName').val();
        var DOB = $('#datepicker').val();
        var Email = $('#email').val();
        var PhoneNumber = $('#phone').val();
        $.ajax({
            url: '/Users/Information/edit',
            type: 'POST',
            data: {
                FullName: FullName,
                Dob: DOB,
                Email: Email,
                phoneNumber: PhoneNumber
            },
            success: function (data) {
                if (data == "success") {
                    showMessage("Edit Account success!", true);
                    $('#PF_FullName').load(" #PF_FullName");
                } else {
                    showMessage("Edit Account fail!", false);
                }
            },
            error: function (data, jqXHR, textStatus, errorThrown) { }
        });
    }
});


