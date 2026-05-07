using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.mahoa;

namespace WindowsFormsApplication1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = ofd.FileName;
                    Log($"📂 Đã chọn file: {ofd.FileName}");
                }
            }
        }

        private void btnEncryptDES_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtKey.Text) || txtKey.Text.Length < 8)
                {
                    MessageBox.Show("Khóa DES phải có ít nhất 8 ký tự!");
                    return;
                }

                string input = txtFilePath.Text;
                string output = input + ".des";
                DesCipher.EncryptFile(input, output, txtKey.Text);
                Log($"✅ Mã hóa DES thành công → {output}");
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi mã hóa DES: {ex.Message}");
            }
        }

        private void btnDecryptDES_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtFilePath.Text;
                string output = input + ".giai.txt";
                DesCipher.DecryptFile(input, output, txtKey.Text);
                Log($"✅ Giải mã DES thành công → {output}");
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi giải mã DES: {ex.Message}");
            }
        }

        private void btnDecryptRSA_Click(object sender, EventArgs e)
        {
            try
            {
                string plain = RSAOracle.Decrypt(txtKey.Text);
                Log($"🔓 RSA Decrypt (Oracle) → {plain}");
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi giải mã RSA (Oracle): {ex.Message}");
            }
        }

        private void btnEncryptRSA_Click(object sender, EventArgs e)
        {
            try
            {
                string cipher = RSAOracle.Encrypt(txtKey.Text);
                Log($"🔒 RSA Encrypt (Oracle) → {cipher}");
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi mã hóa RSA (Oracle): {ex.Message}");
            }
        }

        private void btnGenRSA_Click(object sender, EventArgs e)
        {
            try
            {
                RSAOracle.GenerateKeys();
                Log("✅ Đã tạo cặp khóa RSA trên Oracle (và lưu file local)");
                MessageBox.Show("Tạo khóa RSA thành công trên Oracle!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi tạo khóa RSA (Oracle): {ex.Message}");
            }

        }
        private void Log(string msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
        }
    }
}
