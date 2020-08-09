$('#submit').on('click', function () {
    if (!validateFormEdit()) {
        return;
    }
    var url = "/Users/Devices/updateDevices";
    var deviceName = document.getElementById("device-name").value;
    var deviceNameShow = document.getElementById("device-nameShow").value;
    var deviceBranchOrProtocol = document.getElementById("device-BranchOrProtocol").value;
   
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            name: deviceName,
            nameShow: deviceNameShow,
            branchOrProtocol: deviceBranchOrProtocol
        },
        success: function (data) {
            if (data != "success") {
            } else {
                $('#my_datatable_Devices').DataTable().ajax.reload(null, false);
                showMessage("Sửa thành công !", true);
                $('#formModal').modal('hide');
               
            }
        },
        error: function (data) {
            showMessage("Lỗi tải trang!", false);
        }
    });
});

function validateFormEdit() {

    var deviceName = $("#device-name").val();
    var deviceNameShow = $("#device-nameShow").val();
    var deviceBranchOrProtocol = $("#device-BranchOrProtocol").val();

    var trangThai = true;

    if (deviceName == "") {
        $("#deviceNameValidate").text("Nhập tên thiết bị");
        $("#device-name").css('border-color', 'red');
        trangThai = false;
    }
    else {
        $("#deviceNameValidate").text('');
    }
    if (deviceNameShow == "") {
        $("#deviceNameShowValidate").text("Nhập chi tiết");
        $("#device-nameShow").css('border-color', 'red');
        trangThai = false;
    }
    else {
        $("#deviceNameShowValidate").text("");
    }
    if (deviceBranchOrProtocol == "") {
        $("#deviceBranchOrProtocolValidate").text("Nhập loại");
        $("#device-BranchOrProtocol").css('border-color', 'red');

        trangThai = false;
    }
    else {
        $("#deviceBranchOrProtocolValidate").text("");
    }

    if ($("#Status").text() == 'Tên thiết bị đã tồn tại...' || $("#Status").text() == 'Đang kiểm tra...') {
        trangThai = false;
    }
 
    return trangThai;
};
$('.validateTextBox').keyup(function () {
    var textBox = $(this);
    textBox.css('border-color', 'green');

});