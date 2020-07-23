function validateFormCreate() {

    var userName = document.forms["myForm"]["UserName"].value;
    var fullName = document.forms["myForm"]["FullName"].value;
    var DOB = document.forms["myForm"]["DOB"].value;
    var email = document.forms["myForm"]["Email"].value;
    var phoneNumber = document.forms["myForm"]["PhoneNumber"].value;
    var trangThai = true;


    var atpos = email.indexOf("@");
    var dotpos = email.lastIndexOf(".");


    if (userName == "") {
        document.getElementById("userNameValidate").innerHTML = "Input your user name";
        trangThai = false;
    }
    else {
        document.getElementById("userNameValidate").innerHTML = "";
    }
    if (fullName == "") {
        document.getElementById("fullNameValidate").innerHTML = "Input your name";
        trangThai = false;
    }
    else {
        document.getElementById("fullNameValidate").innerHTML = "";
    }
    if (DOB == "") {
        document.getElementById("DOBValidate").innerHTML = "Choose your date of birth";
        trangThai = false;
    }
    else {
        document.getElementById("DOBValidate").innerHTML = "";
    }
    if (email == "") {

        document.getElementById("emailValidate").innerHTML = "Input email";
        trangThai = false;
    }
    else {
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= email.length) {
            document.getElementById("emailValidate").innerHTML = "Email wrong format";
            trangThai = false;
        }
        else {
            document.getElementById("emailValidate").innerHTML = "";
        }
    }
    if (phoneNumber == "") {
        document.getElementById("phoneNumberValidate").innerHTML = "Input your Phone Number";
        trangThai = false;
    }
    else {
        document.getElementById("phoneNumberValidate").innerHTML = "";
    }
    if ($("#Status").text() == 'UserName exist already...' || $("#Status").text() == 'Checking...') {
        trangThai = false;
    }
    return trangThai;
};


function validateFormEdit() {
    var fullName = document.forms["myForm"]["FullName"].value;
    var DOB = document.forms["myForm"]["DOB"].value;
    var email = document.forms["myForm"]["Email"].value;
    var phoneNumber = document.forms["myForm"]["PhoneNumber"].value;
    var trangThai = true;


    var atpos = email.indexOf("@");
    var dotpos = email.lastIndexOf(".");

    if (fullName == "") {
        document.getElementById("fullNameValidate").innerHTML = "Input your name";
        trangThai = false;
    }
    else {
        document.getElementById("fullNameValidate").innerHTML = "";
    }
    if (DOB == "") {
        document.getElementById("DOBValidate").innerHTML = "Choose your date of birth";
        trangThai = false;
    }
    else {
        document.getElementById("DOBValidate").innerHTML = "";
    }
    if (email == "") {

        document.getElementById("emailValidate").innerHTML = "Input email";
        trangThai = false;
    }
    else {
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= email.length) {
            document.getElementById("emailValidate").innerHTML = "Email wrong format";
            trangThai = false;
        }
        else {
            document.getElementById("emailValidate").innerHTML = "";
        }
    }
    if (phoneNumber == "") {
        document.getElementById("phoneNumberValidate").innerHTML = "Input your Phone Number";
        trangThai = false;
    }
    else {
        document.getElementById("phoneNumberValidate").innerHTML = "";
    }

    return trangThai;
};

