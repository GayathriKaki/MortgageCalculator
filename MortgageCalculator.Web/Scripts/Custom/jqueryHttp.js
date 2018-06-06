
var baseUrl = "http://localhost:59608/"; 
var mortgageItems = [];
$(document).ready(function () {
    $("#divRepaymentResult").hide();
    bindMortageData();
});


//Filter mortgage by selected mortgagetype
function filterMortages() {
    var tmpMortgages = mortgageItems;
    var mortgageType = $("#cmbMortgageType").find('option:selected').val();

    $('#cmbMortgages').find('option').remove()

    //Get selected mortage list
    if (mortgageType != -1) {
        $(tmpMortgages).each(function () {
            tmpMortgages = $.grep(tmpMortgages, function (e) {
                return parseInt(e.MortgageType) === parseInt(mortgageType);
            });
        });
    }

    //Bind mortgage list
    $.each(tmpMortgages, function (i, item) {
        $('#cmbMortgages').append($('<option>', {
            value: item.MortageId,
            text: item.InterestRate + " % P.a " + item.Name
        }));
    });
}



//Calculate repayment details
function getRepaymentDetails() {
    var currentMortgage = [];
    var loanAmount = $("#txtLoanAmount").val();
    var loanDuration = $("#textLoanDuration").val();

    if (!loanAmount || loanAmount <= 0) {
        alert("Loan amount should not be null and it should be greater than 0");
        return false;
    }
    else if (!loanDuration || loanDuration <= 0) {
        alert("Loan duration should not be null and it should be greater than 0");
        return false;
    }

    var selectedMortgageId = $("#cmbMortgages").find('option:selected').val();

    $(mortgageItems).each(function () {
        currentMortgage = $.grep(mortgageItems, function (e) {
            return parseInt(e.MortgageId) === parseInt(selectedMortgageId);
        });
    });

    $("#divRepaymentResult").show();

    var interstRate = parseFloat(currentMortgage[0].InterestRate.toString());

    var p = loanAmount; //principle / loan amount borrowed
    var i = (interstRate / 100) / 12; //monthly interest rate
    var n = loanDuration * 12; //number of payments months

    var mortgagePerMonth = p * i * (Math.pow(1 + i, n)) / (Math.pow(1 + i, n) - 1);
    var totalMortgage = Math.round((parseFloat(mortgagePerMonth.toString()) * n));
    var totalInterestPayable = Math.round(totalMortgage - loanAmount);

    //Total Interest charged
    $("#lblTotalInterestCharged").html("$" + totalInterestPayable);

    //Total Loan Repayment
    $("#lblTotalLoanRepayment").html("$" + totalMortgage);

}

//Validate item exist on array
function IsItemExist(jsonObject, id) {
    var result = false;
    $(jsonObject).each(function () {
        jsonObject = $.grep(jsonObject, function (e) {
            return parseInt(e.Id) === parseInt(id);
        });
    });

    if (Object.keys(jsonObject).length > 0)
        result = true;
    return result;
}

//Bind mortage combobox
function bindMortageData() {

    var urlGetMortgageTypes = baseUrl + "/api/Mortgage";
    $.ajax({
        type: "GET",
        url: urlGetMortgageTypes,
        success: function (data) {
            mortgageItems = data;
            var tmpMortageType = [];
            if (data) {
                $('#cmbMortgageType').append($('<option>', {
                    value: -1,
                    text: "All"
                }));
            }
            $.each(data, function (i, item) {

                //bind mortagetype combobox
                if ($.inArray(item.MortgageType, tmpMortageType) < 0) {
                    tmpMortageType.push(item.MortgageType);
                    $('#cmbMortgageType').append($('<option>', {
                        value: item.MortgageType,
                        text: item.MortgageTypeValue
                    }));
                }

                //bind mortage combobox
                $('#cmbMortgages').append($('<option>', {
                    value: item.MortgageId,
                    text: item.InterestRate + " % P.a " + item.Name
                }));
            });


        },
        error: function () {

        }
    });
}



