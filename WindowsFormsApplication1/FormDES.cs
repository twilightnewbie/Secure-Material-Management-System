using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormDES : Form
    {
        public FormDES()
        {
            InitializeComponent();
        }

        // --- HÀM HỖ TRỢ QUAN TRỌNG: Lấy đúng 8 byte cho Key và IV ---
        // Hàm này giúp chương trình không bị lỗi dù bạn nhập tiếng Việt hay thừa/thiếu ký tự
        private byte[] GetKeyBuffer(string key)
        {
            // Chuyển chuỗi thành byte
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Tạo mảng đích đúng 8 byte (64-bit)
            byte[] validKey = new byte[8];

            // Copy key vào mảng đích (nếu thiếu thì tự điền 0, thừa thì cắt bớt)
            Array.Copy(keyBytes, validKey, Math.Min(keyBytes.Length, 8));

            return validKey;
        }

        // ==================== DES Encrypt (Text) ====================
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string plaintext = txtInput.Text;
                string keyInput = txtKey.Text;

                // Dùng hàm GetKeyBuffer để đảm bảo key luôn hợp lệ
                byte[] key = GetKeyBuffer(keyInput);
                byte[] iv = key; // Dùng Key làm IV cho đơn giản

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = key;
                    des.IV = iv;
                    des.Padding = PaddingMode.PKCS7; // Padding chuẩn

                    byte[] data = Encoding.UTF8.GetBytes(plaintext);

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        txtOutput.Text = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mã hóa: " + ex.Message);
            }
        }

        // ==================== DES Decrypt (Text) ====================
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string cipher = txtOutput.Text;
                string keyInput = txtKey.Text;

                byte[] key = GetKeyBuffer(keyInput);
                byte[] iv = key;

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = key;
                    des.IV = iv;
                    des.Padding = PaddingMode.PKCS7;

                    byte[] encryptedData = Convert.FromBase64String(cipher);

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedData, 0, encryptedData.Length);
                        cs.FlushFinalBlock();
                        txtInput.Text = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giải mã (kiểm tra lại Key): " + ex.Message);
            }
        }

        // ==================== Chọn file ====================
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                txtFilePath.Text = ofd.FileName;
        }

        // ==================== Mã hóa file ====================
        private void btnEncryptFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("File không tồn tại!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileName(txtFilePath.Text) + ".des";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                byte[] key = GetKeyBuffer(txtKey.Text);
                byte[] iv = key;

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = key;
                    des.IV = iv;
                    des.Padding = PaddingMode.PKCS7;

                    // Dùng FileAccess để quản lý quyền truy cập rõ ràng
                    using (FileStream fsInput = new FileStream(txtFilePath.Text, FileMode.Open, FileAccess.Read))
                    using (FileStream fsEncrypted = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                    using (CryptoStream cs = new CryptoStream(fsEncrypted, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // CopyTo tự động xử lý buffer, tránh lỗi file rác
                        fsInput.CopyTo(cs);
                    }
                }
                MessageBox.Show("Mã hóa file thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mã hóa file: " + ex.Message);
            }
        }

        // ==================== Giải mã file ====================
        private void btnDecryptFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("File không tồn tại!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            // Gợi ý tên file gốc bằng cách bỏ đuôi .des (nếu có)
            string originalName = Path.GetFileNameWithoutExtension(txtFilePath.Text);
            if (Path.GetExtension(txtFilePath.Text) != ".des") originalName += "_decrypted";
            sfd.FileName = originalName;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                byte[] key = GetKeyBuffer(txtKey.Text);
                byte[] iv = key;

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = key;
                    des.IV = iv;
                    des.Padding = PaddingMode.PKCS7;

                    using (FileStream fsInput = new FileStream(txtFilePath.Text, FileMode.Open, FileAccess.Read))
                    using (FileStream fsDecrypted = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                    using (CryptoStream cs = new CryptoStream(fsInput, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cs.CopyTo(fsDecrypted);
                    }
                }
                MessageBox.Show("Giải mã file thành công!");
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Sai mật khẩu hoặc file bị hỏng!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giải mã file: " + ex.Message);
            }
        }
    }
}