using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project.Models;
using System.Data.SqlClient;

namespace Project.Services.Manage
{
    public class CreazioneService : BaseService, ICreazioneService
    {
        private const string CREAZIONE_PERSONA_COMMAND = "INSERT INTO [dbo].[Persone] " +
            "(Nome,Cognome,CF,Email,Telefono,Cellulare,Città,Provincia) " +
            "OUTPUT INSERTED.IdPersona " +
            "VALUES (@Nome, @Cognome, @CF,@Email,@Telefono,@Cellulare, @Città, @Provincia)";
        private const string CREAZIONE_CAMERA_COMMAND = "INSERT INTO [dbo].[Camere] " +
            "(NumeroCamera, Descrizione, Tipologia) " +
            "OUTPUT INSERTED.IdCamera " +
            "VALUES (@NumeroCamera, @Descrizione, @Tipologia)";

        private readonly ILogger<CreazioneService> _logger;

        public CreazioneService(IConfiguration configuration,  ILogger<CreazioneService> logger) : base(configuration.GetConnectionString("DB"))
        {
            _logger = logger;
        }


        public Persona CreazionePersona(Persona persona)
        {
            try
            {
                var personaId = ExecuteScalar<int>(CREAZIONE_PERSONA_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@Nome", persona.Nome);
                    command.Parameters.AddWithValue("@Cognome", persona.Cognome);
                    command.Parameters.AddWithValue("@CF", persona.CF);
                    command.Parameters.AddWithValue("@Email", persona.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", persona.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cellulare", persona.Cellulare ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Città", persona.Città ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Provincia", persona.Provincia ?? (object)DBNull.Value);
                });

                return new Persona
                {
                    IdPersona = personaId,
                    Nome = persona.Nome,
                    Cognome = persona.Cognome,
                    CF = persona.CF,
                    Email = persona.Email,
                    Telefono = persona.Telefono,
                    Cellulare = persona.Cellulare,
                    Città = persona.Città,
                    Provincia = persona.Provincia
                };
            }      
            catch (Exception ex)
            { 
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la creazione della persona.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public Camera CreazioneCamera(Camera camera)
        {
            try
            {
                var cameraId = ExecuteScalar<int>(CREAZIONE_CAMERA_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@NumeroCamera", camera.NumeroCamera);
                    command.Parameters.AddWithValue("@Descrizione", camera.Descrizione ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Tipologia", camera.Tipologia ?? (object)DBNull.Value);
                });

                return new Camera
                {
                    IdCamera = cameraId,
                    NumeroCamera = camera.NumeroCamera,
                    Descrizione = camera.Descrizione,
                    Tipologia = camera.Tipologia
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la creazione della camera.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
    }
}
