use [DVGP_CITAVAL]
go

ALTER TABLE [dbo].[Tb_documento] DROP CONSTRAINT [DF__Tb_docume__estad__4589517F]



ALTER TABLE Tb_documento ALTER COLUMN Estado varchar(3)
go

EXEC sp_rename 'Tb_documento.Estado', 'IdEstado', 'COLUMN'
go

update Tb_documento set
IdEstado = 'REG'
where IdEstado='1'

update Tb_documento set
IdEstado = 'ANL'
where IdEstado='0'
