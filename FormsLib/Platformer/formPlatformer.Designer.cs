namespace FormsLib.Platformer
{
    partial class formPlatformer
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
            orangePlatform = new PictureBox();
            Player = new PictureBox();
            label1 = new Label();
            wallRight = new PictureBox();
            wallLeft = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)orangePlatform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wallRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wallLeft).BeginInit();
            SuspendLayout();
            // 
            // orangePlatform
            // 
            orangePlatform.BackColor = Color.OrangeRed;
            orangePlatform.Location = new Point(-19, 391);
            orangePlatform.Name = "orangePlatform";
            orangePlatform.Size = new Size(857, 32);
            orangePlatform.TabIndex = 0;
            orangePlatform.TabStop = false;
            // 
            // Player
            // 
            Player.BackColor = Color.FromArgb(0, 192, 0);
            Player.Location = new Point(366, 12);
            Player.Name = "Player";
            Player.Size = new Size(37, 50);
            Player.TabIndex = 1;
            Player.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(175, 73);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // wallRight
            // 
            wallRight.BackColor = Color.Fuchsia;
            wallRight.Location = new Point(737, 73);
            wallRight.Name = "wallRight";
            wallRight.Size = new Size(64, 321);
            wallRight.TabIndex = 3;
            wallRight.TabStop = false;
            // 
            // wallLeft
            // 
            wallLeft.BackColor = Color.Fuchsia;
            wallLeft.Location = new Point(0, 73);
            wallLeft.Name = "wallLeft";
            wallLeft.Size = new Size(64, 321);
            wallLeft.TabIndex = 4;
            wallLeft.TabStop = false;
            // 
            // formPlatformer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(wallLeft);
            Controls.Add(wallRight);
            Controls.Add(label1);
            Controls.Add(Player);
            Controls.Add(orangePlatform);
            Name = "formPlatformer";
            Text = "formPlatformer";
            ((System.ComponentModel.ISupportInitialize)orangePlatform).EndInit();
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            ((System.ComponentModel.ISupportInitialize)wallRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)wallLeft).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox orangePlatform;
        private PictureBox Player;
        private Label label1;
        private PictureBox wallRight;
        private PictureBox wallLeft;
    }
}