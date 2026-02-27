using System.Collections.Generic;
// Vienkāršs datu veikals administratoriem un sportistiem, kur tiek glabāti reģistrētie administratori un sportisti ar viņu rezultātiem
namespace swimmresult1
{
    public static class AdminStore
    {
        public static List<Admin> Admins = new List<Admin>();
        public static List<Sportists> Sportisti = new List<Sportists>();
    }
}