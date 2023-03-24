var start;
var end;

$(document).ready(function () {

    var today = new Date();
    var dd = today.getDate() < 10 ? `0${today.getDate()}` : today.getDate();
    var mm = today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1;
    var yyyy = today.getFullYear();
    console.log("dd", dd)
    const endDate = `${yyyy}-${mm}-${dd}`
    const initDate = `${yyyy}-${mm}-${dd}`

     start = initDate
     end = endDate
        
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
        startDate: `${dd}-${mm}-${yyyy}`
    }, function (_start, _end) {
        start = _start.format('YYYY-MM-DD')
        end = _end.format('YYYY-MM-DD')
        console.log("start: ", start , " end: ", end);
        });

    consultarInformacion();

    $("#btnFilter").click(function (e) {
        consultarInformacion();
    })

    InitBtnAgregar();

})

function consultarInformacion() {
    $.ajax({
        type: "post",
        url: rootUrl("/Usuarios/_obtenerUsuarios"),
        dataType: "html",
        data: {
            tipoReporte: $("#tipoReporte").val(),
            fechaInicio: start,
            fechaFin  :end
        },
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

        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o" style="font-size:20px;"></i>',
                className: 'btn btn-danger btn-square btn-uppercase',
                titleAttr: 'Exportar a PDF',
                title: "Usuarios",
                //exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:20px;"></i>',
                titleAttr: 'Exportar a Excel',
                className: 'btn btn-success btn-uppercase',
                //exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] }
            },
        ],
        language: {
            search: "Buscar",
            lengthMenu: "Tamaño de la lista _MENU_ ",
            info: "Mostrando _END_ de _TOTAL_ registrados",
            infoEmpty: "No hay información.",
            infoFiltered: "(Filtrado de _MAX_ registros en total)",
            zeroRecords: "No se encontraron coincidencias",
            emptyTable: "No hay registros!",
            paginate: {
                first: "Primera",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultima"
            }
        }
    });
}

function InitBtnAgregar() {
    $('[data-toggle="tooltip"]').tooltip();
    $('#btnAddUser').click(function (e) {
        $('#lblEditUser').html('Agregar Cliente CREA')
        $('#mdEditUser').modal('show');
        $('#configreset').trigger('click')
        $('#btnSaveUser').html('Guardar')
    });
}

function onFailureResultSaveUser(err) {

    console.log("error::::::::", err);
}

function onSuccessResultSaveUser(response) {

    $('#mdEditUser').modal('hide');
    toastr.info(response.error_message);
    consultarInformacion();
}