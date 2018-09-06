
USE [DVGP_CITAVAL]
GO

declare @debug int
declare @parametro int
 
set @debug  = -1

INSERT INTO [dbo].[Tb_Parametro]
           ([Sigla]
           ,[Nombre]
           ,[UsuarioCreacion]
           ,[FechaCreacion]
           ,[UsuarioModificacion]
           ,[FechaModificacion])

SELECT 
      'BNC'
      ,'Entidad Bancaria'
      ,1
      ,getdate()
      ,null
      ,null
 where 1=@debug

 --------------------------------
 SELECT @parametro = CAST(scope_identity() AS int)


INSERT INTO [dbo].[Tb_ParametroDetalle]
           ([Id_Parametro]
           ,[Valor]
           ,[Texto]
           ,[Orden]
           ,[UsuarioCreacion]
           ,[FechaCreacion]
           ,[UsuarioModificacion]
           ,[FechaModificacion]
           ,[IdParametroDetallePadre]
		   
		   )

SELECT @parametro
      ,'BCP'
      ,'Banco de Credito'
      ,1
      ,1
      ,getdate()
      ,1
      ,getdate()
      ,null
where 1 = @debug
union all
SELECT @parametro
      ,'IBK'
      ,'Banco Interbank'
      ,2
      ,1
      ,getdate()
      ,1
      ,getdate()
      ,null
where 1 = @debug
union all
SELECT @parametro
      ,'BBVA'
      ,'Banco Continental'
      ,3
      ,1
      ,getdate()
      ,1
      ,getdate()
      ,null
where 1 = @debug
union all
SELECT @parametro
      ,'BIF'
      ,'B. Interamericano Finanzas'
      ,4
      ,1
      ,getdate()
      ,1
      ,getdate()
      ,null
where 1 = @debug
union all
SELECT @parametro
      ,'Otro'
      ,'Otra entidad'
      ,5
      ,1
      ,getdate()
      ,1
      ,getdate()
      ,null
where 1 = @debug


