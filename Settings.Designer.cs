namespace TV_Calendar
{
    partial class Settings
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PreviousArrowIcon = new TV_Calendar.ArrowIcon();
            this.customLabel1 = new TV_Calendar.CustomLabel();
            this.NextArrowIcon = new TV_Calendar.ArrowIcon();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_Progress);
            // 
            // PreviousArrowIcon
            // 
            this.PreviousArrowIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.PreviousArrowIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PreviousArrowIcon.ForeColor = System.Drawing.Color.White;
            this.PreviousArrowIcon.Location = new System.Drawing.Point(55, 30);
            this.PreviousArrowIcon.Name = "PreviousArrowIcon";
            this.PreviousArrowIcon.Size = new System.Drawing.Size(40, 38);
            this.PreviousArrowIcon.TabIndex = 1;
            this.PreviousArrowIcon.Text = this.PreviousArrowIcon.LeftArrow;
            this.PreviousArrowIcon.Click += new System.EventHandler(previous_Click);
            // 
            // customLabel1
            // 
            this.customLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.customLabel1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.customLabel1.ForeColor = System.Drawing.Color.White;
            this.customLabel1.Location = new System.Drawing.Point(50, 30);
            this.customLabel1.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(400, 38);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "#";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NextArrowIcon
            // 
            this.NextArrowIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.NextArrowIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NextArrowIcon.ForeColor = System.Drawing.Color.White;
            this.NextArrowIcon.Location = new System.Drawing.Point(55, 30);
            this.NextArrowIcon.Name = "NextArrowIcon";
            this.NextArrowIcon.Size = new System.Drawing.Size(40, 38);
            this.NextArrowIcon.TabIndex = 2;
            this.NextArrowIcon.Text = this.NextArrowIcon.RightArrow;
            this.NextArrowIcon.Click += new System.EventHandler(next_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(121)))), ((int)(((byte)(171)))));
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.NextArrowIcon);
            this.Controls.Add(this.PreviousArrowIcon);
            this.Controls.Add(this.customLabel1);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Settings_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.On_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private CustomLabel customLabel1;
        private ArrowIcon PreviousArrowIcon;
        private ArrowIcon NextArrowIcon;




    }
}