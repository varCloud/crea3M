$(document).ready(function () {

    var today = new Date();
    var dd = today.getDate() < 10 ? `0${today.getDate()}` : today.getDate();
    var mm = today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1;
    var yyyy = today.getFullYear();
    const endDate = `${yyyy}-${mm}-${dd}`
    const initDate = `${yyyy}-${mm}-01`

    var start = initDate
    var end = endDate

    $.ajax({
        type: "GET",
        url: "/BillsReceivable/Filtered",
        dataType: "HTML",
        data: {
            initDate,
            endDate
        },
        success: function (response) {
            $('#table_container').html(response)
            $('#History').html(`<h5>Selecciona una cuenta en la lista de cuentas.</h5>`)
            initTable()
        }
    });

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
    });

    $.ajax({
        type: "GET",
        url: "/Sales/BranchOfficeChange",
        dataType: "HTML",
        data: {
            idClient: 0   
        },
        success: function (response) {
            $('#filters').html(response)
        }
    });

    $('#selectedDB').change(() => {
        $.ajax({
            type: "GET",
            url: "/Sales/BranchOfficeChange",
            dataType: "HTML",
            data: {
                Database: $("#selectedDB").val(),
                City: $("#selectedCity").val()
            },
            success: function (response) {
                $('#filters').html(response)
            }
        });
    })

    $('#filter').click(() => {
        $('#History').html(`<h5>Selecciona una cuenta en la lista de cuentas.</h5>`)
        $('#table_container').html(`<div class="col-12 m-t-10" id="table_container">
                            <div class="d-flex align-items-center">
                                <strong>Cargando...</strong>
                                <div class="spinner-border ml-auto black-color" role="status" aria-hidden="true"></div>
                            </div>
                        </div>`)
        $.ajax({
            type: "GET",
            url: "/BillsReceivable/Filtered",
            dataType: "HTML",
            data: {
                initDate: start,
                endDate: end,
                selectedDB: $("#selectedDB").val(),
                User: $("#selectedUser").val(),
                Client: $("#selectedClient").val()
            },
            success: function (response) {
                $('#table_container').html(response)
                initTable()
            },
        });
    })

    $('#clear').click(() => {
        $('#date_picker').data('daterangepicker').setStartDate(`$01-${mm}-${yyyy}`);
        $('#date_picker').data('daterangepicker').setEndDate(`${dd}-${mm}-${yyyy}`);
        $('#table_container').html(`<div class="col-12" id="table_container">
                            <div class="d-flex align-items-center">
                                <strong>Cargando...</strong>
                                <div class="spinner-border ml-auto black-color" role="status" aria-hidden="true"></div>
                            </div>
                        </div>`)
        $.ajax({
            type: "GET",
            url: "/BillsReceivable/Filtered",
            dataType: "HTML",
            data: {
                initDate: initDate,
                endDate: endDate
            },
            success: function (response) {
                $('#table_container').html(response)
                initTable()
            }
        });
    })

    initTable()

    function initTable() {
        $('#myTable').DataTable({
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
                    pageSize: 'A2',
                    title: 'Cuentas por cobrar',
                    exportOptions: {
                        columns: [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18]
                    }

                }
            ],
            columnDefs: [
                {
                    targets: [5, 6, 7],
                    className : 'text-right'
                }
            ],
            language: {
                search: "Buscar",
                lengthMenu: "Tamaño de la lista _MENU_ ",
                info: "Mostrando _END_ de _TOTAL_ registros",
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
});

function reload() {
    $.ajax({
        type: "GET",
        url: "/Sales/BranchOfficeChange",
        dataType: "HTML",
        data: {
            Database: $("#selectedDB").val(),
            City: $("#selectedCity").val(),
            User: $("#selectedUser").val(),
            idClient: 1
        },
        success: function (response) {
            $('#filters').html(response)
        }
    });
}

function getHistory(idcliente) {
    $('#History').html(`<div class="col-12 m-t-10" id="table_container">
                            <div class="d-flex align-items-center">
                                <strong>Cargando...</strong>
                                <div class="spinner-border ml-auto black-color" role="status" aria-hidden="true"></div>
                            </div>
                        </div>`)
    $.ajax({
        type: "GET",
        url: "/BillsReceivable/PaymentHistory",
        dataType: "HTML",
        data: {
            Database: $("#selectedDB").val(),
            idClient: idcliente
        },
        success: function (response) {
            $('#History').html(response)
            initTableHistory()
        }
    });
}

function initTableHistory() {
    $('#myTableHistory').DataTable({
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
                pageSize: 'A3',
                title: 'Listado de Cobros'

            }
        ],
        columnDefs: [
            {
                targets: [7, 8],
                className: 'text-right'
            }
        ],
        language: {
            search: "Buscar",
            lengthMenu: "Tamaño de la lista _MENU_ ",
            info: "Mostrando _END_ de _TOTAL_ registros",
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