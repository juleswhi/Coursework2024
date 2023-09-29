namespace FormsLib.Menus
{
    partial class MainMenu
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
            panelButtonBar = new Panel();
            h = new Button();
            btnPlay = new Button();
            panelButtonBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelButtonBar
            // 
            panelButtonBar.Anchor = AnchorStyles.None;
            panelButtonBar.Controls.Add(h);
            panelButtonBar.Controls.Add(btnPlay);
            panelButtonBar.Location = new Point(297, 0);
            panelButtonBar.Name = "panelButtonBar";
            panelButtonBar.Size = new Size(200, 451);
            panelButtonBar.TabIndex = 0;
            // 
            // h
            // 
            h.BackColor = Color.FromArgb(158, 179, 132);
            h.Dock = DockStyle.Top;
            h.FlatStyle = FlatStyle.Flat;
            h.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            h.ForeColor = Color.FromArgb(64, 64, 64);
            h.Location = new Point(0, 84);
            h.Margin = new Padding(20);
            h.Name = "h";
            h.Size = new Size(200, 84);
            h.TabIndex = 1;
            h.Text = "Settings";
            h.UseVisualStyleBackColor = false;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(158, 179, 132);
            btnPlay.Dock = DockStyle.Top;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            btnPlay.ForeColor = Color.FromArgb(64, 64, 64);
            btnPlay.Location = new Point(0, 0);
            btnPlay.Margin = new Padding(20);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(200, 84);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "Play Chess!";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(206, 222, 189);
            ClientSize = new Size(800, 450);
            Controls.Add(panelButtonBar);
            Name = "MainMenu";
            Text = "MainMenu";
            panelButtonBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelButtonBar;
        private Button btnPlay;
        private Button h;
    }
}