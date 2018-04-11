use [DVGP_CITAVAL]
go

CREATE PROCEDURE [dbo].[DGP_Listar_Compra](
@intIdCompra	INT = NULL,
@varIdTipoDocumento VARCHAR(5) = NULL,
@intIdClienteTE	INT = NULL,
@intIdProducto	INT = NULL,
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
		,C.IdCliente
		,[Cliente] = ISNULL(CP.Nombres, 'CLIENTE EVENTUAL')
		,C.IdPersonal
		,C.FechaCreacion
		,C.TotalUnidades
	FROM dbo.TbCompra AS C
		LEFT JOIN dbo.Tb_ParametroDetalle AS PD
			ON (PD.Id_Parametro = 3 AND C.IdTipoDocumentoCompra = PD.Valor)
		INNER JOIN dbo.Tb_Producto AS P
			ON (C.IdProducto = P.Id_Producto)
		LEFT JOIN dbo.Tb_Cliente_Proveedor AS CP 
			ON (C.IdCliente = CP.Id_Cliente)
		INNER JOIN dbo.Tb_Empresa AS EM
			ON (C.IdEmpresa = EM.Id_Empresa)
	WHERE C.IdCompra = ISNULL(@intIdCompra, C.IdCompra)
		AND ISNULL(C.IdTipoDocumentoCompra, '-1') = ISNULL(@varIdTipoDocumento, ISNULL(C.IdTipoDocumentoCompra, '-1'))
		AND ISNULL(C.IdCliente, -1) = ISNULL(@intIdClienteTE, ISNULL(C.IdCliente, -1))
		AND C.IdProducto = ISNULL(@intIdProducto, C.IdProducto)
		AND CONVERT(VARCHAR, C.FechaCreacion, 112) BETWEEN CONVERT(VARCHAR, @fechaIni, 112) AND CONVERT(VARCHAR, @fechaFin, 112)
		AND C.IdEstado != dbo.DGP_COMPRA_ESTADO_ANULADO()
             order by CP.Nombres
END

