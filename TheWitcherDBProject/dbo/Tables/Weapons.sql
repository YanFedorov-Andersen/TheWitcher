CREATE TABLE [dbo].[Weapons] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [WeaponType]        INT            NULL,
    [PriceOfBuy]        SMALLMONEY     NULL,
    [WeaponAccessLevel] INT            DEFAULT ((1)) NULL,
    [WeaponWeight]      DECIMAL (18)   NULL,
    [Characteristics]   NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([PriceOfBuy]>(0)),
    FOREIGN KEY ([WeaponType]) REFERENCES [dbo].[WeaponsType] ([Id])
);

