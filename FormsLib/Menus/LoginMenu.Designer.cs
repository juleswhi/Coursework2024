namespace FormsLib.Menus
{
    partial class LoginMenu
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
            lblLogin = new Label();
            txtBoxUsername = new TextBox();
            txtBoxPassword = new TextBox();
            btnLogin = new Button();
            lblUsernameError = new Label();
            SuspendLayout();
            // 
            // lblLogin
            // 
            lblLogin.Anchor = AnchorStyles.None;
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lblLogin.ForeColor = Color.FromArgb(64, 64, 64);
            lblLogin.Location = new Point(116, 120);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(96, 37);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "Login";
            // 
            // txtBoxUsername
            // 
            txtBoxUsername.Anchor = AnchorStyles.None;
            txtBoxUsername.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxUsername.Location = new Point(116, 172);
            txtBoxUsername.Name = "txtBoxUsername";
            txtBoxUsername.Size = new Size(114, 22);
            txtBoxUsername.TabIndex = 1;
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.AcceptsReturn = true;
            txtBoxPassword.AcceptsTab = true;
            txtBoxPassword.Anchor = AnchorStyles.None;
            txtBoxPassword.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtBoxPassword.Location = new Point(116, 201);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.Size = new Size(114, 22);
            txtBoxPassword.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.Location = new Point(116, 230);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(114, 23);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblUsernameError
            // 
            lblUsernameError.AutoSize = true;
            lblUsernameError.ForeColor = SystemColors.MenuBar;
            lblUsernameError.Location = new Point(95, 276);
            lblUsernameError.Name = "lblUsernameError";
            lblUsernameError.Size = new Size(0, 15);
            lblUsernameError.TabIndex = 4;
            // 
            // LoginMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(206, 222, 189);
            ClientSize = new Size(349, 450);
            Controls.Add(lblUsernameError);
            Controls.Add(btnLogin);
            Controls.Add(txtBoxPassword);
            Controls.Add(txtBoxUsername);
            Controls.Add(lblLogin);
            Name = "LoginMenu";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLogin;
        private TextBox txtBoxUsername;
        private TextBox txtBoxPassword;
        private Button btnLogin;
        private Label lblUsernameError;
    }
}