﻿namespace FormsLib.Platformer
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
            pictureBox3 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)orangePlatform).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // orangePlatform
            // 
            orangePlatform.BackColor = Color.FromArgb(64, 64, 64);
            orangePlatform.Location = new Point(-12, 406);
            orangePlatform.Name = "orangePlatform";
            orangePlatform.Size = new Size(911, 57);
            orangePlatform.TabIndex = 0;
            orangePlatform.TabStop = false;
            orangePlatform.Click += orangePlatform_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Yellow;
            pictureBox1.Location = new Point(713, 335);
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
            pictureBox2.Location = new Point(394, 234);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(42, 55);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "Player";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox3.Location = new Point(-48, -21);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(47, 459);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox5.Location = new Point(3, -21);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(30, 459);
            pictureBox5.TabIndex = 9;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox4.Location = new Point(811, -9);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(30, 459);
            pictureBox4.TabIndex = 10;
            pictureBox4.TabStop = false;
            // 
            // formPlatformer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(863, 450);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox3);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox orangePlatform;
        private PictureBox pictureBox1;
        private Label labelWin;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
    }
}