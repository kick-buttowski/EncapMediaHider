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
        public String URL="";
        public Double duration = 0, x = 0, changesPos = 0;
        WMP wmp;
        public MiniPlayer(WMP wmp)
        {
            InitializeComponent();
            this.wmp = wmp;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            miniVidPlayer.uiMode = "none";
        }
    }
}
