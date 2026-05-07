using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class DESHelper
    {
        // key phải đủ 8 byte
        private static readonly byte[] DES_KEY = Encoding.UTF8.GetBytes("12345678");
        private static readonly byte[] DES_IV = Encoding.UTF8.GetBytes("87654321");

        // Mã hóa chuỗi
        public static string EncryptString(string plainText)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] data = Encoding.UTF8.GetBytes(plainText);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms,
                    des.CreateEncryptor(DES_KEY, DES_IV), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Giải mã chuỗi
        public static string DecryptString(string cipherText)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] data = Convert.FromBase64String(cipherText);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms,
                    des.CreateDecryptor(DES_KEY, DES_IV), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        // Mã hóa file PDF
        public static void EncryptFile(string inputFile, string outputFile)
        {
            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
            using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            using (CryptoStream cs = new CryptoStream(fsOut,
                des.CreateEncryptor(DES_KEY, DES_IV),
                CryptoStreamMode.Write))
            {
                fsIn.CopyTo(cs);
            }
        }

        // Giải mã file PDF
        public static void DecryptFile(string inputFile, string outputFile)
        {
            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
            using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            using (CryptoStream cs = new CryptoStream(fsIn,
                des.CreateDecryptor(DES_KEY, DES_IV),
                CryptoStreamMode.Read))
            {
                cs.CopyTo(fsOut);
            }
        }
    }

}
