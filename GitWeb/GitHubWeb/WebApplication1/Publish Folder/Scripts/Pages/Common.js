$(document).ready(function () {

    

});

function openModal() {

    $('#modal').show();
    $('#fade').show();

    $('body').css({
        'overflow': 'hidden',
        'height': '100%'
    });
}

function closeModal() {

    $('#modal').hide();
    $('#fade').hide();
    $('body').css({
        'overflow': 'auto',
        'height': 'auto'
    });
}
function CalculateInterst() {
    openModal();
    $.ajax({
        url: "./Home/CalculateInterst",
        type: "get",
        success: function (resp) {
            // Hide the "busy" gif:
            closeModal();
        },
        error: function (resp) {
            // alert(resp);
            closeModal();
        }
    })
}