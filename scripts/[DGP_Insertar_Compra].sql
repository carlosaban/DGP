USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[DGP_Insertar_Compra]    Script Date: 11/05/2018 04:26:23 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Carlos Abanto G
-- CREATE date: 4/7/2015
-- Description:	N/A
-- =============================================
create PROCEDURE [dbo].[DGP_Insertar_Compra] 
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
						
						--,@EsSobrante bit = NULL
						,@Total_Devolucion decimal (18, 2)  = NULL
						,@Total_Amortizacion decimal (18, 2)  = NULL
						,@Total_Saldo decimal (18, 2)  = NULL
						
						,@Observacion varchar(500) = null
						,@IdProducto INT = NULL
						,@TotalUnidades INT = NULL
						,@Usuario INT = NULL
						
						,@IdCaja INT = NULL
						,@Fecha date 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;


INSERT INTO [dbo].[TbCompra]
           ([idTipoDocumentoCompra]
           ,[IdProveedor]
           ,[TotalJabas]
           ,[TotalPeso_Bruto]
           ,[TotalPeso_Tara]
           ,[TotalPeso_Neto]
           ,[Precio]
           ,[MontoTotal]
           ,[MontoSubTotal]
           ,[MontoIgv]
           ,[TotalDevolucion]
           ,[TotalAmortizacion]
           ,[TotalSaldo]
           ,[FechaCreacion]
           ,[UsuarioCreacion]
           ,[IdEstado]
		   ,Observacion
		   ,IdProducto
		   ,TotalUnidades
		   ,IdCaja
		   ,IdPersonal
		   ,Fecha
		   )
     VALUES
           (
		     @idTipoDocumento
			, @idClienteProveedor
			, @Total_Jabas
			, @Total_Peso_Bruto
			, @Total_Peso_Tara
			, @Total_Peso_Neto
			, @Precio
			, @Monto_Total
			, @Monto_SubTotal
			, @Monto_Igv
			, @Total_Devolucion
			, @Total_Amortizacion
			, @Total_Saldo
			, GETDATE()
			, @Usuario
		    , DBO.DGP_VENTA_ESTADO_REGISTRADO()
			, @Observacion
		    , @IdProducto
		    , @TotalUnidades
			, @IdCaja
			, @Usuario
			, @Fecha
		   )

   
           SELECT @idCompra = SCOPE_IDENTITY()
END



	







