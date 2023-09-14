using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Online_Shopping_Cart
{
    
    public static class ExtensionMethods
    {
        private static string encryptionKey = "fepyefdwerijvey";
        public static string ToTittleCase(this string text)
        {
                  return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }
        public static int CountAlfaNumeric(this string text)
        {
            return text.Count(char.IsLetterOrDigit);
        }

        public static string ToSlug(this string text)
        {
            return string.Join("",text.Replace("", "-").Replace(".", "-").Replace("_", "-").Where(m => char.IsLetterOrDigit(m) || m == '-')) ;
        }
        public static string toString(this string text)
        {
            return string.Join("",text);
        }
        public static int WordCount(this string text, char separator=' ')
        {
            return text.Split(separator).Length;
        }
       
        // using this in the extension methods is important.and also provide the encryption key.
        //this method is use to encrypt the password that is getting from user.
        public static string Encrypt(this string plainText)//normal text that is form user.
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }
        // this is the decytpion methods that decrypt the encrypted password
        public static string Decrypt(this string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
