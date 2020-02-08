CREATE TABLE [dbo].[BackLogItems] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [SprintId]          INT            NOT NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [Priority]          NVARCHAR (MAX) NULL,
    [Status]            NVARCHAR (MAX) NULL,
    [WorksOnItusername] NVARCHAR (20)  NULL,
    [EstimatedTime]     INT            NULL,
    [ActualTime]        INT            NOT NULL,
    [Name]              NVARCHAR (450) NULL,
    CONSTRAINT [PK_BackLogItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BackLogItems_Users_WorksOnItusername] FOREIGN KEY ([WorksOnItusername]) REFERENCES [dbo].[Users] ([username]),
    CONSTRAINT [FK_BackLogItems_Projects_Name] FOREIGN KEY ([Name]) REFERENCES [dbo].[Projects] ([Name]),
    CONSTRAINT [FK_BackLogItems_Sprints_SprintId] FOREIGN KEY ([SprintId]) REFERENCES [dbo].[Sprints] ([SprintId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_BackLogItems_Name]
    ON [dbo].[BackLogItems]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BackLogItems_WorksOnItusername]
    ON [dbo].[BackLogItems]([WorksOnItusername] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BackLogItems_SprintId]
    ON [dbo].[BackLogItems]([SprintId] ASC);

