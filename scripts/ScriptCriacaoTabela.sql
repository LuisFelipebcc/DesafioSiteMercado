USE [db_desafiositemercado]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 04/03/2021 18:18:30 ******/
DROP TABLE IF EXISTS [dbo].[Produto]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 04/03/2021 18:18:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nchar](100) NOT NULL,
	[ValorVenda] [decimal](10, 2) NOT NULL,
	[Imagem] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
