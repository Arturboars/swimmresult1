using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using swimmresult1;
using System;
// Meklēšanas loģika sportistiem pēc vārda, uzvārda un ID, kā arī rezultātu un progresu aprēķināšana
namespace swimmresult1.Pages
{
    public class SearchSportistModel : PageModel
    {
        public Dictionary<string, List<string>> ResultsByDistance { get; set; }
        public Dictionary<string, double> ProgressByDistance { get; set; }

        public string Klubs { get; set; }
        public bool Searched { get; set; }

        public void OnGet()
        {
        }

        public void OnPost(string Vards, string Uzvards, string SportistaId)
        {
            Searched = true;

            var sportisti = AdminStore.Sportisti
                .Where(s => s.Vards == Vards &&
                            s.Uzvards == Uzvards &&
                            s.SportistaId == SportistaId)
                .ToList();

            if (sportisti.Any())
            {
                Klubs = sportisti.First().Klubs;

                ResultsByDistance = new Dictionary<string, List<string>>();
                ProgressByDistance = new Dictionary<string, double>();

                var grouped = sportisti.GroupBy(s => s.Distance);

                foreach (var group in grouped)
                {
                    var sortedTimes = group
                        .Select(x => x.Laiks)
                        .OrderBy(t => ConvertToSeconds(t))
                        .ToList();

                    ResultsByDistance[group.Key] = sortedTimes;

                    if (sortedTimes.Count > 1)
                    {
                        double best = ConvertToSeconds(sortedTimes.First());
                        double worst = ConvertToSeconds(sortedTimes.Last());

                        double progress = ((worst - best) / worst) * 100;

                        ProgressByDistance[group.Key] = Math.Round(progress, 2);
                    }
                }
            }
        }

        private double ConvertToSeconds(string time)
        {
            try
            {
                var parts = time.Split(':');
                var minutes = double.Parse(parts[0]);
                var seconds = double.Parse(parts[1].Replace(',', '.'));

                return minutes * 60 + seconds;
            }
            catch
            {
                return double.MaxValue;
            }
        }
    }
}