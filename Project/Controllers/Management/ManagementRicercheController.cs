using Microsoft.AspNetCore.Mvc;
using Project.Services.Management;


namespace Project.Controllers
{
    [Route("Management/[controller]")]
    public class ManagementRicercheController : Controller
    {
        private readonly IRicercheService _ricercheService;

        public ManagementRicercheController(IRicercheService ricercheService)
        {
            _ricercheService = ricercheService;
        }

        [HttpGet("RicercaByCF")]
        public IActionResult RicercaByCF()
        {
            return View();
        }

        [HttpPost("RicercaByCF")]
        public async Task<IActionResult> RicercaByCF(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                ModelState.AddModelError("", "Il codice fiscale non può essere nullo o vuoto.");
                return View();
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByCFAsync(codiceFiscale);
                return View("RisultatiByCF", prenotazioni);
            }
            catch
            {
                return StatusCode(500, "Errore interno del server.");
            }
        }

    }
}
