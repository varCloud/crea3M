//Dropzone.autoDiscover = false;                                                               
function detalleProduct() {
    this.idProductoEcommerce,
        this.identificador,
        this.producto,
        this.unidadVenta,
        this.precioVenta,
        this.idTipoProducto,
        this.tipoProducto,
        this.idCategoriaEcommerce,
        this.CategoriaEcommerce,
        this.descripcion,
        this.activo
}

$(document).ready(function () {

    //$("div#drop").dropzone({ url: rootUrl("/Products/SaveUploadedFile") });
    //Dropzone.forElement("#my-awesome-dropzone")

    

    $('#marca').trigger("change");

    $("#excelfile").change(function (evt) {
        ExportToTable();
    });

    $("#cbMarca").change(function (evt) {
        consultarCategoriasPorMarca(this.value)
    })

    $('#categoria').change(function () {
        var categoria = $('#categoria').val();
        if (categoria > 0)
            consultaProductos();
        console.log("categoria", categoria)
    })
       
        
});

$(document).on('click', '.file-upload-btn', function () {
    var idMarca = parseInt($('#m_marca').val());

    if (idMarca == 0) {
        $("#m-mensaje-archivo-marca").fadeIn();
        return false;
    } else {
        $("#m-mensaje-archivo-marca").fadeOut();
        $('#excelfile').trigger('click');
    } 
  
});

function consultarCategoriasPorMarca(idMarca)
{
        $.ajax({
            type: "post",
            url: rootUrl("/Products/getCatEcommerce"),
            dataType: "json",
            data: {
                idMarca: idMarca,
            },
            success: function (response) {

                $('#categoria').empty()
                $('#idCategoriaEcommerce').empty()
                $('#cotendtIdCategorias').empty();

                $('#categoria').append('<option selected disabled value="">Selecciona una Categoria</option>');
                $.each(response.model, function (index, value) {
                    $('#categoria').append($('<option>', { value: value.idCategoriaEcommerce, text: value.descripcion }));
                });

                $('#idCategoriaEcommerce').append('<option selected disabled value="">Selecciona una Categoria</option>');
                $.each(response.model, function (index, value) {
                    $('#idCategoriaEcommerce').append($('<option>', { value: value.idCategoriaEcommerce, text: value.descripcion }));
                });


                $.each(response.model, function (index, value) {
                    $('#cotendtIdCategorias').append('<p><i class="fa fa-circle text-success" data-icon="car"></i> ' + value.descripcion + ' <b> id Excel: </b> <span class="badge badge-primary badge-pill ml-auto">' + value.idCategoriaEcommerce + '</span></p>');
                });

                //consultaProductos();
                if (response.status == 100) {
                    toastr.info(response.msg);
                }

            },
            error: function (xhr, status, error) {
                alert("Error al obtener las Categorias ")
                ControlErrores(xhr, status, error);
            }
        });
    
}

function alClickEditarProducto(id) {
  
        $.ajax({
            method: "post",
            url: rootUrl("/Products/getProduct"),
            dataType: "json",
            data: {
                idProductoEcommerce: id,
            },
            success: function (resultado) {


                $('#producto').val(resultado.model[0].producto);
                $('#descripcion').val(resultado.model[0].descripcion);
                $('#idProductoEcommerce').val(resultado.model[0].idProductoEcommerce);
                $('#identificador').val(resultado.model[0].identificador);
                $('#precioVenta').val(resultado.model[0].precioVenta);
                $('#unidadVenta').val(resultado.model[0].unidadVenta);
                $('#m_status').val(resultado.model[0].activo);
                $('#cbMarca').val(resultado.model[0].idMarcaEcommerce)
                $('#m_categoria option[value=' + resultado.model[0].idCategoriaEcommerce + ']').attr('selected', 'selected');
                $('#idCategoriaEcommerce').val(resultado.model[0].idCategoriaEcommerce);
                $('#lblEditProduct').html('Editar Producto')
                $('#mdEditProduct').modal('show');
                $('#dvCbMarca').css('display', 'none')
                $('#identificador').attr('disabled', true)
                $('#btnGuardarProd').html('Editar')
            },
            error: function (xhr, status, error) {
                //alert("Error al obtener el detalle del producto")
                ControlErrores(xhr, status, error);
            }
        });

}

function ExportToTable() {

    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
    /*Checks whether the file is a valid excel file*/
    if (regex.test($("#excelfile").val().toLowerCase())) {
        var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
        if ($("#excelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
            xlsxflag = true;
        }
        /*Checks whether the browser supports HTML5*/
        if (typeof (FileReader) != "undefined" && $("#excelfile")[0].files.length > 0) {
            var reader = new FileReader();
            reader.onload = function (e) {

                var data = e.target.result;
                /*Converts the excel data in to object*/
                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }
                /*Gets all the sheetnames of excel in to a variable*/
                var sheet_name_list = workbook.SheetNames;

                var cnt = 0; /*This is used for restricting the script to consider only first sheet of excel*/
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                    /*Convert the cell value to Json*/
                    if (xlsxflag) {
                        var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);

                    }
                    else {
                        var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                    }
                    if (exceljson.length > 0 && cnt == 0) {
                        registarProductos(exceljson);
                        console.log(exceljson);
                        $('#mCargarProduct').modal('hide');
                        $("#excelfile").val("");

                        BindTable(exceljson, '#exceltable');
                        cnt++;
                    }
                });
                $('#exceltable').show();
            }
            if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/
                reader.readAsArrayBuffer($("#excelfile")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#excelfile")[0].files[0]);
            }
        }
        else {
            alert("Sorry! Your browser does not support HTML5!");
        }
    }
    else {
        alert("Please upload a valid Excel file!");
    }
}

function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/
    var columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/
    for (var i = 0; i < jsondata.length; i++) {
        var row$ = $('<tr/>');
        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
            var cellValue = jsondata[i][columns[colIndex]];
            if (cellValue == null)
                cellValue = "";
            row$.append($('<td/>').html(cellValue));
        }
        $(tableid).append(row$);
    }
}

function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/
    var columnSet = [];
    var headerTr$ = $('<tr/>');
    for (var i = 0; i < jsondata.length; i++) {
        var rowHash = jsondata[i];
        for (var key in rowHash) {
            if (rowHash.hasOwnProperty(key)) {
                if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/
                    columnSet.push(key);
                    headerTr$.append($('<th/>').html(key));
                }
            }
        }
    }
    $(tableid).append(headerTr$);
    return columnSet;
}  

function registarProductos(productos_) {

    var idMarca = parseInt($('#m_marca').val());

    if (idMarca == 0) {
        $("#m-mensaje-marca").fadeIn();
        return false;
    } else { $("#m-mensaje-marca").fadeOut(); }

    $.ajax({
        type: "POST",
        url: rootUrl("/Products/registrarProductos"),
        data: { productos: productos_, idMarca: idMarca },
        dataType: "Json",
        async: true,
        success: function (data) {

            console.log("result registrar productos", data)
            if (data.status == 1) {
                toastr.success(data.error_message);

            } else {
                toastr.warning(data.error_message);
            }
            
            consultaProductos();
        },
        error: function () {

        }
    });
}

function initTable() {
    console.log("init table");
    $('#tblProductos').DataTable({
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


    $('#' + 'tblProductos' + '_filter').append('<div style="display: flex;flex-direction:column;align-items:center;justify-content:center;width: 60px;"><button id="btnAgregarProducto" type="button" class="btn btn-primary" title="Agregar Producto" data-toggle="tooltip"><i class="ti-plus"></i></button></div>');
    InitBtnAgregar();
}

function InitBtnAgregar() {
    $('[data-toggle="tooltip"]').tooltip();
    $('#btnAgregarProducto').click(function (e) {
        $('#lblEditProduct').html('Agregar Producto')
        $('#mdEditProduct').modal('show');
        $('#dvCbMarca').css('display', '')
        $('#identificador').attr('disabled', false)
        $('#configreset').trigger('click')
        $('#idProductoEcommerce').val(0)
        $('#btnGuardarProd').html('Guardar')
    });
}

$('#marca').change(function () {
    var marca = $('#marca').val();
    consultarCategoriasPorMarca(marca)
})

function consultaProductos() {
  
    var marca = parseInt($('#marca').val());
    var categoria = parseInt($('#categoria').val());
   
    if (!(categoria > 0)) {
        categoria = 0;
    }
    
    $.ajax({
        type: "post",
        url: rootUrl("/Products/_products"),
        dataType: "html",
        data: {
            idMarca: marca, idCategoria: categoria
        },
        success: function (response) {
            
            $("#table-products").html(response);
            initTable();
            
        },
        error: function (xhr, status, error) {
            alert("Error en la carga de categoria")
            ControlErrores(xhr, status, error);
        }
    });
};

function ActualizarEstatus(event, idProducto) {
    var swicth = $(event).attr("currentTarget");
  
    var id = parseInt(idProducto);
    
    $.ajax({
        type: "POST",
        url: rootUrl("/Products/cambiarEstadoProductos"),
        data: { idProducto: id, activo: swicth.checked },
        dataType: "Json",
        async: true,
        success: function () {
            
        },
        error: function () {

        }
    });
}

function deleteImg(idProduct, pathImg) {

    $.ajax({
        type: "post",
        url: rootUrl("/Products/deleteImgProduct"),
        dataType: "json",
        data: {
            idProduct: idProduct, pathImg: pathImg
        },
        success: function (response) {
            $('#mGaleria').modal('hide');
            consultaProductos();
        },
        error: function (xhr, status, error) {
            alert("Error al editar el Status de la Orden")
            ControlErrores(xhr, status, error);
        }
    });
}

$('#mGaleria').on('show.bs.modal', function (event) {
    var id = $(event.relatedTarget).val();
    $('#contentImg').empty();

    $.ajax({
        method: "post",
        url: rootUrl("/Products/getProductImg"),
        dataType: "json",
        data: {
            idProductoEcommerce: id,
        },
        success: function (resultado) {

            var html = "";
            resultado.model.forEach(element => {
                html+= `
                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6">
                        <div class="card app-file-list">
                            <div class="app-file-icon">
                                <img src="`+ element.productoImagen +`" style="width: 100%; height: 100%;" alt="Alternate Text" />
                                <div class="dropdown position-absolute top-0 right-0 mr-3">
                                    <a href="#" class="font-size-14" data-toggle="dropdown">
                                        <i class="fa fa-ellipsis-v"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <a href="#" onclick="deleteImg('`+ element.idProductoEcommerce + `','` + element.productoImagen +`')" class="dropdown-item">Eliminar Imagen</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                `;

            });

            document.getElementById("contentImg").innerHTML = html;

        },
        error: function (xhr, status, error) {
            alert("Error al obtener el detalle del producto")
            ControlErrores(xhr, status, error);
        }
    });

});

$('#mCargarImg').on('show.bs.modal', function (event) {
    var id = $(event.relatedTarget).val();

    $('#idProducto').val(id);
});

$('#mCargarImg').on('hidden.bs.modal', function () {
    Dropzone.forElement("#my-awesome-dropzone").removeAllFiles(true);
}); 

function onFailureResultGuardarProducto(err){

    console.log("error::::::::", err);
}

function onSuccessResultGuardarProducto(response){

    $('#mdEditProduct').modal('hide');
    toastr.info(response.error_message);
    consultaProductos();

}


function onBeginSubmitGuardarProducto(evt) {
    console.log("onBeginSubmitGuardarProducto", evt)
}

function onCompleteSubmitGuardarProducto(evt) {
    console.log("onCompleteSubmitGuardarProducto",evt)
}






 



