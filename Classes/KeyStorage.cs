using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VKR
{
    public static class KeyStorage
    {
        private static readonly string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keydata");
        private static readonly string saltPath = Path.Combine(basePath, "salt.dat");
        private static readonly string keyPath = Path.Combine(basePath, "key.dat");
        private static readonly string storedKeyPath = Path.Combine(basePath, "storedkey.dat");

        private static byte[] masterKey;

        public static bool MasterKeyLoaded => masterKey != null;

        public static byte[] GetMasterKey()
        {
            if (!MasterKeyLoaded)
                throw new InvalidOperationException("Master key not loaded.");

            return masterKey;
        }

        public static bool KeyFilesExist()
        {
            return File.Exists(saltPath) && File.Exists(keyPath) && File.Exists(storedKeyPath);
        }

        public static void InitializeKeyFromPassword(string password)
        {
            Directory.CreateDirectory(basePath);

            // generate salt and masterKey
            byte[] salt = GenerateRandomBytes(16);
            byte[] derivedKey = DeriveKey(password, salt);
            masterKey = GenerateRandomBytes(32);

            // encrypt and save masterKey
            byte[] encryptedMasterKey = EncryptWithAes(masterKey, derivedKey);
            File.WriteAllBytes(saltPath, salt);
            File.WriteAllBytes(keyPath, encryptedMasterKey);

            // save derivedKey securely (in this version — simple AES using static key for demo purposes)
            byte[] staticKey = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("ncopy-internal-protection"));
            byte[] encryptedDerivedKey = EncryptWithAes(derivedKey, staticKey);
            File.WriteAllBytes(storedKeyPath, encryptedDerivedKey);
        }

        public static bool TryLoadMasterKey()
        {
            try
            {
                byte[] salt = File.ReadAllBytes(saltPath);
                byte[] encryptedMasterKey = File.ReadAllBytes(keyPath);
                byte[] encryptedDerivedKey = File.ReadAllBytes(storedKeyPath);

                byte[] staticKey = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("ncopy-internal-protection"));
                byte[] derivedKey = DecryptWithAes(encryptedDerivedKey, staticKey);

                masterKey = DecryptWithAes(encryptedMasterKey, derivedKey);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                return bytes;
            }
        }

        private static byte[] DeriveKey(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 150000))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        private static byte[] EncryptWithAes(byte[] data, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
        }

        private static byte[] DecryptWithAes(byte[] encryptedData, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                byte[] iv = new byte[16];
                Array.Copy(encryptedData, 0, iv, 0, 16);
                aes.IV = iv;

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedData, 16, encryptedData.Length - 16);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }
    }
}
