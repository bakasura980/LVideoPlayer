using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Have to Implement Our own button event for clearing items from list
//Have to add somehow the option for changing indexes of the ListBox
namespace LPlayer
{
    public partial class PlayList : Form
    {
        public PlayList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Listbox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }


        private void CreateCustomListBoxItems(string item, ref DragEventArgs drageventargs)
        {
                MenuStrip stripmenu = new MenuStrip();
                ToolStripMenuItem toolmenu = new ToolStripMenuItem(item);
                toolmenu.Text = item;
                toolmenu.DropDownItems.Add("Clear");
                stripmenu.Items.Add(toolmenu);
                stripmenu.Enabled = false;
                Listbox1.Items.Add(stripmenu);
                Listbox1.Controls.Add(stripmenu);
                Listbox1.GetItemText(stripmenu);
        }

        private void Listbox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropedfiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var item in dropedfiles)
            {
                CreateCustomListBoxItems(item, ref e);
            }
        }
        //Have to rework/ something does not work
        private void Listbox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            int index = 0;
            if (e.Clicks == 1)
            {
                index = Listbox1.IndexFromPoint(pt);
            }
            if (index > -1)
            {
                if (e.Clicks == 1 && Listbox1.GetSelected(index))
                {
                    MenuStrip menustrip = (MenuStrip)Listbox1.SelectedItem;
                    menustrip.Enabled = true;
                }
            }
        }
    }
}
