$(document).ready(function () {

    var today = new Date();
    var dd = today.getDate() < 10 ? `0${today.getDate()}` : today.getDate();
    var mm = today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1;
    var yyyy = today.getFullYear();
    const endDate = `${yyyy}-${mm}-${dd}`
    const initDate = `${yyyy}-${mm}-01`

    var start = initDate
    var end = endDate
        
    $('input[name="daterangepicker"]').daterangepicker({
        "locale": {
            "format": "DD/MM/YYYY",
            "separator": " al ",
            "applyLabel": "Aplicar",
            "cancelLabel": "Cancelar",
            "fromLabel": "Desde",
            "toLabel": "Hasta",
            "customRangeLabel": "Custom",
            "weekLabel": "W",
            "daysOfWeek": [
                "Do",
                "Lu",
                "Ma",
                "Mi",
                "Ju",
                "Vi",
                "Sa"
            ],
            "monthNames": [
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Deciembre"
            ]
        },
        endDate: `${dd}-${mm}-${yyyy}`,
        startDate: `01-${mm}-${yyyy}`
    }, function (_start, _end) {
        start = _start.format('YYYY-MM-DD')
        end = _end.format('YYYY-MM-DD')
        console.log("start", start);
    });

})

function consultarInformacion() {
    $.ajax({
        type: "post",
        url: rootUrl("/Usuarios/_obtenerUsuarios"),
        dataType: "html",
        data: convertFormToJSON("#frmFilter"),
        success: function (response) {
            $("#content-table-reportes").html(response);
            initTable();

        },
        error: function (xhr, status, error) {
            console.log("error al consultar la tabla")
            ControlErrores(xhr, status, error);
        }
    });
};


function initTable() {
    $('#tblReporteUsuarios').DataTable({
        "scrollY": "550px",
        "scrollCollapse": true,
        scrollX: true,
        dom: 'Bfrtip',
        buttons: [
            'excel',
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                orientation: 'landscape',
                pageSize: 'A1',
                title: 'Listado de ventas',

            }
        ],
        columnDefs: [
            {
                targets: [2, 4, 5, 6, 7, 8, 16],
                className: 'text-right'
            },
            {
                targets: [15],
                className: 'text-center'
            }
        ],
        language: {
            search: "Buscar",
            lengthMenu: "Tamaño de la lista _MENU_ ",
            info: "Mostrando _END_ de _TOTAL_ registraos",
            infoEmpty: "No hay información.",
            infoFiltered: "(Filtrado de _MAX_ registros en total)",
            zeroRecords: "No se encontraron coincidencias",
            emptyTable: "No hay Registros!",
            paginate: {
                first: "Primera",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultima"
            }
        }
    });
}