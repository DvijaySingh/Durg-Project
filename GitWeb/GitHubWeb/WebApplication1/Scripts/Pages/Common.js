$(document).ready(function () {

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