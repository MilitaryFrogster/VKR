using System;
using System.IO;
using System.Security.Cryptography;

namespace VKR
{
    public static class EncryptionHelper
    {
        // Шифрует файл и возвращает байты, готовые к загрузке в облако
        public static byte[] EncryptFile(string inputFilePath)
        {
            byte[] fileData = File.ReadAllBytes(inputFilePath);
            byte[] key = KeyStorage.GetMasterKey();

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (var ms = new MemoryStream())
                {
                    // Пишем сначала IV
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(fileData, 0, fileData.Length);
                        cs.FlushFinalBlock();
                    }

                    return ms.ToArray();
                }
            }
        }

        public static byte[] EncryptData(byte[] data)
        {
            byte[] key = KeyStorage.GetMasterKey();

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (var ms = new MemoryStream())
                {
                    // Пишем IV в начало потока
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


        // Расшифровывает полученные байты и сохраняет как файл
        public static void DecryptFile(byte[] encryptedData, string outputFilePath)
        {
            byte[] key = KeyStorage.GetMasterKey();

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

                    File.WriteAllBytes(outputFilePath, ms.ToArray());
                }
            }
        }
        public static byte[] EncryptBytes(byte[] data)
        {
            using (var inputStream = new MemoryStream(data))
            using (var outputStream = new MemoryStream())
            {
                EncryptStream(inputStream, outputStream);
                return outputStream.ToArray();
            }
        }
        public static void EncryptStream(Stream input, Stream output)
        {
            byte[] key = KeyStorage.GetMasterKey();

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                // Пишем IV в начало потока
                output.Write(aes.IV, 0, aes.IV.Length);

                using (var cryptoStream = new CryptoStream(output, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    input.CopyTo(cryptoStream);
                    cryptoStream.FlushFinalBlock();
                }
            }
        }

        public static ICryptoTransform CreateStreamingEncryptor(out byte[] iv)
        {
            byte[] key = KeyStorage.GetMasterKey();

            var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            iv = aes.IV;

            return aes.CreateEncryptor();
        }




    }
}
