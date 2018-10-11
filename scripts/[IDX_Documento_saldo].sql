USE [DVGP_CITAVAL]
GO

/****** Object:  Index [IDX_Documento_saldo]    Script Date: 01/10/2018 10:15:13 a.m. ******/
DROP INDEX [IDX_Documento_saldo] ON [dbo].[Tb_documento]
GO

/****** Object:  Index [IDX_Documento_saldo]    Script Date: 01/10/2018 10:15:13 a.m. ******/
CREATE NONCLUSTERED INDEX [IDX_Documento_saldo] ON [dbo].[Tb_documento]
(
	[IdCliente] ASC,
	[Fecha] ASC,
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


