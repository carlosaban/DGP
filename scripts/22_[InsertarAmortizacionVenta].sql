use [DVGP_CITAVAL]
go

create     PROCEDURE [dbo].[InsertarAmortizacionVenta](

@decMonto		DECIMAL(8,2)

,@varNumeroDocumento	VARCHAR(20)

,@varObservacion	VARCHAR(100)

,@varIdEstado		VARCHAR(5)

,@intIdVenta		INT

,@intIdCliente		INT = NULL

,@intIdPersonal		INT

,@intIdUsuarioCreacion	INT

, @intIdCaja INT
,@intIdDocumento INT
)

AS

BEGIN

	SET NOCOUNT OFF

	INSERT INTO dbo.Tb_Amort_Venta (

		Monto

		,NumeroDocumento

		,IdFormaPago

		,FechaPago

		,IdTipoAmortizacion

		,Observacion

		,IdEstado

		,Id_Venta

		,Id_Cliente

		,Id_Personal

		,UsuarioCreacion

		,FechaCreacion

		,Id_Caja
		,IdDocumento

	) VALUES (

		@decMonto

		,@varNumeroDocumento

		,''

		,GETDATE()

		,'AMR'

		,@varObservacion

		,@varIdEstado

		,@intIdVenta

		,@intIdCliente

		,@intIdPersonal

		,@intIdUsuarioCreacion

		,GETDATE()

		,@intIdCaja
		,@intIdDocumento
	);

END



