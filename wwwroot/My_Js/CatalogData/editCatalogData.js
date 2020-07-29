$('#submit').on('click', function () {
    if (!validateFormEdit()) {
        return;
    }
    var url = "/Users/CatalogData/updatecatalogdata";
    var TagName = document.getElementById("Tagname").value;
    var Name = document.getElementById("Name").value;
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
            if (data != "success") {
                alert(data);
            } else {
                $('#my_datatable_CatalogData').DataTable().ajax.reload(null, false);
                showMessage("Edit Success!", true);
                $('#formModal').modal('hide');

            }
        },
        error: function (data) {
            showMessage(" Edit Fail!", false);
        }
    });
});

function validateFormEdit() {

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