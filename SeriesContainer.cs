using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TV_Calendar
{
    public partial class SeriesContainer : UserControl
    {
        private const int TOP = 30;

        public SeriesContainer()
        {
            InitializeComponent();
        }

        public string Header 
        {
            get { return this.HeaderLabel.Text; }
            set { this.HeaderLabel.Text = value; }
        }

        public void AddSeries(string title, int row, int col) 
        {
            CustomButton cb = new CustomButton();
            cb.Title = title;
            cb.ParentWidth = Screen.GetWorkingArea(this).Width;
            cb.Row = row;
            cb.Column = col;
            this.Controls.Add(cb);
        }

        public void SetHeader(string title) 
        {
            this.HeaderLabel.Text = title;
        }
    }
}
