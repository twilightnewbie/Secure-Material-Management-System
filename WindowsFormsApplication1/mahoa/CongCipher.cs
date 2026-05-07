using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.mahoa
{
    internal class CongCipher
    {
        public static class AdditionCipher
        {
            private static readonly int key = 7; // khóa cộng
            private static readonly int n = 256;

            public static string Encrypt(string plainText)
            {
                char[] result = new char[plainText.Length];
                for (int i = 0; i < plainText.Length; i++)
                {
                    int m = (int)plainText[i];              // lấy ASCII code
                    int c = (m + key) % n;                  // mã hóa
                    result[i] = (char)c;                    // đổi lại sang ký tự
                }
                return new string(result);
            }

            public static string Decrypt(string cipherText)
            {
                char[] result = new char[cipherText.Length];
                for (int i = 0; i < cipherText.Length; i++)
                {
                    int c = (int)cipherText[i];
                    int m = (c - key + n) % n;              // giải mã
                    result[i] = (char)m;
                }
                return new string(result);
            }
        }
    }
}
