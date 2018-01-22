



ALTER  PROCEDURE [dbo].[EliminarDocumento](

@IdDocumento int

,@Usuario int

,@observacion varchar(100) = null

) AS

DECLARE @TmpVentas TABLE ( posicion int identity(1,1), IdVenta INT )



BEGIN

insert into @TmpVentas (IdVenta)
select a.Id_Venta 
from dbo.Tb_Amort_Venta a
where  a.IdEstado not in ('ANL') 
and a.IdDocumento =@IdDocumento 


UPDATE [dbo].[Tb_documento]

   SET [IdEstado] = dbo.DGP_VENTA_ESTADO_ANULADO() 
       ,[Observacion] = @observacion
	   ,[FechaEliminacion] = GETDATE()
	   ,[UsuarioEliminacion] = @Usuario
WHERE IdDocumento = @IdDocumento

UPDATE [dbo].[Tb_Amort_Venta] SET 
		[IdEstado] = dbo.DGP_VENTA_ESTADO_ANULADO() 
	   ,[FechaEliminacion] = GETDATE()
	   ,[UsuarioEliminacion] = @Usuario
 WHERE IdDocumento = @IdDocumento


declare @maxVentas int ;
declare @i int;
set @i =1;
select @maxVentas =COUNT(*) from @TmpVentas
while (@i <=@maxVentas ) 
	begin
	 select @intIdVenta = IdVenta from @TmpVentas where posicion =@i
	 execute dbo.DGP_Insertar_Venta_Final @intIdVenta, null ;
	 set @i = @i + 1 ;
	end;

END






