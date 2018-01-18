USE [DVGP_CITAVAL]
GO

ALTER TABLE [dbo].[Tb_documento] ADD  CONSTRAINT [DF_Tb_documento_IdEstado]  DEFAULT ('REG') FOR [IdEstado]
GO


