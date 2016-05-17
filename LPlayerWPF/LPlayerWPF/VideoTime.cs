using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPlayerWPF
{
    class VideoTime
    {
        public string PrintTime(int sec, int min, int hour)
        {
            string Timer = string.Empty;
            if (sec < 10 && min < 10 && hour < 10)
            {
                Timer = ("0" + hour.ToString() + ":0" + min.ToString() + ":0" + sec.ToString());
            }
            else if (sec >= 10 && min < 10 && hour < 10)
            {
                Timer = ("0" + hour.ToString() + ":0" + min.ToString() + ":" + sec.ToString());
            }
            else if (sec >= 10 && min >= 10 && hour < 10)
            {
                Timer = ("0" + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString());
            }
            else if (sec < 10 && min >= 10 && hour < 10)
            {
                Timer = ("0" + hour.ToString() + ":" + min.ToString() + ":0" + sec.ToString());
            }
            return Timer;
        }
    }
}
