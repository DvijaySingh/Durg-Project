$(document).ready(function () {

    $("#txtbuyDate,#txtinsdate,#txtbillDate,#txtFinalDate").datepicker({
        showOtherMonths: true,
        changeYear: true,
        selectOtherMonths: true,
    });
    $("#fileupload").change(function () {
        $("#dvPreview").html("");
        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
        if (regex.test($(this).val().toLowerCase())) {
            $("#dvPreview").show();
            $("#dvPreview").append("<img />");
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#dvPreview img").attr("src", e.target.result);
            }
            reader.readAsDataURL($(this)[0].files[0]);
        } else {
            alert("Please upload a valid image file.");
        }
    });
    $("#frmcreatebuyer").validate({

        // Specify the validation rules
        rules: {
            BuyerName: "required",
            Address: "required",
            DepositeAmount: "required",
            BuyDate: "required",
        },

        // Specify the validation error messages
        messages: {
            BuyerName: "Please enter service name",
            Address: "Please enter Address.",
            DepositeAmount:  "Please provide amount.",
            BuyDate: "Please enter buy date.",
             
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    var bulkID = $('#buyerID').val();
    $('#hdnProductBuyerId').val(bulkID);
    $('#hdnInstallmentBuyerID').val(bulkID);
    ProductAutoFill();
    CustomerAutoFill();
    $(document).delegate('.btnedit', 'click', function () {
        debugger;
        var tr = $(this).closest('tr');
        var Id = $(this).attr('id');
        var productName = $(tr).find('td').eq(0).text().trim();
        var Type = $(tr).find('td').eq(1).text().trim();
        var Approxw = $(tr).find('td').eq(2).text().trim();
        var unit = $(tr).find('td').eq(3).text().trim();
        var rate = $(tr).find('td').eq(4).text().trim();
         
        $('#hdnProductId').val(Id);
        $('#txtProductname').val(productName);
        $('#ddlType').val(Type);
        $('#txtprodWeight').val(Approxw);
        $('#txtunit').val(unit);
        $('#txtprodRate').val(rate);

    });
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
    $('#ddlType').val(0);
    $('#txtprodWeight').val('');
    $('#txtunit').val('');
    $('#txtprodRate').val('');
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

function ProductAutoFill() {
    $.ajax({
        url: "/Buyers/GetAllProducts",
        cache: false,
        type: "GET",
        data: {},
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var lstdata = data;
            $("#txtProductname").autocomplete({
                minLength: 0,
                open: function () {
                    $('.ui-menu')
                        .width($('#txtProductname').width() + 22);
                },
                source: lstdata,
                innerHeight: "200",
                select: function (event, ui) {
                    event.preventDefault();
                    var labelValue = ui.item.label;
                    if (labelValue.indexOf('(') > 0) {
                        var codeAndText = labelValue.split('(');
                        $('#txtProductname').val(codeAndText[0]);
                        var categoryAndType = (codeAndText[1].slice(0, -1)).split('#');
                        $('#ddlCategory').val(categoryAndType[0]);
                        $("#ddlType").val(categoryAndType[1]).focus();
                    }
                    else {
                        $("#txtProductname").val(labelValue);
                    }
                },
                click: function (event, ui) {
                    try {
                        event.preventDefault();
                        $("#txtProductname").val(ui.item.label);
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

function CustomerAutoFill() {
    $.ajax({
        url: "/Buyers/GetAllCustomers",
        cache: false,
        type: "GET",
        data: {},
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var lstdata = data;
            $("#txtbuyerName").autocomplete({
                minLength: 1,
                open: function () {
                    $('.ui-menu')
                        .width($('#txtbuyerName').width() + 22);
                },
                source: lstdata,
                innerHeight: "200",
                select: function (event, ui) {
                    event.preventDefault();
                    var labelValue = ui.item.label;
                    if (labelValue.indexOf('(') > 0) {
                        var codeAndText = labelValue.split('(');
                        $('#txtbuyerName').val(codeAndText[0]);
                        var categoryAndType = (codeAndText[1].slice(0, -1)).split('#');;
                        $('#txtbuyerAdd').val(categoryAndType[1]);
                        $('#txtcustCode').val(categoryAndType[0]);
                    }
                    else {
                        $("#txtbuyerName").val(labelValue);
                    }
                },
                click: function (event, ui) {
                    try {
                        event.preventDefault();
                        $("#txtbuyerName").val(ui.item.label);
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
            $("#txtcustCode").autocomplete({
                minLength: 1,
                open: function () {
                    $('.ui-menu')
                        .width($('#txtcustCode').width() + 22);
                },
                source: lstdata,
                innerHeight: "200",
                select: function (event, ui) {
                    event.preventDefault();
                    var labelValue = ui.item.label;
                    if (labelValue.indexOf('(') > 0) {
                        var codeAndText = labelValue.split('(');
                        $('#txtbuyerName').val(codeAndText[0]);
                        var categoryAndType = (codeAndText[1].slice(0, -1)).split('#');;
                        $('#txtbuyerAdd').val(categoryAndType[1]);
                        $('#txtcustCode').val(categoryAndType[0]);
                    }
                    else {
                        $("#txtcustCode").val(labelValue);
                    }
                },
                click: function (event, ui) {
                    try {
                        event.preventDefault();
                        $("#txtcustCode").val(ui.item.label);
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

function RefreshBuyerGrid() {
    $("#tblbuyers").DataTable({
        paging: true,
        aaSorting: [],
        responsive: true
    });
    $('#tblbuyers_length').remove();
    $('#tblbuyers_filter input').addClass('form-control');
}


