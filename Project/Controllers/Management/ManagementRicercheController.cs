using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Management;


namespace Project.Controllers
{
    [Authorize]
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
                return View("RicercaByCF"); // Ritorna alla pagina di ricerca
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByCFAsync(codiceFiscale);

                if (prenotazioni != null && prenotazioni.Any())
                {
                    // Invia una risposta JSON con l'URL della pagina dei risultati
                    return Json(new { redirectUrl = Url.Action("RisultatiByCF", "ManagementRicerche", new { codiceFiscale }) });
                }
                else
                {
                    return Json(new { redirectUrl = Url.Action("RisultatiByCF", "ManagementRicerche", new { codiceFiscale }) });
                }
            }
            catch
            {
                return StatusCode(500, "Errore interno del server.");
            }
        }

        [HttpGet("RisultatiByCF")]
        public async Task<IActionResult> RisultatiByCF(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                return RedirectToAction("RicercaByCF");
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByCFAsync(codiceFiscale);
                return View(prenotazioni);
            }
            catch
            {
                return StatusCode(500, "Errore interno del server.");
            }
        }
    }

}
