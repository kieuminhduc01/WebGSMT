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
        document.getElementById("errorFullname").innerHTML = "You need to input FullName";
        status = false;
    }
    else {
        document.getElementById("errorFullname").innerHTML = "";
    }
    if (Email == "") {
        document.getElementById("errorEmail").innerHTML = "You need to input email";
        status = false;
    }
    else if (ValidateEmail(Email) == false) {
        document.getElementById("errorEmail").innerHTML = "You have entered an invalid email address!";
        status = false;
    }
    else {
        document.getElementById("errorEmail").innerHTML = "";
    }
    if (Dob == "") {
        document.getElementById("errorDOB").innerHTML = "You need to input DOB";
        status = false;
    }
    else {
        document.getElementById("errorDOB").innerHTML = "";
    }
    if (phone == "") {
        document.getElementById("errorPhone").innerHTML = "You need to input phone number";
        status = false;
    }
    else if (ValidatePhoneNumber(phone) == false) {
        document.getElementById("errorPhone").innerHTML = "You have entered an invalid phone";
        status = false;
    }
    else {
        document.getElementById("errorPhone").innerHTML = "";
    }

    return status;
}