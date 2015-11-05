using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LPlayer
{
    class Subs
    {
        private static string minoftext = string.Empty;
        public static string PrintSubs(string subspath, int sec, int min, int hour)
        {
             Regex begintextofsub = new Regex(@"(\d{2}:\d{2}:\d{2})");
             Regex endtextofsub = new Regex(@"(\d{2}:\d{2}:\d{2}),\d{3}$");
             Match matchbegin, matchend;
             StringBuilder subsmin = new StringBuilder();
            
            using (StreamReader reader = new StreamReader(subspath, Encoding.Default))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    matchbegin = begintextofsub.Match(line);
                    matchend = endtextofsub.Match(line);
                    if (matchbegin.ToString() != string.Empty)
                    {
                        if (matchbegin.ToString() == new PrintTime().PrintTimeVideo(sec, min, hour).ToString())
                        {
                            StringBuilder substext = new StringBuilder();
                            do
                            {
                                line = reader.ReadLine();
                                if (line.ToString() != string.Empty)
                                {
                                    substext.Append(line + " ");
                                }
                            } while (line.ToString() != string.Empty);
                            minoftext = substext.ToString();
                        }
                    }
                    if (matchend.ToString() != string.Empty)
                    {
                        if (matchend.ToString().Split(',').First().ToString() == new PrintTime().PrintTimeVideo(sec, min, hour).ToString())
                        {
                            minoftext = "";
                        }
                    }
                    line = reader.ReadLine();
                }
                if (minoftext != string.Empty)
                {
                    return minoftext;
                }
                else if(minoftext == "")
                {
                    return minoftext;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
