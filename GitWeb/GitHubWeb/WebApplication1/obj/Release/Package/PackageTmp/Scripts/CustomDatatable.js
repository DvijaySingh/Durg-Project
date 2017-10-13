
function init_ModifiedDataTables() {

    if (typeof ($.fn.DataTable) === 'undefined') { return; }

    var handleDataTableButtons = function () {
        if ($("#modified-datatable-buttons").length) {
            $("#modified-datatable-buttons").DataTable({
                dom: "Bfrtip",
                buttons: [
                    {
                        extend: "copyHtml5",
                        className: "btn-sm",
                        title: $('#hdnTitle').val(),
                        text: $('#hdncopy').val(),
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                    {
                        extend: "csv",
                        className: "btn-sm",
                        title: $('#hdnTitle').val(),
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                    {
                        extend: "excel",
                        className: "btn-sm",
                        title: $('#hdnTitle').val(),
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                    {
                        extend: "pdfHtml5",
                        className: "btn-sm",
                        title: $('#hdnTitle').val(),
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                    {
                        extend: "print",
                        className: "btn-sm",
                        title: $('#hdnTitle').val(),
                        text: $('#hdnprint').val(),
                        exportOptions: {
                            columns: [1, 2, 3]
                        }
                    },
                ],

                "columnDefs": [{ "visible": false, "targets": 0 }],
                "oLanguage": {
                    "sInfo": $('#hdndatatablebottominfo').val(),
                    "sEmptyTable": $('#hdnNodataavailable').val(),
                    "sInfoEmpty": $('#hdnEmptyInfo').val(),
                    "sSearch": $('#hdnsearch').val(),
                    "oPaginate": {
                        //"sFirst": $(CustomerList.textvariables.hdntblPageFirst).val(),
                        //"sLast": $(CustomerList.textvariables.hdntblPageLast).val(),
                        "sNext": $('#hdnNext').val(),
                        "sPrevious": $('#hdnPrevious').val()
                    },
                    "buttons": {
                        "copyTitle": $('#hdnTitleCopyToClipboard').val(),
                        "copyKeys": 'Appuyez sur <i>ctrl</i> ou <i>\u2318</i> + <i>C</i> pour copier les données du tableau à votre presse-papiers. <br><br>Pour annuler, cliquez sur ce message ou appuyez sur Echap.',
                        "copySuccess": {
                            _: '%d lignes copiées',
                            1: '1 ligne copiée'
                        }
                    }

                }
            });

        }
    };

    TableManageButtons = function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
            }
        };
    }();

    $('.datatable').dataTable();

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true,
    });

    $('#datatable-fixed-header').DataTable({
        fixedHeader: true,
    });

    var $datatable = $('.datatable-checkbox');

    $datatable.dataTable({
        'order': [[1, 'asc']],
        'columnDefs': [
            { orderable: false, targets: [0] }
        ]
    });
    $datatable.on('draw.dt', function () {
        $('checkbox input').iCheck({
            checkboxClass: 'icheckbox_flat-blue'
        });
    });

    TableManageButtons.init();

};

$(document).ready(function () {
    init_ModifiedDataTables();
});








