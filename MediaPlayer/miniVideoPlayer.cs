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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class miniVideoPlayer : Form
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
        double duration = 0, loc=0;
        double prevX = -12;
        public PictureBox pb;
        FileInfo fileInfo; public WMP wmp;
        public TranspBack transpBack;
        VideoPlayer videoPlayer = null;
        List<PictureBox> videosPb = null;
        public static wmpSide wmpSide = null;
        double whereAt = 1.0;
        public bool isMoved = false;
        public NewProgressBar newProgressBar = null;
        public double pastPos = 0.0;

        Color mouseHoverColor = Explorer.globColor;
        Color mouseClickColor = Explorer.globColor;
        public miniVideoPlayer(List<PictureBox> videosPb)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = new Size(515, 301);
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.volume = 0;
            this.videosPb = videosPb;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));
            axWindowsMediaPlayer1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, axWindowsMediaPlayer1.Width, axWindowsMediaPlayer1.Height, 25, 25));
            newProgressBar = new NewProgressBar();
            newProgressBar.Value = 0;
            newProgressBar.ForeColor = mouseClickColor;
            newProgressBar.BackColor = mouseClickColor;
            newProgressBar.Margin = new Padding(0);
            this.Controls.Add(newProgressBar);
            axWindowsMediaPlayer1.settings.rate = 1.25;
        }

        private void axWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            if (isMoved)
            {
                timer1.Enabled = false;
                duration = axWindowsMediaPlayer1.currentMedia.duration;
                axWindowsMediaPlayer1.settings.rate = 1.00;
                newProgressBar.Maximum = (int)duration;
                loc = (e.fX / 649.0) * duration;

                if (loc - prevX > 16 || loc - prevX < -16)
                {
                    prevX = loc;
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = loc;
                    newProgressBar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                }
            }
        }

        public void miniVideoPlayer_MouseLeave(object sender, EventArgs e)
        {
            this.Hide();
            prevX = -12;
            this.Size = new Size(0, 0);
            timer1.Enabled = false;
            isMoved = false;
            whereAt = 1.0;
        }

        public void setData(PictureBox pb, FileInfo fileInfo, VideoPlayer videoPlayer)
        {
            this.pb = pb;
            this.fileInfo = fileInfo;
            this.videoPlayer = videoPlayer;
            newProgressBar.Value = 0;

        }

        public void miniVideoPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                pastPos = 0;
                timer1.Enabled = false;
                isMoved = false;
                whereAt = 1.0;
                this.pb.Image.Dispose();
                this.axWindowsMediaPlayer1.URL = "";
                this.axWindowsMediaPlayer1.Dispose();
                this.axWindowsMediaPlayer1.close();
                this.Dispose();
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (axWindowsMediaPlayer1.currentMedia == null) return;
                try { duration = axWindowsMediaPlayer1.currentMedia.duration;

                    newProgressBar.Maximum = (int)duration;
                }
                catch { duration = 0; }
                if (whereAt == 10.0) whereAt = 1.0;
                double temp = (whereAt / 10.0) * duration;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = temp;
                newProgressBar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                whereAt++;
            }
            catch { }
        }

        private void miniVideoPlayer_Activated(object sender, EventArgs e)
        {
            if (!VideoPlayer.isShort)
            {
                timer1.Enabled = true;
                timer1.Interval = 4000;
                whereAt = 1.0;
            }
        }

        private void miniVideoPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void axWindowsMediaPlayer1_KeyPressEvent(object sender, AxWMPLib._WMPOCXEvents_KeyPressEvent e)
        {
            if (e.nKeyAscii == 13)
            {
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
                wmp.Location = new Point(298, 28);
                wmp.calculateDuration(VideoPlayer.isShort?0:axWindowsMediaPlayer1.Ctlcontrols.currentPosition);

                wmpSide = new wmpSide(wmp, null, false);
                wmpSide.fillUpFP1(videosPb);
                wmpSide.Location = new Point(0, 10);

                transpBack = new TranspBack(wmp, wmpSide, null, null);
                transpBack.Show();
                wmp.Show();
                wmpSide.Show();
            }
        }

        public void axWindowsMediaPlayer1_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            if (!isMoved && !VideoPlayer.isShort)
            {
                pastPos = 0;
                timer1.Enabled = false;
                isMoved = true;
                whereAt = 1.0;
                return;
            }
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
            wmp.Location = new Point(298, 28);
            wmp.calculateDuration(VideoPlayer.isShort?0:(pastPos==0 ? axWindowsMediaPlayer1.Ctlcontrols.currentPosition : pastPos));

            wmpSide = new wmpSide(wmp,null, false);
            wmpSide.fillUpFP1(videosPb);
            wmpSide.Location = new Point(0, 10);
            wmpSide.BackColor = Explorer.darkBackColor;

            transpBack = new TranspBack(wmp, wmpSide, null, null);
            transpBack.Show();
            wmp.Show();
            wmpSide.Show();
        }
    }
}
