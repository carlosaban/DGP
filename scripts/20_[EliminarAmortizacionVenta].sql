
create     PROCEDURE [dbo].[EliminarAmortizacionVenta](

@idAmortizacionVenta		DECIMAL(8,2)
)

AS

BEGIN

	SET NOCOUNT OFF

	update dbo.Tb_Amort_Venta 

		set IdEstado = 'ANL' WHERE Id_Amort_Venta = @idAmortizacionVenta

-- actualizar la venta afectada por la eliminacion de la amortizacion
declare @intIdVenta int 

select @intIdVenta   = a.Id_Venta 
from dbo.Tb_Amort_Venta a
where a.IdDocumento =@idAmortizacionVenta 

exec [dbo].[DGP_Insertar_Venta_Final] @intIdVenta

END

