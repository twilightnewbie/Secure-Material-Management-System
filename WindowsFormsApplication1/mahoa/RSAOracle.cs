using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace WindowsFormsApplication1.mahoa
{
    public static class RSAOracle
    {
        private static string PublicKeyPath = "public.key";
        private static string PrivateKeyPath = "private.key";

        // ==========================
        // 1. TẠO CẶP KHÓA RSA
        // ==========================
        public static void GenerateKeys()
        {
            using (OracleConnection conn = Database.Conn)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleCommand cmd = new OracleCommand(
                    "BEGIN CRYPTO_RSA_DES.GenerateKeyRSA(:p_pub, :p_pri); END;",
                    conn))
                {
                    cmd.Parameters.Add("p_pub", OracleDbType.Clob)
                                  .Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("p_pri", OracleDbType.Clob)
                                  .Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    string publicKey = cmd.Parameters["p_pub"].Value.ToString();
                    string privateKey = cmd.Parameters["p_pri"].Value.ToString();

                    File.WriteAllText(PublicKeyPath, publicKey);
                    File.WriteAllText(PrivateKeyPath, privateKey);
                }
            }
        }

        // ==========================
        // 2. MÃ HÓA RSA
        // ==========================
        public static string Encrypt(string plainText)
        {
            if (!File.Exists(PublicKeyPath))
                throw new Exception("Chưa có public.key – hãy GenerateKeys trước");

            string publicKey = File.ReadAllText(PublicKeyPath);

            using (OracleConnection conn = Database.Conn)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleCommand cmd = new OracleCommand(
                    "SELECT CRYPTO_RSA_DES.EncryptRSA(:p_text, :p_pub) FROM dual",
                    conn))
                {
                    cmd.Parameters.Add("p_text", OracleDbType.Clob).Value = plainText;
                    cmd.Parameters.Add("p_pub", OracleDbType.Clob).Value = publicKey;

                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        // ==========================
        // 3. GIẢI MÃ RSA
        // ==========================
        public static string Decrypt(string cipherText)
        {
            if (!File.Exists(PrivateKeyPath))
                throw new Exception("Chưa có private.key – hãy GenerateKeys trước");

            string privateKey = File.ReadAllText(PrivateKeyPath);

            using (OracleConnection conn = Database.Conn)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleCommand cmd = new OracleCommand(
                    "SELECT CRYPTO_RSA_DES.DecryptRSA(:p_text, :p_pri) FROM dual",
                    conn))
                {
                    cmd.Parameters.Add("p_text", OracleDbType.Clob).Value = cipherText;
                    cmd.Parameters.Add("p_pri", OracleDbType.Clob).Value = privateKey;

                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }
    }
}
