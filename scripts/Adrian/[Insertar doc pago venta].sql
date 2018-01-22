USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[InsertarDocumento]    Script Date: 15/01/2018 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[InsertarDocumento](



@IdTipoDocumento varchar(10)

,@Fecha datetime

,@Monto decimal(18,2)

,@IdCaja int

,@IdCliente int

,@IdPersonal int

,@IdTipoPago varchar(10)= 'EFE'
,@observacion varchar(100) = null

) AS



BEGIN

	

	INSERT INTO [Tb_documento]

           ([IdTipoDocumento]

           ,[Fecha]

           ,[Monto]

           ,[IdEstado]

           ,[FechaCreacion]

		   ,[FechaModificacion]

		   ,[idCliente]

		   ,[IdPersonal]

		   ,[IdCaja]

		   , IdTipoPago

		   ,Observacion

           

)

     VALUES

           (

		    @IdTipoDocumento 

		   ,@Fecha 

		   ,@Monto 

		   ,'REG'

		   ,getdate()

		   ,null

		   ,@idCliente

		   ,@IdPersonal

		   ,@IdCaja
		   
			,@IdTipoPago 

			,@observacion

		   )







		   SELECT SCOPE_IDENTITY();



END






