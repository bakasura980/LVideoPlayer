using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPlayer
{
    public class ScrollBar : ScrollableControl
    {
        private static int selecteditem;
        private static int beginline = 0;
        private static int endline = 51;
        private static int oldscrolled = 0;
        private static int newscrolled = 0;
        private static Graphics graphic;
        private static Pen pen;

        public static Point Position()
        {
            Point point = new Point(0, beginline);
            return point;
        }


        private static Panel panel2;

        public static void PaintScroll(ref MouseEventArgs margs,ref Panel listpanel,ref Panel scrollpanel, ref List<Control> controllist)
        {
            panel2 = listpanel;
            listpanel.MouseWheel -= Listpanel_MouseWheel;
            newscrolled += margs.Delta;
            listpanel.MouseWheel += Listpanel_MouseWheel; 
            if (newscrolled > oldscrolled)
            {
                selecteditem += 1;
                beginline += 50;
                endline += 50;
            }
            else if (newscrolled < oldscrolled)
            {
                selecteditem -= 1;
                beginline -= 50;
                endline -= 50;
            }
            oldscrolled = newscrolled;
            scrollpanel.Invalidate();
            graphic = scrollpanel.CreateGraphics();
            scrollpanel.Paint += PaintScrollBar;
        }

        private static void Listpanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int counter = 0;
            foreach (Control item in panel2.Controls)
            {
                counter++;
                if (panel2.Controls.Count / 2 >= counter)
                {
                    item.Select();
                    item.Enabled = true;
                    item.Show();
                    MessageBox.Show("Scroll");

                }
            }
        }

        private static void PaintScrollBar(object sender, PaintEventArgs paintargs)
        {
            pen = new Pen(Color.Red);
            pen.Width = 25;
            graphic.DrawLine(pen, 0, beginline, 0, endline);
        }
    }
}
