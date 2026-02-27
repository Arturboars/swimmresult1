using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using swimmresult1;

namespace swimmresult1.Pages
{
    // Administratora autorizācijas loģika
    public class AdminLoginModel : PageModel
    {
        public string ErrorMessage { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string Vards, string Uzvards, string TreneraId, string Klubs, string Parole)
        {
            // Administratora meklēšana sarakstā
            var admin = AdminStore.Admins.FirstOrDefault(a =>
                a.Vards == Vards &&
                a.Uzvards == Uzvards &&
                a.TreneraId == TreneraId &&
                a.Klubs == Klubs &&
                a.Parole == Parole);

            if (admin != null)
            {
                // Droša pāreja uz administratora paneli
                return RedirectToPage("AdminPanel", new
                {
                    name = admin.Vards,
                    id = admin.TreneraId
                });
            }

            ErrorMessage = "Nav atrasts administrators";
            return Page();
        }
    }
}