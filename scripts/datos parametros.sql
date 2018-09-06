
update [dbo].[Tb_ParametroDetalle] set
[Valor] = 'NC'
,[Texto] = 'Nota de Credito'
,[Orden] = 2
,[FechaModificacion] = getdate()
where 1 = 1
and [Id_Item] = 18

update [dbo].[Tb_ParametroDetalle] set
[Orden] = 1
,[FechaModificacion] = getdate()
where  1=1
and [Id_Item] = 19


-------------------------
USE [DVGP_CITAVAL]
GO
update [Tb_ParametroDetalle] set

[IdParametroDetallePadre] = 19
where  1=1
and [Id_Parametro] = 7

go

INSERT INTO [Tb_ParametroDetalle]
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
     VALUES
           (7
           ,'NCR'
           ,'Nota Credito Redondeo'
           ,1
           ,1
           ,getdate()
           ,1
           ,getdate()
           ,18
		   
		   )
GO

INSERT INTO [Tb_ParametroDetalle]
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
     VALUES
           (7
           ,'DET'
           ,'Dep. a proveedor'
           ,1
           ,1
           ,getdate()
           ,1
           ,getdate()
           ,19
		   )
GO

