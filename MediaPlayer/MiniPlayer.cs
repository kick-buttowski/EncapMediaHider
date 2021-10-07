using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class MiniPlayer : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public String URL="";
        public Double duration = 0, x = 0, changesPos = 0;
        WMP wmp;
        public MiniPlayer(WMP wmp)
        {
            InitializeComponent();
            this.wmp = wmp;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            miniVidPlayer.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, miniVidPlayer.Width, miniVidPlayer.Height, 15, 15));
            miniVidPlayer.uiMode = "none";
        }
    }
}
