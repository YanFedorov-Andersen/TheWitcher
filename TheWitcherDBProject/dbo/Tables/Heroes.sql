CREATE TABLE [dbo].[Heroes] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [HeroName]        NVARCHAR (20)  NULL,
    [HeroMoney]       SMALLMONEY     DEFAULT ((200)) NULL,
    [HeroLevel]       INT            DEFAULT ((1)) NULL,
    [Playable]        BIT            DEFAULT ((1)) NULL,
    [HeroDescription] NVARCHAR (200) NULL,
    [AvailableWeight] INT            DEFAULT ((10)) NULL,
    [ReleaseDate]     SMALLDATETIME  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([HeroMoney]>(-200))
);

