using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using swimmresult1;

// Administratora panelis, kur administrators var pievienot sportistu rezultātus un redzēt savus datus 
namespace swimmresult1.Pages
{
    public class AdminPanelModel : PageModel
    {
        public string AdminName { get; set; }
        public string AdminId { get; set; }
        public string AdminKlubs { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet(string name, string id)
        {
            var admin = AdminStore.Admins
                .FirstOrDefault(a => a.Vards == name && a.TreneraId == id);

            if (admin != null)
            {
                AdminName = admin.Vards;
                AdminId = admin.TreneraId;
                AdminKlubs = admin.Klubs;
            }
        }

        public IActionResult OnPost(string SportistaVards, string SportistaUzvards,
                                    string SportistaId, string Distance, string Laiks,
                                    string AdminName, string AdminId)
        {
            var admin = AdminStore.Admins
                .FirstOrDefault(a => a.Vards == AdminName && a.TreneraId == AdminId);

            if (admin == null)
                return RedirectToPage("AdminLogin");

            if (SportistaId.Length != 10 || !SportistaId.All(char.IsDigit))
            {
                ErrorMessage = "Sportista ID jābūt 10 cipariem";
                this.AdminName = admin.Vards;
                this.AdminId = admin.TreneraId;
                return Page();
            }

            AdminStore.Sportisti.Add(new Sportists
            {
                Vards = SportistaVards,
                Uzvards = SportistaUzvards,
                SportistaId = SportistaId,
                Distance = Distance,
                Laiks = Laiks,
                Klubs = admin.Klubs
            });

            SuccessMessage = "Rezultāts iesniegts";

            // Atpakaļ uz lapu ar administratora informāciju, lai saglabātu kontekstu
            return RedirectToPage("AdminPanel", new
            {
                name = admin.Vards,
                id = admin.TreneraId
            });
        }
    }
}