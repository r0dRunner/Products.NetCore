CREATE PROCEDURE [dbo].[UpdateProductOption]
	@Id UNIQUEIDENTIFIER,
	@ProductId UNIQUEIDENTIFIER,
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX) = NULL
AS
BEGIN
	UPDATE [dbo].[ProductOption]
	SET [ProductId]=@ProductId,
		[Name]=@Name,
		[Description]=@Description
	WHERE [Id]=@Id

	RETURN 0
END