namespace QuanLyQuanCafe
{
    partial class FrmAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label2 = new Label();
            label1 = new Label();
            btnThongKe = new Button();
            dgvBill = new DataGridView();
            dtpToDate = new DateTimePicker();
            dtpFromDate = new DateTimePicker();
            tabPage2 = new TabPage();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            nmFoodPrice = new NumericUpDown();
            dgvFood = new DataGridView();
            cbFoodCategory = new ComboBox();
            btnUploadImage = new Button();
            btnDeleteFood = new Button();
            btnEditFood = new Button();
            btnAddFood = new Button();
            txtFoodName = new TextBox();
            tabPage3 = new TabPage();
            label6 = new Label();
            btnDeleteTable = new Button();
            btnEditTable = new Button();
            btnAddTable = new Button();
            txtTableName = new TextBox();
            dgvTable = new DataGridView();
            tabPage4 = new TabPage();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            cbAccountRole = new ComboBox();
            btnDeleteAccount = new Button();
            btnUpdateAccount = new Button();
            btnViewAccount = new Button();
            btnCreateAccount = new Button();
            txtPassword = new TextBox();
            txtDisplayName = new TextBox();
            dgvAccount = new DataGridView();
            txtUsername = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBill).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nmFoodPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFood).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTable).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccount).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(984, 561);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(btnThongKe);
            tabPage1.Controls.Add(dgvBill);
            tabPage1.Controls.Add(dtpToDate);
            tabPage1.Controls.Add(dtpFromDate);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(976, 533);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Doanh Thu";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(389, 39);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 5;
            label2.Text = "Đến Ngày:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 35);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 4;
            label1.Text = "Từ Ngày:";
            label1.Click += label1_Click;
            // 
            // btnThongKe
            // 
            btnThongKe.Location = new Point(786, 31);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(75, 23);
            btnThongKe.TabIndex = 3;
            btnThongKe.Text = "Thống kê";
            btnThongKe.UseVisualStyleBackColor = true;
            // 
            // dgvBill
            // 
            dgvBill.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBill.Location = new Point(44, 84);
            dgvBill.Name = "dgvBill";
            dgvBill.Size = new Size(885, 413);
            dgvBill.TabIndex = 2;
            // 
            // dtpToDate
            // 
            dtpToDate.Location = new Point(469, 33);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(200, 23);
            dtpToDate.TabIndex = 1;
            // 
            // dtpFromDate
            // 
            dtpFromDate.Location = new Point(132, 33);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(200, 23);
            dtpFromDate.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(nmFoodPrice);
            tabPage2.Controls.Add(dgvFood);
            tabPage2.Controls.Add(cbFoodCategory);
            tabPage2.Controls.Add(btnUploadImage);
            tabPage2.Controls.Add(btnDeleteFood);
            tabPage2.Controls.Add(btnEditFood);
            tabPage2.Controls.Add(btnAddFood);
            tabPage2.Controls.Add(txtFoodName);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(976, 533);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Thức Ăn";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(660, 151);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 10;
            label5.Text = "Giá tiền:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(660, 102);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 9;
            label4.Text = "Danh mục:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(660, 56);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 8;
            label3.Text = "Tên món:";
            // 
            // nmFoodPrice
            // 
            nmFoodPrice.Location = new Point(816, 143);
            nmFoodPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nmFoodPrice.Name = "nmFoodPrice";
            nmFoodPrice.Size = new Size(120, 23);
            nmFoodPrice.TabIndex = 7;
            // 
            // dgvFood
            // 
            dgvFood.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFood.Location = new Point(19, 37);
            dgvFood.Name = "dgvFood";
            dgvFood.Size = new Size(574, 477);
            dgvFood.TabIndex = 6;
            // 
            // cbFoodCategory
            // 
            cbFoodCategory.FormattingEnabled = true;
            cbFoodCategory.Location = new Point(815, 99);
            cbFoodCategory.Name = "cbFoodCategory";
            cbFoodCategory.Size = new Size(121, 23);
            cbFoodCategory.TabIndex = 5;
            // 
            // btnUploadImage
            // 
            btnUploadImage.Location = new Point(735, 287);
            btnUploadImage.Name = "btnUploadImage";
            btnUploadImage.Size = new Size(121, 23);
            btnUploadImage.TabIndex = 4;
            btnUploadImage.Text = "Upload Ảnh";
            btnUploadImage.UseVisualStyleBackColor = true;
            // 
            // btnDeleteFood
            // 
            btnDeleteFood.Location = new Point(882, 221);
            btnDeleteFood.Name = "btnDeleteFood";
            btnDeleteFood.Size = new Size(75, 23);
            btnDeleteFood.TabIndex = 3;
            btnDeleteFood.Text = "Xoá";
            btnDeleteFood.UseVisualStyleBackColor = true;
            // 
            // btnEditFood
            // 
            btnEditFood.Location = new Point(763, 221);
            btnEditFood.Name = "btnEditFood";
            btnEditFood.Size = new Size(75, 23);
            btnEditFood.TabIndex = 2;
            btnEditFood.Text = "Sửa";
            btnEditFood.UseVisualStyleBackColor = true;
            // 
            // btnAddFood
            // 
            btnAddFood.Location = new Point(651, 218);
            btnAddFood.Name = "btnAddFood";
            btnAddFood.Size = new Size(75, 23);
            btnAddFood.TabIndex = 1;
            btnAddFood.Text = "Thêm";
            btnAddFood.UseVisualStyleBackColor = true;
            // 
            // txtFoodName
            // 
            txtFoodName.Location = new Point(815, 51);
            txtFoodName.Name = "txtFoodName";
            txtFoodName.Size = new Size(120, 23);
            txtFoodName.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(label6);
            tabPage3.Controls.Add(btnDeleteTable);
            tabPage3.Controls.Add(btnEditTable);
            tabPage3.Controls.Add(btnAddTable);
            tabPage3.Controls.Add(txtTableName);
            tabPage3.Controls.Add(dgvTable);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(976, 533);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Bàn Ăn";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(678, 95);
            label6.Name = "label6";
            label6.Size = new Size(52, 15);
            label6.TabIndex = 5;
            label6.Text = "Tên bàn:";
            // 
            // btnDeleteTable
            // 
            btnDeleteTable.Location = new Point(808, 252);
            btnDeleteTable.Name = "btnDeleteTable";
            btnDeleteTable.Size = new Size(75, 23);
            btnDeleteTable.TabIndex = 4;
            btnDeleteTable.Text = "Xoá Bàn";
            btnDeleteTable.UseVisualStyleBackColor = true;
            // 
            // btnEditTable
            // 
            btnEditTable.Location = new Point(808, 200);
            btnEditTable.Name = "btnEditTable";
            btnEditTable.Size = new Size(75, 23);
            btnEditTable.TabIndex = 3;
            btnEditTable.Text = "Sửa Bàn";
            btnEditTable.UseVisualStyleBackColor = true;
            // 
            // btnAddTable
            // 
            btnAddTable.Location = new Point(806, 146);
            btnAddTable.Name = "btnAddTable";
            btnAddTable.Size = new Size(75, 23);
            btnAddTable.TabIndex = 2;
            btnAddTable.Text = "Thêm Bàn";
            btnAddTable.UseVisualStyleBackColor = true;
            // 
            // txtTableName
            // 
            txtTableName.Location = new Point(781, 93);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(100, 23);
            txtTableName.TabIndex = 1;
            // 
            // dgvTable
            // 
            dgvTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTable.Location = new Point(25, 27);
            dgvTable.Name = "dgvTable";
            dgvTable.Size = new Size(614, 478);
            dgvTable.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(label10);
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(label8);
            tabPage4.Controls.Add(label7);
            tabPage4.Controls.Add(cbAccountRole);
            tabPage4.Controls.Add(btnDeleteAccount);
            tabPage4.Controls.Add(btnUpdateAccount);
            tabPage4.Controls.Add(btnViewAccount);
            tabPage4.Controls.Add(btnCreateAccount);
            tabPage4.Controls.Add(txtPassword);
            tabPage4.Controls.Add(txtDisplayName);
            tabPage4.Controls.Add(dgvAccount);
            tabPage4.Controls.Add(txtUsername);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(976, 533);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Tài Khoản";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(650, 173);
            label10.Name = "label10";
            label10.Size = new Size(72, 15);
            label10.TabIndex = 12;
            label10.Text = "Tên hiển thị:";
            label10.Click += label10_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(656, 241);
            label9.Name = "label9";
            label9.Size = new Size(61, 15);
            label9.TabIndex = 11;
            label9.Text = "Mật Khẩu:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(673, 295);
            label8.Name = "label8";
            label8.Size = new Size(45, 15);
            label8.TabIndex = 10;
            label8.Text = "Quyền:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(650, 117);
            label7.Name = "label7";
            label7.Size = new Size(89, 15);
            label7.TabIndex = 9;
            label7.Text = "Tên đăng nhập:";
            label7.Click += label7_Click;
            // 
            // cbAccountRole
            // 
            cbAccountRole.FormattingEnabled = true;
            cbAccountRole.Location = new Point(799, 277);
            cbAccountRole.Name = "cbAccountRole";
            cbAccountRole.Size = new Size(121, 23);
            cbAccountRole.TabIndex = 8;
            cbAccountRole.SelectedIndexChanged += cbAccountRole_SelectedIndexChanged;
            // 
            // btnDeleteAccount
            // 
            btnDeleteAccount.Location = new Point(534, 475);
            btnDeleteAccount.Name = "btnDeleteAccount";
            btnDeleteAccount.Size = new Size(121, 23);
            btnDeleteAccount.TabIndex = 7;
            btnDeleteAccount.Text = "Xoá Tài Khoản";
            btnDeleteAccount.UseVisualStyleBackColor = true;
            // 
            // btnUpdateAccount
            // 
            btnUpdateAccount.Location = new Point(401, 469);
            btnUpdateAccount.Name = "btnUpdateAccount";
            btnUpdateAccount.Size = new Size(127, 23);
            btnUpdateAccount.TabIndex = 6;
            btnUpdateAccount.Text = "Cập Nhật Tài Khoản";
            btnUpdateAccount.UseVisualStyleBackColor = true;
            // 
            // btnViewAccount
            // 
            btnViewAccount.Location = new Point(288, 468);
            btnViewAccount.Name = "btnViewAccount";
            btnViewAccount.Size = new Size(107, 23);
            btnViewAccount.TabIndex = 5;
            btnViewAccount.Text = "Xem Tài Khoản";
            btnViewAccount.UseVisualStyleBackColor = true;
            // 
            // btnCreateAccount
            // 
            btnCreateAccount.Location = new Point(163, 464);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Size = new Size(119, 23);
            btnCreateAccount.TabIndex = 4;
            btnCreateAccount.Text = "Tạo Tài Khoản";
            btnCreateAccount.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(799, 233);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 3;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(800, 173);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(100, 23);
            txtDisplayName.TabIndex = 2;
            // 
            // dgvAccount
            // 
            dgvAccount.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccount.Location = new Point(27, 77);
            dgvAccount.Name = "dgvAccount";
            dgvAccount.Size = new Size(566, 337);
            dgvAccount.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(797, 125);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(100, 23);
            txtUsername.TabIndex = 0;
            // 
            // FrmAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tabControl1);
            Name = "FrmAdmin";
            Text = "Hệ thống quản trị - Admin";
            Load += FrmAdmin_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBill).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nmFoodPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFood).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTable).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccount).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnThongKe;
        private DataGridView dgvBill;
        private DateTimePicker dtpToDate;
        private DateTimePicker dtpFromDate;
        private TabPage tabPage2;
        private Button btnEditFood;
        private Button btnAddFood;
        private TextBox txtFoodName;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button btnDeleteFood;
        private Button btnUploadImage;
        private Button btnDeleteTable;
        private Button btnEditTable;
        private Button btnAddTable;
        private TextBox txtTableName;
        private DataGridView dgvTable;
        private DataGridView dgvAccount;
        private TextBox txtUsername;
        private Button btnDeleteAccount;
        private Button btnUpdateAccount;
        private Button btnViewAccount;
        private Button btnCreateAccount;
        private TextBox txtPassword;
        private TextBox txtDisplayName;
        private DataGridView dgvFood;
        private ComboBox cbFoodCategory;
        private NumericUpDown nmFoodPrice;
        private ComboBox cbAccountRole;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label6;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label10;
    }
}