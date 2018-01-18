



ALTER  PROCEDURE [dbo].[EliminarDocumento](

@IdDocumento int

,@Usuario int

,@observacion varchar(100) = null

) AS



BEGIN



UPDATE [dbo].[Tb_documento]

   SET [IdEstado] = 'ANL'

       ,[Observacion] = @observacion

	   ,[FechaEliminacion] = GETDATE()

	   ,[UsuarioEliminacion] = @Usuario

 WHERE IdDocumento = @IdDocumento



END






