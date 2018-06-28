USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Listar_Compra]    Script Date: 13/06/2018 08:37:27 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[DGP_Listar_Compra](
@IdCompra	INT = NULL,
@IdProveedor	INT = NULL,
@IdProducto	INT = NULL,
@fechaIni		DATETIME = NULL,
@fechaFin		DATETIME = NULL
) AS
BEGIN
	SELECT	C.IdCompra
		,C.IdTipoDocumentoCompra
		,[TipoDocumentoCompra] = PD.Texto
		,C.NumeroDocumento
		,C.TotalPeso_Bruto
		,C.TotalPeso_Tara
		,C.TotalPeso_Neto
		,C.Precio
		,C.MontoSubTotal
		,C.MontoIgv
		,C.MontoTotal --// Importe
		,C.TotalDevolucion
		,C.TotalAmortizacion --// Pago Efectuado
		,C.TotalSaldo --// Saldo Anterior
		,C.Observacion
		,C.IdEstado
		,C.IdEmpresa
		,[Empresa] = EM.RazonSocial
		,C.IdProducto
		,[Producto] = P.Nombre
		,C.IdProveedor
		,[Cliente]  = ISNULL(CP.Nombres, 'CLIENTE EVENTUAL')
		,C.IdPersonal
		,C.FechaCreacion
		,C.TotalUnidades
	FROM dbo.TbCompra AS C
		LEFT JOIN dbo.Tb_ParametroDetalle AS PD	ON (PD.Id_Parametro = 3 AND C.IdTipoDocumentoCompra = PD.Valor)
		LEFT JOIN dbo.Tb_Producto AS P	ON (C.IdProducto = P.Id_Producto)
		LEFT JOIN dbo.Tb_Cliente_Proveedor AS CP ON (C.IdProveedor = CP.Id_Cliente)
		LEFT JOIN dbo.Tb_Empresa AS EM	ON (C.IdEmpresa = EM.Id_Empresa)
	WHERE 1=1 
	AND C.IdCompra = ISNULL(@IdCompra, C.IdCompra)
	---	AND ISNULL(C.IdProveedor, -1) = ISNULL(@IdProveedor, ISNULL(C.IdProveedor, -1))
	--	AND C.IdProducto = ISNULL(@IdProducto, C.IdProducto)
	--	AND CONVERT(VARCHAR, C.Fecha, 112) BETWEEN CONVERT(VARCHAR, @fechaIni, 112) AND CONVERT(VARCHAR, @fechaFin, 112)
	--	AND C.IdEstado != dbo.DGP_VENTA_ESTADO_ANULADO()
             order by CP.Nombres
END

