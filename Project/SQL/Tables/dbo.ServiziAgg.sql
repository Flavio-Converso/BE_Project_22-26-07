CREATE TABLE [dbo].[ServiziAgg] (
    [IdServizioAgg] INT           IDENTITY (1, 1) NOT NULL,
    [Descrizione]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdServizioAgg] ASC),
    UNIQUE NONCLUSTERED ([Descrizione] ASC)
);

