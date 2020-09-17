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

$(document).on('click', '.file-upload-btn', function () {
    var idMarca = parseInt($('#m_marca').val());

    if (idMarca == 0) {
        $("#m-mensaje-marca").fadeIn();
        return false;
    } else {
        $("#m-mensaje-marca").fadeOut();
        $('form#file-upload input[type="file"]').trigger('click');
    }
});

$(document).ready(function () {
    $('#myTable').DataTable({
        "scrollY": "400px",
        "scrollCollapse": true
    });

});

document.getElementById('file-upload').addEventListener('change', ExportToTable, false);

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

function ExportToTable() {

    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
    /*Checks whether the file is a valid excel file*/
    if (regex.test($("#excelfile").val().toLowerCase())) {
        var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
        if ($("#excelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
            xlsxflag = true;
        }
        /*Checks whether the browser supports HTML5*/
        if (typeof (FileReader) != "undefined") {
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
    } else { $("#m-mensaje-marca").fadeOut();}

    $.ajax({
        type: "POST",
        url: rootUrl("/Products/registrarProductos"),
        data: { productos: productos_, idMarca: idMarca },
        dataType: "Json",
        async: true,
        success: function () {
            window.location = rootUrl('/Products/Index')
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
            window.location = rootUrl('/Products/Index')

        },
        error: function (xhr, status, error) {
            alert("Error al editar el Status de la Orden")
            ControlErrores(xhr, status, error);
        }
    });
}

$('#mGaleria').on('show.bs.modal', function (event) {
    var id = $(event.relatedTarget).val();

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
                                <img src="" style="width: 100%; height: 100%;" alt="Alternate Text" />
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

$('#mdEditProduct').on('show.bs.modal', function (event) {
    var id = $(event.relatedTarget).val();

    $.ajax({
        method: "post",
        url: rootUrl("/Products/getProduct"),
        dataType: "json",
        data: {
            idProductoEcommerce: id,
        },
        success: function (resultado) {
            $('#m_producto').val(resultado.model[0].producto);
            $('#m_descripcion').val(resultado.model[0].descripcion);
            $('#m_np').val(resultado.model[0].idProductoEcommerce);
            $('#m_identidicador').val(resultado.model[0].identificador);
            $('#m_precio_venta').val(resultado.model[0].precioVenta);
            $('#m_unidad_venta').val(resultado.model[0].unidadVenta);
            $('#m_status').val(resultado.model[0].activo);
            
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

$('#editProduct').click(function () {

    producto = new detalleProduct();

    producto.idProductoEcommerce = parseInt($('#m_np').val());
    producto.identificador = $('#m_identidicador').val();
    producto.producto = $('#m_producto').val();
    producto.descripcion = $('#m_descripcion').val();
    producto.unidadVenta = $('#m_unidad_venta').val();
    producto.precioVenta = parseFloat($('#m_precio_venta').val());
    
    $.ajax({
        type: "post",
        url: rootUrl("/Products/EditarProduct"),
        dataType: "json",
        data: {
            producto: producto,
        },
        success: function (response) {
            window.location = rootUrl('/Products/Index')

        },
        error: function (xhr, status, error) {
            alert("Error al editar el Status de la Orden")
            ControlErrores(xhr, status, error);
        }
    });


});








 



