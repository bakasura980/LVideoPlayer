using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LPlayer
{
    class Helper
    {
        public List<SubsPropert> GetSub(string path)
        {
            List<SubsPropert> subpropert = new List<SubsPropert>();
            Regex begintextofsub = new Regex(@"(\d{2}:\d{2}:\d{2})");
            Regex endtextofsub = new Regex(@"(\d{2}:\d{2}:\d{2}),\d{3}$");
            Match matchbegin, matchend;
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    StringBuilder subtext = new StringBuilder();
                    matchbegin = begintextofsub.Match(line);
                    matchend = endtextofsub.Match(line);
                    do
                    {
                        line = reader.ReadLine();
                        if (line.ToString() != string.Empty)
                        {
                            subtext.Append(line + " ");
                        }
                    } while (line.ToString() != string.Empty);
                    subpropert.Add(new SubsPropert()
                    {
                        Timebegin = matchbegin.ToString(),
                        Timeend = matchend.ToString(),
                        Text = subtext.ToString()
                    });
                    line = reader.ReadLine();
                }
            }
            return subpropert;
        }
    }
}
