use [DVGP_CITAVAL]
go

CREATE     PROCEDURE [dbo].[ActualizarAmortizacionVenta](

@decMonto		DECIMAL(8,2)

,@varNumeroDocumento	VARCHAR(20)

,@varObservacion	VARCHAR(100)

,@varIdEstado		VARCHAR(5)

,@intIdVenta		INT

,@intIdPersonal		INT


,@intIdAmortVenta	INT
,@intIdUsuarioModificacion int
)

AS

BEGIN

	SET NOCOUNT OFF
	update dbo.Tb_Amort_Venta set Monto = @decMonto,
	NumeroDocumento = @varNumeroDocumento,Observacion=@varObservacion,
	IdEstado=@varIdEstado,Id_Venta=@intIdVenta,
	Id_Personal=@intIdPersonal,UsuarioModificacion=@intIdUsuarioModificacion where Id_Amort_Venta =@intIdAmortVenta

	
END

