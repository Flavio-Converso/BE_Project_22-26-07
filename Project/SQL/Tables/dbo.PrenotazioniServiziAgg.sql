CREATE TABLE [dbo].[PrenotazioniServiziAgg] (
    [IdPrenotazioneServizioAgg] INT             IDENTITY (1, 1) NOT NULL,
    [IdPrenotazione]            INT             NOT NULL,
    [IdServizioAgg]             INT             NOT NULL,
    [Data]                      DATETIME2 (7)   NOT NULL,
    [Quantita]                  INT             NOT NULL,
    [Prezzo]                    DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPrenotazioneServizioAgg] ASC),
    FOREIGN KEY ([IdServizioAgg]) REFERENCES [dbo].[ServiziAgg] ([IdServizioAgg])
);

