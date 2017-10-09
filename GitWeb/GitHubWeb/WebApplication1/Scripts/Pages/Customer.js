$(document).ready(function () {
    $('#btnCreateCust').click(function () {
        $('#AddCustomers').submit();
    });

    $("#AddCustomers").on("submit", function (event) {
        debugger;
        event.preventDefault();
        // Show the "busy" Gif:
        openModal();
        var url = $(this).attr("action");
        var formData = $(this).serialize();
        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            success: function (resp) {
                // Hide the "busy" gif:
                closeModal();
            },
            error: function (resp) {
               // alert(resp);
                closeModal();
            }
        })
    })

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
});