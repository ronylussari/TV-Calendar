namespace TV_Calendar
{
    partial class Element
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
            this.episodeLabel = new TV_Calendar.CustomLabel();
            this.titleLabel = new TV_Calendar.CustomLabel();
            this.SuspendLayout();
            // 
            // episodeLabel
            // 
            this.episodeLabel.CausesValidation = false;
            this.episodeLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.episodeLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.episodeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.episodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.episodeLabel.Location = new System.Drawing.Point(0, 25);
            this.episodeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.episodeLabel.Name = "episodeLabel";
            this.episodeLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.episodeLabel.Size = new System.Drawing.Size(265, 25);
            this.episodeLabel.TabIndex = 1;
            this.episodeLabel.Text = "episodeLabel";
            this.episodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.episodeLabel.MouseEnter += new System.EventHandler(this.labelEnter);
            this.episodeLabel.MouseLeave += new System.EventHandler(this.labelLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.CausesValidation = false;
            this.titleLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Padding = new System.Windows.Forms.Padding(2);
            this.titleLabel.Size = new System.Drawing.Size(265, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "titleLabel";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.titleLabel.MouseEnter += new System.EventHandler(this.labelEnter);
            this.titleLabel.MouseLeave += new System.EventHandler(this.labelLeave);
            // 
            // Element
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.CausesValidation = false;
            this.Controls.Add(this.episodeLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "Element";
            this.Size = new System.Drawing.Size(265, 50);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.Label episodeLabel;
        public CustomLabel episodeLabel;
        public CustomLabel titleLabel;
        //private System.Windows.Forms.Label titleLabel;
    }
}
