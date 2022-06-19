using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace dsa_test
{
    public class Program
    {

        static void Main()
        {
            try
            {
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();

                // Create a string to encrypt.
                string sData = "Here is some data to encrypt.";

                // Encrypt the string to an in-memory buffer.
                string Data = EncryptSMS(sData, tDESalg.Key, tDESalg.IV);

                Console.WriteLine(Data);

                //Decrypt the buffer back to a string.
                string Final = DecryptSMS(Data, tDESalg.Key, tDESalg.IV);

                // Display the decrypted string to the console.
                Console.WriteLine(Final);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string EncryptSMS(string sms, byte[] Key, byte[] IV)
        {
            try
            {
                var buffer = Encoding.UTF8.GetBytes(sms);
                var tripleDES = new TripleDESCryptoServiceProvider()
                {
                    IV = IV,
                    Key = Key,
                };

                ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
                var enc = cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);

                return Convert.ToBase64String(enc);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        public static string DecryptSMS(string EncSMS, byte[] Key, byte[] IV)
        {
            try
            {
                var buffer = Convert.FromBase64String(EncSMS);
                var tripleDES = new TripleDESCryptoServiceProvider()
                {
                    IV = IV,
                    Key = Key,
                };

                ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();
                var enc = cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(enc);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}
