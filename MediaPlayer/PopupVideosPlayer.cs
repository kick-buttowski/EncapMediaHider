using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class PopupVideosPlayer : Form
    {
        Boolean toPlay = false;
        public PopupVideosPlayer()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.uiMode = "none";
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        public void setMediaPlaye(String URL, Double currPos, int volume)
        {
            axWindowsMediaPlayer1.URL = URL;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = currPos;
            axWindowsMediaPlayer1.settings.volume = 0;
        }

        private void axWindowsMediaPlayer1_MouseUpEvent(object sender, AxWMPLib._WMPOCXEvents_MouseUpEvent e)
        {
            if (toPlay)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.uiMode = "none";
                toPlay = !toPlay;
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                axWindowsMediaPlayer1.uiMode = "full";
                toPlay = !toPlay;
            }

        }
    }
}
