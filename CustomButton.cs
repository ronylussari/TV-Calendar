using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TV_Calendar
{
    public partial class CustomButton : UserControl
    {

        private int _TOP = 30;
        private int _LEFT = 10;
        private int _MarginV = 6;
        private int _MarginH = 6;
        private int _ParentWidth = 0;

        public CustomButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
        }

        public int ParentWidth 
        {
            get { return this._ParentWidth; }
            set { this._ParentWidth = value; _LEFT = (this._ParentWidth - (4 * (240 + this.MarginH))) / 2; Console.WriteLine(this._ParentWidth); }
        }

        public int TOP 
        {
            get { return this._TOP; }
            set { this._TOP = value; }
        }

        public int MarginV
        {
            get { return this._MarginV; }
            set { this._MarginV = value; }
        }

        public int MarginH
        {
            get { return this._MarginH; }
            set { this._MarginH = value; }
        }

        public string Title
        {
            get { return this.buttonTitle.Text; }
            set { this.buttonTitle.Text = value; }
        }

        public int Row 
        {
            get { return this.Location.Y; }
            set { this.Location = new Point(this.Location.X, _TOP + value * (48 + _MarginV)); }
        }

        public int Column 
        {
            get { return this.Location.X; }
            set { this.Location = new Point(_LEFT + value * (240 + _MarginV), this.Location.Y); }
        }

    }
}
