$(document).ready(function () {

    var today = new Date();
    var dd = today.getDate() < 10 ? `0${today.getDate()}` : today.getDate();
    var mm = today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1;
    var yyyy = today.getFullYear();
    const endDate = `${yyyy}-${mm}-${dd}`
    const initDate = `${yyyy}-${mm}-01`

    var start = initDate
    var end = endDate

    //Carga tabla por defecto
    $.ajax({
        type: "GET",
        url: "/Sales/Filtered",
        dataType: "HTML",
        data: {
            initDate,
            endDate
        },
        success: function (response) {
            $('#table_container').html(response)
            initTable()
        }
    });

    //Establece filtros por defecto
    $.ajax({
        type: "GET",
        url: "/Sales/BranchOfficeChange",
        dataType: "HTML",
        success: function (response) {
            $('#filters').html(response)

            //$('#selectedCity').change(() => {
            //    $.ajax({
            //        type: "GET",
            //        url: "/Sales/BranchOfficeChange",
            //        dataType: "HTML",
            //        data: {
            //            Database: $("#selectedDB").val(),
            //            City: $("#selectedCity").val()
            //        },
            //        success: function (response) {
            //            $('#filters').html(response)

            //            $('#selectedCity').change(() => {
            //                $.ajax({
            //                    type: "GET",
            //                    url: "/Sales/BranchOfficeChange",
            //                    dataType: "HTML",
            //                    data: {
            //                        Database: $("#selectedDB").val(),
            //                        City: $("#selectedCity").val()
            //                    },
            //                    success: function (response) {
            //                        $('#filters').html(response)
            //                    }
            //                });
            //            })

            //        }
            //    });
            //})
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

    $('#filter').click(() => {
        $('#table_container').html(`<div class="col-12 m-t-10" id="table_container">
                            <div class="d-flex align-items-center">
                                <strong>Cargando...</strong>
                                <div class="spinner-border ml-auto black-color" role="status" aria-hidden="true"></div>
                            </div>
                        </div>`)
        $.ajax({
            type: "GET",
            url: "/Sales/Filtered",
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
            }
        });
    })

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
            url: "/Sales/Filtered",
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
            columnDefs: [
                {
                    targets: [2, 4, 5, 6, 7, 8, 16],
                    className : 'text-right'
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
});

function reload() {
    $.ajax({
        type: "GET",
        url: "/Sales/BranchOfficeChange",
        dataType: "HTML",
        data: {
            Database: $("#selectedDB").val(),
            City: $("#selectedCity").val(),
            User: $("#selectedUser").val()
        },
        success: function (response) {
            $('#filters').html(response)
        }
    });
}