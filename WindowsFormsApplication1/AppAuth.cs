using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApplication1
{
    public static class AppAuth
    {
        public static string Sha256Hex(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] b = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(b.Length * 2);
                foreach (byte t in b)
                    sb.Append(t.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
