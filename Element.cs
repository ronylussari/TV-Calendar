using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

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

        private string type;
        public string Type 
        {
            get { return type; }
            set { type = value; }
        }

        private void labelEnter(object sender, EventArgs e)
        {
            CustomLabel cl = (CustomLabel)sender;
            if (cl.Name == "titleLabel")
            {
                this.titleLabel.ForeColor = Color.FromArgb(255, 153, 153, 153);
            }
        }

        private void labelLeave(object sender, EventArgs e)
        {
            CustomLabel cl = (CustomLabel)sender;
            if (cl.Name == "titleLabel")
            {
                if (this.Type == "normal")
                {
                    this.titleLabel.ForeColor = Color.FromArgb(255, 255, 255, 255);
                }

                if (this.Type == "firstep")
                {
                    this.titleLabel.ForeColor = Color.FromArgb(255, 130, 202, 59);
                }

                if (this.Type == "lastep")
                {
                    this.titleLabel.ForeColor = Color.FromArgb(255, 204, 51, 51);
                }
            }

        }

        public bool Expand
        {
            get { return (this.Width == 265) ? true : false; }
            set { this.Width = (value == true) ? 265 : 247; }
        }
    }
}
