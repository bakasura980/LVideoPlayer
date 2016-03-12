using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPlayer
{
    public class ScrollSubtitles
    {
        private int index;

        private string sentence;

        public string Sentence
        {
            get
            {
                return sentence;
            }
        }

        public string Sentenceendmin
        {
            get
            {
                return sentenceendmin;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        private string sentenceendmin;

        private ScrollSubtitles(string sentence, string endofsentence,int index)
        {
            this.index = index;
            this.sentence = sentence;
            this.sentenceendmin = endofsentence;
        }

        public ScrollSubtitles() { }

        public ScrollSubtitles GetScrolledSub(List<Subtitles> subs, int sec, int min, int hour)
        {
            VideoTime videotime = new VideoTime();
            var time = Convert.ToDateTime(videotime.PrintTime(sec, min, hour));

            int? sub = subs.FindLastIndex((x) => Convert.ToDateTime(x.Timebegin) < time && Convert.ToDateTime(x.Timeend) > time);

            if (sub != -1)
            {
                return new ScrollSubtitles(subs.ElementAt((int)sub).Text, subs.ElementAt((int)sub).Timeend,(int)sub);
            }
            else
            {
                return new ScrollSubtitles(string.Empty, string.Empty,
                subs.FindLastIndex((x) => Convert.ToDateTime(x.Timebegin) < time) + 1);
            }
        }
    }
}
