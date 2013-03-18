namespace TV_Calendar
{
    partial class CustomButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTitle = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonTitle
            // 
            this.buttonTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.buttonTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonTitle.FlatAppearance.BorderSize = 0;
            this.buttonTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTitle.ForeColor = System.Drawing.Color.White;
            this.buttonTitle.Location = new System.Drawing.Point(0, 0);
            this.buttonTitle.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTitle.Name = "buttonTitle";
            this.buttonTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.buttonTitle.Size = new System.Drawing.Size(192, 48);
            this.buttonTitle.TabIndex = 0;
            this.buttonTitle.Text = "buttonTitle";
            this.buttonTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTitle.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::TV_Calendar.Properties.Resources.light_bulb_off;
            this.pictureBox1.Location = new System.Drawing.Point(192, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // CustomButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonTitle);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CustomButton";
            this.Size = new System.Drawing.Size(240, 48);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
