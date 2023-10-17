namespace FormsLib.Menus
{
    partial class formRegister
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
            txtBoxUsername = new TextBox();
            txtBoxSurname = new TextBox();
            txtBoxForename = new TextBox();
            txtBoxEmail = new TextBox();
            txtBoxPassword = new TextBox();
            dtpDOB = new DateTimePicker();
            radioMale = new RadioButton();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            btnRegister = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtBoxUsername
            // 
            txtBoxUsername.Location = new Point(331, 56);
            txtBoxUsername.Name = "txtBoxUsername";
            txtBoxUsername.Size = new Size(138, 23);
            txtBoxUsername.TabIndex = 0;
            txtBoxUsername.Text = "Username";
            // 
            // txtBoxSurname
            // 
            txtBoxSurname.Location = new Point(331, 172);
            txtBoxSurname.Name = "txtBoxSurname";
            txtBoxSurname.Size = new Size(138, 23);
            txtBoxSurname.TabIndex = 1;
            txtBoxSurname.Text = "Surname";
            // 
            // txtBoxForename
            // 
            txtBoxForename.Location = new Point(331, 143);
            txtBoxForename.Name = "txtBoxForename";
            txtBoxForename.Size = new Size(138, 23);
            txtBoxForename.TabIndex = 2;
            txtBoxForename.Text = "Forename";
            // 
            // txtBoxEmail
            // 
            txtBoxEmail.Location = new Point(331, 114);
            txtBoxEmail.Name = "txtBoxEmail";
            txtBoxEmail.Size = new Size(138, 23);
            txtBoxEmail.TabIndex = 3;
            txtBoxEmail.Text = "Email";
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.Location = new Point(331, 85);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.Size = new Size(138, 23);
            txtBoxPassword.TabIndex = 4;
            txtBoxPassword.Text = "Password";
            // 
            // dtpDOB
            // 
            dtpDOB.Location = new Point(331, 201);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(138, 23);
            dtpDOB.TabIndex = 5;
            // 
            // radioMale
            // 
            radioMale.AutoSize = true;
            radioMale.Location = new Point(316, 246);
            radioMale.Name = "radioMale";
            radioMale.Size = new Size(51, 19);
            radioMale.TabIndex = 6;
            radioMale.TabStop = true;
            radioMale.Text = "Male";
            radioMale.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(373, 246);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(63, 19);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "Female";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(442, 246);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(55, 19);
            radioButton2.TabIndex = 8;
            radioButton2.TabStop = true;
            radioButton2.Text = "Other";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(331, 286);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(138, 32);
            btnRegister.TabIndex = 9;
            btnRegister.Text = "Register!";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(713, 12);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // formRegister
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(206, 222, 189);
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnRegister);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(radioMale);
            Controls.Add(dtpDOB);
            Controls.Add(txtBoxPassword);
            Controls.Add(txtBoxEmail);
            Controls.Add(txtBoxForename);
            Controls.Add(txtBoxSurname);
            Controls.Add(txtBoxUsername);
            Name = "formRegister";
            Text = "formRegister";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBoxUsername;
        private TextBox txtBoxSurname;
        private TextBox txtBoxForename;
        private TextBox txtBoxEmail;
        private TextBox txtBoxPassword;
        private DateTimePicker dtpDOB;
        private RadioButton radioMale;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Button btnRegister;
        private Button btnBack;
    }
}