using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Encryption
{
    public class Hasing
    {
        public static string ComputeHash(string plainText,
                                     string hashAlgorithm = "SHA512",
                                     byte[] saltBytes = null)
        {
            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;

                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                saltBytes = new byte[saltSize];

                RandomNumberGenerator.Fill(saltBytes);
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            Array.Copy(plainTextBytes, 0, plainTextWithSaltBytes, 0, plainTextBytes.Length);

            Array.Copy(saltBytes, 0, plainTextWithSaltBytes, plainTextBytes.Length, saltBytes.Length);

            HashAlgorithm hashAlgorithmInstance;

            switch (hashAlgorithm.ToUpperInvariant())
            {
                case "SHA1":
                    hashAlgorithmInstance = SHA1.Create();
                    break;

                case "SHA256":
                    hashAlgorithmInstance = SHA256.Create();
                    break;

                case "SHA384":
                    hashAlgorithmInstance = SHA384.Create();
                    break;

                case "SHA512":
                    hashAlgorithmInstance = SHA512.Create();
                    break;

                default:
                    hashAlgorithmInstance = MD5.Create();
                    break;
            }

            byte[] hashBytes = hashAlgorithmInstance.ComputeHash(plainTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            Array.Copy(hashBytes, 0, hashWithSaltBytes, 0, hashBytes.Length);

            Array.Copy(saltBytes, 0, hashWithSaltBytes, hashBytes.Length, saltBytes.Length);

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public static bool VerifyHash(string plainText,
                                 string hashValue, string hashAlgorithm = "SHA512")
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits, hashSizeInBytes;

            if (hashAlgorithm == null)
                hashAlgorithm = "";
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: 
                    hashSizeInBits = 128;
                    break;
            }

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;
            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
            string expectedHashString =
                        ComputeHash(plainText, hashAlgorithm, saltBytes);
            return (hashValue == expectedHashString);
        }
    }
}
