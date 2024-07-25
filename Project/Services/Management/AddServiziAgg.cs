using Project.Models;

namespace Project.Services.Management
{
    public class AddServiziAgg : BaseService, IAddServiziAgg
    {
        private readonly ILogger<AddServiziAgg> _logger;

        // SQL command for inserting a new record
        private const string ADD_SERVIZI_AGG_COMMAND = @"
            INSERT INTO PrenotazioniServiziAgg
            (IdPrenotazione, IdServizioAgg, Data, Quantita, Prezzo) 
            VALUES (@IdPrenotazione, @IdServizioAgg, @Data, @Quantita, @Prezzo)";

        public AddServiziAgg(IConfiguration configuration, ILogger<AddServiziAgg> logger)
            : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }

        public PrenotazioneServizioAgg AddServizioAgg(PrenotazioneServizioAgg prenotazioneServizioAgg, int idPrenotazione)
        {
            try
            {
                // Execute the SQL command with parameters
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

                // Return the added record
                return prenotazioneServizioAgg;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta di un servizio aggiuntivo.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
    }
}
