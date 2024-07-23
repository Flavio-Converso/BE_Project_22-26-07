CREATE PROCEDURE GetNextNumProgressivo
    @Anno INT,
    @NextNumProgressivo INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Ottieni il numero progressivo massimo per l'anno corrente
    SELECT @NextNumProgressivo = ISNULL(MAX(NumProgressivo), 0) + 1
    FROM [dbo].[Prenotazioni]
    WHERE Anno = @Anno;
END