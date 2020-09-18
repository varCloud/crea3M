$(document).ready(function () {
    $('#myTable').DataTable({
        "scrollY": "400px",
        "scrollCollapse": true
    });
});


$('#mdEditStatus').on('show.bs.modal', function (event) {
    
    var button = $(event.relatedTarget);
    
    var orden = button.data('idorden');
   
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
            window.location = rootUrl('/Orders/Index')
        },
        error: function () {
            alert("Error al editar el numero de Guia")
            ControlErrores(xhr, status, error);
        }
    });
});

