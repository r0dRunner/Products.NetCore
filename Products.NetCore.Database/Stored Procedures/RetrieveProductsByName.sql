CREATE PROCEDURE [dbo].[RetrieveProductsByName]
	@Name NVARCHAR(MAX) = ''
AS
BEGIN
	SET @Name = LOWER(@Name)

	SELECT *
	FROM [dbo].[Product]
	WHERE LOWER([Name]) like '%' + @Name + '%'

	RETURN 0
END
GO