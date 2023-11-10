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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)orangePlatform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // orangePlatform
            // 
            orangePlatform.BackColor = Color.FromArgb(64, 64, 64);
            orangePlatform.Location = new Point(-38, 403);
            orangePlatform.Name = "orangePlatform";
            orangePlatform.Size = new Size(911, 57);
            orangePlatform.TabIndex = 0;
            orangePlatform.TabStop = false;
            orangePlatform.Click += orangePlatform_Click;
            // 
            // Player
            // 
            Player.BackColor = Color.FromArgb(128, 255, 128);
            Player.BorderStyle = BorderStyle.FixedSingle;
            Player.Location = new Point(454, 345);
            Player.Name = "Player";
            Player.Size = new Size(54, 52);
            Player.TabIndex = 3;
            Player.TabStop = false;
            Player.Tag = "Player";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox1.Location = new Point(276, 194);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(117, 66);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox2.Location = new Point(514, 303);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(117, 108);
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox3.Location = new Point(637, 264);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(117, 147);
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // formPlatformer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(Player);
            Controls.Add(orangePlatform);
            Name = "formPlatformer";
            Text = "formPlatformer";
            ((System.ComponentModel.ISupportInitialize)orangePlatform).EndInit();
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox orangePlatform;
        private PictureBox Player;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}