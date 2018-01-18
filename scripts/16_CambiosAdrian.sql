

create procedure ListarAmortVenta(
@IdDocumento int
) AS 

SELECT [Id_Amort_Venta]
      ,[Monto]
      ,[NumeroDocumento]
      ,[IdFormaPago]
      ,[FechaPago]
      ,[IdTipoAmortizacion]
      ,[Observacion]
      ,[IdEstado]
      ,[Id_Venta]
      ,[Id_Cliente]
      ,[Id_Personal]
      ,[UsuarioCreacion]
      ,[FechaCreacion]
      ,[UsuarioModificacion]
      ,[FechaModificacion]
      ,[id_caja]
      ,[IdDocumento]
  FROM [DVGP_CITAVAL].[dbo].[Tb_Amort_Venta]
  where IdDocumento = @IdDocumento 
  go


