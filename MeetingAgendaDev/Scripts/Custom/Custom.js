function ValidateEmail(inputText, msg) {
    if (inputText.value != "") {
        var mailformat = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        if (!inputText.value.match(mailformat)) {
            if (msg != "") {
                /*alert(msg);*/
            }
            inputText.focus();
            return false;
        }
    }
    return true;
}

function ValidateZipCode(inputText, msg) {
    if (inputText.value != "") {
        var ZipUSFormat = /^([0-9]{5})(?:[-\s]*([0-9]{4}))?$/;
        var ZipCAFormat = /^([A-Z][0-9][A-Z])\s*([0-9][A-Z][0-9])$/;
        if (inputText.value.match(ZipUSFormat)) {
            return true;
        }
        else if (inputText.value.match(ZipCAFormat)) {
            return true;
        }
        else {
            /*alert(msg);*/
            inputText.focus();
            return false;
        }
    }
    return true;
}

//Checkbox list single check
function MutExChkList(chk) {
    var chkList = chk.parentNode.parentNode.parentNode;
    var chks = chkList.getElementsByTagName("input");
    for (var i = 0; i < chks.length; i++) {
        if (chks[i] != chk && chk.checked) {
            chks[i].checked = false;
        }
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}



function ValidatePhoneFaxNumber(inputText, msg) {
    var strPhoneNumber = inputText.value.trim();
    // For button click event start
    if (strPhoneNumber.length == 8) {
        strPhoneNumber = strPhoneNumber.substring(0, 3) + strPhoneNumber.substring(4, 8);
    }
    else if (strPhoneNumber.length == 12) {
        strPhoneNumber = strPhoneNumber.substring(0, 3) + strPhoneNumber.substring(4, 7) + strPhoneNumber.substring(8, 12);
    }
    //End

    if (strPhoneNumber != "") {
        if (!strPhoneNumber.match(/^\d+$/)) {
            /*alert(msg);*/
            inputText.focus();
            return false;
        }
        else {
            if (strPhoneNumber.length == 7) {
                strPhoneNumber = strPhoneNumber.substring(0, 3) + "-" + strPhoneNumber.substring(3, 7);
                inputText.value = strPhoneNumber;
                return true;
            }
            else if (strPhoneNumber.length == 10) {
                strPhoneNumber = strPhoneNumber.substring(0, 3) + "-" + strPhoneNumber.substring(3, 6) + "-" + strPhoneNumber.substring(6, 10);
                inputText.value = strPhoneNumber;
                return true;
            }
            else {
                /*alert(msg);*/
                inputText.focus();
                return false;
            }
        }
    }
    return true;
}

function mngPhoneFaxNumber(inputText) {
    var strPhoneNumber = inputText.value.trim();
    if (strPhoneNumber != "") {
        if (strPhoneNumber.length == 8) {
            strPhoneNumber = strPhoneNumber.substring(0, 3) + strPhoneNumber.substring(4, 8);
            inputText.value = strPhoneNumber;
        }
        else if (strPhoneNumber.length == 12) {
            strPhoneNumber = strPhoneNumber.substring(0, 3) + strPhoneNumber.substring(4, 7) + strPhoneNumber.substring(8, 12);
            inputText.value = strPhoneNumber;
        }
    }
}


function ValidateMoney(inputText, msg) {
    var strMoney = inputText.value.trim();
    var MoneyLength = strMoney.length;
    var strOutput = "";
    //For check button click event start
    strMoney = strMoney.replace(".", "");
    strMoney = strMoney.replace(",", "");
    strMoney = strMoney.replace("$", "");
    //End
    if (strMoney != "") {
        var strMoneyZero = strMoney.replace(/0/g, "");
        if (strMoneyZero == "") {
            /*alert(msg);*/
            inputText.focus();
            return false;
        }
        if (!strMoney.match(/^\d+$/)) {
            /*alert(msg);*/
            inputText.focus();
            return false;
        }
        else {
            strCents = "." + strMoney.substring(MoneyLength - 2, MoneyLength);
            strDoller = strMoney.substring(0, MoneyLength - 2)
            for (var i = MoneyLength - 2; i > 0; i = i - 3) {
                strOutput = strDoller.substring(i - 3, i) + "," + strOutput;
            }
            inputText.value = "$ " + strOutput.substring(0, strOutput.length - 1) + strCents;
            return true;
        }
    }
    return true;
}

function mngMoney(inputText) {
    var strMoney = inputText.value.trim();
    var strMoneyZero = strMoney.replace(/0/g, "");
    if (strMoneyZero != "") {
        inputText.value = strMoney.substring(2, strMoney.length).replace(",", "").replace(".", "");
    }
}


function isMoneyKey(inputText, msg) {
    if (inputText.value.trim() != "") {
        var moneyformat = /^\$?\d+(,\d{3})*\.?[0-9]?[0-9]?$/;
        if (!inputText.value.match(moneyformat)) {
            if (msg != "") {
                /*alert(msg);*/
            }
            inputText.focus();
            return false;
        }
        else {
            var strMoney = inputText.value.trim();
            var strOutput = "";
            let result = strMoney.includes(".");
            //For check button click event start
            //strMoney = strMoney.replace(".", "");
            strMoney = strMoney.replace(",", "");
            strMoney = strMoney.replace("$", "");

            var MoneyLength = strMoney.length;
            var strCents;
            var strDoller;
            var LastIndex = strMoney.lastIndexOf(".");
            if (result) {
                strCents = strMoney.substring(LastIndex, MoneyLength);
                strDoller = strMoney.substring(0, LastIndex)
                MoneyLength = MoneyLength - 3;
            }
            else {
                strCents = ".00";
                strDoller = strMoney;
            }
            for (var i = MoneyLength; i > 0; i = i - 3) {
                strOutput = strDoller.substring(i - 3, i) + "," + strOutput;
            }
            inputText.value = "$" + strOutput.substring(0, strOutput.length - 1) + strCents;
            return true;
        }
    }
    return true;
}

function isPercentageKey(inputText, msg) {
    if (inputText.value.trim() != "") {
        var percentageformat = /^(100|[1-9]([0-9])?|0)(\.[0-9]{1,2})?\%?$/;
        if (!inputText.value.match(percentageformat) || inputText.value > 100) {
            if (msg != "") {
                /*alert(msg);*/
            }
            inputText.focus();
            return false;
        }
        else {
            var strPercentage = inputText.value.trim();
            var strOutput = "";
            let result = strPercentage.includes(".");
            //For check button click event start
            //strPercentage = strPercentage.replace(".", "");
            strPercentage = strPercentage.replace("%", "");

            var PercentageLength = strPercentage.length;

            if (result) {
                strFraction = "";
                strWhole = strPercentage;
            }
            else {
                strFraction = ".00";
                strWhole = strPercentage;
            }

            inputText.value = strWhole + strFraction + "%";
            return true;
        }
    }
    return true;
}

