
CREATE PROCEDURE [dbo].[Perfil_delete] 
     @id int = NULL
	,@usuario int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM [perfil]
    WHERE id=@id
END




GO

/****** Object:  StoredProcedure [dbo].[Perfil_insert]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		Carlos Abanto G
-- Create date: 4/7/2015
-- Description:	N/A
-- =============================================
CREATE PROCEDURE [dbo].[Perfil_insert] 
						 @id  int = NULL
						,@descripcion  varchar(255) = NULL


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO [perfil]
           ([descripcion])
     VALUES
           (
           @descripcion
           
           )

   
           SELECT @id = SCOPE_IDENTITY()
END



	






GO

/****** Object:  StoredProcedure [dbo].[Perfil_List]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Perfil_List] 
								@id  int = NULL
								,@descripcion  varchar(255) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
	,[descripcion]
	FROM [perfil]
	WHERE 1=1
	AND id = ISNULL( @id , id )
	AND descripcion = ISNULL( @descripcion , descripcion )


	
END







GO

/****** Object:  StoredProcedure [dbo].[perfil_privilegios_delete]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[perfil_privilegios_delete] 
     @perfilid int = NULL
	,@privilegiosid int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM [perfil_privilegios]
    WHERE perfilid=@perfilid AND privilegiosid = @privilegiosid
END




GO

/****** Object:  StoredProcedure [dbo].[perfil_privilegios_insert]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		Carlos Abanto G
-- Create date: 4/7/2015
-- Description:	N/A
-- =============================================
CREATE PROCEDURE [dbo].[perfil_privilegios_insert] 
						 @perfilid  int = NULL
						,@privilegiosid  int = NULL


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO [perfil_privilegios]
           ([perfilid]
           ,[privilegiosid])
     VALUES
           (@perfilid
           ,@privilegiosid
           )



END



	






GO

/****** Object:  StoredProcedure [dbo].[perfil_privilegios_List]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[perfil_privilegios_List] 
								 @perfilid  int = NULL
								,@privilegiosid  int = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT perfilid ,privilegiosid
	FROM [perfil_privilegios]
	WHERE 1=1
	AND perfilid = ISNULL( @perfilid , perfilid )
	AND privilegiosid = ISNULL( @privilegiosid , privilegiosid )


	
END







GO

/****** Object:  StoredProcedure [dbo].[perfil_privilegios_Update]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[perfil_privilegios_Update]
							@perfilid  int = NULL
							,@privilegiosid  varchar(255) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [perfil_privilegios]
	SET perfilid = @perfilid
	,privilegiosid = @privilegiosid
	WHERE  1=1
	AND perfilid = @perfilid
	AND privilegiosid = @privilegiosid





END







GO

/****** Object:  StoredProcedure [dbo].[Perfil_Update]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Perfil_Update]
							@id  int = NULL
							,@descripcion  varchar(255) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [perfil]
	SET [descripcion] = @descripcion
	WHERE  id = @id





END







GO

/****** Object:  StoredProcedure [dbo].[pregunta_delete]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[pregunta_delete] 
     @id int = NULL
	,@usuario int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [pregunta]
   SET 
      [estado] = -1
      ,[fechamodificacion] = getdate()
      ,[usuariomodificacion] = @usuario
 WHERE id = @id



END







GO

CREATE PROCEDURE [dbo].[Privilegios_delete] 
     @id int = NULL
	,@usuario int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM Privilegios
    WHERE id=@id
END





GO

/****** Object:  StoredProcedure [dbo].[Privilegios_insert]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		Carlos Abanto G
-- Create date: 4/7/2015
-- Description:	N/A
-- =============================================
CREATE PROCEDURE [dbo].[Privilegios_insert] 
						 @id  int = NULL
						,@descripcion  varchar(255) = NULL


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO Privilegios
           ([descripcion])
     VALUES
           (
           @descripcion
           
           )

   
           SELECT @id = SCOPE_IDENTITY()
END



	







GO

/****** Object:  StoredProcedure [dbo].[Privilegios_List]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Privilegios_List] 
								@id  int = NULL
								,@descripcion  varchar(255) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
	,[descripcion]
	FROM [Privilegios]
	WHERE 1=1
	AND id = ISNULL( @id , id )
	AND descripcion = ISNULL( @descripcion , descripcion )


	
END








GO

/****** Object:  StoredProcedure [dbo].[Privilegios_Update]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Privilegios_Update]
							@id  int = NULL
							,@descripcion  varchar(255) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Privilegios]
	SET [descripcion] = @descripcion
	WHERE  id = @id





END








GO

/****** Object:  StoredProcedure [dbo].[Usuario_delete]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Usuario_delete] 
     @id int = NULL
	,@usuario int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    update usuario set 
    estado = -1
    where id = @id
END






GO

/****** Object:  StoredProcedure [dbo].[Usuario_insert]    Script Date: 07/19/2015 20:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		Carlos Abanto G
-- Create date: 4/7/2015
-- Description:	N/A
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_insert] 
						 @id int =  NULL
						,@perfilid int = NULL 
						,@nombre varchar(255) =  NULL
						,@correo varchar(30) =  NULL
						,@usuario varchar(30) =  NULL
						,@password varchar(20) =  NULL
						,@estado int  = NULL
						,@fecha datetime  = NULL
						,@usuarioModificacion int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO [usuario]
           ([perfilid]
           ,[nombre]
           ,[correo]
           ,[usuario]
           ,[password]
           ,[estado]
           ,[fecha])
     VALUES
           (
			 @perfilid
			,@nombre
			,@correo
			,@usuario
			,@password
			,@estado
			,@fecha
           
           )
           SELECT @id = SCOPE_IDENTITY()
END



	








GO

/****** Object:  StoredProcedure [dbo].[Usuario_List]    Script Date: 07/19/2015 20:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE [dbo].[Usuario_List] 
								@id int out
								,@perfilid int  = NULL 
								,@nombre varchar(255) =  NULL
								,@correo varchar(30) =  NULL
								,@usuario varchar(30) =  NULL
								,@password varchar(20) =  NULL
								,@estado int  = NULL
								,@fecha datetime  = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[perfilid]
      ,[nombre]
      ,[correo]
      ,[usuario]
      ,[password]
      ,[estado]
      ,[fecha]
  FROM [usuario]
  WHERE id = isnull( @id , id )
		AND  perfilid = isnull( @perfilid , perfilid )
		AND  nombre = isnull( @nombre , nombre )
		AND  correo = isnull( @correo , correo )
		AND  usuario = isnull( @usuario , usuario )
		AND  password = isnull( @password , password )
		AND  estado = isnull( @estado , estado )
		AND  fecha = isnull( @fecha , fecha )	
		AND estado > -1
END









GO

/****** Object:  StoredProcedure [dbo].[Usuario_privilegios_delete]    Script Date: 07/19/2015 20:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Usuario_privilegios_delete] 
	  @usuarioid int =NULL
	, @privilegiosid int = NULL
	, @usuario int =NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM [usuario_privilegios]
    WHERE usuarioid=@usuarioid AND privilegiosid =  isnull(@privilegiosid, privilegiosid)


END





GO

/****** Object:  StoredProcedure [dbo].[Usuario_privilegios_insert]    Script Date: 07/19/2015 20:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		Carlos Abanto G
-- Create date: 4/7/2015
-- Description:	N/A
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_privilegios_insert] 
						  @usuarioid int
						, @privilegiosid int
						, @inicio datetime
						, @fin datetime


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO [usuario_privilegios]
           ([usuarioid]
           ,[privilegiosid]
           ,[inicio]
           ,[fin])
     VALUES
           (
            @usuarioid
			,@privilegiosid
			,@inicio
			,@fin           
           )





END



	







GO

/****** Object:  StoredProcedure [dbo].[usuario_privilegios_List]    Script Date: 07/19/2015 20:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[usuario_privilegios_List] 
								 @usuarioid int=NULL
								,@privilegiosid int=NULL
								,@inicio datetime=NULL
								,@fin datetime=NULL


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [usuarioid]
      ,[privilegiosid]
      ,[inicio]
      ,[fin]
  FROM [usuario_privilegios]
  WHERE 1=1
		AND usuarioid = isnull( @usuarioid , usuarioid )
		AND privilegiosid = isnull( @privilegiosid , privilegiosid )
		AND inicio = isnull( @inicio , inicio )
		AND fin = isnull( @fin , fin )


	
END








GO

/****** Object:  StoredProcedure [dbo].[usuario_privilegios_Update]    Script Date: 07/19/2015 20:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[usuario_privilegios_Update]
							     @usuarioid int=NULL
								,@privilegiosid int=NULL
								,@inicio datetime=NULL
								,@fin datetime=NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE  [usuario_privilegios]
    SET 
		
		 inicio = @inicio
		,fin = @fin
	WHERE 
	     usuarioid = @usuarioid
		AND privilegiosid = @privilegiosid








END








GO

/****** Object:  StoredProcedure [dbo].[Usuario_Update]    Script Date: 07/19/2015 20:30:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[Usuario_Update]
							@id int =  NULL
							,@perfilid int  = NULL 
							,@nombre varchar(255) =  NULL
							,@correo varchar(30) =  NULL
							,@usuario varchar(30) =  NULL
							,@password varchar(20) =  NULL
							,@estado int  = NULL
							,@fecha datetime  = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [usuario]
	SET 
	perfilid = @perfilid
	,nombre = @nombre
	,correo = @correo
	,usuario = @usuario
	,password = @password
	,estado = @estado
	,fecha = @fecha
	WHERE id = @id





END









GO

