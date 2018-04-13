use [DVGP_CITAVAL]
go



create PROCEDURE [dbo].[ListarDocumento](

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

		  ,d.[FechaModificacion]

		  ,d.[FechaCreacion]

		  ,d.[UsuarioModificacion]

		  ,d.[UsuarioCreacion]

		  ,[UsuarioEliminacion]

		  ,[IdCliente]

		  ,[IdPersonal]

		  ,[IdCaja]

		  ,[IdTipoPago]

		  ,[Observacion]
		  
		  ,cli.Nombres as [ClienteNombre]
	  FROM [dbo].[Tb_documento] d
	  left join [dbo].[Tb_Cliente_Proveedor] cli on cli.Id_Cliente = d.IdCliente
	  WHERE 1=1
	  AND IdEstado = 'REG' 
	  AND IdDocumento = ISNULL(@IdDocumento , IdDocumento)

	  AND FECHA BETWEEN isnull( @FechaInicio , cast('1900-01-01' as date)) AND isnull( @FechaFinal , getdate() )

	  AND IdTipoDocumento= ISNULL(@IdTipoDocumento , IdTipoDocumento)

	  AND IdCaja= ISNULL(@IdCaja , IdCaja)

	  AND IdCliente= ISNULL(@IdCliente , IdCliente)

	  AND IdPersonal= ISNULL(@IdPersonal , IdPersonal)

	 --AND isnull(IdTipoPago, '-1') =  ISNULL(@IdTipoPago , isnull(IdTipoPago, '-1'))

END


go


