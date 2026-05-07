using System;
using System.IO;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Drawing;
using iTextSharp.text.exceptions;

namespace WindowsFormsApplication1
{
    public partial class FormSignPDF : Form
    {
        private byte[] signedPdfBytes;   // PDF đã đóng dấu
        private string lastSignature;    // chữ ký Base64

        // ================= FIX CHUỖI KẾT NỐI =================
        private const string CONN_STR =
            "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))" +
            "(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=luudatabase;Password=uaa;";
        // =====================================================

        public FormSignPDF()
        {
            InitializeComponent();
        }

        // =====================================================
        // 1. CHỌN FILE PDF
        // =====================================================
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "PDF Files (*.pdf)|*.pdf";
                if (op.ShowDialog() == DialogResult.OK)
                    txtFile.Text = op.FileName;
            }
        }

        // =====================================================
        // 2. TẠO KEY RSA 2048
        // =====================================================
        private void btnGenKey_Click(object sender, EventArgs e)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    txtSign_PrivateKey.Text = rsa.ToXmlString(true);
                    txtVerify_PublicKey.Text = rsa.ToXmlString(false);
                }
                MessageBox.Show("✔ Tạo RSA 2048-bit thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo key: " + ex.Message);
            }
        }

        // =====================================================
        // 3. KÝ SỐ PDF (RSA + SHA256)
        // =====================================================
        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(txtFile.Text) ||
                    string.IsNullOrWhiteSpace(txtMaHD.Text) ||
                    string.IsNullOrWhiteSpace(txtSign_PrivateKey.Text))
                {
                    MessageBox.Show("Thiếu Mã HD, File PDF hoặc Private Key!");
                    return;
                }

                byte[] originalPdf = File.ReadAllBytes(txtFile.Text);

                // ĐÓNG DẤU PDF
                signedPdfBytes = StampPdf(originalPdf);

                // HASH SHA256
                byte[] hash;
                using (SHA256 sha = SHA256.Create())
                {
                    hash = sha.ComputeHash(signedPdfBytes);
                }

                // KÝ HASH RSA
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(txtSign_PrivateKey.Text.Trim());
                    byte[] sig = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
                    lastSignature = Convert.ToBase64String(sig);
                    txtSign_Result.Text = lastSignature;
                }

                // LƯU ORACLE
                SaveToOracle(
                    txtMaHD.Text,
                    Path.GetFileName(txtFile.Text),
                    signedPdfBytes,
                    lastSignature
                );

                // XUẤT FILE PDF ĐÃ KÝ
                string outFile = Path.Combine(
                    Path.GetDirectoryName(txtFile.Text),
                    Path.GetFileNameWithoutExtension(txtFile.Text) + "_SIGNED.pdf"
                );

                File.WriteAllBytes(outFile, signedPdfBytes);
                txtFile.Text = outFile; // quan trọng cho VERIFY

                MessageBox.Show("✔ Ký số RSA thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ký PDF: " + ex.Message);
            }
        }

        // =====================================================
        // 4. VERIFY CHỮ KÝ RSA
        // =====================================================
        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(txtFile.Text) ||
                    string.IsNullOrWhiteSpace(txtVerify_PublicKey.Text) ||
                    string.IsNullOrWhiteSpace(txtSign_Result.Text))
                {
                    MessageBox.Show("Thiếu File, Public Key hoặc chữ ký!");
                    return;
                }

                byte[] pdfBytes = File.ReadAllBytes(txtFile.Text);
                byte[] sigBytes = Convert.FromBase64String(txtSign_Result.Text);

                byte[] hash;
                using (SHA256 sha = SHA256.Create())
                {
                    hash = sha.ComputeHash(pdfBytes);
                }

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(txtVerify_PublicKey.Text.Trim());
                    bool ok = rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), sigBytes);

                    if (ok)
                    {
                        lblSignStatus.Text = "✔ HỢP LỆ – FILE NGUYÊN VẸN";
                        lblSignStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblSignStatus.Text = "✘ KHÔNG HỢP LỆ – FILE BỊ SỬA";
                        lblSignStatus.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi verify: " + ex.Message);
            }
        }

        // =====================================================
        // 5. ĐÓNG DẤU PDF + FIX BAD PASSWORD
        // =====================================================
        private byte[] StampPdf(byte[] pdfBytes)
        {
            PdfReader reader;

            try
            {
                reader = new PdfReader(pdfBytes);
            }
            catch (BadPasswordException)
            {
                throw new Exception("PDF đang bị khóa mật khẩu!");
            }

            if (reader.IsEncrypted())
            {
                reader.Close();
                throw new Exception("PDF bị encrypted – không thể ký!");
            }

            using (MemoryStream output = new MemoryStream())
            {
                PdfStamper stamper = new PdfStamper(reader, output);
                PdfContentByte cb = stamper.GetOverContent(1);

                BaseFont bf = BaseFont.CreateFont(
                    BaseFont.HELVETICA_BOLD,
                    BaseFont.CP1252,
                    false
                );

                cb.BeginText();
                cb.SetFontAndSize(bf, 14);
                cb.SetColorFill(BaseColor.RED);
                cb.ShowTextAligned(
                    Element.ALIGN_LEFT,
                    $"SIGNED at: {DateTime.Now:dd/MM/yyyy HH:mm}",
                    50, 50, 0
                );
                cb.EndText();

                stamper.Close();
                reader.Close();

                return output.ToArray();
            }
        }
        private void btnRSA_DecryptFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(txtRSA_InputFile.Text))
                {
                    MessageBox.Show("Chưa chọn file cần giải mã!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSign_PrivateKey.Text))
                {
                    MessageBox.Show("Chưa có Private Key!");
                    return;
                }

                byte[] encData = File.ReadAllBytes(txtRSA_InputFile.Text);

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(txtSign_PrivateKey.Text.Trim());
                    byte[] dec = rsa.Decrypt(encData, false);
                    File.WriteAllBytes(txtRSA_OutputFile.Text, dec);
                }

                MessageBox.Show("✔ Giải mã RSA thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi RSA Decrypt: " + ex.Message);
            }
        }
        // =====================================================
        // 6. LƯU ORACLE
        // =====================================================
        private void SaveToOracle(string id, string fileName, byte[] pdf, string sig)
        {
            using (OracleConnection conn = new OracleConnection(CONN_STR))
            {
                conn.Open();

                string sql =
                    @"INSERT INTO FILE_SIGN
              (DOC_TYPE, DOC_ID, FILENAME, CONTENT_TYPE,
               FILE_DATA, SIGNATURE_P7S, SIGNED_BY)
              VALUES
              ('HOADON', :p_id, :p_filename, 'application/pdf',
               :p_pdf, :p_sig, 'luudatabase')";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.BindByName = true;

                    cmd.Parameters.Add("p_id", OracleDbType.Varchar2).Value = id;
                    cmd.Parameters.Add("p_filename", OracleDbType.Varchar2).Value = fileName;
                    cmd.Parameters.Add("p_pdf", OracleDbType.Blob).Value = pdf;
                    cmd.Parameters.Add("p_sig", OracleDbType.Clob).Value = sig;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void btnRSA_EncryptFile_Click(object sender, EventArgs e)
        {

        }
    }
}
