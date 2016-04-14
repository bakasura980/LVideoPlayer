using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPlayer
{
    public class CustomMenuStrip : MenuStrip
    {
        private int removingindex;

        public int RemovingIndex
        {
            get
            {
                return removingindex;
            }
            set
            {
                removingindex = value;
            }
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public Control RemovingItem
        {
            get
            {
                return items.ElementAt(RemovingIndex);
            }
        }

        private  List<Control> items;
        public CustomMenuStrip()
        {
            items = new List<Control>();
        }

        public int IndexOf(MenuStrip menustrip)
        {
            return items.IndexOf(menustrip);
        }


       
        public List<Control> Elements
        {
            get
            {
                return items;
            }
        }

        public void Add(Control element)
        {
            items.Add(element);
        }

       // private Control previtem = items.Count != 0 ? items.First() : null;
        //public Control Prev
        //{
        //    get
        //    {
        //        return previtem;
        //    }
        //}
    }
}
