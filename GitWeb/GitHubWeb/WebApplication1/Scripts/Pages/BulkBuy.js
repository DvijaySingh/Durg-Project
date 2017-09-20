$(document).ready(function () {
    var dateFormat = "dd/mm/yyyy",
     from = $("#txtOrderDate")
       .datepicker({
           
           showOtherMonths: true,
           changeYear: true,
           selectOtherMonths: true,
           beforeShow: function (textbox, instance) {
               $('#ui-datepicker-div').css({
                   position: 'absolute',
                   top: -20,
                   left: 5
               });
               $('#testingContainer').append($('#ui-datepicker-div'));
               $('#ui-datepicker-div').hide();
           }
       })
       .on("change", function () {
           to.datepicker("option", "minDate", getDate(this) == null ? new Date(2000, 1 - 1, 01) : getDate(this));
       }),
     to = $("#txtDelivaryDate").datepicker({
          
         showOtherMonths: true,
         changeYear: true,
         selectOtherMonths: true,
     })
     .on("change", function () {
         from.datepicker("option", "maxDate", getDate(this));
     });
    StartDate = $("#txtstartDate")
       .datepicker({
           minDate: new Date(2000, 1 - 1, 01),
           //dateFormat: 'dd/mm/yy',
           showOtherMonths: true,
           changeYear: true,
           selectOtherMonths: true,
           beforeShow: function (textbox, instance) {
               $('#ui-datepicker-div').css({
                   position: 'absolute',
                   top: -20,
                   left: 5
               });
               $('#testingContainer').append($('#ui-datepicker-div'));
               $('#ui-datepicker-div').hide();
           }
       })
       .on("change", function () {
           enddate.datepicker("option", "minDate", getDate(this) == null ? new Date(2000, 1 - 1, 01) : getDate(this));
       }),
     enddate = $("#txtendDate").datepicker({
         minDate: new Date(2000, 1 - 1, 01),
         //dateFormat: 'dd/mm/yy',
         showOtherMonths: true,
         changeYear: true,
         selectOtherMonths: true,
     })
     .on("change", function () {
         StartDate.datepicker("option", "maxDate", getDate(this));
     });
    $("#txtinsdate").datepicker({
        minDate: new Date(2000, 1 - 1, 01),
        //dateFormat: 'dd/mm/yy',
        showOtherMonths: true,
        changeYear: true,
        selectOtherMonths: true,
    })


    var bulkID = $('#BulkBuyID').val();
    $('#hdnproductBulkBuyID').val(bulkID);
    $('#hdnvendorBulkBuyID').val(bulkID);
    $('#hdnInstallBulkBuyID').val(bulkID);

    $(document).delegate('.btnedit', 'click', function () {
      
        var tr = $(this).closest('tr');
        var Id = $(this).attr('id');
        var productName = $(tr).find('td').eq(0).text().trim();
        var Type = $(tr).find('td').eq(1).text().trim();
        var Approxw = $(tr).find('td').eq(2).text().trim();
        $('#hdnProductId').val(Id);
        $('#txtProductname').val(productName);
        $('#ddlType option:selected').val(1);
        $('#txtprodWeight').val(Approxw);
       
    });
    $(document).delegate('.btnvendoredit', 'click', function () {
        var tr = $(this).closest('tr');
        var Id = $(this).attr('id');
        var  Name = $(tr).find('td').eq(0).text().trim();
        var amountTaken = $(tr).find('td').eq(1).text().trim();
        var rate = $(tr).find('td').eq(2).text().trim();
        var startDate = $(tr).find('td').eq(3).text().trim();
        var ClosingDate = $(tr).find('td').eq(4).text().trim();
       
        var RetAmount = $(tr).find('td').eq(5).text().trim();
        $('#hdnvendorId').val(Id);
        $('#txtVendorName').val(Name);
        $('#txtamountTaken').val(amountTaken);
        $('#txtstartDate').val(startDate);
        $('#txtendDate').val(ClosingDate);
        $('#txtrate').val(rate);
        $('#txtReturnAmount').val(RetAmount);
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
    RefreshVendorDatatable();
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
}

function RefreshVendorDatatable() {
    $("#tblVendors").DataTable({
        paging: true,
        aaSorting: [],
        responsive: true

    });
    $('#tblVendors_length').remove();
    //$('#tblProducts_filter label').remove();
    $('#tblVendors_filter input').addClass('form-control');

    $('#hdnvendorId').val('');
    $('#txtVendorName').val('');
    $('#txtamountTaken').val('');
    $('#txtstartDate').val('');
    $('#txtendDate').val('');
    $('#txtrate').val('');
    $('#txtReturnAmount').val('');
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

