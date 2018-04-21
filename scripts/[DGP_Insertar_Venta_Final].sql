USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Insertar_Venta_Final]    Script Date: 18/04/2018 12:18:42 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER              PROCEDURE [dbo].[DGP_Insertar_Venta_Final] (
@intIdVenta		INT
,@decPrecioVenta		decimal(8,2) = null
,@IdUsuario int  = 1
,@IdCaja int = 1
) AS

begin

	DECLARE @RC int

	-- TODO: Set parameter values here.

	EXECUTE @RC = [dbo].[DGP_Insertar_Venta_Final_Sin_Reaplicar] 
	   @intIdVenta
	  ,@decPrecioVenta
	  ,@IdUsuario
	  ,@IdCaja


END;


