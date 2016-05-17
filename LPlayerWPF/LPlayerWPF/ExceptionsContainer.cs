using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPlayerWPF
{
    class FormatException : Exception
    {
        public FormatException(string message)
           : base(message)
        { }
    }
}
