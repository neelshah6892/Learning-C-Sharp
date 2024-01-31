using System;
using System.Security.Cryptography;
using System.Text;
namespace AES256
{

#pragma warning disable SYSLIB0022
    class Program
    {
        private static string getString(byte[] b)
        {
            return Encoding.UTF8.GetString(b);
        }
        static void Main(string[] args)
        {
            byte[] data = Encoding.UTF8.GetBytes("Kifs@12345678");
            byte[] a = Convert.FromBase64String("fogoZNg4jLgROSA7gnNt3gVxUwKxZGUG94swhWSVZXE=");
            Console.WriteLine("Key : {0}", getString(a)); 
            byte[] enc = Encrypt(data, a); string result = Convert.ToBase64String(enc); 
            Console.WriteLine("Encrypted text", result); byte[] dec = Decrypt(enc, a);
            Console.WriteLine("Encrypted : {0}", getString(enc)); 
            //Console.WriteLine("Decrypted : {0}", getString(dec));
            // Console.ReadKey();
        }
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            using (RijndaelManaged csp = new RijndaelManaged())
            {
                csp.KeySize = 256; csp.BlockSize = 128; csp.Key = key; csp.Padding = PaddingMode.PKCS7; csp.Mode = CipherMode.ECB;
                ICryptoTransform encrypter = csp.CreateEncryptor(); return encrypter.TransformFinalBlock(data, 0, data.Length);
            }
        }
        private static byte[] Decrypt(byte[] data, byte[] key)
        {
            using (RijndaelManaged csp = new RijndaelManaged())
            {
                csp.KeySize = 256; csp.BlockSize = 128; csp.Key = key; csp.Padding = PaddingMode.PKCS7; csp.Mode = CipherMode.ECB;
                ICryptoTransform decrypter = csp.CreateDecryptor(); return decrypter.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}