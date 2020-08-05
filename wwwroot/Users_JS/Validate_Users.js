function ValidateEmail(inputText) {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (inputText.match(mailformat)) {
        return true;
    }
    else {
        return false;
    }
}

function ValidatePhoneNumber(inputtxt) {
    var phoneno = /^\d{10}$/;
    if ((inputtxt.match(phoneno)))
        {
        return true;
    }
    else {
        return false;
    }
}

function ValidateEditForm() {
    var FullName = document.forms["informationForm"]["fullName"].value;
    var Email = document.forms["informationForm"]["email"].value;
    var Dob = document.forms["informationForm"]["datepicker"].value;
    var phone = document.forms["informationForm"]["phone"].value;
    var status = true;

    if (FullName == "") {
        document.getElementById("errorFullname").innerHTML = " Chưa nhập họ tên ";
        status = false;
    }
    else {
        document.getElementById("errorFullname").innerHTML = "";
    }
    if (Email == "") {
        document.getElementById("errorEmail").innerHTML = "Email không được để trống";
        status = false;
    }
    else if (ValidateEmail(Email) == false) {
        document.getElementById("errorEmail").innerHTML = "Địa chỉ email sai định dạng";
        status = false;
    }
    else {
        document.getElementById("errorEmail").innerHTML = "";
    }
    if (Dob == "") {
        document.getElementById("errorDOB").innerHTML = "Chưa nhập ngày sinh";
        status = false;
    }
    else {
        document.getElementById("errorDOB").innerHTML = "";
    }
    if (phone == "") {
        document.getElementById("errorPhone").innerHTML = "Chưa nhập số điện thoại";
        status = false;
    }
    else if (ValidatePhoneNumber(phone) == false) {
        document.getElementById("errorPhone").innerHTML = "Số điện thoại sai định dạng";
        status = false;
    }
    else {
        document.getElementById("errorPhone").innerHTML = "";
    }

    return status;
}