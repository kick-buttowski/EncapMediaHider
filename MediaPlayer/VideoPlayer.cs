﻿using AxWMPLib;
using MediaPlayer;
using MediaPlayer.Properties;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WMPLib;

namespace MediaPlayer
{

    public partial class VideoPlayer : Form, IMessageFilter
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

        public Boolean enter = false, ctrl = false, scrollZero = true, hoveredOver = true, hoveredOver2 = true, toggleFullScreen = true, playStatus = true, toMute = true, toRepeat = false, loadAll = false;
        String directoryPath;
        public static Boolean toRefresh = false, toChangeTheme = false; 
        bool isChecked = false, isGlobalChecked = false;

        public static String imagePBLink = "";
        List<Button> typeButtons = new List<Button>();
        Dictionary<String, Double> timeSpan = new Dictionary<String, Double>();
        List<Image> videosImg = new List<Image>();
        public static List<String> videoUrls = new List<String>();
        public static wmpSide wmpSide1 = null;
        Explorer exp;
        Label globalDetails = new Label();
        Double zoom = 1.0;
        public static PictureBox prevPB = null;
        PictureBox globalPb = new PictureBox(); PictureBox pbb = new PictureBox();
        List<Label> allVidDet = new List<Label>();
        Boolean mutltiSelect = false, sortBySize = false, sortByDate = true, bySort = false;
        public static Boolean isCropped = false;
        DirectoryInfo mainDi = null;
        List<PictureBox> lpb = new List<PictureBox>(), lpbDupe = new List<PictureBox>(), wpbDupe = new List<PictureBox>();
        List<PictureBox> wpb = new List<PictureBox>();
        List<PictureBox> videosPb = new List<PictureBox>();
        List<PictureBox> shortVideosPb = new List<PictureBox>();
        List<PictureBox> gifsPb = new List<PictureBox>();
        List<PictureBox> gpb = new List<PictureBox>();
        List<Image> disposePb = new List<Image>();
        List<FileInfo> imagesFi = new List<FileInfo>();
        List<String> gpbList = new List<String>();
        DirectoryInfo _4kdir = null;
        Button globalBtn = null;
        int ind = 1, whereAt = 0, noOfStackImg = 25, vertScrollValue = 0, noOfTimerRuns = 0;
        String type = "Videos";
        List<PictureBox> unListed = new List<PictureBox>();
        WMP wmp = null;
        TranspBack transpBack = null;
        Double durationDupe = 0, block = 0;
        List<PictureBox> deletePb = new List<PictureBox>();
        miniVideoPlayer miniVideoPlayer = null;
        DirectoryInfo prevfi = null, nextFi = null;
        Engine engine = new Engine();

        NewProgressBar newProgressBar = null;
        checkBox checkBox = new checkBox();

        static Random rr = new Random();
        //static int rand1 = Explorer.rr.Next(0, 256);
        //static int[] color = Explorer.Rand_Color(rand1, 0.5, 0.25);
        static Color globColor = Color.FromArgb(rr.Next(256), rr.Next(256), rr.Next(256));
        Color darkBackColor = Color.FromArgb(0, 0, 0);
        Color lightBackColor = Color.FromArgb(31, 31, 31);
        Color mouseHoverColor = Explorer.globColor;
        Color mouseClickColor = Explorer.globColor;
        Color selectedPbColor = Explorer.globColor;
        List<String> folders = null;

        CustomToolTip tip = null;

        public VideoPlayer(String directoryPath, Explorer exp, String type, DirectoryInfo prevFi, DirectoryInfo nextFi, List<String> folders)
        {
            mouseHoverColor = Explorer.globColor;
            mouseClickColor = Explorer.globColor;
            selectedPbColor = Explorer.globColor;
            InitializeComponent();
            if(Explorer.wmpOnTop == null)
            {
                Explorer.wmpOnTop = new WmpOnTop();
            }
            this.type = type;
            this.exp = exp;
            this.directoryPath = directoryPath;
            this.DoubleBuffered = true;
            Application.AddMessageFilter(this);
            checkBox.Size = new Size(50, 50);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            picturesBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picturesBtn.Width, picturesBtn.Height, 20, 20));
            videosBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, videosBtn.Width, videosBtn.Height, 20, 20));
            expBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, expBtn.Width, expBtn.Height, 20, 20));
            shortVideosBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picturesBtn.Width, picturesBtn.Height, 20, 20));
            fourKBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, videosBtn.Width, videosBtn.Height, 20, 20));
            gifsBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, videosBtn.Width, videosBtn.Height, 20, 20));
            button3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 20, 20));
            button4.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button4.Width, button4.Height, 20, 20));
            button5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button5.Width, button5.Height, 20, 20));
            textBox1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox1.Width, textBox1.Height, 20, 20));
            trackBar1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, trackBar1.Width, trackBar1.Height, 20, 20));
            button9.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button9.Width, button9.Height, 20, 20));
            button10.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button10.Width, button10.Height, 20, 20));
            mainDi = new DirectoryInfo(directoryPath);
            button5.Text = mainDi.Name;
            this.folders = folders;
            this.nextFi = nextFi;
            this.prevfi = prevFi;
            button3.Text = "Prev: " + prevFi.Name;
            button4.Text = "Next: " + nextFi.Name;
            if (!Directory.Exists(mainDi + "\\imgPB")) { Directory.CreateDirectory(mainDi + "\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics")) { Directory.CreateDirectory(mainDi + "\\Pics"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "Gifs")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "Gifs"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "Gifs\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "Gifs\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "kkkk")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "kkkk"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "kkkk\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "kkkk\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "GifVideos")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "GifVideos"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "GifVideos\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "GifVideos\\imgPB"); }
            if (!File.Exists(mainDi + "\\links.txt")) {
                FileStream fi = File.Create(mainDi.FullName + "\\links.txt");
                fi.Close();
            }
            if (!File.Exists(mainDi + "\\webLinks.txt"))
            {
                FileStream fi = File.Create(mainDi.FullName + "\\webLinks.txt");
                fi.Close();
            }
            trackBar1.Value = Explorer.globalVol;
            typeButtons.Add(videosBtn);
            typeButtons.Add(picturesBtn);
            typeButtons.Add(fourKBtn);
            typeButtons.Add(shortVideosBtn);
            typeButtons.Add(gifsBtn);
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());
                    button3.Tag = img1;
                    if (img1.Width > img1.Height)
                        tip = new CustomToolTip(505, 327);
                    else
                        tip = new CustomToolTip(300, 450);
                    disposePb.Add(img1);
                    tip.SetToolTip(button3, prevfi.Name);
                    myPictureBox1.Image = Image.FromFile(prevfi.FullName + "\\imgPB\\smallRes.png");
                }
                catch
                {

                }
            }
            if (File.ReadAllText(nextFi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img2 = Image.FromFile(File.ReadAllText(nextFi.FullName + "\\disPic.txt").Trim());
                    if (tip == null)

                        if (img2.Width > img2.Height)
                            tip = new CustomToolTip(505, 327);
                        else
                            tip = new CustomToolTip(300, 450);
                    button4.Tag = img2;
                    disposePb.Add(img2);
                    tip.SetToolTip(button4, nextFi.Name);
                    myPictureBox2.Image = Image.FromFile(nextFi.FullName + "\\imgPB\\smallRes.png");
                }
                catch { }
            }
            if (File.ReadAllText(mainDi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img3 = Image.FromFile(File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim()); if (tip == null)

                        if (img3.Width > img3.Height)
                            tip = new CustomToolTip(505, 327);
                        else
                            tip = new CustomToolTip(300, 450);
                    button5.Tag = img3;
                    disposePb.Add(img3);
                    tip.SetToolTip(button5, mainDi.Name);
                }
                catch { }
            }
            //flowLayoutPanel1.BackColor = mainBackColor;
            //flowLayoutPanel2.BackColor = sideBackColor;
            newProgressBar = new NewProgressBar();
            newProgressBar.Size = new Size(1643, 25);
            newProgressBar.Location = new Point(276, -3);
            newProgressBar.Value = 0;
            newProgressBar.Margin = new Padding(0);
            setTheme();

        }

        private void setTheme()
        {

            this.BackColor = Color.FromArgb(59, 59, 59);
            button6.BackColor = lightBackColor;
            button7.BackColor = lightBackColor;
            button8.BackColor = lightBackColor;
            button2.BackColor = lightBackColor;
            button9.BackColor = lightBackColor;
            button10.BackColor = lightBackColor;
            flowLayoutPanel3.BackColor = darkBackColor;
            flowLayoutPanel1.BackColor = darkBackColor;
            flowLayoutPanel2.BackColor = darkBackColor;
            button1.BackColor = lightBackColor;
            button1.ForeColor = Color.White;
            button2.BackColor = lightBackColor;
            button2.ForeColor = Color.White;
            button9.BackColor = lightBackColor;
            button9.ForeColor = Color.White;
            button10.BackColor = lightBackColor;
            button10.ForeColor = Color.White;
            expBtn.BackColor = lightBackColor;
            expBtn.ForeColor = Color.White;
            expBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            expBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            videosBtn.BackColor = lightBackColor;
            videosBtn.ForeColor = Color.White;
            videosBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            videosBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            picturesBtn.BackColor = lightBackColor;
            picturesBtn.ForeColor = Color.White;
            picturesBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            picturesBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            fourKBtn.BackColor = lightBackColor;
            fourKBtn.ForeColor = Color.White;
            fourKBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            fourKBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            shortVideosBtn.BackColor = lightBackColor;
            shortVideosBtn.ForeColor = Color.White;
            shortVideosBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            shortVideosBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            gifsBtn.BackColor = lightBackColor;
            gifsBtn.ForeColor = Color.White;
            gifsBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            gifsBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button3.BackColor = lightBackColor;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            button3.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button4.BackColor = lightBackColor;
            button4.ForeColor = Color.White;
            button4.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            button4.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button5.BackColor = lightBackColor;
            button5.ForeColor = Color.White;
            button5.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            button5.FlatAppearance.MouseDownBackColor = mouseClickColor;
            textBox1.BackColor = lightBackColor;
            textBox1.ForeColor = Color.White;
            trackBar1.BackColor = lightBackColor;
            button2.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button2.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button9.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button9.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button10.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button10.FlatAppearance.MouseDownBackColor = mouseClickColor;

            newProgressBar.ForeColor = mouseClickColor;
            newProgressBar.BackColor = mouseClickColor;


            if (globalBtn != null)
                globalBtn.BackColor = mouseClickColor;

            if (prevPB != null)
            {
                prevPB.BackColor = mouseClickColor;
                globalDetails.ForeColor = mouseClickColor;
            }
        }


        private List<String> manualSort(List<String> priorityList)
        {
            List<String> tempList = new List<String>();
            {
                for (int i = 0; i < priorityList.Count; i++)
                {
                    for (int j = 0; j < priorityList.Count - i - 1; j++)
                    {
                        int priority = int.Parse(priorityList.ElementAt(j).Substring(0, priorityList.ElementAt(j).IndexOf("@")));
                        int priority1 = int.Parse(priorityList.ElementAt(j + 1).Substring(0, priorityList.ElementAt(j + 1).IndexOf("@")));
                        if (priority1 > priority)
                        {
                            String temp = priorityList.ElementAt(j + 1);
                            String temp1 = priorityList.ElementAt(j);
                            priorityList.RemoveAt(j + 1);
                            priorityList.Insert(j + 1, temp1);
                            priorityList.RemoveAt(j);
                            priorityList.Insert(j, temp);
                        }

                    }
                }
            }
            return priorityList;
        }

        public void controlDisposer()
        {
            newProgressBar.Value = 0;
            newProgressBar.Step = 1;
            flowLayoutPanel1.Controls.Clear();
            mutltiSelect = false;
            noOfTimerRuns = 0;
            allVidDet.Clear();
            prevPB = null;

            if (!bySort)
            {
                if(type.Contains("Short") || type.Contains("Gifs"))
                {
                    sortBySize = true;
                    sortByDate = false;
                }
                else
                {
                    sortByDate = true;
                    sortBySize = false;
                }
            }
            bySort = false;

            if (miniVideoPlayer != null)
            {
                try
                {
                    if (miniVideoPlayer.pb!=null && miniVideoPlayer.pb.Image != null)
                        miniVideoPlayer.pb.Image.Dispose();
                }
                catch { }
                miniVideoPlayer.axWindowsMediaPlayer1.Dispose();

                if (miniVideoPlayer.wmp != null)
                {
                    miniVideoPlayer.wmp.Hide();
                    Application.RemoveMessageFilter(wmp);
                    miniVideoPlayer.wmp.timer1.Enabled = false;
                    try
                    {
                        miniVideoPlayer.wmp.axWindowsMediaPlayer1.Ctlcontrols.pause();
                        miniVideoPlayer.wmp.axWindowsMediaPlayer1.currentPlaylist.clear();
                        miniVideoPlayer.wmp.axWindowsMediaPlayer1.URL = "";
                        miniVideoPlayer.wmp.miniPlayer.miniVidPlayer.currentPlaylist.clear();
                        miniVideoPlayer.wmp.miniPlayer.URL = "";
                    }
                    catch { }
                    miniVideoPlayer.wmp.miniPlayer.miniVidPlayer.Dispose();
                    miniVideoPlayer.wmp.miniPlayer.Dispose();
                    miniVideoPlayer.wmp.axWindowsMediaPlayer1.Dispose();

                    miniVideoPlayer.wmp.Dispose();
                    miniVideoPlayer.wmp.Close();
                }

                if (miniVideoPlayer.transpBack != null)
                {
                    miniVideoPlayer.transpBack.Hide();
                    miniVideoPlayer.transpBack.Dispose();
                    GC.Collect();
                    miniVideoPlayer.transpBack.Close();
                }
                miniVideoPlayer.Dispose();
                miniVideoPlayer.Close();
                GC.Collect();
            }
            button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
            if (deletePb.Count > 0)
            {
                foreach (PictureBox pb in deletePb)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            deletePb.Clear();

            if (videosPb.Count > 0)
            {
                foreach (PictureBox pb in videosPb)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            videosPb.Clear();


            if (shortVideosPb.Count > 0)
            {
                foreach (PictureBox pb in shortVideosPb)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            shortVideosPb.Clear();


            if (lpb.Count > 0)
            {
                foreach (PictureBox pb in lpb)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            lpb.Clear();

            if (wpb.Count > 0)
            {
                foreach (PictureBox pb in wpb)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            wpb.Clear();

            if (lpbDupe.Count > 0)
            {
                foreach (PictureBox pb in lpbDupe)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            lpbDupe.Clear();

            if (wpbDupe.Count > 0)
            {
                foreach (PictureBox pb in wpbDupe)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            wpbDupe.Clear();

            if (unListed.Count > 0)
            {
                foreach (PictureBox pb in unListed)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            unListed.Clear();


            if (gpb.Count > 0)
            {
                foreach (PictureBox pb in gpb)
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
            }
            gpb.Clear();

            if (disposePb.Count > 0)
            {
                foreach (Image img in disposePb)
                {
                    if (img != null)
                        img.Dispose();
                }
            }
            disposePb.Clear();
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "" && File.Exists(File.ReadAllText(prevfi.FullName + "\\disPic.txt")))
            {
                Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());
                button3.Tag = img1;
                disposePb.Add(img1);
            }
            if (File.ReadAllText(nextFi.FullName + "\\disPic.txt") != "" && File.Exists(File.ReadAllText(nextFi.FullName + "\\disPic.txt")))
            {
                Image img2 = Image.FromFile(File.ReadAllText(nextFi.FullName + "\\disPic.txt").Trim());
                button4.Tag = img2;
                disposePb.Add(img2);
            }
            if (File.ReadAllText(mainDi.FullName + "\\disPic.txt") != "" && File.Exists(File.ReadAllText(mainDi.FullName + "\\disPic.txt")))
            {
                Image img3 = Image.FromFile(File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim());
                button5.Tag = img3;
                disposePb.Add(img3);
            }
            if (globalPb.Image != null) globalPb.Image.Dispose();

            if(type == "Videos" || type == " Short Videos")
            {
                flowLayoutPanel1.ContextMenuStrip = contextMenuStrip1;
            }
            else
            {
                flowLayoutPanel1.ContextMenuStrip = contextMenuStrip3;
            }

            GC.Collect();
        }

        private void pbClick(PictureBox pb)
        {
            if (type == "Videos")
            {
                Application.RemoveMessageFilter(this);
                Point controlLoc = this.PointToScreen(pb.Location);
                Point relativeLoc = new Point(controlLoc.X - this.Location.X + 276, controlLoc.Y - this.Location.Y - 10 + 106);
                miniVideoPlayer.setData(pb, new FileInfo(pb.Name), this);
                miniVideoPlayer.axWindowsMediaPlayer1.enableContextMenu = false;

                miniVideoPlayer.Location = relativeLoc;
                miniVideoPlayer.Size = new Size(515, 301);
                miniVideoPlayer.axWindowsMediaPlayer1.URL = pb.Name;

                miniVideoPlayer.Show();
                if (enter == true)
                    miniVideoPlayer.axWindowsMediaPlayer1_MouseDownEvent(null, null);
            }
            else if (type == "Pictures" || type == "4K" || type == "Gifs")
            {

                Application.RemoveMessageFilter(this);
                if (pb.Image.Width > pb.Image.Height || type == "Gifs")
                {
                    PicViewer picViewer = new PicViewer(pb.Name, this, type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), 1920, 970, type == "Gifs" ? true : false);

                    picViewer.Size = new Size(1920, 970); picViewer.Location = new Point(0, 0);
                    picViewer.Show();
                    wmpSide1 = new wmpSide(null, picViewer, true);
                    wmpSide1.BackColor = darkBackColor;
                    wmpSide1.Location = new Point(19, 970);
                    wmpSide1.fillUpFP1(type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), true);
                    wmpSide1.Size = new Size(1910, 110);
                }
                else
                {
                    PicViewer picViewer = new PicViewer(pb.Name, this, type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), 1750, 1080, false);
                    picViewer.Size = new Size(1750, 1080);
                    picViewer.Location = new Point(170, 0);
                    picViewer.Show();
                    wmpSide1 = new wmpSide(null, picViewer, true);
                    wmpSide1.BackColor = darkBackColor;
                    wmpSide1.Location = new Point(5, 8);
                    wmpSide1.fillUpFP1(type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), false);
                    wmpSide1.Size = new Size(120, 1080);

                }
                wmpSide1.Show();
            }
            /*else if (type == "4K")
            {
                Application.RemoveMessageFilter(this);
                if (mutltiSelect == true)
                {
                    if (pb.BackColor == mouseClickColor)
                    {
                        deletePb.Remove(pb);
                        pb.BackColor = lightBackColor;
                    }
                    else
                    {
                        deletePb.Add(pb);
                        pb.BackColor = mouseClickColor;
                    }
                    return;
                }

                vertScrollValue = flowLayoutPanel1.VerticalScroll.Value;
                GC.Collect();
                Form2 form2 = null;
                if (pb.Image.Width > pb.Image.Height)
                {
                    form2 = new Form2(pb, this, wpbDupe, Calculator.disCef, true);
                    form2.Size = new Size(1920,970); form2.Location = new Point(0, 0);
                    form2.Show();
                    wmpSide1 = new wmpSide(null, form2, null, true);
                    wmpSide1.BackColor = darkBackColor;
                    wmpSide1.Location = new Point(19, 970);
                    wmpSide1.fillUpFP1(wpbDupe,true);
                    wmpSide1.Size = new Size(1910, 110);
                }
                else
                {
                    form2 = new Form2(pb, this, lpbDupe, Calculator.disCef, true);
                    form2.Size = new Size(1750, 1080); form2.Location = new Point(170, 0);
                    form2.Show();
                    wmpSide1 = new wmpSide(null, form2, null, true);
                    wmpSide1.BackColor = darkBackColor;
                    form2.button6.Location = new Point(form2.button6.Location.X - 170, form2.button6.Location.Y);
                    form2.button7.Location = new Point(form2.button7.Location.X - 170, form2.button7.Location.Y);
                    form2.button8.Location = new Point(form2.button8.Location.X - 170, form2.button8.Location.Y);
                    wmpSide1.Location = new Point(5, 8);
                    wmpSide1.fillUpFP1(lpbDupe,false);
                    wmpSide1.Size = new Size(120, 1080);
                }
                wmpSide1.Show();
                form2.Activate();
                Calculator.disCef = true;
                disCef = true;
            }*/
            else if (type == " Short Videos")
            {
                Application.RemoveMessageFilter(this);
                WMP wmp = null;

                wmp = new WMP(pb);
                //wmp.axWindowsMediaPlayer1.URL = fileInfo.FullName;
                wmp.axWindowsMediaPlayer1.Name = pb.Name;
                WMPLib.IWMPPlaylist playlist = wmp.axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
                WMPLib.IWMPMedia media;
                WMPLib.IWMPMedia currMedia = null;
                int i = 0;
                DirectoryInfo shortVideoDi = new DirectoryInfo(new FileInfo(pb.Name).DirectoryName);

                List<FileInfo> filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
                if (sortBySize)
                    filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.Length).ToList();
                if (sortByDate)
                    filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();


                foreach (FileInfo file in filesFi)
                {
                    media = wmp.axWindowsMediaPlayer1.newMedia(file.FullName);
                    playlist.appendItem(media);

                    if (file.FullName.Equals(pb.Name))
                        currMedia = media;
                }

                wmp.axWindowsMediaPlayer1.currentPlaylist = playlist;
                wmp.axWindowsMediaPlayer1.Ctlcontrols.playItem(currMedia);

                wmp.Location = new Point(298, 100);
                wmp.calculateDuration(0, true);

                wmpSide1 = new wmpSide(wmp, null, true);
                wmpSide1.fillUpFP1(shortVideosPb);
                wmpSide1.Location = new Point(0, 55);

                TranspBack transpBack = new TranspBack(wmp, wmpSide1, this, shortVideosPb);
                transpBack.Show();
                wmp.Show();
                wmpSide1.Show();
            }
        }

        public void renameVideoFiles(String filePath)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.Contains("placeholdeerr") || fi.Name.ToLower().EndsWith(".txt"))
                    continue;
                var inputFile = new MediaFile { Filename = fi.FullName };
                engine.GetMetadata(inputFile);
                int seconds = inputFile.Metadata.Duration.Seconds;
                int min = inputFile.Metadata.Duration.Minutes;
                int hours = inputFile.Metadata.Duration.Hours;
                String vidDetText = "Reso^ " + inputFile.Metadata.VideoData.FrameSize + "  " + "Dura^ " + (hours > 0 ? hours + " Hour " : "") + (min > 0 ? min + " Min " : "") + (seconds > 0 ? seconds + " Sec" : "");
                FileInfo videoFi = new FileInfo(fi.FullName);

                String siz;
                double gbd = (double)(videoFi.Length) / (1000.0 * 1000.0 * 1000.0);
                gbd = Math.Round(gbd, 2);
                double mbd = (double)(videoFi.Length) / (1000.0 * 1000.0);
                mbd = Math.Round(mbd, 2);
                long kbd = videoFi.Length / (1000);

                if (gbd > 1)
                {
                    siz = gbd + " GB";
                }
                else if (mbd > 1)
                {
                    siz = mbd + " MB";
                }
                else
                {
                    siz = kbd + " KB";
                }

                vidDetText = vidDetText + "  Size^ " + siz;

                String fileName = fi.Name;
                if (File.Exists(di.FullName + "\\imgPB\\resized_" + fi.Name + ".jpg"))
                {
                    try
                    {
                        File.Copy(@di.FullName + "\\imgPB\\resized_" + fi.Name + ".jpg", @fi.DirectoryName + "\\imgPB\\resized_" + vidDetText + "placeholdeerr" + fi.Name + ".jpg");
                        File.Delete(@di.FullName + "\\imgPB\\resized_" + fi.Name + ".jpg");
                    }
                    catch
                    {
                        try
                        {
                            fileName = di.Name + fileName.Substring(fileName.LastIndexOf("."));
                            File.Copy(@di.FullName + "\\imgPB\\resized_" + fi.Name + ".jpg", @fi.DirectoryName + "\\imgPB\\resized_" + vidDetText + "placeholdeerr" + fileName + ".jpg");
                            File.Delete(@di.FullName + "\\imgPB\\resized_" + fi.Name + ".jpg");
                        }
                        catch { }
                    }
                }
                else
                {
                    try
                    {
                        File.Copy(@di.FullName + "\\imgPB\\" + fi.Name + ".jpg", @fi.DirectoryName + "\\imgPB\\" + vidDetText + "placeholdeerr" + fi.Name + ".jpg");
                        File.Delete(@di.FullName + "\\imgPB\\" + fi.Name + ".jpg");
                    }
                    catch
                    {
                        try
                        {
                            fileName = di.Name + fileName.Substring(fileName.LastIndexOf("."));
                            File.Copy(@di.FullName + "\\imgPB\\" + fi.Name + ".jpg", @fi.DirectoryName + "\\imgPB\\" + vidDetText + "placeholdeerr" + fileName + ".jpg");
                            File.Delete(@di.FullName + "\\imgPB\\" + fi.Name + ".jpg");
                        }
                        catch { }
                    }
                }
                int o = 0;
                for ( o=0;o<100;o++)
                {
                    try
                    {
                        if (!File.Exists(@fi.DirectoryName + "\\" + vidDetText + "placeholdeerr" + o + fi.Name))
                        {
                            File.Copy(fi.FullName, @fi.DirectoryName + "\\" + vidDetText + "placeholdeerr" + o + fi.Name);
                            fileName = fi.Name;
                            break;
                        }
                    }
                    catch
                    {
                        if (!File.Exists(@fi.DirectoryName + "\\" + vidDetText + "placeholdeerr" + o + fileName))
                        {
                            File.Copy(fi.FullName, @fi.DirectoryName + "\\" + vidDetText + "placeholdeerr" + o + fileName);
                            break;
                        }
                    }
                }
                if (File.Exists(fi.DirectoryName + "\\" + vidDetText + "placeholdeerr" + o + fileName))
                    try
                    {
                        File.Delete(@fi.FullName);
                    }
                    catch { }
            }
        }

        private List<String> GetRandomFiles()
        {
            List<String> priorList = new List<string>();
            DirectoryInfo parDir = new DirectoryInfo("F:\\Calculator");
            DirectoryInfo directory2 = new DirectoryInfo("H:\\vivado\\rand_name\\rand_name.ir");
            List<DirectoryInfo> supPar = new List<DirectoryInfo>();
            supPar.Add(directory2);
            supPar.Add(parDir);

            if (isGlobalChecked)
            {

                foreach (DirectoryInfo di0 in supPar)
                    foreach (DirectoryInfo di in di0.GetDirectories())
                    {
                        foreach (DirectoryInfo di2 in di.GetDirectories())
                        {
                            Random r = new Random();
                            foreach (FileInfo fiii in di2.GetFiles())
                            {
                                if (!fiii.FullName.EndsWith(".txt"))
                                {
                                    String temp = di2.GetFiles().ElementAt(r.Next(di2.GetFiles().Length)).FullName;
                                    while (temp.EndsWith(".txt"))
                                        temp = di2.GetFiles().ElementAt(r.Next(di2.GetFiles().Length)).FullName;
                                    priorList.Add("0@" + temp);
                                    break;
                                }
                            }

                        }
                    }
            }
            else
            {
                foreach (DirectoryInfo di2 in mainDi.Parent.GetDirectories())
                {
                    Random r = new Random();
                    foreach (FileInfo fiii in di2.GetFiles())
                    {
                        if (!fiii.FullName.EndsWith(".txt"))
                        {
                            String temp = di2.GetFiles().ElementAt(r.Next(di2.GetFiles().Length)).FullName;
                            while (temp.EndsWith(".txt"))
                                temp = di2.GetFiles().ElementAt(r.Next(di2.GetFiles().Length)).FullName;
                            priorList.Add("0@" + temp);
                            break;
                        }
                    }

                }
            }
            return priorList;
        }

        private void VideoPlayer_Load()
        {
            allVidDet.Clear();
            List<String> priorityList = new List<String>();
            if (!File.Exists(mainDi.FullName + "\\priority.txt"))
            {
                FileStream fi = File.Create(mainDi.FullName + "\\priority.txt");
                fi.Close();
            }

            String[] priorStr = File.ReadAllLines(mainDi.FullName + "\\priority.txt");
            renameVideoFiles(mainDi.FullName);
            for (int i = 0; i < priorStr.Length; i++)
                priorityList.Add(priorStr[i]);

            priorityList = manualSort(priorityList);

            foreach (FileInfo fi in mainDi.GetFiles().OrderByDescending(f => f.Length).ToList())
            {
                Boolean temp = false;
                foreach (String str in priorityList)
                    if (str.Contains(fi.Name)) { temp = true; break; }
                if (temp == false && !fi.Name.EndsWith(".txt")) { priorityList.Add("0@" + fi.FullName); }
            }
            String toWriteText = "";
            foreach (String str in priorityList)
            {
                String priority = str.Substring(0, str.IndexOf("@"));
                String fileName = str.Substring(str.IndexOf("@") + 1);
                fileName = fileName.Substring(fileName.LastIndexOf("\\"));
                fileName = mainDi.FullName + fileName;
                FileInfo fii = new FileInfo(fileName);
                if (fii.Exists)
                    toWriteText = toWriteText + priority + "@" + fii.FullName + "\n";
            }
            File.WriteAllText(mainDi.FullName + "\\priority.txt", toWriteText);
            priorityList.Clear();
            priorStr = File.ReadAllLines(mainDi.FullName + "\\priority.txt");

            for (int i = 0; i < priorStr.Length; i++)
                priorityList.Add(priorStr[i]);

            List<Label> meta = new List<Label>();
            int noOfCtrl = 0;
            foreach (FileInfo fi in mainDi.GetFiles())
            {
                if (fi.Name.Contains(".txt"))
                    continue;
                noOfCtrl++;
            }
            if (priorityList.Count != noOfCtrl)
            {
                DialogResult result = MessageBox.Show("No of controls added did not matched with actual count", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            String[] links = File.ReadAllLines(mainDi + "\\links.txt");
            String[] webLinks = File.ReadAllLines(mainDi + "\\webLinks.txt");
            foreach (String s in links)
            {
                priorityList.Insert(0, "0@" + s);
            }

            if (isChecked)
            {
                priorityList = GetRandomFiles();
            }

            newProgressBar.Maximum = priorityList.Count;
            for (int i = 0; i < priorityList.Count; i++)
            {
                FileInfo fileInfo = null;
                if (priorityList[i].Split('@')[1] != "")
                    fileInfo = new FileInfo(priorityList[i].Split('@')[1]);
                else
                    continue;

                if (!File.Exists(fileInfo.FullName) || fileInfo.FullName.EndsWith(".txt"))
                    continue;
                if (fileInfo.Length == 0) {
                    try
                    {
                        File.Delete(fileInfo.FullName);
                        
                    }catch{ }
                    continue;
                }
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Top;
                pb.Name = fileInfo.FullName;
                pb.Size = new Size(515, 292);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Cursor = Cursors.Hand;
                pb.ContextMenuStrip = contextMenuStrip1;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 30, 30));
                pb.Image = setDefaultPic(fileInfo, pb);
                pb.Margin = new Padding(5, 10, 17, 0);

                videoUrls.Add(fileInfo.Name);
                videosPb.Add(pb);
                flowLayoutPanel1.Controls.Add(videosPb.ElementAt(videosPb.Count - 1));
                newProgressBar.PerformStep();

                String vidDetText = fileInfo.Name.Contains("placeholdeerr") ?fileInfo.Name.Replace("placeholdeerr", "\n").Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:").Substring(fileInfo.Name.IndexOf("Reso")):"";
                //int hours = (int)(duration / 3600);
                //int min = (int)((duration / 60) - (60 * hours));

                Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(515, 48);
                vidDetails.ForeColor = Color.White;
                vidDetails.TextAlign = ContentAlignment.TopCenter;
                vidDetails.Padding = new Padding(0);
                vidDetails.Margin = new Padding(5, 0, 17, 5);
                allVidDet.Add(vidDetails);

                vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 15, 15));
                meta.Add(vidDetails);

                if (meta.Count == 3)
                {
                    foreach (Label label in meta)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }
                    meta.Clear();
                }

                pb.MouseClick += (s, args) =>
                {
                    if (prevPB != null)
                    {
                        prevPB.BackColor = darkBackColor;
                        Font myfont1 = new Font("Consolas", 9, FontStyle.Regular);
                        globalDetails.Font = myfont1;
                        globalDetails.ForeColor = Color.White;
                    }
                    prevPB = pb;
                    globalDetails = vidDetails; 
                    Font myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails.ForeColor = mouseClickColor;
                    enter = false;
                    pbClick(pb);
                };
            }

            for (int y = 0; y < 3 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(515, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            foreach (Label label in meta)
            {
                label.Margin = new Padding(5, 0, 17, 50);
                flowLayoutPanel1.Controls.Add(label);
            }
            meta.Clear();
            /*Button dummy = new Button();
            dummy.Text = "";
            dummy.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy.BackColor = lightBackColor;
            dummy.Size = new Size(515 * (3-meta.Count), 1);
            dummy.Margin = new Padding(0, 3, 3, 3);
            dummy.FlatStyle = FlatStyle.Flat;
            dummy.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy);*/
            if(priorityList.Count == 0)
            {
                newProgressBar.Maximum = 100;
                newProgressBar.Value = 100;
            }
            else
            {
                newProgressBar.Value = newProgressBar.Maximum;
            }
            button5.Text = button5.Text.Substring(0, button5.Text.Contains("(") ? button5.Text.IndexOf("(") : button5.Text.Length) + "(" + priorityList.Count + ")";
            miniVideoPlayer = new miniVideoPlayer(videosPb);
        }

        private void clearImageStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;
                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                videosBtn_Click(null, null);
            }
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            expBtn_Click(null, null);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            expBtn_Click(null, null);
        }

        private void picturesBtn_Click(object sender, EventArgs e)
        {
            if (globalBtn != null)
                globalBtn.BackColor = lightBackColor;
            scrollZero = false;
            type = "Pictures";
            globalBtn = picturesBtn;
            picturesBtn.BackColor = mouseClickColor;
            deletePb.Clear();
            controlDisposer();
            GC.Collect();
            flowLayoutPanel1.Padding = new Padding(5, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            imageViewer_load();
        }

        private void ResizeImage(string SoucePath, string DestPath, params int[] numbers)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(SoucePath);

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

                if (numbers.Length == 4 && numbers[1] != 0 && numbers[0]==0)
                {
                    destWidth = (int)((double)numbers[1] * ratio);
                    destHeight = (int)((double)numbers[1]);
                }
                else if (numbers.Length == 4 && numbers[1] == 0 && numbers[0] != 0)
                {
                    destWidth = (int)((double)numbers[0]);
                    destHeight = (int)((double)numbers[0] / ratio);
                }
                else if(numbers.Length == 4 && numbers[1] != 0 && numbers[0] != 0)
                {
                    destWidth = (int)((double)numbers[0]);
                    destHeight = (int)((double)numbers[1]);
                }
                else
                {
                    destWidth = 195;
                    destHeight = (int)((double)195/ratio);
                }
            }
            Bitmap bmp = new Bitmap(destWidth, destHeight);

            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.DrawImage(img, 0, 0, destWidth, destHeight);

            img.Dispose();
            /*String ext = Path.GetExtension(SoucePath).ToLower();
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(DestPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    byte[] bytes;
                    switch (ext)
                    {
                        case ".png":
                            bmp.Save(memory, ImageFormat.Png);
                            bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".jpe":
                        case ".jfif":
                        case ".exif":
                            bmp.Save(memory, ImageFormat.Jpeg);
                            bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case ".gif":
                            bmp.Save(memory, ImageFormat.Gif);
                            bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case ".bmp":
                        case ".dib":
                        case ".rle":
                            bmp.Save(memory, ImageFormat.Bmp);
                            bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                        case ".tiff":
                        case ".tif":
                            bmp.Save(memory, ImageFormat.Tiff);
                            bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                    }
                }
            }*/
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp.Save(DestPath, myImageCodecInfo, myEncoderParameters);
            //bmp.Save(DestPath);
            bmp.Dispose();
            GC.Collect();
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

        private String renameFile(int i, FileInfo fi)
        {

            Image img = Image.FromFile(fi.FullName);
            String siz = "";
            double gbd = (double)(fi.Length) / (1000.0 * 1000.0 * 1000.0);
            gbd = Math.Round(gbd, 2);
            double mbd = (double)(fi.Length) / (1000.0 * 1000.0);
            mbd = Math.Round(mbd, 2);
            long kbd = fi.Length / (1000);

            if (gbd > 1)
            {
                siz = gbd + " GB";
            }
            else if (mbd > 1)
            {
                siz = mbd + " MB";
            }
            else
            {
                siz = kbd + " KB";
            }

            siz = siz + "^^" + img.Width + "x" + img.Height + "placeholdderr" + i;
            img.Dispose();
            String direc = fi.DirectoryName.Replace("\\Pics", "");
            direc = direc.Substring(direc.LastIndexOf("\\") + 1);
            try
            {
                File.Move(fi.FullName, fi.DirectoryName + "\\" + siz + direc + fi.Extension);
            }
            catch
            {
            }
            return (fi.DirectoryName + "\\" + siz + direc + fi.Extension);
        }

        private void createSmallResFiles(DirectoryInfo di)
        {
            int i = 1;

            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.ToLower().EndsWith(".txt")) continue;
                if (!fi.Name.Contains("placeholdderr"))
                {
                    i++;
                }
            }

            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.ToLower().EndsWith(".txt")) continue;
                if (!fi.Name.Contains("placeholdderr"))
                {
                    renameFile(i, fi);
                    i++;
                }
            }

            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.ToLower().EndsWith(".txt")) continue;
                if (!File.Exists(di.FullName + "\\ImgPB\\" + fi.Name))
                    ResizeImage(fi.FullName, di.FullName + "\\ImgPB\\" + fi.Name, 195, 0, 262, 0);
            }
        }

        private void setBestRes(Double Width, Double Height, Double ratio, PictureBox pbb)
        {
            int bestHeight = 1010;
            int bestWidth = 1800;

            if (Width > bestWidth && Height > bestHeight)
            {
                pbb.Height = bestHeight;
                pbb.Width = (int)(bestHeight / ratio);
            }
            else if (Width > bestWidth)
            {
                pbb.Width = bestWidth;
                pbb.Height = (int)(bestWidth * ratio);
            }
            else if (Height > bestHeight)
            {
                pbb.Height = bestHeight;
                pbb.Width = (int)(bestHeight / ratio);
            }
            else
            {
                pbb.Height = (int)Height;
                pbb.Width = (int)Width;
            }
        }

        private Image zoomPic(Image img, Double zoom)
        {
            Bitmap bm = null;
            try
            {
                bm = new Bitmap(img, Convert.ToInt32((int)(img.Width * zoom)), Convert.ToInt32((int)(img.Height * zoom)));
                Graphics gpu = Graphics.FromImage(bm);
                gpu.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
            catch { return img; }
            GC.Collect();
            return bm;
        }

        /*private void setSingleImage(PictureBox pb, PictureBox gg)
        {
            globalPb = gg;
            panel1.AutoScroll = true;
            if (pbb.Image != null)
            {

                panel1.Controls.Clear();
                pbb.Image.Dispose();
            }
            pbb = new PictureBox();
            Image img = Image.FromFile(pb.Name);
            pbb.Image = img;
            pbb.SizeMode = PictureBoxSizeMode.StretchImage;
            Double Width = pbb.Image.Width;
            Double Height = pbb.Image.Height;
            Double ratio = (Double)(Height / Width);
            setBestRes(Width, Height, ratio, pbb);
            Width = pbb.Image.Width;
            Height = pbb.Image.Height;
            ratio = (Double)(Height / Width);
            if (globalBtn.Text.Equals("Gifs"))
            {
                if (pbb.Width > pbb.Height && pbb.Width < 900)
                {
                    pbb.Width = 900;
                    pbb.Height = (int)(ratio * 900);
                }

                if (pbb.Width < pbb.Height && pbb.Height < 700)
                {
                    pbb.Height = 700;
                    pbb.Width = (int)(pbb.Height / ratio);
                }
            }
            flowLayoutPanel1.Hide();
            flowLayoutPanel1.Dock = DockStyle.None;
            //flowLayoutPanel1.Size = new Size(0, 0);
            panel1.Show();
            panel1.Location = flowLayoutPanel1.Location;

            panel1.Controls.Add(pbb);
            panel1.Size = flowLayoutPanel1.Size;
            pbb.Name = pb.Name;
            pbb.Anchor = AnchorStyles.None;
            pbb.Location = new Point((panel1.Width / 2) - (pbb.Width / 2),
                      (panel1.Height / 2) - (pbb.Height / 2));
            pbb.Refresh();

            if (!globalBtn.Text.Equals("Gifs"))
            {
                pbb.MouseDoubleClick += (senderr, argss) =>
                {
                    pbb.Image = img;
                    setSingleImage(pbb, globalPb);
                };
                zoom = 1.0;
                pbb.MouseClick += (senderr, argss) =>
                {
                    if (argss.Button == MouseButtons.Right)
                    {
                        flowLayoutPanel1.VerticalScroll.Value = vertScrollValue;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, vertScrollValue);
                        panel1.Hide();
                        panel1.Controls.Clear();
                        panel1.Dock = DockStyle.None;
                        panel1.Size = new Size(0, 0);
                        flowLayoutPanel1.Show();
                        return;
                    }
                    if (argss.Button == MouseButtons.Middle)
                    {
                        Bitmap bmp = new Bitmap(pbb.Image);
                        for (int i = 0; i < 100; i++)
                        {
                            if (!File.Exists(mainDi.FullName + "\\Pics\\" + i + " - " + mainDi.Name + ".jpg"))
                            {
                                bmp.Save(mainDi.FullName + "\\Pics\\" + i + " - " + mainDi.Name + ".jpg");
                                break;
                            }
                        }
                        return;
                    }
                    zoom = zoom + 0.25;
                    pbb.Image = zoomPic(img, zoom);
                    SetDisplayRectLocation(0, 500);
                    AdjustFormScrollbars(true);
                    pbb.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    pbb.SizeMode = PictureBoxSizeMode.AutoSize;
                    pbb.Size = new Size(pbb.Image.Width, pbb.Image.Height);
                    int locX = (panel1.Width / 2) - (pbb.Image.Width / 2);
                    int locY = (panel1.Height / 2) - (pbb.Image.Height / 2);
                    pbb.Location = new Point(locX > 0 ? locX : 0,
                          locY > 0 ? locY : 0);

                };

                pbb.MouseWheel += (senderr, argss) =>
                {
                    if (argss.Button == MouseButtons.Right)
                    {
                        flowLayoutPanel1.VerticalScroll.Value = vertScrollValue;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, vertScrollValue);
                        panel1.Hide();
                        panel1.Controls.Clear();
                        panel1.Dock = DockStyle.None;
                        panel1.Size = new Size(0, 0);
                        flowLayoutPanel1.Show();
                        return;
                    }
                    zoom = zoom + (argss.Delta > 0 ? 0.15 : -0.08);
                    pbb.Image = zoomPic(img, zoom);
                    SetDisplayRectLocation(0, 500);
                    AdjustFormScrollbars(true);
                    pbb.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    pbb.SizeMode = PictureBoxSizeMode.AutoSize;
                    pbb.Size = new Size(pbb.Image.Width, pbb.Image.Height);
                    int locX = (panel1.Width / 2) - (pbb.Image.Width / 2);
                    int locY = (panel1.Height / 2) - (pbb.Image.Height / 2);
                    pbb.Location = new Point(locX > 0 ? locX : 0,
                          locY > 0 ? locY : 0);

                };
            }
        }*/

        private void imageViewer_load()
        {
            flowLayoutPanel1.Padding = new Padding(5, 0, 0, 0);
            DirectoryInfo picsDir = new DirectoryInfo(mainDi.FullName + "\\Pics");
            createSmallResFiles(picsDir);
            GC.Collect();
            int count = picsDir.GetFiles().Count(), i = 0;
            List<FileInfo> filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            if (sortBySize)
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.Length)
                                                  .ToList();
            if (sortByDate)
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();

            foreach (FileInfo fi in filesFi)
            {
                if (fi.Length == 0)
                {
                    try
                    {
                        File.Delete(fi.FullName);
                    }
                    catch { }
                    continue;
                }
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Top;
                pb.Name = fi.FullName;
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.ContextMenuStrip = contextMenuStrip3;
                pb.Margin = new Padding(3);
                try
                {
                    Image imgg = Image.FromFile(picsDir.FullName + "\\imgPB\\" + fi.Name);
                    pb.Image = imgg;
                    pb.ImageLocation = picsDir.FullName + "\\imgPB\\" + fi.Name;
                    if (imgg.Width > imgg.Height)
                    {
                        Double ratio = (Double)imgg.Width / (Double)imgg.Height;
                        pb.Height = (int)(318 / ratio);
                        pb.Width = 318;
                        wpb.Add(pb);
                    }
                    else
                    {
                        Double ratio = (Double)imgg.Width / (Double)imgg.Height;
                        pb.Width = 225;
                        pb.Height = (int)(225 / ratio);
                        lpb.Add(pb);
                    }
                }
                catch (Exception) { }
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 20, 20));
                pb.MouseClick += (s, args) =>
                {
                    if (mutltiSelect == true)
                    {
                        if (pb.BackColor == mouseClickColor)
                        {
                            deletePb.Remove(pb);
                            pb.BackColor = lightBackColor;
                        }
                        else
                        {
                            deletePb.Add(pb);
                            pb.BackColor = mouseClickColor;
                        }
                        return;
                    }
                    deletePb.Clear();
                    deletePb.Add(pb);

                    if (prevPB == null)
                    {
                        prevPB = pb;
                        prevPB.BackColor = selectedPbColor;
                        return;
                    }

                    prevPB.BackColor = lightBackColor;

                    prevPB = pb;
                    prevPB.BackColor = selectedPbColor;
                };

                pb.MouseDoubleClick += (s, args) =>
                {
                    pbClick(pb);
                };

                pb.MouseEnter += (s, args) => {

                    if (pb.BackColor != mouseClickColor)
                        pb.BackColor = lightBackColor;
                };

                pb.MouseLeave += (s, args) => {
                    if(pb.BackColor == lightBackColor)
                    pb.BackColor = darkBackColor;
                };
            }

            int tempI = 1;
            newProgressBar.Maximum = lpb.Count + wpb.Count;
            List<Label> meta = new List<Label>();
            foreach (PictureBox pb in lpb)
            {
                gpb.Add(pb);
                newProgressBar.PerformStep();
                i++;
                flowLayoutPanel1.Controls.Add(gpb.ElementAt(gpb.Count - 1));
                if (tempI % 5 == 0)
                    uiReload();
                tempI++;

                pb.Paint += (s, args) =>
                {
                    using (Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\tReso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(8, pb.Height - 18));
                    }
                };
                /*Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(264, 40);
                vidDetails.ForeColor = Color.DarkGray;
                vidDetails.TextAlign = ContentAlignment.TopCenter;
                vidDetails.Padding = pb.Padding;
                vidDetails.Margin = new Padding(3);
                meta.Add(vidDetails);

                if (meta.Count == 6)
                {
                    foreach (Label label in meta)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }
                    meta.Clear();
                }*/
            }

            PictureBox dumPb = new PictureBox();
            dumPb.Dock = DockStyle.Top;
            dumPb.Size = new Size(225 * (7 - lpb.Count % 7), 1);
            flowLayoutPanel1.Controls.Add(dumPb);
            /*Button dummy = new Button();
            dummy.Text = "";
            dummy.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy.BackColor = lightBackColor;
            dummy.Size = new Size(264 * (6 - meta.Count), 1);
            dummy.Margin = new Padding(3);
            dummy.FlatStyle = FlatStyle.Flat;
            dummy.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy);

            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }

            Button dummy1 = new Button();
            dummy1.Text = "";
            dummy1.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy1.BackColor = lightBackColor;
            dummy1.Size = new Size(264 * (6 - meta.Count), 1);
            dummy1.Margin = new Padding(3);
            dummy1.FlatStyle = FlatStyle.Flat;
            dummy1.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy1);

            meta.Clear();*/

            foreach (PictureBox pb in wpb)
            {
                gpb.Add(pb);
                newProgressBar.PerformStep();
                i++;
                flowLayoutPanel1.Controls.Add(gpb.ElementAt(gpb.Count - 1));
                pb.Paint += (s, args) =>
                {
                    using (Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\t\t  Reso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(25, pb.Height - 16));
                    }
                };
                /*Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(399, 40);
                vidDetails.ForeColor = Color.DarkGray;
                vidDetails.TextAlign = ContentAlignment.TopCenter;
                vidDetails.Padding = pb.Padding;
                vidDetails.Margin = new Padding(3);
                meta.Add(vidDetails);*/

                /*if (meta.Count == 4)
                {
                    foreach (Label label in meta)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }
                    meta.Clear();
                }*/
            }
            PictureBox dumPb1 = new PictureBox();
            dumPb1.Dock = DockStyle.Top;
            dumPb1.Size = new Size(1500,30);
            flowLayoutPanel1.Controls.Add(dumPb1);

            if (lpb.Count + wpb.Count == 0)
            {
                newProgressBar.Maximum = 100;
                newProgressBar.Value = 100;
            }
            else
            {
                newProgressBar.Value = newProgressBar.Maximum;
            }
            button5.Text = button5.Text.Substring(0, button5.Text.Contains("(") ? button5.Text.IndexOf("(") : button5.Text.Length) + "(" + gpb.Count + ")";
            /*Button dummy2 = new Button();
            dummy2.Text = "";
            dummy2.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy2.BackColor = lightBackColor;
            dummy2.Size = new Size(399 * (4 - meta.Count), 1);
            dummy2.Margin = new Padding(3);
            dummy2.FlatStyle = FlatStyle.Flat;
            dummy2.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy2);

            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }*/
        }

        private void loadFiles(String dirPath)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";

            ofd.InitialDirectory = dirPath;
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (String fi in ofd.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(fi);
                    for (int k = 0; k < 50; k++)
                    {
                        if (File.Exists(dirPath + "\\" + k + fileInfo.Name))
                            continue;
                        File.Move(fi, dirPath + "\\" + k + fileInfo.Name);
                        break;
                    }
                }
            }
        }

        private void expBtn_Click(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
            this.Hide();
            exitForm();
            exp.Show();
            exp.textBox3.Focus();
        }

        private void VideoPlayer_Enter(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            FileInfo fi = new FileInfo(pb.Name);
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = fi.DirectoryName;
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                try
                {

                    if (deletePb.Count > 0)
                    {
                        foreach (PictureBox delPb in deletePb)
                        {

                            FileInfo fi1 = new FileInfo(delPb.Name);
                            File.Move(fi1.FullName, fbd.SelectedPath + "\\" + fi1.Name);
                        }
                        refreshFolder();
                    }
                    else
                    {

                        File.Move(fi.FullName, fbd.SelectedPath + "\\" + fi.Name);
                        refreshFolder();
                    }
                }
                catch
                {
                    MessageBox.Show("Unable to do the move action!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }


        private void videosBtn_Click(object sender, EventArgs e)
        {

            if (globalBtn != null)
                globalBtn.BackColor = lightBackColor;

            type = "Videos";
            scrollZero = true;
            globalBtn = videosBtn;
            videosBtn.BackColor = mouseClickColor;
            deletePb.Clear();

            controlDisposer();
            this.Controls.Add(newProgressBar);
            flowLayoutPanel1.Padding = new Padding(16, 0, 0, 0);
            VideoPlayer_Load();
        }

        public void loadImages(int noOf)
        {
            newProgressBar.Value = 0;
            newProgressBar.Maximum = noOf;
            for (int j = whereAt; j < (imagesFi.Count < whereAt + noOf ? imagesFi.Count : whereAt + noOf); j++)
            {
                FileInfo fi = imagesFi.ElementAt(j);

                if (fi.Length == 0)
                {
                    try
                    {
                        File.Delete(fi.FullName);
                    }
                    catch { }
                    continue;
                }
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Top;
                pb.Name = fi.Name;
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.Name = fi.FullName;
                pb.ContextMenuStrip = contextMenuStrip3;
                try
                {
                    Image imgg = null;
                    try
                    {
                        imgg = Image.FromFile(_4kdir.FullName + "\\imgPB\\" + fi.Name);
                        pb.ImageLocation = _4kdir.FullName + "\\imgPB\\" + fi.Name;
                        pb.Image = imgg;
                        int width = imgg.Width;
                        int height = imgg.Height;
                        if (width > height)
                        {
                            Double ratio = (Double)imgg.Width / (Double)imgg.Height;
                            pb.Height = (int)(318 / ratio);
                            pb.Width = 318;
                            wpb.Add(pb);
                        }
                        else
                        {
                            Double ratio = (Double)imgg.Width / (Double)imgg.Height;
                            pb.Width = 225;
                            pb.Height = (int)(225 / ratio);
                            lpb.Add(pb);
                        }
                    }
                    catch { }

                }
                catch (Exception) { }

                pb.Margin = new Padding(3);
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 20, 20));
                pb.MouseWheel += (s1, args1) =>
                {
                    VScrollProperties vs = flowLayoutPanel1.VerticalScroll;

                    if (flowLayoutPanel1.VerticalScroll.Value == vs.Maximum - vs.LargeChange + 1)
                    {
                        loadImages(80);
                    }
                };

                pb.MouseClick += (s, args) =>
                {
                    if (mutltiSelect == true)
                    {
                        if (pb.BackColor == mouseClickColor)
                        {
                            deletePb.Remove(pb);
                            pb.BackColor = lightBackColor;
                        }
                        else
                        {
                            deletePb.Add(pb);
                            pb.BackColor = mouseClickColor;
                        }
                        return;
                    }
                    deletePb.Clear();
                    deletePb.Add(pb);

                    if (prevPB == null)
                    {
                        prevPB = pb;
                        prevPB.BackColor = selectedPbColor;
                        return;
                    }

                    prevPB.BackColor = lightBackColor;

                    prevPB = pb;
                    prevPB.BackColor = selectedPbColor;
                };

                pb.MouseDoubleClick += (s, args) =>
                {
                    pbClick(pb);
                };

                pb.MouseEnter += (s, args) => {

                    if (pb.BackColor != mouseClickColor)
                        pb.BackColor = lightBackColor;
                };

                pb.MouseLeave += (s, args) => {

                    if(pb.BackColor == lightBackColor)
                    pb.BackColor = darkBackColor;
                };
            }

            whereAt = whereAt + noOf;
            int tempI = 1, i = 0;
            List<Label> meta = new List<Label>();
            foreach (PictureBox pb in lpb)
            {
                gpb.Add(pb);
                newProgressBar.PerformStep();
                i++;
                lpbDupe.Add(pb);
                flowLayoutPanel1.Controls.Add(pb);
                pb.Paint += (s, args) =>
                {
                    using (Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\tReso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(8, pb.Height - 18));
                    }
                };/*
                Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(264, 50);
                vidDetails.ForeColor = Color.DarkGray;
                vidDetails.TextAlign = ContentAlignment.TopCenter;
                vidDetails.Padding = pb.Padding;
                vidDetails.Margin = new Padding(3);
                meta.Add(vidDetails);

                if (meta.Count == 6)
                {
                    foreach (Label label in meta)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }
                    meta.Clear();
                }*/
            }

            PictureBox dumPb = new PictureBox();
            dumPb.Dock = DockStyle.Top;
            dumPb.Size = new Size(225 * (7 - lpb.Count % 7), 1);
            flowLayoutPanel1.Controls.Add(dumPb);
            /*Button dummy = new Button();
            dummy.Text = "";
            dummy.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy.BackColor = lightBackColor;
            dummy.Size = new Size(264 * (6 - meta.Count), 1);
            dummy.Margin = new Padding(3);
            dummy.FlatStyle = FlatStyle.Flat;
            dummy.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy);

            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }

            Button dummy1 = new Button();
            dummy1.Text = "";
            dummy1.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy1.BackColor = lightBackColor;
            dummy1.Size = new Size(264 * (6 - meta.Count), 1);
            dummy1.Margin = new Padding(3);
            dummy1.FlatStyle = FlatStyle.Flat;
            dummy1.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy1);

            meta.Clear();
            */

            foreach (PictureBox pb in wpb)
            {
                gpb.Add(pb);
                newProgressBar.PerformStep();
                i++;
                wpbDupe.Add(pb);
                flowLayoutPanel1.Controls.Add(pb);

                pb.Paint += (s, args) =>
                {
                    using (Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\t\t  Reso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(25, pb.Height - 16));
                    }
                };
                /*Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(399, 50);
                vidDetails.ForeColor = Color.DarkGray;
                vidDetails.TextAlign = ContentAlignment.TopCenter;
                vidDetails.Padding = pb.Padding;
                vidDetails.Margin = new Padding(3);
                meta.Add(vidDetails);

                if (meta.Count == 4)
                {
                    foreach (Label label in meta)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }
                    meta.Clear();
                }*/
            }

            PictureBox dumPb1 = new PictureBox();
            dumPb1.Dock = DockStyle.Top;
            dumPb1.Size = new Size(318 * (5 - wpb.Count % 5), 1);
            flowLayoutPanel1.Controls.Add(dumPb1);
            button5.Text = button5.Text.Substring(0, button5.Text.Contains("(") ? button5.Text.IndexOf("(") : button5.Text.Length) + "(" + gpb.Count + ")";
            /*Button dummy2 = new Button();
            dummy2.Text = "";
            dummy2.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy2.BackColor = lightBackColor;
            dummy2.Size = new Size(399 * (4 - meta.Count), 1);
            dummy2.Margin = new Padding(3);
            dummy2.FlatStyle = FlatStyle.Flat;
            dummy2.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy2);

            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }


            Button dummy3 = new Button();
            dummy3.Text = "";
            dummy3.Font = new Font("Consolas", 13, FontStyle.Bold);
            dummy3.BackColor = lightBackColor;
            dummy3.Size = new Size(399 * (4 - meta.Count), 1);
            dummy3.Margin = new Padding(3);
            dummy3.FlatStyle = FlatStyle.Flat;
            dummy3.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy3);
            flowLayoutPanel1.VerticalScroll.Value = 0;*/
            newProgressBar.Value = noOf;
            lpb.Clear();
            wpb.Clear();
        }

        private void load4kImages()
        {
            _4kdir = new DirectoryInfo(mainDi.FullName + "\\Pics\\kkkk");

            createSmallResFiles(_4kdir);

            if (sortBySize)
                imagesFi = _4kdir.GetFiles().OrderByDescending(f => f.Length)
                                                      .ToList();
            if(sortByDate)
                imagesFi = _4kdir.GetFiles().OrderByDescending(f => f.CreationTime)
                                                      .ToList();
            newProgressBar.Value = 0;

            if (loadAll)
                loadImages(imagesFi.Count + 10);
            else
                loadImages(100);

        }

        private void fourKBtn_Click(object sender, EventArgs e)
        {
            if (globalBtn != null)
                globalBtn.BackColor = lightBackColor;
            scrollZero = false;
            type = "4K";
            globalBtn = fourKBtn;
            fourKBtn.BackColor = mouseClickColor;
            deletePb.Clear();
            controlDisposer();
            flowLayoutPanel1.Padding = new Padding(5, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            whereAt = 0;
            load4kImages();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            String filePath = "";
            int files = 0;
            if (globalBtn.Text.Equals("Videos"))
            {
                filePath = mainDi.FullName;
                files = Directory.GetFiles(filePath).Count() - 3;
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                filePath = mainDi.FullName + "\\Pics";
                files = Directory.GetFiles(filePath).Count();
            }
            else if (globalBtn.Text.Equals(" Short Videos"))
            {
                filePath = mainDi.FullName + "\\Pics\\GifVideos";
                files = Directory.GetFiles(filePath).Count();
            }
            else if (globalBtn.Text.Equals("4K"))
            {
                filePath = mainDi.FullName + "\\Pics\\kkkk";
                files = Directory.GetFiles(filePath).Count();
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                filePath = mainDi.FullName + "\\Pics\\Gifs";
                files = Directory.GetFiles(filePath).Count();
            }
            MessageBox.Show("Total number of controls - " + (flowLayoutPanel1.Controls.Count - 1) + "\n" + "Total number of file - " + files, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
            FileInfo fi = new FileInfo(pb.Name);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName);
            foreach (FileInfo di1 in di.GetFiles())
            {
                if (di1.Name.StartsWith("_resized"))
                    try
                    {
                        File.Delete(di1.FullName);
                    }
                    catch { }

            }
            for (int i = 0; i < 10; i++)
            {
                if (!File.Exists(fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1)))
                {
                    try { ResizeImage(fi.FullName, fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1), 300, 0, 505, 0); } catch { return; }
                    String newFileName = renameFile(i, new FileInfo(fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1)));
                    File.WriteAllText(mainDi.FullName + "\\disPic.txt", newFileName);
                    imagePBLink = newFileName;
                    break;
                }
            }
            toRefresh = true;
        }

        public void refreshFolder()
        {

            if (globalBtn.Text.Equals("Videos"))
            {
                videosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                picturesBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals(" Short Videos"))
            {
                shortVideosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("4K"))
            {
                fourKBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                gifsBtn_Click(null, null);
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                try
                {
                    if (deletePb.Count > 0)
                    {
                        foreach (PictureBox delPb in deletePb)
                        {
                            FileInfo fi = new FileInfo(delPb.Name);
                            try
                            {
                                File.Delete(delPb.Name);
                                File.Delete(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                            }
                            catch { }
                        }
                        refreshFolder();
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        try
                        {
                            File.Delete(pb.Name);
                            File.Delete(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                        }
                        catch { }
                        refreshFolder();
                    }
                }
                catch { }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
            File.WriteAllText(mainDi.FullName + "\\disPic.txt", pb.Name);
            toRefresh = true;
            imagePBLink = pb.Name;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\kkkk\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                fourKBtn_Click(null, null);
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\GifVideos\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                shortVideosBtn_Click(null, null);
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\Gifs\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                gifsBtn_Click(null, null);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                picturesBtn_Click(null, null);
            }
        }

        private void loadShortVideos()
        {
            DirectoryInfo shortVideoDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\GifVideos");
            int noOfFile = shortVideoDi.GetFiles().Length;
            renameVideoFiles(mainDi.FullName + "\\Pics\\GifVideos");

            List<FileInfo> filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            if (sortBySize)
                filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.Length)
                                                  .ToList();
            if (sortByDate)
                filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            newProgressBar.Maximum = filesFi.Count;
            foreach (FileInfo fileInfo in filesFi)
            {
                if (fileInfo.Length == 0)
                {
                    try
                    {
                        File.Delete(fileInfo.FullName);
                    }
                    catch { }
                    continue;
                }
                if (fileInfo.Name.ToLower().EndsWith(".webm"))
                {
                    try
                    {
                        var inputFile = new MediaFile { Filename = fileInfo.FullName };
                        var outputFile = new MediaFile { Filename = fileInfo.DirectoryName + "\\1" + fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.')) + ".mp4" };

                        using (var eng = new Engine())
                        {
                            eng.Convert(inputFile, outputFile);

                            if (File.Exists(fileInfo.DirectoryName + "\\1" + fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.')) + ".mp4"))
                            {
                                File.Delete(fileInfo.FullName);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to convert!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Top;
                pb.Name = fileInfo.FullName;
                    pb.Size = new Size(397, 225);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Cursor = Cursors.Hand;
                pb.ContextMenuStrip = contextMenuStrip1;
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 35, 35));

                pb.Image = setDefaultPic(fileInfo, pb);
                pb.Margin = new Padding(5, 3, 0, 3);

                pb.MouseClick += (s, args) =>
                {
                    if (mutltiSelect == true)
                    {
                        if (pb.BackColor == mouseClickColor)
                        {
                            deletePb.Remove(pb);
                            pb.BackColor = lightBackColor;
                        }
                        else
                        {
                            deletePb.Add(pb);
                            pb.BackColor = mouseClickColor;
                        }
                        return;
                    }
                    pbClick(pb);
                };

                shortVideosPb.Add(pb);
                newProgressBar.PerformStep();
                flowLayoutPanel1.Controls.Add(shortVideosPb.ElementAt(shortVideosPb.Count - 1));

                Label vidDetails = new Label();
                vidDetails.Text = fileInfo.Name.Replace("placeholdeerr", "\n\n").Replace("Reso^ ", "\nReso:").Replace("Dura^ ", "\nDura:").Replace("Size^ ", "\nSize:").Substring(fileInfo.Name.IndexOf("Reso"));
                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(123, 200);
                vidDetails.ForeColor = Color.White;
                vidDetails.TextAlign = ContentAlignment.TopLeft;
                vidDetails.Margin = new Padding(0, 15, 14, 3);
                vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 20, 20));

                allVidDet.Add(vidDetails);
                flowLayoutPanel1.Controls.Add(vidDetails);
            }
            newProgressBar.Value = filesFi.Count;
            if (filesFi.Count == 0)
            {
                newProgressBar.Maximum = 100;
                newProgressBar.Value = 100;
            }
            else
            {
                newProgressBar.Value = newProgressBar.Maximum;
            }
            button5.Text = button5.Text.Substring(0, button5.Text.Contains("(") ? button5.Text.IndexOf("(") : button5.Text.Length) + "(" + shortVideosPb.Count + ")";
        }

        private void shortVideosBtn_Click(object sender, EventArgs e)
        {
            if (globalBtn != null)
                globalBtn.BackColor = lightBackColor;
            scrollZero = true;
            globalBtn = shortVideosBtn;
            shortVideosBtn.BackColor = mouseClickColor;

            type = " Short Videos";
            controlDisposer();
            deletePb.Clear();
            GC.Collect();
            flowLayoutPanel1.Padding = new Padding(9, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            loadShortVideos();
        }

        private void VideoPlayer_Load(object sender, EventArgs e)
        {
            if (type.Equals("Videos"))
            {
                videosBtn.BackColor = mouseClickColor;
                globalBtn = videosBtn;
                videosBtn_Click(null, null);
            }
            else if (type.Equals("Pictures"))
            {
                picturesBtn.BackColor = mouseClickColor;
                globalBtn = picturesBtn;
                picturesBtn_Click(null, null);
            }
            else if (type.Equals("4K"))
            {
                fourKBtn.BackColor = mouseClickColor;
                globalBtn = fourKBtn;
                fourKBtn_Click(null, null);
            }
            else if (type.Equals(" Short Videos"))
            {
                shortVideosBtn.BackColor = mouseClickColor;
                globalBtn = shortVideosBtn;
                shortVideosBtn_Click(null, null);
            }
            else if (type.Equals("Gifs"))
            {
                gifsBtn.BackColor = mouseClickColor;
                globalBtn = gifsBtn;
                gifsBtn_Click(null, null);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalBtn.Text.Equals("Videos"))
            {
                videosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                picturesBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals(" Short Videos"))
            {
                shortVideosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("4K"))
            {
                fourKBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                gifsBtn_Click(null, null);
            }
        }

        public void uiReload()
        {

        }
        private void gif_Load()
        {
            flowLayoutPanel1.Padding = new Padding(8, 0, 0, 0);
            DirectoryInfo picsDir = new DirectoryInfo(mainDi.FullName + "\\Pics\\Gifs");
            createSmallResFiles(picsDir);

            GC.Collect();

            List<FileInfo> filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            if (sortBySize)
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.Length)
                                                  .ToList();
            if (sortByDate)
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            newProgressBar.Maximum = filesFi.Count;
            foreach (FileInfo fi in filesFi)
            {
                if (fi.Length == 0)
                {
                    try
                    {
                        File.Delete(fi.FullName);
                    }
                    catch { }
                    continue;
                }
                if (fi.Name.ToLower().EndsWith(".txt"))
                    continue;
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Top;
                pb.Name = fi.FullName;
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.ContextMenuStrip = contextMenuStrip3;
                pb.Margin = new Padding(3);
                try
                {
                    Image imgg = Image.FromFile(picsDir.FullName + "\\imgPB\\" + fi.Name);
                    pb.Image = imgg;
                    pb.ImageLocation = picsDir.FullName + "\\imgPB\\" + fi.Name;
                    if (imgg.Width > imgg.Height)
                    {
                        Double ratio = (Double)imgg.Width / (Double)imgg.Height;
                        pb.Height = (int)(315 / ratio);
                        pb.Width = 315;
                        wpb.Add(pb);
                    }
                    else
                    {
                        Double ratio = (Double)imgg.Width / (Double)imgg.Height;
                        pb.Width = 225;
                        pb.Height = (int)(225 / ratio);
                        lpb.Add(pb);
                    }
                }
                catch (Exception) { }
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 20, 20));

                pb.MouseClick += (s, args) =>
                {
                    if (mutltiSelect == true)
                    {
                        if (pb.BackColor == mouseClickColor)
                        {
                            deletePb.Remove(pb);
                            pb.BackColor = lightBackColor;
                        }
                        else
                        {
                            deletePb.Add(pb);
                            pb.BackColor = mouseClickColor;
                        }
                        return;
                    }
                    deletePb.Clear();
                    deletePb.Add(pb);

                    if (prevPB == null)
                    {
                        prevPB = pb;
                        prevPB.BackColor = selectedPbColor;
                        return;
                    }

                    prevPB.BackColor = lightBackColor;

                    prevPB = pb;
                    prevPB.BackColor = selectedPbColor;
                };

                pb.MouseDoubleClick += (s, args) =>
                {
                    pbClick(pb);
                };

            }

            foreach (PictureBox pb in lpb)
            {
                gpb.Add(pb);
                newProgressBar.PerformStep();
                pb.Paint += (s, args) =>
                {
                    using (Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\tReso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(8, pb.Height - 17));
                    }
                };

                flowLayoutPanel1.Controls.Add(gpb.ElementAt(gpb.Count - 1));
            }
            PictureBox dumPb = new PictureBox();
            dumPb.Dock = DockStyle.Top;
            dumPb.Size = new Size(225 * (7 - lpb.Count % 7), 1);
            flowLayoutPanel1.Controls.Add(dumPb);

            foreach (PictureBox pb in wpb)
            {
                gpb.Add(pb);
                newProgressBar.PerformStep();
                pb.Paint += (s, args) =>
                {
                    using (Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\t\t  Reso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(25, pb.Height - 15));
                    }
                };
                flowLayoutPanel1.Controls.Add(gpb.ElementAt(gpb.Count - 1));
            }
            PictureBox dumPb1 = new PictureBox();
            dumPb1.Dock = DockStyle.Top;
            dumPb1.Size = new Size(1500, 30);
            flowLayoutPanel1.Controls.Add(dumPb1);
            if (filesFi.Count == 0)
            {
                newProgressBar.Maximum = 1;
                newProgressBar.Value = 1;
            }
            else
                newProgressBar.Value = newProgressBar.Maximum;
            button5.Text = button5.Text.Substring(0, button5.Text.Contains("(") ? button5.Text.IndexOf("(") : button5.Text.Length) + "(" + gpb.Count + ")";
        }

        private void gifsBtn_Click(object sender, EventArgs e)
        {
            if (globalBtn != null)
                globalBtn.BackColor = lightBackColor;
            scrollZero = true;
            globalBtn = gifsBtn;
            gifsBtn.BackColor = mouseClickColor;

            type = "Gifs";
            deletePb.Clear();
            controlDisposer();
            GC.Collect();
            flowLayoutPanel1.Padding = new Padding(8, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            gif_Load();
        }

        private void VideoPlayer_Activated(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
            if (transpBack != null)
            {
                try
                {
                    transpBack.Hide();
                    transpBack.Dispose();
                    transpBack.Close();
                    GC.Collect();
                }
                catch { }
            }

            if (wmp != null)
            {
                try
                {
                    wmp.Hide();
                    Application.RemoveMessageFilter(wmp);
                    wmp.timer1.Enabled = false;
                    wmp.axWindowsMediaPlayer1.Ctlcontrols.pause();
                    wmp.axWindowsMediaPlayer1.currentPlaylist.clear();
                    wmp.axWindowsMediaPlayer1.Dispose();
                    wmp.miniPlayer.miniVidPlayer.currentPlaylist.clear();
                    wmp.miniPlayer.miniVidPlayer.Dispose();
                    wmp.miniPlayer.Dispose();
                    wmp.Dispose();
                    wmp.Close();
                    GC.Collect();
                }
                catch { }
            }

            if (isCropped)
            {
                refreshToolStripMenuItem_Click(null, null);
                isCropped = false;
            }
        }

        private void ctlDisposer_DoWork(object sender, DoWorkEventArgs e)
        {
            controlDisposer();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Explorer.globalVol = trackBar1.Value;
            button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (type.Equals("Videos"))
            {
                videosBtn.BackColor = mouseClickColor;
                globalBtn = videosBtn;
                videosBtn_Click(null, null);
            }
            else if (type.Equals("Pictures"))
            {
                picturesBtn.BackColor = mouseClickColor;
                globalBtn = picturesBtn;
                picturesBtn_Click(null, null);
            }
            else if (type.Equals("4K"))
            {
                fourKBtn.BackColor = mouseClickColor;
                globalBtn = fourKBtn;
                fourKBtn_Click(null, null);
            }
            else if (type.Equals(" Short Videos"))
            {
                shortVideosBtn.BackColor = mouseClickColor;
                globalBtn = shortVideosBtn;
                shortVideosBtn_Click(null, null);
            }
            else if (type.Equals("Gifs"))
            {
                gifsBtn.BackColor = mouseClickColor;
                globalBtn = gifsBtn;
                gifsBtn_Click(null, null);
            }
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            mutltiSelect = !mutltiSelect;

            button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            mutltiSelect = false;
            if (deletePb.Count > 0)
            {
                foreach (PictureBox delPb in deletePb)
                {
                    delPb.BackColor = lightBackColor;
                }
            }

            button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            String filePath = "";
            if (globalBtn.Text.Equals("Videos"))
            {
                filePath = mainDi.FullName;
                loadFiles(filePath);
                videosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                filePath = mainDi.FullName + "\\Pics";
                loadFiles(filePath);
                picturesBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals(" Short Videos"))
            {
                filePath = mainDi.FullName + "\\Pics\\GifVideos";
                loadFiles(filePath);
                shortVideosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("4K"))
            {
                filePath = mainDi.FullName + "\\Pics\\kkkk";
                loadFiles(filePath);
                fourKBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                filePath = mainDi.FullName + "\\Pics\\Gifs";
                loadFiles(filePath);
                gifsBtn_Click(null, null);
            }
        }

        private void VideoPlayer_Deactivate(object sender, EventArgs e)
        {
            vertScrollValue = flowLayoutPanel1.VerticalScroll.Value;
            Application.RemoveMessageFilter(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (scrollZero == true && noOfTimerRuns <= 10)
            {
                flowLayoutPanel1.VerticalScroll.Value = 0;
                flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                scrollZero = false;
                noOfTimerRuns++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctrl = !ctrl;
            button2.Text = "Taskbar Control " + (ctrl == true ? "On" : "Off");
            button2.BackColor = (ctrl == true ? mouseClickColor : lightBackColor);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
            this.Hide();
            exitForm();
            exp.Show();
            exp.textBox3.Focus();
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
            CropImage cropImage = new CropImage(Image.FromFile(pb.Name), pb.Name);
            cropImage.Show();

        }

        private void moveToPicturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalBtn.Text.Equals("4K"))
            {
                PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
                DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    try
                    {
                        if (deletePb.Count > 0)
                        {
                            foreach (PictureBox delPb in deletePb)
                            {
                                FileInfo fi = new FileInfo(delPb.Name);
                                try
                                {
                                    File.Move(delPb.Name, fi.DirectoryName.Substring(0, fi.DirectoryName.LastIndexOf("\\") + 1) + fi.Name);
                                    delPb.Image.Dispose();
                                    File.Move(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg",
                                        fi.DirectoryName.Substring(0, fi.DirectoryName.LastIndexOf("\\")) + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            FileInfo fi = new FileInfo(pb.Name);
                            try
                            {
                                File.Move(fi.FullName, fi.DirectoryName.Substring(0, fi.DirectoryName.LastIndexOf("\\") + 1) + fi.Name);
                                pb.Image.Dispose();
                                File.Move(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg",
                                    fi.DirectoryName.Substring(0, fi.DirectoryName.LastIndexOf("\\")) + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                            }
                            catch { }

                        }
                    }
                    catch { }
            }
        }

        private void moveTo4KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalBtn.Text.Equals("Pictures"))
            {
                PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
                DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    try
                    {
                        if (deletePb.Count > 0)
                        {
                            foreach (PictureBox delPb in deletePb)
                            {
                                FileInfo fi = new FileInfo(delPb.Name);
                                try
                                {
                                    File.Move(delPb.Name, fi.DirectoryName + "\\kkkk\\" + fi.Name);
                                    delPb.Image.Dispose();
                                    File.Move(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg",
                                       fi.DirectoryName + "\\kkkk\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            FileInfo fi = new FileInfo(pb.Name);
                            try
                            {
                                File.Move(fi.FullName, fi.DirectoryName + "\\kkkk\\" + fi.Name);
                                pb.Image.Dispose();
                                File.Move(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg",
                                   fi.DirectoryName + "\\kkkk\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                            }
                            catch { }

                        }
                    }
                    catch { }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                try
                {
                    FileInfo fi = new FileInfo(pb.Name);
                    try
                    {
                        File.Delete(pb.Name);
                        refreshFolder();
                    }
                    catch { }
                }
                catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            sortBySize = false;
            sortByDate = true;
            bySort = true;
            if (type == "Pictures")
                picturesBtn_Click(null, null);
            else if (type == "4K")
                fourKBtn_Click(null, null);
            else if (type == " Short Videos")
                shortVideosBtn_Click(null, null);
            else if (type == "Gifs")
                gifsBtn_Click(null, null);
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = new FileInfo(pb.Name).DirectoryName;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    DirectoryInfo dif = new DirectoryInfo(fbd.SelectedPath);
                    File.AppendAllText(dif.FullName + "\\links.txt", "\n" + pb.Name);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            isChecked = !isChecked;
            button9.BackColor = (isChecked == true ? mouseClickColor : lightBackColor);
            if (isChecked)
                button10.Enabled = true;
            else
                button10.Enabled = false;
            refreshFolder();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            isGlobalChecked = !isGlobalChecked;
            button10.BackColor = (isGlobalChecked == true ? mouseClickColor : lightBackColor);
            refreshFolder();
        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            mouseHoverColor = colorDialog1.Color;
            mouseClickColor = colorDialog1.Color;
            selectedPbColor = colorDialog1.Color;

            Explorer.globColor = colorDialog1.Color;

            File.WriteAllText(Explorer.pardirectory[1].FullName + "\\ThemeColor.txt",
                                colorDialog1.Color.R + "," + colorDialog1.Color.G + "," + colorDialog1.Color.B);
            toChangeTheme = true;
            setTheme();
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
            PopUpTextBox popUpTextBox = new PopUpTextBox();
            popUpTextBox.ShowDialog();
            if (popUpTextBox.fileName.Length > 0)
            {
                File.AppendAllText(mainDi.FullName + "\\webLinks.txt", "\n" + popUpTextBox.fileName);
            }
            Application.AddMessageFilter(this);
        }


        private void deleteToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                try
                {
                    if (deletePb.Count > 0)
                    {
                        foreach (PictureBox delPb in deletePb)
                        {
                            FileInfo fi = new FileInfo(delPb.Name);
                            try
                            {
                                File.Delete(delPb.Name);
                                File.Delete(fi.DirectoryName + "\\imgPB\\" + fi.Name.Replace(fi.Extension, "") + ".jpg");
                            }
                            catch { }
                        }
                        refreshFolder();
                    }
                }
                catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Text = "Prev: " + mainDi.Name; prevPB = null;
            prevfi = mainDi;
            mainDi = nextFi;
            if (disposePb.Count > 0)
            {
                foreach (Image pb in disposePb)
                {
                    pb.Dispose();
                }
            }
            disposePb.Clear();
            button5.Text = mainDi.Name;
            for (int k = 0; k < folders.Count; k++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folders.ElementAt(k));
                if (dirInfo.Name.ToLower().Contains(mainDi.Name.ToLower()))
                {
                    nextFi = new DirectoryInfo(folders.ElementAt(k + 1 >= folders.Count ? 0 : k + 1));
                }
            }
            button4.Text = "Next: " + nextFi.Name; disposePb.Clear();
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());

                    button3.Tag = img1;
                    disposePb.Add(img1);
                    myPictureBox1.Image = Image.FromFile(prevfi.FullName + "\\imgPB\\smallRes.png");
                }
                catch { }
            }
            if (File.ReadAllText(nextFi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img2 = Image.FromFile(File.ReadAllText(nextFi.FullName + "\\disPic.txt").Trim());
                    button4.Tag = img2;
                    disposePb.Add(img2);
                    myPictureBox2.Image = Image.FromFile(nextFi.FullName + "\\imgPB\\smallRes.png");
                }
                catch { }
            }
            if (File.ReadAllText(mainDi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img3 = Image.FromFile(File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim());
                    button5.Tag = img3;
                    disposePb.Add(img3);
                }
                catch { }
            }
            globalBtn.BackColor = darkBackColor;
            VideoPlayer_Load(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Text = "Next: " + mainDi.Name; prevPB = null;
            nextFi = mainDi;
            mainDi = prevfi;
            if (disposePb.Count > 0)
            {
                foreach (Image pb in disposePb)
                {
                    pb.Dispose();
                }
            }
            disposePb.Clear();
            button5.Text = mainDi.Name;
            for (int k = 0; k < folders.Count; k++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folders.ElementAt(k));
                if (dirInfo.Name.ToLower().Contains(mainDi.Name.ToLower()))
                {
                    prevfi = new DirectoryInfo(folders.ElementAt(k - 1 < 0 ? folders.Count - 1 : k - 1));
                }
            }
            button3.Text = "Prev: " + prevfi.Name;
            globalBtn.BackColor = darkBackColor; disposePb.Clear();
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());
                    button3.Tag = img1;
                    disposePb.Add(img1);

                    myPictureBox1.Image = Image.FromFile(prevfi.FullName + "\\imgPB\\smallRes.png");
                }
                catch { }
            }
            if (File.ReadAllText(nextFi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img2 = Image.FromFile(File.ReadAllText(nextFi.FullName + "\\disPic.txt").Trim());
                    button4.Tag = img2;
                    disposePb.Add(img2);
                    myPictureBox2.Image = Image.FromFile(nextFi.FullName + "\\imgPB\\smallRes.png");
                }
                catch { }
            }
            if (File.ReadAllText(mainDi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img3 = Image.FromFile(File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim());
                    button5.Tag = img3;
                    disposePb.Add(img3);
                }
                catch { }
            }
            VideoPlayer_Load(null, null);
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            if (miniVideoPlayer != null)
                miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);

        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            sortBySize = true;
            sortByDate = false;
            bySort = true;
            if (type == "Pictures")
                picturesBtn_Click(null, null);
            else if (type == "4K")
                fourKBtn_Click(null, null);
            else if (type == " Short Videos")
                shortVideosBtn_Click(null, null);
            else if (type == "Gifs")
                gifsBtn_Click(null, null);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            FileInfo fi = new FileInfo(pb.Name);
            long si = fi.Length / (1000 * 1000);
            double sid = (Double)si / 1000.0;
            String siz = sid + " GB";
            if (si < 1000)
            {
                siz = si + " MB";
            }

            MessageBox.Show("Size of the file - " + siz, "Size Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            FileInfo fi1 = new FileInfo(pb.Name);
            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                stackDi = new DirectoryInfo(mainDi.FullName + "\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    if (fi.Name.Contains(fi1.Name.Substring(0, fi1.Name.LastIndexOf('.'))))
                        try { fi.Delete(); } catch { }
                noOfStackImg = 0;
            }
        }

        private void loadAll4KImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalBtn.Text.Equals("4K"))
            {
                loadAll = !loadAll;
            }
            else
                return;
            fourKBtn_Click(null, null);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

            FileInfo fi = null;
            if (globalBtn.Text.Equals("Videos"))
            {
                fi = new FileInfo(mainDi.FullName);
            }

            try { if (fi != null) { File.Delete(fi.FullName + "\\priority.txt"); videosBtn_Click(null, null); } } catch { }
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            FileInfo fi = new FileInfo(pb.Name);

            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Thread convert = new Thread(() =>
            {
                try
                {
                    var inputFile = new MediaFile { Filename = fi.FullName };
                    var outputFile = new MediaFile { Filename = fi.DirectoryName + "\\1" + fi.Name.Substring(0, fi.Name.LastIndexOf('.')) + ".mp4" };

                    using (var eng = new Engine())
                    {
                        eng.Convert(inputFile, outputFile);
                    }
                }
                catch
                {
                    MessageBox.Show("Unable to convert!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                });
                convert.Start();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
            String url = pb.Name;
            FileInfo fi = new FileInfo(url);
            DirectoryInfo di = new DirectoryInfo(fi.DirectoryName + "\\imgPB");
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                try
                {
                    File.Delete(url);
                    foreach (FileInfo fi1 in di.GetFiles())
                    {
                        if (fi1.Name.Contains(fi.Name.Substring(0, fi.Name.LastIndexOf("."))))
                            try { fi1.Delete(); refreshFolder(); } catch { }
                    }
                }
                catch { }
        }

        public void exitForm()
        {
            Controls.Clear();
            controlDisposer();
            this.Dispose();
            GC.Collect();
            this.Close();
        }

        private void createImgStack(FileInfo fi, params int[] noOfFrames)
        {
            int numberOfFrames = noOfFrames.Length > 0 ? noOfFrames[0] : 25;
            System.IO.DirectoryInfo diImgPb = new DirectoryInfo(fi.DirectoryName + "\\imgPB");

            WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
            IWMPMedia mediainfo = wmp.newMedia(fi.FullName);
            Double duration = mediainfo.duration;
            double block = duration / numberOfFrames;

            for (int i = 0; i < numberOfFrames; i++)
            {
                Boolean contains = false;
                foreach (FileInfo fiImgPb in diImgPb.GetFiles())
                    if (fiImgPb.Name.Contains(fi.Name.Substring(0, fi.Name.LastIndexOf('.')) + "_" + i))
                    {
                        return;
                    }

                if (!contains)
                {
                    String fileName = fi.Name.Substring(0, fi.Name.LastIndexOf('.')) + "_" + i;
                    var inputFile = new MediaFile { Filename = fi.FullName };
                    var outputFile = new MediaFile { Filename = diImgPb.FullName + "\\" + fileName + ".jpg" };

                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);
                        double secSpan = (i + 1) * block;
                        secSpan = secSpan > duration ? duration - 2 : secSpan;
                        var options = new ConversionOptions { Seek = (TimeSpan.FromSeconds((int)secSpan)) };
                        try
                        {
                            engine.GetThumbnail(inputFile, outputFile, options);
                        }
                        catch { }
                    }
                }
            }
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        private Image setDefaultPic(FileInfo fi, params PictureBox[] picBox)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(fi.DirectoryName + "\\imgPB");

            String fileName = fi.Name;
            if (!File.Exists(fi.FullName))
                return null;

            if (File.Exists(di.FullName + "\\resized_" + fi.Name + ".jpg"))
            {
                picBox[0].ImageLocation = di.FullName + "\\resized_" + fi.Name + ".jpg";
                return Image.FromFile(di.FullName + "\\resized_" + fi.Name + ".jpg");
            }

            foreach (FileInfo file in di.GetFiles())
                if (file.Name.Contains(fi.Name + ".jpg"))
                {
                    if (file.Name.Contains("resized"))
                    {
                        if (picBox.Length > 0)
                            picBox[0].ImageLocation = file.FullName;
                        if (File.Exists(file.FullName))
                            return Image.FromFile(file.FullName);
                        else
                            return null;
                    }
                    else
                    {
                        if (type == " Short Videos")
                            ResizeImage(file.FullName, di.FullName + "\\resized_" + fileName + ".jpg", 0, 228, 402, 0);
                        else
                            ResizeImage(file.FullName, di.FullName + "\\resized_" + fileName + ".jpg", 0, 292, 515, 0);
                        try
                        {
                            File.Delete(file.FullName);
                        }
                        catch { }
                        if (picBox.Length > 0)
                            picBox[0].ImageLocation = di.FullName + "\\resized_" + fileName + ".jpg";
                        return Image.FromFile(di.FullName + "\\resized_" + fileName + ".jpg");
                    }
                }

            if (!File.Exists(fi.FullName))
                return null;
            var inputFile = new MediaFile { Filename = fi.FullName };
            var outputFile = new MediaFile { Filename = di.FullName + "\\" + fileName + ".jpg" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                int sec = (int)inputFile.Metadata.Duration.TotalSeconds / 6;
                if (sec < 1)
                    sec = 0;
                sec = 3 * sec;
                Image img = null;
                var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(sec) };
                engine.GetThumbnail(inputFile, outputFile, options);
                try
                {
                    img = Image.FromFile(di.FullName + "\\" + fileName + ".jpg");

                    if (picBox.Length > 0)
                        picBox[0].ImageLocation = di.FullName + "\\" + fileName + ".jpg";
                }
                catch { }

                return img;
            }
        }

        private void VideoPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.RemoveMessageFilter(this);
            String s = "";
            foreach (String Url in timeSpan.Keys)
            {
                s = s + Url + "|" + timeSpan[Url] + "\n";
            }
            try
            {
                File.WriteAllText(directoryPath + "\\timeSpan.txt", s);
            }
            catch { }

            GC.Collect();
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close")) { }
            else
            {
                Application.Exit();
            }
        }


        private const UInt32 WM_KEYDOWN = 0x0100;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;
                checkBox.Hide();
                if (Control.ModifierKeys == Keys.Shift)
                {
                    button2_Click(null, null);
                }
                else if (Control.ModifierKeys == Keys.Control)
                {
                    if (mutltiSelect)
                    {
                        foreach (PictureBox pb in deletePb)
                        {
                            pb.BackColor = lightBackColor;
                        }
                        if (prevPB != null)
                        {
                            prevPB.BackColor = selectedPbColor;
                            deletePb.Add(prevPB);
                        }
                    }
                    mutltiSelect = !mutltiSelect;
                    button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
                }
                else if (ctrl && keyCode == Keys.Up)
                {
                    int idx = typeButtons.IndexOf(globalBtn);
                    globalBtn.BackColor = lightBackColor;
                    idx = idx - 1 < 0 ? typeButtons.Count - 1 : idx - 1;
                    type = typeButtons.ElementAt(idx).Text;
                    VideoPlayer_Load(null, null);
                }
                else if (ctrl && keyCode == Keys.Down)
                {
                    int idx = typeButtons.IndexOf(globalBtn);
                    globalBtn.BackColor = lightBackColor;
                    idx = idx + 1 >= typeButtons.Count ? 0 : idx + 1;
                    type = typeButtons.ElementAt(idx).Text;
                    VideoPlayer_Load(null, null);
                }
                else if (ctrl && keyCode == Keys.Left)
                {
                    button3_Click(null, null);
                }
                else if (ctrl && keyCode == Keys.Right)
                {
                    button4_Click(null, null);
                }
                else if (keyCode == Keys.Left)
                {
                        if (prevPB == null)
                            setInitPb(0);
                        else
                            setNextPic("Left");

                }
                else if (keyCode == Keys.Right)
                {
                        if (prevPB == null)
                            setInitPb(0);
                        else
                            setNextPic("Right");
                }
                else if (keyCode == Keys.Up)
                {
                    if (prevPB == null)
                        setInitPb(0);
                    else
                        setNextPic("Up");
                }
                else if (keyCode == Keys.Down)
                {
                    if (prevPB == null)
                        setInitPb(0);
                    else
                        setNextPic("Down");
                }
                else if (keyCode == Keys.Enter)
                {
                    enter = true;
                    if (prevPB != null)
                        pbClick(prevPB);
                }
                else if (keyCode == Keys.Escape || keyCode == Keys.Back)
                {

                    expBtn_Click(null, null);
                }
                else if (keyCode == Keys.Add)
                {
                    trackBar1.Value = trackBar1.Value + 2 > 100 ? 100 : trackBar1.Value + 2;
                    Explorer.globalVol = trackBar1.Value;
                    button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
                }
                else if (keyCode == Keys.Subtract)
                {
                    trackBar1.Value = trackBar1.Value - 2 < 0 ? 0 : trackBar1.Value - 2;
                    Explorer.globalVol = trackBar1.Value;
                    button1.Text = "Global Vol: " + trackBar1.Value.ToString() + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
                }
                else if (keyCode == Keys.M)
                {
                    trackBar1.Value = 0;
                    Explorer.globalVol = trackBar1.Value;
                    button1.Text = "Global Vol: " + 0 + "\nMultselect: " + (mutltiSelect == true ? "On" : "Off");
                }
                else if (keyCode == Keys.NumPad1 || keyCode == Keys.NumPad2 || keyCode == Keys.NumPad3 || keyCode == Keys.NumPad4 || keyCode == Keys.NumPad5)
                {
                    if (keyCode == Keys.NumPad1)
                    {
                        int idx = 0;
                        globalBtn.BackColor = lightBackColor;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad2)
                    {
                        int idx = 1;
                        globalBtn.BackColor = lightBackColor;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad3)
                    {
                        int idx = 2;
                        globalBtn.BackColor = lightBackColor;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad4)
                    {
                        int idx = 3;
                        globalBtn.BackColor = lightBackColor;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad5)
                    {
                        int idx = 4;
                        globalBtn.BackColor = lightBackColor;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                }

                return true;
            }
            return false;
        }

        /*public void setInitPb(int toCross)
        {
            PictureBox tempPb = null;
            Point relativeLoc = new Point(0, 0);
        Point controlLoc = new Point(0, 0);
            if (videosPb.Count > 0)
            {
                int idx = 0;
                int y = 0; Point controlLoc = new Point(0, 0);
                checkBox.Show();
                if (prevPB == null)
                {
                    tempPb = videosPb.ElementAt(0);
                }
                else
                {
                    idx = toCross > 0 ? (videosPb.IndexOf(prevPB) + toCross >= videosPb.Count ? 0 : videosPb.IndexOf(prevPB) + toCross) :
                        (videosPb.IndexOf(prevPB) + toCross < 0 ? gpb.Count() - 1 : videosPb.IndexOf(prevPB) + toCross);
                    tempPb = videosPb.ElementAt(idx);
                }
                prevPB = tempPb;

                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(tempPb.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, 34);
                    checkBox.Location = relativeLoc;
                    ind++;
                }
                else
                {
                    controlLoc = this.PointToScreen(tempPb.Location);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, controlLoc.Y - this.Location.Y - 7);
                    checkBox.Location = relativeLoc;
                }
            }
            else if (gpb.Count > 0)
            {
                int idx = 0;
                int y = 0; Point controlLoc = new Point(0,0);
                checkBox.Show();
                if (prevPB == null)
                {
                    tempPb = gpb.ElementAt(0);
                }
                else
                {
                    idx = toCross > 0 ? (gpb.IndexOf(prevPB) + toCross >= gpb.Count ? 0 : gpb.IndexOf(prevPB) + toCross) :
                        (gpb.IndexOf(prevPB) + toCross <0 ? gpb.Count() - 1 : gpb.IndexOf(prevPB) + toCross);
                    tempPb = gpb.ElementAt(idx);
                }
                prevPB = tempPb;

                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(tempPb.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, 34);
                    checkBox.Location = relativeLoc;
                    ind++;
                }
                else
                {
                    controlLoc = this.PointToScreen(tempPb.Location);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, controlLoc.Y - this.Location.Y - 7);
                    checkBox.Location = relativeLoc;
                }

            }
            else if (shortVideosPb.Count > 0)
            {
                int idx = 0;
                int y = 0; Point controlLoc = new Point(0, 0);
                checkBox.Show();
                if (prevPB == null)
                {
                    tempPb = shortVideosPb.ElementAt(0);
                }
                else
                {
                    idx = toCross > 0 ? (shortVideosPb.IndexOf(prevPB) + toCross >= shortVideosPb.Count ? 0 : shortVideosPb.IndexOf(prevPB) + toCross) :
                        (shortVideosPb.IndexOf(prevPB) + toCross < 0 ? shortVideosPb.Count() - 1 : shortVideosPb.IndexOf(prevPB) + toCross);
                    tempPb = shortVideosPb.ElementAt(idx);
                }
                prevPB = tempPb;

                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(tempPb.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, 34);
                    checkBox.Location = relativeLoc;
                    ind++;
                }
                else
                {
                    controlLoc = this.PointToScreen(tempPb.Location);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, controlLoc.Y - this.Location.Y - 7);
                    checkBox.Location = relativeLoc;
                }
            }
            else if (gifsPb.Count > 0)
            {
                int idx = 0;
                int y = 0; Point controlLoc = new Point(0, 0);
                checkBox.Show();
                if (prevPB == null)
                {
                    tempPb = gifsPb.ElementAt(0);
                }
                else
                {
                    idx = toCross > 0 ? (gifsPb.IndexOf(prevPB) + toCross >= gifsPb.Count ? 0 : gifsPb.IndexOf(prevPB) + toCross) :
                        (gifsPb.IndexOf(prevPB) + toCross < 0 ? gifsPb.Count() - 1 : gifsPb.IndexOf(prevPB) + toCross);
                    tempPb = gifsPb.ElementAt(idx);
                }
                prevPB = tempPb;

                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(tempPb.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, 34);
                    checkBox.Location = relativeLoc;
                    ind++;
                }
                else
                {
                    controlLoc = this.PointToScreen(tempPb.Location);
                    relativeLoc = new Point(controlLoc.X - this.Location.X + 207, controlLoc.Y - this.Location.Y - 7);
                    checkBox.Location = relativeLoc;
                }
            }
        }*/

        public void setInitPb(int idx)
        {
            PictureBox tempPb = null;
            Point relativeLoc = new Point(0, 0);
            Point controlLoc = new Point(0, 0);
            if (videosPb.Count > 0)
            {
                if (prevPB == null)
                {
                    globalDetails = allVidDet.ElementAt(0);
                    prevPB = videosPb.ElementAt(0);
                    prevPB.BackColor = mouseClickColor;
                    allVidDet.ElementAt(0).ForeColor = mouseClickColor;
                    Font myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    return;
                }
                prevPB.BackColor = lightBackColor;
                int index = videosPb.IndexOf(prevPB) + idx;
                if (index < 0)
                {
                    globalDetails.ForeColor = Color.White;
                    Font myfont = new Font("Consolas", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(videosPb.Count - 1);
                    prevPB = videosPb.ElementAt(videosPb.Count - 1);
                    allVidDet.ElementAt(videosPb.Count - 1).ForeColor = mouseClickColor;
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    index = videosPb.Count - 1;
                }
                else if (index >= videosPb.Count)
                {
                    globalDetails.ForeColor = Color.White;
                    Font myfont = new Font("Consolas", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(0);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    prevPB = videosPb.ElementAt(0);
                    allVidDet.ElementAt(0).ForeColor = mouseClickColor;
                    index = 0;
                }
                else
                {
                    globalDetails.ForeColor = Color.White;
                    Font myfont = new Font("Consolas", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(index);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    prevPB = videosPb.ElementAt(index);
                    allVidDet.ElementAt(index).ForeColor = mouseClickColor;
                }

                prevPB.BackColor = mouseClickColor;
                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000 || prevPB.Location.Y < 0)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                    controlLoc = this.PointToScreen(prevPB.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34 < 0 ? 0 : controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34 < 0 ? 0 : controlLoc.Y - 34);
                }
                if ((index == 0))
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                }
            }
            else if (gpb.Count > 0)
            {
                if (prevPB == null)
                {
                    prevPB = gpb.ElementAt(0);
                    prevPB.BackColor = mouseClickColor;
                    return;
                }
                prevPB.BackColor = lightBackColor;
                int index = gpb.IndexOf(prevPB);
                if (!(type == "4K"))
                    index = index + (idx > 1 ? (lpb.Contains(prevPB) ? 7 : (wpb.Contains(prevPB) ? 5 : idx)) : (idx < -1 ? (lpb.Contains(prevPB) ? -7 : (wpb.Contains(prevPB) ? -5 : idx)) : idx));
                else
                    index = index + (idx > 1 ? (lpbDupe.Contains(prevPB) ? 7 : (wpbDupe.Contains(prevPB) ? 5 : idx)) : (idx < -1 ? (lpbDupe.Contains(prevPB) ? -7 : (wpbDupe.Contains(prevPB) ? -5 : idx)) : idx));

                if (index < 0)
                {
                    index = gpb.Count - 1;
                    prevPB = gpb.ElementAt(gpb.Count - 1);
                }
                else if (index >= gpb.Count)
                {
                    index = 0;
                    prevPB = gpb.ElementAt(0);
                }
                else
                    prevPB = gpb.ElementAt(index);

                prevPB.BackColor = mouseClickColor;

                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000 || prevPB.Location.Y < 0)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(prevPB.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34 < 0 ? 0 : controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34 < 0 ? 0 : controlLoc.Y - 34);
                }
                if ((index == 0))
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                }
            }
            else if (shortVideosPb.Count > 0)
            {
                if (prevPB == null)
                {
                    globalDetails = allVidDet.ElementAt(0);
                    allVidDet.ElementAt(0).ForeColor = mouseClickColor;
                    prevPB = shortVideosPb.ElementAt(0);
                    prevPB.BackColor = mouseClickColor;
                    Font myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    return;
                }
                prevPB.BackColor = lightBackColor;
                int index = shortVideosPb.IndexOf(prevPB) + idx;
                if (index < 0)
                {
                    globalDetails.ForeColor = Color.White;

                    Font myfont = new Font("Consolas", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    index = shortVideosPb.Count - 1;
                    prevPB = shortVideosPb.ElementAt(shortVideosPb.Count - 1);
                    globalDetails = allVidDet.ElementAt(shortVideosPb.Count - 1);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    allVidDet.ElementAt(shortVideosPb.Count - 1).ForeColor = mouseClickColor;
                }
                else if (index >= shortVideosPb.Count)
                {
                    index = 0;
                    prevPB = shortVideosPb.ElementAt(0);
                    globalDetails.ForeColor = Color.White;
                    Font myfont = new Font("Consolas", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(index);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    allVidDet.ElementAt(index).ForeColor = mouseClickColor;
                }
                else
                {
                    prevPB = shortVideosPb.ElementAt(index);
                    globalDetails.ForeColor = Color.White;
                    Font myfont = new Font("Consolas", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(index);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    allVidDet.ElementAt(index).ForeColor = mouseClickColor;
                }

                prevPB.BackColor = mouseClickColor;
                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000 || prevPB.Location.Y < 0)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(prevPB.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34 < 0 ? 0 : controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34 < 0 ? 0 : controlLoc.Y - 34);
                }
                if ((index == 0))
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                }
            }
            else if (gifsPb.Count > 0)
            {
                if (prevPB == null)
                {
                    prevPB = gifsPb.ElementAt(0);
                    prevPB.BackColor = mouseClickColor;
                    return;
                }
                prevPB.BackColor = lightBackColor;
                int index = gifsPb.IndexOf(prevPB) + idx;
                if (index < 0)
                {
                    index = gifsPb.Count;
                    prevPB = gifsPb.ElementAt(gifsPb.Count - 1);
                }
                else if (index >= videosPb.Count)
                {
                    index = 0;
                    prevPB = gifsPb.ElementAt(0);
                }
                else
                    prevPB = gifsPb.ElementAt(index);

                prevPB.BackColor = mouseClickColor;

                int mod = prevPB.Location.Y + prevPB.Height;
                if (mod > 1000 || prevPB.Location.Y < 0)
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                    controlLoc = this.PointToScreen(prevPB.Location);
                    flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - 34;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - 34);
                }
                if ((index == 0))
                {
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                }
            }
        }

        public void setNextPic(String dir)
        {
            if (dir == "Right")
            {
                setInitPb(1);
            }
            else if (dir == "Left")
            {
                setInitPb(-1);
            }
            else if (dir == "Up")
            {
                setInitPb(-3);
            }
            else if (dir == "Down")
            {
                setInitPb(3);
            }
        }

    }
}