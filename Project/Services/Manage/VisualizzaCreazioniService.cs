using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Project.Services.Manage
{
    public class VisualizzaCreazioniService : BaseService, IVisualizzaCreazioniService
    {
        private const string GET_ALL_CAMERE_COMMAND =
            "SELECT IdCamera, NumeroCamera, Descrizione, Tipologia FROM [dbo].[Camere]";

        private const string GET_ALL_PERSONE_COMMAND =
            "SELECT IdPersona, Nome, Cognome, CF, Email, Telefono, Cellulare, Città, Provincia FROM [dbo].[Persone]";

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
    }
}
