USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Listar_Venta]    Script Date: 09/08/2018 12:38:44 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







ALTER           PROCEDURE [dbo].[DGP_Listar_Venta](
@intIdVenta	INT = NULL
,@intIdCaja	INT = NULL
) AS
/*
**************************************************
PROCEDIMIENTO	: dbo.DGP_Listar_Venta
FECHA CREACION	: 07/03/2009 (dd/MM/yyyy)
AUTOR		: Alexander Macuri
**************************************************
*/
BEGIN
	SELECT	V.Id_Venta
		,V.IdTipoDocumentoVenta
		,[TipoDocumentoVenta] = PD.Texto
		,V.NumeroDocumento
		,V.Total_Peso_Bruto
		,V.Total_Peso_Tara
		,V.Total_Peso_Neto
		,V.Precio
		,V.Monto_SubTotal
		,V.Monto_Igv
		,V.Monto_Total --// Importe
		,V.EsSobrante
		,V.TieneDevolucion
		,V.Total_Devolucion
		,V.Total_Amortizacion --// Pago Efectuado
		,V.Total_Saldo --// Saldo Anterior
		,V.Observacion
		,V.IdEstado
		,V.Id_Caja
		,V.Id_Empresa
		,[Empresa] = EM.RazonSocial
		,V.Id_Producto
		,[Producto] = P.Nombre
		,V.Id_Cliente
		,[Cliente] = ISNULL(C.Nombres, ' CE : ' + V.ClienteEventual)
		,V.Id_Personal
		,V.FechaCreacion
		,v.TotalUnidades
		,cj.Fecha as Fecha
	FROM dbo.Tb_Venta AS V
		LEFT JOIN dbo.Tb_ParametroDetalle AS PD
			ON (PD.Id_Parametro = 3 AND V.IdTipoDocumentoVenta = PD.Valor)
		INNER JOIN dbo.Tb_Producto AS P
			ON (V.Id_Producto = P.Id_Producto)
		LEFT JOIN dbo.Tb_Cliente_Proveedor AS C 
			ON (V.Id_Cliente = C.Id_Cliente)
		INNER JOIN dbo.Tb_Empresa AS EM
			ON (V.Id_Empresa = EM.Id_Empresa)
		inner join dbo.tb_caja cj on v.Id_Caja = cj.Id_Caja
	WHERE V.Id_Venta = ISNULL(@intIdVenta, V.Id_Venta)
		AND V.Id_Caja = ISNULL(@intIdCaja, V.Id_Caja)
		AND V.IdEstado != dbo.DGP_VENTA_ESTADO_ANULADO()
             order by C.Nombres
END


