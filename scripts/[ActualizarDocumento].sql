USE [DVGP_CITAVAL]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarDocumento]    Script Date: 02/09/2018 10:40:19 p.m. ******/
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
,@IdFormaPago varchar(10)= 'EFE'
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
      ,[IdFormaPago] = @IdFormaPago
      ,[Observacion] = @observacion
 WHERE IdDocumento = @IdDocumento

END
