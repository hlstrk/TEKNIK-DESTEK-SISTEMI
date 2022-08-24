namespace TeknikServis.Bll
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class ToPasswordRepository
    {
        
        private static UnicodeEncoding _encoder = new UnicodeEncoding();
        public static byte[] ByteDonustur(string deger)
        {

            UnicodeEncoding byteConverter = new UnicodeEncoding();
            return byteConverter.GetBytes(deger);

        }

        public static byte[] Byte8(string deger)
        {
            char[] arrayChar = deger.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }

        internal object Md5(object parola)
        {
            throw new NotImplementedException();
        }





        
            public string HashSifrele(string data)
            {

            string EncryptionKey = "MAKV2SPD2DS2";
            byte[] clearBytes = Encoding.Unicode.GetBytes(data);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }

            data = data.Replace("/", "test");
            data = data.Replace(":", "firmasi");
            data = data.Replace("+", "hex");


            return data;
        }
        

    

        public string HashCoz(string data)
        {
            data = data.Replace("/", "test");
            data = data.Replace(":", "firmasi");
            data = data.Replace("+", "hex");
            string EncryptionKey = "MAKV2SPD2DS2";
            byte[] cipherBytes = Convert.FromBase64String(data);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    data = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
           
            return data;
        }



        public string Md5(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri yok");
            }
            else
            {
                MD5CryptoServiceProvider sifre = new MD5CryptoServiceProvider();
                byte[] sifrebytes = sifre.ComputeHash(Encoding.UTF8.GetBytes(strGiris));
                var sb = new StringBuilder();
                foreach (byte b in sifrebytes)
                    sb.Append(b.ToString("x2").ToLower());

                return sb.ToString();

            }
        }
        public string Sha1(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri yok.");
            }
            else
            {
                SHA1CryptoServiceProvider sifre = new SHA1CryptoServiceProvider();
                 byte[] sifrebytes = sifre.ComputeHash(Encoding.UTF8.GetBytes(strGiris));
                 var sb = new StringBuilder();
                 foreach (byte b in sifrebytes)
                     sb.Append(b.ToString("x2").ToLower());

                 return sb.ToString();
            }
        }
        public string Sha256(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek Veri Yok");
            }
            else
            {
                SHA256Managed sifre = new SHA256Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        public string Sha384(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri bulunamadı.");
            }
            else
            {
                SHA384Managed sifre = new SHA384Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        public string Sha512(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri yok.");
            }
            else
            {


                SHA512Managed sifre = new SHA512Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
    }
}
