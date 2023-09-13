namespace DesktopUI.Forms
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
            SidePanel = new Panel();
            btnQuiz = new Button();
            SidePanel.SuspendLayout();
            SuspendLayout();
            // 
            // SidePanel
            // 
            SidePanel.BackColor = Color.FromArgb(206, 222, 189);
            SidePanel.Controls.Add(btnQuiz);
            SidePanel.Dock = DockStyle.Left;
            SidePanel.Location = new Point(0, 0);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(150, 450);
            SidePanel.TabIndex = 0;
            // 
            // btnQuiz
            // 
            btnQuiz.Dock = DockStyle.Top;
            btnQuiz.Location = new Point(0, 0);
            btnQuiz.Name = "btnQuiz";
            btnQuiz.Size = new Size(150, 75);
            btnQuiz.TabIndex = 0;
            btnQuiz.Text = "Quiz";
            btnQuiz.UseVisualStyleBackColor = true;
            btnQuiz.Click += btnQuiz_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 241, 228);
            ClientSize = new Size(800, 450);
            Controls.Add(SidePanel);
            Name = "MainMenu";
            Text = "MainMenu";
            SidePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel SidePanel;
        private Button btnQuiz;
    }
}