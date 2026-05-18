using System;
using System.Data;
using System.Drawing;
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
        public FrmAdmin(string displayName = "")
        {
            InitializeComponent();

            string welcomeText = $"Xin chào, {displayName}!";
            if (this.Controls.Find("lblUserTab1", true).FirstOrDefault() is Label lbl1) lbl1.Text = welcomeText;
            if (this.Controls.Find("lblUserTab2", true).FirstOrDefault() is Label lbl2) lbl2.Text = welcomeText;
            if (this.Controls.Find("lblUserTab3", true).FirstOrDefault() is Label lbl3) lbl3.Text = welcomeText;
            if (this.Controls.Find("lblUserTab4", true).FirstOrDefault() is Label lbl4) lbl4.Text = welcomeText;

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

            if (this.Controls.Find("btnPrintReport", true).FirstOrDefault() is Button btnPrint)
                btnPrint.Click += btnPrintReport_Click;
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

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (dgvBill.Rows.Count == 0 || (dgvBill.Rows.Count == 1 && dgvBill.Rows[0].IsNewRow))
            {
                MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel CSV (*.csv)|*.csv", FileName = "BaoCaoDoanhThu.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                        {
                            sw.Write("\ufeff");

                            string[] headers = { "Mã HĐ", "Tên bàn", "Giảm giá", "Thời gian ra", "Thành tiền" };
                            sw.WriteLine(string.Join(",", headers));

                            double totalRevenue = 0;

                            foreach (DataGridViewRow row in dgvBill.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    string id = "HD" + (row.Cells["Id"].Value?.ToString() ?? "");
                                    string tableName = row.Cells["TableName"].Value?.ToString() ?? "";
                                    string discount = (row.Cells["Discount"].Value?.ToString() ?? "0") + "%";

                                    string dateOut = "";
                                    if (row.Cells["DateCheckOut"].Value is DateTime dt)
                                    {
                                        dateOut = dt.ToString("dd/MM/yyyy HH:mm:ss");
                                    }

                                    double price = 0;
                                    if (row.Cells["TotalPrice"].Value != null)
                                    {
                                        double.TryParse(row.Cells["TotalPrice"].Value.ToString(), out price);
                                        totalRevenue += price;
                                    }

                                    string[] cells = { id, tableName, discount, dateOut, price.ToString("N0") };
                                    sw.WriteLine(string.Join(",", cells.Select(c => $"\"{c}\"")));
                                }
                            }

                            sw.WriteLine();
                            sw.WriteLine($",,,,Tổng doanh thu:,\"{totalRevenue:N0}\"");
                        }
                        MessageBox.Show("Xuất file báo cáo Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

                int foodId = (int)dgvFood.CurrentRow.Cells["Id"].Value;
                string foodName = txtFoodName.Text.Trim();
                string imageFolder = Path.Combine(Application.StartupPath, "Images");

                var picFoodImage = this.Controls.Find("picFoodImage", true).FirstOrDefault() as PictureBox;
                if (picFoodImage != null)
                {
                    if (picFoodImage.Image != null)
                    {
                        picFoodImage.Image.Dispose();
                        picFoodImage.Image = null;
                    }

                    if (Directory.Exists(imageFolder))
                    {
                        string imagePath = Directory.GetFiles(imageFolder, $"food_{foodId}.*").FirstOrDefault();

                        if (string.IsNullOrEmpty(imagePath))
                        {
                            string safeName = new string(foodName.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c).ToArray()).Replace(' ', '_');
                            imagePath = Directory.GetFiles(imageFolder, $"{safeName}.*").FirstOrDefault();
                        }

                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                            {
                                picFoodImage.Image = Image.FromStream(stream);
                            }
                        }
                    }
                }
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
                    bool isFoodUsed = await db.BillInfos.AnyAsync(bi => bi.IdFood == id);

                    if (isFoodUsed)
                    {
                        MessageBox.Show("Món này đã từng được bán và đang nằm trong lịch sử hóa đơn. Không thể xóa để bảo toàn dữ liệu doanh thu!\n\nGiải pháp: Hãy sửa tên hoặc sửa giá thay vì xóa.", "Từ chối xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var food = await db.Foods.FindAsync(id);
                    if (food != null)
                    {
                        db.Foods.Remove(food);
                        await db.SaveChangesAsync();

                        string imageFolder = Path.Combine(Application.StartupPath, "Images");
                        if (Directory.Exists(imageFolder))
                        {
                            string[] oldImages = Directory.GetFiles(imageFolder, $"food_{id}.*");
                            foreach (string img in oldImages)
                            {
                                try { File.Delete(img); } catch { }
                            }
                        }

                        await LoadFoodsAsync();
                        MessageBox.Show("Đã xóa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Có lỗi xảy ra: " + errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFood.CurrentRow == null || string.IsNullOrWhiteSpace(txtFoodName.Text) || txtFoodName.Text != dgvFood.CurrentRow.Cells["Name"].Value.ToString())
                {
                    MessageBox.Show("Vui lòng click chọn một món cụ thể trong bảng danh sách bên trái trước khi tải ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int foodId = Convert.ToInt32(dgvFood.CurrentRow.Cells["Id"].Value);
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = openFileDialog.FileName;
                    string destFolder = Path.Combine(Application.StartupPath, "Images");
                    string extension = Path.GetExtension(sourcePath).ToLowerInvariant();

                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    foreach (string existingFile in Directory.GetFiles(destFolder, $"food_{foodId}.*"))
                    {
                        File.Delete(existingFile);
                    }

                    string destPath = Path.Combine(destFolder, $"food_{foodId}{extension}");
                    File.Copy(sourcePath, destPath, true);

                    var picFoodImage = this.Controls.Find("picFoodImage", true).FirstOrDefault() as PictureBox;
                    if (picFoodImage != null)
                    {
                        if (picFoodImage.Image != null)
                        {
                            picFoodImage.Image.Dispose();
                        }
                        using (var stream = new FileStream(destPath, FileMode.Open, FileAccess.Read))
                        {
                            picFoodImage.Image = Image.FromStream(stream);
                        }
                    }

                    MessageBox.Show("Upload ảnh cho món thành công.");
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

        private void label1_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void cbAccountRole_SelectedIndexChanged(object sender, EventArgs e) { }
        private void pnlFoodEditorCard_Paint(object sender, PaintEventArgs e) { }
        private void lblFoodEditorSubtitle_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
        private void pnlRevenueFilterCard_Paint(object sender, PaintEventArgs e) { }
        private void pnlRevenueFilterCard_Paint_1(object sender, PaintEventArgs e) { }
    }
}