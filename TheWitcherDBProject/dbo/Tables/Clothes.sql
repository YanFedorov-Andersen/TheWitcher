CREATE TABLE [dbo].[Clothes] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ClothesType]        INT            NULL,
    [PriceOfBuy]         SMALLMONEY     NULL,
    [ClothesAccessLevel] INT            DEFAULT ((1)) NULL,
    [ClothesWeight]      INT            NULL,
    [Characteristics]    NVARCHAR (200) NULL,
    [Colour]             NVARCHAR (10)  DEFAULT ('grey') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([PriceOfBuy]>(0)),
    FOREIGN KEY ([ClothesType]) REFERENCES [dbo].[ClothesType] ([Id])
);

