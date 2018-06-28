USE [DVGP_CITAVAL]
GO

ALTER TABLE [dbo].[TbCompra] DROP CONSTRAINT [FK_TbCompra_Tb_Empresa]
GO

ALTER TABLE [dbo].[TbCompra] DROP CONSTRAINT [FK_TbCompra_Tb_Caja]
GO

ALTER TABLE [dbo].[TbCompra] DROP CONSTRAINT [DF_TbCompra_FechaCreacion]
GO

ALTER TABLE [dbo].[TbCompra] DROP CONSTRAINT [DF_TbCompra_TotalJabas]
GO

/****** Object:  Table [dbo].[TbCompra]    Script Date: 11/05/2018 04:42:42 p.m. ******/
DROP TABLE [dbo].[TbCompra]
GO

/****** Object:  Table [dbo].[TbCompra]    Script Date: 11/05/2018 04:42:42 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TbCompra](
	[IdCompra] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoDocumentoCompra] [varchar](5) NULL,
	[NumeroDocumento] [varchar](100) NULL,
	[TotalJabas] int NOT NULL,
	[TotalPeso_Bruto] [decimal](18, 2) NOT NULL,
	[TotalPeso_Tara] [decimal](18, 2) NOT NULL,
	[TotalPeso_Neto] [decimal](18, 2) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[MontoSubTotal] [decimal](18, 2) NOT NULL,
	[MontoIgv] [decimal](18, 2) NOT NULL,
	[MontoTotal] [decimal](18, 2) NOT NULL,
	[TotalDevolucion] [decimal](18, 2) NULL,
	[TotalAmortizacion] [decimal](18, 2) NULL,
	[TotalSaldo] [decimal](18, 2) NULL,
	[Observacion] [varchar](500) NULL,
	[IdEstado] [varchar](3) NULL,
	[IdCaja] [int] NOT NULL,
	[IdEmpresa] [int] NULL,
	[IdProducto] [int] NOT NULL,
	[IdProveedor] [int] NULL,
	[IdPersonal] [int] NOT NULL,
	[UsuarioCreacion] [int] NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioModificacion] [int] NULL,
	[FechaModificacion] [datetime] NULL,
	[UsuarioEliminacion] [int] NULL,
	[FechaEliminacion] [datetime] NULL,
	[TotalUnidades] [int] NULL,
	[Fecha] [date] NULL,
 CONSTRAINT [PK__TbCompra__07F6335A] PRIMARY KEY CLUSTERED 
(
	[IdCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TbCompra] ADD  CONSTRAINT [DF_TbCompra_TotalJabas]  DEFAULT ((0)) FOR [TotalJabas]
GO

ALTER TABLE [dbo].[TbCompra] ADD  CONSTRAINT [DF_TbCompra_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO

ALTER TABLE [dbo].[TbCompra]  WITH CHECK ADD  CONSTRAINT [FK_TbCompra_Tb_Caja] FOREIGN KEY([IdCaja])
REFERENCES [dbo].[Tb_Caja] ([Id_Caja])
GO

ALTER TABLE [dbo].[TbCompra] CHECK CONSTRAINT [FK_TbCompra_Tb_Caja]
GO

ALTER TABLE [dbo].[TbCompra]  WITH CHECK ADD  CONSTRAINT [FK_TbCompra_Tb_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Tb_Empresa] ([Id_Empresa])
GO

ALTER TABLE [dbo].[TbCompra] CHECK CONSTRAINT [FK_TbCompra_Tb_Empresa]
GO


