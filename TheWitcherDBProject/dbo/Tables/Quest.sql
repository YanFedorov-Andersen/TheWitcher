CREATE TABLE [dbo].[Quest] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [QuestName]   NVARCHAR (20) NULL,
    [Award]       SMALLMONEY    NULL,
    [LeadTime]    TIME (7)      NULL,
    [AccessLevel] INT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Award]>(0))
);

