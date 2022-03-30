using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class TranspBack : Form
    {
        WMP wmp = null;
        VideoPlayer videoPlayer = null;
        int k = 0;
        List<PictureBox> vidPb = null;
        public static Boolean toTop = false;

        public TranspBack(WMP wmp, VideoPlayer videoPlayer, List<PictureBox> vidPb)
        {
            InitializeComponent();
            this.wmp = wmp;
            this.videoPlayer = videoPlayer;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.vidPb = vidPb;
        }

        public void TranspBack_MouseClick(object sender, MouseEventArgs e)
        {
                wmp.timer3.Enabled = false;
            try
            {
                wmp.Hide();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = wmp.axWindowsMediaPlayer1.Name;
                Explorer.wmpOnTop.axWindowsMediaPlayer1.settings.volume = 0;
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = wmp.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                Explorer.wmpOnTop.WmpOnTop_Activated((int)wmp.duration);

                FileInfo fi = new FileInfo(Explorer.wmpOnTop.axWindowsMediaPlayer1.URL);
                if (File.Exists(fi.DirectoryName + "\\resume.txt"))
                {
                    String[] resumeFile = File.ReadAllLines(fi.DirectoryName + "\\resume.txt");
                    String fileStr = "";
                    foreach (String str in resumeFile)
                    {
                        if (str.Contains("@@" + fi.Name + "@@!"))
                        {
                            fileStr = fileStr + "@@" + fi.Name + "@@!" + Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                            continue;
                        }
                        fileStr = fileStr + str + "\n";
                    }
                    File.WriteAllText(fi.DirectoryName + "\\resume.txt", fileStr);
                }
                        this.Hide();
                    Application.RemoveMessageFilter(wmp);
                    Application.AddMessageFilter(videoPlayer);


                foreach (PictureBox pb in wmp.disposePb)
                {
                    pb.Image.Dispose();
                    pb.Dispose();
                    pb.Tag = null;
                }
                wmp.disposePb.Clear();

                    wmp.axWindowsMediaPlayer1.Ctlcontrols.pause();
                    wmp.axWindowsMediaPlayer1.currentPlaylist.clear();
                    wmp.axWindowsMediaPlayer1.URL = "";
                    wmp.axWindowsMediaPlayer1.Dispose();
                    wmp.miniPlayer.miniVidPlayer.currentPlaylist.clear();
                    wmp.miniPlayer.URL = "";
                    wmp.miniPlayer.miniVidPlayer.Dispose();
                    wmp.miniPlayer.Dispose();
                    wmp.Dispose();
                    wmp.Close();

                    this.Dispose();
                    GC.Collect();
                    this.Close();
                if (toTop) Explorer.wmpOnTop.Show();
                else
                {
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                    Explorer.wmpOnTop.Dispose();
                    Explorer.wmpOnTop = null;
                    return;
                }
                }
                catch { }
        }

        private void TranspBack_FormClosing(object sender, FormClosingEventArgs e)
        {
            TranspBack_MouseClick(sender, null);
        }

        private void TranspBack_Activated(object sender, EventArgs e)
        {
            if (k == 1)
            {
                TranspBack_MouseClick(null,null);
            }
            k++;
        }
    }
}
