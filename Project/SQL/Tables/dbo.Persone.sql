CREATE TABLE [dbo].[Persone] (
    [IdPersona] INT           IDENTITY (1, 1) NOT NULL,
    [Nome]      VARCHAR (50)  NOT NULL,
    [Cognome]   VARCHAR (50)  NOT NULL,
    [CF]        VARCHAR (16)  NOT NULL,
    [Email]     VARCHAR (100) NULL,
    [Telefono]  VARCHAR (20)  NULL,
    [Cellulare] VARCHAR (20)  NULL,
    [Città]     VARCHAR (50)  NULL,
    [Provincia] CHAR (2)      NULL,
    PRIMARY KEY CLUSTERED ([IdPersona] ASC),
    UNIQUE NONCLUSTERED ([CF] ASC)
);

