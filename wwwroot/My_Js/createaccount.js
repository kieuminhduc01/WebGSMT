

$('.btnCreateAccount').on('click', function () {
    debugger;
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
                showMessage("Create Account success!", true);
                reloadDataTable();
            } else {
                showMessage("Cannot Create Account!", false);
            }
            
        },
        error: function (data, jqXHR, textStatus, errorThrown) {
            
        }
    });

});
