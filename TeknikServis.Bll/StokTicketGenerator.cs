namespace TeknikServis.Bll
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using TeknikServis.Dal.Concrete.EntityFramework.Repository;
    using TeknikServis.Interfaces;

    public class StokTicketGenerator
    {
        ITeknikService teknikService = new TeknikManager(new EfTeknikRepository());

        private static Random random = new Random();


        public string TicketOlustur()
        {

            

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            finalString="#"+finalString;
            return finalString;
        }
        

    

    }
}
