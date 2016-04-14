using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LPlayer
{
    public class Subtitles
    {
        private string timebegin;

        public string Timebegin
        {
            get
            {
                return timebegin;
            }

            set
            {
                timebegin = value;
            }
        }

        private string timeend;

        public string Timeend
        {
            get
            {
                return timeend;
            }

            set
            {
                timeend = value;
            }
        }

        private string text;

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}^{1}^{2}", this.timebegin, this.timeend, this.text);
        }

        private static void FillCollection(ref List<Subtitles> subscollection, params Group[] groups)
        {
            subscollection.Add(new Subtitles()
            {
                Timebegin = groups[0].ToString(),
                Timeend = groups[1].ToString(),
                Text = groups[2].ToString()
            });
        }

        public static List<Subtitles> GetSubs(string path)
        {
            List<Subtitles> subslist = new List<Subtitles>();
            Regex subregex = new Regex(@"(\d{2}:\d{2}:\d{2}),\d{3}\s+?-->\s+?(\d{2}:\d{2}:\d{2}),\d{3}\s+?(.*\n.*|.*|.+\n.*\n.*)");
            Match regexmatch = null;
            string line;
            try
            {
                using (StreamReader reader = new StreamReader(path, Encoding.Default))
                {
                    StringBuilder block = new StringBuilder();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != string.Empty)
                        {
                            block.Append(line + ' ');
                        }
                        else if (line == string.Empty || reader.EndOfStream)
                        {
                            regexmatch = subregex.Match(block.ToString());
                            FillCollection(ref subslist, regexmatch.Groups[1], regexmatch.Groups[2], regexmatch.Groups[3]);
                            block.Clear();
                        }
                    }
                }
                return subslist;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }

        private string minoftext = string.Empty;
        private string endofsubs = string.Empty;
        private int elementindex = 0;
        public string PrintSubs(List<Subtitles> subs, int sec, int min, int hour, bool isFscroll)
        {
            if (isFscroll)
            {
                ScrollSubtitles scrolled = new ScrollSubtitles().GetScrolledSub(subs,sec,min,hour);
                endofsubs = scrolled.Sentenceendmin;
                minoftext = scrolled.Sentence;
                elementindex = scrolled.Index;
                return minoftext;
            }
            else
            {
                if (Convert.ToDateTime(string.Format("{0}:{1}:{2}", hour, min, sec)) < Convert.ToDateTime(subs.ElementAt(elementindex).Timebegin))
                {
                    if (endofsubs != string.Empty)
                    {
                        if (Convert.ToDateTime(endofsubs) < Convert.ToDateTime(string.Format("{0}:{1}:{2}", hour, min, sec)))
                        {
                            minoftext = string.Empty;
                        }
                    }
                    return minoftext;
                }
                else
                {
                    minoftext = subs.ElementAt(elementindex).Text;
                    endofsubs = subs.ElementAt(elementindex).Timeend;
                    elementindex += 1;
                    return minoftext;
                }
            }
        }
    }
}
