namespace QuanLyQuanCafe
{
    partial class FrmMain
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            panel1 = new Panel();
            cbCategory = new ComboBox();
            cbFood = new ComboBox();
            nmFoodCount = new NumericUpDown();
            btnCheckOut = new Button();
            btnSwitchTable = new Button();
            btnPlusFood = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nmFoodCount).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(500, 661);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listView1.Dock = DockStyle.Fill;
            listView1.Location = new Point(500, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(684, 661);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tên Món";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Số Lượng";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Đơn Giá";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Thành Tiền";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPlusFood);
            panel1.Controls.Add(btnSwitchTable);
            panel1.Controls.Add(btnCheckOut);
            panel1.Controls.Add(nmFoodCount);
            panel1.Controls.Add(cbFood);
            panel1.Controls.Add(cbCategory);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(984, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 661);
            panel1.TabIndex = 2;
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(50, 60);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(121, 23);
            cbCategory.TabIndex = 0;
            // 
            // cbFood
            // 
            cbFood.FormattingEnabled = true;
            cbFood.Location = new Point(50, 118);
            cbFood.Name = "cbFood";
            cbFood.Size = new Size(121, 23);
            cbFood.TabIndex = 1;
            // 
            // nmFoodCount
            // 
            nmFoodCount.Location = new Point(57, 184);
            nmFoodCount.Name = "nmFoodCount";
            nmFoodCount.Size = new Size(120, 23);
            nmFoodCount.TabIndex = 2;
            // 
            // btnCheckOut
            // 
            btnCheckOut.Location = new Point(45, 244);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(98, 23);
            btnCheckOut.TabIndex = 3;
            btnCheckOut.Text = "Thanh toán";
            btnCheckOut.UseVisualStyleBackColor = true;
            // 
            // btnSwitchTable
            // 
            btnSwitchTable.Location = new Point(45, 297);
            btnSwitchTable.Name = "btnSwitchTable";
            btnSwitchTable.Size = new Size(98, 23);
            btnSwitchTable.TabIndex = 4;
            btnSwitchTable.Text = "Chuyển bàn";
            btnSwitchTable.UseVisualStyleBackColor = true;
            // 
            // btnPlusFood
            // 
            btnPlusFood.Location = new Point(45, 350);
            btnPlusFood.Name = "btnPlusFood";
            btnPlusFood.Size = new Size(98, 26);
            btnPlusFood.TabIndex = 5;
            btnPlusFood.Text = "Thêm món";
            btnPlusFood.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 661);
            Controls.Add(panel1);
            Controls.Add(listView1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phần mềm quản lý quán Cà phê / Trà sữa";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nmFoodCount).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Panel panel1;
        private Button btnPlusFood;
        private Button btnSwitchTable;
        private Button btnCheckOut;
        private NumericUpDown nmFoodCount;
        private ComboBox cbFood;
        private ComboBox cbCategory;
    }
}