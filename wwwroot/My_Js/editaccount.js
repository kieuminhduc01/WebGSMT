$('#btnEditAccount').on('click', function () {
    debugger;
    var username = $('#UserName').val();
    var Password = $('#Password').val();
    var FullName = $('#FullName').val();
    var DOB = $('#DOB').val();
    var Email = $('#Email').val();
    var PhoneNumber = $('#PhoneNumber').val();
    var Active = $('#Active').val();

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
            Active: Active
        },
        success: function (data) {
            alert(data);
            showMessage("Edit Account success!", true);
            reloadDataTable();
        },
        error: function (data, jqXHR, textStatus, errorThrown) { }
    });

});