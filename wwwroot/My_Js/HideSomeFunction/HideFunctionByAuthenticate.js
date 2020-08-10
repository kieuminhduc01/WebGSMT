
$(document).ready(function loadPermisson() {
    $.get("/Authenticate/permissions", function (data, status) {
        sessionStorage.lstPermission = data;
        var temp = sessionStorage.lstPermission;
        var lst = temp.split(",");

        function ShowPermission(item) {

            if (item == "Thiet bi-Xem") {
                $("#menu-ThietBi").show();
            }
            if (item == "Quan Tri Vien") {
                $("#menu-QuanTriVien").show();
            }
            if (item == "Quan Tri Vien-Vai Tro-Xem") {
                $("#menu-QuanTriVien-VaiTro-Xem").show();
            }
            if (item == "Quan Tri Vien-Nguoi Dung-Xem") {
                $("#menu-QuanTriVien-NguoiDung-Xem").show();
            }
        }
        lst.forEach(ShowPermission);
    });
});

function loadPermissionThietBiVaGiaoThuc() {
    var temp = sessionStorage.lstPermission;
    var lst = temp.split(",");

    function ShowPermission(item) {

        if (item == "Thiet bi-Them moi") {
            $("#Thietbi-Themmoi").show();
        }
        if (item == "Thiet bi-Sua") {
            $(".bt-open-edit-devices-form").show();
        }
        if (item == "Thiet bi-Xoa") {
            $(".bt-delete-devices").show();
        }
    }
    lst.forEach(ShowPermission);
}

function loadPermissionDanhMucDuLieu() {
    var temp = sessionStorage.lstPermission;
    var lst = temp.split(",");
    function ShowPermission(item) {

        if (item == "Danh muc du lieu-Them moi") {
            $(".Danhmucdulieu-Themmoi").show();
        }
        if (item == "Danh muc du lieu-Sua") {
            $(".edit-catalogdata-form").show();
        }
        if (item == "Danh muc du lieu-Xoa") {
            $(".delete-catalogdata-form").show();
        }
    }
    lst.forEach(ShowPermission);
}

function loadPermissionDanhSachVaiTro() {
    var temp = sessionStorage.lstPermission;
    var lst = temp.split(",");
    function ShowPermission(item) {

        if (item == "Quan Tri Vien-Vai Tro-Them moi") {
            $("#QuanTriVien-VaiTro-Themmoi").show();
        }
        if (item == "Quan Tri Vien-Vai Tro-Sua") {
            $(".bt-open-edit-account-form").show();
        }
        if (item == "Quan Tri Vien-Vai Tro-Xoa") {
            $(".bt-delete-role").show();
        }
    }
    lst.forEach(ShowPermission);
}
function loadPermissionDanhNguoiDung() {
    var temp = sessionStorage.lstPermission;
    var lst = temp.split(",");
    function ShowPermission(item) {

        if (item == "Quan Tri Vien-Nguoi Dung-Them moi") {
            $("#QuanTriVien-NguoiDung-Themmoi").show();
        }
        if (item == "Quan Tri Vien-Nguoi Dung-Sua") {
            $(".btnEditAccount").show();
            $(".bt-update-active").show();
        }
        if (item == "Quan Tri Vien-Nguoi Dung-Xoa") {
            $(".bt-delete-account").show();
        }
    }
    lst.forEach(ShowPermission);
}