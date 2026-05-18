#nullable disable
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nh?p ??y ?? tài kho?n và m?t kh?u!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new CafeDbContext();
                var account = await db.Accounts.FirstOrDefaultAsync(a => a.Username == username && a.Password == password);

                if (account != null)
                {
                    this.Hide();

                    if (account.Role == "Admin")
                    {
                        FrmAdmin frmAdmin = new FrmAdmin(account.DisplayName);
                        frmAdmin.ShowDialog();
                    }
                    else
                    {
                        FrmMain frmMain = new FrmMain(account.DisplayName);
                        frmMain.ShowDialog();
                    }

                    this.Show();
                    txtPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Sai tài kho?n ho?c m?t kh?u!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i k?t n?i c? s? d? li?u: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}