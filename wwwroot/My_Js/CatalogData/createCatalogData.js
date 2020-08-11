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
                $('#my_datatable_CatalogData').DataTable().ajax.reload();
                $('#formModal').modal('hide');
                showMessage("Tạo mới thành công!", true);
            }
            else {
                showMessage("Lỗi tạo mới!", false);
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
        $("#tagnamecheck").text("Nhập tên thẻ!");
        $("#Tagname").css('border-color', 'red');
        trangThai = false;
    }
    else {
        $("#tagnamecheck").text('');
    }
    if (name == "") {
        $("#namecheck").text("Nhập tên của thiết bị hoặc tên giao thức!");
        $("#Name").css('border-color', 'red');
        trangThai = false;
    }
    else {
        $("#namecheck").text("");
    }
    if (address == "") {
        $("#addresscheck").text("Nhập địa chỉ!");
        $("#Address").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#addresscheck").text("");
    }

    if (unit == "") {
        $("#unitcheck").text("Nhập đơn vị!");
        $("#Unit").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#unitcheck").text("");
    }
    if (min == "") {
        $("#mincheck").text("Nhập giới hạn cảnh báo!");
        $("#Min").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#mincheck").text("");
    }
    if (max == "") {
        $("#maxcheck").text("Nhập giới hạn cảnh báo!");
        $("#Max").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#maxcheck").text("");
    }

    if ($("#Status").text() == 'Tên thẻ đã tồn tại...' || $("#Status").text() == 'Checking...') {
        trangThai = false;
    }

    return trangThai;
};
$('.validateTextBox').keyup(function () {
    var textBox = $(this);
    textBox.css('border-color', 'green');

});
function ExistDeviceCheck() {
    $("#Status").html("Đang kiểm tra...");
    $.post("/Users/Devices/CheckExist",
        {
            deviceName: $("#device-name").val()
        },
        function (data) {
            if (data == 0) {
                $("#Status").html('<font color="Green">Tên thiết bị hợp lệ...</font>');
                $("#device-name").css("border-color", "Green");

            }
            else {
                $("#Status").html('<font color="Red">Tên thiết bị đã tồn tại...</font>');
                $("#device-name").css("border-color", "Red");
            }
        });
}