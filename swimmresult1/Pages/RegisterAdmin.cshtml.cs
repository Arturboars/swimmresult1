using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using swimmresult1;

namespace swimmresult1.Pages
{
    // Jauna administratora reģistrācijas loģika
    public class RegisterAdminModel : PageModel
    {
        public string ErrorMessage { get; set; }

        public void OnGet()
        {

        }

        public void OnPost(string Vards, string Uzvards, string TreneraId, string Klubs, string Parole, string RepeatParole)
        {
            // Pārbaude vai visi lauki aizpildīti
            if (string.IsNullOrWhiteSpace(Vards) ||
                string.IsNullOrWhiteSpace(Uzvards) ||
                string.IsNullOrWhiteSpace(TreneraId) ||
                string.IsNullOrWhiteSpace(Klubs) ||
                string.IsNullOrWhiteSpace(Parole) ||
                string.IsNullOrWhiteSpace(RepeatParole))
            {
                ErrorMessage = "Ievadiet informāciju lejā";
                return;
            }

            // Parolei jābūt vismaz 8 simboliem
            if (Parole.Length < 8)
            {
                ErrorMessage = "Parolei jābūt vismaz 8 simboliem";
                return;
            }

            // Trenera ID jābūt tieši 6 cipariem
            if (TreneraId.Length != 6 || !TreneraId.All(char.IsDigit))
            {
                ErrorMessage = "Trenera ID jābūt tieši 6 cipariem";
                return;
            }

            // Paroļu sakritības pārbaude
            if (Parole != RepeatParole)
            {
                ErrorMessage = "Paroles nesakrīt";
                return;
            }

            // Pārbaude vai administrators jau eksistē
            var existingAdmin = AdminStore.Admins.FirstOrDefault(a =>
                a.Vards == Vards &&
                a.Uzvards == Uzvards &&
                a.TreneraId == TreneraId);

            if (existingAdmin != null)
            {
                ErrorMessage = "Šāds administrators jau ir";
                return;
            }

            // Administratora saglabāšana
            AdminStore.Admins.Add(new Admin
            {
                Vards = Vards,
                Uzvards = Uzvards,
                TreneraId = TreneraId,
                Klubs = Klubs,
                Parole = Parole
            });

            Response.Redirect("/AdminLogin");
        }
    }
}