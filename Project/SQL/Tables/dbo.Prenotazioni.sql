CREATE TABLE [dbo].[Prenotazioni] (
    [IdPrenotazione]   INT             IDENTITY (1, 1) NOT NULL,
    [DataPrenotazione] DATETIME2 (7)   NOT NULL,
    [NumProgressivo]   INT             NOT NULL,
    [Anno]             INT             NOT NULL,
    [SoggiornoDal]     DATETIME2 (7)   NOT NULL,
    [SoggiornoAl]      DATETIME2 (7)   NOT NULL,
    [Caparra]          DECIMAL (18, 2) NOT NULL,
    [Tariffa]          DECIMAL (18, 2) NOT NULL,
    [TipoPensione]     VARCHAR (50)    NULL,
    [IdPersona]        INT             NOT NULL,
    [IdCamera]         INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPrenotazione] ASC),
    FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[Persone] ([IdPersona]),
    CHECK ([TipoPensione]='Prima Colazione' OR [TipoPensione]='Pensione Completa' OR [TipoPensione]='Mezza Pensione'),
    CONSTRAINT [CHK_SoggiornoDates] CHECK ([SoggiornoAl]>[SoggiornoDal])
);

