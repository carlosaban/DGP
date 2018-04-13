use [DVGP_CITAVAL]
go


create   PROCEDURE [dbo].[ActualizarDocumento](
@IdDocumento int
,@IdTipoDocumento varchar(10)
,@Fecha datetime
,@Monto decimal(18,2)
,@IdCaja int
,@IdCliente int
,@IdPersonal int
,@IdTipoPago varchar(10)= 'EFE'
,@observacion varchar(100) = null
) AS

BEGIN

UPDATE [dbo].[Tb_documento]
   SET [IdTipoDocumento] = @IdTipoDocumento
      ,[Fecha] = @Fecha
      ,[Monto] = @Monto
      ,[FechaModificacion] = getdate()
      --,[UsuarioModificacion] = getdate()
      ,[IdCliente] = @IdCliente
      ,[IdPersonal] = @IdPersonal
      ,[IdCaja] = @IdCaja
      ,[IdTipoPago] = @IdTipoPago
      ,[Observacion] = @observacion
 WHERE IdDocumento = @IdDocumento

END
GO