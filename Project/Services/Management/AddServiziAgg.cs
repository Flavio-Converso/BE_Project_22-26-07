using Project.Models;

namespace Project.Services.Management
{
    public class AddServiziAgg : BaseService, IAddServiziAgg
    {
        private readonly ILogger<AddServiziAgg> _logger;

        private const string ADD_SERVIZI_AGG_COMMAND = @"
            INSERT INTO PrenotazioniServiziAgg
            (IdPrenotazione, IdServizioAgg, Data, Quantita, Prezzo) 
            VALUES (@IdPrenotazione, @IdServizioAgg, @Data, @Quantita, @Prezzo)";
        private const string GET_SERVIZIAGG = "SELECT * FROM ServiziAgg";
        public AddServiziAgg(IConfiguration configuration, ILogger<AddServiziAgg> logger)
            : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }

        public PrenotazioneServizioAgg AddServizioAgg(PrenotazioneServizioAgg prenotazioneServizioAgg, int idPrenotazione)
        {
            try
            {
                ExecuteNonQuery(
                    ADD_SERVIZI_AGG_COMMAND,
                    command =>
                    {
                        command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);
                        command.Parameters.AddWithValue("@IdServizioAgg", prenotazioneServizioAgg.IdServizioAgg);
                        command.Parameters.AddWithValue("@Data", prenotazioneServizioAgg.Data);
                        command.Parameters.AddWithValue("@Quantita", prenotazioneServizioAgg.Quantita);
                        command.Parameters.AddWithValue("@Prezzo", prenotazioneServizioAgg.Prezzo);
                    });

                return prenotazioneServizioAgg;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta di un servizio aggiuntivo.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public List<ServizioAgg> GetServiziAgg()
        {
            try
            {
                return ExecuteReader(GET_SERVIZIAGG, null, reader => new ServizioAgg
                {
                    IdServizioAgg = reader.GetInt32(0),
                    Descrizione = reader.GetString(1),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei servizi aggiuntivi.");
                return null;
            }
        }
    }
}
