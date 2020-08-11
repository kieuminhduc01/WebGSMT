$('.importData').on('click', function () {

    $('#uploadModal').modal('show');
    $('#uploadModal').modal({
        backdrop: true
    });
});

$('#submit').on('click', function () {
    var uploadfile = $("#nameFile").val();
    $.ajax({
        url: "/Users/CatalogData/importfile",
        type: 'POST',
        data: {
            postedFiles: uploadfile
        },
        success: function (data) {
            if (data == "success") {
                $('#my_datatable_CatalogData').DataTable().ajax.reload(function () { loadPermissionDanhMucDuLieu(); }, false);
                $('#uploadModal').modal('hide');
                showMessage("Tải file lên thành công!", true);
            } else {
                showMessage(data, false);
            }

        },
        error: function (data) {
        }
    });
});

function uploadcsvfile() {
    var postedFiles = new FormData();
    postedFiles.append('postedFiles', document.getElementById('nameFile').files[0]);
    $.ajax({
        url: "/Users/CatalogData/importfile",
        type: "POST",
        data: postedFiles,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data == "success") {
                $('#my_datatable_CatalogData').DataTable().ajax.reload(function () { loadPermissionDanhMucDuLieu(); }, false);
                $('#uploadModal').modal('hide');
                showMessage("Tải file lên thành công!", true);
            } else {
                showMessage(data, false);
            }

        },
        error: function (data) {

        }
    })
}