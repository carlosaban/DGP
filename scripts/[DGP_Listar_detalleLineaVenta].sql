USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Listar_detalleLineaVenta]    Script Date: 09/08/2018 12:57:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create     PROCEDURE [dbo].[DGP_Listar_detalleLineaVenta](

  @IdVenta	INT
, @FechaInicial date
, @FechaFinal date
, @IdCliente INT 
) 
/*
**************************************************

PROCEDIMIENTO	: dbo.DGP_Listar_detalleLineaVenta

FECHA CREACION	: 29/03/2009 (dd/MM/yyyy)

AUTOR		: Carlos Abanto

**************************************************
*/

AS

BEGIN

	SET NOCOUNT OFF

		SELECT	det.Id_Linea_Venta
		,det.Cantidad_Javas
		,det.Peso_Bruto
		,det.Peso_Tara
		,det.Peso_Neto
		,det.EsDevolucion
		,det.EsPesoTaraEditado
		,det.Observacion
		,det.Id_Venta
		,det.IdEstado
		,isnull( det.unidades , 0) unidades
		,v.Id_Venta
		,v.ClienteEventual
		,v.Monto_Total
		,v.Total_Devolucion
		,v.Total_Peso_Bruto
		,v.Total_Peso_Tara
		,v.Total_Peso_Neto
		,v.Total_Jabas
		,v.Id_Producto
		,v.TotalUnidades
		, CAJ.Fecha
		
		, ISNULL (CLI.Nombres , V.ClienteEventual ) AS CLIENTE
		,P.Nombre AS PRODUCTO
		, p.TieneDetalle

	FROM dbo.Tb_Linea_Venta det
	INNER JOIN  Tb_Venta v on v.Id_Venta = det.Id_Venta
	INNER JOIN Tb_Caja caj on caj.Id_Caja = v.Id_Caja
	INNER JOIN Tb_Producto P ON P.Id_Producto = V.Id_Producto
	LEFT JOIN Tb_Cliente_Proveedor  CLI ON CLI.Id_Cliente = V.Id_Cliente
	WHERE 1=1	    
	    AND v.IdEstado not in ('ANL')
		AND v.Id_Venta = ISNULL(@IdVenta, v.Id_Venta)
		AND isnull(v.id_Cliente, '-1') = ISNULL(@IdCliente, isnull(v.id_Cliente, '-1'))
		AND caj.Fecha between ISNULL(@FechaInicial ,caj.Fecha ) and ISNULL( @FechaFinal ,caj.Fecha ) 
	
	ORDER BY Id_Linea_Venta;



END












