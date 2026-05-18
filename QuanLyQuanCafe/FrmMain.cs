#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe
{
    public partial class FrmMain : Form
    {
        private int? _selectedTableId;
        private bool _isBindingCategory;
        private System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        private Image qrCodeImage = null;
        private int printBillId = 0;
        private double printTotal = 0;
        private string printTableName = "";
        private int printDiscount = 0;
        private double _currentRawTotal = 0;

        public FrmMain(string displayName = "")
        {
            InitializeComponent();
            ConfigureStaticUi();
            WireEvents();

            if (this.Controls.Find("lblDisplayName", true).FirstOrDefault() is Label lbl)
            {
                lbl.Text = $"Xin chào, {displayName}!";
            }
        }

        private void ConfigureStaticUi()
        {
            listView1.BackColor = Color.White;
            listView1.ForeColor = Color.FromArgb(87, 54, 34);
            listView1.BorderStyle = BorderStyle.None;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = false;

            if (this.Controls.Find("cbDiscount", true).FirstOrDefault() is ComboBox cbDiscount)
            {
                cbDiscount.DataSource = null;
                cbDiscount.Items.Clear();
                cbDiscount.Items.AddRange(new object[] { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "100%" });
                cbDiscount.SelectedIndex = 0;
                cbDiscount.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void WireEvents()
        {
            Load += FrmMain_Load;

            cbCategory.SelectedIndexChanged += async (_, _) =>
            {
                if (!_isBindingCategory)
                {
                    await LoadFoodsAsync();
                }
            };

            txtSearchFood.TextChanged += async (_, _) => await LoadFoodsAsync();
            listView1.Resize += (_, _) => ResizeBillColumns();

            if (this.Controls.Find("btnRemoveFood", true).FirstOrDefault() is Button btnRemove)
                btnRemove.Click += BtnRemoveFood_Click;

            if (this.Controls.Find("btnCheckOut", true).FirstOrDefault() is Button btnCheck)
                btnCheck.Click += BtnCheckOut_Click;

            if (this.Controls.Find("btnSwitchTable", true).FirstOrDefault() is Button btnSwitch)
            {
                btnSwitch.Click += BtnSwitchTable_Click;
            }
            else
            {
                MessageBox.Show("Lỗi giao diện: Nút chuyển bàn chưa được đặt tên là btnSwitchTable!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (this.Controls.Find("cbDiscount", true).FirstOrDefault() is ComboBox cbDiscount)
                cbDiscount.SelectedIndexChanged += (_, _) => UpdateTotalDisplay();

            printDocument1.PrintPage += PrintDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Width = 800;
            printPreviewDialog1.Height = 1000;
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadTablesAsync();
            await LoadFoodsAsync();
            await LoadSwitchTableAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                using var db = new CafeDbContext();
                var categories = await db.FoodCategories
                    .OrderBy(c => c.Name)
                    .Select(c => new CategoryOption
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToListAsync();

                categories.Insert(0, new CategoryOption
                {
                    Id = 0,
                    Name = "Tất cả"
                });

                _isBindingCategory = true;
                cbCategory.DataSource = categories;
                cbCategory.DisplayMember = nameof(CategoryOption.Name);
                cbCategory.ValueMember = nameof(CategoryOption.Id);
                _isBindingCategory = false;
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
                using var db = new CafeDbContext();
                var tables = await db.TableFoods
                    .OrderBy(t => t.Id)
                    .ToListAsync();

                RenderTableButtons(tables);

                if (tables.Count == 0)
                {
                    ResetBillPanel();
                    return;
                }

                if (_selectedTableId != null)
                {
                    var selectedTable = tables.FirstOrDefault(t => t.Id == _selectedTableId);
                    if (selectedTable != null)
                    {
                        await SelectTableAsync(selectedTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadSwitchTableAsync()
        {
            try
            {
                using var db = new CafeDbContext();
                var tables = await db.TableFoods.ToListAsync();
                if (this.Controls.Find("cbSwitchTable", true).FirstOrDefault() is ComboBox cbSwitch)
                {
                    cbSwitch.DataSource = tables;
                    cbSwitch.DisplayMember = "Name";
                    cbSwitch.ValueMember = "Id";
                }
                else
                {
                    MessageBox.Show("Lỗi giao diện: Ô chọn bàn chưa được đặt tên là cbSwitchTable!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn chuyển: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderTableButtons(List<TableFood> tables)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                control.Dispose();
            }

            flowLayoutPanel1.Controls.Clear();

            var lblTableEmpty = this.Controls.Find("lblTableEmptyState", true).FirstOrDefault() as Label;
            if (lblTableEmpty != null)
            {
                lblTableEmpty.Visible = tables.Count == 0;
            }

            foreach (var table in tables)
            {
                string statusText = table.Status == "Trống" ? "TRỐNG" : "CÓ KHÁCH";
                string formattedName = FormatTableName(table.Name);

                var button = new Button
                {
                    Width = 116,
                    Height = 68,
                    Margin = new Padding(3),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    Text = formattedName + Environment.NewLine + statusText,
                    Tag = table,
                    UseVisualStyleBackColor = false
                };

                button.FlatAppearance.BorderColor = Color.FromArgb(221, 207, 190);
                button.Click += async (_, _) => await SelectTableAsync(table);
                ApplyTableButtonStyle(button, table.Id == _selectedTableId);
                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private async Task SelectTableAsync(TableFood table)
        {
            _selectedTableId = table.Id;

            if (this.Controls.Find("lblSelectedTableName", true).FirstOrDefault() is Label lblTableName)
                lblTableName.Text = FormatTableName(table.Name);

            if (this.Controls.Find("lblSelectedTableStatus", true).FirstOrDefault() is Label lblTableStatus)
                lblTableStatus.Text = GetStatusText(table.Status);

            if (this.Controls.Find("lblBillMeta", true).FirstOrDefault() is Label lblMeta)
                lblMeta.Text = table.Status == "Trống"
                    ? "Bàn đang trống, sẵn sàng phục vụ."
                    : "Bàn đang có khách hoặc có hóa đơn mở.";

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Button button && button.Tag is TableFood buttonTable)
                {
                    ApplyTableButtonStyle(button, buttonTable.Id == table.Id);
                }
            }

            await LoadOpenBillAsync(table.Id);
        }

        private void ApplyTableButtonStyle(Button button, bool isSelected)
        {
            if (isSelected)
            {
                button.BackColor = Color.FromArgb(229, 178, 97);
                button.ForeColor = Color.FromArgb(61, 36, 24);
                button.FlatAppearance.BorderColor = Color.FromArgb(194, 145, 72);
            }
            else
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.FromArgb(87, 54, 34);
                button.FlatAppearance.BorderColor = Color.FromArgb(221, 207, 190);
            }
        }

        private async Task LoadFoodsAsync()
        {
            try
            {
                using var db = new CafeDbContext();
                var query = db.Foods
                    .Include(f => f.Category)
                    .AsQueryable();

                int categoryId = 0;
                if (cbCategory.SelectedValue is int selectedCategoryId)
                {
                    categoryId = selectedCategoryId;
                }

                if (categoryId > 0)
                {
                    query = query.Where(f => f.IdCategory == categoryId);
                }

                string keyword = txtSearchFood.Text.Trim();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(f => f.Name.Contains(keyword));
                }

                var foods = await query
                    .OrderBy(f => f.Category.Name)
                    .ThenBy(f => f.Name)
                    .Select(f => new FoodCardInfo
                    {
                        Id = f.Id,
                        Name = f.Name,
                        CategoryName = f.Category.Name,
                        Price = f.Price
                    })
                    .ToListAsync();

                RenderFoodCards(foods);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RenderFoodCards(List<FoodCardInfo> foods)
        {
            foreach (Control control in flpFoodCards.Controls)
            {
                control.Dispose();
            }

            flpFoodCards.Controls.Clear();

            var lblFoodEmpty = this.Controls.Find("lblFoodEmptyState", true).FirstOrDefault() as Label;
            if (lblFoodEmpty != null)
            {
                lblFoodEmpty.Visible = foods.Count == 0;
            }

            foreach (var food in foods)
            {
                flpFoodCards.Controls.Add(CreateFoodCard(food));
            }
        }

        private Control CreateFoodCard(FoodCardInfo food)
        {
            var card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(218, 256),
                Margin = new Padding(3)
            };

            var picture = new PictureBox
            {
                BackColor = Color.FromArgb(245, 238, 229),
                Location = new Point(19, 16),
                Size = new Size(180, 106),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = LoadFoodImage(food.Id, food.Name)
            };

            var category = new Label
            {
                AutoSize = true,
                BackColor = Color.FromArgb(248, 231, 208),
                ForeColor = Color.FromArgb(122, 73, 37),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Location = new Point(19, 136),
                Padding = new Padding(10, 4, 10, 4),
                Text = food.CategoryName
            };

            var name = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(87, 54, 34),
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                Location = new Point(19, 176),
                Text = food.Name
            };

            var price = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(210, 110, 27),
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                Location = new Point(19, 215),
                Text = $"{food.Price:N0} đ"
            };

            var addButton = new Button
            {
                BackColor = Color.FromArgb(103, 68, 46),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                Location = new Point(121, 208),
                Size = new Size(78, 36),
                Text = "Chọn",
                Tag = food.Id,
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Hand
            };

            addButton.FlatAppearance.BorderSize = 0;
            addButton.Click += async (_, _) =>
            {
                if (_selectedTableId == null)
                {
                    if (this.Controls.Find("lblOrderEmptyState", true).FirstOrDefault() is Label lblOrderEmpty)
                        lblOrderEmpty.Text = "Hãy chọn bàn trước khi thao tác với món.";
                    return;
                }

                await AddFoodToTableAsync(food.Id, 1);
            };

            card.Controls.Add(addButton);
            card.Controls.Add(price);
            card.Controls.Add(name);
            card.Controls.Add(category);
            card.Controls.Add(picture);
            return card;
        }

        private async Task AddFoodToTableAsync(int foodId, int count)
        {
            if (_selectedTableId == null) return;

            try
            {
                using var db = new CafeDbContext();
                var bill = await db.Bills.FirstOrDefaultAsync(b => b.IdTable == _selectedTableId.Value && b.Status == 0);
                int billID = 0;

                if (bill == null)
                {
                    var nB = new Bill() { DateCheckIn = DateTime.Now, IdTable = _selectedTableId.Value, Status = 0, Discount = 0 };
                    db.Bills.Add(nB);
                    var table = await db.TableFoods.FindAsync(_selectedTableId.Value);
                    if (table != null) table.Status = "Có khách";
                    await db.SaveChangesAsync();
                    billID = nB.Id;
                }
                else
                {
                    billID = bill.Id;
                }

                var exist = await db.BillInfos.FirstOrDefaultAsync(bi => bi.IdBill == billID && bi.IdFood == foodId);
                if (exist != null)
                {
                    exist.Count += count;
                }
                else
                {
                    db.BillInfos.Add(new BillInfos() { IdBill = billID, IdFood = foodId, Count = count });
                }
                await db.SaveChangesAsync();

                await LoadTablesAsync();
                await LoadOpenBillAsync(_selectedTableId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnRemoveFood_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng click chọn một món trên hóa đơn để bớt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int foodId = (int)listView1.SelectedItems[0].Tag;

            try
            {
                using var db = new CafeDbContext();
                var bill = await db.Bills.FirstOrDefaultAsync(b => b.IdTable == _selectedTableId.Value && b.Status == 0);

                if (bill != null)
                {
                    var billInfo = await db.BillInfos.FirstOrDefaultAsync(bi => bi.IdBill == bill.Id && bi.IdFood == foodId);
                    if (billInfo != null)
                    {
                        billInfo.Count -= 1;
                        if (billInfo.Count <= 0)
                        {
                            db.BillInfos.Remove(billInfo);
                        }
                        await db.SaveChangesAsync();

                        await LoadTablesAsync();
                        await LoadOpenBillAsync(_selectedTableId.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateTotalDisplay()
        {
            var lblOrderTotal = this.Controls.Find("lblOrderTotalValue", true).FirstOrDefault() as Label;
            if (lblOrderTotal == null) return;

            int discount = 0;
            if (this.Controls.Find("cbDiscount", true).FirstOrDefault() is ComboBox cbDiscount)
            {
                int.TryParse(cbDiscount.Text.Replace("%", "").Trim(), out discount);
            }

            double finalTotal = _currentRawTotal - (_currentRawTotal * discount / 100);
            lblOrderTotal.Text = $"{finalTotal:N0} đ";
        }

        private async void BtnCheckOut_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new CafeDbContext();
                var bill = await db.Bills.FirstOrDefaultAsync(b => b.IdTable == _selectedTableId.Value && b.Status == 0);

                if (bill != null)
                {
                    var table = await db.TableFoods.FindAsync(_selectedTableId.Value);
                    string tableNameFormat = table != null ? table.Name : "Bàn";

                    double totalPrice = 0;
                    var details = await db.BillInfos.Where(bi => bi.IdBill == bill.Id).ToListAsync();

                    foreach (var item in details)
                    {
                        var food = await db.Foods.FindAsync(item.IdFood);
                        if (food != null)
                        {
                            totalPrice += (double)food.Price * item.Count;
                        }
                    }

                    int discount = 0;
                    if (this.Controls.Find("cbDiscount", true).FirstOrDefault() is ComboBox cbDiscount)
                    {
                        int.TryParse(cbDiscount.Text.Replace("%", "").Trim(), out discount);
                    }

                    double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

                    if (MessageBox.Show($"Thanh toán cho {tableNameFormat}?\nTổng tiền: {totalPrice:N0} VNĐ\nGiảm giá: {discount}%\nCần thu: {finalTotalPrice:N0} VNĐ", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        using var transaction = await db.Database.BeginTransactionAsync();
                        try
                        {
                            bill.Status = 1;
                            bill.DateCheckOut = DateTime.Now;
                            bill.Discount = discount;
                            bill.TotalPrice = finalTotalPrice;

                            if (table != null) table.Status = "Trống";

                            await db.SaveChangesAsync();
                            await transaction.CommitAsync();

                            printBillId = bill.Id;
                            printTotal = finalTotalPrice;
                            printDiscount = discount;
                            printTableName = tableNameFormat;

                            string bankBin = "970422";
                            string bankAccount = "0978144025";
                            string accountName = "TRAN CAM HUNG";
                            string addInfo = $"Thanh toan Bill {printBillId}";
                            string qrUrl = $"https://img.vietqr.io/image/{bankBin}-{bankAccount}-compact2.png?amount={finalTotalPrice}&addInfo={Uri.EscapeDataString(addInfo)}&accountName={Uri.EscapeDataString(accountName)}";

                            try
                            {
                                using var client = new System.Net.Http.HttpClient();
                                var response = await client.GetAsync(qrUrl);
                                using var stream = await response.Content.ReadAsStreamAsync();
                                qrCodeImage = Image.FromStream(stream);
                            }
                            catch { qrCodeImage = null; }

                            int itemCount = details.Count;
                            int baseHeight = 310;
                            int itemHeight = itemCount * 22;
                            int qrHeight = qrCodeImage != null ? 220 : 0;
                            int discountHeight = discount > 0 ? 50 : 0;
                            int dynamicHeight = baseHeight + itemHeight + qrHeight + discountHeight;

                            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Thermal80mm", 315, dynamicHeight);
                            printPreviewDialog1.ShowDialog();

                            ResetBillPanel();
                            await LoadTablesAsync();

                            if (this.Controls.Find("cbDiscount", true).FirstOrDefault() is ComboBox c)
                            {
                                c.SelectedIndex = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            MessageBox.Show("Lỗi quá trình lưu thanh toán. Đã hoàn tác! " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bàn này chưa có món nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float pageWidth = e.PageBounds.Width;

            Font titleFont = new Font("Segoe UI", 15, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 11, FontStyle.Bold);
            Font infoFont = new Font("Segoe UI", 9, FontStyle.Regular);
            Font tableHeaderFont = new Font("Segoe UI", 9, FontStyle.Bold);
            Font itemFont = new Font("Segoe UI", 9, FontStyle.Regular);
            Font totalFont = new Font("Segoe UI", 12, FontStyle.Bold);
            Font qrTitleFont = new Font("Segoe UI", 9, FontStyle.Italic);
            Font footerFont = new Font("Segoe UI", 10, FontStyle.Italic);

            int y = 15;
            int margin = 10;
            string text = "";
            SizeF textSize;

            text = "VELVET BEAN CAFE";
            textSize = g.MeasureString(text, titleFont);
            g.DrawString(text, titleFont, Brushes.Black, new PointF((pageWidth - textSize.Width) / 2, y));
            y += 25;

            text = "8C Tống Hữu Định, P. An Khánh, HCM";
            textSize = g.MeasureString(text, infoFont);
            g.DrawString(text, infoFont, Brushes.Black, new PointF((pageWidth - textSize.Width) / 2, y));
            y += 20;

            text = "HÓA ĐƠN THANH TOÁN";
            textSize = g.MeasureString(text, headerFont);
            g.DrawString(text, headerFont, Brushes.Black, new PointF((pageWidth - textSize.Width) / 2, y));
            y += 30;

            g.DrawString($"Bàn: {printTableName}", infoFont, Brushes.Black, new PointF(margin, y));
            string rightText = $"Mã: HD{printBillId.ToString("D5")}";
            textSize = g.MeasureString(rightText, infoFont);
            g.DrawString(rightText, infoFont, Brushes.Black, new PointF(pageWidth - margin - textSize.Width, y));
            y += 18;

            g.DrawString($"Ngày: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", infoFont, Brushes.Black, new PointF(margin, y));
            y += 22;

            g.DrawLine(new Pen(Color.Black, 1), margin, y, pageWidth - margin, y);
            y += 5;

            g.DrawString("Tên món", tableHeaderFont, Brushes.Black, new PointF(margin, y));
            g.DrawString("SL", tableHeaderFont, Brushes.Black, new PointF(160, y));
            g.DrawString("Đơn giá", tableHeaderFont, Brushes.Black, new PointF(190, y));
            g.DrawString("T.Tiền", tableHeaderFont, Brushes.Black, new PointF(250, y));
            y += 18;
            g.DrawLine(new Pen(Color.Black, 1), margin, y, pageWidth - margin, y);
            y += 5;

            double subTotal = 0;

            using (var db = new CafeDbContext())
            {
                var details = db.BillInfos.Where(bi => bi.IdBill == printBillId).ToList();
                foreach (var info in details)
                {
                    var food = db.Foods.Find(info.IdFood);
                    if (food != null)
                    {
                        string foodName = food.Name;
                        if (foodName.Length > 16) foodName = foodName.Substring(0, 14) + "..";

                        g.DrawString(foodName, itemFont, Brushes.Black, new PointF(margin, y));
                        g.DrawString(info.Count.ToString(), itemFont, Brushes.Black, new PointF(160, y));
                        g.DrawString((food.Price / 1000).ToString("N0") + "k", itemFont, Brushes.Black, new PointF(190, y));
                        g.DrawString(((food.Price * info.Count) / 1000).ToString("N0") + "k", itemFont, Brushes.Black, new PointF(250, y));
                        y += 22;

                        subTotal += food.Price * info.Count;
                    }
                }
            }

            y += 5;
            g.DrawLine(new Pen(Color.Black, 1), margin, y, pageWidth - margin, y);
            y += 10;

            if (printDiscount > 0)
            {
                text = $"Thành tiền: {subTotal:N0} đ";
                textSize = g.MeasureString(text, headerFont);
                g.DrawString(text, headerFont, Brushes.Black, new PointF(pageWidth - margin - textSize.Width, y));
                y += 25;

                double discountAmount = subTotal * printDiscount / 100;
                text = $"Giảm giá ({printDiscount}%): -{discountAmount:N0} đ";
                textSize = g.MeasureString(text, headerFont);
                g.DrawString(text, headerFont, Brushes.Black, new PointF(pageWidth - margin - textSize.Width, y));
                y += 25;
            }

            text = $"TỔNG TIỀN: {printTotal:N0} đ";
            textSize = g.MeasureString(text, totalFont);
            g.DrawString(text, totalFont, Brushes.Black, new PointF(pageWidth - margin - textSize.Width, y));
            y += 40;

            if (qrCodeImage != null)
            {
                text = "Quét mã để thanh toán";
                textSize = g.MeasureString(text, qrTitleFont);
                g.DrawString(text, qrTitleFont, Brushes.Black, new PointF((pageWidth - textSize.Width) / 2, y));
                y += 18;

                int qrSize = 150;
                float xQrImage = (pageWidth - qrSize) / 2;
                g.DrawImage(qrCodeImage, xQrImage, y, qrSize, qrSize);
                y += qrSize + 10;

                text = "MB BANK - 0978144025";
                textSize = g.MeasureString(text, infoFont);
                g.DrawString(text, infoFont, Brushes.Black, new PointF((pageWidth - textSize.Width) / 2, y));
                y += 20;
            }

            text = "Cảm ơn & Hẹn gặp lại!";
            textSize = g.MeasureString(text, footerFont);
            g.DrawString(text, footerFont, Brushes.Black, new PointF((pageWidth - textSize.Width) / 2, y));
        }

        private async void BtnSwitchTable_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == null)
            {
                MessageBox.Show("Vui lòng chọn bàn hiện tại (bàn đang có khách) trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cbSwitchTable = this.Controls.Find("cbSwitchTable", true).FirstOrDefault() as ComboBox;

            if (cbSwitchTable == null)
            {
                MessageBox.Show("Lỗi giao diện: Không tìm thấy ô chọn bàn (cbSwitchTable).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var tableTarget = cbSwitchTable.SelectedItem as TableFood;

            if (tableTarget == null || tableTarget.Id == _selectedTableId.Value)
            {
                MessageBox.Show("Vui lòng chọn một bàn khác từ danh sách để chuyển đến!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sourceName = "";
            if (this.Controls.Find("lblSelectedTableName", true).FirstOrDefault() is Label lblName)
                sourceName = lblName.Text;

            if (MessageBox.Show($"Bạn có chắc chắn muốn chuyển khách từ {sourceName} sang {tableTarget.Name} không?", "Xác nhận chuyển bàn", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    using var db = new CafeDbContext();

                    var checkTargetBill = await db.Bills.FirstOrDefaultAsync(b => b.IdTable == tableTarget.Id && b.Status == 0);
                    if (checkTargetBill != null)
                    {
                        MessageBox.Show("Bàn đích đang có khách! Vui lòng chọn một bàn trống để chuyển.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var billSource = await db.Bills.FirstOrDefaultAsync(b => b.IdTable == _selectedTableId.Value && b.Status == 0);

                    if (billSource != null)
                    {
                        billSource.IdTable = tableTarget.Id;

                        var tableSource = await db.TableFoods.FindAsync(_selectedTableId.Value);
                        if (tableSource != null) tableSource.Status = "Trống";

                        var targetDbTable = await db.TableFoods.FindAsync(tableTarget.Id);
                        if (targetDbTable != null) targetDbTable.Status = "Có khách";

                        await db.SaveChangesAsync();

                        ResetBillPanel();
                        await LoadTablesAsync();

                        MessageBox.Show("Chuyển bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bàn hiện tại không có hóa đơn nào để chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi chuyển bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task LoadOpenBillAsync(int tableId)
        {
            try
            {
                using var db = new CafeDbContext();
                var openBill = await db.Bills
                    .Where(b => b.IdTable == tableId && b.Status == 0)
                    .OrderByDescending(b => b.DateCheckIn)
                    .FirstOrDefaultAsync();

                listView1.Items.Clear();
                _currentRawTotal = 0;

                var lblOrderEmpty = this.Controls.Find("lblOrderEmptyState", true).FirstOrDefault() as Label;
                var lblMeta = this.Controls.Find("lblBillMeta", true).FirstOrDefault() as Label;

                if (openBill == null)
                {
                    if (lblOrderEmpty != null)
                    {
                        lblOrderEmpty.Text = "Chưa có món nào trong hóa đơn.";
                        lblOrderEmpty.Visible = true;
                    }
                    UpdateTotalDisplay();
                    ResizeBillColumns();
                    return;
                }

                var billItems = await (
                    from billInfo in db.BillInfos
                    join food in db.Foods on billInfo.IdFood equals food.Id
                    where billInfo.IdBill == openBill.Id
                    orderby food.Name
                    select new
                    {
                        food.Id,
                        food.Name,
                        billInfo.Count,
                        Price = food.Price,
                        Total = billInfo.Count * food.Price
                    })
                    .ToListAsync();

                foreach (var item in billItems)
                {
                    var listItem = new ListViewItem(item.Name);
                    listItem.Tag = item.Id;
                    listItem.SubItems.Add(item.Count.ToString());
                    listItem.SubItems.Add($"{item.Price:N0} đ");
                    listItem.SubItems.Add($"{item.Total:N0} đ");
                    listView1.Items.Add(listItem);

                    _currentRawTotal += item.Total;
                }

                int totalCount = billItems.Sum(item => item.Count);

                if (lblMeta != null)
                    lblMeta.Text = $"Hóa đơn #{openBill.Id} • {totalCount} món • {openBill.DateCheckIn:dd/MM HH:mm}";

                if (lblOrderEmpty != null)
                {
                    lblOrderEmpty.Visible = billItems.Count == 0;
                    lblOrderEmpty.Text = billItems.Count == 0
                        ? "Hóa đơn đang mở nhưng chưa có món."
                        : string.Empty;
                }

                UpdateTotalDisplay();
                ResizeBillColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetBillPanel()
        {
            _selectedTableId = null;
            _currentRawTotal = 0;

            if (this.Controls.Find("lblSelectedTableName", true).FirstOrDefault() is Label lblTableName)
                lblTableName.Text = "Bàn --";

            if (this.Controls.Find("lblSelectedTableStatus", true).FirstOrDefault() is Label lblTableStatus)
                lblTableStatus.Text = "Chưa có bàn trong dữ liệu.";

            if (this.Controls.Find("lblBillMeta", true).FirstOrDefault() is Label lblMeta)
                lblMeta.Text = "Không có dữ liệu hóa đơn.";

            if (this.Controls.Find("lblOrderEmptyState", true).FirstOrDefault() is Label lblOrderEmpty)
            {
                lblOrderEmpty.Text = "Chưa có món nào trong hóa đơn.";
                lblOrderEmpty.Visible = true;
            }

            UpdateTotalDisplay();
            listView1.Items.Clear();
            ResizeBillColumns();
        }

        private void ResizeBillColumns()
        {
            int width = Math.Max(listView1.ClientSize.Width, 280);
            if (listView1.Columns.Count >= 4)
            {
                listView1.Columns[0].Width = (int)(width * 0.38);
                listView1.Columns[1].Width = (int)(width * 0.12);
                listView1.Columns[2].Width = (int)(width * 0.22);
                listView1.Columns[3].Width = (int)(width * 0.24);
            }
        }

        private Image LoadFoodImage(int foodId, string foodName)
        {
            string imagePath = FindFoodImagePath(foodId, foodName);
            if (imagePath != null)
            {
                try
                {
                    using var image = Image.FromFile(imagePath);
                    return new Bitmap(image);
                }
                catch
                {
                }
            }

            return CreatePlaceholderImage(foodName);
        }

        private string FindFoodImagePath(int foodId, string foodName)
        {
            string imageFolder = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(imageFolder))
            {
                return null;
            }

            string byId = Directory.GetFiles(imageFolder, $"food_{foodId}.*").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(byId))
            {
                return byId;
            }

            string safeName = SanitizeFileName(foodName);
            string byName = Directory.GetFiles(imageFolder, $"{safeName}.*").FirstOrDefault();
            return byName;
        }

        private static string SanitizeFileName(string value)
        {
            return new string(value
                .Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch)
                .ToArray())
                .Replace(' ', '_');
        }

        private static string FormatTableName(string tableName)
        {
            if (tableName.StartsWith("Bàn", StringComparison.OrdinalIgnoreCase))
            {
                return tableName;
            }

            if (int.TryParse(tableName, out int tableNumber))
            {
                return $"Bàn {tableNumber:00}";
            }

            return $"Bàn {tableName}";
        }

        private static string GetStatusText(string status)
        {
            return string.Equals(status, "Trống", StringComparison.OrdinalIgnoreCase)
                ? "Bàn đang trống, chọn để bắt đầu hóa đơn mới."
                : "Bàn đang có khách hoặc đang được phục vụ.";
        }

        private static Image CreatePlaceholderImage(string foodName)
        {
            var bitmap = new Bitmap(180, 106);
            using var graphics = Graphics.FromImage(bitmap);
            using var backgroundBrush = new SolidBrush(Color.FromArgb(245, 238, 229));
            using var titleBrush = new SolidBrush(Color.FromArgb(122, 73, 37));
            using var subBrush = new SolidBrush(Color.FromArgb(168, 132, 103));
            using var borderPen = new Pen(Color.FromArgb(230, 214, 197));
            using var titleFont = new Font("Georgia", 18F, FontStyle.Bold);
            using var subFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            graphics.Clear(Color.White);
            graphics.FillRectangle(backgroundBrush, 0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawRectangle(borderPen, 0, 0, bitmap.Width - 1, bitmap.Height - 1);

            string shortName = foodName.Length > 12 ? foodName.Substring(0, 12) + "..." : foodName;
            string iconText = foodName.Length > 0 ? foodName.Substring(0, 1).ToUpperInvariant() : "?";

            graphics.DrawString(iconText, titleFont, titleBrush, new RectangleF(0, 18, bitmap.Width, 36), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
            graphics.DrawString(shortName, subFont, subBrush, new RectangleF(12, 66, bitmap.Width - 24, 24), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            return bitmap;
        }

        private sealed class CategoryOption
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private sealed class FoodSelectOption
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private sealed class FoodCardInfo
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string CategoryName { get; set; } = string.Empty;
            public double Price { get; set; }
        }
    }
}