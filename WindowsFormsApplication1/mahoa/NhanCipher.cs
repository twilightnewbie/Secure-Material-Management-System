using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.mahoa
{
    internal class NhanCipher
    {
        private static int ModInverse(int n, int b)
        {
            int r1 = n, r2 = b;
            int t1 = 0, t2 = 1;

            while (r2 > 0)
            {
                int q = r1 / r2;

                int r = r1 - q * r2;
                r1 = r2;
                r2 = r;

                int t = t1 - q * t2;
                t1 = t2;
                t2 = t;
            }

            if (r1 == 1)
            {
                if (t1 < 0) t1 += n;
                return t1;
            }

            return -1;
        }
        public static class MultiplicationCipher
        {
            private static readonly int key = 5; // khóa nhân, gcd(5,256)=1
            private static readonly int n = 256;

            public static string Encrypt(string plainText)
            {
                char[] result = new char[plainText.Length];
                for (int i = 0; i < plainText.Length; i++)
                {
                    int m = (int)plainText[i];
                    int c = (m * key) % n;
                    result[i] = (char)c;
                }
                return new string(result);
            }

            public static string Decrypt(string cipherText)
            {
                char[] result = new char[cipherText.Length];
                int keyInv = ModInverse(n, key);   // tìm nghịch đảo theo slide

                for (int i = -0; i < cipherText.Length; i++)
                {
                    int c = (int)cipherText[i];
                    int m = (c * keyInv) % n;
                    result[i] = (char)m;
                }
                return new string(result);
            }
        }
    }
}
