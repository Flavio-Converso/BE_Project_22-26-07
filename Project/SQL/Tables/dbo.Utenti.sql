CREATE TABLE [dbo].[Utenti] (
    [IdUtente] INT           IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUtente] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

