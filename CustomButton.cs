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
        private bool buttonState = false;
        public delegate void ButtonClickedEventHandler(object sender, EventArgs e);
        public event EventHandler OnUserControlButtonClicked;

        public CustomButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
        }

        public string Title
        {
            get { return this.buttonTitle.Text; }
            set { this.buttonTitle.Text = value; }
        }

        public bool ButtonState 
        {
            get { return this.buttonState; }
        }

        public void SwitchOn()
        {
            this.buttonState = true;
            this.Icon.Image = Properties.Resources.light_bulb_on;
        }

        public void SwitchOff()
        {
            this.buttonState = false;
            this.Icon.Image = Properties.Resources.light_bulb_off;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (OnUserControlButtonClicked != null)
                OnUserControlButtonClicked(this, e);
        }

        private void CustomButton_EnabledChanged(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
