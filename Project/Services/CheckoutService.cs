using Project.Models.ViewModels;

namespace Project.Services
{
    public class CheckoutService : BaseService, ICheckoutService
    {
        private readonly ILogger<CheckoutService> _logger;
        private const string GET_STANZA_PERIODO_TARIFFA = @"
    SELECT 
    C.NumeroCamera, 
    P.SoggiornoDal, 
    P.SoggiornoAl, 
    P.Tariffa,
    P.Caparra
FROM 
    Prenotazioni AS P 
INNER JOIN 
    Camere AS C ON P.IdCamera = C.IdCamera 
WHERE 
    P.IdPrenotazione = @Id";

        private const string GET_SERVIZI_BY_PRENOTAZIONE = @"
    SELECT 
    S.Descrizione, 
    PS.Data, 
    PS.Quantita, 
    PS.Prezzo 
FROM 
    ServiziAgg AS S 
INNER JOIN 
    PrenotazioniServiziAgg AS PS ON S.IdServizioAgg = PS.IdServizioAgg 
WHERE 
    PS.IdPrenotazione = @Id";

        private const string GET_IMPORTO = @"
    SELECT 
    (p.Tariffa - p.Caparra + ISNULL(SUM(ps.Quantita * ps.Prezzo), 0)) AS ServizioPrezzo 
FROM 
    Prenotazioni AS p 
LEFT JOIN 
    PrenotazioniServiziAgg AS ps ON p.IdPrenotazione = ps.IdPrenotazione 
WHERE 
    p.IdPrenotazione = @Id 
GROUP BY 
    p.Tariffa, p.Caparra";

        public CheckoutService(IConfiguration configuration, ILogger<CheckoutService> logger)
            : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }

        public async Task<CheckoutViewModel> GetPrenotazioneConImportoDaSaldare(int idPrenotazione)
        {
            var model = new CheckoutViewModel();

            try
            {
                var stanzaDetails = await ExecuteReaderAsync<StanzaViewModel>(
                    GET_STANZA_PERIODO_TARIFFA,
                    cmd => cmd.Parameters.AddWithValue("@Id", idPrenotazione),
                    reader => new StanzaViewModel
                    {
                        NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                        SoggiornoDal = reader.GetDateTime(reader.GetOrdinal("SoggiornoDal")),
                        SoggiornoAl = reader.GetDateTime(reader.GetOrdinal("SoggiornoAl")),
                        Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                        Caparra = reader.GetDecimal(reader.GetOrdinal("Caparra"))
                    });

                if (stanzaDetails.Any())
                {
                    model.NumeroCamera = stanzaDetails.First().NumeroCamera;
                    model.SoggiornoDal = stanzaDetails.First().SoggiornoDal;
                    model.SoggiornoAl = stanzaDetails.First().SoggiornoAl;
                    model.Tariffa = stanzaDetails.First().Tariffa;
                    model.Caparra = stanzaDetails.First().Caparra;
                }

                var serviziAgg = await ExecuteReaderAsync<ServizioAggViewModel>(
                    GET_SERVIZI_BY_PRENOTAZIONE,
                    cmd => cmd.Parameters.AddWithValue("@Id", idPrenotazione),
                    reader => new ServizioAggViewModel
                    {
                        ServizioAgg = reader.GetString(reader.GetOrdinal("Descrizione")),
                        DataServizio = reader.IsDBNull(reader.GetOrdinal("Data")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Data")),
                        Quantita = reader.IsDBNull(reader.GetOrdinal("Quantita")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Quantita")),
                        Prezzo = reader.IsDBNull(reader.GetOrdinal("Prezzo")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Prezzo"))
                    });

                model.ServiziAgg = serviziAgg;

                model.ImportoDaSaldare = await ExecuteScalarAsync<decimal>(
                    GET_IMPORTO,
                    cmd => cmd.Parameters.AddWithValue("@Id", idPrenotazione));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei dettagli di prenotazione per ID {IdPrenotazione}", idPrenotazione);
                throw;
            }

            return model;
        }


    }

}
