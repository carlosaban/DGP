



alter PROCEDURE [dbo].[ListarDocumento](

@IdDocumento int = null

,@IdTipoDocumento varchar(10) = null

,@FechaInicio date

,@FechaFinal date

,@IdCaja int =null

,@IdCliente int 

,@IdPersonal int =null

,@IdTipoPago varchar(100) =null

) AS



BEGIN

	



	SELECT [IdDocumento]

		  ,[IdTipoDocumento]

		  ,[Fecha]

		  ,[Monto]

		  ,[IdEstado]

		  ,[EsEliminado]

		  ,[FechaEliminacion]

		  ,[FechaModificacion]

		  ,[FechaCreacion]

		  ,[UsuarioModificacion]

		  ,[UsuarioCreacion]

		  ,[UsuarioEliminacion]

		  ,[IdCliente]

		  ,[IdPersonal]

		  ,[IdCaja]

		  ,[IdTipoPago]

		  ,[Observacion]

	  FROM [dbo].[Tb_documento]

	  WHERE 1=1
	  AND IdEstado = 'REG' 
	  AND IdDocumento = ISNULL(@IdDocumento , IdDocumento)

	  AND FECHA BETWEEN isnull( @FechaInicio , cast('1900-01-01' as date)) AND isnull( @FechaFinal , getdate() )

	  AND IdTipoDocumento= ISNULL(@IdTipoDocumento , IdTipoDocumento)

	  AND IdCaja= ISNULL(@IdCaja , IdCaja)

	  AND IdCliente= ISNULL(@IdCliente , IdCliente)

	  AND IdPersonal= ISNULL(@IdPersonal , IdPersonal)

	  AND IdTipoPago =  ISNULL(@IdTipoPago , @IdTipoPago)





END






