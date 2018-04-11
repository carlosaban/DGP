use [DVGP_CITAVAL]
go

update [dbo].[Tb_documento] set
[UsuarioModificacion] = null,
[UsuarioCreacion] = null ,
[UsuarioEliminacion] = null

ALTER TABLE [Tb_documento] DROP COLUMN [UsuarioModificacion];
ALTER TABLE [Tb_documento] add  [UsuarioModificacion] int;
ALTER TABLE [Tb_documento] DROP COLUMN [UsuarioCreacion];
ALTER TABLE [Tb_documento] add  [UsuarioCreacion] int;
ALTER TABLE [Tb_documento] DROP COLUMN [UsuarioEliminacion];
ALTER TABLE [Tb_documento] add  [UsuarioEliminacion] int;
go