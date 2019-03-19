IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [dbo].[Users] (
    [Id] bigint NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [SecondName] nvarchar(max) NULL,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [dbo].[Posts] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Content] nvarchar(max) NULL,
    [CreationDate] datetime2 NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [dbo].[Comments] (
    [Id] bigint NOT NULL IDENTITY,
    [Content] nvarchar(max) NULL,
    [ParentCommentId] bigint NULL,
    [CreationDate] datetime2 NOT NULL,
    [UserId] bigint NOT NULL,
    [PostId] bigint NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Comments_ParentCommentId] FOREIGN KEY ([ParentCommentId]) REFERENCES [dbo].[Comments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Comments_ParentCommentId] ON [dbo].[Comments] ([ParentCommentId]);

GO

CREATE INDEX [IX_Comments_PostId] ON [dbo].[Comments] ([PostId]);

GO

CREATE INDEX [IX_Comments_UserId] ON [dbo].[Comments] ([UserId]);

GO

CREATE INDEX [IX_Posts_UserId] ON [dbo].[Posts] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190318083532_InitialCreate', N'2.2.2-servicing-10034');

GO

