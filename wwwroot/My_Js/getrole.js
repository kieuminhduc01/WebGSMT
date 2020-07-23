function getRole() {
    var username = $('#usernameCheck').val();
    $.ajax({
        url: '/Admin/Role/getRole',
        type: 'GET',
        data: {
            UserName: username
        },
        success: function (data) {
            $('#checkBoxRole').html(data);
        },
        error: function (data) {
            showMessage("Error Load Role", false);
        }
    });
}

getRole();