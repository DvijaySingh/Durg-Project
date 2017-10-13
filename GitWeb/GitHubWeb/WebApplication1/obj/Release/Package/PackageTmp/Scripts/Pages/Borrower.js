$(document).ready(function () {

    $("#txtdate").datepicker({
        minDate: new Date(2000, 1 - 1, 01),
        //dateFormat: 'dd/mm/yy',
        showOtherMonths: true,
        changeYear: true,
        selectOtherMonths: true,
    })
    $("#txtinsdate").datepicker({
        minDate: new Date(2000, 1 - 1, 01),
        //dateFormat: 'dd/mm/yy',
        showOtherMonths: true,
        changeYear: true,
        selectOtherMonths: true,
    })

    var BorrowerID = $('#hdnBorrowerID').val();
    $('#hdnInsBorrowID').val(BorrowerID);
    CustomerAutoFill();
  
    $(document).delegate('.btnInstallmetedit', 'click', function () {
        var tr = $(this).closest('tr');
        var Id = $(this).attr('id');
        var Amount = $(tr).find('td').eq(0).text().trim();
        var date = $(tr).find('td').eq(1).text().trim();
        $('#hdnInstallment').val(Id);
        $('#txtinsAmount').val(Amount);
        $('#txtinsdate').val(date);
    });
    RefreshProductDataTable();
    RefreshInstallmentDatatable();
    document.getElementById("defaultOpen").click();

});
function RefreshProductDataTable() {
    $("#tblbulkProducts").DataTable({
        paging: true,
        aaSorting: [],
        responsive: true

    });
    $('#tblbulkProducts_length').remove();
    //$('#tblProducts_filter label').remove();
    $('#tblbulkProducts_filter input').addClass('form-control');

    $('#hdnProductId').val('');
    $('#txtProductname').val('');
    $('#ddlType option:selected').val(0);
    $('#txtprodWeight').val('');
    $('#txtunit').val('');
}


function RefreshInstallmentDatatable() {
    $("#tblinstallment").DataTable({
        paging: true,
        aaSorting: [],
        responsive: true

    });
    $('#tblinstallment_length').remove();
    //$('#tblProducts_filter label').remove();
    $('#tblinstallment_filter input').addClass('form-control');
    $('#hdnInstallment').val('');
    $('#txtinsAmount').val('');
    $('#txtinsdate').val('');
}
function getDate(element) {
    var date;
    try {
        date = $.datepicker.parseDate(dateFormat, element.value);
    } catch (error) {
        date = null;
    }

    return date;
}

function FormatCurrentDate(dt) {
    var month = dt.getMonth() + 1;
    var day = dt.getDate();
    var year = dt.getFullYear();

    return formattedDate = month + "/" + day + "/" + year;
}

function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
function CallProductInfo() {
    document.getElementById("productInfo").click();
}

function CustomerAutoFill() {
    $.ajax({
        url: "/Buyers/GetAllCustomers",
        cache: false,
        type: "GET",
        data: {},
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var lstdata = data;
            $("#txtName").autocomplete({
                minLength: 1,
                open: function () {
                    $('.ui-menu')
                        .width($('#txtName').width() + 22);
                },
                source: lstdata,
                innerHeight: "200",
                select: function (event, ui) {
                    event.preventDefault();
                    var labelValue = ui.item.label;
                    if (labelValue.indexOf('(') > 0) {
                        var codeAndText = labelValue.split('(');
                        $('#txtName').val(codeAndText[0]);
                        var categoryAndType = (codeAndText[1].slice(0, -1)).split('#');;
                        $('#txtAddress').val(categoryAndType[1]);
                        $('#txtCustCode').val(categoryAndType[0]);
                    }
                    else {
                        $("#txtName").val(labelValue);
                    }
                },
                click: function (event, ui) {
                    try {
                        event.preventDefault();
                        $("#txtName").val(ui.item.label);
                    }
                    catch (e) { }
                },
            }).focus(function () {
                try {
                    $(this).autocomplete("search", "");
                }
                catch (e) { }
            });
            // for cust code auto complete
            $("#txtCustCode").autocomplete({
                minLength: 1,
                open: function () {
                    $('.ui-menu')
                        .width($('#txtCustCode').width() + 22);
                },
                source: lstdata,
                innerHeight: "200",
                select: function (event, ui) {
                    event.preventDefault();
                    var labelValue = ui.item.label;
                    if (labelValue.indexOf('(') > 0) {
                        var codeAndText = labelValue.split('(');
                        $('#txtName').val(codeAndText[0]);
                        var categoryAndType = (codeAndText[1].slice(0, -1)).split('#');;
                        $('#txtAddress').val(categoryAndType[1]);
                        $('#txtCustCode').val(categoryAndType[0]);
                    }
                    else {
                        $("#txtCustCode").val(labelValue);
                    }
                },
                click: function (event, ui) {
                    try {
                        event.preventDefault();
                        $("#txtCustCode").val(ui.item.label);
                    }
                    catch (e) { }
                },
            }).focus(function () {
                try {
                    $(this).autocomplete("search", "");
                }
                catch (e) { }
            });
        },
        error: function () {
        }
    });

}

function RefreshBorrowersGrid() {
    $("#tblborrower").DataTable({
        paging: true,
        aaSorting: [],
        responsive: true
    });
    $('#tblborrower_length').remove();
    $('#tblborrower_filter input').addClass('form-control');
}

