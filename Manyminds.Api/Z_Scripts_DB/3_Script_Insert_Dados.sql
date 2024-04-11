USE [ManymindsBD]
GO

INSERT INTO [dbo].[Tab_Fornecedor]([Nome])VALUES('Nike');
INSERT INTO [dbo].[Tab_Fornecedor]([Nome])VALUES('Adidas');
INSERT INTO [dbo].[Tab_Fornecedor]([Nome])VALUES('Olympicus');

INSERT INTO [dbo].[Tab_Usuario]([Email],[Senha]) VALUES ('teste@manyminsd.com.br','senha123');

INSERT INTO [dbo].[Tab_Produto]([FornecedorCodigo],[Nome],[Valor],[Ativo])VALUES(1 ,'Tennis', 199.99,0);
INSERT INTO [dbo].[Tab_Produto]([FornecedorCodigo],[Nome],[Valor],[Ativo])VALUES(2 ,'Camisa', 120.01,0);
INSERT INTO [dbo].[Tab_Produto]([FornecedorCodigo],[Nome],[Valor],[Ativo])VALUES(3 ,'Bermuda', 78.60,0);
INSERT INTO [dbo].[Tab_Produto]([FornecedorCodigo],[Nome],[Valor],[Ativo])VALUES(1 ,'Meias', 39.99,0);

GO


