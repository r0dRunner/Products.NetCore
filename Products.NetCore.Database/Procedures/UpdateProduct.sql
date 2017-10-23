CREATE PROCEDURE [dbo].[UpdateProduct]
	@Id UNIQUEIDENTIFIER,
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX) = NULL,
	@Price DECIMAL(18, 2),
	@DeliveryPrice DECIMAL(18, 2)
AS
BEGIN
	UPDATE [dbo].[Product]
	SET [Name]=@Name,
		[Description]=@Description,
		[Price]=@Price,
		[DeliveryPrice]=@DeliveryPrice
	WHERE [Id]=@Id

	RETURN 0
END