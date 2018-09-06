USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Listar_DetalleMaestra]    Script Date: 02/09/2018 08:03:00 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER   PROCEDURE [dbo].[DGP_Listar_DetalleMaestra] (
@intIdParametro	INT = NULL
, @IdParametroDetallePadre INT  = null

)
AS
/*
********************************************************
PROCEDIMIENTO		: dbo.DGP_Listar_DetalleMaestra
FECHA			: 04/03/2009 (dd/MM/yyyy)
AUTOR			: Alexander Macuri
********************************************************
*/
BEGIN
	SELECT	Id_Item
		,Id_Parametro
		,Valor
		,Texto
	FROM dbo.Tb_ParametroDetalle
	WHERE 1=1
	and Id_Parametro = ISNULL(@intIdParametro, Id_Parametro)
	and ISNULL(IdParametroDetallePadre, -1) = ISNULL(@IdParametroDetallePadre, ISNULL(IdParametroDetallePadre, -1) )
	ORDER BY Id_Parametro, Orden;
END



