using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{

    class CustomToolTip : ToolTip
    {
        [DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr h, int x, int y, 
       int width, int height, bool redraw);

        public TextureBrush b = null;
        int width = 0, height = 0, xLoc = -1, yLoc = -1;
        public CustomToolTip(int width, int height, params int[] loc)
        {
            this.width = width;
            this.height = height;
            this.OwnerDraw = true;
            this.Popup += new PopupEventHandler(this.OnPopup);
            this.Draw += new DrawToolTipEventHandler(this.OnDraw);
            if (loc.Length == 2)
            {
                this.xLoc = loc[0];
                this.yLoc = loc[1];
            }
        }

        private void OnPopup(object sender, PopupEventArgs e) // use this event to set the size of the tool tip
        {
            e.ToolTipSize = new Size(width, height);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) // use this to customzie the tool tip
        {
            Graphics g = e.Graphics;

            // to set the tag for each button or object
            Control parent = e.AssociatedControl;
            Image pelican = parent.Tag as Image;
            //create your own custom brush to fill the background with the image
            if (parent.Tag != null)
            {
                try
                {
                    b = new TextureBrush(new Bitmap(pelican));// get the image from Tag

                    if (xLoc != -1)
                    {
                        var h = (IntPtr)this.GetType().GetProperty("Handle",
                            BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

                        MoveWindow(h, xLoc, yLoc, e.Bounds.Width - 2, e.Bounds.Height, false);

                        e.DrawBackground();
                        e.DrawBorder();
                    }

                    g.DrawImage(pelican, e.Bounds);
                    b.Dispose();
                }
                catch
                {
                    try
                    {
                        if(b!=null)
                        b.Dispose();
                    }
                    catch { }
                }
            }
        }
    }

}
