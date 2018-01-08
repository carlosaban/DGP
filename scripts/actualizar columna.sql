ALTER TABLE Tb_documento ALTER COLUMN estado varchar(3)

EXEC sp_rename 'Tb_documento.estado', 'IdEstado', 'COLUMN'
go

update Tb_documento set
IdEstado = 'REG'
where IdEstado='1'

update Tb_documento set
IdEstado = 'ANL'
where IdEstado='0'
