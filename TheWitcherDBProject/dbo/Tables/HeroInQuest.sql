CREATE TABLE [dbo].[HeroInQuest] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [HeroId]    INT           NULL,
    [QuestId]   INT           NULL,
    [StartTime] SMALLDATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([HeroId]) REFERENCES [dbo].[Heroes] ([Id]),
    FOREIGN KEY ([QuestId]) REFERENCES [dbo].[Quest] ([Id])
);

