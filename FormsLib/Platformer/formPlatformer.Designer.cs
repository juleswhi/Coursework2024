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
            pictureBox1 = new PictureBox();
            labelWin = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)orangePlatform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Yellow;
            pictureBox1.Location = new Point(671, 354);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 34);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "Token";
            // 
            // labelWin
            // 
            labelWin.AutoSize = true;
            labelWin.Font = new Font("Impact", 26.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelWin.Location = new Point(337, 92);
            labelWin.Name = "labelWin";
            labelWin.Size = new Size(0, 43);
            labelWin.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(0, 192, 192);
            pictureBox2.Location = new Point(119, 281);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(34, 50);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "Player";
            // 
            // formPlatformer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox2);
            Controls.Add(labelWin);
            Controls.Add(pictureBox1);
            Controls.Add(orangePlatform);
            Name = "formPlatformer";
            Tag = "";
            Text = "formPlatformer";
            ((System.ComponentModel.ISupportInitialize)orangePlatform).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox orangePlatform;
        private PictureBox pictureBox1;
        private Label labelWin;
        private PictureBox pictureBox2;
    }
}