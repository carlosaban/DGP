use [DVGP_CITAVAL]
go

CREATE TABLE [dbo].[tb_Archivo](
	[IdArchivo] [int] NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Extencion] [varchar](10) NOT NULL,
	[stream] [varbinary](max) NULL,
	[EsEliminado] [bit] NULL,
 CONSTRAINT [PK_tb_Archivo] PRIMARY KEY CLUSTERED 
(
	[IdArchivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


