USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Actualizar_Compra]    Script Date: 11/05/2018 04:37:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Carlos Abanto G
-- CREATE date: 4/7/2015
-- Description:	N/A
-- =============================================
create PROCEDURE [dbo].[DGP_Actualizar_Compra] 
						 @idCompra int OUT
						,@idTipoDocumento varchar (3)  = NULL
						,@idClienteProveedor int = NULL
						,@Total_Jabas decimal (18, 2)  = NULL
						,@Total_Peso_Bruto decimal (18, 2)  = NULL
						,@Total_Peso_Tara decimal (18, 2)  = NULL
						,@Total_Peso_Neto decimal (18, 2)  = NULL
						,@Precio decimal (18, 2)  = NULL
						,@Monto_SubTotal decimal (18, 2)  = NULL
						,@Monto_Igv decimal (18, 2)  = NULL
						,@Monto_Total decimal (18, 2)  = NULL
						
						,@Total_Devolucion decimal (18, 2)  = NULL
						,@Total_Amortizacion decimal (18, 2)  = NULL
						,@Total_Saldo decimal (18, 2)  = NULL
						
						,@Observacion varchar(500) = null
						,@IdProducto INT = NULL
						,@TotalUnidades INT = NULL
						,@Usuario INT = NULL
						
						,@IdCaja INT = NULL
						,@IdEstado varchar(3) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	UPDATE [dbo].[TbCompra]
   SET 
	  IdTipoDocumentoCompra = @idTipoDocumento
	, IdProveedor = @idClienteProveedor
	, TotalJabas = @Total_Jabas
	, TotalPeso_Bruto = @Total_Peso_Bruto
	, TotalPeso_Tara = @Total_Peso_Tara
	, TotalPeso_Neto = @Total_Peso_Neto
	, Precio = @Precio
	, MontoTotal = @Monto_Total
	, MontoSubTotal = @Monto_SubTotal
	, MontoIgv = @Monto_Igv
	--, EsSobrante = @EsSobrante
	, TotalDevolucion = @Total_Devolucion
	, TotalAmortizacion = @Total_Amortizacion
	, TotalSaldo = @Total_Saldo
	, FechaModificacion = getdate()
	, UsuarioModificacion = @Usuario
	, IdEstado = @IdEstado
 WHERE idCompra = @idCompra

   
END



	







