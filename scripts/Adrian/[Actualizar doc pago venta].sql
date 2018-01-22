USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarDocumento]    Script Date: 14/01/2018 19:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER   PROCEDURE [dbo].[ActualizarDocumento](
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
      ,[UsuarioModificacion] = getdate()
      ,[IdCliente] = @IdCliente
      ,[IdPersonal] = @IdPersonal
      ,[IdCaja] = @IdCaja
      ,[IdTipoPago] = @IdTipoPago
      ,[Observacion] = @observacion
 WHERE IdDocumento = @IdDocumento

END


select top 10 *  from Tb_documento order by IdDocumento desc