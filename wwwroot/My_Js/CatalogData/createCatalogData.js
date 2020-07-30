$('#submit').on('click', function () {
    if (!validateFormCreate()) {
        return;
    }

    var url = "/Users/CatalogData/createcatalog";
    var TagName = document.getElementById("Tagname").value;
    var Name = $("#Name").val();
    var Address = document.getElementById("Address").value;
    var Unit = document.getElementById("Unit").value;
    var Min = document.getElementById("Min").value;
    var Max = document.getElementById("Max").value;
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            TagName: TagName,
            deviceName: Name,
            Address: Address,
            Unit: Unit,
            WarningMin: Min,
            WarningMax: Max
        },
        success: function (data) {
            if (data) {
                $('#my_datatable_CatalogData').DataTable().ajax.reload(null, false);
                $('#formModal').modal('hide');
                showMessage("Create success!", true);
            }
            else {
                showMessage("Create Fail!", false);
            }
        }
    });
});
function validateFormCreate() {

    var tagName = $("#Tagname").val();
    var name = $("#Name").val();
    var address = $("#Address").val();
    var unit = $("#Unit").val();
    var min = $("#Min").val();
    var max = $("#Max").val();

    var trangThai = true;

    if (tagName == "") {
        $("#tagnamecheck").text("Input tag name!");
        $("#Tagname").css('border-color', 'red');
        trangThai = false;
    }
    else {
        $("#tagnamecheck").text('');
    }
    if (name == "") {
        $("#namecheck").text("Input name of device or protocol");
        $("#Name").css('border-color', 'red');
        trangThai = false;
    }
    else {
        $("#namecheck").text("");
    }
    if (address == "") {
        $("#addresscheck").text("Input adrdress");
        $("#Address").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#addresscheck").text("");
    }

    if (unit == "") {
        $("#unitcheck").text("Input unit");
        $("#Unit").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#unitcheck").text("");
    }
    if (min == "") {
        $("#mincheck").text("Input warning min");
        $("#Min").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#mincheck").text("");
    }
    if (max == "") {
        $("#maxcheck").text("Input warning max");
        $("#Max").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#maxcheck").text("");
    }

    if ($("#Status").text() == 'TagName has already exists...' || $("#Status").text() == 'Checking...') {
        trangThai = false;
    }

    return trangThai;
};
$('.validateTextBox').keyup(function () {
    var textBox = $(this);
    textBox.css('border-color', 'green');

});
function ExistDeviceCheck() {
    $("#Status").html("Checking...");
    $.post("/Users/Devices/CheckExist",
        {
            deviceName: $("#device-name").val()
        },
        function (data) {
            if (data == 0) {
                $("#Status").html('<font color="Green">UseName hợp lệ...</font>');
                $("#device-name").css("border-color", "Green");

            }
            else {
                $("#Status").html('<font color="Red">UserName đã tồn tại...</font>');
                $("#device-name").css("border-color", "Red");
            }
        });
}