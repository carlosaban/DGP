use DVGP_CITAVAL
go

CREATE PROCEDURE [dbo].[DGP_Listar_CompraCliente] (  
@intIdCliente INT  
,@intIdProducto INT  
,@intIdCaja INT  
)  
AS  
DECLARE @varIdEstado CHAR(3)  
BEGIN  
 SET @varIdEstado = dbo.DGP_COMPRA_ESTADO_ANULADO();  
  
 SELECT IdCompra  
  ,'Jabas: '+CONVERT(VARCHAR, TotalJabas,0) + 'Cod. Compra '+ CONVERT(VARCHAR, IdCompra)  AS [Compra]  
 FROM dbo.TbCompra  
 WHERE IdEstado not in (@varIdEstado)  
  AND IdCliente = @intIdCliente  
  AND IdCaja = @intIdCaja
  AND IdProducto = @intIdProducto;  
END
GO

CREATE PROCEDURE [dbo].[DGP_Listar_ProductoClientCompra](
@intIdCliente	INT
) AS
BEGIN
	SELECT	DISTINCT C.IdProducto
		,P.Nombre AS Producto
	FROM dbo.TbCompra AS C
		INNER JOIN dbo.Tb_Producto AS P	ON (C.IdProducto = P.Id_Producto)
	WHERE C.IdEstado = dbo.DGP_COMPRA_ESTADO_REGISTRADO()
		AND C.IdCliente = @intIdCliente;
END

CREATE PROCEDURE [dbo].[DGP_Obtener_Compra](
@intIdCompra	INT
) AS

BEGIN
	SELECT	C.IdCompra
		,C.IdTipoDocumentoCompra
		,C.NumeroDocumento
		,C.TotalPeso_Bruto
		,C.TotalPeso_Tara
		,C.TotalPeso_Neto
		,C.Precio
		,C.MontoSubTotal
		,C.MontoIgv
		,C.MontoTotal
		,C.TotalDevolucion
		,C.TotalAmortizacion
		,C.TotalSaldo
		,C.Observacion
		,C.IdEstado
		,C.IdCaja
		,C.IdEmpresa
		,C.IdProducto
		,[Producto] = P.Nombre
		,C.IdCliente
		,C.IdPersonal
		,C.TotalUnidades 
	FROM dbo.TbCompra AS C
		INNER JOIN dbo.Tb_Producto AS P ON (C.IdProducto = P.Id_Producto)
	WHERE C.IdEstado = dbo.DGP_VENTA_ESTADO_REGISTRADO()
		AND C.IdCompra = @intIdCompra
END


