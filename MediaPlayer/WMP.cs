using AxWMPLib;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MediaPlayer
{

    public partial class WMP : Form, IMessageFilter
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

        WMP wmp = null;
        public Boolean hoveredOver = true, hoveredOver2 = true, toggleFullScreen = true, 
            keyLock = false, playStatus = true, toMute = true, toRepeat = false;
        String directoryPath;
        Dictionary<String, Double> timeSpan = new Dictionary<String, Double>();
        Thread countThread = null, imgStackThread = null, miniPlayerThread = null;
        Explorer exp;
        int xWidth, yWidth;
        Double currRate = 1.0, formClosingPos = 0.0, x=0.0, changesPos = 0.0, x1 = 0.0;
        int startFrom = 30, endAt = 55, noOfPixToMove = 9, stakImg = 0;
        NewProgressBar newProgressBar = null;
        TimeSpan time = new TimeSpan(), time1 = new TimeSpan();
        String durInFor = null;
        //CustomToolTip tip = null;
        public List<PictureBox> disposePb = new List<PictureBox>();

        public List<PictureBox> vidPb = new List<PictureBox>();
        public Boolean typeImg = false;
        public PictureBox globalPb = null;
        PicViewer picViewer = null;
        public List<PictureBox> dispPb = new List<PictureBox>();
        public Double startRepeatFrom = 0, duration = 0, currPos = 0;
        double whereAt = 1;

        public void controlDisposer()
        {
            if (dispPb.Count > 0)
            {
                foreach (PictureBox pb in dispPb)
                {
                    if (pb.Image != null)
                    {
                        pb.Image.Dispose();
                        pb.Dispose();
                    }
                }
            }
            dispPb.Clear();
            flowLayoutPanel1.Controls.Clear();
        }


        public void vidPicBox(List<PictureBox> vidPb, int j)
        {

            PictureBox smallPb = new PictureBox();
            smallPb.Image = File.Exists(vidPb[j < 0 ? (vidPb.Count + j) : j].ImageLocation) ? Image.FromFile(vidPb[j < 0 ? (vidPb.Count + j) : j].ImageLocation) : null;
            //smallPb.Cursor = Cursors.Hand;
            if (vidPb[j < 0 ? (vidPb.Count + j) : j].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
            {
                globalPb = smallPb;
                smallPb.Size = new Size(245, (int)(245 / 1.7777));
                smallPb.Margin = new Padding(25, 18, 0, 10);

                smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 12, 12));
            }
            else
            {
                smallPb.Size = new Size(275, (int)(275 / 1.7777));

                smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 14, 14));
                smallPb.Margin = new Padding(10,8,0,0);
            }
            
            smallPb.SizeMode = PictureBoxSizeMode.Zoom;

            smallPb.Name = vidPb[j < 0 ? (vidPb.Count + j) : j].Name;

            smallPb.MouseEnter += (s, args) =>
            {
                miniPlayer.miniVidPlayer.URL = smallPb.Name;
                miniPlayer.miniVidPlayer.settings.volume = 0;
                miniPlayer.miniVidPlayer.Location = new Point(0,0);
                miniPlayer.Location = new Point(smallPb.Location.X + smallPb.Width + 10 + flowLayoutPanel1.Location.X
                                                        , flowLayoutPanel1.Location.Y + smallPb.Location.Y - ((miniPlayer.miniVidPlayer.Height- smallPb.Height)/2));

                miniPlayer.Show();

                timer4.Enabled = true;
                timer4.Interval = 3000;
                miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = (1.0 / 10.0) * duration;
                miniPlayer.miniVidPlayer.settings.rate = 1.35;
                whereAt = 2.0;
                timer4.Start();

            };


            smallPb.MouseLeave += (s, args) =>
            {
                timer4.Stop();
                timer4.Enabled = false;
                miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                miniPlayer.miniVidPlayer.settings.rate = 1.0;
                miniPlayer.Hide();
                GC.Collect();
            };

            smallPb.MouseClick += (s, args) =>
            {
                timer4.Stop();
                timer4.Enabled = false;
                miniPlayer.miniVidPlayer.settings.rate = 1.0;
                miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                miniPlayer.Hide();
                wmp.axWindowsMediaPlayer1.URL = smallPb.Name;
                wmp.axWindowsMediaPlayer1.Name = smallPb.Name;
                wmp.textBox5.Text = Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "\tSize:");
                
                wmp.calculateDuration(miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition);

                controlDisposer();
                fillUpFP1(vidPb);
            };


            dispPb.Add(smallPb);
            flowLayoutPanel1.Controls.Add(smallPb);
        }

        public void fillUpFP1(List<PictureBox> vidPb, params Boolean[] typeImg)
        {
            flowLayoutPanel1.Controls.Clear();
            this.vidPb = vidPb;
            int prevX = 0, prevY = 0;
            if (typeImg.Length > 0) this.typeImg = typeImg[0];
            if (vidPb != null)
            {
                for (int i = 0; i < vidPb.Count; i++)
                {
                    if (wmp != null && vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                    {
                        List<int> seq = new List<int>();
                        for (int j = i - 2; j < i; j++)
                            seq.Add(j < 0 ? vidPb.Count + j : j);

                        for (int j = i; j < i + 3; j++)
                            seq.Add(j >= vidPb.Count ? j - vidPb.Count : j);

                        for (int j = 0; j < seq.Count; j++)
                        {
                            if (seq[j] < 0)
                            {
                                seq.RemoveAt(j);
                                seq.Insert(j, 0);
                            }

                            if (seq[j] >= vidPb.Count)
                            {
                                seq.RemoveAt(j);
                                seq.Insert(j, vidPb.Count - 1);
                            }
                        }
                        foreach (int j in seq)
                            vidPicBox(vidPb, j);

                        //flowLayoutPanel1.Controls.Add(label1);
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            textBox2.Text = "Volume: " + trackBar1.Value;
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
        }

        private void WMP_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (PictureBox pb in disposePb)
            {
                pb.Image.Dispose();
                pb.Dispose();
                pb.Tag = null;
            }
            disposePb.Clear();
            controlDisposer();
            TranspBack.toTop = false;
            if (this.axWindowsMediaPlayer1.fullScreen)
            {
                this.axWindowsMediaPlayer1.fullScreen = false;
            }
            this.Hide();
        }

        private void axWindowsMediaPlayer1_MouseUpEvent(object sender, _WMPOCXEvents_MouseUpEvent e)
        {

        }

        PictureBox prevPB = new PictureBox();

        FileInfo currFi = null;

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            
            pb.Image.Dispose();
            try
            {
                File.Delete(pb.Name);
            }
            catch { return; }
            pb.Dispose();
            pb.Tag = null;
            //this.Controls.Remove(pb);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (PictureBox pb in disposePb)
            {
                pb.Image.Dispose();
                pb.Dispose();
                pb.Tag = null;
            }
            disposePb.Clear();
            controlDisposer();
            TranspBack.toTop = false;
            if (this.axWindowsMediaPlayer1.fullScreen)
            {
                this.axWindowsMediaPlayer1.fullScreen = false;
            }
            this.Hide();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (PictureBox pb in disposePb)
            {
                pb.Image.Dispose();
                pb.Dispose();
                pb.Tag = null;
            }
            disposePb.Clear();
            controlDisposer();
            TranspBack.toTop = true;
            if (this.axWindowsMediaPlayer1.fullScreen)
            {
                this.axWindowsMediaPlayer1.fullScreen = false;
            }
            this.Hide();
            TranspBack.toTop = false;
        }

        private void keyS_DragEnter(object sender, EventArgs e)
        {

            label1.Visible = true;
            label1.Location = new Point(6, keyS.Location.Y + keyS.Height + 2);
        }

        private void keyS_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void WMP_FormClosed(object sender, FormClosedEventArgs e)
        {
            List<String> temp = Calculator.staleDeletes;
            if (Calculator.staleDeletes.Count > 0)
                foreach (String str in Calculator.staleDeletes.ToList())
            {
                try
                {
                    File.Delete(str);
                    temp.Remove(str);
                }
                catch { }
            }
            Calculator.staleDeletes = temp;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (miniPlayer.miniVidPlayer.currentMedia == null) return;
            if (whereAt == 10.0) whereAt = 1.0;
            double temp = (whereAt / 10.0) * duration;
            miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = temp;
            whereAt++;
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PictureBox pb in disposePb)
            {
                pb.Image.Dispose();
                try
                {
                    File.Delete(pb.Name);
                }
                catch { return; }
                pb.Dispose();
                pb.Tag = null;
            }
            disposePb.Clear();
        }

        public MiniPlayer miniPlayer = null;
        DirectoryInfo mainDi = null;
        List<String> videoUrls = new List<String>();
        FileInfo vidFi = null;
        PictureBox refPb = new PictureBox();
        Color mouseClickColor = Color.FromArgb(81, 160, 213);
        List<PictureBox> videosPb = null;

        public WMP(PictureBox refPb, PicViewer picViewer, List<PictureBox> videosPb)
        {
            InitializeComponent();

            wmp = this;
            this.picViewer = picViewer;
            this.videosPb = videosPb;
            panel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 12, 12));
            flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height, 15, 15));
            keyS.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, keyS.Width, keyS.Height, 12, 12));
            label1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, label1.Width, label1.Height, 9, 9));
            label1.BackColor = Color.FromArgb(20, 20, 20);
            this.refPb = refPb;
            mouseClickColor = Explorer.globColor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Application.AddMessageFilter(this);
            videoUrls = VideoPlayer.videoUrls;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            stakImg = 0;

            curr.Text = "00:00:00 / ";
            curr.Font = new Font("Consolas", 8, FontStyle.Regular);
            curr.Location = new Point(axWindowsMediaPlayer1.Location.X + axWindowsMediaPlayer1.Width - 160, 950);
            curr.BackColor = Color.FromArgb(50, 50, 50);
            curr.Size = new Size(160, 20);
            curr.ForeColor = Color.White;
            curr.TextAlign = ContentAlignment.TopLeft;
            curr.Padding = new Padding(0);
            curr.Margin = new Padding(0);

            track.Text = "00:00:00";
            track.Font = new Font("Consolas", 8, FontStyle.Regular);
            track.BackColor = Color.FromArgb(50, 50, 50);
            track.Size = new Size(75, 20);
            track.ForeColor = Color.White;
            track.TextAlign = ContentAlignment.MiddleCenter;
            track.Padding = new Padding(0);
            track.Margin = new Padding(0);
            track.Visible = false;

            newProgressBar = new NewProgressBar();
            newProgressBar.Size = new Size(axWindowsMediaPlayer1.Width, 15);
            newProgressBar.Margin = new Padding(0, 0, 0, 0);
            textBox1.Size = new Size(axWindowsMediaPlayer1.Width, 7);
            textBox1.BackColor = curr.BackColor;
            String temp = " 0\t\t\t             1\t\t\t\t      2\t\t\t\t 3\t\t\t              4\t\t\t\t        5\t\t\t\t  6\t\t\t                 7\t\t\t\t        8\t\t\t\t   9";
            textBox1.Text = temp;
            newProgressBar.Location = new Point(axWindowsMediaPlayer1.Location.X-2, 934);
            textBox1.Location = new Point(axWindowsMediaPlayer1.Location.X, 944);
            newProgressBar.Value = 0;
            newProgressBar.ForeColor = Color.Black;
            newProgressBar.BackColor = Color.Black;
            newProgressBar.Margin = new Padding(0);


            this.Controls.Add(newProgressBar);

        }

        private void miniProgress_MouseClick(object sender, MouseEventArgs e)
        {
            if (e != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    FileInfo fi = new FileInfo(axWindowsMediaPlayer1.URL);
                    refPb.Image.Dispose();
                    File.Delete(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");

                    Double dur = 0;
                    dur = miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition;

                    var inputFile = new MediaFile { Filename = axWindowsMediaPlayer1.currentMedia.sourceURL };
                    var outputFile = new MediaFile { Filename = fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg" };
                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);
                        var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(dur * 1000) };
                        engine.GetThumbnail(inputFile, outputFile, options);
                    }
                    refPb.Image = Image.FromFile(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");
                    return;
                }
            }
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition;
            miniPlayer.Hide();
            miniPlayer.Show();
        }

        
        public void calculateDuration(Double duration1)
        {
            if (Explorer.wmpOnTop == null)
            {
                Explorer.wmpOnTop = new WmpOnTop();
            }
            else
            {
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                Explorer.wmpOnTop.Dispose();
                Explorer.wmpOnTop = new WmpOnTop();
            }
            WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
            IWMPMedia mediainfo = wmp.newMedia(axWindowsMediaPlayer1.Name);

            axWindowsMediaPlayer1.stretchToFit = true;
            this.duration = mediainfo.duration;
            miniPlayer = new MiniPlayer(this);
            miniPlayer.Location = new Point(50, 699);

            axWindowsMediaPlayer1.settings.volume = Explorer.globalVol;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = duration1;
            int shift = 1;
            trackBar1.Value = Explorer.globalVol;
            axWindowsMediaPlayer1.uiMode = "none";
            if (VideoPlayer.isShort)
            {
                timer1.Enabled = true;
                timer1.Tick += ((timers, timerargs) =>
                {
                    if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition - duration > -0.05)
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0.007;
                });
                shift = 200;
                timer3.Interval = 50;
            }
            textBox2.Text = "Volume: " + Explorer.globalVol.ToString();
            if(axWindowsMediaPlayer1.Name.Contains("placeholdeerr"))
            textBox5.Text = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "\tSize:");
            time = TimeSpan.FromSeconds((int)duration);
            durInFor = time.ToString(@"hh\:mm\:ss");
            newProgressBar.Maximum = (int)(duration* shift);
            timer3.Enabled = true;
            timer3.Tick += (s, args) =>
            {
                try
                {
                    int currTrack = (int)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                    time = TimeSpan.FromSeconds(currTrack);
                    curr.Text = time.ToString(@"hh\:mm\:ss") + " / " + durInFor;
                    newProgressBar.Value = (int)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition* shift);
                }
                catch { }
            };

            /*newProgressBar.MouseEnter += (s, args) => {
                miniProgress_MouseEnter(null, args);
            };

            newProgressBar.MouseMove += (s, args) => {
                miniProgress_MouseMove(null, args);
            };

            newProgressBar.MouseLeave += (s, args) => {
                miniProgress_MouseLeave(null, args);
            };*/



            FileInfo wmpFi = new FileInfo(axWindowsMediaPlayer1.Name);
            label3.Text = wmpFi.Name.Substring(wmpFi.Name.IndexOf("placeholdeerr") + "placeholdeerr".Length);
            if (!Directory.Exists(wmpFi.DirectoryName + "\\TimeFrame"))
            {
                Directory.CreateDirectory(wmpFi.DirectoryName + "\\TimeFrame");
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(wmpFi.DirectoryName + "\\TimeFrame");

            foreach (PictureBox pb in disposePb)
            {
                pb.Image.Dispose();
                pb.Dispose();
                pb.Tag = null;
            }
            disposePb.Clear();

            foreach (FileInfo fi in directoryInfo.GetFiles())
            {
                if (!fi.Name.Contains("placeholder" + wmpFi.Name + ".jpg"))
                    continue;

                PictureBox tempBox = new PictureBox();
                Image img = Image.FromFile(fi.FullName);
                tempBox.Image = img;
                tempBox.Size = new Size(147, 83);
                tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
                tempBox.Name = fi.FullName;
                //pb.ContextMenuStrip = contextMenuStrip1;
                tempBox.Margin = new Padding(0, 0, 0, 0);
                Double c = Convert.ToDouble(fi.Name.Substring(0, fi.Name.IndexOf("placeholder")));
                tempBox.Location = new Point((int)((c / (Double)(duration * 1000)) * axWindowsMediaPlayer1.Width) + 305,
                    axWindowsMediaPlayer1.Location.Y-tempBox.Height-2);
                tempBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tempBox.Width, tempBox.Height, 8, 8));
                tempBox.ContextMenuStrip = contextMenuStrip1;

                tempBox.MouseClick += (s, args) =>
                {
                    //axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (Double)c / 1000.0;
                    track.Visible = false;
                    miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                    miniPlayer.Hide();
                    GC.Collect();
                    trackBar1.Focus();
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition;
                };

                tempBox.MouseEnter += (s, args) =>
                {
                    miniPlayer.miniVidPlayer.URL = axWindowsMediaPlayer1.currentMedia.sourceURL;
                    miniPlayer.miniVidPlayer.settings.volume = 0;
                    int x = tempBox.Location.X - (miniPlayer.miniVidPlayer.Width - tempBox.Width) + 85;
                    miniPlayer.miniVidPlayer.Location = new Point(x>1457?1457:x, miniPlayer.miniVidPlayer.Location.Y);
                    miniPlayer.Location = new Point(50, axWindowsMediaPlayer1.Location.Y-2);

                    miniPlayer.Show();
                    miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = ((Double)c / 1000.0) - 1.5;
                };

                tempBox.MouseMove += (s, args) => {

                    track.Visible = true;
                    track.Location = new Point(tempBox.Location.X - 72, args.Y);
                    int currTrack = (int)(miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition);
                    time = TimeSpan.FromSeconds(currTrack);
                    track.Text = time.ToString(@"hh\:mm\:ss");
                    track.BringToFront();
                };

                tempBox.MouseLeave += (s, args) =>
                {
                    track.Visible = false;
                    miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                    miniPlayer.Hide();
                    GC.Collect();
                    trackBar1.Focus();
                };
                //tempBox.Tag = img;
                //tip = new CustomToolTip(img.Width, img.Height, tempBox.Location.X + 185, tempBox.Location.Y + 112);
                //tip.SetToolTip(tempBox, "Time Frame");

                this.Controls.Add(tempBox);
                disposePb.Add(tempBox);

                //tip.InitialDelay = 10;
                //tip.AutoPopDelay = 10000;
            }
            axWindowsMediaPlayer1.Select();
            fillUpFP1(videosPb);
        }

        private void miniProgress_MouseLeave(object sender, EventArgs e)
        {
            track.Visible = false;
            miniPlayer.miniVidPlayer.Ctlcontrols.pause();
            miniPlayer.Hide();
            //miniPlayer.miniVidPlayer.Dispose();
            GC.Collect();
            trackBar1.Focus();
        }

        private void miniProgress_MouseMove(object sender, MouseEventArgs e)
        {
            int curX = e.X;
            Double maxX = miniProgress.Size.Width;

            track.Location = new Point(e.X + 328, e.Y + 952);
            int currTrack = (int)(miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition);
            time = TimeSpan.FromSeconds(currTrack);
            track.Text = time.ToString(@"hh\:mm\:ss");


            if (curX - x > noOfPixToMove || curX - x < -1 * noOfPixToMove)
            {
                x = e.X;
                miniPlayer.miniVidPlayer.Location = new Point(e.X+58>1457?1457:(e.X+63), miniPlayer.miniVidPlayer.Location.Y);
                changesPos = x.Map(0.0, maxX, 0.0, duration);
                miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = changesPos;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private Bitmap resizedImageWithReturn(Image img, params int[] numbers)
        {
            int width = img.Width, destWidth;
            int height = img.Height, destHeight;
            if (width > height)
            {
                Double ratio = (Double)width / (Double)height;
                /*destHeight = (int)(numbers.Length == 4 ? numbers[3] == 0 ? (int)((Double)numbers[2] / ratio) : numbers[3] : (int)(390 / ratio));
                destWidth = (int)(numbers.Length == 4 ? numbers[2] : 390);*/

                if (numbers.Length == 4 && numbers[3] != 0 && numbers[2] == 0)
                {
                    destWidth = (int)((double)numbers[3] * ratio);
                    destHeight = (int)((double)numbers[3]);
                }
                else if (numbers.Length == 4 && numbers[2] != 0 && numbers[3] == 0)
                {
                    destWidth = (int)((double)numbers[2]);
                    destHeight = (int)((double)numbers[2] / ratio);
                }
                else if (numbers.Length == 4 && numbers[2] != 0 && numbers[3] != 0)
                {
                    destWidth = (int)((double)numbers[2]);
                    destHeight = (int)((double)numbers[3]);
                }
                else
                {
                    destWidth = 262;
                    destHeight = (int)((double)195 / ratio);
                }
            }
            else
            {
                Double ratio = (Double)width / (Double)height;

                if (numbers.Length == 4 && numbers[1] != 0 && numbers[0] == 0)
                {
                    destWidth = (int)((double)numbers[1] * ratio);
                    destHeight = (int)((double)numbers[1]);
                }
                else if (numbers.Length == 4 && numbers[1] == 0 && numbers[0] != 0)
                {
                    destWidth = (int)((double)numbers[0]);
                    destHeight = (int)((double)numbers[0] / ratio);
                }
                else if (numbers.Length == 4 && numbers[1] != 0 && numbers[0] != 0)
                {
                    destWidth = (int)((double)numbers[0]);
                    destHeight = (int)((double)numbers[1]);
                }
                else
                {
                    destWidth = 195;
                    destHeight = (int)((double)195 / ratio);
                }
            }

            var destRect = new Rectangle(0, 0, destWidth, destHeight);
            var destImage = new Bitmap(destWidth, destHeight);

            destImage.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        private void ResizeImage(string SoucePath, string DestPath, params int[] numbers)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(SoucePath);

            int width = img.Width, destWidth;
            int height = img.Height, destHeight;
            if (width > height)
            {
                Double ratio = (Double)width / (Double)height;

                if (numbers.Length == 4 && numbers[3] != 0 && numbers[2] == 0)
                {
                    destWidth = (int)((double)numbers[3] * ratio);
                    destHeight = (int)((double)numbers[3]);
                }
                else if (numbers.Length == 4 && numbers[2] != 0 && numbers[3] == 0)
                {
                    destWidth = (int)((double)numbers[2]);
                    destHeight = (int)((double)numbers[2] / ratio);
                }
                else if (numbers.Length == 4 && numbers[2] != 0 && numbers[3] != 0)
                {
                    destWidth = (int)((double)numbers[2]);
                    destHeight = (int)((double)numbers[3]);
                }
                else
                {
                    destWidth = 262;
                    destHeight = (int)((double)195 / ratio);
                }
            }
            else
            {
                Double ratio = (Double)width / (Double)height;

                if (numbers.Length == 4 && numbers[1] != 0 && numbers[0] == 0)
                {
                    destWidth = (int)((double)numbers[1] * ratio);
                    destHeight = (int)((double)numbers[1]);
                }
                else if (numbers.Length == 4 && numbers[1] == 0 && numbers[0] != 0)
                {
                    destWidth = (int)((double)numbers[0]);
                    destHeight = (int)((double)numbers[0] / ratio);
                }
                else if (numbers.Length == 4 && numbers[1] != 0 && numbers[0] != 0)
                {
                    destWidth = (int)((double)numbers[0]);
                    destHeight = (int)((double)numbers[1]);
                }
                else
                {
                    destWidth = 195;
                    destHeight = (int)((double)195 / ratio);
                }
            }
            Bitmap bmp = new Bitmap(destWidth, destHeight);

            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.DrawImage(img, 0, 0, destWidth, destHeight);

            img.Dispose();
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            try
            {
                bmp.Save(DestPath, myImageCodecInfo, myEncoderParameters);
                //bmp.Save(DestPath);
                bmp.Dispose();
                GC.Collect();
            }
            catch { }
        }

        private void miniProgress_MouseEnter(object sender, EventArgs e)
        {
            track.Visible = true;
            miniPlayer.miniVidPlayer.URL = axWindowsMediaPlayer1.currentMedia.sourceURL;
            miniPlayer.miniVidPlayer.settings.volume = 0;

            miniPlayer.Location = new Point(50, 699);
            miniPlayer.Show();
            miniPlayer.miniVidPlayer.Ctlcontrols.play();
            //miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            noOfPixToMove = 8;
            if (duration > 60 * 60)
            {
                noOfPixToMove = 6;
            }
            else if (duration > 60 * 40)
            {
                noOfPixToMove = 7;
            }
            else if (duration > 60 * 20)
            {
                noOfPixToMove = 8;
            }
        }

        private const UInt32 WM_KEYDOWN = 0x0100;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                FileInfo wmpFi = new FileInfo(axWindowsMediaPlayer1.Name);
                DirectoryInfo wmpDi = new DirectoryInfo(wmpFi.DirectoryName);

                Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;

                if (Control.ModifierKeys == Keys.Control && keyCode == Keys.A)
                {
                    keyLock = !keyLock;
                    textBox3.Text = "KeyLock: " + (keyLock == true ? "On" : "Off");
                }

                if (keyCode == Keys.Back || keyCode == Keys.Escape)
                {
                    foreach (PictureBox pb in disposePb)
                    {
                        pb.Image.Dispose();
                        pb.Dispose();
                        pb.Tag = null;
                    }
                    disposePb.Clear();
                    controlDisposer();
                    TranspBack.toTop = true;
                    if (this.axWindowsMediaPlayer1.fullScreen)
                    {
                        this.axWindowsMediaPlayer1.fullScreen = false;
                    }
                    this.Hide();
                    TranspBack.toTop = false;
                }
                else 
                if (!keyLock)
                {
                    
                    if (keyCode == Keys.OemOpenBrackets)
                    {
                        startRepeatFrom = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        toRepeat = true;
                    }
                    else if (keyCode == Keys.OemCloseBrackets)
                    {
                        if (!toRepeat) { }
                        else
                        {
                            timer2.Enabled = true;
                            timer2.Start();
                            timer2.Interval = (int)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000 - startRepeatFrom * 1000);
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = startRepeatFrom;
                            timer2.Tick += (s, a) =>
                            {
                                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = startRepeatFrom;
                            };
                        }
                    }

                    if (keyCode == Keys.O || keyCode == Keys.G || keyCode == Keys.B)
                    {
                        int endTo = timer2.Interval;
                        Thread gifThread = new Thread(() =>
                        {
                            try
                            {
                                String temp = wmpFi.Name.Substring(wmpFi.Name.IndexOf("placeholdeerr") + 13);
                                var inputFile = new MediaFile { Filename = wmpFi.FullName };
                                var outputFile = new MediaFile
                                {
                                    Filename = (!VideoPlayer.isShort ? (wmpFi.DirectoryName + "\\" + (keyCode == Keys.G ? "Pics\\Gifs\\" : (keyCode == Keys.B ? "Pics\\Affinity\\" : ""))) :
                                    (keyCode == Keys.G ? (wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")) + "\\Gifs\\") : (keyCode == Keys.B ? (wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")) + "\\Affinity\\") : wmpFi.DirectoryName + "\\"))) +
                                      temp.Substring(0, temp.LastIndexOf(".")) + endTo + (keyCode == Keys.G ? ".gif" : ".mp4")
                            };

                                using (var engine = new Engine())
                                {
                                    engine.GetMetadata(inputFile);

                                    var options = new ConversionOptions();
                                    options.CutMedia(TimeSpan.FromMilliseconds(startRepeatFrom * 1000), TimeSpan.FromMilliseconds(endTo));

                                    engine.Convert(inputFile, outputFile, options);
                                }
                            }
                            catch { }
                        });
                        gifThread.Start();
                    }

                    if (keyCode == Keys.P)
                    {
                        timer2.Enabled = false;
                        toRepeat = false;
                    }

                    if (keyCode == Keys.F)
                    {
                        ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                    }

                    if (keyCode == Keys.W)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition + 1;
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                    }

                    if (keyCode == Keys.Q)
                    {
                        ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(-1);
                    }


                    if (keyCode == Keys.C)
                    {
                        long c = (long)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000);
                        String fileName = axWindowsMediaPlayer1.Name;
                        var engine = new Engine();
                        var inputFile = new MediaFile { Filename = fileName };
                        var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(c) };
                        var outputFile = new MediaFile { Filename = (!VideoPlayer.isShort ? (wmpFi.DirectoryName + "\\Pics\\") : 
                            (wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")+1)))/*"C:\\Users\\Harsha Vardhan\\Videos\\"*/ + wmpDi.Name + "-" + c + ".jpg" };
                        engine.GetThumbnail(inputFile, outputFile, options);
                    }

                    if (keyCode == Keys.D)
                    {
                        
                        FileInfo fi = new FileInfo(refPb.Name);
                        Image img = null;
                        if (refPb.Image != null)
                        {
                            img = refPb.Image;
                            refPb.Image = null;
                            img.Dispose();
                        }
                        if (globalPb.Image != null)
                        {
                            img = globalPb.Image;
                            globalPb.Image = null;
                            img.Dispose();
                        }
                        GC.Collect();


                        if (File.Exists(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg"))
                            try
                            {
                                File.Delete(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");
                            }
                            catch { Calculator.staleDeletes.Add(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg"); }


                        if (File.Exists(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg"))
                            try
                            {
                                File.Delete(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg");
                            }
                            catch { Calculator.staleDeletes.Add(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg"); }

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

                        try
                        {
                            refPb.Image = Image.FromFile(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");
                            refPb.ImageLocation = fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg";
                        }
                        catch { }
                        globalPb.Image = Image.FromFile(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");

                        if (File.Exists(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg"))
                            try
                            {
                                File.Delete(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg");
                            }
                            catch { Calculator.staleDeletes.Add(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg"); }
                    }

                    if (keyCode == Keys.Enter)
                    {
                        this.axWindowsMediaPlayer1.fullScreen = !this.axWindowsMediaPlayer1.fullScreen;
                    }

                    if (keyCode == Keys.Space)
                    {
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                            axWindowsMediaPlayer1.Ctlcontrols.pause();
                        else
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                    }

                    if (keyCode == Keys.H)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0.0;
                    }

                    if (keyCode == Keys.L)
                    {
                        if (currRate > 3.5) { }
                        else
                            currRate = currRate + 0.1;
                        axWindowsMediaPlayer1.settings.rate = currRate;
                        textBox4.Text = "Playback Speed: " + currRate + "x";
                    }
                    if (keyCode == Keys.J)
                    {
                        if (currRate < 0.2) { }
                        else
                            currRate = currRate - 0.1;
                        axWindowsMediaPlayer1.settings.rate = currRate;
                        textBox4.Text = "Playback Speed: " + currRate + "x";
                    }
                    if (keyCode == Keys.K)
                    {
                        axWindowsMediaPlayer1.settings.rate = 1.0;
                        currRate = 1.0;
                        textBox4.Text = "Playback Speed: " + currRate + "x";
                    }
                    if (keyCode == Keys.Right)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + (duration < 220 ? 2.0 : 5.0);
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (Control.ModifierKeys == Keys.Control && keyCode == Keys.Right)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + (duration < 220 ? 8.0 : 18.0);
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (Control.ModifierKeys == Keys.Shift && keyCode == Keys.Right)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + (duration < 220 ? 18.0 : 55.0);
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (keyCode == Keys.Left)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - (duration<220? 2.0:5.0)) < 0 ? 0 : (c - (duration < 220 ? 2.0 : 5.0));
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (Control.ModifierKeys == Keys.Control && keyCode == Keys.Left)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - (duration < 220 ? 8.0 : 20.0)) < 0 ? 0 : (c - (duration < 220 ? 8.0 : 18.0));
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (Control.ModifierKeys == Keys.Shift && keyCode == Keys.Left)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - (duration < 220 ? 20.0 : 60.0)) < 0 ? 0 : (c - (duration < 220 ? 18.0 : 55.0));
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }

                    if ((Control.ModifierKeys == Keys.Control && keyCode == Keys.Up) || keyCode==Keys.Z)
                    {
                        WMPLib.IWMPMedia media = axWindowsMediaPlayer1.currentMedia;
                        WMPLib.IWMPMedia targetMedia = null;
                        for (int i = 0; i < axWindowsMediaPlayer1.currentPlaylist.count; i++)
                        {
                            if (media.sourceURL == axWindowsMediaPlayer1.currentPlaylist.Item[i].sourceURL)
                            {
                                if (i == 0)
                                    targetMedia = axWindowsMediaPlayer1.currentPlaylist.Item[axWindowsMediaPlayer1.currentPlaylist.count - 1];
                                break;
                            }
                            targetMedia = axWindowsMediaPlayer1.currentPlaylist.Item[i];
                        }
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                        if (targetMedia != null) axWindowsMediaPlayer1.Ctlcontrols.playItem(targetMedia);

                        this.duration = axWindowsMediaPlayer1.currentMedia.duration; 
                        time = TimeSpan.FromSeconds((int)duration);
                        String durInFor = time.ToString(@"hh\:mm\:ss");
                        newProgressBar.Maximum = (int)duration;
                        textBox5.Text = Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "\tSize:");
                    }

                    else if ((Control.ModifierKeys == Keys.Control && keyCode == Keys.Down) || keyCode == Keys.X)
                    {
                        WMPLib.IWMPMedia media = axWindowsMediaPlayer1.currentMedia;
                        WMPLib.IWMPMedia targetMedia = null;
                        for (int i = 0; i < axWindowsMediaPlayer1.currentPlaylist.count; i++)
                        {
                            if (media.sourceURL == axWindowsMediaPlayer1.currentPlaylist.Item[i].sourceURL)
                            {
                                if (i + 1 == axWindowsMediaPlayer1.currentPlaylist.count)
                                {
                                    targetMedia = axWindowsMediaPlayer1.currentPlaylist.Item[0];
                                    axWindowsMediaPlayer1.Name = axWindowsMediaPlayer1.currentPlaylist.Item[0].sourceURL;
                                }
                                else
                                {
                                    targetMedia = axWindowsMediaPlayer1.currentPlaylist.Item[i + 1];
                                    axWindowsMediaPlayer1.Name = axWindowsMediaPlayer1.currentPlaylist.Item[i+1].sourceURL;
                                }
                                break;
                            }

                        }
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                        if (targetMedia != null) axWindowsMediaPlayer1.Ctlcontrols.playItem(targetMedia);
                        this.duration = axWindowsMediaPlayer1.currentMedia.duration;
                        time = TimeSpan.FromSeconds((int)duration);
                        durInFor = time.ToString(@"hh\:mm\:ss");
                        newProgressBar.Maximum = (int)duration;
                        textBox5.Text = Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "\tSize:");
                    }

                    else if (keyCode == Keys.Up)
                    {
                        if (axWindowsMediaPlayer1.settings.volume <= 98)
                            axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume + 2);
                        else
                            axWindowsMediaPlayer1.settings.volume = 100;

                        textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                        trackBar1.Value = axWindowsMediaPlayer1.settings.volume;
                    }
                    else if (keyCode == Keys.Down)
                    {
                        if (axWindowsMediaPlayer1.settings.volume >= 2)
                            axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume - 2);
                        else
                            axWindowsMediaPlayer1.settings.volume = 0;

                        textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                        trackBar1.Value = axWindowsMediaPlayer1.settings.volume;
                    }
                    if (keyCode == Keys.M)
                    {
                        if (axWindowsMediaPlayer1.settings.volume > 0)
                            axWindowsMediaPlayer1.settings.volume = 0;
                        else
                            axWindowsMediaPlayer1.settings.volume = 15;

                        textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                        trackBar1.Value = axWindowsMediaPlayer1.settings.volume;
                    }

                    if (keyCode == Keys.S)
                    {
                        axWindowsMediaPlayer1.stretchToFit = !axWindowsMediaPlayer1.stretchToFit;
                    }

                    if (keyCode == Keys.T)
                    {
                        if (File.Exists(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg"))
                            File.Delete(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg");

                        long c = (long)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000);

                        if (File.Exists(wmpFi.DirectoryName + "\\TimeFrame\\" + c + wmpFi.Name + ".jpg"))
                            File.Delete(wmpFi.DirectoryName + "\\TimeFrame\\" + c + wmpFi.Name + ".jpg");

                        String fileName = axWindowsMediaPlayer1.Name;
                        var engine = new Engine();
                        var inputFile = new MediaFile { Filename = fileName };
                        var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(c) };
                        var outputFile = new MediaFile
                        {
                            Filename = wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg"
                        };
                        engine.GetThumbnail(inputFile, outputFile, options);
                        ResizeImage(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg", wmpFi.DirectoryName + "\\TimeFrame\\" + c + "placeholder" + wmpFi.Name + ".jpg"
                                            , 0, 292, 400, 0);
                        PictureBox tempBox = new PictureBox();
                        Image img = Image.FromFile(wmpFi.DirectoryName + "\\TimeFrame\\" + c + "placeholder" + wmpFi.Name + ".jpg");
                        tempBox.Image = img;
                        tempBox.Size = new Size(147, 83);
                        tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        tempBox.Name = wmpFi.DirectoryName + "\\TimeFrame\\" + c + "placeholder" + wmpFi.Name + ".jpg";
                        //pb.ContextMenuStrip = contextMenuStrip1;
                        tempBox.Margin = new Padding(0, 0, 0, 0);
                        tempBox.Location = new Point((int)((c / (Double)(duration * 1000)) * axWindowsMediaPlayer1.Width) + 305,
                                axWindowsMediaPlayer1.Location.Y - tempBox.Height - 2);
                        tempBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tempBox.Width, tempBox.Height, 8, 8));
                        tempBox.ContextMenuStrip = contextMenuStrip1;


                        tempBox.MouseClick += (s, args) =>
                        {
                            //axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (Double)c / 1000.0;
                            track.Visible = false;
                            miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                            miniPlayer.Hide();
                            GC.Collect();
                            trackBar1.Focus();
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition;

                        };

                        tempBox.MouseEnter += (s, args) =>
                        {
                            miniPlayer.miniVidPlayer.URL = axWindowsMediaPlayer1.currentMedia.sourceURL;
                            miniPlayer.miniVidPlayer.settings.volume = 0;
                            int x = tempBox.Location.X - (miniPlayer.miniVidPlayer.Width - tempBox.Width) + 85;
                            miniPlayer.miniVidPlayer.Location = new Point(x > 1457 ? 1457 : x, miniPlayer.miniVidPlayer.Location.Y);
                            miniPlayer.Location = new Point(50, axWindowsMediaPlayer1.Location.Y-2);

                            miniPlayer.Show();
                            miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = ((Double)c / 1000.0)-1.5;
                        };

                        tempBox.MouseMove += (s, args) => {

                            track.Visible = true;
                            track.Location = new Point(tempBox.Location.X - 72, args.Y);
                            int currTrack = (int)(miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition);
                            time = TimeSpan.FromSeconds(currTrack);
                            track.Text = time.ToString(@"hh\:mm\:ss");
                            track.BringToFront();
                        };

                        tempBox.MouseLeave += (s, args) =>
                        {
                            track.Visible = false;
                            miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                            miniPlayer.Hide();
                            GC.Collect();
                            trackBar1.Focus();
                        };
                        //tempBox.Tag = img;
                        //tip = new CustomToolTip(img.Width, img.Height, tempBox.Location.X + 185, tempBox.Location.Y + 112);
                        //tip.SetToolTip(tempBox, "Time Frame");

                        //tip.InitialDelay = 10;
                        //tip.AutoPopDelay = 10000;
                        this.Controls.Add(tempBox);
                        disposePb.Add(tempBox);
                        axWindowsMediaPlayer1.Focus();
                    }

                    if (keyCode == Keys.NumPad0 || keyCode == Keys.NumPad1 || keyCode == Keys.NumPad2 || keyCode == Keys.NumPad3 || keyCode == Keys.NumPad4 ||
                        keyCode == Keys.NumPad5 || keyCode == Keys.NumPad6 || keyCode == Keys.NumPad7 || keyCode == Keys.NumPad8 || keyCode == Keys.NumPad9)
                    {
                        if(keyCode == Keys.NumPad0)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
                        }
                        else if (keyCode == Keys.NumPad1)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.1)*axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad2)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.2) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad3)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.3) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad4)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.4) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad5)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.5) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad6)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.6) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad7)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.7) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad8)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.8) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                        else if (keyCode == Keys.NumPad9)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.9) * axWindowsMediaPlayer1.currentMedia.duration;
                        }
                    }
                }

                return true;
            }
            return false;
        }
    }
}
