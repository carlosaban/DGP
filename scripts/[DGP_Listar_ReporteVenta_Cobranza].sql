alter       PROCEDURE [dbo].[DGP_Listar_ReporteVenta_Cobranza] (

@intIdVenta	INT = NULL

,@datFecha	DATETIME = NULL

,@intIdCliente	INT = NULL

,@intIdZona	INT = NULL

) AS

/*

**************************************************

PROCEDIMIENTO	: dbo.DGP_Listar_ReporteVenta_Cobranza

FECHA CREACION	: 09/05/2009 (dd/MM/yyyy)

AUTOR		: Alexander Macuri

**************************************************

*/

BEGIN



	SELECT 	V.Id_Venta
		, CJ.FECHA
		,[Tipo_Cobranza] = ''
		,V.Total_Saldo
		,V.Id_Cliente
		,[Cliente] = isnull( C.Nombres , v.ClienteEventual)
		,C.Id_Zona
		,[Zona] = isnull (Z.Descripcion , 'Cliente eventual')
		, case  C.ESCLIENTECONGELADO
		 	 when 0 THEN ''
		 	 when 1 THEN 'congelado'
		  END as TIPO_CLIENTE ,

		prod.nombre as PRODUCTO
		, V.Total_Peso_Neto as PesoNeto
		, iif(V.Total_Saldo= V.Monto_Total, '','Saldo') as EsSaldo
	FROM dbo.Tb_Venta AS V

	     INNER JOIN Tb_Producto prod on prod.Id_Producto = v.Id_Producto

	     INNER JOIN dbo.Tb_Caja CJ ON CJ.ID_CAJA= V.ID_CAJA

	     left JOIN dbo.Tb_Cliente_Proveedor AS C  ON (V.Id_Cliente = C.Id_Cliente)

	     left JOIN dbo.Tb_Zona AS Z  ON (C.Id_Zona = Z.Id_Zona)

	WHERE 	1=1

		and v.EsSobrante = 0

		AND V.IdEstado != dbo.DGP_VENTA_ESTADO_ANULADO()

		AND V.IdEstado != dbo.DGP_VENTA_ESTADO_CANCELADO()

		AND V.Id_Venta = ISNULL(@intIdVenta, V.Id_Venta)

		AND  CJ.FECHA  = ISNULL(@datFecha, CJ.FECHA )

		AND isnull( V.Id_Cliente , -1) = ISNULL(@intIdCliente, isnull( V.Id_Cliente , -1) )

		AND isnull(C.Id_Zona , -1) = ISNULL(@intIdZona, isnull(C.Id_Zona , -1) )

	ORDER BY C.Id_Zona ASC , C.Nombres,   V.IdEstado , CJ.FECHA, prod.nombre    , V.Total_Saldo  ;

END
