CREATE TABLE [dbo].[HeroWeapon] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [HeroId]      INT        NULL,
    [WeaponId]    INT        NULL,
    [IsActive]    BIT        DEFAULT ((0)) NULL,
    [PriceOfSell] SMALLMONEY DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([PriceOfSell]>(0)),
    FOREIGN KEY ([HeroId]) REFERENCES [dbo].[Heroes] ([Id]),
    FOREIGN KEY ([WeaponId]) REFERENCES [dbo].[Weapons] ([Id])
);

