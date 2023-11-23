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
            menuOption1 = new Controls.MenuOption();
            menuOption2 = new Controls.MenuOption();
            menuOption3 = new Controls.MenuOption();
            label1 = new Label();
            SuspendLayout();
            // 
            // menuOption1
            // 
            menuOption1.DefaultColor = Color.Yellow;
            menuOption1.Location = new Point(335, 101);
            menuOption1.Name = "menuOption1";
            menuOption1.OptionText = "Play Quiz";
            menuOption1.Selected = false;
            menuOption1.Size = new Size(107, 32);
            menuOption1.TabIndex = 0;
            menuOption1.Text = "Play Quit";
            menuOption1.KeyDown += LoginMenu_KeyDown;
            // 
            // menuOption2
            // 
            menuOption2.DefaultColor = Color.Yellow;
            menuOption2.Location = new Point(344, 156);
            menuOption2.Name = "menuOption2";
            menuOption2.OptionText = "Play Games";
            menuOption2.Selected = false;
            menuOption2.Size = new Size(83, 68);
            menuOption2.TabIndex = 1;
            menuOption2.Text = "menuOption2";
            // 
            // menuOption3
            // 
            menuOption3.DefaultColor = Color.Yellow;
            menuOption3.Location = new Point(334, 248);
            menuOption3.Name = "menuOption3";
            menuOption3.OptionText = "Settings";
            menuOption3.Selected = false;
            menuOption3.Size = new Size(93, 34);
            menuOption3.TabIndex = 2;
            menuOption3.Text = "menuOption3";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(139, 258);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // LoginMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(777, 437);
            Controls.Add(label1);
            Controls.Add(menuOption3);
            Controls.Add(menuOption2);
            Controls.Add(menuOption1);
            Name = "LoginMenu";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.MenuOption menuOption1;
        private Controls.MenuOption menuOption2;
        private Controls.MenuOption menuOption3;
        private Label label1;
    }
}