CREATE TABLE [dbo].[ClothesType] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [ClothesType]         NVARCHAR (20)  NULL,
    [TypeCharacteristics] NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

