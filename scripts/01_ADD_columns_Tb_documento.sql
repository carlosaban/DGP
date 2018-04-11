/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
use [DVGP_CITAVAL]
go

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Tb_documento ADD
	Observacion varchar(200) NULL,
	IdEntidadBancaria int NULL,
	CodigoReferencia varchar(50) NULL,
	IdArchivo int NULL,
	IdTipoPago varchar(100) NULL
GO
ALTER TABLE dbo.Tb_documento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
