namespace FormsLib.Platformer
{
    partial class formPlatformerMainMenu
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(64, 64, 64);
            pictureBox1.Location = new Point(-91, 401);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1042, 50);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Lime;
            pictureBox2.Location = new Point(213, 317);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(42, 61);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "Player";
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(431, 303);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(96, 50);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            pictureBox3.Tag = "Play-Chess";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(431, 317);
            label1.Name = "label1";
            label1.Size = new Size(96, 23);
            label1.TabIndex = 3;
            label1.Text = "Play Chess";
            // 
            // formPlatformerMainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "formPlatformerMainMenu";
            Text = "formPlatformerMainMenu";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label1;
    }
}