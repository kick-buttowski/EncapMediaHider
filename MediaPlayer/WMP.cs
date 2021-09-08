using AxWMPLib;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MediaPlayer
{

    public partial class WMP : Form, IMessageFilter
    {
        public Boolean hoveredOver = true, hoveredOver2 = true, toggleFullScreen = true, 
            keyLock = false, playStatus = true, toMute = true, toRepeat = false;
        String directoryPath;
        Dictionary<String, Double> timeSpan = new Dictionary<String, Double>();
        Thread countThread = null, imgStackThread = null, miniPlayerThread = null;
        Explorer exp;
        int xWidth, yWidth;
        Double currRate = 1.0, formClosingPos = 0.0, x=0.0, changesPos = 0.0;
        int startFrom = 30, endAt = 55, noOfPixToMove = 9, stakImg = 0;
        NewProgressBar newProgressBar = null;
        TimeSpan time = new TimeSpan(), time1 = new TimeSpan();
        String durInFor = null;

        public Double startRepeatFrom = 0, duration = 0, currPos = 0;

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

        private void axWindowsMediaPlayer1_MouseUpEvent(object sender, _WMPOCXEvents_MouseUpEvent e)
        {

        }

        PictureBox prevPB = new PictureBox();

        FileInfo currFi = null;
        public MiniPlayer miniPlayer = null;
        DirectoryInfo mainDi = null;
        List<String> videoUrls = new List<String>();
        FileInfo vidFi = null;
        PictureBox refPb = new PictureBox();
        Boolean[] isShort = null;
        Color mouseClickColor = Color.FromArgb(81, 160, 213);

        public WMP(PictureBox refPb)
        {
            InitializeComponent();
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
            curr.Location = new Point(this.Width - 162, 834);
            curr.BackColor = Color.FromArgb(50, 50, 50);
            curr.Size = new Size(160, 20);
            curr.ForeColor = Color.White;
            curr.TextAlign = ContentAlignment.TopLeft;
            curr.Padding = new Padding(0);
            curr.Margin = new Padding(0);

            newProgressBar = new NewProgressBar();
            newProgressBar.Size = new Size(this.Width-3, 15);
            newProgressBar.Location = new Point(0, 823);
            newProgressBar.Value = 0;
            newProgressBar.ForeColor = mouseClickColor;
            newProgressBar.BackColor = mouseClickColor;
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

        
        public void calculateDuration(Double duration1, params Boolean[] isShort)
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

            if (isShort.Length > 0) this.isShort = isShort;
            WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
            IWMPMedia mediainfo = wmp.newMedia(axWindowsMediaPlayer1.Name);

            axWindowsMediaPlayer1.stretchToFit = true;
            this.duration = mediainfo.duration;
            miniPlayer = new MiniPlayer(this);
            miniPlayer.Location = new Point(50, 688);
            axWindowsMediaPlayer1.settings.volume = Explorer.globalVol;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = duration1;
            int shift = 1;
            trackBar1.Value = Explorer.globalVol;
            axWindowsMediaPlayer1.uiMode = "none";
            if (isShort.Length>0)
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
        }

        private void miniProgress_MouseLeave(object sender, EventArgs e)
        {
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
            if (curX - x > noOfPixToMove || curX - x < -1 * noOfPixToMove)
            {
                x = e.X;
                miniPlayer.miniVidPlayer.Location = new Point(e.X+58, miniPlayer.miniVidPlayer.Location.Y);
                changesPos = x.Map(0.0, maxX, 0.0, duration);
                miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = changesPos;
                toolTip1.ShowAlways = true;
                time1 = TimeSpan.FromSeconds(changesPos);
                toolTip1.SetToolTip(miniProgress, time1.ToString(@"hh\:mm\:ss"));
            }
        }

        private void miniProgress_MouseEnter(object sender, EventArgs e)
        {
            miniPlayer.miniVidPlayer.URL = axWindowsMediaPlayer1.currentMedia.sourceURL;
            miniPlayer.miniVidPlayer.settings.volume = 0;

            miniPlayer.Show();
            miniPlayer.miniVidPlayer.Ctlcontrols.play();
            miniPlayer.miniVidPlayer.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
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
            toolTip1.ShowAlways = true;
            time1 = TimeSpan.FromSeconds(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
            toolTip1.SetToolTip(miniProgress, time1.ToString(@"hh\:mm\:ss"));
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
                    if (this.axWindowsMediaPlayer1.fullScreen)
                    {
                        this.axWindowsMediaPlayer1.fullScreen = false;
                    }
                    this.Hide();
                    if (miniVideoPlayer.wmpSide != null) miniVideoPlayer.wmpSide.Hide();
                    if (VideoPlayer.wmpSide1 != null) VideoPlayer.wmpSide1.Hide();
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

                    if (keyCode == Keys.O || keyCode == Keys.G)
                    {
                        int endTo = timer2.Interval;

                        Thread gifThread = new Thread(() =>
                        {
                            try
                            {
                                var inputFile = new MediaFile { Filename = wmpFi.FullName };
                                var outputFile = new MediaFile
                                {
                                    Filename = (isShort==null?(wmpFi.DirectoryName + "\\"):(wmpFi.DirectoryName.Substring(0,wmpFi.DirectoryName.LastIndexOf("\\")) + "\\Gifs\\")) +
                                    wmpFi.Name.Substring(0,wmpFi.Name.LastIndexOf(".")) + endTo + (keyCode == Keys.G ? ".gif" : ".mp4")
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
                        var outputFile = new MediaFile { Filename = (isShort == null ? (wmpFi.DirectoryName + "\\Pics\\") : (wmpFi.DirectoryName.Substring(0, wmpFi.DirectoryName.LastIndexOf("\\")+1))) + wmpDi.Name + "-" + c + ".jpg" };
                        engine.GetThumbnail(inputFile, outputFile, options);
                    }

                    if (keyCode == Keys.D)
                    {
                        if(miniVideoPlayer.wmpSide!=null)
                            miniVideoPlayer.wmpSide.controlDisposer();
                        else if (VideoPlayer.wmpSide1 != null)
                            VideoPlayer.wmpSide1.controlDisposer();
                        FileInfo fi = new FileInfo(refPb.Name);
                        if(refPb.Image != null)
                        refPb.Image.Dispose();

                        try
                        {
                            File.Delete(fi.DirectoryName + "\\ImgPB\\" + fi.Name + ".jpg");
                        }
                        catch { }

                        try
                        {
                            File.Delete(fi.DirectoryName + "\\ImgPB\\resized_" + fi.Name + ".jpg");
                        }
                        catch { }

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
                        if (miniVideoPlayer.wmpSide != null)
                            miniVideoPlayer.wmpSide.fillUpFP1(miniVideoPlayer.wmpSide.vidPb);
                        else if (VideoPlayer.wmpSide1 != null)
                            VideoPlayer.wmpSide1.fillUpFP1(VideoPlayer.wmpSide1.vidPb);
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
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + 20.0;
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (Control.ModifierKeys == Keys.Shift && keyCode == Keys.Right)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + 60.0;
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
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - 20.0) < 0 ? 0 : (c - 20.0);
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }
                    if (Control.ModifierKeys == Keys.Shift && keyCode == Keys.Left)
                    {
                        double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - 60.0) < 0 ? 0 : (c - 60.0);
                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                        }
                    }

                    if ((Control.ModifierKeys == Keys.Control && keyCode == Keys.Up) || keyCode==Keys.B)
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

                    else if ((Control.ModifierKeys == Keys.Control && keyCode == Keys.Down) || keyCode == Keys.N)
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

                    if(keyCode == Keys.NumPad0 || keyCode == Keys.NumPad1 || keyCode == Keys.NumPad2 || keyCode == Keys.NumPad3 || keyCode == Keys.NumPad4 ||
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
