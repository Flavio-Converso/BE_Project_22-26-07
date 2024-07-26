using Project.Models.ViewModels;

namespace Project.Services
{
    public class CheckoutService : BaseService, ICheckoutService
    {
        private readonly ILogger<CheckoutService> _logger;

        private const string GET_PRENOTAZIONE_CON_IMPORTO_DA_SALDARE_COMMAND = @"
        SELECT 
            p.IdPrenotazione,
            c.NumeroCamera,
            p.SoggiornoDal,
            p.SoggiornoAl,
            p.Tariffa,
            p.Caparra,
            sa.Descrizione AS ServizioAggiuntivo,
            psa.Data AS DataServizio,
            psa.Quantita,
            psa.Prezzo,
            (p.Tariffa - p.Caparra + COALESCE(SUM(psa.Prezzo * psa.Quantita), 0)) AS ImportoDaSaldare
        FROM 
            Prenotazioni p
        JOIN 
            Camere c ON p.IdCamera = c.IdCamera
        LEFT JOIN 
            PrenotazioniServiziAgg psa ON p.IdPrenotazione = psa.IdPrenotazione
        LEFT JOIN 
            ServiziAgg sa ON psa.IdServizioAgg = sa.IdServizioAgg
        WHERE 
            p.IdPrenotazione = @IdPrenotazione
        GROUP BY 
            p.IdPrenotazione, c.NumeroCamera, p.SoggiornoDal, p.SoggiornoAl, 
            p.Tariffa, p.Caparra, sa.Descrizione, psa.Data, psa.Quantita, psa.Prezzo
        ORDER BY 
            p.IdPrenotazione;";

        public CheckoutService(IConfiguration configuration, ILogger<CheckoutService> logger)
            : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }

        public async Task<CheckoutViewModel> GetPrenotazioneConImportoDaSaldare(int idPrenotazione)
        {
            var prenotazione = new CheckoutViewModel();

            try
            {
                await ExecuteReaderAsync(GET_PRENOTAZIONE_CON_IMPORTO_DA_SALDARE_COMMAND,
                    command => command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione),
                    reader =>
                    {
                        if (!reader.HasRows) return null;

                        prenotazione.IdPrenotazione = reader.GetInt32(reader.GetOrdinal("IdPrenotazione"));
                        prenotazione.NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera"));
                        prenotazione.SoggiornoDal = reader.GetDateTime(reader.GetOrdinal("SoggiornoDal"));
                        prenotazione.SoggiornoAl = reader.GetDateTime(reader.GetOrdinal("SoggiornoAl"));
                        prenotazione.Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa"));
                        prenotazione.Caparra = reader.GetDecimal(reader.GetOrdinal("Caparra"));
                        prenotazione.ImportoDaSaldare = reader.GetDecimal(reader.GetOrdinal("ImportoDaSaldare"));

                        if (!reader.IsDBNull(reader.GetOrdinal("ServizioAggiuntivo")))
                        {
                            var servizio = new ServizioAggViewModel
                            {
                                ServizioAgg = reader.GetString(reader.GetOrdinal("ServizioAggiuntivo")),
                                DataServizio = reader.IsDBNull(reader.GetOrdinal("DataServizio")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataServizio")),
                                Quantita = reader.IsDBNull(reader.GetOrdinal("Quantita")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Quantita")),
                                Prezzo = reader.IsDBNull(reader.GetOrdinal("Prezzo")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Prezzo"))
                            };

                            prenotazione.ServiziAgg.Add(servizio);
                        }

                        return prenotazione;
                    });

                if (prenotazione.ServiziAgg.Count == 0)
                {
                    _logger.LogInformation("No additional services found for reservation ID {IdPrenotazione}", idPrenotazione);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reservation with outstanding amount for ID {IdPrenotazione}", idPrenotazione);
            }

            return prenotazione;
        }
    }
}
