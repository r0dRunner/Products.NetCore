CREATE TABLE [dbo].[Product] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_ProductId] DEFAULT (newsequentialid()) NOT NULL,
    [Name]          NVARCHAR (MAX)   NOT NULL,
    [Description]   NVARCHAR (MAX)   NULL,
    [Price]         DECIMAL (18, 2)  NOT NULL,
    [DeliveryPrice] DECIMAL (18, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
