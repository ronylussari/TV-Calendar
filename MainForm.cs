/*
 * Created by SharpDevelop.
 * User: krzysztof
 * Date: 2013-03-11
 * Time: 14:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text;
using HtmlAgilityPack;

namespace TV_Calendar
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
    public partial class MainForm : Form
	{

        private static DateTime thisDay = DateTime.Today;
        private int elementCount = 0;
        private int maxElementView = 10;
        private int WorkingHeight;
        private bool ElementExpand = false;
        private int changeSpeed = 1;

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            //Properties.Settings.Default.Favorites = "";
            //Properties.Settings.Default.Save();

            if (Properties.Settings.Default.Favorites == "")
            {
            }
            else
            {
                backgroundWorker.RunWorkerAsync();
            }
            moveForm();
            setDate();
            WorkingHeight = Screen.GetWorkingArea(this).Height;
            //
            //Properties.Settings.Default.Favorites = "Arrow;Bones;Continuum;Covert Affairs;Dexter;Game of Thrones;Greys Anatomy;Grimm;Hawaii Five-0;How I Met Your Mother;Lost Girl;The Mentalist;Merlin;New Girl;Nikita;Once Upon a Time;Teen Wolf;True Blood;The Vampire Diaries;";
		}

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            //// There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            htmlDoc.LoadHtml(DownloadToday("http://www.pogdesign.co.uk/cat/"));
            HtmlNodeCollection seriesToday = htmlDoc.DocumentNode.SelectNodes("//td[@class='today']/div");

            if (seriesToday != null)
            {
                elementCount = seriesToday.Count;
                char[] delimiterChars = { ';' };
                int licz = 0;
                
                List<int> Index = new List<int>();
                List<string> Title = new List<string>();
                List<string> TitleUrl = new List<string>();
                List<string> Episode = new List<string>();
                List<string> EpisodeUrl = new List<string>();
                List<string> Type = new List<string>();

                for (int index = 0; index < elementCount; index++)
                {
                    HtmlNode p = seriesToday[index].SelectSingleNode("p");
                    string title = correctTitle(p.SelectSingleNode("a[1]").InnerText.Trim());
                    if (Array.Exists(Properties.Settings.Default.Favorites.Split(delimiterChars), delegate(string s) { return s.Equals(title); }) != false)
                    {
                        Index.Add(licz);
                        Title.Add(title);
                        TitleUrl.Add(getSeriesUrl(p.SelectSingleNode("a[1]")));
                        Episode.Add(p.SelectSingleNode("a[2]").InnerText.Trim());
                        EpisodeUrl.Add(getEpisodeUrl(p.SelectSingleNode("a[2]")));
                        Type.Add(p.GetAttributeValue("class", "normal"));
                        licz++;
                    }
                }

                if (licz > 6)
                {
                    if ((licz * 38 + 100) > getMaxHeight())
                    {
                        maxElementView = (getMaxHeight()-100) / 38;
                        ElementExpand = false;
                        changeSpeed = 2;
                    }
                    else
                    {
                        maxElementView = licz;
                        ElementExpand = true;
                    }
                }
                else
                {
                    maxElementView = licz;
                    ElementExpand = true;
                }

                for (int index = 0; index < licz; index++)
                {
                    StorageTV storageTv = new StorageTV();

                    storageTv.Index = index;
                    storageTv.LastIndex = index - 1;
                    storageTv.Title = Title[index];
                    storageTv.TitleUrl = TitleUrl[index];
                    storageTv.Episode = Episode[index];
                    storageTv.EpisodeUrl = EpisodeUrl[index];
                    storageTv.Expand = ElementExpand;
                    storageTv.Type = Type[index];

                    backgroundWorker.ReportProgress(index, storageTv);
                }
                

            }
            else 
            {
                Console.WriteLine("html => NULL");
            }
            
        }

        private void BackgroundWorker_Progress(object sender0, System.ComponentModel.ProgressChangedEventArgs e0)
        {
            Element el = new Element();
            StorageTV sTv = (StorageTV)e0.UserState;

            el.Name = "Element" + sTv.Index;
            el.Title = sTv.Title;
            el.TitleUrl = sTv.TitleUrl;

            el.Episode = sTv.Episode;
            el.EpisodeUrl = sTv.EpisodeUrl;

            el.Index = sTv.Index;
            el.Location = new Point(0, (sTv.Index * 38) / 1 + 38);
            el.Expand = sTv.Expand;
            el.Type = sTv.Type;

            if (sTv.Type == "lastep")
            {
                el.titleLabel.ForeColor = Color.FromArgb(255, 204, 51, 51);
                el.episodeLabel.ForeColor = Color.FromArgb(255,204,51,51);
            }

            if (sTv.Type == "firstep")
            {
                el.titleLabel.ForeColor = Color.FromArgb(255, 130, 202, 59);
                el.episodeLabel.ForeColor = Color.FromArgb(255, 130, 202, 59);
            }

            if (sTv.Index % 2 == 0)
            {
                el.BackColor = Color.FromArgb(255, 44, 44, 44);
            }
            else
            {
                el.BackColor = Color.FromArgb(255, 53, 53, 53);
            }
            el.titleLabel.Click += (sender, e) => openSeries(sender, e, el.TitleUrl);
            el.episodeLabel.Click += (sender, e) => openSeries(sender, e, el.EpisodeUrl);
            this.Controls.Add(el);

        }

        private void BackgroundWorker_Completed(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (ElementExpand != true)
            {
                this.arrowIcon1.Location = new Point(208, 0);
                this.AutoScroll = true;
            }
            ChangeSize();
        }

        private void setDate()
        {
            int day = thisDay.Day;
            switch (day) 
            {
                case 1: case 21: case 31:
                    this.thLabel.Text = "st";
                    break;
                case 2: case 22:
                    this.thLabel.Text = "nd";
                    break;
                case 3: case 23:
                    this.thLabel.Text = "rd";
                    break;
            }
            this.numberLabel.Text = day.ToString();
            this.dayLabel.Text = thisDay.DayOfWeek.ToString();
        }

        private string correctTitle(string s) 
        {
            if (s.IndexOf("Summary") != -1)
            {
                return s.Replace("Summary", "").Trim();
            }
            else 
            {
                return s;
            }
        }

        private string correctEpisode(string s)
        {
            Match match = Regex.Match(s, @"^Season (?<season>\w+), Episode (?<episode>\w+)",
        RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return "S: " + match.Groups["season"].Value + "- Ep: " + match.Groups["episode"].Value;
            }
            else
            {
                return "null";
            }
        }

        private string getSeriesUrl(HtmlNode n) 
        {
            //System.Diagnostics.Debug.WriteLine(n.GetAttributeValue("href", ""));
            return "http://www.pogdesign.co.uk" + n.GetAttributeValue("href", "");
        }

        private string getEpisodeUrl(HtmlNode n)
        {
            return "http://www.pogdesign.co.uk" + n.GetAttributeValue("href", "");
        }
        
        private void ChangeSize()
        {
            // Element * height + topH + footer
            int WIN_H = 38 + (maxElementView) * 38;
            moveForm(WIN_H);
            //this.ClientSize = new Size(265,WIN_H);
            while (this.ClientSize.Height < WIN_H) 
            {
                this.ClientSize = new Size(265, this.ClientSize.Height + changeSpeed);
                Application.DoEvents();
            }
        }

        private int getMaxHeight()
        {
            return (WorkingHeight*80)/100;
        }

        private static string DownloadToday(string url = "")
        {

            if (url == "")
            {
                url = generateTodayUrl();
            }
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

            if (doc == null || doc == string.Empty)
            {
                MessageBox.Show("Error: Html content is null!");
                return null;
            }
            else
            {
                return doc;
            }
            
        }

        private void moveForm()
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width - 10,
                          workingArea.Bottom - Size.Height - 10);
        }

        private void moveForm(int h)
        {
            //int difH = Size.Height + h;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            //this.Location = new Point(workingArea.Right - Size.Width - 10,
                          //workingArea.Bottom - Size.Height - 10 - h + 38);
            Point p = new Point(workingArea.Right - Size.Width - 10, workingArea.Bottom - Size.Height - 10 - h + 38);

            while (this.Location.Y > p.Y)
            {
                this.Location = new Point(this.Location.X, this.Location.Y - changeSpeed);
                Application.DoEvents();
            }
        }

        private static string generateTodayUrl()
        {
            string d = thisDay.Day.ToString();
            string m = thisDay.Month.ToString();
            string y = thisDay.Year.ToString();
            
            return "http://www.pogdesign.co.uk/cat/day/" + d + "-" + m + "-" + y;
        }

        private void arrowClick(object sender, EventArgs e)
        {
            openUrl("http://www.pogdesign.co.uk/cat/");
        }

        private void exitMenuItem(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsMenuItem(object sender, EventArgs e)
        {
            Settings options = new Settings();
            options.Show();
        }

        private void menuItemEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.ForeColor = Color.Blue;
        }

        private void menuItemLeave(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.ForeColor = Color.White;
        }

        private void openEpisode(object sender, EventArgs e, string url)
        {
            openUrl(url);
        }

        private void openSeries(object sender, EventArgs e, string url)
        {   
            openUrl(url);
        }

        private void openUrl(string url) 
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                {
                    System.Diagnostics.Debug.WriteLine(noBrowser.Message);
                }
            }
            catch (System.Exception other)
            {
                System.Diagnostics.Debug.WriteLine(other.Message);
            }
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            // preventing scrollbar jumping
            this.Activate();
        }

        private void arrowEnter(object sender, EventArgs e)
        {
            arrowIcon1.ForeColor = Color.FromArgb(255, 255, 255, 255);
            arrowIcon1.Invalidate();
        }

        private void arrowLeave(object sender, EventArgs e)
        {
            arrowIcon1.ForeColor = Color.FromArgb(255, 74, 74, 74);
            arrowIcon1.Invalidate();
        }

    }
}