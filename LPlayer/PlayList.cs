using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Add scroll option
namespace LPlayer
{
    public partial class PlayList : Form
    {
        private List<Control> cmenustrip = new List<Control>();
        public PlayList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MouseWheel += PlayList_Wheel;
            this.panel2.AutoScroll = false;
            this.panel2.Controls.Add(new ScrollBar());
            this.panel2.MouseWheel += Panel2_MouseWheel;
        }

        private void Panel2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (panel2.Controls.Count > 0)
            {
                label1.Text = "Here";
                ScrollBar.PaintScroll(ref e, ref panel2, ref panel1, ref cmenustrip);
            }
        }

        private void Listbox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void CreateCustomListBoxItems(string item)
        {
            MenuStrip stripmenu = new MenuStrip();
            ToolStripMenuItem toolmenu = new ToolStripMenuItem(item);
            toolmenu.Text = item;
            toolmenu.DropDownItems.Add("Clear");
            stripmenu.Items.Add(toolmenu);
            stripmenu.Enabled = false;
            panel2.Controls.Add(stripmenu);
            cmenustrip.Add(stripmenu);
        }

        private void Listbox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropedfiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var item in dropedfiles)
            {
                CreateCustomListBoxItems(item);
            }
            if (cmenustrip.Count > 6)
            {
                this.panel2.VerticalScroll.Enabled = false;
                this.panel2.VerticalScroll.Visible = false;
            }
        }
        private void PlayList_Wheel(object sender, MouseEventArgs m)
        {
            
        }

        private int indextoremove;
        private MenuStrip enabledmenu;
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            MenuStrip currentmenustrip = (MenuStrip)panel2.GetChildAtPoint(panel2.PointToClient(MousePosition));
            if (currentmenustrip != null)
            {
                if (enabledmenu != currentmenustrip && enabledmenu != null)
                {
                    enabledmenu.Enabled = false;
                    currentmenustrip.Enabled = true;
                    currentmenustrip.MouseClick += Currentmenustrip_MouseClick;
                    enabledmenu = currentmenustrip;
                }
                else
                {
                    currentmenustrip.Enabled = true;
                    currentmenustrip.MouseClick += Currentmenustrip_MouseClick;
                    enabledmenu = currentmenustrip;
                }
            }
        }
        private void Currentmenustrip_MouseClick(object sender, MouseEventArgs e)
        {
            MenuStrip stripmenu = (MenuStrip)sender;
            indextoremove = panel2.Controls.IndexOf(stripmenu);
            ToolStripMenuItem toolmenuitem = new ToolStripMenuItem();
            foreach (var item in stripmenu.Items)
            {
                toolmenuitem = (ToolStripMenuItem)item;
            }
            foreach (var item in toolmenuitem.DropDownItems)
            {
                ((ToolStripDropDownItem)item).Click += PlayList_Click;
            }
        }

        private void PlayList_Click(object sender, EventArgs e)
        {
            if (panel2.Controls.Count != 0)
            {
                panel2.Controls.Remove(enabledmenu);
                cmenustrip.Remove(enabledmenu);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
