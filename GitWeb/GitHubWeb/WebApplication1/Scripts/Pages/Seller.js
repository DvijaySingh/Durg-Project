$(document).ready(function () {

    $("#txtSelldate").datepicker({
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

    var bulkID = $('#hdnSellerID').val();
    $('#hdnProdSellerId').val(bulkID);
    $('#hdnInstallmentSellerID').val(bulkID);

    $(document).delegate('.btnedit', 'click', function () {

        var tr = $(this).closest('tr');
        var Id = $(this).attr('id');
        var productName = $(tr).find('td').eq(0).text().trim();
        var Type = $(tr).find('td').eq(1).text().trim();
        var Approxw = $(tr).find('td').eq(2).text().trim();
        var unit = $(tr).find('td').eq(3).text().trim();
        $('#hdnProductId').val(Id);
        $('#txtProductname').val(productName);
        $('#ddlType option:selected').val(1);
        $('#txtprodWeight').val(Approxw);
        $('#txtUnits').val(unit);

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

