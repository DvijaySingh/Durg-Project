$(document).ready(function () {
    var dateFormat = "dd/mm/yyyy",
     from = $("#txtOrderDate")
       .datepicker({
           minDate: new Date(2000, 1 - 1, 01),
           dateFormat: 'dd/mm/yy',
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
         minDate: new Date(2000, 1 - 1, 01),
         dateFormat: 'dd/mm/yy',
         showOtherMonths: true,
         changeYear: true,
         selectOtherMonths: true,
     })
     .on("change", function () {
         from.datepicker("option", "maxDate", getDate(this));
     });

    

    $(document).delegate('.btnedit', 'click', function () {
        debugger;
        var tr = $(this).closest('tr');
        var Id = $(this).attr('id');
        var productName = $(tr).find('td').eq(0).text().trim();
        var Type = $(tr).find('td').eq(1).text().trim();
        var Approxw = $(tr).find('td').eq(2).text().trim();
        var actualw = $(tr).find('td').eq(3).text().trim();
        var qty = $(tr).find('td').eq(4).text().trim();
        var amount = $(tr).find('td').eq(5).text().trim();
        var desc = $(tr).find('td').eq(6).text().trim();
        $('#hdnProductId').val(Id);
        $('#txtProductname').val(productName);
        $('#ddlType option:selected').val(1);
        $('#txtAppxw').val(Approxw);
        $('#txtActualw').val(actualw);
        $('#txtqty').val(qty);
        $('#txtamount').val(amount);
        $('#txtdesc').val(desc);
    });
    RefreshDatatable();
    document.getElementById("defaultOpen").click();

});
function RefreshDatatable() {
    $("#tblCProducts").DataTable({
        paging: true,
        aaSorting: [],
        responsive: true

    });
    $('#tblCProducts_length').remove();
    //$('#tblProducts_filter label').remove();
    $('#tblCProducts_filter input').addClass('form-control');

    $('#hdnProductId').val('');
    $('#txtProductname').val('');
    $('#ddlType option:selected').val(0);
    $('#txtAppxw').val('');
    $('#txtActualw').val('');
    $('#txtqty').val('');
    $('#txtamount').val('');
    $('#txtdesc').val('');
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

