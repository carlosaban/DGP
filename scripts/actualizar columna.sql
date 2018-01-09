ALTER TABLE Tb_documento ALTER COLUMN IdEstado varchar(3)
go

EXEC sp_rename 'Tb_documento.IdEstado', 'IdEstado', 'COLUMN'
go

update Tb_documento set
IdEstado = 'REG'
where IdEstado='1'

update Tb_documento set
IdEstado = 'ANL'
where IdEstado='0'
