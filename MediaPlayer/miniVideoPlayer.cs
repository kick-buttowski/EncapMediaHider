using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
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
    public partial class miniVideoPlayer : Form
    {
        double duration = 0, loc=0;
        double prevX = -12;
        public PictureBox pb;
        FileInfo fileInfo; public WMP wmp;
        public TranspBack transpBack;
        VideoPlayer videoPlayer = null;
        List<PictureBox> videosPb = null;
        public static wmpSide wmpSide = null;

        public miniVideoPlayer(List<PictureBox> videosPb)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = new Size(515, 301);
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.volume = 0;
            this.videosPb = videosPb;
        }

        private void axWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            duration = axWindowsMediaPlayer1.currentMedia.duration;

            loc = (e.fX / 515.0) * duration;

            if (loc - prevX > 12 || loc - prevX < -12)
            {
                prevX = loc;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = loc;
            }
        }

        public void miniVideoPlayer_MouseLeave(object sender, EventArgs e)
        {
            this.Hide();
            prevX = -12;
            this.Size = new Size(0, 0);
        }

        public void setData(PictureBox pb, FileInfo fileInfo, VideoPlayer videoPlayer)
        {
            this.pb = pb;
            this.fileInfo = fileInfo;
            this.videoPlayer = videoPlayer;
           
        }

        public void miniVideoPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                this.pb.Image.Dispose();
                this.axWindowsMediaPlayer1.URL = "";
                this.axWindowsMediaPlayer1.Dispose();
                this.axWindowsMediaPlayer1.close();
                this.Dispose();
            }
            catch { }
        }

        public void axWindowsMediaPlayer1_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            try
            {
                if (e!=null)
                {
                    if (e.nButton == 2)
                    {
                        FileInfo fi = new FileInfo(axWindowsMediaPlayer1.URL);
                        if(pb.Image!=null)
                        pb.Image.Dispose();
                        File.Delete(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");

                        Double dur = 0;
                        dur = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                        var inputFile = new MediaFile { Filename = axWindowsMediaPlayer1.currentMedia.sourceURL };
                        var outputFile = new MediaFile { Filename = fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg" };
                        using (var engine = new Engine())
                        {
                            engine.GetMetadata(inputFile);
                            var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(dur * 1000) };
                            engine.GetThumbnail(inputFile, outputFile, options);
                        }
                        pb.Image = Image.FromFile(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");
                        return;
                    }
                }
            }
            catch { }
            miniVideoPlayer_MouseLeave(null, null);

            try
            {
                StreamReader sr = new StreamReader(fileInfo.DirectoryName + "\\priority.txt");
                String line;
                String toWriteText = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(pb.Name))
                    {
                        int priority = int.Parse(line.Substring(0, line.IndexOf("@")));
                        line = (priority + 1) + "@" + pb.Name;
                    }
                    toWriteText = toWriteText + line + "\n";
                }
                sr.Close();
                File.WriteAllText(fileInfo.DirectoryName + "\\priority.txt", toWriteText);
            }
            catch { }

            wmp = new WMP(pb);
            wmp.axWindowsMediaPlayer1.URL = fileInfo.FullName;
            wmp.axWindowsMediaPlayer1.Name = fileInfo.FullName;
            wmp.Location = new Point(298, 100);
            wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);

            wmpSide = new wmpSide(wmp,null, false);
            wmpSide.fillUpFP1(videosPb);
            wmpSide.Location = new Point(0, 55);

            transpBack = new TranspBack(wmp, wmpSide, null, null);
            transpBack.Show();
            wmp.Show();
            wmpSide.Show();
        }
    }
}
