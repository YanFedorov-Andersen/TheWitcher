CREATE TABLE [dbo].[WeaponsType] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [WeaponsType]         NVARCHAR (20)  NULL,
    [TypeCharacteristics] NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

