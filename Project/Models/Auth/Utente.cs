namespace Project.Models.Auth
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}