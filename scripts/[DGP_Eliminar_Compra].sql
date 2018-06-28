USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Eliminar_Compra]    Script Date: 11/05/2018 04:40:31 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Carlos Abanto G
-- CREATE date: 4/7/2015
-- Description:	N/A
-- =============================================
create PROCEDURE [dbo].[DGP_Eliminar_Compra] 
						 @idCompra int 
						 ,@Usuario int 
						

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[TbCompra]
   SET 
	  FechaEliminacion = GETDATE()
	, UsuarioEliminacion = @Usuario
	, idestado = dbo.DGP_VENTA_ESTADO_ANULADO()
 WHERE idCompra = @idCompra

   
END



	







