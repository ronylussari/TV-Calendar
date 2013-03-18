using System;
using System.Collections.Generic;
using System.Text;

namespace TV_Calendar
{
    class StorageTV
    {
        private string title;
        private string title_url;
        private string episode;
        private string episode_url;
        private int index;
        private int lastIndex;
        private bool expand;

        public int Index
        {
            get { return this.index; }
            set { this.index = value; }
        }

        public int LastIndex
        {
            get { return this.lastIndex; }
            set { this.lastIndex = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string TitleUrl
        {
            get { return title_url; }
            set { title_url = value; }
        }

        public string EpisodeUrl
        {
            get { return episode_url; }
            set { episode_url = value; }
        }

        public string Episode
        {
            get { return this.episode; }
            set { this.episode = value; }
        }


        public bool Expand
        {
            get { return this.expand; }
            set { this.expand = value; }
        }
    }
}
