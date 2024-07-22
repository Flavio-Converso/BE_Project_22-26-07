CREATE TABLE [dbo].[Camere] (
    [IdCamera]     INT           IDENTITY (1, 1) NOT NULL,
    [NumeroCamera] INT           NOT NULL,
    [Descrizione]  VARCHAR (255) NULL,
    [Tipologia]    VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([IdCamera] ASC),
    UNIQUE NONCLUSTERED ([NumeroCamera] ASC),
    CHECK ([Tipologia]='doppia' OR [Tipologia]='singola')
);

