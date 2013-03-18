using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TV_Calendar
{
    public partial class Element : UserControl
    {
        public Element()
        {
            InitializeComponent();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            SetStyle(ControlStyles.Selectable, false);
            this.BringToFront();
        }

        public string Title
        {
            get { return this.titleLabel.Text; }
            set { this.titleLabel.Text = value; }
        }

        public string Episode
        {
            get { return this.episodeLabel.Text; }
            set { this.episodeLabel.Text = value; }
        }

        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private string titleUrl;
        public string TitleUrl
        {
            get { return titleUrl; }
            set { titleUrl = value; }
        }

        private string episodeUrl;
        public string EpisodeUrl
        {
            get { return episodeUrl; }
            set { episodeUrl = value; }
        }

        private void labelEnter(object sender, EventArgs e)
        {
            CustomLabel cl = (CustomLabel)sender;
            if (cl.Name == "titleLabel")
            {
                cl.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
            else 
            {
                cl.Font = new System.Drawing.Font("Tahoma", 8.55F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
        }

        private void labelLeave(object sender, EventArgs e)
        {
            CustomLabel cl = (CustomLabel)sender;
            if (cl.Name == "titleLabel")
            {
                cl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
            else
            {
                cl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
        }


        public bool Expand
        {
            get { return (this.Width == 265) ? true : false; }
            set { this.Width = (value == true) ? 265 : 247; }
        }
    }
}
