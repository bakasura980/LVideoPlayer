using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPlayerWPF
{
    public partial class PlayList : Form
    {
        private PlayListVideosList videos = new PlayListVideosList();
        private bool isopen = false;

        public bool IsOpen
        {
            get
            {
                return isopen;
            }
            set
            {
                isopen = value;
            }
        }

        public PlayList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private string CheckVideoFormat(string video)
        {
            if (MaintainFormats.CheckFormat(video))
            {
                videos.Add(video);
                string nameofvideo = video.Substring(video.LastIndexOf('\\') + 1);
                return nameofvideo;
            }
            else
            {
                return null;
            }
        }

        private void CreateDinamicalyMenu(string nameofthedropedfile)
        {
            string name = CheckVideoFormat(nameofthedropedfile);
            if (name != null)
            {
                MenuStrip stripmenu = new MenuStrip();
                ToolStripMenuItem toolmenu = new ToolStripMenuItem(name);
                toolmenu.DropDownItems.Add("Clear");
                stripmenu.Items.Add(toolmenu);
                stripmenu.Enabled = false;
                panel1.Controls.Add(stripmenu);
            }
        }

        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropedfiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var item in dropedfiles ?? Enumerable.Empty<string>())
            {
                CreateDinamicalyMenu(item);
            }
            if (panel1.Controls.Count > 6)
            {
                this.panel1.VerticalScroll.Enabled = true;
                this.panel1.VerticalScroll.Visible = true;
            }
        }

        private int indextoremove;
        private MenuStrip enabledmenu;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MenuStrip currentmenustrip = (MenuStrip)panel1.GetChildAtPoint(panel1.PointToClient(MousePosition));
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
            indextoremove = panel1.Controls.IndexOf(stripmenu);
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
            if (panel1.Controls.Count != 0)
            {
                panel1.Controls.Remove(enabledmenu);
            }
        }
        private MemoryStream stream = new MemoryStream();
        public MemoryStream CurrentStream
        {
            get
            {
                return stream;
            }
            set
            {
                stream = value;
            }
        }

        private void ReadyBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlayList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Serialize();
        }

        private void Serialize()
        {
            IFormatter f = new BinaryFormatter();
            if (videos.ListOfVideos.Count > 0)
            {
                f.Serialize(stream, videos);
                stream.Position = 0;
            }
            isopen = false;
        }
    }
}