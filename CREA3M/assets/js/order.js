$(document).ready(function () {
    $('#myTable').DataTable({
        "scrollY": "400px",
        "scrollCollapse": true
    });
});


$('#mdEditStatus').on('show.bs.modal', function (event) {
    
    var id = $(event.relatedTarget).val();
    $('#idUsuario').val(id);
    
});


$('#editStatus').click(function () {

    var idStatus = parseInt($('#status').val());
    var idUsuario = parseInt($('#idUsuario').val());

    $.ajax({
        type: "GET",
        url: rootUrl("/Orders/EditarStatus"),
        dataType: "HTML",
        data: {
            idUsuarioOrdenCompra: idUsuario,
            idStatusOrdenCompra: idStatus
        },
        success: function (response) {
            window.location = rootUrl('/Orders/Index')
            
        },
        error: function (xhr, status, error) {
            alert("Error al editar el Status de la Orden")
            ControlErrores(xhr, status, error);
        }
    });


});