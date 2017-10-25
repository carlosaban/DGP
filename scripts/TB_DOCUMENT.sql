USE [DVGP_CITAVAL]
GO

/****** Object:  Table [dbo].[Tb_documento]    Script Date: 20/10/2017 11:26:21 p.m. ******/
DROP TABLE [dbo].[Tb_documento]
GO

/****** Object:  Table [dbo].[Tb_documento]    Script Date: 20/10/2017 11:26:21 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tb_documento](
	[IdDocumento] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoDocumento] VARCHAR(10) not NULL,
	[Fecha] [datetime] NOT NULL,
	[Monto] [decimal](18, 2) NULL,
	[estado] [int] NULL DEFAULT 1,
	[EsEliminado] [bit] NULL,
	[FechaEliminacion] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioModificacion] [datetime] NULL,
	[UsuarioCreacion] [datetime] NULL,
	[UsuarioEliminacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


