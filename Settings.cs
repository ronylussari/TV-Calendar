using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Threading;
using System.Diagnostics;

namespace TV_Calendar
{
    public partial class Settings : Form
    {
        //private string cat;
        private int EL_PER_WIDTH = 1;
        private int categoryPosition = 1;
        private int ElementCount = 0;
        private char[] _name = { ' ','#', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public Settings()
        {
            InitializeComponent();
            //this.ResizeRedraw = true;
            char[] delimiterChars = { ';' };
            string[] Favorites = Properties.Settings.Default.Favorites.Split(delimiterChars);
            this.categoryPosition = 23;
            backgroundWorker1.RunWorkerAsync(this.categoryPosition);

        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private void On_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            //// There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            htmlDoc.LoadHtml(DownloadPage("http://www.pogdesign.co.uk/cat/showselect.php"));
            HtmlNodeCollection series = htmlDoc.DocumentNode.SelectNodes("//div[@class='butthold'][" + (int)e.Argument + "]/div");

            ElementCount = series.Count;

            if (series != null)
            {
                for (int i = 1; i < ElementCount; i++) 
                {
                    Console.WriteLine(series[i].SelectSingleNode("label").InnerText.Trim());
                    backgroundWorker1.ReportProgress(i, correctTitle(series[i].SelectSingleNode("label").InnerText.Trim()));
                }
            }
            else 
            {
                Console.WriteLine("NULL NODE");
            }

        }

        private static string DownloadPage(string url)
        {
            string doc = string.Empty;
            WebClient client = new WebClient();
            client.Proxy = null;
            try
            {
                client.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.Headers.Add("Accept-Charset", "ISO-8859-2,utf-8;q=0.7,*;q=0.3");
                client.Headers.Add("Accept-Language", "pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4");
                client.Headers.Add("content-type", "application/x-www-form-urlencoded");
                //client.Headers.Add(HttpRequestHeader.Cookie, "__utma=101190144.673706671.1363098380.1363098380.1363098380.1; __utmz=101190144.1363098380.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); CAT_UID=26c8a45933f3b64eabf1ff824301038f"); //login
                client.Headers.Add(HttpRequestHeader.Cookie, "__utma=101190144.673706671.1363098380.1363098380.1363098380.1; __utmz=101190144.1363098380.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); CAT_UID=eb6417987a775cd29775c63b6175aca4");  //default
                //
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22");
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                doc = client.DownloadString(url);
            }
            return doc;
        }

        private void backgroundWorker1_Progress(object sender, ProgressChangedEventArgs e)
        {
            string title = (string)e.UserState;
            int i = e.ProgressPercentage-1;

            CustomButton button = new CustomButton();
            button.Title = title;
            if (Properties.Settings.Default.Favorites.Contains(title + ";"))
            {
                button.SwitchOn();
            }
            else 
            {
                button.SwitchOff();
            }
            button.Name = "custombutton" + i;
            button.OnUserControlButtonClicked += new EventHandler(seriesButton_Click);

            int Row = i / this.EL_PER_WIDTH;
            int Column = (i % this.EL_PER_WIDTH);
            if (this.EL_PER_WIDTH == 1) 
            {
                Column = 0;
            }
            
            Console.WriteLine("ROW => " + Row + ", COLUMN => " + Column);
            int marginLeft = (this.ClientSize.Width - (this.EL_PER_WIDTH*250))/2;
            //Console.WriteLine("(" + this.ClientSize.Width + " - " + 100 + ") - (" + Column + " * " + 260 + ") = " + marginLeft);
            int posX = marginLeft + Column * 250;
            int posY = 80 + 63 * Row;

            button.Location = new Point(posX, posY);
            this.Controls.Add(button);

        }

        private string correctTitle(string title) 
        {
            if(title.Contains("[The]"))
            {
                title = "The " + title.Replace("[The]", "");
                title = title.Trim();
            }

            if (title.Contains("&amp;"))
            {
                title = title.Replace("&amp;", "&");
                title = title.Trim();
            }
            return title;
        }

        private void seriesButton_Click(object sender, System.EventArgs e) 
        {
            
            CustomButton button = (CustomButton)sender;
            if (button.ButtonState == false)
            {
                if (Properties.Settings.Default.Favorites.Contains(button.Title + ";") != true)
                {
                    Properties.Settings.Default.Favorites += button.Title + ";";
                    Properties.Settings.Default.Save();
                    if (Properties.Settings.Default.Favorites.Contains(button.Title + ";"))
                    {
                        button.SwitchOn();
                        MessageBox.Show(Properties.Settings.Default.Favorites);
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.Favorites.Contains(button.Title + ";"))
                {
                    Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(button.Title + ";", "");
                    Properties.Settings.Default.Save();
                    if (Properties.Settings.Default.Favorites.Contains(button.Title + ";") != true)
                    {
                        button.SwitchOff();
                        MessageBox.Show(Properties.Settings.Default.Favorites);
                    }
                }
            }
        }

        private void next_Click(object sender, EventArgs e) 
        {
            if (this.backgroundWorker1.IsBusy == false && this.categoryPosition >= 1 && this.categoryPosition < 27)
            {
                CustomButton button;
                for (int i = 0; i < ElementCount; i++) 
                {
                    button = this.Controls[("custombutton" + i).ToString()] as CustomButton;
                    if (button != null)
                    {
                        button.Dispose();
                    }
                }
                this.categoryPosition++;
                this.customLabel1.Text = getCategory(this.categoryPosition);
                this.backgroundWorker1.RunWorkerAsync(this.categoryPosition);
            }
        }

        private void previous_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy == false && this.categoryPosition > 1 && this.categoryPosition <= 27)
            {
                CustomButton button;
                for (int i = 0; i < ElementCount; i++)
                {
                    button = this.Controls[("custombutton" + i).ToString()] as CustomButton;
                    if (button != null)
                    {
                        button.Dispose();
                    }
                }
                this.categoryPosition--;
                this.customLabel1.Text = getCategory(this.categoryPosition);
                this.backgroundWorker1.RunWorkerAsync(this.categoryPosition);
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.EL_PER_WIDTH = (this.ClientSize.Width - 100) / 250;
            this.customLabel1.SendToBack();
            this.PreviousArrowIcon.Location = new Point(55, 30);
            this.NextArrowIcon.Location = new Point(ClientSize.Width - 95, 30);
        }

        private string getCategory(int index)
        {
            return "" + _name[index];
        }

    }
}
