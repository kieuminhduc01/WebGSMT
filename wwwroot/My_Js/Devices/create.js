$('#submit').on('click', function () {
    if (!validateFormCreate()) {
        return;
    }
    var name = $('#device-name').val();
    var nameShow = $('#device-nameShow').val(); 
    var branchOrProtocol = $('#device-BranchOrProtocol').val();
    var url = "/Users/Devices/Create";
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            name: name,
            nameShow: nameShow,
            branchOrProtocol: branchOrProtocol
        },
        success: function (data) {
            if (data) {
                $('#my_datatable_Devices').DataTable().ajax.reload(null, false);
                $('#formEditDevices').modal('hide');
                showMessage("Tạo mới thành công !", true);
            }
            else {
                alert(data);

            }
        }
    });
});
function validateFormCreate() {
    
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
        if (!(/^(?!.*__.*)(?!.*\.\..*)[a-z0-9_.]+$/iu.test(deviceName))) {
            $('#deviceNameValidate').text("Tên thiết bị sai định dạng");
            trangThai = false;
        } else
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
function ExistDeviceCheck() {
    $("#Status").html("Checking...");
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