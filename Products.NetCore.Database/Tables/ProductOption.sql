CREATE TABLE [dbo].[ProductOption] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_ProductOptionId] DEFAULT (newsequentialid()) NOT NULL,
    [ProductId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductOption_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE
);