﻿using System.Security.Cryptography;
using System.Text;

namespace BobrVerse.Auth.Utilities
{
    public static class TokenEncryptionUtility
    {
        public static string EncryptToken(string token, string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.GenerateIV();
            var iv = aesAlg.IV;

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encryptedToken = encryptor.TransformFinalBlock(tokenBytes, 0, tokenBytes.Length);

            return Convert.ToBase64String(iv.Concat(encryptedToken).ToArray());
        }

        public static bool TryDecryptToken(string encryptedToken, string secretKey, out string? decryptedToken)
        {
            try
            {
                var tokenBytes = Convert.FromBase64String(encryptedToken);
                var iv = tokenBytes.Take(16).ToArray();
                var encryptedData = tokenBytes.Skip(16).ToArray();

                var key = Encoding.ASCII.GetBytes(secretKey);
                using var aesAlg = Aes.Create();
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                var decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                decryptedToken = Encoding.UTF8.GetString(decryptedBytes);
                return true;
            }
            catch
            {
                decryptedToken = null;
                return false;
            }
        }
    }
}
