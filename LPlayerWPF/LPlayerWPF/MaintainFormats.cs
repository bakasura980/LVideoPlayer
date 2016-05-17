using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LPlayerWPF
{
    public static class MaintainFormats
    {
        private static List<string> supportableextensions = new List<string>()
        {
            "wmv",
            "mpeg",
            "avi",
            "mp4",
            "mp3"
        };


        public static bool CheckFormat(string video)
        {
            try
            {
                if (supportableextensions.Find((x) => x.Equals(video.Substring(video.Length - 3))) == null)
                {
                    throw new FormatException("This format is not supportable");
                }
                return true;
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
