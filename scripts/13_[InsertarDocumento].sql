use [DVGP_CITAVAL]
go

alter   PROCEDURE dbo.[InsertarDocumento](



@IdTipoDocumento varchar(10)

,@Fecha datetime

,@Monto decimal(18,2)

,@Usuario int

,@IdCaja int

,@IdCliente int

,@IdPersonal int

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

		   )







		   SELECT SCOPE_IDENTITY();



END






