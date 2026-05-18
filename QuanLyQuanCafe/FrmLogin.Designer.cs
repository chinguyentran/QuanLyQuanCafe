namespace QuanLyQuanCafe
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlShell = new Panel();
            pnlLoginCard = new Panel();
            btnExit = new Button();
            btnLogin = new Button();
            txtPassword = new TextBox();
            label2 = new Label();
            txtUsername = new TextBox();
            label1 = new Label();
            lblLoginNote = new Label();
            lblLoginTitle = new Label();
            pnlHeader = new Panel();
            lblHeaderSubtitle = new Label();
            lblHeaderTitle = new Label();
            pnlShell.SuspendLayout();
            pnlLoginCard.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlShell
            // 
            pnlShell.BackColor = Color.FromArgb(246, 239, 230);
            pnlShell.Controls.Add(pnlLoginCard);
            pnlShell.Controls.Add(pnlHeader);
            pnlShell.Dock = DockStyle.Fill;
            pnlShell.Location = new Point(0, 0);
            pnlShell.Name = "pnlShell";
            pnlShell.Padding = new Padding(20);
            pnlShell.Size = new Size(560, 420);
            pnlShell.TabIndex = 0;
            // 
            // pnlLoginCard
            // 
            pnlLoginCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlLoginCard.BackColor = Color.FromArgb(252, 248, 243);
            pnlLoginCard.Controls.Add(btnExit);
            pnlLoginCard.Controls.Add(btnLogin);
            pnlLoginCard.Controls.Add(txtPassword);
            pnlLoginCard.Controls.Add(label2);
            pnlLoginCard.Controls.Add(txtUsername);
            pnlLoginCard.Controls.Add(label1);
            pnlLoginCard.Controls.Add(lblLoginNote);
            pnlLoginCard.Controls.Add(lblLoginTitle);
            pnlLoginCard.Location = new Point(72, 150);
            pnlLoginCard.Name = "pnlLoginCard";
            pnlLoginCard.Padding = new Padding(28);
            pnlLoginCard.Size = new Size(416, 222);
            pnlLoginCard.TabIndex = 1;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.White;
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(214, 197, 180);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(87, 54, 34);
            btnExit.Location = new Point(214, 160);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 38);
            btnExit.TabIndex = 7;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(229, 178, 97);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(61, 36, 24);
            btnLogin.Location = new Point(52, 160);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(150, 38);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10.5F);
            txtPassword.Location = new Point(150, 116);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(214, 26);
            txtPassword.TabIndex = 5;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(87, 54, 34);
            label2.Location = new Point(52, 119);
            label2.Name = "label2";
            label2.Size = new Size(68, 19);
            label2.TabIndex = 4;
            label2.Text = "Mật khẩu";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10.5F);
            txtUsername.Location = new Point(150, 76);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(214, 26);
            txtUsername.TabIndex = 3;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(87, 54, 34);
            label1.Location = new Point(52, 79);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 2;
            label1.Text = "Tài khoản";
            // 
            // lblLoginNote
            // 
            lblLoginNote.AutoSize = true;
            lblLoginNote.Font = new Font("Segoe UI", 10F);
            lblLoginNote.ForeColor = Color.FromArgb(131, 90, 58);
            lblLoginNote.Location = new Point(32, 40);
            lblLoginNote.Name = "lblLoginNote";
            lblLoginNote.Size = new Size(220, 19);
            lblLoginNote.TabIndex = 1;
            lblLoginNote.Text = "Đăng nhập để vào hệ thống quán.";
            // 
            // lblLoginTitle
            // 
            lblLoginTitle.AutoSize = true;
            lblLoginTitle.Font = new Font("Georgia", 20F, FontStyle.Bold);
            lblLoginTitle.ForeColor = Color.FromArgb(87, 54, 34);
            lblLoginTitle.Location = new Point(28, 8);
            lblLoginTitle.Name = "lblLoginTitle";
            lblLoginTitle.Size = new Size(166, 31);
            lblLoginTitle.TabIndex = 0;
            lblLoginTitle.Text = "Đăng nhập";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(102, 66, 44);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(20, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(28);
            pnlHeader.Size = new Size(520, 104);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 11.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(244, 231, 214);
            lblHeaderSubtitle.Location = new Point(31, 61);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(221, 21);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Velvet Bean Cafe management";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Georgia", 24F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(28, 20);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(170, 38);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Welcome";
            // 
            // FrmLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(560, 420);
            Controls.Add(pnlShell);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Velvet Bean Cafe - Đăng nhập";
            pnlShell.ResumeLayout(false);
            pnlLoginCard.ResumeLayout(false);
            pnlLoginCard.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlShell;
        private Panel pnlHeader;
        private Label lblHeaderSubtitle;
        private Label lblHeaderTitle;
        private Panel pnlLoginCard;
        private Label lblLoginTitle;
        private Label lblLoginNote;
        private Label label1;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label label2;
        private Button btnLogin;
        private Button btnExit;
    }
}
