use [DVGP_CITAVAL]
go

create procedure ListarAmortVenta(
@IdDocumento int
) AS 

SELECT a.[Id_Amort_Venta]
      ,a.[Monto]
      ,a.[NumeroDocumento]
      ,a.[IdFormaPago]
      ,a.[FechaPago]
      ,a.[IdTipoAmortizacion]
      ,a.[Observacion]
      ,a.[IdEstado]
      ,a.[Id_Venta]
      ,a.[Id_Cliente]
      ,a.[Id_Personal]
      ,a.[UsuarioCreacion]
      ,a.[FechaCreacion]
      ,a.[UsuarioModificacion]
      ,a.[FechaModificacion]
      ,a.[id_caja]
      ,a.[IdDocumento]
	  , a.Monto + v.Total_Saldo as [SaldoMaximoVenta]
  FROM [Tb_Amort_Venta] a
  inner join [Tb_Venta] v on v.Id_Venta = a.Id_Venta 
  where IdDocumento = @IdDocumento 
  and a.IdEstado not in ('ANL')
  and v.IdEstado not in ('ANL')
  go


