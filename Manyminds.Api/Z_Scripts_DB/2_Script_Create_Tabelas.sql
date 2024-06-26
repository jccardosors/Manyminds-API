USE [ManymindsBD]
GO
/****** Object:  Table [dbo].[Tab_Fornecedor]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_Fornecedor](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NULL,
 CONSTRAINT [PK_Tab_Fornecedor] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tab_PedidoCompra]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_PedidoCompra](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[FornecedorCodigo] [int] NULL,
	[Data] [datetime] NULL,
	[Observacao] [varchar](max) NULL,
	[Status] [int] NULL,
	[ValorTotal] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Tab_PedidoCompra] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tab_PedidoCompraItem]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_PedidoCompraItem](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[PedidoCompraCodigo] [int] NULL,
	[Quantidade] [int] NULL,
	[ProdutoCodigo] [int] NULL,
 CONSTRAINT [PK_Tab_PedidoCompraItem] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tab_Produto]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_Produto](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[FornecedorCodigo] [int] NULL,
	[Nome] [varchar](150) NULL,
	[Valor] [decimal](18, 2) NULL,
	[Ativo] [bit] NULL,
 CONSTRAINT [PK_Tab_Produto] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tab_RegistroLogs]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_RegistroLogs](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioCodigo] [int] NULL,
	[Descricao] [varchar](max) NULL,
	[Data] [datetime] NULL,
 CONSTRAINT [PK_Tab_RegistroLogs] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tab_Usuario]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_Usuario](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NULL,
	[Senha] [varchar](50) NULL,
 CONSTRAINT [PK_Tab_Usuario] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tab_UsuarioControleAcesso]    Script Date: 10/04/2024 19:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tab_UsuarioControleAcesso](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[UltimoAcesso] [datetime] NULL,
	[NumeroTentativa] [int] NULL,
	[Bloqueado] [bit] NULL,
	[UsuarioEmail] [varchar](100) NULL,
 CONSTRAINT [PK_Tab_UsuarioControleAcesso] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tab_PedidoCompra]  WITH CHECK ADD  CONSTRAINT [FK_Tab_PedidoCompra_Tab_Fornecedor] FOREIGN KEY([FornecedorCodigo])
REFERENCES [dbo].[Tab_Fornecedor] ([Codigo])
GO
ALTER TABLE [dbo].[Tab_PedidoCompra] CHECK CONSTRAINT [FK_Tab_PedidoCompra_Tab_Fornecedor]
GO
ALTER TABLE [dbo].[Tab_PedidoCompraItem]  WITH CHECK ADD  CONSTRAINT [FK_Tab_PedidoCompraItem_Tab_PCompra] FOREIGN KEY([ProdutoCodigo])
REFERENCES [dbo].[Tab_Produto] ([Codigo])
GO
ALTER TABLE [dbo].[Tab_PedidoCompraItem] CHECK CONSTRAINT [FK_Tab_PedidoCompraItem_Tab_PCompra]
GO
ALTER TABLE [dbo].[Tab_PedidoCompraItem]  WITH CHECK ADD  CONSTRAINT [FK_Tab_PedidoCompraItem_Tab_PedidoCompraItem] FOREIGN KEY([Codigo])
REFERENCES [dbo].[Tab_PedidoCompraItem] ([Codigo])
GO
ALTER TABLE [dbo].[Tab_PedidoCompraItem] CHECK CONSTRAINT [FK_Tab_PedidoCompraItem_Tab_PedidoCompraItem]
GO
ALTER TABLE [dbo].[Tab_Produto]  WITH CHECK ADD  CONSTRAINT [FK_Tab_Produto_Tab_Fornecedor] FOREIGN KEY([FornecedorCodigo])
REFERENCES [dbo].[Tab_Fornecedor] ([Codigo])
GO
ALTER TABLE [dbo].[Tab_Produto] CHECK CONSTRAINT [FK_Tab_Produto_Tab_Fornecedor]
GO
ALTER TABLE [dbo].[Tab_RegistroLogs]  WITH CHECK ADD  CONSTRAINT [FK_Tab_RegistroLogs_Tab_Usuario] FOREIGN KEY([UsuarioCodigo])
REFERENCES [dbo].[Tab_Usuario] ([Codigo])
GO
ALTER TABLE [dbo].[Tab_RegistroLogs] CHECK CONSTRAINT [FK_Tab_RegistroLogs_Tab_Usuario]
GO
