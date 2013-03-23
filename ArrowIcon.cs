using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Drawing.Design;

namespace TV_Calendar
{
    public partial class ArrowIcon:Label
    {
        private const string _LeftArrow = "«";
        private const string _RightArrow = "»";

        public ArrowIcon() 
        {
            SetStyle(ControlStyles.Selectable, false);
            this.Size = new Size(30, 30);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ArrowIcon
            // 
            this.ResumeLayout(false);

        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            byte[] fontArray = Properties.Resources.OpenSans_ExtraBold;
            int dataLength = Properties.Resources.OpenSans_ExtraBold.Length;


            // ASSIGN MEMORY AND COPY  BYTE[] ON THAT MEMORY ADDRESS
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);


            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            //PASS THE FONT TO THE  PRIVATEFONTCOLLECTION OBJECT
            pfc.AddMemoryFont(ptrData, dataLength);

            //FREE THE  "UNSAFE" MEMORY
            Marshal.FreeCoTaskMem(ptrData);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            Font font = new Font(pfc.Families[0], Font.Size, FontStyle.Regular);
            e.Graphics.DrawString(Text, font, new SolidBrush(this.ForeColor), new PointF(20, 17), format);

        }

        public string LeftArrow 
        {
            get { return _LeftArrow; }
        }

        public string RightArrow
        {
            get { return _RightArrow; }
        }

    }
}
