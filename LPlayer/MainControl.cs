using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPlayer
{
    public class MainControl
    {
        public static void SetVisibleValue(bool isvisible, params Control[] maincontrols)
        {
            foreach (var item in maincontrols)
            {
                item.Visible = isvisible;
            }
        }
    }
}
