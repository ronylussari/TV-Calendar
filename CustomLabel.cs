using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing;

namespace TV_Calendar
{
    public partial class CustomLabel:Label
    {
        //private PrivateFontCollection private_fonts;

        public CustomLabel()
        {
            SetStyle(ControlStyles.Selectable, false);
            //this.filledLabel = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomLabel
            // 
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.On_Paint);
            this.ResumeLayout(false);

        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //if (this.filledLabel != true)
            //{

            RectangleF rF = new RectangleF();
            rF.Width = this.Width - this.Padding.Right;
            rF.Height = this.Height - this.Padding.Bottom;
            rF.Location = new Point(0 + this.Padding.Left, 0 + this.Padding.Top);

            SolidBrush brush = new SolidBrush(this.ForeColor);

            StringFormat format = new StringFormat();
            if (TextAlign == ContentAlignment.MiddleCenter)
            {
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
            }
            else 
            {
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Near;
            }

            g.DrawString(this.Text, new Font(this.Font.Name, this.Font.Size, this.Font.Style), brush, rF,format);
        }

    }
}
