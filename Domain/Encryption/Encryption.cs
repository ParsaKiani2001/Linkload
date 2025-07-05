using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Encryption
{
    public class Encryption
    {
        private static readonly string IV = "HR$PIOJH354GS12$2PJ4";
        private static readonly string Key = "f9938d3ea245f12e2d2453240c97d921";
        private static readonly UTF8Encoding utf8 = new UTF8Encoding();

        public static string Encrypt(string text)
        {
            try
            {
                byte[] textBytes = utf8.GetBytes(text);
                using (Aes aes = Aes.Create())
                {
                    aes.BlockSize = 128;
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.IV = Encoding.ASCII.GetBytes(IV);
                    aes.Key = Encoding.ASCII.GetBytes(Key);
                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (MemoryStream memory = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(memory, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(textBytes, 0, textBytes.Length);
                            csEncrypt.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(memory.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
        public static string Decrypt(string? chiperText)
        {
            try
            {
                byte[] ciphertextBytes = Convert.FromBase64String(chiperText);

                using (Aes aes = Aes.Create())
                {
                    aes.BlockSize = 128;
                    aes.KeySize = 256;
                    aes.Key = Encoding.ASCII.GetBytes(Key);
                    aes.IV = Encoding.ASCII.GetBytes(IV);
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (MemoryStream msDecrypt = new MemoryStream(ciphertextBytes))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch
            {
                return "ERROR";
            }
        }
    }
}
