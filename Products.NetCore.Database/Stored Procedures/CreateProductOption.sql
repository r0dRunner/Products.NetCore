CREATE PROCEDURE [dbo].[CreateProductOption]
	@Id UNIQUEIDENTIFIER = NULL,
	@ProductId UNIQUEIDENTIFIER,
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX) = NULL
AS
BEGIN
	IF (@Id IS NULL)
		INSERT INTO [dbo].[ProductOption]([ProductId], [Name], [Description])
		OUTPUT INSERTED.Id
		VALUES(@ProductId, @Name, @Description)
	ELSE
		INSERT INTO [dbo].[ProductOption]([Id], [ProductId], [Name], [Description])
		OUTPUT INSERTED.Id
		VALUES(@Id, @ProductId, @Name, @Description)

	RETURN 0
END