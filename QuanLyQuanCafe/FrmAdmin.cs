using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();


            btnThongKe.Click += btnThongKe_Click;

            btnAddFood.Click += btnAddFood_Click;
            btnEditFood.Click += btnEditFood_Click;
            btnDeleteFood.Click += btnDeleteFood_Click;
            btnUploadImage.Click += btnUploadImage_Click;
            dgvFood.CellClick += dgvFood_CellClick;

            btnAddTable.Click += btnAddTable_Click;
            btnEditTable.Click += btnEditTable_Click;
            btnDeleteTable.Click += btnDeleteTable_Click;
            dgvTable.CellClick += dgvTable_CellClick;

            btnCreateAccount.Click += btnCreateAccount_Click;
            btnViewAccount.Click += btnViewAccount_Click;
            btnUpdateAccount.Click += btnUpdateAccount_Click;
            btnDeleteAccount.Click += btnDeleteAccount_Click;
            dgvAccount.CellClick += dgvAccount_CellClick;
        }

        private async void FrmAdmin_Load(object sender, EventArgs e)
        {
            await LoadTablesAsync();
            await LoadAccountsAsync();
            await LoadCategoryComboBoxAsync();
            await LoadFoodsAsync();
            cbAccountRole.Items.AddRange(new string[] { "Admin", "NhanVien" });
        }

        private async void btnThongKe_Click(object sender, EventArgs e)
        {
            await LoadBillsAsync();
        }

        private async Task LoadBillsAsync()
        {
            try
            {
                using (var db = new CafeDbContext())
                {
                    var fromDate = dtpFromDate.Value.Date;
                    var toDate = dtpToDate.Value.Date.AddDays(1).AddTicks(-1);

                    var bills = await db.Bills
                        .Include(b => b.TableFood)
                        .Where(b => b.DateCheckIn >= fromDate && b.DateCheckIn <= toDate && b.Status == 1)
                        .Select(b => new
                        {
                            b.Id,
                            TableName = b.TableFood.Name,
                            b.TotalPrice,
                            b.DateCheckIn,
                            b.DateCheckOut,
                            b.Discount
                        })
                        .ToListAsync();

                    dgvBill.DataSource = bills;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadCategoryComboBoxAsync()
        {
            try
            {
                using (var db = new CafeDbContext())
                {
                    var categories = await db.FoodCategories.ToListAsync();
                    cbFoodCategory.DataSource = categories;
                    cbFoodCategory.DisplayMember = "Name";
                    cbFoodCategory.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadFoodsAsync()
        {
            try
            {
                using (var db = new CafeDbContext())
                {
                    var foods = await db.Foods
                        .Include(f => f.Category)
                        .Select(f => new
                        {
                            f.Id,
                            f.Name,
                            CategoryName = f.Category.Name,
                            f.Price,
                            f.IdCategory
                        })
                        .ToListAsync();

                    dgvFood.DataSource = foods;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFood.CurrentRow != null)
            {
                txtFoodName.Text = dgvFood.CurrentRow.Cells["Name"].Value.ToString();
                nmFoodPrice.Value = Convert.ToDecimal(dgvFood.CurrentRow.Cells["Price"].Value);
                cbFoodCategory.SelectedValue = dgvFood.CurrentRow.Cells["IdCategory"].Value;
            }
        }

        private async void btnAddFood_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFoodName.Text)) return;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var food = new Food
                    {
                        Name = txtFoodName.Text.Trim(),
                        Price = (double)nmFoodPrice.Value,
                        IdCategory = (int)cbFoodCategory.SelectedValue
                    };

                    db.Foods.Add(food);
                    await db.SaveChangesAsync();
                    await LoadFoodsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnEditFood_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow == null) return;
            int id = (int)dgvFood.CurrentRow.Cells["Id"].Value;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var food = await db.Foods.FindAsync(id);
                    if (food != null)
                    {
                        food.Name = txtFoodName.Text.Trim();
                        food.Price = (double)nmFoodPrice.Value;
                        food.IdCategory = (int)cbFoodCategory.SelectedValue;

                        await db.SaveChangesAsync();
                        await LoadFoodsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow == null) return;
            int id = (int)dgvFood.CurrentRow.Cells["Id"].Value;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var food = await db.Foods.FindAsync(id);
                    if (food != null)
                    {
                        db.Foods.Remove(food);
                        await db.SaveChangesAsync();
                        await LoadFoodsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(sourcePath);
                    string destFolder = Path.Combine(Application.StartupPath, "Images");

                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    string destPath = Path.Combine(destFolder, fileName);
                    File.Copy(sourcePath, destPath, true);
                    MessageBox.Show("Upload " + fileName + " OK!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadTablesAsync()
        {
            try
            {
                using (var db = new CafeDbContext())
                {
                    var tables = await db.TableFoods.ToListAsync();
                    dgvTable.DataSource = tables;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTable.CurrentRow != null)
            {
                txtTableName.Text = dgvTable.CurrentRow.Cells["Name"].Value.ToString();
            }
        }

        private async void btnAddTable_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableName.Text)) return;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var table = new TableFood
                    {
                        Name = txtTableName.Text.Trim(),
                        Status = "Trống"
                    };

                    db.TableFoods.Add(table);
                    await db.SaveChangesAsync();
                    await LoadTablesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnEditTable_Click(object sender, EventArgs e)
        {
            if (dgvTable.CurrentRow == null) return;
            int id = (int)dgvTable.CurrentRow.Cells["Id"].Value;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var table = await db.TableFoods.FindAsync(id);
                    if (table != null)
                    {
                        table.Name = txtTableName.Text.Trim();
                        await db.SaveChangesAsync();
                        await LoadTablesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (dgvTable.CurrentRow == null) return;
            int id = (int)dgvTable.CurrentRow.Cells["Id"].Value;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var table = await db.TableFoods.FindAsync(id);
                    if (table != null)
                    {
                        db.TableFoods.Remove(table);
                        await db.SaveChangesAsync();
                        await LoadTablesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadAccountsAsync()
        {
            try
            {
                using (var db = new CafeDbContext())
                {
                    var accounts = await db.Accounts.ToListAsync();
                    dgvAccount.DataSource = accounts;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnViewAccount_Click(object sender, EventArgs e)
        {
            await LoadAccountsAsync();
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAccount.CurrentRow != null)
            {
                txtUsername.Text = dgvAccount.CurrentRow.Cells["Username"].Value.ToString();
                txtDisplayName.Text = dgvAccount.CurrentRow.Cells["DisplayName"].Value.ToString();
                txtPassword.Text = dgvAccount.CurrentRow.Cells["Password"].Value.ToString();
                cbAccountRole.Text = dgvAccount.CurrentRow.Cells["Role"].Value.ToString();
            }
        }

        private async void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) return;

            try
            {
                using (var db = new CafeDbContext())
                {
                    var account = new Account
                    {
                        Username = txtUsername.Text.Trim(),
                        DisplayName = txtDisplayName.Text.Trim(),
                        Password = txtPassword.Text.Trim(),
                        Role = cbAccountRole.Text.Trim()
                    };

                    db.Accounts.Add(account);
                    await db.SaveChangesAsync();
                    await LoadAccountsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            if (dgvAccount.CurrentRow == null) return;
            string username = dgvAccount.CurrentRow.Cells["Username"].Value.ToString();

            try
            {
                using (var db = new CafeDbContext())
                {
                    var account = await db.Accounts.FindAsync(username);
                    if (account != null)
                    {
                        account.DisplayName = txtDisplayName.Text.Trim();
                        account.Password = txtPassword.Text.Trim();
                        account.Role = cbAccountRole.Text.Trim();
                        await db.SaveChangesAsync();
                        await LoadAccountsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (dgvAccount.CurrentRow == null) return;
            string username = dgvAccount.CurrentRow.Cells["Username"].Value.ToString();

            try
            {
                using (var db = new CafeDbContext())
                {
                    var account = await db.Accounts.FindAsync(username);
                    if (account != null)
                    {
                        db.Accounts.Remove(account);
                        await db.SaveChangesAsync();
                        await LoadAccountsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cbAccountRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}