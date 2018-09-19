CREATE TABLE [dbo].[HeroClothes] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [HeroId]      INT        NULL,
    [ClothesId]   INT        NULL,
    [IsActive]    BIT        DEFAULT ((0)) NULL,
    [PriceOfSell] SMALLMONEY DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([PriceOfSell]>(0)),
    FOREIGN KEY ([ClothesId]) REFERENCES [dbo].[Clothes] ([Id]),
    FOREIGN KEY ([HeroId]) REFERENCES [dbo].[Heroes] ([Id])
);

