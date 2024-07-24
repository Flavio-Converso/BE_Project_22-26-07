
using Project.Models;


namespace Project.Services.Management
{
    public class VisualizzaCreazioniService : BaseService, IVisualizzaCreazioniService
    {
        private const string GET_ALL_CAMERE_COMMAND =
            "SELECT IdCamera, NumeroCamera, Descrizione, Tipologia FROM [dbo].[Camere]";

        private const string GET_ALL_PERSONE_COMMAND =
            "SELECT IdPersona, Nome, Cognome, CF, Email, Telefono, Cellulare, Città, Provincia FROM [dbo].[Persone]";

        private const string GET_ALL_PRENOTAZIONI_COMMAND =
    "SELECT IdPrenotazione, DataPrenotazione, NumProgressivo, Anno, SoggiornoDal, SoggiornoAl, Caparra, Tariffa, TipoPensione, IdPersona, IdCamera " +
    "FROM [dbo].[Prenotazioni]";

        private readonly ILogger<VisualizzaCreazioniService> _logger;

        public VisualizzaCreazioniService(IConfiguration configuration, ILogger<VisualizzaCreazioniService> logger)
            : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }

        public List<Camera> GetAllCamere()
        {
            try
            {
                return ExecuteReader(
                    GET_ALL_CAMERE_COMMAND,
                    command => { /* Nessun parametro aggiuntivo */ },
                    reader => new Camera
                    {
                        IdCamera = reader.GetInt32(0),
                        NumeroCamera = reader.GetInt32(1),
                        Descrizione = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Tipologia = reader.IsDBNull(3) ? null : reader.GetString(3)
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle camere.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public List<Persona> GetAllPersone()
        {
            try
            {
                return ExecuteReader(
                    GET_ALL_PERSONE_COMMAND,
                    command => { /* Nessun parametro aggiuntivo */ },
                    reader => new Persona
                    {
                        IdPersona = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        CF = reader.GetString(3),
                        Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Cellulare = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Città = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Provincia = reader.IsDBNull(8) ? null : reader.GetString(8)
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle persone.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
        public List<Prenotazione> GetAllPrenotazioni()
        {
            try
            {
                return ExecuteReader(
                    GET_ALL_PRENOTAZIONI_COMMAND,
                    command => { /* Nessun parametro aggiuntivo */ },
                    reader => new Prenotazione
                    {
                        IdPrenotazione = reader.GetInt32(0),
                        DataPrenotazione = reader.GetDateTime(1),
                        NumProgressivo = reader.GetInt32(2),
                        Anno = reader.GetInt32(3),
                        SoggiornoDal = reader.GetDateTime(4),
                        SoggiornoAl = reader.GetDateTime(5),
                        Caparra = reader.GetDecimal(6),
                        Tariffa = reader.GetDecimal(7),
                        TipoPensione = reader.IsDBNull(8) ? null : reader.GetString(8),
                        IdPersona = reader.GetInt32(9),
                        IdCamera = reader.GetInt32(10)
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle prenotazioni.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

    }
}
