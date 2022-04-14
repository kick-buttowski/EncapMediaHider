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
        VideoPlayer videoPlayer = null;
        public static String staticCurrURL;
        public static Double staticCurrPos;
        public Boolean hoveredOver = true, hoveredOver2 = true, toggleFullScreen = true, saveResume = true, toAddMessageFilter = true,
            keyLock = false, playStatus = true, toMute = true, toRepeat = false, playable =true, pressedSpace = true, manualFrameChange = false, toAutoSkip = true;
        String directoryPath;
        public static Boolean volumeLock = false;
        List<PictureBox> playListVidPb = null;
        WindowsMediaPlayer wmpSmallPb = new WindowsMediaPlayerClass();
        FileInfo fileName = null;
        Dictionary<String, Double> timeSpan = new Dictionary<String, Double>();
        Thread countThread = null, imgStackThread = null, miniPlayerThread = null;
        Explorer exp;
        int xWidth, yWidth;
        Double currRate = 1.0, formClosingPos = 0.0, x=0.0, changesPos = 0.0, x1 = 0.0;
        int startFrom = 30, endAt = 55, noOfPixToMove = 9, stakImg = 0, topC = 2, botC = 3, layout1Size = 828;
        NewProgressBar newProgressBar = null;
        TimeSpan time = new TimeSpan(), time1 = new TimeSpan();
        String durInFor = null;
        List<double> startPoint = new List<double>();
        List<double> endPoint = new List<double>();
        //CustomToolTip tip = null;
        public List<PictureBox> disposePb = new List<PictureBox>();

        public List<PictureBox> vidPb = new List<PictureBox>();
        public Boolean typeImg = false, repeat = true;
        public PictureBox globalPb = null, tempBoxOpen = null, tempBoxClose = null;
        PicViewer picViewer = null;
        public List<PictureBox> dispPb = new List<PictureBox>();
        public Double startRepeatFrom = 0, duration = 0, currPos = 0, endRepeatTo = 0, hoverDuration = 0;
        double whereAt = 1;
        TimeSpan startTime, endTime;
        Boolean overWmpSide = false, overPlSide = false;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var sb = new SolidBrush(Color.FromArgb(140, 30,30,30));
            e.Graphics.FillRectangle(sb, this.DisplayRectangle);
        }

        /*protected override void OnHandleCreated(EventArgs e)
        {
            AppUtils.EnableAcrylic(this, Color.Transparent);
            base.OnHandleCreated(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
        }*/


        public MiniPlayer miniPlayer = null;
        DirectoryInfo mainDi = null;
        List<String> videoUrls = new List<String>();
        FileInfo vidFi = null;
        PictureBox refPb = new PictureBox();
        Color mouseClickColor = Color.FromArgb(81, 160, 213);
        List<PictureBox> videosPb = null;
        List<PictureBox> videosPbPl = new List<PictureBox>();

        public void formClosingDispoer()
        {
            this.timer3.Enabled = false;
            if (Explorer.staticExp != null || VideoPlayer.exp!=null)
            {
                foreach(PictureBox pb in videosPb)
                {
                    if(pb.Name == axWindowsMediaPlayer1.URL)
                    {
                        if (Explorer.staticExp != null)
                        {
                            Explorer.staticExp.prevPb = pb;
                            Explorer.staticExp.axWindowsMediaPlayer1.URL = axWindowsMediaPlayer1.URL;
                            Explorer.staticExp.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                            Explorer.staticExp.axWindowsMediaPlayer1.Ctlcontrols.pause();
                            break;
                        }
                        else if (VideoPlayer.exp != null)
                        {
                            VideoPlayer.exp.prevPb = pb;
                            VideoPlayer.exp.axWindowsMediaPlayer1.URL = axWindowsMediaPlayer1.URL;
                            VideoPlayer.exp.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                            VideoPlayer.exp.axWindowsMediaPlayer1.Ctlcontrols.pause();
                            break;
                        }
                    }
                }
            }
            Application.RemoveMessageFilter(this);
            try
            {
                this.Hide();
                if (Explorer.wmpOnTop != null)
                {
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = this.axWindowsMediaPlayer1.Name;
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.settings.volume = 0;
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                    Explorer.wmpOnTop.WmpOnTop_Activated((int)this.duration);

                    FileInfo fi = new FileInfo(Explorer.wmpOnTop.axWindowsMediaPlayer1.URL);
                    if (File.Exists(fi.DirectoryName + "\\resume.txt"))
                    {
                        String[] resumeFile = File.ReadAllLines(fi.DirectoryName + "\\resume.txt");
                        String fileStr = "";
                        foreach (String str in resumeFile)
                        {
                            if (str.Contains("@@" + fi.Name + "@@!"))
                            {
                                fileStr = fileStr + "@@" + fi.Name + "@@!" + axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                                continue;
                            }
                            fileStr = fileStr + str + "\n";
                        }
                        File.WriteAllText(fi.DirectoryName + "\\resume.txt", fileStr);
                    }

                    if (!Explorer.wmpOnTop.axWindowsMediaPlayer1.URL.Contains("\\Pics\\") && File.Exists(Explorer.directory3.FullName + "\\resumeDb.txt") && saveResume)
                    {
                            String[] resumeFile = File.ReadAllLines(Explorer.directory3.FullName + "\\resumeDb.txt");
                            Boolean doesExist = false;
                            String fileStr = "";
                            foreach (String str in resumeFile)
                            {
                                if (str.Contains(Explorer.wmpOnTop.axWindowsMediaPlayer1.URL + "@@!"))
                                {
                                    doesExist = true;
                                    fileStr = fileStr + Explorer.wmpOnTop.axWindowsMediaPlayer1.URL + "@@!" +
                                        Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                                    break;
                                }
                            }
                            foreach (String str in resumeFile)
                            {
                                if (!str.Contains(Explorer.wmpOnTop.axWindowsMediaPlayer1.URL + "@@!"))
                                {
                                    fileStr = fileStr + str + "\n";
                                }
                            }
                            if (!doesExist)
                            {
                                int max = resumeFile.Length >= 8 ? 7 : resumeFile.Length;
                                fileStr = Explorer.wmpOnTop.axWindowsMediaPlayer1.URL +
                                    "@@!" + Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";

                                for (int i = 0; i < max; i++)
                                {
                                    fileStr = fileStr + resumeFile[i] + "\n";
                                }
                            }
                            File.WriteAllText(Explorer.directory3.FullName + "\\resumeDb.txt", fileStr);
                    }

                    //Application.AddMessageFilter(videoPlayer);
                }

                foreach (PictureBox pb in this.disposePb)
                {
                    pb.Image.Dispose();
                    pb.Dispose();
                    pb.Tag = null;
                }
                this.disposePb.Clear();

                //this.timer1.Enabled = false;
                this.axWindowsMediaPlayer1.Ctlcontrols.pause();
                this.axWindowsMediaPlayer1.currentPlaylist.clear();
                this.axWindowsMediaPlayer1.URL = "";
                //this.axWindowsMediaPlayer1.Dispose();
                this.miniPlayer.miniVidPlayer.currentPlaylist.clear();
                this.miniPlayer.URL = "";
                //this.miniPlayer.miniVidPlayer.Dispose();
                //this.miniPlayer.Dispose();
                //this.Dispose();
                //this.Close();

                GC.Collect();
                if (TranspBack.toTop) Explorer.wmpOnTop.Show();
                else
                {
                    if (Explorer.wmpOnTop != null)
                    {
                        Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                        Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                        Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                        Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                        Explorer.wmpOnTop.Dispose();
                        Explorer.wmpOnTop = null;
                    }
                    return;
                }
            }
            catch { }
        }

        public void setRefPb(PictureBox refPb, List<PictureBox> videosPb, VideoPlayer videoPlayer)
        {
            this.refPb = refPb;
            this.videosPb = videosPb;
            this.videoPlayer = videoPlayer;
            this.playListVidPb = null;

            flowLayoutPanel2.Hide();
            toAddMessageFilter = true;
            saveResume = true;
        }

        public void setRefPb(PictureBox refPb, List<PictureBox> videosPb, VideoPlayer videoPlayer, List<PictureBox> playListVidPb)
        {
            this.refPb = refPb;
            this.videosPb = videosPb;
            this.videoPlayer = videoPlayer;
            this.playListVidPb = playListVidPb;
            toAddMessageFilter = true;
            flowLayoutPanel2.Hide();
            saveResume = true;
        }

        public void setRefPb(PictureBox refPb, List<PictureBox> videosPb, VideoPlayer videoPlayer, Boolean saveResume)
        {
            this.refPb = refPb;
            this.videosPb = videosPb;
            this.videoPlayer = videoPlayer;
            this.saveResume = saveResume;
            toAddMessageFilter = true;
            flowLayoutPanel2.Hide();
            this.playListVidPb = null;
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            overWmpSide = true;
        }
        
        private void fillUpPl()
        {
            if (playListVidPb == null)
                return;
            //calcXY(playListVidPb.Count);
            //flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width, layout1Size);
            //flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height, 15, 15));

            for (int i = 0; i < playListVidPb.Count; i++)
            {
                String baseFile = "";
                foreach (String str in File.ReadAllLines(playListVidPb[i].Name).ToList())
                {
                    if (str.Trim().Length > 0)
                    {
                        baseFile = str.Trim();
                        break;
                    }
                }
                if (baseFile.Length == 0)
                    continue;
                if (wmp != null && baseFile.Equals(videosPb.ElementAt(0).Name))
                {
                    List<int> seq = new List<int>();
                    for (int j = i - 2; j < i; j++)
                        seq.Add(j < 0 ? playListVidPb.Count + j : j);

                    for (int j = i; j < i + 3; j++)
                        seq.Add(j >= playListVidPb.Count ? j - playListVidPb.Count : j);

                    for (int j = 0; j < seq.Count; j++)
                    {
                        if (seq[j] < 0)
                        {
                            seq.RemoveAt(j);
                            seq.Insert(j, 0);
                        }

                        if (seq[j] >= playListVidPb.Count)
                        {
                            seq.RemoveAt(j);
                            seq.Insert(j, playListVidPb.Count - 1);
                        }
                    }

                    foreach (int j in seq)
                    {
                        PictureBox smallPb = new PictureBox();
                        smallPb.Image = File.Exists(playListVidPb[j < 0 ? (playListVidPb.Count + j) : j].ImageLocation) ? Image.FromFile(playListVidPb[j < 0 ? (playListVidPb.Count + j) : j].ImageLocation) : null;

                        baseFile = "";
                        foreach (String str in File.ReadAllLines(playListVidPb[j < 0 ? (playListVidPb.Count + j) : j].Name).ToList())
                        {
                            if (str.Trim().Length > 0)
                            {
                                baseFile = str.Trim();
                                break;
                            }
                        }
                        if (baseFile.Length == 0)
                            continue;

                        if (baseFile.Equals(videosPb.ElementAt(0).Name))
                        {
                            globalPb = smallPb;
                            smallPb.Size = new Size(110, (int)(110 / 1.7777));
                            smallPb.Margin = new Padding(10, 14, 0, 10);

                            smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 8, 8));
                        }
                        else
                        {
                            smallPb.Size = new Size(127, (int)(127 / 1.7777));

                            smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 10, 10));
                            smallPb.Margin = new Padding(3, 8, 0, 0);
                        }

                        smallPb.SizeMode = PictureBoxSizeMode.Zoom;

                        smallPb.Name = playListVidPb[j < 0 ? (playListVidPb.Count + j) : j].Name;

                        smallPb.MouseEnter += (s, args) =>
                        {
                            overPlSide = true;
                        };


                        smallPb.MouseLeave += (s, args) =>
                        {
                            overPlSide = false;
                        };

                        smallPb.MouseClick += (s, args) =>
                        {
                            flowLayoutPanel2.Controls.Clear();
                            if (videosPbPl.Count > 0)
                            {
                                foreach (PictureBox pb in videosPbPl)
                                {
                                    if (pb.Image != null)
                                    {
                                        pb.Image.Dispose();
                                        pb.Dispose();
                                    }
                                }
                            }
                            videosPbPl.Clear();

                            controlDisposer();
                            videosPb.Clear();
                            foreach (String file in File.ReadLines(smallPb.Name))
                            {
                                PictureBox tempPb = new PictureBox();
                                tempPb.Name = file;
                                tempPb.Image = videoPlayer.setDefaultPic(new FileInfo(file), tempPb);
                                if (tempPb.Image != null) tempPb.Image.Dispose();
                                videosPb.Add(tempPb);
                            }

                            baseFile = "";
                            foreach (String str in File.ReadAllLines(smallPb.Name).ToList())
                            {
                                if (str.Trim().Length > 0)
                                {
                                    baseFile = str.Trim();
                                    break;
                                }
                            }
                            if (baseFile.Length == 0)
                                return;

                            wmp.axWindowsMediaPlayer1.URL = baseFile;
                            wmp.axWindowsMediaPlayer1.Name = baseFile;

                            if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                            {
                                String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                                    "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");

                            }
                            startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                            endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                            Double currPosition = 0;
                            IWMPMedia mediainfo = wmpSmallPb.newMedia(smallPb.Name);
                            this.hoverDuration = mediainfo.duration;
                            currPosition = (1.0 / 8.0) * hoverDuration;

                            wmp.calculateDuration(currPosition);

                            fillUpPl();
                            fillUpFP1(videosPb);
                        };

                        videosPbPl.Add(smallPb);
                        flowLayoutPanel2.Controls.Add(smallPb);
                    }

                    //flowLayoutPanel1.Controls.Add(label1);
                }
            }
        }

        private void flowLayoutPanel2_MouseEnter(object sender, EventArgs e)
        {
            overPlSide = true;
        }

        private void flowLayoutPanel2_MouseLeave(object sender, EventArgs e)
        {
            overPlSide = false;
        }

        private void playListBtn_Click(object sender, EventArgs e)
        {
            //controlDisposer();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            overWmpSide = false;
        }

        public WMP(PictureBox refPb, PicViewer picViewer, List<PictureBox> videosPb, VideoPlayer videoPlayer)
        {
            InitializeComponent();
            
            wmp = this;
            this.videoPlayer = videoPlayer;
            this.picViewer = picViewer;
            this.videosPb = videosPb;
            miniPlayer = new MiniPlayer(this);
            panel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 12, 12));
            keyS.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, keyS.Width, keyS.Height, 12, 12));
            label1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, label1.Width, label1.Height, 9, 9));
            flowLayoutPanel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel2.Width, flowLayoutPanel2.Height, 15, 15));
            this.refPb = refPb;
            mouseClickColor = Explorer.globColor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            videoUrls = VideoPlayer.videoUrls;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            stakImg = 0;
            panel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 14, 14));
            curr.Text = "00:00:00 / ";
            curr.Font = new Font("Consolas", 8, FontStyle.Regular);
            curr.Location = new Point(panel1.Location.X + axWindowsMediaPlayer1.Width - 160, 950);
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
            String temp = " 0\t\t\t             1\t\t\t\t       2\t\t\t\t 3\t\t\t              4\t\t\t\t        5\t\t\t\t 6\t\t\t               7\t\t\t\t        8\t\t\t\t  9";
            textBox1.Text = temp;
            newProgressBar.Location = new Point(panel1.Location.X, 934);
            textBox1.Location = new Point(panel1.Location.X - 2, 944);
            newProgressBar.Value = 0;
            newProgressBar.ForeColor = Color.Black;
            newProgressBar.BackColor = Color.White;
            newProgressBar.Margin = new Padding(0);

            this.Controls.Add(newProgressBar);
            skipFlowPanel.Location = new Point(12,944);
            skipFlowPanel.BackColor = flowLayoutPanel1.BackColor;
            skipFlowPanel.Size = new Size(flowLayoutPanel1.Width, 113);
            skipFlowPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, skipFlowPanel.Width, skipFlowPanel.Height, 12, 12));
            autoSkip.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, autoSkip.Width, autoSkip.Height, 12, 12));
        }

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
                overWmpSide = true;
                /*miniPlayer.miniVidPlayer.URL = smallPb.Name;
                miniPlayer.miniVidPlayer.settings.volume = 0;
                miniPlayer.miniVidPlayer.Location = new Point(0,0);
                miniPlayer.Location = new Point(smallPb.Location.X + smallPb.Width + 10 + flowLayoutPanel1.Location.X
                                                        , flowLayoutPanel1.Location.Y + smallPb.Location.Y - ((miniPlayer.miniVidPlayer.Height- smallPb.Height)/2));

                miniPlayer.Show();
                IWMPMedia mediainfo = wmpSmallPb.newMedia(smallPb.Name);

                this.hoverDuration = mediainfo.duration;
                timer4.Enabled = true;
                timer4.Interval = 3500;
                miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = (1.0 / 8.0) * hoverDuration;
                miniPlayer.miniVidPlayer.settings.rate = 1.60;
                whereAt = 2.0;
                timer4.Start();*/
            };


            smallPb.MouseLeave += (s, args) =>
            {
                overWmpSide = false;
                /*timer4.Stop();
                timer4.Enabled = false;
                miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                miniPlayer.miniVidPlayer.settings.rate = 1.0;
                miniPlayer.Hide();
                GC.Collect();*/
            };

            smallPb.MouseClick += (s, args) =>
            {
                overWmpSide = false;

                if (File.Exists(Directory.GetParent(refPb.Name).FullName + "\\resume.txt"))
                {
                    String[] resumeFileTemp = File.ReadAllLines(Directory.GetParent(refPb.Name).FullName + "\\resume.txt");
                    String fileStr = "";
                    foreach (String str in resumeFileTemp)
                    {
                        if (str.Contains("@@" + refPb.Name.Substring(refPb.Name.LastIndexOf("\\") + 1) + "@@!"))
                        {
                            fileStr = fileStr + "@@" + refPb.Name.Substring(refPb.Name.LastIndexOf("\\") + 1) + "@@!" + axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                            continue;
                        }
                        fileStr = fileStr + str + "\n";
                    }
                    File.WriteAllText(Directory.GetParent(refPb.Name).FullName + "\\resume.txt", fileStr);
                }

                refPb = smallPb;
                wmp.axWindowsMediaPlayer1.URL = smallPb.Name;
                wmp.axWindowsMediaPlayer1.Name = smallPb.Name;

                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                {
                    String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                    textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                        "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");

                }
                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                Double currPosition = 0;
                IWMPMedia mediainfo = wmpSmallPb.newMedia(smallPb.Name);
                this.hoverDuration = mediainfo.duration;
                currPosition = (1.0 / 8.0) * hoverDuration;

                if (File.Exists(Directory.GetParent(smallPb.Name).FullName + "\\resume.txt"))
                {
                    String[] resumeFile = File.ReadAllLines(Directory.GetParent(smallPb.Name).FullName + "\\resume.txt");
                    FileInfo fi = new FileInfo(smallPb.Name);
                    foreach (String str in resumeFile)
                        if (str.Contains("@@" + fi.Name + "@@!"))
                        {
                            currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                        }
                }

                wmp.calculateDuration(currPosition);

                controlDisposer();
                fillUpFP1(vidPb);
            };

            dispPb.Add(smallPb);
            flowLayoutPanel1.Controls.Add(smallPb);
        }

        private void WMP_Activated(object sender, EventArgs e)
        {

            if (Explorer.staticExp != null)
                Explorer.staticExp.Hide();
        }

        private void autoSkip_Click(object sender, EventArgs e)
        {
            toAutoSkip = !toAutoSkip;
            autoSkip.Text = "Auto Skip " + (toAutoSkip ? "On" : "Off");
        }

        public void calcXY(int count)
        {
            if (count == 4)
            {
                topC = 1;
                botC = 3;
                layout1Size = 665;
                flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Location.X, 114 + 80);
            }
            else if (count == 3)
            {
                topC = 1;
                botC = 2;
                layout1Size = 505;
                flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Location.X, 114 + 154);
            }
            else if (count == 2)
            {
                topC = 0;
                botC = 2;
                layout1Size = 343;
                flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Location.X, 114 + 228);
            }
            else if (count == 1)
            {
                topC = 0;
                botC = 1;
                layout1Size = 181;
                flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Location.X, 114 + 302);
            }
            else { topC = 2; botC = 3; layout1Size = 828; flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Location.X, 114); }
        }

        public void fillUpFP1(List<PictureBox> vidPb, params Boolean[] typeImg)
        {
            if (vidPb == null)
                return;
            calcXY(vidPb.Count);
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width, layout1Size);
            flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height, 15, 15));
            flowLayoutPanel1.Controls.Clear();
            this.vidPb = vidPb;
            if (typeImg.Length > 0) this.typeImg = typeImg[0];
            if (vidPb != null)
            {
                for (int i = 0; i < vidPb.Count; i++)
                {
                    if (wmp != null && vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                    {
                        List<int> seq = new List<int>();
                        for (int j = i - topC; j < i; j++)
                            seq.Add(j < 0 ? vidPb.Count + j : j);

                        for (int j = i; j < i + botC; j++)
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            textBox2.Text = "Volume: " + trackBar1.Value;
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
        }

        private void WMP_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.axWindowsMediaPlayer1.fullScreen)
            {
                this.axWindowsMediaPlayer1.fullScreen = false;
            }
            if (videoPlayer!=null)
                videoPlayer.Show();
            if (Explorer.staticExp != null)
                Explorer.staticExp.Show();
            foreach (PictureBox pb in disposePb)
            {
                pb.Image.Dispose();
                pb.Dispose();
                pb.Tag = null;
            }
            disposePb.Clear();

            flowLayoutPanel2.Controls.Clear();
            if (videosPbPl.Count > 0)
            {
                foreach (PictureBox pb in videosPbPl)
                {
                    if (pb.Image != null)
                    {
                        pb.Image.Dispose();
                        pb.Dispose();
                    }
                }
            }
            videosPbPl.Clear();

            controlDisposer();
            TranspBack.toTop = false;
            formClosingDispoer();
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
            formClosingDispoer();
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

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
                /*if (!pressedSpace)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }*/

                //pressedSpace = false;
                // Test the current state of the player and display a message for each state.
                switch (e.newState)
                {
                    case 0:    // Undefined
                    textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tUndefined";
                        break;

                    case 1:    // Stopped
                        if (!repeat)
                        {
                            if (playable)
                            {
                                for (int i = 0; i < vidPb.Count; i++)
                                {
                                    if (vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                                    {
                                        if (i == vidPb.Count - 1) i = 0;
                                        else i = i + 1;

                                        wmp.axWindowsMediaPlayer1.URL = vidPb.ElementAt(i).Name;
                                        wmp.axWindowsMediaPlayer1.Name = vidPb.ElementAt(i).Name;
                                    if (Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                                    {
                                        String temp = Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                        textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                        "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
                                    }
                                    startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                    endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                                    wmp.calculateDuration(0);

                                        controlDisposer();
                                        fillUpFP1(vidPb);
                                        break;
                                    }
                                }
                            }
                            playable = !playable;
                        }
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tStopped";
                        if (!pressedSpace)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                        }
                        
                        pressedSpace = false;
                    break;

                    case 2:    // Paused
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tPaused";
                        break;

                    case 3:    // Playing
                    textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tPlaying";
                        break;

                    case 4:    // ScanForward

                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tScanForward";
                        break;

                    case 5:    // ScanReverse
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tScanReverse";
                        break;

                    case 6:    // Buffering
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tBuffering";
                        break;

                    case 7:    // Waiting
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tWaiting";
                        break;

                    case 8:    // MediaEnded
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tMediaEnded";
                        break;

                    case 9:    // Transitioning
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tTransitioning";
                        break;

                    case 10:   // Ready
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tReady";
                        break;

                    case 11:   // Reconnecting
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tReconnecting";
                        break;

                    case 12:   // Last
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\tLast";
                        break;

                    default:
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\t" + ("Unknown State: " + e.newState.ToString());
                        break;

                }
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


        private void miniProgress_MouseClick(object sender, MouseEventArgs e)
        {
            if (e != null)
            {
                /*if (e.Button == MouseButtons.Right)
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
                }*/
            }
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition;
            miniPlayer.Hide();
            miniPlayer.Show();
        }

        
        public void calculateDuration(Double duration1)
        {
            if (toAddMessageFilter)
            {
                toAddMessageFilter = false;
                Application.AddMessageFilter(this);
            }
            if (Explorer.wmpOnTop == null)
            {
                Explorer.wmpOnTop = new WmpOnTop(videoPlayer);
            }
            else
            {
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                Explorer.wmpOnTop.Dispose();
                Explorer.wmpOnTop = new WmpOnTop(videoPlayer);
            }
            WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
            IWMPMedia mediainfo = wmp.newMedia(axWindowsMediaPlayer1.Name);

            axWindowsMediaPlayer1.stretchToFit = true;
            this.duration = mediainfo.duration;
            miniPlayer.Location = new Point(50, 699);

            axWindowsMediaPlayer1.settings.volume = Explorer.globalVol;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = duration1;
            int shift = 1;
            trackBar1.Value = Explorer.globalVol;
            axWindowsMediaPlayer1.uiMode = "none";
            if (VideoPlayer.isShort)
            {
               /* timer1.Tick += ((timers, timerargs) =>
                {
                    if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition - duration > -0.05)
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0.007;
                });*/
                shift = 200;
                timer3.Interval = 50;
            }
            textBox2.Text = "Volume: " + Explorer.globalVol.ToString();
            if (axWindowsMediaPlayer1.Name.Contains("placeholdeerr"))
            {
                String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                    "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
            }
            time = TimeSpan.FromSeconds((int)duration);
            durInFor = time.ToString(@"hh\:mm\:ss");
            newProgressBar.Maximum = (int)(duration* shift);
            timer3.Enabled = true;
            timer3.Tick += (s, args) =>
            {
                try
                {
                    int currTrack = (int)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                    double currTrackDouble = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                    if (toRepeat && endRepeatTo > startRepeatFrom)
                    {
                        if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition >= endRepeatTo || axWindowsMediaPlayer1.Ctlcontrols.currentPosition<startRepeatFrom)
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = startRepeatFrom;
                        }
                    }
                    if(toAutoSkip && startPoint.Count > 0)
                    {
                        for(int k=0; k<startPoint.Count; k++)
                        {
                            if (currTrackDouble > startPoint[k] && currTrackDouble < endPoint[k])
                            {
                                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = endPoint[k];
                                break;
                            }
                        }
                    }
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
            try
            {
                label3.Text = wmpFi.Name.Substring(wmpFi.Name.IndexOf("placeholdeerr") + "placeholdeerr".Length);
            }
            catch { }
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
                tempBox.Size = new Size(161, 91);
                tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
                tempBox.Name = fi.FullName;
                //pb.ContextMenuStrip = contextMenuStrip1;
                tempBox.Margin = new Padding(0, 0, 0, 0);
                Double c = Convert.ToDouble(fi.Name.Substring(0, fi.Name.IndexOf("placeholder")));
                tempBox.Location = new Point((int)((c / (Double)(duration * 1000)) * axWindowsMediaPlayer1.Width) + 305,
                    panel1.Location.Y-tempBox.Height);
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
                    miniPlayer.Location = new Point(50, panel1.Location.Y-2);

                    miniPlayer.Show();
                    miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = ((Double)c / 1000.0) - 1.5;
                    track.Location = new Point(tempBox.Location.X - 72, tempBox.Location.Y);
                };

                tempBox.MouseMove += (s, args) => {

                    track.Visible = true;
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
            flowLayoutPanel2.Controls.Clear();
            if (videosPbPl.Count > 0)
            {
                foreach (PictureBox pb in videosPbPl)
                {
                    if (pb.Image != null)
                    {
                        pb.Image.Dispose();
                        pb.Dispose();
                    }
                }
            }
            videosPbPl.Clear();
            if (playListVidPb != null)
            {
                flowLayoutPanel2.Show();
                fillUpPl();
            }

            mainDi = Directory.GetParent(axWindowsMediaPlayer1.Name);
            if (!File.Exists(mainDi + "\\ToSkipParts.txt"))
            {
                FileStream fi = File.Create(mainDi.FullName + "\\ToSkipParts.txt");
                fi.Close();
            }
            List<string> list = File.ReadAllLines(mainDi.FullName + "\\ToSkipParts.txt").ToList();

            fileName = new FileInfo(axWindowsMediaPlayer1.Name);
            skipFlowPanel.Controls.Clear();
            foreach (String s in list)
            {
                if (s.Split('@').Length != 3)
                    continue;
                try
                {
                    if (!fileName.Name.Equals(s.Split('@')[0]))
                        continue;
                    startPoint.Add(Double.Parse(s.Split('@')[1]));
                    endPoint.Add(Double.Parse(s.Split('@')[2]));

                    Label dupeLabel2 = new Label();
                    dupeLabel2.Text = TimeSpan.FromSeconds(startPoint.ElementAt(startPoint.Count-1)).ToString(@"hh\:mm\:ss") + " to " + TimeSpan.FromSeconds(endPoint.ElementAt(endPoint.Count-1)).ToString(@"hh\:mm\:ss");
                    dupeLabel2.Font = new Font("Consolas", 7, FontStyle.Regular);
                    dupeLabel2.BackColor = miniProgress.BackColor;
                    dupeLabel2.Size = new Size(skipFlowPanel.Width - 80, 20);
                    dupeLabel2.ForeColor = Color.White;
                    dupeLabel2.TextAlign = ContentAlignment.MiddleCenter;
                    dupeLabel2.Margin = new Padding(10, 4, 0, 0);
                    dupeLabel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dupeLabel2.Width, dupeLabel2.Height, 5, 5));

                    dupeLabel2.MouseEnter += (s1, a1) =>
                    {
                        miniPlayer.miniVidPlayer.URL = axWindowsMediaPlayer1.currentMedia.sourceURL;
                        miniPlayer.miniVidPlayer.settings.volume = 0;
                        miniPlayer.miniVidPlayer.Location = new Point(0, 0);
                        miniPlayer.Location = new Point(3, skipFlowPanel.Location.Y - miniPlayer.miniVidPlayer.Size.Height);
                        miniPlayer.Show();
                        miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = Double.Parse(s.Split('@')[1]);
                    };

                    dupeLabel2.MouseLeave += (s1, args) =>
                    {
                        miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                        miniPlayer.Hide();
                        GC.Collect();
                        trackBar1.Focus();
                    };

                    skipFlowPanel.Controls.Add(dupeLabel2);

                    Button butt2 = new Button();
                    butt2.Text = "Del";
                    butt2.Font = new Font("Arial", 7, FontStyle.Regular);
                    butt2.ForeColor = Color.White;
                    butt2.BackColor = miniProgress.BackColor;
                    butt2.Size = new Size(55, 20);
                    butt2.FlatStyle = FlatStyle.Flat;
                    butt2.FlatAppearance.BorderSize = 0;
                    butt2.TextAlign = ContentAlignment.TopCenter;
                    butt2.Margin = new Padding(3, 4, 0, 0);
                    //butt2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, butt2.Width, butt2.Height, 5, 5));

                    butt2.FlatAppearance.MouseOverBackColor = miniProgress.BackColor;
                    butt2.FlatAppearance.MouseDownBackColor = miniProgress.BackColor;
                    butt2.MouseClick += (s1, a1) =>
                    {
                        List<String> lala = File.ReadAllLines(mainDi.FullName + "\\ToSkipParts.txt").ToList();
                        String toPersist = "";
                        foreach(String ss in lala)
                        {
                            if (ss.Contains("@" + Double.Parse(s.Split('@')[1]) + "@" + Double.Parse(s.Split('@')[2])))
                                continue;
                            toPersist = toPersist + ss + "\n";
                        }
                        File.WriteAllText(mainDi.FullName + "\\ToSkipParts.txt", toPersist);
                        skipFlowPanel.Controls.Remove(dupeLabel2);
                        skipFlowPanel.Controls.Remove(butt2);
                        startPoint.Remove(Double.Parse(s.Split('@')[1]));
                        endPoint.Remove(Double.Parse(s.Split('@')[2]));
                    };
                    skipFlowPanel.Controls.Add(butt2);
                }
                catch { }
            }

            if (repeat)
                axWindowsMediaPlayer1.settings.setMode("loop", true);
            else
                axWindowsMediaPlayer1.settings.setMode("loop", false);
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

        private void openBracket(FileInfo wmpFi)
        {
            startLabel.Visible = true;
            startLabel.Location = new Point((int)((startRepeatFrom / axWindowsMediaPlayer1.currentMedia.duration) * axWindowsMediaPlayer1.Width) + panel1.Location.X - (startLabel.Width / 2),
                textBox1.Location.Y);
            if (axWindowsMediaPlayer1.Name.Length > 0 && axWindowsMediaPlayer1.Name.Contains("placeholdeerr"))
            {
                String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
            "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
            }

            if (File.Exists(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg"))
                File.Delete(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg");
            if (tempBoxOpen != null)
            {
                tempBoxOpen.Image.Dispose();
                tempBoxOpen.Dispose();
            }
            long c = (long)(startRepeatFrom * 1000);

            String fileName = axWindowsMediaPlayer1.Name;
            var engine = new Engine();
            var inputFile = new MediaFile { Filename = fileName };
            var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(c) };
            var outputFile = new MediaFile
            {
                Filename = wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg"
            };
            engine.GetThumbnail(inputFile, outputFile, options);
            ResizeImage(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg", wmpFi.DirectoryName + "\\TimeFrame\\tmpOpen.jpg"
                                , 0, 292, 300, 0);
            tempBoxOpen = new PictureBox();
            Image img = Image.FromFile(wmpFi.DirectoryName + "\\TimeFrame\\tmpOpen.jpg");
            tempBoxOpen.Image = img;
            tempBoxOpen.Size = new Size(197, 110);
            tempBoxOpen.SizeMode = PictureBoxSizeMode.StretchImage;
            //pb.ContextMenuStrip = contextMenuStrip1;
            tempBoxOpen.Margin = new Padding(0, 0, 0, 0);
            tempBoxOpen.Location = new Point((int)((c / (Double)(duration * 1000)) * axWindowsMediaPlayer1.Width) + 312,
                    miniProgress.Location.Y - tempBoxOpen.Height - 9);
            tempBoxOpen.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tempBoxOpen.Width, tempBoxOpen.Height, 8, 8));
            tempBoxOpen.ContextMenuStrip = contextMenuStrip1;


            tempBoxOpen.MouseClick += (s, args) =>
            {
                GC.Collect();
                trackBar1.Focus();
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = startRepeatFrom;
            };
            this.Controls.Add(tempBoxOpen);
            disposePb.Add(tempBoxOpen);
            tempBoxOpen.BringToFront();
            axWindowsMediaPlayer1.Focus();
        }

        private void closeBracket(FileInfo wmpFi)
        {
            startLabel.Visible = true;
            endLabel.Visible = true;
            endLabel.Location = new Point((int)((endRepeatTo / axWindowsMediaPlayer1.currentMedia.duration) * axWindowsMediaPlayer1.Width) + panel1.Location.X - (endLabel.Width / 2),
                textBox1.Location.Y);
            if (axWindowsMediaPlayer1.Name.Length > 0 && axWindowsMediaPlayer1.Name.Contains("placeholdeerr"))
            {
                String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                endTime = TimeSpan.FromSeconds((int)endRepeatTo);

                textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
            "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
            }


            if (File.Exists(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg"))
                File.Delete(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg");
            if (tempBoxClose != null)
            {
                tempBoxClose.Image.Dispose();
                tempBoxClose.Dispose();
            }
            long c = (long)(endRepeatTo * 1000);

            String fileName = axWindowsMediaPlayer1.Name;
            var engine = new Engine();
            var inputFile = new MediaFile { Filename = fileName };
            var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(c) };
            var outputFile = new MediaFile
            {
                Filename = wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg"
            };
            engine.GetThumbnail(inputFile, outputFile, options);
            ResizeImage(wmpFi.DirectoryName + "\\TimeFrame\\tmp.jpg", wmpFi.DirectoryName + "\\TimeFrame\\tmpClose.jpg"
                                , 0, 292, 300, 0);
            tempBoxClose = new PictureBox();
            Image img = Image.FromFile(wmpFi.DirectoryName + "\\TimeFrame\\tmpClose.jpg");
            tempBoxClose.Image = img;
            tempBoxClose.Size = new Size(197, 110);
            tempBoxClose.SizeMode = PictureBoxSizeMode.StretchImage;
            //pb.ContextMenuStrip = contextMenuStrip1;
            tempBoxClose.Margin = new Padding(0, 0, 0, 0);
            tempBoxClose.Location = new Point((int)((c / (Double)(duration * 1000)) * axWindowsMediaPlayer1.Width) + 312,
                    miniProgress.Location.Y + 8);
            tempBoxClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tempBoxClose.Width, tempBoxClose.Height, 8, 8));
            tempBoxClose.ContextMenuStrip = contextMenuStrip1;


            tempBoxClose.MouseClick += (s, args) =>
            {
                GC.Collect();
                trackBar1.Focus();
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = endRepeatTo;
            };
            this.Controls.Add(tempBoxClose);
            disposePb.Add(tempBoxClose);
            tempBoxClose.BringToFront();
            axWindowsMediaPlayer1.Focus();
        }

        private const UInt32 WM_KEYDOWN = 0x0100;
        private const int WM_MOUSEWHEEL = 0x20a;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                if (overWmpSide)
                {
                    /*timer4.Stop();
                    timer4.Enabled = false;
                    miniPlayer.miniVidPlayer.Ctlcontrols.pause();
                    miniPlayer.miniVidPlayer.settings.rate = 1.0;
                    miniPlayer.Hide();
                    GC.Collect();*/
                    overWmpSide = false;
                    if (m.WParam.ToString() == "7864320")
                    {
                        for (int i = 0; i < vidPb.Count; i++)
                        {
                            if (vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                            {
                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFileTemp = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    String fileStr = "";
                                    foreach (String str in resumeFileTemp)
                                    {
                                        if (str.Contains("@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!"))
                                        {
                                            fileStr = fileStr + "@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!" + axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                                            continue;
                                        }
                                        fileStr = fileStr + str + "\n";
                                    }
                                    File.WriteAllText(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt", fileStr);
                                }

                                if (i == 0) i = vidPb.Count - 1;
                                else i = i - 1;
                                refPb = vidPb.ElementAt(i);
                                wmp.axWindowsMediaPlayer1.URL = vidPb.ElementAt(i).Name;
                                wmp.axWindowsMediaPlayer1.Name = vidPb.ElementAt(i).Name;
                                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                                {
                                    String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                    textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                                        "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
                                }
                                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                endTime = TimeSpan.FromSeconds((int)endRepeatTo);

                                Double currPosition = 0;
                                IWMPMedia mediainfo = wmpSmallPb.newMedia(vidPb.ElementAt(i).Name);
                                this.hoverDuration = mediainfo.duration;
                                currPosition = (1.0 / 8.0) * hoverDuration;

                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFile = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    FileInfo fi = new FileInfo(vidPb.ElementAt(i).Name);
                                    foreach (String str in resumeFile)
                                        if (str.Contains("@@" + fi.Name + "@@!"))
                                        {
                                            currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                        }
                                }

                                wmp.calculateDuration(currPosition);

                                controlDisposer();
                                fillUpFP1(vidPb);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < vidPb.Count; i++)
                        {
                            if (vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                            {
                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFileTemp = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    String fileStr = "";
                                    foreach (String str in resumeFileTemp)
                                    {
                                        if (str.Contains("@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!"))
                                        {
                                            fileStr = fileStr + "@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!" + axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                                            continue;
                                        }
                                        fileStr = fileStr + str + "\n";
                                    }
                                    File.WriteAllText(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt", fileStr);
                                }

                                if (i == vidPb.Count - 1) i = 0;
                                else i = i + 1;
                                refPb = vidPb.ElementAt(i);
                                wmp.axWindowsMediaPlayer1.URL = vidPb.ElementAt(i).Name;
                                wmp.axWindowsMediaPlayer1.Name = vidPb.ElementAt(i).Name;
                                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                                {
                                    String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                    textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                                        "               \t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
                                }

                                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                endTime = TimeSpan.FromSeconds((int)endRepeatTo);

                                Double currPosition = 0;
                                IWMPMedia mediainfo = wmpSmallPb.newMedia(vidPb.ElementAt(i).Name);
                                this.hoverDuration = mediainfo.duration;
                                currPosition = (1.0 / 8.0) * hoverDuration;

                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFile = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    FileInfo fi = new FileInfo(vidPb.ElementAt(i).Name);
                                    foreach (String str in resumeFile)
                                        if (str.Contains("@@" + fi.Name + "@@!"))
                                        {
                                            currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                        }
                                }

                                wmp.calculateDuration(currPosition);

                                controlDisposer();
                                fillUpFP1(vidPb);
                                break;
                            }
                        }
                    }
                    return true;
                }
                if (overPlSide)
                {
                    if (m.WParam.ToString() == "7864320")
                    {
                        for (int i = 0; i < videosPbPl.Count; i++)
                        {
                            String baseFile = "";
                            foreach (String str in File.ReadAllLines(videosPbPl[i].Name).ToList())
                            {
                                if (str.Trim().Length > 0)
                                {
                                    baseFile = str.Trim();
                                    break;
                                }
                            }
                            if (baseFile.Length == 0)
                                continue;
                            if (baseFile.Equals(videosPb[0].Name))
                            {
                                if (i == 0) i = videosPbPl.Count - 1;
                                else i = i - 1;
                                controlDisposer();
                                videosPb.Clear();
                                foreach (String file in File.ReadLines(videosPbPl[i].Name))
                                {
                                    PictureBox tempPb = new PictureBox();
                                    tempPb.Name = file;
                                    tempPb.Image = videoPlayer.setDefaultPic(new FileInfo(file), tempPb);
                                    if (tempPb.Image != null) tempPb.Image.Dispose();
                                    videosPb.Add(tempPb);
                                }

                                baseFile = "";
                                foreach (String str in File.ReadAllLines(videosPbPl[i].Name).ToList())
                                {
                                    if (str.Trim().Length > 0)
                                    {
                                        baseFile = str.Trim();
                                        break;
                                    }
                                }
                                if(baseFile.Length == 0)
                                return true;

                                wmp.axWindowsMediaPlayer1.URL = baseFile;
                                wmp.axWindowsMediaPlayer1.Name = baseFile;

                                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                                {
                                    String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                    textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                                        "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");

                                }
                                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                                Double currPosition = 0;
                                IWMPMedia mediainfo = wmpSmallPb.newMedia(videosPbPl[i].Name);
                                this.hoverDuration = mediainfo.duration;
                                currPosition = (1.0 / 8.0) * hoverDuration;

                                wmp.calculateDuration(currPosition);


                                flowLayoutPanel2.Controls.Clear();
                                if (videosPbPl.Count > 0)
                                {
                                    foreach (PictureBox pb in videosPbPl)
                                    {
                                        if (pb.Image != null)
                                        {
                                            pb.Image.Dispose();
                                            pb.Dispose();
                                        }
                                    }
                                }
                                videosPbPl.Clear();

                                fillUpFP1(videosPb);
                                fillUpPl();
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < videosPbPl.Count; i++)
                        {
                            String baseFile = "";
                            foreach (String str in File.ReadAllLines(videosPbPl[i].Name).ToList())
                            {
                                if (str.Trim().Length > 0)
                                {
                                    baseFile = str.Trim();
                                    break;
                                }
                            }
                            if (baseFile.Length == 0)
                                continue;
                            if (baseFile.Equals(videosPb[0].Name))
                            {
                                if (i == videosPbPl.Count - 1) i = 0;
                                else i = i + 1;
                                controlDisposer();
                                videosPb.Clear();
                                foreach (String file in File.ReadLines(videosPbPl[i].Name))
                                {
                                    PictureBox tempPb = new PictureBox();
                                    tempPb.Name = file;
                                    tempPb.Image = videoPlayer.setDefaultPic(new FileInfo(file), tempPb);
                                    if (tempPb.Image != null) tempPb.Image.Dispose();
                                    videosPb.Add(tempPb);
                                }

                                baseFile = "";
                                foreach (String str in File.ReadAllLines(videosPbPl[i].Name).ToList())
                                {
                                    if (str.Trim().Length > 0)
                                    {
                                        baseFile = str.Trim();
                                        break;
                                    }
                                }
                                if (baseFile.Length == 0)
                                    return true;

                                wmp.axWindowsMediaPlayer1.URL = baseFile;
                                wmp.axWindowsMediaPlayer1.Name = baseFile;

                                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                                {
                                    String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                    textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                                        "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");

                                }
                                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                                Double currPosition = 0;
                                IWMPMedia mediainfo = wmpSmallPb.newMedia(videosPbPl[i].Name);
                                this.hoverDuration = mediainfo.duration;
                                currPosition = (1.0 / 8.0) * hoverDuration;

                                wmp.calculateDuration(currPosition);


                                flowLayoutPanel2.Controls.Clear();
                                if (videosPbPl.Count > 0)
                                {
                                    foreach (PictureBox pb in videosPbPl)
                                    {
                                        if (pb.Image != null)
                                        {
                                            pb.Image.Dispose();
                                            pb.Dispose();
                                        }
                                    }
                                }
                                videosPbPl.Clear();

                                fillUpFP1(videosPb);
                                fillUpPl();
                                break;
                            }
                        }
                    }
                    return true;
                }
                if (volumeLock)
                    return true;
                if(m.WParam.ToString() == "7864320")
                {
                    if (axWindowsMediaPlayer1.settings.volume <= 98)
                        axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume + 2);
                    else
                        axWindowsMediaPlayer1.settings.volume = 100;

                    textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                    trackBar1.Value = axWindowsMediaPlayer1.settings.volume;

                    Explorer.globalVol = trackBar1.Value;
                }
                else
                {
                    if (axWindowsMediaPlayer1.settings.volume >= 2)
                        axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume - 2);
                    else
                        axWindowsMediaPlayer1.settings.volume = 0;

                    textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                    trackBar1.Value = axWindowsMediaPlayer1.settings.volume;

                    Explorer.globalVol = trackBar1.Value;
                }
                return true;
            }
            else if (m.Msg == WM_KEYDOWN)
            {
                FileInfo wmpFi = new FileInfo(axWindowsMediaPlayer1.Name);
                DirectoryInfo wmpDi = new DirectoryInfo(wmpFi.DirectoryName);

                Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;

                if (Control.ModifierKeys == Keys.Control && keyCode == Keys.L)
                {
                    keyLock = !keyLock;
                    textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\t" + axWindowsMediaPlayer1.playState.ToString().Replace("wmpps", "");
                }

                if (Control.ModifierKeys == Keys.Control && keyCode == Keys.V)
                {
                    volumeLock = !volumeLock;
                    textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\t" + axWindowsMediaPlayer1.playState.ToString().Replace("wmpps", "");
                }

                if (keyCode == Keys.Back || keyCode == Keys.Escape)
                {
                    if (this.axWindowsMediaPlayer1.fullScreen)
                    {
                        this.axWindowsMediaPlayer1.fullScreen = false;
                    }
                    if (keyCode == Keys.Escape)
                    {
                        if (videoPlayer != null)
                            videoPlayer.Show();

                        if (Explorer.staticExp != null)
                            Explorer.staticExp.Show();
                    }
                    foreach (PictureBox pb in disposePb)
                    {
                        pb.Image.Dispose();
                        pb.Dispose();
                        pb.Tag = null;
                    }
                    disposePb.Clear();

                    flowLayoutPanel2.Controls.Clear();
                    if (videosPbPl.Count > 0)
                    {
                        foreach (PictureBox pb in videosPbPl)
                        {
                            if (pb.Image != null)
                            {
                                pb.Image.Dispose();
                                pb.Dispose();
                            }
                        }
                    }
                    videosPbPl.Clear();

                    controlDisposer();
                    TranspBack.toTop = keyCode == Keys.Back?true:false;
                    formClosingDispoer();
                    TranspBack.toTop = false;
                }
                else 
                if (!keyLock)
                {
                    
                    if (keyCode == Keys.OemOpenBrackets)
                    {
                        startRepeatFrom = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        openBracket(wmpFi);
                    }
                    else if (keyCode == Keys.OemCloseBrackets)
                    {
                        toRepeat = true;
                        endRepeatTo = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        closeBracket(wmpFi);
                    }

                    if(keyCode == Keys.Add || keyCode == Keys.Subtract)
                    {
                        if (toRepeat)
                        {
                            if (Control.ModifierKeys == Keys.Control && keyCode == Keys.Add)
                            {
                                endRepeatTo = endRepeatTo + 0.40;
                                closeBracket(wmpFi);
                            }
                            else if (keyCode == Keys.Add)
                            {
                                startRepeatFrom = startRepeatFrom + 0.40;
                                openBracket(wmpFi);
                            }
                            else if (Control.ModifierKeys == Keys.Control && keyCode == Keys.Subtract)
                            {
                                endRepeatTo -= 0.40;
                                closeBracket(wmpFi);
                            }
                            else if (keyCode == Keys.Subtract)
                            {
                                startRepeatFrom = startRepeatFrom - 0.40;
                                openBracket(wmpFi);
                            }
                        }
                    }

                    if (keyCode == Keys.O || keyCode == Keys.G || keyCode == Keys.B || keyCode == Keys.A || keyCode == Keys.S)
                    {
                        if (endRepeatTo <= startRepeatFrom || !toRepeat)
                            return true;
                        if (keyCode == Keys.S)
                        {
                            File.AppendAllText(mainDi.FullName + "\\ToSkipParts.txt", "\n" + fileName.Name + "@" + startRepeatFrom + "@" + endRepeatTo);
                            startPoint.Add(startRepeatFrom);
                            endPoint.Add(endRepeatTo);
                            toRepeat = false;
                            Label dupeLabel2 = new Label();
                            dupeLabel2.Text = TimeSpan.FromSeconds(startRepeatFrom).ToString(@"hh\:mm\:ss") + " to " + TimeSpan.FromSeconds(endRepeatTo).ToString(@"hh\:mm\:ss");
                            dupeLabel2.Font = new Font("Consolas", 7, FontStyle.Regular);
                            dupeLabel2.BackColor = miniProgress.BackColor;
                            dupeLabel2.Size = new Size(skipFlowPanel.Width - 80, 20);
                            dupeLabel2.ForeColor = Color.White;
                            dupeLabel2.TextAlign = ContentAlignment.MiddleCenter;
                            dupeLabel2.Margin = new Padding(10, 4, 0, 0);
                            dupeLabel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dupeLabel2.Width, dupeLabel2.Height, 5, 5));
                            skipFlowPanel.Controls.Add(dupeLabel2);

                            Button butt2 = new Button();
                            butt2.Text = "Del";
                            butt2.Font = new Font("Arial", 7, FontStyle.Regular);
                            butt2.ForeColor = Color.White;
                            butt2.BackColor = miniProgress.BackColor;
                            butt2.Size = new Size(55, 20);
                            butt2.FlatStyle = FlatStyle.Flat;
                            butt2.FlatAppearance.BorderSize = 0;
                            butt2.TextAlign = ContentAlignment.TopCenter;
                            butt2.Margin = new Padding(3, 4, 0, 0);
                            //butt2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, butt2.Width, butt2.Height, 5, 5));

                            butt2.FlatAppearance.MouseOverBackColor = miniProgress.BackColor;
                            butt2.FlatAppearance.MouseDownBackColor = miniProgress.BackColor;
                            butt2.MouseClick += (s1, a1) =>
                            {
                                List<String> lala = File.ReadAllLines(mainDi.FullName + "\\ToSkipParts.txt").ToList();
                                String toPersist = "";
                                foreach (String ss in lala)
                                {
                                    if (ss.Contains("@" + startRepeatFrom + "@" + endRepeatTo))
                                        continue;
                                    toPersist = toPersist + ss + "\n";
                                }
                                File.WriteAllText(mainDi.FullName + "\\ToSkipParts.txt", toPersist);
                                skipFlowPanel.Controls.Remove(dupeLabel2);
                                skipFlowPanel.Controls.Remove(butt2);
                                startPoint.Remove(startRepeatFrom);
                                endPoint.Remove(endRepeatTo);
                            };
                            skipFlowPanel.Controls.Add(butt2);
                            return true;
                        }
                        String type = "Videos";
                        if (keyCode == Keys.G)
                        {
                            type = "Gifs";
                        }
                        else if (keyCode == Keys.B)
                        {
                            type = "Affinity";
                        }
                        else if (keyCode == Keys.A)
                        {
                            type = "Gif Videos";
                        }

                        if (tempBoxClose != null)
                        {
                            tempBoxClose.Image.Dispose();
                            tempBoxClose.Dispose();
                        }
                        if (tempBoxOpen != null)
                        {
                            tempBoxOpen.Image.Dispose();
                            tempBoxOpen.Dispose();
                        }
                        
                        
                            Thread gifThread = new Thread(() =>
                            {
                                DialogResult dialog = MessageBox.Show("Will be trimmed and saved to " + type + " folder! you sure?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                int endTo = (int)(endRepeatTo * 1000 - startRepeatFrom * 1000);
                                if (dialog == DialogResult.Yes)
                                {
                                    try
                                    {
                                        String temp = wmpFi.Name.Substring(wmpFi.Name.IndexOf("placeholdeerr") + (wmpFi.Name.Contains("placeholdeerr") ? 13 : 1));
                                        var inputFile = new MediaFile { Filename = wmpFi.FullName };
                                        String fileName = "";
                                        if (!VideoPlayer.isShort)
                                        {
                                            if (keyCode == Keys.G)
                                            {
                                                fileName = wmpFi.DirectoryName + "\\Pics\\Gifs\\";
                                            }
                                            else if (keyCode == Keys.B)
                                            {
                                                fileName = wmpFi.DirectoryName + "\\Pics\\Affinity\\";
                                            }
                                            else if (keyCode == Keys.A)
                                            {
                                                fileName = wmpFi.DirectoryName + "\\Pics\\GifVideos\\";
                                            }
                                            else
                                            {
                                                fileName = wmpFi.DirectoryName + "\\";
                                            }
                                        }
                                        else
                                        {
                                            if (keyCode == Keys.G)
                                            {
                                                fileName = wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")) + "\\Gifs\\";
                                            }
                                            else if (keyCode == Keys.B)
                                            {
                                                fileName = wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")) + "\\Affinity\\";
                                            }
                                            else
                                            {
                                                fileName = wmpFi.DirectoryName + "\\";
                                            }

                                        }
                                        fileName = fileName + temp.Substring(0, temp.LastIndexOf(".")) + endTo + (keyCode == Keys.G ? ".gif" : ".mp4");
                                        var outputFile = new MediaFile
                                        {
                                            Filename = fileName
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

                                    finally
                                    {
                                        DialogResult result = MessageBox.Show("Trimmed " + wmpFi.Name, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                }

                            });
                            gifThread.Start();
                    }

                    if (keyCode == Keys.P)
                    {
                        if(tempBoxClose!=null)
                        {
                            tempBoxClose.Image.Dispose();
                            tempBoxClose.Dispose();
                        }
                        if (tempBoxOpen != null)
                        {
                            tempBoxOpen.Image.Dispose();
                            tempBoxOpen.Dispose();
                        }
                        toRepeat = false;
                        startLabel.Visible = false;
                        endLabel.Visible = false;
                    }

                    if (keyCode == Keys.F)
                    {
                        ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        manualFrameChange = true;
                    }

                    if (keyCode == Keys.W)
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition + 0.3;
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                    }

                    if (keyCode == Keys.Q)
                    {
                        ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(-1);
                        manualFrameChange = true;
                    }

                    if(keyCode == Keys.R)
                    {
                        //timer1.Enabled = !timer1.Enabled;
                        repeat = !repeat;
                        textBox3.Text = "\tKeyLock: " + (keyLock == true ? "On" : "Off") + "\tVolLock: " + (volumeLock == true ? "On" : "Off") + "\tLoop: " + (repeat ? "On" : "Off") + "\t" + axWindowsMediaPlayer1.playState.ToString().Replace("wmpps", "");
                        if (repeat)
                            axWindowsMediaPlayer1.settings.setMode("loop", true);
                        else
                            axWindowsMediaPlayer1.settings.setMode("loop", false);
                    }

                    if (keyCode == Keys.C)
                    {
                        long c = (long)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000);
                        String fileName = axWindowsMediaPlayer1.Name;
                        var engine = new Engine();
                        var inputFile = new MediaFile { Filename = fileName };
                        var options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(c) };
                        var outputFile = new MediaFile { Filename = (
                            (!VideoPlayer.isShort ? (wmpFi.DirectoryName.Replace("\\Pics\\Affinity", "").Replace("\\Pics\\GifVideos", "") + "\\Pics\\") : 
                            (wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")+1))) + wmpDi.Name + "-" + c + ".jpg")
                            //"C:\\Users\\Harsha Vardhan\\Videos\\" + wmpDi.Name + "-" + c + ".jpg") 
                            };
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
                            globalPb.Image = Image.FromFile(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");
                        }
                        catch { }

                    }

                    if (keyCode == Keys.Enter)
                    {
                        this.axWindowsMediaPlayer1.fullScreen = !this.axWindowsMediaPlayer1.fullScreen;
                    }

                    if (keyCode == Keys.Space)
                    {
                        pressedSpace = true;
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
                        for (int i = 0; i < vidPb.Count; i++)
                        {
                            if (vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                            {
                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFileTemp = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    String fileStr = "";
                                    foreach (String str in resumeFileTemp)
                                    {
                                        if (str.Contains("@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!"))
                                        {
                                            fileStr = fileStr + "@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!" + axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                                            continue;
                                        }
                                        fileStr = fileStr + str + "\n";
                                    }
                                    File.WriteAllText(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt", fileStr);
                                }

                                if (i == 0) i = vidPb.Count - 1;
                                else i = i - 1;
                                refPb = vidPb.ElementAt(i);
                                wmp.axWindowsMediaPlayer1.URL = vidPb.ElementAt(i).Name;
                                wmp.axWindowsMediaPlayer1.Name = vidPb.ElementAt(i).Name;
                                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr")) { 
                                String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                    "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
                                }
                                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                endTime = TimeSpan.FromSeconds((int)endRepeatTo);

                                Double currPosition = 0;
                                IWMPMedia mediainfo = wmpSmallPb.newMedia(vidPb.ElementAt(i).Name);
                                this.hoverDuration = mediainfo.duration;
                                currPosition = (1.0 / 8.0) * hoverDuration;


                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFile = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    FileInfo fi = new FileInfo(vidPb.ElementAt(i).Name);
                                    foreach (String str in resumeFile)
                                        if (str.Contains("@@" + fi.Name + "@@!"))
                                        {
                                            currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                        }
                                }

                                wmp.calculateDuration(currPosition);
                                controlDisposer();
                                fillUpFP1(vidPb);
                                break;
                            }
                        }
                        /*WMPLib.IWMPMedia media = axWindowsMediaPlayer1.currentMedia;
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
                        textBox5.Text = Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "Size:");*/
                    }

                    else if ((Control.ModifierKeys == Keys.Control && keyCode == Keys.Down) || keyCode == Keys.X)
                    {
                        for (int i = 0; i < vidPb.Count; i++)
                        {
                            if (vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                            {
                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFileTemp = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    String fileStr = "";
                                    foreach (String str in resumeFileTemp)
                                    {
                                        if (str.Contains("@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!"))
                                        {
                                            fileStr = fileStr + "@@" + vidPb.ElementAt(i).Name.Substring(vidPb.ElementAt(i).Name.LastIndexOf("\\") + 1) + "@@!" + axWindowsMediaPlayer1.Ctlcontrols.currentPosition + "\n";
                                            continue;
                                        }
                                        fileStr = fileStr + str + "\n";
                                    }
                                    File.WriteAllText(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt", fileStr);
                                }

                                if (i == vidPb.Count-1) i = 0;
                                else i = i + 1;

                                refPb = vidPb.ElementAt(i);
                                wmp.axWindowsMediaPlayer1.URL = vidPb.ElementAt(i).Name;
                                wmp.axWindowsMediaPlayer1.Name = vidPb.ElementAt(i).Name;
                                if (Path.GetFileName(axWindowsMediaPlayer1.Name).Contains("placeholdeerr"))
                                {
                                    String temp = Path.GetFileName(axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:");

                                    textBox5.Text = (temp.Substring(0, temp.IndexOf("  ")) + temp.Substring(temp.IndexOf("Size"))).Replace("Size", "\t      Size") +
                        "\t[: " + startTime.ToString(@"hh\:mm\:ss") + "\t]: " + endTime.ToString(@"hh\:mm\:ss");
                                }

                                startTime = TimeSpan.FromSeconds((int)startRepeatFrom);
                                endTime = TimeSpan.FromSeconds((int)endRepeatTo);
                                Double currPosition = 0;
                                IWMPMedia mediainfo = wmpSmallPb.newMedia(vidPb.ElementAt(i).Name);
                                this.hoverDuration = mediainfo.duration;
                                currPosition = (1.0 / 8.0) * hoverDuration;

                                if (File.Exists(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt"))
                                {
                                    String[] resumeFile = File.ReadAllLines(Directory.GetParent(vidPb.ElementAt(i).Name).FullName + "\\resume.txt");
                                    FileInfo fi = new FileInfo(vidPb.ElementAt(i).Name);
                                    foreach (String str in resumeFile)
                                        if (str.Contains("@@" + fi.Name + "@@!"))
                                        {
                                            currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                        }
                                }

                                wmp.calculateDuration(currPosition);

                                controlDisposer();
                                fillUpFP1(vidPb);
                                break;
                            }
                        }

                        /*WMPLib.IWMPMedia media = axWindowsMediaPlayer1.currentMedia;
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
                        textBox5.Text = Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).Substring(0, Path.GetFileName(axWindowsMediaPlayer1.currentMedia.sourceURL).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "Size:");*/
                    }

                    else if (keyCode == Keys.Up)
                    {
                        if (volumeLock)
                            return true;
                        if (axWindowsMediaPlayer1.settings.volume <= 98)
                            axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume + 2);
                        else
                            axWindowsMediaPlayer1.settings.volume = 100;

                        textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                        trackBar1.Value = axWindowsMediaPlayer1.settings.volume;

                        Explorer.globalVol = trackBar1.Value;
                    }
                    else if (keyCode == Keys.Down)
                    {
                        if (volumeLock)
                            return true;
                        if (axWindowsMediaPlayer1.settings.volume >= 2)
                            axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume - 2);
                        else
                            axWindowsMediaPlayer1.settings.volume = 0;

                        textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                        trackBar1.Value = axWindowsMediaPlayer1.settings.volume;

                        Explorer.globalVol = trackBar1.Value;
                    }
                    if (keyCode == Keys.M)
                    {
                        if (volumeLock)
                            return true;
                        if (axWindowsMediaPlayer1.settings.volume > 0)
                            axWindowsMediaPlayer1.settings.volume = 0;
                        else
                            axWindowsMediaPlayer1.settings.volume = 15;

                        textBox2.Text = "Volume: " + axWindowsMediaPlayer1.settings.volume.ToString();
                        trackBar1.Value = axWindowsMediaPlayer1.settings.volume;

                        Explorer.globalVol = trackBar1.Value;
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
                        tempBox.Size = new Size(161, 91);
                        tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        tempBox.Name = wmpFi.DirectoryName + "\\TimeFrame\\" + c + "placeholder" + wmpFi.Name + ".jpg";
                        //pb.ContextMenuStrip = contextMenuStrip1;
                        tempBox.Margin = new Padding(0, 0, 0, 0);
                        tempBox.Location = new Point((int)((c / (Double)(duration * 1000)) * axWindowsMediaPlayer1.Width) + 305,
                                panel1.Location.Y - tempBox.Height);
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
                            miniPlayer.Location = new Point(50, panel1.Location.Y-2);
                            miniPlayer.Show();
                            miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = ((Double)c / 1000.0)-1.5;
                            track.Location = new Point(tempBox.Location.X - 72, tempBox.Location.Y);

                        };

                        tempBox.MouseMove += (s, args) => {

                            track.Visible = true;
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
