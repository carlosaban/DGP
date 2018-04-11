use [DVGP_CITAVAL]
go

ALTER TABLE [dbo].[Tb_documento] ADD  CONSTRAINT [DF_Tb_documento_IdEstado]  DEFAULT ('REG') FOR [IdEstado]
GO


