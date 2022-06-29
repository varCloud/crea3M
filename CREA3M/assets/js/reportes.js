$(document).ready(function () {

    consultarInformacion();
    $("#btnFilter").click(function (e)  {
        consultarInformacion();
    })
})


function consultarInformacion() {
    $.ajax({
        type: "post",
        url: rootUrl("/Reportes/_ObtenerTipoReporte"),
        dataType: "html",
        data:  convertFormToJSON("#frmFilter"),
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
    $('#tblReporte').DataTable({
        "scrollY": "550px",
        "scrollCollapse": true,
        //"dom": 'frtip',

        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o" style="font-size:20px;"></i>',
                className: 'btn btn-danger btn-square btn-uppercase',
                titleAttr: 'Exportar a PDF',
                title: "Reporte",
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


   // $('#' + 'tblImagenes' + '_filter').append('<div style="display: flex;flex-direction:column;align-items:center;justify-content:center;width: 60px;"><button id="btnAgregarImagen" type="button" class="btn btn-primary" title="Agregar Imagen" data-toggle="tooltip"><i class="ti-plus"></i></button></div>');
   // InitBtnAgregar();
}

function convertFormToJSON(form) {
    return $(form)
        .serializeArray()
        .reduce(function (json, { name, value }) {
            json[name] = value;
            return json;
        }, {});
}
