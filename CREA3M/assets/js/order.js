﻿function Busqueda() {
    this.startDate,
    this.endDate,
    this.tipoBusqueda
}

$(document).ready(function () {
    

    $('input[name="daterangepicker"]').daterangepicker(
        {
            timePicker: false,
            startDate: moment().startOf('day'),
            endDate: moment().startOf('day'),
            locale: {
                format: 'YYYY-MM-DD',
                lang: 'es'
            }
        }
    );

    $('#startDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        timePicker: false,
        startDate: moment().startOf('day'),
        endDate: moment().startOf('day'),
        locale: {
            format: 'YYYY-MM-DD',
            lang: 'es'
        }
    });

    $('#endDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        timePicker: false,
        startDate: moment().startOf('day'),
        endDate: moment().startOf('day'),
        locale: {
            format: 'YYYY-MM-DD',
            lang: 'es'
        }
    });

   
    consultarOrderList();
});

function initTable() {
    console.log("init table tblOrders");
    $('#tblOrders').DataTable({
        "scrollY": "550px",
        "scrollCollapse": true,


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

function fechaCorrecta(fecha1, fecha2) {

    //Split de las fechas recibidas para separarlas
    var x = fecha1.split("-");
    var z = fecha2.split("-");

    //Cambiamos el orden al formato americano, de esto dd/mm/yyyy a esto mm/dd/yyyy
    fecha1 = x[1] + "-" + x[2] + "-" + x[0];
    fecha2 = z[1] + "-" + z[2] + "-" + z[0];

    //Comparamos las fechas
    if (Date.parse(fecha1) > Date.parse(fecha2)) {
        return false;
    } else {
        return true;
    }
}

function consultarOrderList() {

    busqueda = new Busqueda();

    busqueda.tipoBusqueda = $('#tipoBusqueda').val();

    busqueda.startDate = $('#startDate').val();

    busqueda.endDate = $('#endDate').val();

    if (busqueda.tipoBusqueda == "-1") {
        $("#mensaje-estatus").fadeIn();
        return false;
    }
    else {
        $("#mensaje-estatus").fadeOut();
        if (!fechaCorrecta(busqueda.startDate, busqueda.endDate)) {
            $("#mensaje-startDate").fadeIn();
            return false;
        } else{
            $("#mensaje-startDate").fadeOut();
        }
    }

    $.ajax({
        type: "post",
        url: rootUrl("/Orders/_orders"),
        dataType: "html",
        data: {
            busqueda: busqueda
        },
        success: function (response) {

            $("#table-orders").html(response);
            initTable();

        },
        error: function (xhr, status, error) {
            alert("Error en la carga de categoria")
            ControlErrores(xhr, status, error);
        }
    });
    

}

$('#mdEditStatus').on('show.bs.modal', function (event) {
    
    var button = $(event.relatedTarget);
    
    var orden = button.data('idorden');
    var idStatus = button.data('idstatus');

    $('#status').val(idStatus);
    $('#idUsuario').val(orden);
    
});

$('#mdEditGuia').on('show.bs.modal', function (event) {

    var button = $(event.relatedTarget);

    var guia = button.data('guia');
    $('#m_guia').val(guia);

    var orden = button.data('orden');
    $('#idOrden').val(orden);
    
});


$('#editGuia').click(function () {

    var guia = $('#m_guia').val();
    var orden = $('#idOrden').val();
    $.ajax({
        type: "POST",
        url: rootUrl("/Orders/EditarGuia"),
        data: { guia: guia, orden: orden},
        dataType: "Json",
        async: true,
        success: function () {
            window.location = rootUrl('/Orders/Index')
        },
        error: function () {
            alert("Error al editar el numero de Guia")
            ControlErrores(xhr, status, error);
        }
    });

});


$('#editStatus').click(function () {

    var idStatus = parseInt($('#status').val());
    var idUsuario = parseInt($('#idUsuario').val());

    var combo = document.getElementById("status");
    var selected = combo.options[combo.selectedIndex].text;
   
    $.ajax({
        type: "POST",
        url: rootUrl("/Orders/EditarStatus"),
        data: {
            idUsuarioOrdenCompra: idUsuario,
            idStatusOrdenCompra: idStatus
        },
        dataType: "Json",
        async: true,
        success: function () {
            $('#mdEditStatus').modal('hide');
            $('#' + idUsuario).text(selected);
        },
        error: function () {
            alert("Error al editar el numero de Guia")
            ControlErrores(xhr, status, error);
        }
    });
});

