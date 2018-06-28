USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Actualizar_EstadoVentaRegistrada]    Script Date: 08/06/2018 12:03:04 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER   PROCEDURE [dbo].[DGP_Actualizar_EstadoVentaRegistrada](
@intIdVenta		INT
,@varEstado		VARCHAR(5)
,@varObservacion	VARCHAR(500)
) 
/*
**************************************************
PROCEDIMIENTO	: dbo.DGP_Actualizar_EstadoVentaRegistrada
FECHA CREACION	: 18/04/2009 (dd/MM/yyyy)
AUTOR		: Alexander Macuri
**************************************************
*/
AS
BEGIN
	SET NOCOUNT OFF
	UPDATE dbo.Tb_Venta SET
		IdEstado = @varEstado
		,Observacion = @varObservacion
	WHERE Id_Venta = @intIdVenta;

	if @varEstado = dbo.DGP_VENTA_ESTADO_ANULADO()
	begin

		UPDATE dbo.[Tb_Amort_Venta] SET
			IdEstado = dbo.DGP_VENTA_ESTADO_ANULADO()
			,[FechaModificacion] = GETDATE()
		WHERE Id_Venta = @intIdVenta
		
		--execute dbo.DGP_Insertar_Venta_Final @intIdVenta, null ;

	end 


END



