namespace 拼图游戏
{
    partial class Form_Original
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
            this.pic_Original = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Original)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Original
            // 
            this.pic_Original.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_Original.Location = new System.Drawing.Point(0, 0);
            this.pic_Original.Name = "pic_Original";
            this.pic_Original.Size = new System.Drawing.Size(629, 483);
            this.pic_Original.TabIndex = 0;
            this.pic_Original.TabStop = false;
            this.pic_Original.Click += new System.EventHandler(this.pic_Original_Click);
            // 
            // Form_Original
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 483);
            this.Controls.Add(this.pic_Original);
            this.MaximizeBox = false;
            this.Name = "Form_Original";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "原图";
            this.Load += new System.EventHandler(this.Form_Original_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Original)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Original;
    }
}