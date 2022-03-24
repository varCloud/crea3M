
Dropzone.autoDiscover = false;

$(document).ready(function () {
    initDropZone();
    consultaImages();


})

function ActualizarEstatus(event, idImagen) {
    var swicth = $(event).attr("currentTarget");
    $.ajax({
        type: "POST",
        url: rootUrl("/Slider/DesactivarImagen"),
        data: { idImagen: idImagen, estatus: swicth.checked },
        dataType: "json",
        async: true,
        success: function (data) {
            toastr.success('Imagen actualizada');
        },
        error: function () {

        }
    });
}

function onEliminarImagen(id, event) {

    swal({
        title: "¿Estas seguro que deseas eliminar la imagen?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) { 
                eliminarImagenes(id)
            } else {
                console.log("cancel")
            }
        });
}

function consultaImages(idImagen = 0) {
    $.ajax({
        type: "post",
        url: rootUrl("/Slider/_obtenerImagenes"),
        dataType: "html",
        data: {
            idImagen: idImagen
        },
        success: function (response) {
            $("#content-table-images").html(response);
            initTable();

        },
        error: function (xhr, status, error) {
            console.log("error al consultar la tabla")
            ControlErrores(xhr, status, error);
        }
    });
};

function eliminarImagenes(idImagen = 0) {
    $.ajax({
        type: "post",
        url: rootUrl("/Slider/EliminarImagen"),
        dataType: "json",
        data: {
            idImagen: idImagen
        },
        success: function (response) {
            if(response.estatus == 200)
                swal(response.mensaje, { icon: "success", });
            consultaImages();
        },
        error: function (xhr, status, error) {
            console.log("error al consultar la tabla" , error)
           
        }
    });
};

function initTable() {
    $('#tblImagenes').DataTable({
        "scrollY": "550px",
        "scrollCollapse": true,
        "dom": 'frtip',

        //dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o" style="font-size:20px;"></i>',
                className: 'btn btn-outline-primary',
                titleAttr: 'Exportar a PDF',
                title: "Productos",
                exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:20px;"></i>',
                titleAttr: 'Exportar a Excel',
                className: 'btn btn-outline-info',
                exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] }
            },
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


    $('#' + 'tblImagenes' + '_filter').append('<div style="display: flex;flex-direction:column;align-items:center;justify-content:center;width: 60px;"><button id="btnAgregarImagen" type="button" class="btn btn-primary" title="Agregar Imagen" data-toggle="tooltip"><i class="ti-plus"></i></button></div>');
    InitBtnAgregar();
}

function InitBtnAgregar() {
    $('[data-toggle="tooltip"]').tooltip();
    $('#btnAgregarImagen').click(function (e) {
        $('#mImgSlider').modal('show');
    });
}

function initDropZone() { 
    $("#dropzone-imagenes-slider").dropzone({
        maxFiles: 2000,
        url: rootUrl("/Slider/SaveUploadedFile"),
        success: function (file, response) {
            console.log(response);
        }
    });
}

$('#mImgSlider').on('hidden.bs.modal', function () {
    Dropzone.forElement("#dropzone-imagenes-slider").removeAllFiles(true);
    consultaImages();
}); 