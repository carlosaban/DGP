/*
   lunes, 15 de octubre de 201805:20:53 p.m.
   User: sa
   Server: USUARIO-PC\SQLEXPRESS2012
   Database: DVGP_CITAVAL
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
ALTER TABLE dbo.Tb_Prod_x_Cliente ADD
	TipoPrecio int NULL
GO
ALTER TABLE dbo.Tb_Prod_x_Cliente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
