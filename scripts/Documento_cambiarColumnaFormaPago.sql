ALTER TABLE [dbo].[Tb_documento] ALTER COLUMN [IdTipoPago]  varchar (10)
go
EXEC sp_RENAME 'Tb_documento.IdTipoPago', 'IdFormaPago', 'COLUMN'
go
ALTER TABLE [dbo].[Tb_documento] ALTER COLUMN [IdEntidadBancaria]  varchar (10)
go
EXEC sp_RENAME 'Tb_documento.IdEntidadBancaria', 'IdBanco', 'COLUMN'
go
EXEC sp_RENAME 'Tb_documento.CodigoReferencia', 'NumeroRecibo', 'COLUMN'
go

ALTER TABLE dbo.Tb_documento ADD NumeroOperacion varchar(50) NULL
GO
ALTER TABLE dbo.Tb_documento ADD IdProveedor int NULL
GO


