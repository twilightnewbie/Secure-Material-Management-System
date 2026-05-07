using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApplication1.mahoa
{
    public static class DesCipher
    {
        // Mã hóa file DES
        public static void EncryptFile(string inputFile, string outputFile, string key)
        {
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (FileStream fsEncrypted = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                DES.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                using (CryptoStream cs = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        // Giải mã file DES
        public static void DecryptFile(string inputFile, string outputFile, string key)
        {
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (FileStream fsDecrypted = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                DES.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                ICryptoTransform desdecrypt = DES.CreateDecryptor();
                using (CryptoStream cs = new CryptoStream(fsInput, desdecrypt, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsDecrypted.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}
