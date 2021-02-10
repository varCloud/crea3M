select * from BC_TBL_ORDEN_COMPRA_DETALLE
select * from [dbo].[BC_TBL_PRODUCTO_ECOMMERCE]
select * from [dbo].[BC_TBL_PRODUCTO_IMAGEN]
select * from [dbo].[BC_TBL_USUARIO_CARRITO_COMPRA]
select * from [dbo].[BC_TBL_USUARIO_DOMICILIO]
select * from [dbo].[BC_TBL_USUARIO_ECOMMERCE]
select * from [dbo].[BC_TBL_USUARIO_ORDEN_COMPRA]

--truncate table BC_TBL_ORDEN_COMPRA_DETALLE
--truncate table [BC_TBL_PRODUCTO_ECOMMERCE]
--truncate table [BC_TBL_PRODUCTO_IMAGEN]
--truncate table [dbo].[BC_TBL_USUARIO_CARRITO_COMPRA]

--delete from  [dbo].[BC_TBL_USUARIO_DOMICILIO]
--DBCC CHECKIDENT ('BC_TBL_USUARIO_DOMICILIO', RESEED, 0)  

--delete from  [dbo].[BC_TBL_USUARIO_ECOMMERCE]
--DBCC CHECKIDENT ('BC_TBL_USUARIO_ECOMMERCE', RESEED, 0)  

--delete from [dbo].[BC_TBL_USUARIO_ORDEN_COMPRA]
--DBCC CHECKIDENT ('BC_TBL_USUARIO_ORDEN_COMPRA', RESEED, 0)  




--update [dbo].[BC_TBL_PRODUCTO_ECOMMERCE]  set precioEnvio = 0

update [BC_TBL_USUARIO_ORDEN_COMPRA] set idStatusOrdenCompra = 2 where folioCompra = 'trfyypzjek7tkokywocq'

exec BC_SP_CONSULTAR_WH_STATUS_ORDEN_COMPRA 'trfyypzjek7tkokywocq','charge.succeeded','200','Cargo Realizado Exitosamente'
     

select * from Productos

SELECT * FROM TipoProducto

-- OBTENER LOS PRODUCTOS QUE AUN NO ESTAN DADOS DE ALTA
select PE.identificador , p.Identificador ,P.Producto ,tp.idTipoProducto, TP.Descripcion from BC_TBL_PRODUCTO_ECOMMERCE pe RIGHT join 
Productos p ON P.Identificador  = PE.identificador JOIN TipoProducto TP ON TP.idTipoProducto = P.idTipoProducto
WHERE PE.identificador IS NULL



select * from BC_TBL_PRODUCTO_ECOMMERCE   order by fechaRegistro desc

update BC_TBL_PRODUCTO_ECOMMERCE set idMarcaEcommerce = 2 where idProductoEcommerce = 129


select c.descripcion, b.descripcion , a.* from 
BC_TBL_PRODUCTO_ECOMMERCE A join  BC_CAT_CATEGORIA_ECOMMERCE C  on a.idCategoriaEcommerce = C.idCategoriaEcommerce
join BC_CAT_MARCA_ECOMMERCE b on b.idMarcaEcommerce  = a.idMarcaEcommerce
where a.idProductoEcommerce  = 129
