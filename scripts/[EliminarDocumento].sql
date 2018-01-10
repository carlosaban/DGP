



<<<<<<< HEAD
alter   PROCEDURE [dbo].[EliminarDocumento](
=======
ALTER  PROCEDURE [dbo].[EliminarDocumento](
>>>>>>> 807eba88f591034e66f1bb47dc8098b7e41e360c

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






