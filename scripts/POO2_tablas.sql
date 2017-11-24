USE [Negocios2017]
GO

/****** Object:  Table [dbo].[Tb_Clientes]    Script Date: 23/11/2017 06:25:35 p.m. ******/
DROP TABLE [dbo].[Tb_Clientes]
GO

/****** Object:  Table [dbo].[tb_pedidoscabe]    Script Date: 23/11/2017 06:25:35 p.m. ******/
DROP TABLE [dbo].[tb_pedidoscabe]
GO

/****** Object:  Table [dbo].[tb_pedidosdeta]    Script Date: 23/11/2017 06:25:35 p.m. ******/
DROP TABLE [dbo].[tb_pedidosdeta]
GO

/****** Object:  Table [dbo].[tb_pedidosdeta]    Script Date: 23/11/2017 06:25:35 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tb_pedidosdeta](
	[IdPedido] [varchar](50) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[idPedidoDetalle] [varchar](50) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[tb_pedidoscabe]    Script Date: 23/11/2017 06:25:35 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tb_pedidoscabe](
	[IdPedido] [varchar](50) NOT NULL,
	[FechaPedido] [date] NULL,
	[DireccionDestinatario] [varchar](100) NULL,
	[PaisDestinatario] [varchar](50) NULL,
	[IdCliente] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Tb_Clientes]    Script Date: 23/11/2017 06:25:35 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tb_Clientes](
	[idCliente] [varchar](50) NOT NULL,
	[NombreCia] [varchar](100) NULL,
	[Direccion] [varchar](100) NULL,
	[IdPais] [varchar](100) NULL,
	[Telefono] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


