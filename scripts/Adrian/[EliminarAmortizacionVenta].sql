USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[InsertarAmortizacionVenta]    Script Date: 22/01/2018 04:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter     PROCEDURE [dbo].[EliminarAmortizacionVenta](

@idAmortizacionVenta		DECIMAL(8,2)
)

AS

BEGIN

	SET NOCOUNT OFF

	update dbo.Tb_Amort_Venta 

		set IdEstado = 'ANL' WHERE Id_Amort_Venta = @idAmortizacionVenta

END

