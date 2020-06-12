$(document).ready(function () {

    var today = new Date();
    var dd = today.getDate() < 10 ? `0${today.getDate()}` : today.getDate();
    var mm = today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1;
    var yyyy = today.getFullYear();
    const endDate = `${yyyy}-${mm}-${dd}`
    const initDate = `${yyyy}-${mm}-01}`


    $('input[name="daterangepicker"]').daterangepicker({ endDate: `${mm}-${dd}-${yyyy}`, startDate: `${mm}-01-${yyyy}` }, function (start, end) {
        $('#table_container').html("Cargando...")
        $.ajax({
            type: "GET",
            url: "/Sales/Filtered",
            dataType: "HTML",
            data: {
                initDate: start.format('YYYY-MM-DD'),
                endDate: end.format('YYYY-MM-DD')
            },
            success: function (response) {
                $('#table_container').html(response)
                initTable()
            }
        });
    });

    $('#clear').click(() => {
        $('#date_picker').data('daterangepicker').setStartDate(`${mm}-01-${yyyy}`);
        $('#date_picker').data('daterangepicker').setEndDate(`${mm}-${dd}-${yyyy}`);
        $('#table_container').html("Cargando...")
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
            responsive: true,
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
