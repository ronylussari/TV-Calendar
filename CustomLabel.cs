using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TV_Calendar
{
    public class CustomLabel:Label
    {

        public CustomLabel()
        {
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            base.OnPaint(pe);
        }
    }
}
