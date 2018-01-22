

  update privilegios
  set descripcion ='Mantenimiento de Doc. Pagos Venta' where id=35
  insert into privilegios values (36,'Mantenimiento de Doc. Pagos Compra',1,1)

  insert into perfil_privilegios values(1,36),(2,36),(3,36)


  go


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


