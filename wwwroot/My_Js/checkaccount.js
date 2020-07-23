
function UserCheck() {
    $("#Status").html("Checking...");
    $.post("/Admin/Account/CheckExits",
        {
            userdata: $("#UserName").val()
        },
        function (data) {
            if (data == 0) {
                $("#Status").html('<font color="Green">UseName hợp lệ...</font>');
                $("#UserName").css("border-color", "Green");

            }
            else {
                $("#Status").html('<font color="Red">UserName đã tồn tại...</font>');
                $("#UserName").css("border-color", "Red");
            }
        });
}