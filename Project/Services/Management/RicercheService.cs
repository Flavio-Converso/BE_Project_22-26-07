using Project.Models;
using System.Data.SqlClient;

namespace Project.Services.Management
{
    public class RicercheService : BaseService, IRicercheService
    {
        private const string RICERCA_PRENOTAZIONE_BY_CF_COMMAND = @"
            SELECT p.*
            FROM Prenotazioni p
            INNER JOIN Persone per ON p.IdPersona = per.IdPersona
            WHERE per.CF = @CodiceFiscale";

        private const string RICERCA_PRENOTAZIONE_BY_TIPO_PENSIONE_COMMAND = @"
            SELECT p.*
            FROM Prenotazioni p
            WHERE p.TipoPensione = @TipoPensione";

        private readonly ILogger<RicercheService> _logger;

        public RicercheService(IConfiguration configuration, ILogger<RicercheService> logger)
            : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }

        public async Task<List<Prenotazione>> GetPrenotazioniByCFAsync(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                throw new ArgumentException("Il codice fiscale non può essere nullo o vuoto.", nameof(codiceFiscale));
            }

            return await ExecuteQueryAsync(
                RICERCA_PRENOTAZIONE_BY_CF_COMMAND,
                command => command.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale)
            );
        }

        public async Task<List<Prenotazione>> GetPrenotazioniByTipoPensioneAsync(string tipoPensione)
        {
            if (string.IsNullOrWhiteSpace(tipoPensione))
            {
                throw new ArgumentException("Il tipo di pensione non può essere nullo o vuoto.", nameof(tipoPensione));
            }

            var validPensionTypes = new HashSet<string> { "Prima Colazione", "Pensione Completa", "Mezza Pensione" };
            if (!validPensionTypes.Contains(tipoPensione))
            {
                throw new ArgumentException($"Il tipo di pensione '{tipoPensione}' non è valido. I tipi validi sono: {string.Join(", ", validPensionTypes)}.");
            }

            return await ExecuteQueryAsync(
                RICERCA_PRENOTAZIONE_BY_TIPO_PENSIONE_COMMAND,
                command => command.Parameters.AddWithValue("@TipoPensione", tipoPensione)
            );
        }

        private async Task<List<Prenotazione>> ExecuteQueryAsync(string query, Action<SqlCommand> parameterizeCommand)
        {
            Func<SqlDataReader, Prenotazione> readAction = reader => new Prenotazione
            {
                IdPrenotazione = reader.GetInt32(reader.GetOrdinal("IdPrenotazione")),
                DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione")),
                NumProgressivo = reader.GetInt32(reader.GetOrdinal("NumProgressivo")),
                Anno = reader.GetInt32(reader.GetOrdinal("Anno")),
                SoggiornoDal = reader.GetDateTime(reader.GetOrdinal("SoggiornoDal")),
                SoggiornoAl = reader.GetDateTime(reader.GetOrdinal("SoggiornoAl")),
                Caparra = reader.GetDecimal(reader.GetOrdinal("Caparra")),
                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                TipoPensione = reader.IsDBNull(reader.GetOrdinal("TipoPensione")) ? null : reader.GetString(reader.GetOrdinal("TipoPensione")),
                IdPersona = reader.GetInt32(reader.GetOrdinal("IdPersona")),
                IdCamera = reader.GetInt32(reader.GetOrdinal("IdCamera"))
            };

            try
            {
                return await ExecuteReaderAsync(
                    query,
                    parameterizeCommand,
                    readAction
                );
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Errore durante l'esecuzione della query.");
                throw;
            }
        }
    }
}
