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
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text;

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
        //private static Stopwatch sw;

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            backgroundWorker.RunWorkerAsync();
            //sw = new Stopwatch();
            //sw.Start();
            moveForm();
            this.numberLabel.Text = thisDay.Day.ToString();
            this.dayLabel.Text = thisDay.DayOfWeek.ToString();
            WorkingHeight = Screen.GetWorkingArea(this).Height;
            Properties.Settings.Default.Favorites = "Arrow;Bones;Continuum;Covert Affairs;Dexter;Game of Thrones;Greys Anatomy;Grimm;Hawaii Five-0;How I Met Your Mother;Lost Girl;Mentalist [The];Merlin;New Girl;Nikita;Once Upon a Time;Teen Wolf;True Blood;Vampire Diaries [The]";
            Properties.Settings.Default.Save();

		}

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            //// There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            htmlDoc.LoadHtml(DownloadToday());
            HtmlNodeCollection seriesToday = htmlDoc.DocumentNode.SelectNodes("//div[@class='box930']/div[@class='contbox ovbox']");
            Console.WriteLine(seriesToday.Count);
            
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
                for (int index = 0; index < elementCount; index++)
                {

                    string title = correctTitle(seriesToday[index].SelectSingleNode("h4").InnerText.Trim());
                    if(Array.Exists(Properties.Settings.Default.Favorites.Split(delimiterChars), delegate(string s) { return s.Equals(title); })!=false)
                    {
                        Index.Add(licz);
                        Title.Add(title);
                        TitleUrl.Add(getSeriesUrl(seriesToday[index].SelectSingleNode("h4/a")));
                        Episode.Add(correctEpisode(seriesToday[index].SelectSingleNode("h5/a/span").InnerText.Trim()));
                        EpisodeUrl.Add(getEpisodeUrl(seriesToday[index].SelectSingleNode("h5/a")));
                        licz++;
                    }
                }

                if (licz > 6)
                {
                    if ((licz * 50 + 100) > getMaxHeight())
                    {
                        maxElementView = getMaxHeight() / 50;
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
                    backgroundWorker.ReportProgress(index, storageTv);
                }

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
            el.Location = new Point(0, (sTv.Index * 51)/1 + 38);
            el.Expand = sTv.Expand;

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
            ChangeSize();
            moveForm();
            //sw.Stop();
            //MessageBox.Show("Seconds => " + sw.Elapsed.Seconds);
        }

        public string correctTitle(string s) 
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

        public string correctEpisode(string s)
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

        public string getSeriesUrl(HtmlNode n) 
        {
            //System.Diagnostics.Debug.WriteLine(n.GetAttributeValue("href", ""));
            return "http://www.pogdesign.co.uk" + n.GetAttributeValue("href", "");
        }

        public string getEpisodeUrl(HtmlNode n)
        {
            return "http://www.pogdesign.co.uk" + n.GetAttributeValue("href", "");
        }

        private void ChangeSize()
        {
            // Element * height + topH + footer
            int WIN_H = 38 + (maxElementView) * 51;
            this.ClientSize = new Size(265,WIN_H);
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

        private void moveForm()
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width - 10,
                          workingArea.Bottom - Size.Height - 10);
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

        private void wIDTHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("WIN_W => " + this.Width);
            MessageBox.Show(""+this.maxElementView);
        }

        private void hEIGHTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("WIN_H => " + this.Height);
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            // preventing scrollbar jumping
            this.Activate();
        }

    }
}