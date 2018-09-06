USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[InsertarDocumento]    Script Date: 03/09/2018 10:52:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[InsertarDocumento](



@IdTipoDocumento varchar(10)

,@Fecha datetime

,@Monto decimal(18,2)

,@Usuario int

,@IdCaja int

,@IdCliente int

,@IdPersonal int

, @IdFormaPago varchar(10)
, @observacion varchar(200)
, @NumeroRecibo varchar(50)
, @IdBanco varchar(50) 
, @NumeroOperacion varchar(50)

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

		    , IdFormaPago 
			, observacion 
			, NumeroRecibo
			, IdBanco 
			, NumeroOperacion 

           

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

		    , @IdFormaPago 
			, @observacion 
			, @NumeroRecibo
			, @IdBanco 
			, @NumeroOperacion 


		   )







		   SELECT SCOPE_IDENTITY();



END






