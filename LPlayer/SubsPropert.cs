using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPlayer
{
    class SubsPropert
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
    }
}
