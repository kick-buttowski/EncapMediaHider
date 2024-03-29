﻿using AxWMPLib;
using MediaPlayer;
using Calculator.Properties;
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
using System.Net;
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

        public Boolean enter = false, ctrl = false, scrollZero = true, hoveredOver = true, hoveredOver2 = true, toggleFullScreen = true,
            playStatus = true, toMute = true, toRepeat = false, loadAll = false, enlargeImage = false, isFirst = true, isRefresh = false, stackPics = false;
        public Dictionary<String, Boolean> isEnlarged = new Dictionary<string, bool>(), tipForFirstTime = new Dictionary<string, bool>();
        String directoryPath;
        public static Boolean toRefresh = false, toChangeTheme = false; 
        public static bool isChecked = false, isGlobalChecked = false, isShort = false, isHoveredOverPb = false;
        WindowsMediaPlayer wmpDura = new WindowsMediaPlayerClass();
        public static String imagePBLink = "";
        List<Button> typeButtons = new List<Button>();
        Dictionary<String, Double> timeSpan = new Dictionary<String, Double>();
        List<Image> videosImg = new List<Image>();
        public static List<String> videoUrls = new List<String>();
        public static Explorer exp;
        int randNo = 0;
        public Label globalDetails = new Label();
        Double zoom = 1.0, loc = 0, prevX = -12, inWmpDuration = 0, whereAtMp = 1.0;
        public static Point relativeLoc;
        public static PictureBox prevPB = null;
        PictureBox globalPb = new PictureBox(); PictureBox pbb = new PictureBox();
        List<Label> allVidDet = new List<Label>();
        Boolean mutltiSelect = false, sortBySize = false, sortByDate = true, bySort = false, toSort = false,
            sortBySizeOrderDesc = false, sortByDateOrderDesc = false;
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
        Dictionary<String, Image> miniImages = new Dictionary<String, Image>(), largeImages = new Dictionary<string, Image>();
        DirectoryInfo _4kdir = null;
        Button globalBtn = null;
        int ind = 1, whereAt = 0, noOfStackImg = 25, vertScrollValue = 0, noOfTimerRuns = 0;
        String type = "Videos";
        List<PictureBox> unListed = new List<PictureBox>();
        WMP wmp = null;
        TranspBack transpBack = null;
        Double durationDupe = 0, block = 0;
        List<PictureBox> deletePb = new List<PictureBox>();
        //public static miniVideoPlayer miniVideoPlayer = null;
        DirectoryInfo prevfi = null, nextFi = null;
        Engine engine = new Engine();
        int popUpY = 270;
        int popUpX = 320;
        Image img3 = null;
        Image img4 = null;
        public static Double popUpVideoWidth = 0, popUpVideoHeight = 0;
        public static int stepWise = 15;
        NewProgressBar newProgressBar = null, newProgressBarNew = null;
        checkBox checkBox = new checkBox();
        FlowLayoutPanel prevFlowLayoutPanel = null;
        Label prevVidDetails = null;
        PictureBox prevPb = null;
        static Random rr = new Random();
        //static int rand1 = Explorer.rr.Next(0, 256);
        //static int[] color = Explorer.Rand_Color(rand1, 0.5, 0.25);
        static Color globColor = Color.FromArgb(rr.Next(256), rr.Next(256), rr.Next(256));
        public Color darkBackColor = Explorer.darkBackColor;
        Color lightBackColor = Explorer.lightBackColor;
        Color kindaDark = Explorer.kindaDark;
        Color mouseHoverColor = Explorer.globColor;
        Color mouseClickColor = Explorer.globColor;
        Color selectedPbColor = Explorer.globColor;
        List<String> folders = null;
        DirectoryInfo playListDi = null;
        CustomToolTip tip = null, tip1 = null;

        public VideoPlayer(String directoryPath, Explorer exp, String type, DirectoryInfo prevFi, DirectoryInfo nextFi, List<String> folders)
        {
            mouseHoverColor = Explorer.globColor;
            mouseClickColor = Explorer.globColor;
            selectedPbColor = Explorer.globColor;
            InitializeComponent();
            if(Explorer.wmpOnTop == null)
            {
                Explorer.wmpOnTop = new WmpOnTop(this);
            }
            this.type = type;
            VideoPlayer.exp = exp;
            this.directoryPath = directoryPath;
            this.DoubleBuffered = true;
            Application.AddMessageFilter(this);
            checkBox.Size = new Size(50, 50);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            picturesBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picturesBtn.Width, picturesBtn.Height, 25, 25));
            videosBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, videosBtn.Width, videosBtn.Height, 25, 25));
            expBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, expBtn.Width, expBtn.Height, 25, 25));
            shortVideosBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picturesBtn.Width, picturesBtn.Height, 25, 25));
            fourKBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, fourKBtn.Width, fourKBtn.Height, 25, 25));
            gifsBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, gifsBtn.Width, gifsBtn.Height, 25, 25));
            bsButton.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, bsButton.Width, bsButton.Height, 25, 25));
            button3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 5, 5));
            button4.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button4.Width, button4.Height, 5, 5));
            button5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button5.Width, button5.Height, 5, 5));

            move.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, move.Width, move.Height, 5, 5));
            addFile.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, addFile.Width, addFile.Height, 5, 5));
            theme.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, theme.Width, theme.Height, 5, 5));
            refresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, refresh.Width, refresh.Height, 5, 5));
            multisel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, multisel.Width, multisel.Height, 5, 5));
            delete.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, delete.Width, delete.Height, 5, 5));
            sortDate.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, sortDate.Width, sortDate.Height, 5, 5));
            sortSize.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, sortSize.Width, sortSize.Height, 5, 5));
            convert.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, convert.Width, convert.Height, 5, 5));
            load4K.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, load4K.Width, load4K.Height, 5, 5));
            reset.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, reset.Width, reset.Height, 5, 5));
            navController.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, navController.Width, navController.Height, 5, 5));
            rcGifs.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, rcGifs.Width, rcGifs.Height, 5, 5));
            rcVid.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, rcVid.Width, rcVid.Height, 5, 5));
            rcSVid.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, rcSVid.Width, rcSVid.Height, 5, 5));
            rc4K.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, rc4K.Width, rc4K.Height, 5, 5));
            rcPics.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, rcPics.Width, rcPics.Height, 5, 5));
            rcAffinity.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, rcAffinity.Width, rcAffinity.Height, 5, 5));
            resetAndSet.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, resetAndSet.Width, resetAndSet.Height, 5, 5));
            setDp.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, setDp.Width, setDp.Height, 5, 5));
            edit.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, edit.Width, edit.Height, 5, 5));

            //this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 10, 10));
            //flowLayoutPanel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel2.Width, flowLayoutPanel2.Height, 10, 10));
            //button6.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button6.Width, button6.Height, 10, 10));
            //this.WindowState = FormWindowState.Maximized;
            //textBox1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox1.Width, textBox1.Height, 20, 20));
            trackBar1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, trackBar1.Width, trackBar1.Height, 5, 5));
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 5, 5));
            mainDi = new DirectoryInfo(directoryPath);
            button5.Text = mainDi.Name;
            this.folders = folders;
            this.nextFi = nextFi;
            this.prevfi = prevFi;
            button3.Text = "" + prevFi.Name;
            button4.Text = "" + nextFi.Name;
            if (!Directory.Exists(mainDi + "\\imgPB")) { Directory.CreateDirectory(mainDi + "\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics")) { Directory.CreateDirectory(mainDi + "\\Pics"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "Gifs")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "Gifs"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "Gifs\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "Gifs\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "kkkk")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "kkkk"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "kkkk\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "kkkk\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "GifVideos")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "GifVideos"); }
            if (!Directory.Exists(mainDi + "\\Pics\\" + "GifVideos\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\" + "GifVideos\\imgPB"); }
            if (!Directory.Exists(mainDi + "\\PlayLists")) { Directory.CreateDirectory(mainDi + "\\PlayLists"); }
            playListDi = new DirectoryInfo(mainDi + "\\PlayLists");
            if (!Directory.Exists(mainDi + "\\Pics\\Affinity")) { Directory.CreateDirectory(mainDi + "\\Pics\\Affinity"); }
            if (!Directory.Exists(mainDi + "\\Pics\\Affinity\\imgPB")) { Directory.CreateDirectory(mainDi + "\\Pics\\Affinity\\imgPB"); }

            if (!Directory.Exists(mainDi + "\\Online")) { Directory.CreateDirectory(mainDi + "\\Online"); }

            if (!File.Exists(mainDi + "\\links.txt")) {
                FileStream fi = File.Create(mainDi.FullName + "\\links.txt");
                fi.Close();
            }
            if (!File.Exists(mainDi + "\\resume.txt"))
            {
                FileStream fi = File.Create(mainDi.FullName + "\\resume.txt");
                fi.Close();
            }
            if (!File.Exists(mainDi + "\\webLinks.txt"))
            {
                FileStream fi = File.Create(mainDi.FullName + "\\webLinks.txt");
                fi.Close();
            }
            if (!File.Exists(mainDi + "\\inTypes.txt"))
            {
                FileStream fi = File.Create(mainDi.FullName + "\\inTypes.txt");
                fi.Close();
            }
            trackBar1.Value = Explorer.globalVol;
            typeButtons.Add(videosBtn);
            typeButtons.Add(picturesBtn);
            typeButtons.Add(fourKBtn);
            typeButtons.Add(bsButton);
            typeButtons.Add(shortVideosBtn);
            typeButtons.Add(gifsBtn);
            isEnlarged.Add("Videos", false);
            isEnlarged.Add("Affinity", false);
            isEnlarged.Add("Pictures", false);
            isEnlarged.Add("Gifs", false);
            isEnlarged.Add("Gif Vid", false);
            isEnlarged.Add("4K", false);
            isEnlarged.Add("Stack", false);

            tipForFirstTime.Add("LargeVideos", true);
            tipForFirstTime.Add("LargeAffinity", true);
            tipForFirstTime.Add("LargePictures", true);
            tipForFirstTime.Add("LargeGifs", true);
            tipForFirstTime.Add("LargeGif Vid", true);
            tipForFirstTime.Add("Large4K", true);
            tipForFirstTime.Add("LargeStack", true);
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());
                    button3.Tag = img1;
                    if (img1.Width > img1.Height)
                        tip = new CustomToolTip(401, 260);
                    else
                        tip = new CustomToolTip(266, 400);
                    disposePb.Add(img1);
                    tip.SetToolTip(button3, prevfi.Name);

                    button3.Image = resizedImage(img1, 0, 50, 0, 0);
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
                            tip = new CustomToolTip(266, 400);
                    button4.Tag = img2;
                    disposePb.Add(img2);
                    tip.InitialDelay = 10;
                    tip.AutoPopDelay = 6000;
                    tip.SetToolTip(button4, nextFi.Name);
                    button4.Image = resizedImage(img2, 0, 50, 0, 0);
                }
                catch { }
            }
            if (File.ReadAllText(mainDi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    img3 = Image.FromFile(File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim());
                    if (tip == null)

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
            newProgressBar.Size = new Size(1720, 25);
            newProgressBar.Location = new Point(-2, -3);
            newProgressBar.Value = 0;
            newProgressBar.Margin = new Padding(0);

            newProgressBarNew = new NewProgressBar();
            newProgressBarNew.Value = 0;
            newProgressBarNew.BackColor = Color.White;
            newProgressBarNew.ForeColor = Color.Black;
            newProgressBarNew.Margin = new Padding(0);

            setTheme();
            loadMiniImages();
            enlargeImage = true;

            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.volume = 0;
            axWindowsMediaPlayer1.settings.setMode("loop", true);

            populateContextMenuForLink();
        }

        private void populateContextMenuForLink()
        {
            int id = 0, subId = 0;
            ToolStripMenuItem[] tempStripMenuItem = new ToolStripMenuItem[exp.allFoldersDict.Count];
            this.toolStripMenuItem42.DropDownItems.Clear();
            foreach (String keys in exp.allFoldersDict.Keys)
            {
                tempStripMenuItem[id] = new System.Windows.Forms.ToolStripMenuItem();
                tempStripMenuItem[id].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                tempStripMenuItem[id].Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tempStripMenuItem[id].Name = keys;
                tempStripMenuItem[id].Size = new System.Drawing.Size(239, 24);
                tempStripMenuItem[id].Text = keys.Substring(1);

                ToolStripMenuItem[] subTempStripMenuItem = new ToolStripMenuItem[exp.allFoldersDict[keys].Count];
                subId = 0;
                foreach (String folder in exp.allFoldersDict[keys])
                {
                    subTempStripMenuItem[subId] = new System.Windows.Forms.ToolStripMenuItem();
                    subTempStripMenuItem[subId].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                    subTempStripMenuItem[subId].Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    subTempStripMenuItem[subId].Name = folder;
                    subTempStripMenuItem[subId].Size = new System.Drawing.Size(239, 24);
                    subTempStripMenuItem[subId].Text = folder.Substring(folder.LastIndexOf("\\") + 1);
                    subTempStripMenuItem[subId].MouseDown += (s, args) =>
                    {
                        PictureBox pb = (PictureBox)contextMenuStrip1.SourceControl;
                        File.AppendAllText(folder + "\\links.txt", "\n" + pb.Name);
                    };
                    subTempStripMenuItem[subId].Tag = subId++;

                }

                tempStripMenuItem[id].DropDownItems.AddRange(subTempStripMenuItem);
                tempStripMenuItem[id].Tag = id++;
            }
            this.toolStripMenuItem42.DropDownItems.AddRange(tempStripMenuItem);

        }

        private int correctFit(int width, int height)
        {
            double ratio = (double)width / (double)height;
            if (ratio >= 1.7)
                return enlargeImage?140:123;
            else if (ratio >= 1.60)
                return enlargeImage ? 150 : 133;
            else if (ratio >= 1.50)
                return enlargeImage ? 160 : 143;
            else if (ratio >= 1.40)
                return enlargeImage ? 170 : 153;
            else if (ratio >= 1.30)
                return enlargeImage ? 180 : 163;
            else if (ratio >= 1.20)
                return enlargeImage ? 191 : 173;

            return 133;
        }


        private void setToolTip(String keyName, Image img, Button tipBtn)
        {

                Image img1 = img.Width > img.Height ? resizedImage(img, 0, 0, 0, popUpY) : resizedImage(img, 0, popUpX, 0, 0);
            try
            {

                largeImages.Add(keyName, img1);
            }
            catch { 
            }
                tipBtn.Tag = img1;
            if (tipForFirstTime[keyName])
            {
                int yLoc = tipBtn.Location.Y + (tipBtn.Height / 2) + 28 - (popUpY / 2);
                if (1080 - tipBtn.Location.Y -33 <= (popUpY / 2))
                {
                    yLoc = 1078 - (popUpY);
                    tip = new CustomToolTip(img1.Width, popUpY, tipBtn.Location.X + tipBtn.Size.Width + 25, yLoc);
                }
                else if (tipBtn.Location.Y - (popUpY / 2) < 0)
                {
                    tip = new CustomToolTip(img1.Width, popUpY, tipBtn.Location.X + tipBtn.Size.Width + 15, tipBtn.Location.Y);
                }
                else
                {
                    tip = new CustomToolTip(img1.Width, popUpY, tipBtn.Location.X + tipBtn.Size.Width + 25, yLoc);
                }

                /*yLoc = tipBtn.Location.Y + (tipBtn.Height / 2) - (popUpY / 2);
                if (1080 - tipBtn.Location.Y <= (popUpY / 2))
                {
                    yLoc = yLoc - (popUpY / 2) + 10;
                    tip1 = new CustomToolTip(img1.Width, popUpX, tipBtn.Location.X + tipBtn.Size.Width + 25, yLoc);
                }
                else if (tipBtn.Location.Y - (popUpY / 2) < 0)
                {
                    tip1 = new CustomToolTip(img1.Width, popUpX, tipBtn.Location.X + tipBtn.Size.Width + 15, tipBtn.Location.Y);
                }
                else
                {
                    tip1 = new CustomToolTip(img1.Width, popUpX, tipBtn.Location.X + tipBtn.Size.Width + 15, yLoc);
                }
                */
                tip.SetToolTip(tipBtn, tipBtn.Text);
                tipForFirstTime[keyName] = false;
                
                tip.InitialDelay = 100;
                tip.AutoPopDelay = 6000;
            }

        }

        private void loadMiniImages()
        {
            foreach (Image img in miniImages.Values)
            {
                img.Dispose();
            }
            foreach (Image img in largeImages.Values)
            {
                img.Dispose();
            }
            foreach (Button str in typeButtons)
                isEnlarged[str.Text] = false;
            if (img4 != null)
                img4.Dispose();
            miniImages.Clear();
            largeImages.Clear();
            List<FileInfo> sfi = mainDi.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            Random r = new Random();
            String fileName = ".txt";
            int[] res = new int[] { 1080, 1920 };
            int pick = 0,i=0;

            if (img4 == null)
            {
                String fName = File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim();
                if(fName!="" && File.Exists(fName))
                    img4 = Image.FromFile(fName);
            }

            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt"))
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Extension;
                    if (fileName.EndsWith(".txt"))
                        sfi.RemoveAt(pick);
                }
                Image img = setDefaultPic(sfi[pick]);
                enlargeImage = false;
                if (img != null)
                {
                    miniImages.Add("Videos", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    videosBtn.Image = miniImages["Videos"];
                    enlargeImage = true;
                    largeImages.Add("Videos", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    try
                    {
                        setToolTip("LargeVideos", img, videosBtn);
                    }
                    catch { }

                    img.Dispose();
                }
            }
            else
            {
                videosBtn.Image = null;
            }


            /*DirectoryInfo prevDir = new DirectoryInfo(prevfi.FullName);
            sfi = prevDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            fileName = ".txt";
            pick = 0;
            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt"))
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Extension;
                    if (fileName.EndsWith(".txt"))
                        sfi.RemoveAt(pick);
                }
                Image img = setDefaultPic(sfi[pick]);
                miniImages.Add("Prev", resizedImage(img, 0, 0, 0, 90));
                button3.Image = miniImages["Prev"];

                img.Dispose();
            }
            else
                button3.Image = global::MediaPlayer.Properties.Resources.left_right__2_;


            prevDir = new DirectoryInfo(nextFi.FullName);
            sfi = prevDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            fileName = ".txt";
            pick = 0;
            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt"))
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Extension;
                    if (fileName.EndsWith(".txt"))
                        sfi.RemoveAt(pick);
                }
                Image img = setDefaultPic(sfi[pick]);
                miniImages.Add("Next", resizedImage(img, 0, 0, 0, 90));
                button4.Image = miniImages["Next"];
                img.Dispose();
            }
            else
                button4.Image = global::MediaPlayer.Properties.Resources.left_right__2_;*/


            DirectoryInfo picsDir = new DirectoryInfo(mainDi.FullName + "\\Pics");
            sfi = picsDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            /*            SkipWhile(k => int.TryParse(k.Name.Substring(k.Name.IndexOf("^^") + 2).Substring(0, k.Name.IndexOf("placeholdderr")).Split('x')[0].ToString(), )
                        > int.Parse(k.Name.Substring(k.Name.IndexOf("^^") + 2).Substring(0, k.Name.IndexOf("placeholdderr")).Split('x')[1].ToString())).ToList();*/
            fileName = ".txt";
            pick = 0;

            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt") || res[1] > res[0])
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Name;

                    if (fileName.EndsWith(".txt")) continue;
                    Image imggg = Image.FromFile(sfi[pick].FullName);
                    res[0] = imggg.Width;
                    res[1] = imggg.Height;
                    imggg.Dispose();
                    if (fileName.EndsWith(".txt") || res[1] > res[0])
                    {
                        sfi.RemoveAt(pick);
                        if (sfi.Count == 0)
                            break;
                    }
                }

                if (res[1] < res[0])
                {
                    Image img = Image.FromFile(sfi[pick].FullName);

                    enlargeImage = false;
                    miniImages.Add("Pictures", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    picturesBtn.Image = miniImages["Pictures"];
                    enlargeImage = true;
                    largeImages.Add("Pictures", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    try
                    {
                        setToolTip("LargePictures", img, picturesBtn);

                    }
                    catch { }
                    img.Dispose();
                }
                else
                {
                    picturesBtn.Image = null;
                }

            }

            else
            {
                picturesBtn.Image = null;
            }


            DirectoryInfo fourkDir = new DirectoryInfo(mainDi.FullName + "\\Pics\\kkkk");
            res = new int[] { 1080, 1920 };
            sfi = fourkDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            /*            SkipWhile(k => int.TryParse(k.Name.Substring(k.Name.IndexOf("^^") + 2).Substring(0, k.Name.IndexOf("placeholdderr")).Split('x')[0].ToString(), )
                        > int.Parse(k.Name.Substring(k.Name.IndexOf("^^") + 2).Substring(0, k.Name.IndexOf("placeholdderr")).Split('x')[1].ToString())).ToList();*/
            fileName = ".txt";
            pick = 0;

            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt") || res[1] > res[0])
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Name;

                    if (fileName.EndsWith(".txt") || !fileName.Contains("placeholdderr")) continue;
                    String temp = fileName.Substring(fileName.IndexOf("^^") + 2);
                    temp = temp.Substring(0, temp.IndexOf("placeholdderr"));
                    res[0] = int.Parse(temp.Split('x')[0]);
                    res[1] = int.Parse(temp.Split('x')[1]);

                    if (fileName.EndsWith(".txt") || res[1] > res[0])
                    {
                        sfi.RemoveAt(pick);
                        if (sfi.Count == 0)
                            break;
                    }
                }

                if (res[1] < res[0])
                {
                    Image img = Image.FromFile(sfi[pick].FullName);
                    enlargeImage = false;
                    miniImages.Add("4K", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                        fourKBtn.Image = miniImages["4K"];
                    enlargeImage = true;
                    largeImages.Add("4K", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    try { 
                    setToolTip("Large4K", img, fourKBtn);

                    }
                    catch { }
                    img.Dispose();
                }
                else
                {
                        fourKBtn.Image = null;
                }

            }
            else
            {
                    fourKBtn.Image = null;
            }


            DirectoryInfo gifVid = new DirectoryInfo(mainDi.FullName + "\\Pics\\GifVideos");
            sfi = gifVid.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            fileName = ".txt";
            pick = 0; i = 0;
            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt"))
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Extension;
                    if (fileName.EndsWith(".txt"))
                        sfi.RemoveAt(pick);
                }
                Image img = setDefaultPic(sfi[pick]);
                if (img != null)
                {
                    enlargeImage = false;
                    miniImages.Add("Gif Vid", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    shortVideosBtn.Image = miniImages["Gif Vid"];
                    enlargeImage = true;
                    largeImages.Add("Gif Vid", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    try
                    {
                        setToolTip("LargeGif Vid", img, shortVideosBtn);

                    }
                    catch { }
                    img.Dispose();
                }
            }
            else
            {
                    shortVideosBtn.Image = null;
            }



            DirectoryInfo affinityVid = new DirectoryInfo(mainDi.FullName + "\\Pics\\Affinity");
            sfi = affinityVid.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            fileName = ".txt";
            pick = 0; i = 0;
            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt"))
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Extension;
                    if (fileName.EndsWith(".txt"))
                        sfi.RemoveAt(pick);
                }
                Image img = setDefaultPic(sfi[pick]);
                if (img != null)
                {
                    enlargeImage = false;
                    miniImages.Add("Affinity", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    bsButton.Image = miniImages["Affinity"];
                    enlargeImage = true;
                    largeImages.Add("Affinity", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    try
                    {
                        setToolTip("LargeAffinity", img, bsButton);

                    }
                    catch { }
                    img.Dispose();
                }
            }
            else
            {
                bsButton.Image = null;
            }


            DirectoryInfo gifsDir = new DirectoryInfo(mainDi.FullName + "\\Pics\\Gifs");
            res = new int[] { 1080, 1920 };
            sfi = gifsDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
            /*            SkipWhile(k => int.TryParse(k.Name.Substring(k.Name.IndexOf("^^") + 2).Substring(0, k.Name.IndexOf("placeholdderr")).Split('x')[0].ToString(), )
                        > int.Parse(k.Name.Substring(k.Name.IndexOf("^^") + 2).Substring(0, k.Name.IndexOf("placeholdderr")).Split('x')[1].ToString())).ToList();*/
            fileName = ".txt";
            pick = 0;

            if (sfi.Count > 0)
            {
                while (fileName.EndsWith(".txt") || res[1] > res[0])
                {
                    pick = r.Next(0, sfi.Count);
                    fileName = sfi[pick].Name;

                    if (fileName.EndsWith(".txt")) continue;
                    Image imggg = Image.FromFile(sfi[pick].FullName);
                    res[0] = imggg.Width;
                    res[1] = imggg.Height;
                    imggg.Dispose();
                    if (fileName.EndsWith(".txt") || res[1] > res[0])
                    {
                        sfi.RemoveAt(pick);
                        if (sfi.Count == 0)
                            break;
                    }
                }

                if (res[1] < res[0])
                {
                    Image img = Image.FromFile(sfi[pick].FullName);

                    enlargeImage = false;
                    miniImages.Add("Gifs", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    gifsBtn.Image = miniImages["Gifs"];
                    enlargeImage = true;
                    largeImages.Add("Gifs", resizedImage(img, 0, 0, 0, correctFit(img.Width, img.Height)));
                    try
                    {
                        setToolTip("LargeGifs", img, gifsBtn);

                    }
                    catch { }
                    img.Dispose();
                }
                else
                {
                    gifsBtn.Image = null;
                }

            }

            else
            {
                gifsBtn.Image = null;
            }




            if (File.Exists(Explorer.pardirectory[1].FullName + "\\Stack.jpg"))
            {
                Image img = Image.FromFile(Explorer.pardirectory[1].FullName + "\\Stack.jpg");
                miniImages.Add("Stack", resizedImage(img, 0, 0, 0, 124));
                largeImages.Add("Stack", resizedImage(img, 0, 0, 0, 134));
                expBtn.Image = miniImages["Stack"];
                setToolTip("LargeStack", img, expBtn);
                img.Dispose();
            }
            if (img4 != null)
                img4.Dispose();

        }

        private void setTheme()
        {
            hoverPointer.Visible = false;
            this.BackColor = lightBackColor;
            panel1.BackColor = lightBackColor;
            pointer.BackColor = mouseClickColor;
            hoverPointer.BackColor = mouseClickColor;
            button6.BackColor = darkBackColor;
            button7.BackColor = darkBackColor;
            button8.BackColor = darkBackColor;
            trackBar1.BackColor = lightBackColor;
            divider.BackColor = kindaDark;
            flowLayoutPanel3.BackColor = lightBackColor;
            flowLayoutPanel1.BackColor = darkBackColor;
            flowLayoutPanel2.BackColor = lightBackColor;
            flowLayoutPanel4.BackColor = lightBackColor;
            button1.BackColor = kindaDark;
            button1.ForeColor = Color.White;
            button9.BackColor = darkBackColor;
            button9.ForeColor = Color.White;
            button10.BackColor = darkBackColor;
            button10.ForeColor = Color.White;
            expBtn.BackColor = lightBackColor;
            expBtn.ForeColor = Color.White;
            expBtn.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            expBtn.FlatAppearance.MouseDownBackColor = mouseClickColor;
            bsButton.BackColor = lightBackColor;
            bsButton.ForeColor = Color.White;
            bsButton.FlatAppearance.MouseOverBackColor = kindaDark;
            bsButton.FlatAppearance.MouseDownBackColor = kindaDark;
            videosBtn.BackColor = lightBackColor;
            videosBtn.ForeColor = Color.White;
            videosBtn.FlatAppearance.MouseOverBackColor = kindaDark;
            videosBtn.FlatAppearance.MouseDownBackColor = kindaDark;
            picturesBtn.BackColor = lightBackColor;
            picturesBtn.ForeColor = Color.White;
            picturesBtn.FlatAppearance.MouseOverBackColor = kindaDark;
            picturesBtn.FlatAppearance.MouseDownBackColor = kindaDark;
            fourKBtn.BackColor = lightBackColor;
            fourKBtn.ForeColor = Color.White;
            fourKBtn.FlatAppearance.MouseOverBackColor = kindaDark;
            fourKBtn.FlatAppearance.MouseDownBackColor = kindaDark;
            shortVideosBtn.BackColor = lightBackColor;
            shortVideosBtn.ForeColor = Color.White;
            shortVideosBtn.FlatAppearance.MouseOverBackColor = kindaDark;
            shortVideosBtn.FlatAppearance.MouseDownBackColor = kindaDark;
            gifsBtn.BackColor = lightBackColor;
            gifsBtn.ForeColor = Color.White;
            gifsBtn.FlatAppearance.MouseOverBackColor = kindaDark;
            gifsBtn.FlatAppearance.MouseDownBackColor = kindaDark;

            button3.BackColor = darkBackColor;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.MouseOverBackColor = darkBackColor;
            button3.FlatAppearance.MouseDownBackColor = darkBackColor;

            button4.BackColor = darkBackColor;
            button4.ForeColor = Color.White;
            button4.FlatAppearance.MouseOverBackColor = darkBackColor;
            button4.FlatAppearance.MouseDownBackColor = darkBackColor;

            addFile.BackColor = darkBackColor;
            addFile.ForeColor = Color.White;
            addFile.FlatAppearance.MouseOverBackColor = kindaDark;
            addFile.FlatAppearance.MouseDownBackColor = kindaDark;

            refresh.BackColor = darkBackColor;
            refresh.ForeColor = Color.White;
            refresh.FlatAppearance.MouseOverBackColor = kindaDark;
            refresh.FlatAppearance.MouseDownBackColor = kindaDark;

            multisel.BackColor = darkBackColor;
            multisel.ForeColor = Color.White;
            multisel.FlatAppearance.MouseOverBackColor = kindaDark;
            multisel.FlatAppearance.MouseDownBackColor = kindaDark;

            delete.BackColor = darkBackColor;
            delete.ForeColor = Color.White;
            delete.FlatAppearance.MouseOverBackColor = kindaDark;
            delete.FlatAppearance.MouseDownBackColor = kindaDark;

            theme.BackColor = darkBackColor;
            theme.ForeColor = Color.White;
            theme.FlatAppearance.MouseOverBackColor = kindaDark;
            theme.FlatAppearance.MouseDownBackColor = kindaDark;

            move.BackColor = darkBackColor;
            move.ForeColor = Color.White;
            move.FlatAppearance.MouseOverBackColor = kindaDark;
            move.FlatAppearance.MouseDownBackColor = kindaDark;

            sortSize.BackColor = darkBackColor;
            sortSize.ForeColor = Color.White;
            sortSize.FlatAppearance.MouseOverBackColor = kindaDark;
            sortSize.FlatAppearance.MouseDownBackColor = kindaDark;

            sortDate.BackColor = darkBackColor;
            sortDate.ForeColor = Color.White;
            sortDate.FlatAppearance.MouseOverBackColor = kindaDark;
            sortDate.FlatAppearance.MouseDownBackColor = kindaDark;

            convert.BackColor = darkBackColor;
            convert.ForeColor = Color.White;
            convert.FlatAppearance.MouseOverBackColor = kindaDark;
            convert.FlatAppearance.MouseDownBackColor = kindaDark;

            reset.BackColor = darkBackColor;
            reset.ForeColor = Color.White;
            reset.FlatAppearance.MouseOverBackColor = kindaDark;
            reset.FlatAppearance.MouseDownBackColor = kindaDark;

            load4K.BackColor = darkBackColor;
            load4K.ForeColor = Color.White;
            load4K.FlatAppearance.MouseOverBackColor = kindaDark;
            load4K.FlatAppearance.MouseDownBackColor = kindaDark;

            button5.BackColor = darkBackColor;
            button5.ForeColor = Color.White;
            button5.FlatAppearance.MouseOverBackColor = darkBackColor;
            button5.FlatAppearance.MouseDownBackColor = darkBackColor;

            navController.BackColor = darkBackColor;
            navController.ForeColor = Color.White;
            navController.FlatAppearance.MouseOverBackColor = kindaDark;
            navController.FlatAppearance.MouseDownBackColor = kindaDark;

            rcVid.BackColor = darkBackColor;
            rcVid.ForeColor = Color.White;
            rcVid.FlatAppearance.MouseOverBackColor = kindaDark;
            rcVid.FlatAppearance.MouseDownBackColor = kindaDark;

            rcPics.BackColor = darkBackColor;
            rcPics.ForeColor = Color.White;
            rcPics.FlatAppearance.MouseOverBackColor = kindaDark;
            rcPics.FlatAppearance.MouseDownBackColor = kindaDark;

            rc4K.BackColor = darkBackColor;
            rc4K.ForeColor = Color.White;
            rc4K.FlatAppearance.MouseOverBackColor = kindaDark;
            rc4K.FlatAppearance.MouseDownBackColor = kindaDark;

            rcSVid.BackColor = darkBackColor;
            rcSVid.ForeColor = Color.White;
            rcSVid.FlatAppearance.MouseOverBackColor = kindaDark;
            rcSVid.FlatAppearance.MouseDownBackColor = kindaDark;

            rcAffinity.BackColor = darkBackColor;
            rcAffinity.ForeColor = Color.White;
            rcAffinity.FlatAppearance.MouseOverBackColor = kindaDark;
            rcAffinity.FlatAppearance.MouseDownBackColor = kindaDark;

            rcGifs.BackColor = darkBackColor;
            rcGifs.ForeColor = Color.White;
            rcGifs.FlatAppearance.MouseOverBackColor = kindaDark;
            rcGifs.FlatAppearance.MouseDownBackColor = kindaDark;

            resetAndSet.BackColor = darkBackColor;
            resetAndSet.ForeColor = Color.White;
            resetAndSet.FlatAppearance.MouseOverBackColor = kindaDark;
            resetAndSet.FlatAppearance.MouseDownBackColor = kindaDark;

            edit.BackColor = darkBackColor;
            edit.ForeColor = Color.White;
            edit.FlatAppearance.MouseOverBackColor = kindaDark;
            edit.FlatAppearance.MouseDownBackColor = kindaDark;

            setDp.BackColor = darkBackColor;
            setDp.ForeColor = Color.White;
            setDp.FlatAppearance.MouseOverBackColor = kindaDark;
            setDp.FlatAppearance.MouseDownBackColor = kindaDark;

            //textBox1.BackColor = lightBackColor;
            //textBox1.ForeColor = Color.White;
            button9.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button9.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button10.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button10.FlatAppearance.MouseDownBackColor = mouseClickColor;


            button7.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button7.FlatAppearance.MouseDownBackColor = mouseClickColor;

            button8.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button8.FlatAppearance.MouseDownBackColor = mouseClickColor;

            newProgressBar.ForeColor = darkBackColor;
            newProgressBar.BackColor = darkBackColor;


            if (globalBtn != null)
                globalBtn.ForeColor = mouseClickColor;

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
            flowLayoutPanel1.Size = new Size(1648, flowLayoutPanel1.Height);
            if (type == "Videos" || type == "Gif Vid" || type == "Affinity")
            {
                convert.Enabled = true;
                edit.Enabled = false;
                setDp.Enabled = false;
                resetAndSet.Enabled = false;
            }
            else
            {
                convert.Enabled = false;
                edit.Enabled = true;
                setDp.Enabled = true;
                resetAndSet.Enabled = true;
            }

            if (type == "4K")
                load4K.Enabled = true;
            else
                load4K.Enabled = false;


            if (type == "Videos")
                reset.Enabled = true;
            else
                reset.Enabled = false;

            sortDate.Enabled = true;
            sortSize.Enabled = true;
            newProgressBar.Value = 0;
            newProgressBar.Step = 1;
            flowLayoutPanel1.Controls.Clear();
            mutltiSelect = false;
            multisel.ForeColor = Color.White;
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

            /*if (miniVideoPlayer != null)
            {
                try
                {
                    if (miniVideoPlayer.pb!=null && miniVideoPlayer.pb.Image != null)
                        miniVideoPlayer.pb.Image.Dispose();

                    miniVideoPlayer.axWindowsMediaPlayer1.Dispose();
                }
                catch { }

                if (miniVideoPlayer.wmp != null)
                {
                    miniVideoPlayer.wmp.Hide();
                    Application.RemoveMessageFilter(wmp);
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
                miniVideoPlayer = null;
                GC.Collect();
            }*/
            button1.Text = trackBar1.Value.ToString();
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
                img3 = Image.FromFile(File.ReadAllText(mainDi.FullName + "\\disPic.txt").Trim());
                button5.Tag = img3;
                disposePb.Add(img3);
            }
            if (globalPb.Image != null) globalPb.Image.Dispose();

            if(type == "Videos" || type == "Gif Vid")
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
        {/*
            if (type == "Videos" || type == "Gif Vid" || type == "Affinity")
            {
                String fileName = pb.Name;
                if (fileName.EndsWith(".txt"))
                {
                    List<String> dir = File.ReadAllLines(fileName).ToList();
                    if (dir.Count == 0)
                        return;
                    fileName = dir.ElementAt(0);

                    List<PictureBox> tempList = new List<PictureBox>();
                    foreach (String file in dir)
                    {
                        PictureBox tempPb = new PictureBox();
                        tempPb.Name = file;
                        tempPb.Image = setDefaultPic(new FileInfo(file), tempPb);
                        if (tempPb.Image != null) tempPb.Image.Dispose();
                        tempList.Add(tempPb);
                    }
                    if (miniVideoPlayer == null) miniVideoPlayer = new miniVideoPlayer(tempList);
                    else miniVideoPlayer.setVideosPb(tempList);
                }
                else if (type == "Videos")
                {
                    if (miniVideoPlayer == null) miniVideoPlayer = new miniVideoPlayer(videosPb);
                    else miniVideoPlayer.setVideosPb(videosPb);
                }
                else
                {
                    if (miniVideoPlayer == null) miniVideoPlayer = new miniVideoPlayer(shortVideosPb);
                    else miniVideoPlayer.setVideosPb(shortVideosPb);
                }
                WindowsMediaPlayer wmp = new WindowsMediaPlayerClass();
                IWMPMedia mediainfo = wmp.newMedia(fileName);
                Double duration = mediainfo.duration;

                if (duration < 3 * 60)
                {
                    miniVideoPlayer.axWindowsMediaPlayer1.Dock = DockStyle.Fill;
                    isShort = true;
                }
                else
                {
                    miniVideoPlayer.axWindowsMediaPlayer1.Dock = DockStyle.None;
                    isShort = false;
                }

                Application.RemoveMessageFilter(this);
                Point controlLoc = this.PointToScreen(pb.Location);
                int x = controlLoc.X - this.Location.X + 240, y = controlLoc.Y - this.Location.Y - 10 + 90;
                if(x + 582 > 1920)
                {
                    x = x - ((x + 582) - 1920) - 2;
                }

                if (y < 0)
                {
                    y = 1;
                }
                else
                if (y + 330 > 1080)
                {
                    y = y - ((y + 330) - 1080) - 2;
                }
                popUpVideoWidth = 582.0;
                popUpVideoHeight = 330.0;
                if (type == "Gif Vid" || type == "Affinity")
                {
                    popUpVideoWidth = 436.0;
                    popUpVideoHeight = 247.0;
                    x = flowLayoutPanel1.Location.X + pb.Location.X - (((int)popUpVideoWidth-pb.Width)/2);
                    y = flowLayoutPanel1.Location.Y + pb.Location.Y - (((int)popUpVideoHeight - pb.Height) / 2);
                    stepWise = 8;

                }
                relativeLoc = new Point(x, y);
                miniVideoPlayer.setData(pb, new FileInfo(fileName), this);
                try
                {
                    miniVideoPlayer.axWindowsMediaPlayer1.enableContextMenu = false;
                }
                catch { return; }

                //miniVideoPlayer.Region = null;
                miniVideoPlayer.axWindowsMediaPlayer1.Region = null;
                miniVideoPlayer.Location = new Point( pb.Location.X + flowLayoutPanel1.Location.X, pb.Location.Y + flowLayoutPanel1.Location.Y);
                //miniVideoPlayer.Location = relativeLoc;
                miniVideoPlayer.pastPos = 0;
                //miniVideoPlayer.Size = new Size(582, 330);
                miniVideoPlayer.Size = pb.Size;
                miniVideoPlayer.axWindowsMediaPlayer1.Size = new Size(pb.Size.Width, pb.Size.Height-3);
                miniVideoPlayer.axWindowsMediaPlayer1.Location = new Point(0, 4);
                miniVideoPlayer.newProgressBar.Location = new Point(0, -3);
                miniVideoPlayer.newProgressBar.Size = new Size(pb.Size.Width, 10);
                miniVideoPlayer.axWindowsMediaPlayer1.URL = fileName;
                //miniVideoPlayer.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, miniVideoPlayer.Width, miniVideoPlayer.Height, 20, 20));
                miniVideoPlayer.axWindowsMediaPlayer1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, miniVideoPlayer.axWindowsMediaPlayer1.Width, miniVideoPlayer.axWindowsMediaPlayer1.Height, 20, 20));
                //miniVideoPlayer.duration = miniVideoPlayer.axWindowsMediaPlayer1.currentMedia.duration;
                //miniVideoPlayer.newProgressBar.Maximum = (int)miniVideoPlayer.duration;
                miniVideoPlayer.Show();
                if (enter == true)
                {
                    if (!isShort)
                    {
                        String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\resume.txt");
                        FileInfo fi = new FileInfo(fileName);
                        isShort = false;
                        foreach (String str in resumeFile)
                            if (str.Contains("@@" + fi.Name + "@@!"))
                            {
                                miniVideoPlayer.pastPos = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                miniVideoPlayer.isMoved = true;
                                //miniVideoPlayer.duration = miniVideoPlayer.axWindowsMediaPlayer1.currentMedia.duration;
                                //miniVideoPlayer.newProgressBar.Maximum = (int)miniVideoPlayer.duration;
                            }
                    }
                    miniVideoPlayer.axWindowsMediaPlayer1_MouseDownEvent(null, null);
                }
            }
            else */if (type == "Pictures" || type == "4K" || type == "Gifs")
            {

                Application.RemoveMessageFilter(this);
                if (pb.Image.Width > pb.Image.Height || type == "Gifs")
                {
                    PicViewer picViewer = new PicViewer(pb.Name, this, type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), 1920, 1080, type == "Gifs" ? true : false);
                    //picViewer.picPanel.Dock = DockStyle.Fill;
                    //picViewer.flowLayoutPanel1.Dock = DockStyle.Bottom;
                    picViewer.flowLayoutPanel1.Size = new Size(1840,110);
                    picViewer.originalSize = new Size(1840, 110);
                    picViewer.flowLayoutPanel1.Location = new Point(40, 955);
                    picViewer.originalPoint = new Point(40, 955);
                    picViewer.flowLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
                    picViewer.flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.flowLayoutPanel1.Width, picViewer.flowLayoutPanel1.Height, 15, 15));
                    picViewer.fillUpFP1(type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), true);
                    picViewer.hideBtnW.Visible = true;
                    picViewer.hideBtnW.Location = new Point(960-(picViewer.hideBtnH.Width/2), 1070);
                    picViewer.editBtnH.Location = new Point(1909, 540 - (picViewer.hideBtnH.Height / 2));
                    picViewer.editBtnH.Visible = true;
                    picViewer.editPanel.Location = new Point(1905 - (picViewer.editPanel.Width), 540 - (picViewer.editPanel.Height / 2));
                    picViewer.editPanel.Visible = false;
                    picViewer.label1.Location = new Point(1918 - (picViewer.label1.Width), 2);
                    picViewer.label1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.label1.Width, picViewer.label1.Height, 10, 10));
                    picViewer.label1.Visible = false;
                    picViewer.Show();
                    /*wmpSide1 = new wmpSide(null, picViewer, true);
                    wmpSide1.BackColor = darkBackColor;
                    wmpSide1.Location = new Point(0, 970);
                    wmpSide1.fillUpFP1(type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), true);
                    wmpSide1.Size = new Size(1920, 110);*/
                }
                else
                {
                    PicViewer picViewer = new PicViewer(pb.Name, this, type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), 1920, 1080, false);
                    //picViewer.picPanel.Dock = DockStyle.Fill;
                    //picViewer.flowLayoutPanel1.Dock = DockStyle.Left;
                    picViewer.flowLayoutPanel1.Size = new Size(170, 1030);
                    picViewer.originalSize = new Size(170, 1030);
                    picViewer.flowLayoutPanel1.Location = new Point(15,25);
                    picViewer.originalPoint = new Point(15, 25);
                    picViewer.flowLayoutPanel1.Padding = new Padding(0, 6, 0, 0);
                    picViewer.flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.flowLayoutPanel1.Width, picViewer.flowLayoutPanel1.Height, 15, 15));
                    picViewer.fillUpFP1(type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), false);
                    picViewer.hideBtnH.Visible = true;
                    picViewer.hideBtnH.Location = new Point(-2, 540 - (picViewer.hideBtnH.Height/2));
                    picViewer.editBtnH.Location = new Point(1909, 540 - (picViewer.hideBtnH.Height / 2));
                    picViewer.editBtnH.Visible = true;
                    picViewer.editPanel.Location = new Point(1905 - (picViewer.editPanel.Width), 540 - (picViewer.editPanel.Height / 2));
                    picViewer.editPanel.Visible = false;
                    picViewer.label1.Location = new Point(1918 - (picViewer.label1.Width), 2);
                    picViewer.label1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.label1.Width, picViewer.label1.Height, 10, 10));
                    picViewer.label1.Visible = false;
                    picViewer.Show();
                    /*wmpSide1 = new wmpSide(null, picViewer, true);
                    wmpSide1.Size = new Size(120, 1080);
                    wmpSide1.BackColor = darkBackColor;
                    wmpSide1.Location = new Point(0, 0);
                    wmpSide1.fillUpFP1(type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), false);*/

                }
                //wmpSide1.Show();
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
        }


        public void renameVideoFiles(String filePath, String destPath, List<String> fileStr)
        {
            FileInfo fi = new FileInfo(filePath);
            if (fi.Name.Contains("placeholdeerr") || fi.Name.ToLower().EndsWith(".txt"))
            {
                if(globalBtn.Text.Equals("Videos") && fi.Name.Contains("placeholdeerr")) fileStr.Add(fi.Name + "@@!0");
                return;
            }
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
                if (File.Exists(destPath + "\\imgPB\\resized_" + fi.Name + ".jpg"))
                {
                    try
                    {
                        File.Copy(@destPath + "\\imgPB\\resized_" + fi.Name + ".jpg", @fi.DirectoryName + "\\imgPB\\resized_" + vidDetText + "placeholdeerr" + fi.Name + ".jpg");
                        File.Delete(@destPath + "\\imgPB\\resized_" + fi.Name + ".jpg");
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    try
                    {
                        File.Copy(@destPath + "\\imgPB\\" + fi.Name + ".jpg", @fi.DirectoryName + "\\imgPB\\" + vidDetText + "placeholdeerr" + fi.Name + ".jpg");
                        File.Delete(@destPath + "\\imgPB\\" + fi.Name + ".jpg");
                    }
                    catch
                    {

                    }
                }
                int o = 0;
                for (o = 0; o < 100; o++)
                {
                    try
                    {
                        if (!File.Exists(destPath + "\\" + vidDetText + "placeholdeerr" + o + fi.Name))
                        {
                            File.Move(fi.FullName, destPath + "\\" + vidDetText + "placeholdeerr" + o + fi.Name);
                        if (globalBtn.Text.Equals("Videos")) fileStr.Add(destPath + "\\" + vidDetText + "placeholdeerr" + o + fi.Name + "@@!0");
                        fileName = fi.Name;
                            break;
                        }
                    }
                    catch
                    {
                    }
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

        private Bitmap resizedImage(Image img, params int[] numbers)
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

        private List<String> GetRandomFiles()
        {
            List<String> priorList = new List<string>();
            DirectoryInfo parDir = new DirectoryInfo("F:\\Calculator");
            DirectoryInfo directory2 = new DirectoryInfo("H:\\vivado\\rand_name\\rand_name.ir");
            List<DirectoryInfo> supPar = new List<DirectoryInfo>();

            supPar.Add(new DirectoryInfo("I:\\ubuntu\\home\\xdm\\bin"));
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
                if (mainDi.FullName.Contains("I:\\") || mainDi.FullName.Contains("H:\\"))
                {
                    DirectoryInfo tempDi = null;
                    if(mainDi.FullName.Contains("I:\\"))
                        tempDi = new DirectoryInfo("H:\\vivado\\rand_name\\rand_name.ir\\zBest of the Best");
                    else
                        tempDi = new DirectoryInfo("I:\\ubuntu\\home\\xdm\\bin\\build");
                    foreach (DirectoryInfo di2 in tempDi.GetDirectories())
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
            return priorList;
        }

        private void VideoPlayer_Load()
        {
            allVidDet.Clear();
            Boolean mouseEnter = false;
            List<String> priorityList = new List<String>();
            List<String> inBuildTypes = File.ReadAllLines(mainDi.FullName + "\\inTypes.txt").ToList();
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
                temp = false;
                foreach (String str in inBuildTypes)
                    if (str.Contains(fi.Name)) { temp = true; break; }
                if (temp == false && !fi.Name.EndsWith(".txt")) { inBuildTypes.Add("Local Videos@" + fi.FullName); }
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

            if (File.Exists(mainDi + "\\links.txt"))
            {
                String[] links = File.ReadAllLines(mainDi + "\\links.txt");
                //if(File.Exists(mainDi + "\\webLinks.txt"))
                    //String[] webLinks = File.ReadAllLines(mainDi + "\\webLinks.txt");
                foreach (String s in links)
                {
                    priorityList.Insert(0, "0@" + s);
                }
            }

            String[] webLinkStr = File.ReadAllLines(mainDi.FullName + "\\webLinks.txt");


            if (isChecked)
            {
                priorityList = GetRandomFiles();
            }


            String resumeTxt = File.ReadAllText(mainDi.FullName + "\\resume.txt");
            newProgressBar.Maximum = priorityList.Count;
            if (toSort) {
                priorityList.Clear();
                List<FileInfo> tempFi = null;
                if (sortBySize)
                {
                    tempFi = mainDi.GetFiles().OrderByDescending(f => f.Length)
                                                      .ToList();
                    sortBySizeOrderDesc = !sortBySizeOrderDesc;
                    if (!sortBySizeOrderDesc)
                        tempFi.Reverse();
                }
                if (sortByDate)
                {
                    tempFi = mainDi.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
                    sortByDateOrderDesc = !sortByDateOrderDesc;
                    if (!sortByDateOrderDesc)
                        tempFi.Reverse();
                }


                foreach (FileInfo fi in tempFi)
                {
                    if (fi.Name.EndsWith(".txt"))
                        continue;
                    priorityList.Add("0@" + fi.FullName);
                }
                toSort = false;
            }

            if (exp.isGames)
            {
                priorityList.Clear();
                foreach (DirectoryInfo fi in mainDi.GetDirectories().OrderByDescending(f => f.LastWriteTime)
                                                      .ToList())
                {
                    if (fi.Name.Contains("imgPB") || fi.Name.Contains("Online") || fi.Name.Contains("Pics") || fi.Name.Contains("PlayLists")) { }
                    else
                        priorityList.Add("0@" + fi.FullName);
                }
                for (int i = 0; i < priorityList.Count; i++)
                {
                    DirectoryInfo fileInfo = null;
                    if (priorityList[i].Split('@')[1] != "")
                        fileInfo = new DirectoryInfo(priorityList[i].Split('@')[1]);
                    else
                        continue;

                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Top;
                    pb.Name = fileInfo.FullName;
                    pb.Size = new Size(767, 435);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.ContextMenuStrip = contextMenuStrip1;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 18, 18));
                    pb.Image = File.Exists(fileInfo.FullName + "\\images\\Start.jpg") ? resizedImage(Image.FromFile(fileInfo.FullName + "\\images\\Start.jpg"), 0, 0, 767, 0) : null;
                    pb.Margin = new Padding(5, 10, 17, 0);
                    videosPb.Add(pb);

                    pb.MouseClick += (s, args) =>
                    {
                        System.Diagnostics.Process.Start(fileInfo.FullName + "\\SuslikX.com.swf");
                        //Process.Start(Directory.GetParent(fileInfo.Parent.FullName).Parent.FullName + "\\flashplayer_25_sa.exe");
                    };

                    flowLayoutPanel1.Controls.Add(videosPb.ElementAt(videosPb.Count - 1));
                }
            }
            else
            {
                flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + 18, flowLayoutPanel1.Height);
                Label dupeLabel3 = new Label();
                dupeLabel3.Text = "Play Lists";
                dupeLabel3.Font = new Font("Segoe UI", 22, FontStyle.Bold);
                dupeLabel3.BackColor = darkBackColor;
                dupeLabel3.Size = new Size(1610, 55);
                dupeLabel3.ForeColor = Color.White;
                dupeLabel3.TextAlign = ContentAlignment.MiddleLeft;
                dupeLabel3.Margin = new Padding(15, 8, 0, 5);
                dupeLabel3.MouseEnter += (s, a) =>
                {
                    if (prevFlowLayoutPanel != null)
                    {
                        flowLayoutPanel1_MouseEnter(null, null);
                    }
                    GC.Collect();
                };

                if (Directory.Exists("E:\\Softwares\\Games\\0Games\\" + mainDi.Name))
                {
                    foreach (String di in Directory.GetDirectories("E:\\Softwares\\Games\\0Games\\" + mainDi.Name))
                    {
                        if (di.Contains("\\imgPB") || di.Contains("\\Online") || di.Contains("\\Pics") || di.Contains("\\PlayLists") ||
                            File.Exists(playListDi.FullName + "\\" + di.Substring(di.LastIndexOf("\\") + 1) + ".txt"))
                        {
                            continue;
                        }
                        DirectoryInfo diInfo = new DirectoryInfo(di + "\\videos");
                        List<FileInfo> filesList = diInfo.GetFiles().ToList().OrderBy(f => f.Name)
                                                      .ToList();
                        for (int k= 0;k < filesList.Count; k++)
                        {
                            FileInfo fiInfo = filesList[k];
                            if (fiInfo.Name.EndsWith(".txt"))
                                continue;

                            String files = "";
                            String keyWord = fiInfo.Name.Substring(0, fiInfo.Name.LastIndexOf("."));
                            if (File.Exists(playListDi.FullName + "\\Games " + diInfo.Parent.Name + "~" + keyWord + ".txt"))
                            {
                                while (k < filesList.Count && filesList[k].Name.StartsWith(keyWord))
                                    k++;
                                if (k == filesList.Count)
                                    break;
                                k--;
                                continue;
                            }
                            while (filesList[k].Name.StartsWith(keyWord))
                            {
                                files = files + filesList[k].FullName + "\n";
                                k++;
                                if (k == filesList.Count)
                                    break;
                            }
                            File.WriteAllText(playListDi.FullName + "\\Games " + diInfo.Parent.Name + "~" + keyWord + ".txt", files);
                            if (k == filesList.Count)
                                break;
                            k--;
                        }
                    }
                }
                if (playListDi.GetFiles().Length!=0) flowLayoutPanel1.Controls.Add(dupeLabel3);

                int id = 0;
                ToolStripMenuItem[] tempStripMenuItem = new ToolStripMenuItem[playListDi.GetFiles().Length];
                String prevPl = playListDi.GetFiles().Length>0?playListDi.GetFiles().ElementAt(0).Name:"", currPl = "";

                for (int i = 0; i < playListDi.GetFiles().Count(); i++)
                {
                    FlowLayoutPanel indFlowLayoutPanel = new FlowLayoutPanel();
                    indFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                    indFlowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                    indFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);
                    indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 23, 279);

                    indFlowLayoutPanel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
                    int max = 4;
                    for (int j = 0; j < max; j++)
                    {
                        if (i + j == playListDi.GetFiles().Count())
                            break;

                        FileInfo subDir = playListDi.GetFiles().ElementAt(i + j);
                        if (subDir.Name.StartsWith("Games "))
                        {
                            currPl = playListDi.GetFiles().ElementAt(i + j).Name;
                            if (prevPl.Split('~')[0] != currPl.Split('~')[0])
                            {
                                prevPl = currPl;
                                max = j;
                                continue;
                            }
                            prevPl = currPl;
                        }
                        List<String> files = File.ReadAllLines(subDir.FullName).ToList();
                        
                        this.toolStripMenuItem55.DropDownItems.Clear();
                        tempStripMenuItem[id] = new System.Windows.Forms.ToolStripMenuItem();

                        tempStripMenuItem[id].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));

                        tempStripMenuItem[id].Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        tempStripMenuItem[id].Name = subDir.FullName;
                        tempStripMenuItem[id].Size = new System.Drawing.Size(239, 24);
                        tempStripMenuItem[id].Text = subDir.Name.Replace(subDir.Extension, "");
                        tempStripMenuItem[id].MouseDown += (s, args) =>
                        {
                            if (contextMenuStrip1.SourceControl == null)
                                return;
                            Label clickedPb = (Label)contextMenuStrip1.SourceControl;
                            FileInfo fi = new FileInfo(clickedPb.Name);
                            files = File.ReadAllLines(subDir.FullName).ToList();
                            try
                            {
                                if (!files.Contains(fi.FullName))
                                {
                                    files.Add(fi.FullName);
                                    File.WriteAllLines(subDir.FullName, files);
                                }
                                    //refreshFolder();
                                }
                            catch
                            {
                                MessageBox.Show("Unable to do the move action!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        };

                        tempStripMenuItem[id].Tag = id++;

                        PictureBox pb = new PictureBox();
                        pb.Dock = DockStyle.Top;
                        pb.Size = new Size(391, 221);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.ContextMenuStrip = contextMenuStrip1;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.ContextMenuStrip = contextMenuStrip5;
                        DirectoryInfo di = new DirectoryInfo(subDir.FullName);
                        Random random = new Random();
                        if (files.Count > 0)
                        {
                            if (!subDir.Name.StartsWith("Games "))
                                randNo = random.Next(files.Count);
                            else
                                randNo = 0;
                            pb.Image = setDefaultPic(new FileInfo(files.ElementAt(randNo)), pb);
                        }
                        pb.Margin = new Padding(0, 0, 0, 0);
                        pb.Name = subDir.FullName;

                        Label vidDetails = new Label();
                        vidDetails.Text = subDir.Name.Replace(subDir.Extension, "") + "\n" + "No of Files: " + files.Count;
                        vidDetails.Font = new Font("Arial", 8, FontStyle.Regular);
                        vidDetails.BackColor = flowLayoutPanel2.BackColor;
                        vidDetails.Size = new Size(391, 40);
                        vidDetails.ForeColor = Color.White;
                        vidDetails.TextAlign = ContentAlignment.TopCenter;
                        vidDetails.Padding = new Padding(0);
                        vidDetails.Margin = new Padding(0, 3, 0, 0);


                        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                        flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                        flowLayoutPanel.BackColor = flowLayoutPanel2.BackColor;
                        flowLayoutPanel.Margin = new Padding(15, 0, 0, 0);
                        flowLayoutPanel.Size = new Size(pb.Width, pb.Height + vidDetails.Height + 3);
                        flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));

                        flowLayoutPanel.Controls.Add(pb);
                        flowLayoutPanel.Controls.Add(vidDetails);
                        indFlowLayoutPanel.Controls.Add(flowLayoutPanel);


                        vidDetails.MouseEnter += (s, e) =>
                        {
                            if (isHoveredOverPb)
                            {
                                timer1.Stop();
                                axWindowsMediaPlayer1.settings.rate = 1.0;
                            }
                        };

                        vidDetails.MouseMove += (s, e) =>
                        {
                            if (isHoveredOverPb)
                            {
                                loc = e.X;

                                if (loc - prevX > 5 || loc - prevX < -5)
                                {
                                    prevX = loc;
                                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = ((double)loc / (double)vidDetails.Width) * inWmpDuration;
                                    newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                                }
                            }
                        };

                        vidDetails.MouseClick += (s, e) =>
                        {
                            if (isHoveredOverPb)
                            {
                                Application.RemoveMessageFilter(this);

                                if (pb.Name.EndsWith(".txt"))
                                {
                                    String folderName = pb.Name.Substring(pb.Name.LastIndexOf("\\")+1).Replace(".txt", "");
                                    List<PictureBox> tempList = new List<PictureBox>(), tempPlayListPb = new List<PictureBox>();
                                    List<String> dir = File.ReadAllLines(pb.Name).ToList();
                                    if (dir.Count == 0)
                                        return;

                                    foreach (String file in dir)
                                    {
                                        PictureBox tempPb = new PictureBox();
                                        tempPb.Name = file;
                                        tempPb.Image = setDefaultPic(new FileInfo(file), tempPb);
                                        if (tempPb.Image != null) tempPb.Image.Dispose();
                                        tempList.Add(tempPb);
                                    }

                                    foreach (String file in Directory.GetFiles(pb.Name.Substring(0, pb.Name.LastIndexOf("\\"))))
                                    {
                                        PictureBox tempPb = new PictureBox();
                                        String baseFile = "";
                                        foreach (String str in File.ReadAllLines(file).ToList())
                                        {
                                            if (str.Trim().Length > 0)
                                            {
                                                baseFile = str.Trim();
                                                break;
                                            }
                                        }
                                        if (baseFile.Length == 0)
                                            continue;
                                        tempPb.Name = file;
                                        tempPb.Image = setDefaultPic(new FileInfo(baseFile), tempPb);
                                        if (tempPb.Image != null) tempPb.Image.Dispose();

                                        if (file.Substring(file.LastIndexOf("\\") + 1).StartsWith("Games "))
                                        {
                                            if (file.Substring(file.LastIndexOf("\\") + 1).Split('~')[0].Equals(pb.Name.Substring(pb.Name.LastIndexOf("\\") + 1).Split('~')[0]))
                                                tempPlayListPb.Add(tempPb);
                                        }
                                        else
                                            if(!pb.Name.Substring(pb.Name.LastIndexOf("\\") + 1).StartsWith("Games "))
                                            tempPlayListPb.Add(tempPb);
                                    }

                                    this.Hide();
                                    Explorer.wmp.Location = new Point(0, 28);
                                    if (prevPb.Name.Substring(prevPb.Name.LastIndexOf("\\") + 1).StartsWith("Games "))
                                        Explorer.wmp.repeat = false;

                                    Explorer.wmp.setRefPb(pb, tempList, this, tempPlayListPb);
                                    Explorer.wmp.axWindowsMediaPlayer1.URL = files.ElementAt(randNo);
                                    Explorer.wmp.axWindowsMediaPlayer1.Name = files.ElementAt(randNo);
                                    Explorer.wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                                    Explorer.wmp.Show();
                                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                                }
                            }
                        };

                        vidDetails.MouseLeave += (s, e) =>
                        {
                            whereAtMp = 1.0;
                            while ((whereAtMp / 9.0) * inWmpDuration < axWindowsMediaPlayer1.Ctlcontrols.currentPosition)
                            {
                                whereAtMp++;
                            }
                            axWindowsMediaPlayer1.settings.rate = 1.35;
                            timer1.Start();
                        };

                        pb.MouseHover += (s1, q1) =>
                        {
                            if (randNo >= files.Count)
                                return;
                            newProgressBarNew.Size = new Size(462, 3);
                            axWindowsMediaPlayer1.Size = new Size(462, (int)(462 * 0.567));
                            GC.Collect();
                            timer1.Stop();
                            IWMPMedia mediainfo = wmpDura.newMedia(files.ElementAt(randNo));
                            inWmpDuration = mediainfo.duration;

                            if (prevFlowLayoutPanel != null)
                            {
                                flowLayoutPanel1_MouseEnter(null, null);
                            }

                            isHoveredOverPb = true;
                            prevFlowLayoutPanel = flowLayoutPanel;
                            prevPb = pb;
                            prevVidDetails = vidDetails;

                            foreach (Control tempPanel1 in flowLayoutPanel.Parent.Controls)
                            {
                                tempPanel1.Margin = new Padding(2, 0, 0, 0);
                            }
                            flowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width - 23, 308);
                            vidDetails.Size = new Size(axWindowsMediaPlayer1.Width, 40);
                            vidDetails.Padding = new Padding(0, 1, 0, 0);
                            flowLayoutPanel.Region = null;
                            flowLayoutPanel.Size = new Size(axWindowsMediaPlayer1.Width, (int)((axWindowsMediaPlayer1.Width) * 0.567) + 45);
                            flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));

                            flowLayoutPanel.Controls.Clear();

                            whereAtMp = 1.0;
                            axWindowsMediaPlayer1.URL = files.ElementAt(randNo);
                            axWindowsMediaPlayer1.settings.rate = 1.35;
                            newProgressBarNew.Maximum = (int)inWmpDuration;

                            Double currPosition = 0;

                            if (File.Exists(mainDi.FullName + "\\resume.txt"))
                            {
                                String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\resume.txt");
                                FileInfo fi = new FileInfo(files.ElementAt(randNo));
                                foreach (String str in resumeFile)
                                    if (str.Contains("@@" + fi.Name + "@@!"))
                                    {
                                        currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                        while ((whereAtMp / 9.0) * inWmpDuration < currPosition)
                                        {
                                            whereAtMp++;
                                        }
                                    }
                            }

                            if (currPosition == 0)
                            {
                                whereAtMp = 1.0;
                                currPosition = (whereAtMp / 9.0) * inWmpDuration;
                                whereAtMp++;
                            }
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = currPosition;

                            newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                        //VideoPlayer.axWindowsMediaPlayer1.URL = pb.Name;
                        flowLayoutPanel.Controls.Add(axWindowsMediaPlayer1);
                            flowLayoutPanel.Controls.Add(newProgressBarNew);
                            flowLayoutPanel.Controls.Add(vidDetails);
                            timer1.Start();
                        };
                    }
                    i = i + (max-1);
                    if(indFlowLayoutPanel.Controls.Count!=0)flowLayoutPanel1.Controls.Add(indFlowLayoutPanel);
                }
                foreach(ToolStripMenuItem c in tempStripMenuItem)
                {
                    if(c!=null)
                        this.toolStripMenuItem55.DropDownItems.Add(c);
                }

                Label dupeLabel2 = new Label();
                dupeLabel2.Text = "Local Videos";
                dupeLabel2.Font = new Font("Segoe UI", 22, FontStyle.Bold);
                dupeLabel2.BackColor = darkBackColor;
                dupeLabel2.Size = new Size(1610, 55);
                dupeLabel2.ForeColor = Color.White;
                dupeLabel2.TextAlign = ContentAlignment.MiddleLeft;
                dupeLabel2.Margin = new Padding(15, 8, 0, 8);
                dupeLabel2.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
                flowLayoutPanel1.Controls.Add(dupeLabel2);

                for (int i = 0; i < priorityList.Count; i++)
                {
                    FlowLayoutPanel indFlowLayoutPanel = new FlowLayoutPanel();
                    indFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                    indFlowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                    indFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);
                    indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 23, 361);

                    indFlowLayoutPanel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
                    int max = 3;
                    for (int j = 0; j < max; j++)
                    {
                        if (i + j == priorityList.Count)
                            break;
                        FileInfo fileInfo = null;
                        if (priorityList[i + j].Split('@')[1] != "")
                            fileInfo = new FileInfo(priorityList[i + j].Split('@')[1]);
                        else
                        {
                            max = max + 1;
                            continue;
                        }

                        if (!File.Exists(fileInfo.FullName) || fileInfo.FullName.EndsWith(".txt"))
                        {
                            max = max + 1;
                            continue;
                        }
                        if (fileInfo.Length == 0)
                        {
                            try
                            {
                                File.Delete(fileInfo.FullName);
                            }
                            catch { }
                            continue;
                        }

                        PictureBox pb = new PictureBox();
                        pb.Dock = DockStyle.Top;
                        pb.Name = fileInfo.FullName;
                        pb.Size = new Size(523, 297);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Cursor = Cursors.Hand;
                        pb.ContextMenuStrip = contextMenuStrip1;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Image = setDefaultPic(fileInfo, pb);
                        pb.Margin = new Padding(0, 0, 0, 0);

                        if (!resumeTxt.Contains("@@" + fileInfo.Name + "@@!"))
                        {
                            resumeTxt = resumeTxt + "\n" + "@@" + fileInfo.Name + "@@!0";
                        }
                        videoUrls.Add(fileInfo.Name);
                        videosPb.Add(pb);

                        //flowLayoutPanel1.Controls.Add(videosPb.ElementAt(videosPb.Count - 1));
                        newProgressBar.PerformStep();

                        String vidDetText = fileInfo.Name.Contains("placeholdeerr") ? fileInfo.Name.Replace("placeholdeerr00", "\n").Replace("placeholdeerr0", "\n")
                            .Replace("placeholdeerr", "\n").Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:").Substring(fileInfo.Name.IndexOf("Reso")) : "";
                        //int hours = (int)(duration / 3600);
                        //int min = (int)((duration / 60) - (60 * hours));

                        Label vidDetails = new Label();
                        vidDetails.Text = vidDetText;
                        vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                        vidDetails.Name = fileInfo.FullName;
                        vidDetails.BackColor = flowLayoutPanel2.BackColor;
                        vidDetails.Size = new Size(523, 48);
                        vidDetails.ForeColor = Color.White;
                        vidDetails.TextAlign = ContentAlignment.TopCenter;
                        vidDetails.Padding = new Padding(0, 3, 0, 0);
                        vidDetails.Margin = new Padding(0, 0, 0, 0);
                        vidDetails.ContextMenuStrip = contextMenuStrip1;
                        allVidDet.Add(vidDetails);
                        /*vidDetails.MouseEnter += (s1, q1) =>
                        {

                            if (miniVideoPlayer != null)
                                miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);
                        };*/
                        //meta.Add(vidDetails);

                        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                        flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                        flowLayoutPanel.BackColor = flowLayoutPanel2.BackColor;
                        flowLayoutPanel.Margin = new Padding(15, 0, 0, 0);
                        flowLayoutPanel.Size = new Size(pb.Width, pb.Height + vidDetails.Height + 3);
                        flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));

                        flowLayoutPanel.Controls.Add(videosPb.ElementAt(videosPb.Count - 1));
                        flowLayoutPanel.Controls.Add(vidDetails);
                        indFlowLayoutPanel.Controls.Add(flowLayoutPanel);

                        /*if (meta.Count == 3)
                        {
                            foreach (Label label in meta)
                            {
                                flowLayoutPanel1.Controls.Add(label);
                            }

                            foreach (Label label in meta)
                            {
                                String[] metaData = label.Text.Split('\n');
                                if (metaData.Length == 2)
                                    label.Text = metaData[1];
                                Label dupeLabel = new Label();
                                dupeLabel.Text = metaData[0];
                                dupeLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                                dupeLabel.BackColor = darkBackColor;
                                dupeLabel.Size = new Size(515, 24);
                                dupeLabel.ForeColor = Color.White;
                                dupeLabel.TextAlign = ContentAlignment.TopCenter;
                                dupeLabel.Padding = new Padding(0);
                                dupeLabel.Margin = new Padding(5, 0, 17, 6);
                                dupeLabel.MouseEnter += (s1, q1) =>
                                {

                                    if (miniVideoPlayer != null)
                                        miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);
                                };

                                flowLayoutPanel1.Controls.Add(dupeLabel);
                            }
                            meta.Clear();
                        }*/

                        vidDetails.MouseEnter += (s, e) =>
                        {
                            if (isHoveredOverPb)
                            {
                                timer1.Stop();
                                axWindowsMediaPlayer1.settings.rate = 1.0;
                            }
                        };

                        vidDetails.MouseMove += (s, e) =>
                        {
                            if (isHoveredOverPb)
                            {
                                loc = e.X;

                                if (loc - prevX > 5 || loc - prevX < -5)
                                {
                                    prevX = loc;
                                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = ((double)loc / (double)vidDetails.Width) * inWmpDuration;
                                    newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                                }
                            }
                        };

                        vidDetails.MouseClick += (s, e) =>
                        {
                            if (isHoveredOverPb)
                            {
                                Application.RemoveMessageFilter(this);

                                this.Hide();
                                Explorer.wmp.Location = new Point(0, 28);
                                Explorer.wmp.setRefPb(pb, videosPb, this);
                                Explorer.wmp.axWindowsMediaPlayer1.URL = pb.Name;
                                Explorer.wmp.axWindowsMediaPlayer1.Name = pb.Name;
                                Explorer.wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                                Explorer.wmp.Show();
                                axWindowsMediaPlayer1.Ctlcontrols.pause();
                            }
                        };

                        vidDetails.MouseLeave += (s, e) =>
                        {
                            whereAtMp = 1.0;
                            while ((whereAtMp / 9.0) * inWmpDuration < axWindowsMediaPlayer1.Ctlcontrols.currentPosition)
                            {
                                whereAtMp++;
                            }
                            axWindowsMediaPlayer1.settings.rate = 1.35;
                            timer1.Start();
                        };

                        pb.MouseHover += (s1, q1) =>
                        {
                            GC.Collect();
                            timer1.Stop();
                            axWindowsMediaPlayer1.Size = new Size(590, (int)(590 * 0.567));
                            newProgressBarNew.Size = new Size(590, 3);
                            IWMPMedia mediainfo = wmpDura.newMedia(fileInfo.FullName);
                            inWmpDuration = mediainfo.duration;

                            if (prevFlowLayoutPanel != null)
                            {
                                flowLayoutPanel1_MouseEnter(null, null);
                            }

                            isHoveredOverPb = true;
                            prevFlowLayoutPanel = flowLayoutPanel;
                            prevPb = pb;
                            prevVidDetails = vidDetails;

                            foreach (Control tempPanel1 in flowLayoutPanel.Parent.Controls)
                            {
                                tempPanel1.Margin = new Padding(2, 0, 0, 0);
                            }
                            flowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width - 23, 381);
                            vidDetails.Size = new Size(axWindowsMediaPlayer1.Width, 48);
                            vidDetails.Padding = new Padding(0, 1, 0, 0);
                            flowLayoutPanel.Region = null;
                            flowLayoutPanel.Size = new Size(axWindowsMediaPlayer1.Width, (int)((axWindowsMediaPlayer1.Width) * 0.567) + 45);
                            flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));


                            flowLayoutPanel.Controls.Clear();

                            whereAtMp = 1.0;
                            axWindowsMediaPlayer1.URL = pb.Name;
                            axWindowsMediaPlayer1.settings.rate = 1.35;
                            newProgressBarNew.Maximum = (int)inWmpDuration;

                            Double currPosition = 0;

                            if (File.Exists(mainDi.FullName + "\\resume.txt"))
                            {
                                String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\resume.txt");
                                FileInfo fi = new FileInfo(pb.Name);
                                foreach (String str in resumeFile)
                                    if (str.Contains("@@" + fi.Name + "@@!"))
                                    {
                                        currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                        while ((whereAtMp / 9.0) * inWmpDuration < currPosition)
                                        {
                                            whereAtMp++;
                                        }
                                    }
                            }

                            if (currPosition == 0)
                            {
                                whereAtMp = 1.0;
                                currPosition = (whereAtMp / 9.0) * inWmpDuration;
                                whereAtMp++;
                            }
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = currPosition;
                            try
                            {
                                newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                            }
                            catch
                            {
                                newProgressBarNew.Value = 0;
                            }

                        //VideoPlayer.axWindowsMediaPlayer1.URL = pb.Name;
                        flowLayoutPanel.Controls.Add(axWindowsMediaPlayer1);
                            flowLayoutPanel.Controls.Add(newProgressBarNew);
                            flowLayoutPanel.Controls.Add(vidDetails);
                            timer1.Start();
                        /* if (VideoPlayer.miniVideoPlayer != null)
                             VideoPlayer.miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);
                         pbClick(pb, isShort, fileName);*/
                        //timer.Start();
                        /*timer.Stop();
                        videoHoverEnabler[pb.Name] = true;
                        timer.Start();
                        timer.Tick += (s2, a2) =>
                        {
                            if (videoHoverEnabler[fileName] == true)
                            {
                                pbClick(pb);
                                timer.Enabled = false;
                                timer.Dispose();
                            }
                            else
                            {
                                timer.Enabled = false;
                                timer.Dispose();
                                GC.Collect();
                            }
                        };*/
                        };

                        /*vidDetails.MouseLeave += (s1, a1) =>
                        {
                            if (vidDetails.Font.Name != "Comic Sans MS")
                                vidDetails.BackColor = darkBackColor;
                        };*/

                        /*pb.MouseEnter += (s1, q1) =>
                        {
                            mouseEnter = true;
                            if (miniVideoPlayer != null)
                                miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);

                            //vidDetails.BackColor = mouseClickColor;
                            //timer2.Interval = 100;
                            /*if (prevPB != null)
                            {
                                prevPB.BackColor = darkBackColor;
                                Font myfont1 = new Font("Segoe UI", 9, FontStyle.Regular);
                                globalDetails.Font = myfont1;
                                globalDetails.ForeColor = Color.White;
                            }
                            prevPB = pb;
                            globalDetails = vidDetails;
                            Font myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                            globalDetails.Font = myfont;
                            globalDetails.ForeColor = mouseClickColor;
                            enter = false;

                            /*timer2.Enabled = true;
                            timer2.Tick += (s2, a) =>
                            {
                                timer2.Enabled = false;
                                if (mouseEnter) ;
                            };
                                    pbClick(pb);
                        };*/

                        /* pb.MouseClick += (s, args) =>
                         {
                             if (prevPB != null)
                             {
                                 prevPB.BackColor = darkBackColor;
                                 Font myfont1 = new Font("Segoe UI", 9, FontStyle.Regular);
                                 globalDetails.Font = myfont1;
                                 globalDetails.BackColor = darkBackColor;
                             }
                             prevPB = pb;
                             globalDetails = vidDetails; 
                             Font myfont = new Font("Comic Sans MS", 9, FontStyle.Bold);
                             globalDetails.Font = myfont;
                             globalDetails.BackColor = mouseClickColor;
                             enter = false;
                             pbClick(pb);
                         };*/

                        /*vidDetails.MouseClick += (s, args) =>
                        {
                            if (prevPB != null)
                            {
                                prevPB.BackColor = darkBackColor;
                                Font myfont1 = new Font("Segoe UI", 9, FontStyle.Regular);
                                globalDetails.Font = myfont1;
                                globalDetails.BackColor = darkBackColor;
                            }
                            prevPB = pb;
                            globalDetails = vidDetails;
                            Font myfont = new Font("Comic Sans MS", 9, FontStyle.Bold);
                            globalDetails.Font = myfont;
                            globalDetails.BackColor = mouseClickColor;
                        };*/

                    }
                    i = i + 2;
                    flowLayoutPanel1.Controls.Add(indFlowLayoutPanel);
                }
            }

            File.WriteAllText(mainDi.FullName + "\\resume.txt", resumeTxt);
            /*for (int y = 0; y < 3 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(515, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }
            for (int y = 0; y < 3 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(515, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            foreach (Label label in meta)
            {
                String[] metaData = label.Text.Split('\n');
                label.Text = metaData[1];
                Label dupeLabel = new Label();
                dupeLabel.Text = metaData[0];
                dupeLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                dupeLabel.BackColor = darkBackColor;
                dupeLabel.Size = new Size(515, 24);
                dupeLabel.ForeColor = Color.White;
                dupeLabel.TextAlign = ContentAlignment.TopCenter;
                dupeLabel.Padding = new Padding(0);
                dupeLabel.Margin = new Padding(5, 0, 17, 50);
                flowLayoutPanel1.Controls.Add(dupeLabel);
            }

            for (int y = 0; y < 3 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(515, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            meta.Clear();*/
            /*Button dummy = new Button();
            dummy.Text = "";
            dummy.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            dummy.BackColor = lightBackColor;
            dummy.Size = new Size(515 * (3-meta.Count), 1);
            dummy.Margin = new Padding(0, 3, 3, 3);
            dummy.FlatStyle = FlatStyle.Flat;
            dummy.FlatAppearance.BorderSize = 0;
            flowLayoutPanel1.Controls.Add(dummy);*/
            //flowLayoutPanel1.Padding = new Padding(0,0,0,0);
            Label dupeLabel1 = new Label();
            dupeLabel1.Text = "Online Videos";
            dupeLabel1.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            dupeLabel1.BackColor = darkBackColor;
            dupeLabel1.Size = new Size(1610, 55);
            dupeLabel1.ForeColor = Color.White;
            dupeLabel1.TextAlign = ContentAlignment.MiddleLeft;
            dupeLabel1.Margin = new Padding(0, 15, 0, 5);
            if(webLinkStr.Length != 0) flowLayoutPanel1.Controls.Add(dupeLabel1);
            meta.Clear();
            for (int i = 0; i < webLinkStr.Length; i++)
            {
                if (webLinkStr[i].Trim().Length == 0)
                    continue;
                String[] splitted = webLinkStr[i].Split('@');
                if (splitted.Length < 2)
                    continue;
                string htmlCode = "";
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        htmlCode = client.DownloadString(splitted[1]);
                    }
                    catch { }
                }
                String videoUrl = getVideoUrl(htmlCode, splitted[1]);
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Top;
                pb.Name = splitted[0];
                pb.Size = new Size(515, 292);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Cursor = Cursors.Hand;
                pb.ContextMenuStrip = contextMenuStrip4;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 18, 18));

                if (Directory.Exists(mainDi.FullName + "\\Online\\" + pb.Name))
                {
                    DirectoryInfo picsDir = new DirectoryInfo(mainDi.FullName + "\\Online\\" + pb.Name);
                    List<FileInfo> sfi = picsDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();

                    if (sfi.Count > 0)
                    {
                        foreach (FileInfo fi in sfi)
                        {
                            if (fi.Name.StartsWith("resized"))
                            {
                                continue;
                            }
                            else
                            {
                                ResizeImage(fi.FullName, fi.DirectoryName + "\\resized_" + fi.Name + ".jpg", 0, 292, 515, 0);
                                File.Delete(fi.FullName);
                            }
                        }
                        sfi = picsDir.GetFiles().SkipWhile(s => s.FullName.EndsWith(".txt")).ToList();
                        Random rand = new Random();
                        pb.Image = Image.FromFile(sfi.ElementAt(rand.Next(sfi.Count)).FullName);
                    }
                }
                pb.Margin = new Padding(5, 5, 17, 0);

                pb.MouseClick += (s, args) =>
                {
                    Process.Start("chrome.exe", videoUrl);
                    /*Browser browser = new Browser();
                    //browser.webBrowser1.Navigate(new Uri(splitted[1]));
                    //browser.axWindowsMediaPlayer1.Visible = false;// = splitted[1];
                    if (browser.webView21 != null)
                    {
                        browser.webView21.Source = new Uri("https://" + videoUrl);
                    }
                    browser.Show();*/
                };


                Label vidDetails = new Label();
                vidDetails.Text = pb.Name;
                vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                vidDetails.BackColor = darkBackColor;
                vidDetails.Size = new Size(515, 24);
                vidDetails.ForeColor = Color.White;
                vidDetails.TextAlign = ContentAlignment.TopCenter;
                vidDetails.Padding = new Padding(0);
                vidDetails.Margin = new Padding(5, 2, 17, 20);
                vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 8, 8));
                meta.Add(vidDetails);

                pb.MouseEnter += (s, args) =>
                {
                    pb.BackColor = mouseClickColor;
                    vidDetails.BackColor = mouseClickColor;
                };

                pb.MouseLeave += (s, args) =>
                {
                    pb.BackColor = darkBackColor;
                    vidDetails.BackColor = darkBackColor;
                };

                if (meta.Count == 3)
                {
                    foreach (Label label in meta)
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }

                    foreach (Label label in meta)
                    {
                        String[] metaData = label.Text.Split('\n');
                        if (metaData.Length == 2)
                            label.Text = metaData[1];
                        Label dupeLabel = new Label();
                        dupeLabel.Text = metaData[0];
                        dupeLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                        dupeLabel.BackColor = darkBackColor;
                        dupeLabel.Size = new Size(515, 24);
                        dupeLabel.ForeColor = Color.White;
                        dupeLabel.TextAlign = ContentAlignment.TopCenter;
                        dupeLabel.Padding = new Padding(0);
                        dupeLabel.Margin = new Padding(5, 0, 17, 6);
                        flowLayoutPanel1.Controls.Add(dupeLabel);
                    }
                    meta.Clear();
                }

                flowLayoutPanel1.Controls.Add(pb);
            }

            for (int y = 0; y < 3 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(515, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }

            if (priorityList.Count == 0)
            {
                newProgressBar.Maximum = 100;
                newProgressBar.Value = 100;
            }
            else
            {
                newProgressBar.Value = newProgressBar.Maximum;
            }
            button5.Text = button5.Text.Substring(0, button5.Text.Contains("(") ? button5.Text.IndexOf("(") : button5.Text.Length) + "(" + priorityList.Count + ")";
        }

        private String getVideoUrl(String html, String url)
        {
            if (html == "") return "";
            if (url.Contains("hqporner.com"))
            {

                int startAt = html.IndexOf("'/blocks/altplayer.php?i=//") + "'/blocks/altplayer.php?i=//".Length;
                int length = html.Substring(startAt).IndexOf("'");

                /*string htmlCode = "";
                using (WebClient client = new WebClient())
                {
                    String secondLevel = "https://" + html.Substring(startAt, length);
                    htmlCode = client.DownloadString(secondLevel);
                }*/
                return html.Substring(startAt, length);
            }
            /*else if (url.Contains("goodporn.to"))
            {
                int startAt = html.IndexOf("Download:");
                String trimmed = html.Substring(startAt);

                String[] htmlLines = trimmed.Split('\n');
                String returnLink = "";
                foreach(String str in htmlLines)
                {
                    if (str.Contains("href"))
                    {
                        returnLink = str.Substring(str.IndexOf("href=\"") + 6);
                        returnLink = returnLink.Substring(0,returnLink.IndexOf("/?download=true"));
                    }
                    if (str.Contains("</div>"))
                        return returnLink;
                }
            }*/
            return url;
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
                isRefresh = true;
                videosBtn_Click(null, null);
                isRefresh = false;
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
            if (!isRefresh && globalBtn != null && globalBtn.Text != "Pictures")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }
            scrollZero = false;
            type = "Pictures";
            isEnlarged[type] = true;
            globalBtn = picturesBtn;
            picturesBtn.ForeColor = mouseClickColor;
            deletePb.Clear();
            controlDisposer();
            GC.Collect();
            flowLayoutPanel1.Padding = new Padding(5, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            imageViewer_load();
        }


        private void picturesBtn_Click(object sender, MouseEventArgs e)
        {
            if (!isRefresh && globalBtn != null && globalBtn.Text != "Pictures")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }
            scrollZero = false;
            type = "Pictures";
            isEnlarged[type] = true;
            globalBtn = picturesBtn;
            picturesBtn.ForeColor = mouseClickColor;
            deletePb.Clear();
            controlDisposer();
            GC.Collect();
            flowLayoutPanel1.Padding = new Padding(5, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            imageViewer_load();
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, picturesBtn.Location.Y + ((picturesBtn.Size.Height - pointer.Size.Height) / 2) - 3);
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
            try
            {
                bmp.Save(DestPath, myImageCodecInfo, myEncoderParameters);
                //bmp.Save(DestPath);
                bmp.Dispose();
                GC.Collect();
            }
            catch { }
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
                    ResizeImage(fi.FullName, di.FullName + "\\ImgPB\\" + fi.Name, 225, 0, 318, 0);
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
            if (stackPics)
                picsDir = new DirectoryInfo(mainDi.Parent.FullName + "\\StackPics");
            createSmallResFiles(picsDir);
            GC.Collect();
            int count = picsDir.GetFiles().Count(), i = 0;
            List<FileInfo> filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            if (sortBySize)
            {
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.Length)
                                                  .ToList();
                sortBySizeOrderDesc = !sortBySizeOrderDesc;
                if (!sortBySizeOrderDesc)
                    filesFi.Reverse();
            }
            if (sortByDate)
            {
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();

                sortByDateOrderDesc = !sortByDateOrderDesc;
                if (!sortByDateOrderDesc)
                    filesFi.Reverse();
            }

            foreach (FileInfo fi in filesFi)
            {
                Image img = null, img1 = null;
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
                    pb.Image = resizedImage(imgg, 195, 0, 262,0);
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
                    imgg.Dispose();
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
                            pb.BackColor = darkBackColor;
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

                    prevPB.BackColor = darkBackColor;

                    prevPB = pb;
                    prevPB.BackColor = selectedPbColor;
                };

                pb.MouseDoubleClick += (s, args) =>
                {
                    pbClick(pb);
                };

                pb.MouseEnter += (s, args) => {

                    /*if (pb.BackColor != mouseClickColor)
                        pb.BackColor = lightBackColor;*/
                    img1 = pb.Image;
                    pb.Image = Image.FromFile(picsDir.FullName + "\\imgPB\\" + fi.Name);
                    //pb.SizeMode = PictureBoxSizeMode.StretchImage;
                };

                pb.MouseLeave += (s, args) => {
                    //if(pb.BackColor == lightBackColor)
                    //pb.BackColor = darkBackColor;
                    //pb.SizeMode = PictureBoxSizeMode.CenterImage;
                    pb.Image.Dispose();
                    pb.Image = img1;
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
                    using (Font myFont = new Font("Segoe UI", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        try
                        {
                            args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\tReso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(8, pb.Height - 18));
                        }
                        catch { }
                    }
                };
                /*Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
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
            dummy.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
            dummy1.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
                vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
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
            dummy2.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
            List<String> fileStr = new List<string>();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (String fi in ofd.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(fi);
                    if (globalBtn.Text.Equals("Videos") || globalBtn.Text.Equals("Gif Vid") || globalBtn.Text.Equals("Affinity"))
                    {
                        renameVideoFiles(fi, dirPath, fileStr);
                        continue;
                    }
                    for (int k = 0; k < 50; k++)
                    {
                        if (File.Exists(dirPath + "\\" + fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf(".")) + k + fileInfo.Extension))
                            continue;
                        File.Move(fi, dirPath + "\\" + fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf(".")) + k + fileInfo.Extension);
                        break;
                    }
                }

                if (globalBtn.Text.Equals("Videos"))
                {
                    String files = "";
                    if (File.Exists(Explorer.directory3.FullName + "\\newFiles.txt"))
                    {
                        foreach (String str in File.ReadAllLines(Explorer.directory3.FullName + "\\newFiles.txt"))
                            fileStr.Add(str);
                        int max = fileStr.Count >= 4 ? 4 : fileStr.Count;

                        for (int i = 0; i < max; i++)
                        {
                            files = files + fileStr.ElementAt(i) + "\n";
                        }
                        File.WriteAllText(Explorer.directory3.FullName + "\\newFiles.txt", files);
                    }

                    videosBtn_Click(null, null);
                }
                else if (globalBtn.Text.Equals("Pictures"))
                {
                    picturesBtn_Click(null, null);
                }
                else if (globalBtn.Text.Equals("Gif Vid"))
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
                else if (globalBtn.Text.Equals("Affinity"))
                {
                    bsButton_Click(null, null);
                }
            }
        }

        private void expBtn_Click(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
                exp.Show();
                exp.textBox3.Focus();
            this.Hide();
            exitForm();
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
            if (!isRefresh && globalBtn != null && globalBtn.Text!="Videos")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }

            type = "Videos";
            isEnlarged[type] = true;
            scrollZero = true;
            globalBtn = videosBtn;
            videosBtn.ForeColor = mouseClickColor;
            deletePb.Clear();
            controlDisposer();
            this.Controls.Add(newProgressBar);
            VideoPlayer_Load();
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, videosBtn.Location.Y + ((videosBtn.Size.Height - pointer.Size.Height) / 2) - 3);
        }

        public void loadImages(int noOf)
        {
            newProgressBar.Value = 0;
            newProgressBar.Maximum = noOf;
            Image img1 = null;
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
                        pb.Image = resizedImage(imgg, 195,0,262,0);
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
                        imgg.Dispose();
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
                            pb.BackColor = darkBackColor;
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

                    prevPB.BackColor = darkBackColor;

                    prevPB = pb;
                    prevPB.BackColor = selectedPbColor;
                };

                pb.MouseDoubleClick += (s, args) =>
                {
                    pbClick(pb);
                };

                pb.MouseEnter += (s, args) => {

                    /*if (pb.BackColor != mouseClickColor)
                        pb.BackColor = lightBackColor;*/
                    img1 = pb.Image;
                    pb.Image = Image.FromFile(_4kdir.FullName + "\\imgPB\\" + fi.Name);
                    //pb.SizeMode = PictureBoxSizeMode.StretchImage;
                };

                pb.MouseLeave += (s, args) => {
                    //if(pb.BackColor == lightBackColor)
                    //pb.BackColor = darkBackColor;
                    //pb.SizeMode = PictureBoxSizeMode.CenterImage;
                    pb.Image.Dispose();
                    pb.Image = img1;
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
                    using (Font myFont = new Font("Segoe UI", 8, FontStyle.Regular))
                    {
                        FileInfo fi = new FileInfo(pb.Name);
                        args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\tReso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(8, pb.Height - 18));
                    }
                };/*
                Label vidDetails = new Label();
                vidDetails.Text = vidDetText;
                vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
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
            dummy.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
            dummy1.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
                vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
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
            dummy2.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
            dummy3.Font = new Font("Segoe UI", 13, FontStyle.Bold);
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
            {
                imagesFi = _4kdir.GetFiles().OrderByDescending(f => f.Length)
                                                      .ToList();
                sortBySizeOrderDesc = !sortBySizeOrderDesc;
                if (!sortBySizeOrderDesc)
                    imagesFi.Reverse();
            }
            if (sortByDate)
            {
                imagesFi = _4kdir.GetFiles().OrderByDescending(f => f.CreationTime)
                                                      .ToList();
                sortByDateOrderDesc = !sortByDateOrderDesc;
                if (!sortByDateOrderDesc)
                    imagesFi.Reverse();
            }
            newProgressBar.Value = 0;

            if (loadAll)
                loadImages(imagesFi.Count + 10);
            else
                loadImages(100);

        }

        private void fourKBtn_Click(object sender, EventArgs e)
        {
            if (!isRefresh && globalBtn != null && globalBtn.Text != "4K")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }
            scrollZero = false;
            type = "4K";
            isEnlarged[type] = true;
            globalBtn = fourKBtn;
            fourKBtn.ForeColor = mouseClickColor;
            deletePb.Clear();
            controlDisposer();
            this.Controls.Add(newProgressBar);
            whereAt = 0;
            load4kImages();
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, fourKBtn.Location.Y + ((fourKBtn.Size.Height - pointer.Size.Height) / 2) - 3);
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
            else if (globalBtn.Text.Equals("Gif Vid"))
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
            if (globalBtn.Text.Equals("Pictures") || globalBtn.Text.Equals("4K"))
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
                        try { ResizeImage(fi.FullName, fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1), 308, 0, 513, 0); } catch { return; }
                        String newFileName = renameFile(i, new FileInfo(fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1)));
                        File.WriteAllText(mainDi.FullName + "\\disPic.txt", newFileName);
                        imagePBLink = newFileName;
                        break;
                    }
                }
                toRefresh = true;
            }
        }

        public void refreshFolder()
        {
            isRefresh = true;
            if (globalBtn.Text.Equals("Videos"))
            {
                videosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                picturesBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Gif Vid"))
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
            else if (globalBtn.Text.Equals("Affinity"))
            {
                bsButton_Click(null, null);
            }
            isRefresh = true;
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
            if (globalBtn.Text.Equals("Pictures") || globalBtn.Text.Equals("4K"))
            {
                File.WriteAllText(mainDi.FullName + "\\disPic.txt", pb.Name);
                toRefresh = true;
                imagePBLink = pb.Name;
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                if (File.Exists(mainDi.FullName + "\\disGifPic.txt"))
                {
                    String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\disGifPic.txt");
                    Boolean doesExist = false;
                    String fileStr = "";
                    foreach (String str in resumeFile)
                    {
                        if (str.Contains(pb.Name))
                        {
                            doesExist = true;
                            return;
                        }
                    }

                    if (!doesExist)
                    {
                        int max = resumeFile.Length >= 3 ? 2 : resumeFile.Length;
                        fileStr = pb.Name + "\n";

                        for (int i = 0; i < max; i++)
                        {
                            fileStr = fileStr + resumeFile[i] + "\n";
                        }
                    }
                    File.WriteAllText(mainDi.FullName + "\\disGifPic.txt", fileStr);
                }
            }
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
                isRefresh = true;
                fourKBtn_Click(null, null);
                isRefresh = false;
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
                isRefresh = true;
                shortVideosBtn_Click(null, null);
                isRefresh = false;
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
                isRefresh = true;
                gifsBtn_Click(null, null);
                isRefresh = false;
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
                isRefresh = true; 
                picturesBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void loadShortVideos()
        {
            flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Width + 18, flowLayoutPanel1.Height);
            DirectoryInfo shortVideoDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\" + (type.Contains("Affinity")?"Affinity" : "GifVideos"));
            int noOfFile = shortVideoDi.GetFiles().Length;
            renameVideoFiles(mainDi.FullName + "\\Pics\\" + (type.Contains("Affinity") ? "Affinity" : "GifVideos"));

            List<FileInfo> filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
            if (sortBySize)
            {
                filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.Length)
                                                  .ToList();

                sortBySizeOrderDesc = !sortBySizeOrderDesc;
                if (!sortBySizeOrderDesc)
                    filesFi.Reverse();
            }
            if (sortByDate)
            {
                filesFi = shortVideoDi.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
                sortByDateOrderDesc = !sortByDateOrderDesc;
                if (!sortByDateOrderDesc)
                    filesFi.Reverse();
            }
            newProgressBar.Maximum = filesFi.Count;

            Label dupeLabel2 = new Label();
            dupeLabel2.Text = "Local Videos";
            dupeLabel2.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            dupeLabel2.BackColor = darkBackColor;
            dupeLabel2.Size = new Size(1610, 55);
            dupeLabel2.ForeColor = Color.White;
            dupeLabel2.TextAlign = ContentAlignment.MiddleLeft;
            dupeLabel2.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
            dupeLabel2.Margin = new Padding(15, 8, 0, 0);
            flowLayoutPanel1.Controls.Add(dupeLabel2);

            newProgressBarNew.Size = new Size(476, 3);
            axWindowsMediaPlayer1.Size = new Size(476, (int)(476 * 0.567));
            List<Label> meta = new List<Label>();
            for (int i=0; i<filesFi.Count; i++)
            {
                FlowLayoutPanel indFlowLayoutPanel = new FlowLayoutPanel();
                indFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                indFlowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                indFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);
                indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 23, 279);

                indFlowLayoutPanel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
                int max = 4;
                for (int j = 0; j < max; j++)
                {
                    if (i + j == filesFi.Count)
                        break;
                    FileInfo fileInfo = filesFi.ElementAt(i + j);

                    if (!File.Exists(fileInfo.FullName) || fileInfo.FullName.EndsWith(".txt"))
                    {
                        max = max + 1;
                        continue;
                    }
                    if (fileInfo.Length == 0)
                    {
                        try
                        {
                            File.Delete(fileInfo.FullName);
                        }
                        catch { }
                        max = max + 1;
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
                    bool mouseEnter = false;
                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Top;
                    pb.Name = fileInfo.FullName;
                    pb.Size = new Size(386, 219);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.ContextMenuStrip = contextMenuStrip1;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Image = setDefaultPic(fileInfo, pb);
                    pb.Margin = new Padding(0);
                    videoUrls.Add(fileInfo.Name);
                    shortVideosPb.Add(pb);
                    //flowLayoutPanel1.Controls.Add(shortVideosPb.ElementAt(shortVideosPb.Count - 1));
                    newProgressBar.PerformStep();

                    String vidDetText = fileInfo.Name.Contains("placeholdeerr") ? fileInfo.Name.Replace("placeholdeerr00", "\n").Replace("placeholdeerr0", "\n")
                        .Replace("placeholdeerr", "\n").Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:").Substring(fileInfo.Name.IndexOf("Reso")) : "";

                    Label vidDetails = new Label();
                    vidDetails.Text = vidDetText;
                    vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                    vidDetails.BackColor = flowLayoutPanel2.BackColor;
                    vidDetails.Size = new Size(386, 45);
                    vidDetails.ForeColor = Color.White;
                    vidDetails.TextAlign = ContentAlignment.TopCenter;
                    vidDetails.Padding = new Padding(0, 2, 0, 0);
                    vidDetails.Margin = new Padding(0, 0, 0, 0);
                    vidDetails.Name = fileInfo.FullName;
                    vidDetails.ContextMenuStrip = contextMenuStrip1;
                    allVidDet.Add(vidDetails);

                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                    flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                    flowLayoutPanel.BackColor = flowLayoutPanel2.BackColor;
                    flowLayoutPanel.Margin = new Padding(15, 0, 0, 10);
                    flowLayoutPanel.Size = new Size(pb.Width, pb.Height + vidDetails.Height + 3);
                    flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 9, 9));

                    flowLayoutPanel.Controls.Add(pb);
                    flowLayoutPanel.Controls.Add(vidDetails);
                    indFlowLayoutPanel.Controls.Add(flowLayoutPanel);


                    vidDetails.MouseEnter += (s, e) =>
                    {
                        if (isHoveredOverPb)
                        {
                            axWindowsMediaPlayer1.settings.rate = 1.0;
                        }
                    };

                    vidDetails.MouseMove += (s, e) =>
                    {
                        if (isHoveredOverPb)
                        {
                            loc = e.X;

                            if (loc - prevX > 4 || loc - prevX < -4)
                            {
                                prevX = loc;
                                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = ((double)loc / (double)vidDetails.Width) * inWmpDuration;
                                newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                            }
                        }
                    };

                    vidDetails.MouseClick += (s, e) =>
                    {
                        if (isHoveredOverPb)
                        {
                            Application.RemoveMessageFilter(this);

                            this.Hide();
                            Explorer.wmp.Location = new Point(0, 28);
                            Explorer.wmp.setRefPb(pb, videosPb, this);
                            Explorer.wmp.axWindowsMediaPlayer1.URL = pb.Name;
                            Explorer.wmp.axWindowsMediaPlayer1.Name = pb.Name;
                            Explorer.wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                            Explorer.wmp.Show();
                            axWindowsMediaPlayer1.Ctlcontrols.pause();
                        }
                    };

                    vidDetails.MouseLeave += (s, e) =>
                    {
                        axWindowsMediaPlayer1.settings.rate = 1.35;
                    };

                    /*meta.Add(vidDetails);

                    if (meta.Count == 4)
                    {
                        foreach (Label label in meta)
                        {
                            flowLayoutPanel1.Controls.Add(label);
                        }

                        foreach (Label label in meta)
                        {
                            String[] metaData = label.Text.Split('\n');
                            if (metaData.Length == 2)
                                label.Text = metaData[1];
                            Label dupeLabel = new Label();
                            dupeLabel.Text = metaData[0];
                            dupeLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                            dupeLabel.BackColor = darkBackColor;
                            dupeLabel.Size = new Size(386, 24);
                            dupeLabel.ForeColor = Color.White;
                            dupeLabel.TextAlign = ContentAlignment.TopCenter;
                            dupeLabel.Padding = new Padding(0);
                            dupeLabel.Margin = new Padding(5, 0, 10, 6);
                            dupeLabel.MouseEnter += (s1, q1) =>
                            {

                                if (miniVideoPlayer != null)
                                    miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);
                            };

                            flowLayoutPanel1.Controls.Add(dupeLabel);
                        }
                        meta.Clear();
                    }

                    pb.MouseLeave += (s1, a1) =>
                    {
                        mouseEnter = false;
                        if (vidDetails.Font.Name != "Comic Sans MS")
                            vidDetails.BackColor = darkBackColor;
                    };*/

                    pb.MouseHover += (s1, q1) =>
                    {
                        GC.Collect();
                        timer1.Stop();
                        IWMPMedia mediainfo = wmpDura.newMedia(pb.Name);
                        inWmpDuration = mediainfo.duration;

                        if (prevFlowLayoutPanel != null)
                        {
                            flowLayoutPanel1_MouseEnter(null, null);
                        }

                        isHoveredOverPb = true;
                        prevFlowLayoutPanel = flowLayoutPanel;
                        prevPb = pb;
                        prevVidDetails = vidDetails;

                        foreach (Control tempPanel1 in flowLayoutPanel.Parent.Controls)
                        {
                            tempPanel1.Margin = new Padding(2, 0, 0, 0);
                        }
                        flowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width - 23, 316);
                        vidDetails.Size = new Size(axWindowsMediaPlayer1.Width, 45);
                        vidDetails.Padding = new Padding(0, 1, 0, 0);
                        flowLayoutPanel.Region = null;
                        flowLayoutPanel.Size = new Size(axWindowsMediaPlayer1.Width, (int)((axWindowsMediaPlayer1.Width) * 0.567) + 45);
                        flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));

                        flowLayoutPanel.Controls.Clear();

                        axWindowsMediaPlayer1.URL = pb.Name;
                        axWindowsMediaPlayer1.settings.rate = 1.35;
                        newProgressBarNew.Maximum = (int)inWmpDuration;
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;

                        newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                    //VideoPlayer.axWindowsMediaPlayer1.URL = pb.Name;
                    flowLayoutPanel.Controls.Add(axWindowsMediaPlayer1);
                        flowLayoutPanel.Controls.Add(newProgressBarNew);
                        flowLayoutPanel.Controls.Add(vidDetails);
                    };

                    /*pb.MouseClick += (s, args) =>
                    {
                        if (prevPB != null)
                        {
                            prevPB.BackColor = darkBackColor;
                            Font myfont1 = new Font("Segoe UI", 9, FontStyle.Regular);
                            globalDetails.Font = myfont1;
                            globalDetails.BackColor = darkBackColor;
                        }
                        prevPB = pb;
                        globalDetails = vidDetails;
                        Font myfont = new Font("Comic Sans MS", 9, FontStyle.Bold);
                        globalDetails.Font = myfont;
                        globalDetails.BackColor = mouseClickColor;
                        enter = false;
                        pbClick(pb);
                    };*/

                }
                i = i + 3;
                flowLayoutPanel1.Controls.Add(indFlowLayoutPanel);
            }

           /* for (int y = 0; y < 4 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(386, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            foreach (Label label in meta)
            {
                flowLayoutPanel1.Controls.Add(label);
            }
            for (int y = 0; y < 4 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(386, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            foreach (Label label in meta)
            {
                String[] metaData = label.Text.Split('\n');
                label.Text = metaData[1];
                Label dupeLabel = new Label();
                dupeLabel.Text = metaData[0];
                dupeLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                dupeLabel.BackColor = darkBackColor;
                dupeLabel.Size = new Size(386, 24);
                dupeLabel.ForeColor = Color.White;
                dupeLabel.TextAlign = ContentAlignment.TopCenter;
                dupeLabel.Padding = new Padding(0);
                dupeLabel.Margin = new Padding(5, 0, 10, 50);
                flowLayoutPanel1.Controls.Add(dupeLabel);
            }

            for (int y = 0; y < 4 - meta.Count; y++)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(386, 1);
                flowLayoutPanel1.Controls.Add(pb);
            }
            meta.Clear();*/
            /*foreach (FileInfo fileInfo in filesFi)
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
                pb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pb.Width, pb.Height, 20 ,20));

                pb.Image = setDefaultPic(fileInfo, pb);
                pb.Margin = new Padding(5, 3, 0, 4);


                pb.MouseEnter += (s1, q1) =>
                {
                    if (miniVideoPlayer != null)
                        miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);
                };

                pb.MouseClick += (s, args) =>
                {
                    /*if (mutltiSelect == true)
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
                vidDetails.Text = fileInfo.Name.Replace("placeholdeerr00", "\n\n").Replace("placeholdeerr0", "\n\n").Replace("placeholdeerr", "\n\n").Replace("Reso^ ", "\nReso:").Replace("Dura^ ", "\nDura:").Replace("Size^ ", "\nSize:").Substring(fileInfo.Name.IndexOf("Reso"));
                vidDetails.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                vidDetails.BackColor = lightBackColor;
                vidDetails.Size = new Size(123, 200);
                vidDetails.ForeColor = Color.White;
                vidDetails.TextAlign = ContentAlignment.TopLeft;
                vidDetails.Margin = new Padding(0, 15, 14, 3);
                vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 10, 10));
                vidDetails.MouseEnter += (s1, q1) =>
                {

                    if (miniVideoPlayer != null)
                        miniVideoPlayer.miniVideoPlayer_MouseLeave(null, null);
                };
                allVidDet.Add(vidDetails);


                pb.MouseHover += (s, args) =>
                {
                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    if (prevPB != null)
                    {
                        prevPB.BackColor = lightBackColor;

                        globalDetails.BackColor = lightBackColor;
                        
                        globalDetails.Font = myfont;
                    }
                    globalDetails = vidDetails;
                    prevPB = pb;
                    prevPB.BackColor = mouseClickColor;

                    globalDetails.BackColor = mouseClickColor;
                    myfont = new Font("Comic Sans MS", 8, FontStyle.Bold);
                    globalDetails.Font = myfont;
                    enter = false;
                    if (Explorer.wmpOnTop != null && Explorer.wmpOnTop.axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
                        return;
                    pbClick(pb);
                };

                flowLayoutPanel1.Controls.Add(vidDetails);
            }*/


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
            if (!isRefresh && globalBtn != null && globalBtn.Text != "Gif Vid")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }
            scrollZero = true;
            globalBtn = shortVideosBtn;
            shortVideosBtn.ForeColor = mouseClickColor;

            type = "Gif Vid";
            isEnlarged[type] = true;
            controlDisposer();
            deletePb.Clear();
            GC.Collect();
            this.Controls.Add(newProgressBar);
            loadShortVideos();
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, shortVideosBtn.Location.Y + ((shortVideosBtn.Size.Height - pointer.Size.Height) / 2) - 3);
        }

        private void VideoPlayer_Load(object sender, EventArgs e)
        {
            if (type.Equals("Videos"))
            {
                globalBtn = videosBtn;
                videosBtn.ForeColor = mouseClickColor;
                videosBtn_Click(null, null);
            }
            else if (type.Equals("Pictures"))
            {
                globalBtn = picturesBtn;
                picturesBtn.ForeColor = mouseClickColor;
                picturesBtn_Click(null, null);
            }
            else if (type.Equals("4K"))
            {
                globalBtn = fourKBtn;
                fourKBtn.ForeColor = mouseClickColor;
                fourKBtn_Click(null, null);
            }
            else if (type.Equals("Gif Vid"))
            {
                globalBtn = shortVideosBtn;
                shortVideosBtn.ForeColor = mouseClickColor;
                shortVideosBtn_Click(null, null);
            }
            else if (type.Equals("Gifs"))
            {
                globalBtn = gifsBtn;
                gifsBtn.ForeColor = mouseClickColor;
                gifsBtn_Click(null, null);
            }
            else if (type.Equals("Affinity"))
            {
                globalBtn = bsButton;
                bsButton.ForeColor = mouseClickColor;
                bsButton_Click(null, null);
            }
            if (isFirst && globalBtn != null)
            {
                    isEnlarged[globalBtn.Text] = false;
                    enlargeEnter(globalBtn);
                    isEnlarged[globalBtn.Text] = true;
                    isFirst = false;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRefresh = true;
            if (globalBtn.Text.Equals("Videos"))
            {
                videosBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                picturesBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Gif Vid"))
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
            else if (globalBtn.Text.Equals("Affinity"))
            {
                bsButton_Click(null, null);
            }
            isRefresh = false;
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
            {
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.Length)
                                                  .ToList();
                sortBySizeOrderDesc = !sortBySizeOrderDesc;
                if (!sortBySizeOrderDesc)
                    filesFi.Reverse();
            }
            if (sortByDate)
            {
                filesFi = picsDir.GetFiles().OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();

                sortByDateOrderDesc = !sortByDateOrderDesc;
                if (!sortByDateOrderDesc)
                    filesFi.Reverse();
            }
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
                            pb.BackColor = darkBackColor;
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

                pb.MouseEnter += (s, args) =>
                {
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                };


                pb.MouseLeave += (s, args) =>
                {
                    pb.SizeMode = PictureBoxSizeMode.CenterImage;
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
                        if(fi.Name.Contains("placeholdderr")) args.Graphics.DrawString("Size:" + fi.Name.Substring(0, fi.Name.IndexOf("placeholdderr")).Replace("^^", "\t\t  Reso:").Replace("x", "*").Replace("_", "").Replace("resized", "").Replace("cropped", ""), myFont, Brushes.White, new Point(25, pb.Height - 15));
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
            if (!isRefresh && globalBtn != null && globalBtn.Text != "Gifs")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }
            scrollZero = true;
            globalBtn = gifsBtn;
            gifsBtn.ForeColor = mouseClickColor;

            type = "Gifs";
            isEnlarged[type] = true;
            deletePb.Clear();
            controlDisposer();
            GC.Collect();
            flowLayoutPanel1.Padding = new Padding(8, 0, 0, 0);
            this.Controls.Add(newProgressBar);
            gif_Load();
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, gifsBtn.Location.Y + ((gifsBtn.Size.Height - pointer.Size.Height) / 2)-3);
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

            if (NewFileForm.newFile)
            {
                toolStripMenuItem10_Click(null, null);
                NewFileForm.newFile = false;
            }
        }

        private void ctlDisposer_DoWork(object sender, DoWorkEventArgs e)
        {
            controlDisposer();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Explorer.globalVol = trackBar1.Value;
            button1.Text = trackBar1.Value.ToString();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (type.Equals("Videos"))
            {
                videosBtn.ForeColor = mouseClickColor;
                globalBtn = videosBtn;
                videosBtn_Click(null, null);
            }
            else if (type.Equals("Pictures"))
            {
                picturesBtn.ForeColor = mouseClickColor;
                globalBtn = picturesBtn;
                picturesBtn_Click(null, null);
            }
            else if (type.Equals("4K"))
            {
                fourKBtn.ForeColor = mouseClickColor;
                globalBtn = fourKBtn;
                fourKBtn_Click(null, null);
            }
            else if (type.Equals("Gif Vid"))
            {
                shortVideosBtn.ForeColor = mouseClickColor;
                globalBtn = shortVideosBtn;
                shortVideosBtn_Click(null, null);
            }
            else if (type.Equals("Gifs"))
            {
                gifsBtn.ForeColor = mouseClickColor;
                globalBtn = gifsBtn;
                gifsBtn_Click(null, null);
            }
            else if (globalBtn.Text.Equals("Affinity"))
            {
                bsButton.ForeColor = mouseClickColor;
                globalBtn = bsButton;
                bsButton_Click(null, null);
            }
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            mutltiSelect = !mutltiSelect;

            button1.Text = trackBar1.Value.ToString();
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            mutltiSelect = false;
            multisel.ForeColor = Color.White;
            if (deletePb.Count > 0)
            {
                foreach (PictureBox delPb in deletePb)
                {
                    delPb.BackColor = darkBackColor;
                }
            }

            button1.Text = trackBar1.Value.ToString();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            String filePath = "";
            isRefresh = true;
            if (globalBtn.Text.Equals("Videos"))
            {
                filePath = mainDi.FullName;
                loadFiles(filePath);
            }
            else if (globalBtn.Text.Equals("Pictures"))
            {
                filePath = mainDi.FullName + "\\Pics";
                loadFiles(filePath);
            }
            else if (globalBtn.Text.Equals("Gif Vid"))
            {
                filePath = mainDi.FullName + "\\Pics\\GifVideos";
                loadFiles(filePath);
            }
            else if (globalBtn.Text.Equals("4K"))
            {
                filePath = mainDi.FullName + "\\Pics\\kkkk";
                loadFiles(filePath);
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                filePath = mainDi.FullName + "\\Pics\\Gifs";
                loadFiles(filePath);
            }
            else if (globalBtn.Text.Equals("Affinity"))
            {
                filePath = mainDi.FullName + "\\Pics\\Affinity";
                loadFiles(filePath);
            }
            isRefresh = false;
        }

        private void VideoPlayer_Deactivate(object sender, EventArgs e)
        {
            vertScrollValue = flowLayoutPanel1.VerticalScroll.Value;
            Application.RemoveMessageFilter(this);
        }

        /*private void timer1_Tick(object sender, EventArgs e)
        {
            if (scrollZero == true && noOfTimerRuns <= 10)
            {
                flowLayoutPanel1.VerticalScroll.Value = 0;
                flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                scrollZero = false;
                noOfTimerRuns++;
            }
        }*/

        private void button8_Click(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
                exp.Show();
                exp.textBox3.Focus();
            this.Hide();
            exitForm();
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
            PictureBox pb = null;
            try
            {
                pb = (PictureBox)(contextMenuStrip1.SourceControl);
            }
            catch
            {
                
            }
            if(pb==null)
                pb = prevPB;
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                try
                {
                    FileInfo fi = new FileInfo(pb.Name);
                    try
                    {
                        File.Delete(pb.Name);
                        flowLayoutPanel1.Controls.Remove(pb);
                    }
                    catch { }
                }
                catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Environment.Exit(0);
            }
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
            isRefresh = true;
            if (type == "Pictures")
                picturesBtn_Click(null, null);
            else if (type == "4K")
                fourKBtn_Click(null, null);
            else if (type == "Gif Vid")
                shortVideosBtn_Click(null, null);
            else if (type == "Affinity")
                bsButton_Click(null, null);
            else if (type == "Gifs")
                gifsBtn_Click(null, null);
            isRefresh = false;
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            Label pb = (Label)contextMenuStrip1.SourceControl;
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
            button9.BackColor = (isChecked == true ? mouseClickColor : darkBackColor);
            if (isChecked)
                button10.Enabled = true;
            else
                button10.Enabled = false;
            refreshFolder();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            isGlobalChecked = !isGlobalChecked;
            button10.BackColor = (isGlobalChecked == true ? mouseClickColor : darkBackColor);
            refreshFolder();
        }

        private void enlargeEnter(Button b)
        {
            if (largeImages.Count==0 || isEnlarged[b.Text])
                return;
            try
            {
                b.Image = largeImages[b.Text];
                b.ForeColor = mouseClickColor;
                Padding p = b.Margin;
                b.Region = null;
                b.Size = new Size(b.Width + 30, b.Height + 15);
                b.Margin = new Padding(p.Left - 15, p.Top - 8, p.Right, p.Bottom);
                b.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 25, 25));
                //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top - 10, label1.Margin.Right, label1.Margin.Bottom);
            }
            catch { }
        }

        private void enlargeLeave(Button b, String typee)
        {
            if (miniImages.Count ==0 || isEnlarged[b.Text])
                return;
            try
            {
                b.Image = miniImages[typee];
                Padding p = b.Margin;
                b.Region = null;
                b.Margin = new Padding(p.Left + 15, p.Top, p.Right, p.Bottom);
                b.Size = new Size(b.Width - 30, b.Height - 15);
                b.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 25, 25));
                //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top + 10, label1.Margin.Right, label1.Margin.Bottom);
            }
            catch { }
        }

        private void videosBtn_MouseEnter(object sender, EventArgs e)
        {
            enlargeEnter((Button)sender);
            hoverPointer.Location = new Point(0, videosBtn.Location.Y + ((videosBtn.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void picturesBtn_MouseEnter(object sender, EventArgs e)
        {
            enlargeEnter((Button)sender);
            hoverPointer.Location = new Point(0, picturesBtn.Location.Y + ((picturesBtn.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void fourKBtn_MouseEnter(object sender, EventArgs e)
        {
            enlargeEnter((Button)sender);
            hoverPointer.Location = new Point(0, fourKBtn.Location.Y + ((fourKBtn.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void shortVideosBtn_MouseEnter(object sender, EventArgs e)
        {
            enlargeEnter((Button)sender);
            hoverPointer.Location = new Point(0, shortVideosBtn.Location.Y + ((shortVideosBtn.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void gifsBtn_MouseEnter(object sender, EventArgs e)
        {
            enlargeEnter((Button)sender);
            hoverPointer.Location = new Point(0, gifsBtn.Location.Y + ((gifsBtn.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void videosBtn_MouseLeave(object sender, EventArgs e)
        {
            if (globalBtn != null && !globalBtn.Text.Equals("Videos"))
                videosBtn.ForeColor = Color.White;
            enlargeLeave((Button)sender, "Videos");
        }

        private void flowLayoutPanel1_MouseHover(object sender, EventArgs e)
        {

        }


        private void bsButton_Click(object sender, EventArgs e)
        {
            if (!isRefresh && globalBtn != null && globalBtn.Text != "Affinity")
            {
                globalBtn.ForeColor = Color.White;
                isEnlarged[type] = false;
                enlargeLeave(globalBtn, type);
            }
            scrollZero = true;
            globalBtn = bsButton;
            bsButton.ForeColor = mouseClickColor;

            type = "Affinity";
            isEnlarged[type] = true;
            controlDisposer();
            deletePb.Clear();
            GC.Collect();
            this.Controls.Add(newProgressBar);
            loadShortVideos();
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, bsButton.Location.Y + ((bsButton.Size.Height - pointer.Size.Height) / 2) - 3);
        }

        private void bsButton_MouseEnter(object sender, EventArgs e)
        {

            enlargeEnter((Button)sender);
            hoverPointer.Location = new Point(0, bsButton.Location.Y + ((bsButton.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void bsButton_MouseLeave(object sender, EventArgs e)
        {

            if (globalBtn != null && !globalBtn.Text.Equals("Affinity"))
                bsButton.ForeColor = Color.White;
            enlargeLeave((Button)sender, "Affinity");
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem_Click(null, null);
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
            NewFileForm newFileForm = new NewFileForm(mainDi);
            newFileForm.Location = new Point(addFile.Location.X, addFile.Location.Y + addFile.Size.Height + 8);
            newFileForm.Show();
            //toolStripMenuItem10_Click(null, null);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (type == "Videos" || type == "Gif Vid" || type == "Affinity")
                toolStripMenuItem1_Click(null, null);
            else
                deleteToolStripMenuItem1_Click_1(null, null);
        }

        private void multisel_Click(object sender, EventArgs e)
        {
            if (mutltiSelect)
            {
                foreach (PictureBox pb in deletePb)
                {
                    pb.BackColor = darkBackColor;
                }
                if (prevPB != null)
                {
                    prevPB.BackColor = selectedPbColor;
                    deletePb.Add(prevPB);
                }
            }

            if (mutltiSelect)
                multisel.ForeColor = Color.White;
            else
                multisel.ForeColor = mouseClickColor;
            mutltiSelect = !mutltiSelect;
            button1.Text = trackBar1.Value.ToString();
        }

        private void button11_Click_1(object sender, EventArgs e)
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

        private void move_Click(object sender, EventArgs e)
        {
            PictureBox pb = null;
            try
            {
                pb = (PictureBox)(contextMenuStrip1.SourceControl);
            }
            catch
            {

            }
            if (pb == null)
                pb = prevPB;
            FileInfo fi = null;
            if (pb!=null)
            {
                fi = new FileInfo(pb.Name);
            }
            else if(deletePb.Count > 0)
                fi = new FileInfo(deletePb[0].Name);
            if (fi == null)
                return;
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

        private void sortSize_Click(object sender, EventArgs e)
        {
            sortBySize = true;
            sortByDate = false;
            bySort = true;
            isRefresh = true;
            toSort = true;
            if (type == "Videos")
                videosBtn_Click(null, null);
            else if (type == "Pictures")
                picturesBtn_Click(null, null);
            else if (type == "4K")
                fourKBtn_Click(null, null);
            else if (type == "Gif Vid")
                shortVideosBtn_Click(null, null);
            else if (type == "Affinity")
                bsButton_Click(null, null);
            else if (type == "Gifs")
                gifsBtn_Click(null, null);
            isRefresh = false;
        }

        private void sortDate_Click(object sender, EventArgs e)
        {
            sortBySize = false;
            sortByDate = true;
            bySort = true;
            isRefresh = true;
            toSort = true;
            if (type == "Videos")
                videosBtn_Click(null, null);
            else if (type == "Pictures")
                picturesBtn_Click(null, null);
            else if (type == "4K")
                fourKBtn_Click(null, null);
            else if (type == "Gif Vid")
                shortVideosBtn_Click(null, null);
            else if (type == "Affinity")
                bsButton_Click(null, null);
            else if (type == "Gifs")
                gifsBtn_Click(null, null);
            isRefresh = false;
        }

        private void convert_Click(object sender, EventArgs e)
        {

            if (prevPB == null)
                return;
            PictureBox pb = prevPB;

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

        private void reset_Click(object sender, EventArgs e)
        {

            FileInfo fi = null;
            if (globalBtn.Text.Equals("Videos"))
            {
                fi = new FileInfo(mainDi.FullName);
            }

            try { if (fi != null) { File.Delete(fi.FullName + "\\priority.txt"); isRefresh = true; videosBtn_Click(null, null); isRefresh = false; } } catch { }
        }

        private void load4K_Click(object sender, EventArgs e)
        {
            if (globalBtn.Text.Equals("4K"))
            {
                loadAll = !loadAll;
            }
            else
                return;
            isRefresh = true;
            fourKBtn_Click(null, null);
            isRefresh = false;
        }


        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
        }

        private void button3_MouseMove(object sender, EventArgs e)
        {

            button3.ForeColor = mouseClickColor;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {

            button5.ForeColor = mouseClickColor;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {

            button5.ForeColor = Color.White;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {

            button4.ForeColor = mouseClickColor;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {

            button4.ForeColor = Color.White;
        }

        private void navController_Click(object sender, EventArgs e)
        {
            ctrl = !ctrl;
            divider.BackColor = ctrl == true ? mouseClickColor : kindaDark;
        }

        private void rcVid_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;
                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                isRefresh = true;
                videosBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void rcPics_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                isRefresh = true;
                picturesBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void rc4K_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\kkkk\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                isRefresh = true;
                fourKBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void rcSVid_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\GifVideos\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                isRefresh = true;
                shortVideosBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void rcGifs_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\Gifs\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                isRefresh = true;
                gifsBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void rcAffinity_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DirectoryInfo stackDi = null;

                controlDisposer();
                stackDi = new DirectoryInfo(mainDi.FullName + "\\Pics\\Affinity\\ImgPB");
                foreach (FileInfo fi in stackDi.GetFiles())
                    try { fi.Delete(); } catch { }
                isRefresh = true;
                gifsBtn_Click(null, null);
                isRefresh = false;
            }
        }

        private void toolStripMenuItem48_Click(object sender, EventArgs e)
        {
            Label pb = (Label)contextMenuStrip1.SourceControl;
            Process.Start(Directory.GetParent(pb.Name).FullName);
        }

        private void toolStripMenuItem49_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip2.SourceControl;
            Process.Start(Directory.GetParent(pb.Name).FullName);
        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip3.SourceControl;
            Process.Start(Directory.GetParent(pb.Name).FullName);
        }

        private void toolStripMenuItem70_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip4.SourceControl;
            Application.RemoveMessageFilter(this);
            PopUpTextBox popUpTextBox = new PopUpTextBox();
            popUpTextBox.Location = new Point(pb.Location.X + flowLayoutPanel1.Location.X + pb.Width/2, pb.Location.Y + flowLayoutPanel1.Location.Y);
            popUpTextBox.TopMost = true;
            popUpTextBox.ShowDialog();
            if (popUpTextBox.fileName.Length > 0)
            {
                String webLinks = File.ReadAllText(mainDi + "\\webLinks.txt");
                int k = 0;
                while (Directory.Exists(mainDi + "\\Online\\" + mainDi.Name + k))
                {
                    k++;
                }
                Directory.CreateDirectory(mainDi + "\\Online\\" + (popUpTextBox.name.Length > 0 ? popUpTextBox.name : (mainDi.Name + k)));
                webLinks = webLinks + "\n" + (popUpTextBox.name.Length > 0 ? popUpTextBox.name : (mainDi.Name + k)) + "@" + popUpTextBox.fileName;
                File.WriteAllText(mainDi + "\\webLinks.txt", webLinks);
                refreshToolStripMenuItem_Click(null, null);
            }
            Application.AddMessageFilter(this);
        }

        private void toolStripMenuItem51_Click(object sender, EventArgs e)
        {
            PictureBox pb = null;
            try {
                pb = (PictureBox)(contextMenuStrip4.SourceControl);
            }
            catch {}

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";

            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (String fi in ofd.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(fi);
                    for (int k = 0; k < 50; k++)
                    {
                        if (File.Exists(mainDi + "\\Online\\" + pb.Name + "\\" + k + fileInfo.Name))
                            continue;
                        File.Move(fi, mainDi + "\\Online\\" + pb.Name + "\\" + k +fileInfo.Name);
                        break;
                    }
                }
            }
        }

        private void toolStripMenuItem62_Click(object sender, EventArgs e)
        {
            PictureBox pb = null;
            try
            {
                pb = (PictureBox)(contextMenuStrip4.SourceControl);
            }
            catch { }

            String[] strArray = File.ReadAllLines(mainDi.FullName + "\\webLinks.txt");
            String finalized = "";
            foreach(String str in strArray)
            {
                if (str.Contains(pb.Name + "@"))
                    continue;
                finalized = finalized + str + "\n";
            }
            File.WriteAllText(mainDi.FullName + "\\webLinks.txt", finalized);
            if(Directory.Exists(mainDi.FullName + "\\Online\\" + pb.Name))
            {
                Directory.Delete(mainDi.FullName + "\\Online\\" + pb.Name);
            }
            refreshToolStripMenuItem_Click(null, null);
        }

        private void toolStripMenuItem53_Click(object sender, EventArgs e)
        {
            try
            {
                Label pb = (Label)contextMenuStrip1.SourceControl;
                FileInfo fi = new FileInfo(pb.Name);

                File.Move(fi.FullName, fi.DirectoryName + "\\Pics\\Affinity\\" + fi.Name);
                pb.Image.Dispose();
                pb.Dispose();
                GC.Collect();
            }
            catch { return; }

        }

        private void flowLayoutPanel2_MouseEnter(object sender, EventArgs e)
        {
        }

        private void divider_MouseEnter(object sender, EventArgs e)
        {
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
        {
            if (e.nButton == 2)
                return;

            if (prevPb.Name.EndsWith(".txt"))
            {
                Application.RemoveMessageFilter(this);
                List<PictureBox> tempList = new List<PictureBox>(), tempPlayListPb = new List<PictureBox>();
                List<String> dir = File.ReadAllLines(prevPb.Name).ToList();
                if (dir.Count == 0)
                    return;

                foreach (String file in dir)
                {
                    PictureBox tempPb = new PictureBox();
                    tempPb.Name = file;
                    tempPb.Image = setDefaultPic(new FileInfo(file), tempPb);
                    if (tempPb.Image != null) tempPb.Image.Dispose();
                    tempList.Add(tempPb);
                }

                foreach (String file in Directory.GetFiles(prevPb.Name.Substring(0, prevPb.Name.LastIndexOf("\\"))))
                {
                    PictureBox tempPb = new PictureBox();
                    String baseFile = "";
                    foreach (String str in File.ReadAllLines(file).ToList())
                    {
                        if (str.Trim().Length > 0)
                        {
                            baseFile = str.Trim();
                            break;
                        }
                    }
                    if (baseFile.Length == 0)
                        continue;
                    tempPb.Name = file;
                    tempPb.Image = setDefaultPic(new FileInfo(baseFile), tempPb);
                    if (tempPb.Image != null) tempPb.Image.Dispose();

                    if (file.Substring(file.LastIndexOf("\\") + 1).StartsWith("Games "))
                    {
                        if (file.Substring(file.LastIndexOf("\\") + 1).Split('~')[0].Equals(prevPb.Name.Substring(prevPb.Name.LastIndexOf("\\") + 1).Split('~')[0]))
                            tempPlayListPb.Add(tempPb);
                    }
                    else
                        if (!prevPb.Name.Substring(prevPb.Name.LastIndexOf("\\") + 1).StartsWith("Games "))
                        tempPlayListPb.Add(tempPb);
                }

                this.Hide();
                Explorer.wmp.Location = new Point(0, 28);
                if (prevPb.Name.Substring(prevPb.Name.LastIndexOf("\\") + 1).StartsWith("Games "))
                    Explorer.wmp.repeat = false;
                List<String> files = File.ReadAllLines(prevPb.Name).ToList();
                Explorer.wmp.setRefPb(prevPb, tempList, this, tempPlayListPb);
                Explorer.wmp.axWindowsMediaPlayer1.URL = files.ElementAt(randNo);
                Explorer.wmp.axWindowsMediaPlayer1.Name = files.ElementAt(randNo);
                Explorer.wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                Explorer.wmp.Show();
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            else if (isHoveredOverPb)
            {
                Application.RemoveMessageFilter(this);

                this.Hide();
                Explorer.wmp.Location = new Point(0, 28);
                Explorer.wmp.setRefPb(prevPb, type == "Videos"?videosPb:shortVideosPb, this);
                Explorer.wmp.axWindowsMediaPlayer1.URL = prevPb.Name;
                Explorer.wmp.axWindowsMediaPlayer1.Name = prevPb.Name;
                Explorer.wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                Explorer.wmp.Show();
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (whereAtMp >= 9.0) whereAtMp = 1.0;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (whereAtMp / 9.0) * inWmpDuration;
            newProgressBarNew.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            whereAtMp++;
        }

        private void button10_MouseEnter(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem56_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)contextMenuStrip5.SourceControl;
            try
            {
                File.Delete(pb.Name);
                refreshToolStripMenuItem_Click(null, null);
            }
            catch { }
        }

        private void shortVideosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Label pb = (Label)contextMenuStrip1.SourceControl;
                FileInfo fi = new FileInfo(pb.Name);

                File.Move(fi.FullName, fi.DirectoryName + "\\Pics\\GifVideos\\" + fi.Name);
                pb.Image.Dispose();
                pb.Dispose();
                GC.Collect();
            }
            catch { return; }
        }

        private void toolStripMenuItem58_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Thread convert = new Thread(() =>
                {
                    try
                    {
                        Label pb = (Label)contextMenuStrip1.SourceControl;
                        FileInfo file = new FileInfo(pb.Name);
                        var inputFile = new MediaFile { Filename = pb.Name };
                        var outputFile = new MediaFile { Filename = file.DirectoryName + "\\converted_" + file.Name.Substring(file.Name.IndexOf("placeholdeerr") + 13) };

                        var conversionOptions = new ConversionOptions
                        {
                            VideoSize = VideoSize.Hd720
                        };

                        using (var engine = new Engine())
                        {
                            engine.Convert(inputFile, outputFile, conversionOptions);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to compress!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                });
                convert.Start();
            }
        }

        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Thread convert = new Thread(() =>
                {
                    try
                    {

                        Label pb = (Label)contextMenuStrip1.SourceControl;
                        FileInfo file = new FileInfo(pb.Name);
                        var inputFile = new MediaFile { Filename = pb.Name };
                        var outputFile = new MediaFile { Filename = file.DirectoryName + "\\converted_" + file.Name.Substring(file.Name.IndexOf("placeholdeerr") + 13) };

                        var conversionOptions = new ConversionOptions
                        {
                            VideoSize = VideoSize.Hd480
                        };

                        using (var engine = new Engine())
                        {
                            engine.Convert(inputFile, outputFile, conversionOptions);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to compress!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                });
                convert.Start();
            }

        }

        private void toolStripMenuItem60_Click(object sender, EventArgs e)
        {
            Label pb = (Label)contextMenuStrip1.SourceControl;

            FileInfo fi = new FileInfo(pb.Name);

            DialogResult result = MessageBox.Show("Are you sure?", "Converting to Gif", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Thread convert = new Thread(() =>
                {
                    try
                    {
                        var inputFile = new MediaFile { Filename = fi.FullName };
                        var outputFile = new MediaFile { Filename = pb.Name.Substring(0, pb.Name.LastIndexOf("\\")).Replace("\\Pics\\GifVideos", "").Replace("\\Pics\\Affinity", "") + "\\Pics\\Gifs\\1" + fi.Name.Substring(fi.Name.IndexOf("placeholdeerr") + 13).Substring(0, fi.Name.Substring(fi.Name.IndexOf("placeholdeerr") + 13).LastIndexOf('.')) + ".gif" };

                        using (var eng = new Engine())
                        {
                            eng.Convert(inputFile, outputFile);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to convert!", "Failed gif convert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                });
                convert.Start();
            }
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {

        }

        private void setDp_Click(object sender, EventArgs e)
        {
            if (prevPB == null) return;

            if (globalBtn.Text.Equals("Pictures") || globalBtn.Text.Equals("4K"))
            {
                PictureBox pb = prevPB;
                File.WriteAllText(mainDi.FullName + "\\disPic.txt", pb.Name);
                toRefresh = true;
                imagePBLink = pb.Name;
            }
            else if (globalBtn.Text.Equals("Gifs"))
            {
                PictureBox pb = prevPB;

                if (File.Exists(mainDi.FullName + "\\disGifPic.txt"))
                {
                    String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\disGifPic.txt");
                    Boolean doesExist = false;
                    String fileStr = "";
                    foreach (String str in resumeFile)
                    {
                        if (str.Contains(pb.Name))
                        {
                            doesExist = true;
                            return;
                        }
                    }

                    if (!doesExist)
                    {
                        int max = resumeFile.Length >= 3 ? 2 : resumeFile.Length;
                        fileStr = pb.Name + "\n";

                        for (int i = 0; i < max; i++)
                        {
                            fileStr = fileStr + resumeFile[i] + "\n";
                        }
                    }
                    File.WriteAllText(mainDi.FullName + "\\disGifPic.txt", fileStr);
                }
            }
        }

        private void resetAndSet_Click(object sender, EventArgs e)
        {
            if (prevPB == null) return;

            if (globalBtn.Text.Equals("Pictures") || globalBtn.Text.Equals("4K"))
            {
                PictureBox pb = prevPB;
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
                        try { ResizeImage(fi.FullName, fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1), 308, 0, 513, 0); } catch { return; }
                        String newFileName = renameFile(i, new FileInfo(fi.DirectoryName + "\\_resized" + i + fi.Name.Substring(fi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1)));
                        File.WriteAllText(mainDi.FullName + "\\disPic.txt", newFileName);
                        imagePBLink = newFileName;
                        break;
                    }
                }
                toRefresh = true;
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {

            if (prevPB == null) return;
            PictureBox pb = prevPB;
            Application.RemoveMessageFilter(this);
            if (pb.Image.Width > pb.Image.Height || type == "Gifs")
            {
                PicViewer picViewer = new PicViewer(pb.Name, this, type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), 1920, 1080, type == "Gifs" ? true : false);
                //picViewer.picPanel.Dock = DockStyle.Fill;
                //picViewer.flowLayoutPanel1.Dock = DockStyle.Bottom;
                picViewer.flowLayoutPanel1.Size = new Size(1840, 110);
                picViewer.originalSize = new Size(1840, 110);
                picViewer.flowLayoutPanel1.Location = new Point(40, 955);
                picViewer.originalPoint = new Point(40, 955);
                picViewer.flowLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
                picViewer.flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.flowLayoutPanel1.Width, picViewer.flowLayoutPanel1.Height, 15, 15));
                picViewer.fillUpFP1(type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), true);
                picViewer.hideBtnW.Visible = true;
                picViewer.hideBtnW.Location = new Point(960 - (picViewer.hideBtnH.Width / 2), 1070);
                picViewer.editBtnH.Location = new Point(1909, 540 - (picViewer.hideBtnH.Height / 2));
                picViewer.editBtnH.Visible = true;
                picViewer.editPanel.Location = new Point(1905 - (picViewer.editPanel.Width), 540 - (picViewer.editPanel.Height / 2));
                picViewer.editPanel.Visible = true;
                picViewer.label1.Location = new Point(1918 - (picViewer.label1.Width), 2);
                picViewer.label1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.label1.Width, picViewer.label1.Height, 10, 10));
                picViewer.label1.Visible = true;
                picViewer.Show();
                /*wmpSide1 = new wmpSide(null, picViewer, true);
                wmpSide1.BackColor = darkBackColor;
                wmpSide1.Location = new Point(0, 970);
                wmpSide1.fillUpFP1(type == "4K" ? wpbDupe : (type == "Gifs" ? gpb : wpb), true);
                wmpSide1.Size = new Size(1920, 110);*/
            }
            else
            {
                PicViewer picViewer = new PicViewer(pb.Name, this, type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), 1920, 1080, false);
                //picViewer.picPanel.Dock = DockStyle.Fill;
                //picViewer.flowLayoutPanel1.Dock = DockStyle.Left;
                picViewer.flowLayoutPanel1.Size = new Size(170, 1030);
                picViewer.originalSize = new Size(170, 1030);
                picViewer.flowLayoutPanel1.Location = new Point(15, 25);
                picViewer.originalPoint = new Point(15, 25);
                picViewer.flowLayoutPanel1.Padding = new Padding(0, 6, 0, 0);
                picViewer.flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.flowLayoutPanel1.Width, picViewer.flowLayoutPanel1.Height, 15, 15));
                picViewer.fillUpFP1(type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), false);
                picViewer.hideBtnH.Visible = true;
                picViewer.hideBtnH.Location = new Point(-2, 540 - (picViewer.hideBtnH.Height / 2));
                picViewer.editBtnH.Location = new Point(1909, 540 - (picViewer.hideBtnH.Height / 2));
                picViewer.editBtnH.Visible = true;
                picViewer.editPanel.Location = new Point(1905 - (picViewer.editPanel.Width), 540 - (picViewer.editPanel.Height / 2));
                picViewer.editPanel.Visible = true;
                picViewer.label1.Location = new Point(1918 - (picViewer.label1.Width), 2);
                picViewer.label1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, picViewer.label1.Width, picViewer.label1.Height, 10, 10));
                picViewer.label1.Visible = true;
                picViewer.Show();
                /*wmpSide1 = new wmpSide(null, picViewer, true);
                wmpSide1.Size = new Size(120, 1080);
                wmpSide1.BackColor = darkBackColor;
                wmpSide1.Location = new Point(0, 0);
                wmpSide1.fillUpFP1(type == "4K" ? lpbDupe : (type == "Gifs" ? gpb : lpb), false);*/

            }
        }

        private void toolStripMenuItem47_Click(object sender, EventArgs e)
        {
            Label pb = (Label)contextMenuStrip1.SourceControl;
            if (File.Exists(mainDi.FullName + "\\resume.txt"))
            {
                String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\resume.txt");
                FileInfo fi = new FileInfo(pb.Name);
                isShort = false;
                foreach (String str in resumeFile)
                    if (str.Contains("@@" + fi.Name + "@@!"))
                    {
                        Application.RemoveMessageFilter(this);

                        this.Hide();
                        Explorer.wmp.Location = new Point(0, 28);
                        Explorer.wmp.setRefPb(prevPb, videosPb, this);
                        Explorer.wmp.axWindowsMediaPlayer1.URL = pb.Name;
                        Explorer.wmp.axWindowsMediaPlayer1.Name = pb.Name;
                        Explorer.wmp.calculateDuration(Double.Parse(str.Substring(str.IndexOf("@@!") + 3)));
                        Explorer.wmp.Show();
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                    }
            }
        }

        private void picturesBtn_MouseLeave(object sender, EventArgs e)
        {
            if (globalBtn != null && !globalBtn.Text.Equals("Pictures"))
                picturesBtn.ForeColor = Color.White;
            enlargeLeave((Button)sender, "Pictures");
        }

        private void expBtn_MouseEnter(object sender, EventArgs e)
        {
            hoverPointer.Visible = false;
            expBtn.ForeColor = mouseClickColor;
            try
            {
                expBtn.Image = largeImages[expBtn.Text];
                expBtn.ForeColor = mouseClickColor;
                Padding p = expBtn.Margin;
                expBtn.Region = null;
                expBtn.Size = new Size(expBtn.Width + 16, expBtn.Height + 8);
                expBtn.Margin = new Padding(p.Left - 8, p.Top - 4, p.Right, p.Bottom);
                expBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, expBtn.Width, expBtn.Height, 25, 25));
            }
            catch { }
            hoverPointer.Location = new Point(0, expBtn.Location.Y + ((expBtn.Size.Height - hoverPointer.Size.Height) / 2) - 3);
            hoverPointer.Visible = true;
        }

        private void expBtn_MouseLeave(object sender, EventArgs e)
        {
            hoverPointer.Visible = false;
            expBtn.ForeColor = Color.White; try
            {
                expBtn.Image = miniImages[expBtn.Text] != null ? miniImages[expBtn.Text] : null;
                Padding p = expBtn.Margin;
                expBtn.Region = null;
                expBtn.Margin = new Padding(p.Left + 8, p.Top + 4, p.Right, p.Bottom);
                expBtn.Size = new Size(expBtn.Width - 16, expBtn.Height - 8);
                expBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, expBtn.Width, expBtn.Height, 25, 25));
            }
            catch { }
        }

        private void fourKBtn_MouseLeave(object sender, EventArgs e)
        {
            hoverPointer.Visible = false;
            if (globalBtn != null && !globalBtn.Text.Equals("4K"))
                fourKBtn.ForeColor = Color.White;
            enlargeLeave((Button)sender, "4K");
        }

        private void shortVideosBtn_MouseLeave(object sender, EventArgs e)
        {
            hoverPointer.Visible = false;
            if (globalBtn!=null && !globalBtn.Text.Equals("Gif Vid"))
                shortVideosBtn.ForeColor = Color.White;
            enlargeLeave((Button)sender, "Gif Vid");
        }

        private void gifsBtn_MouseLeave(object sender, EventArgs e)
        {
            hoverPointer.Visible = false;
            if (globalBtn != null && !globalBtn.Text.Equals("Gifs"))
                gifsBtn.ForeColor = Color.White;
            enlargeLeave((Button)sender, "Gifs");
            /*Button btn = (Button)sender;
            int width = btn.Size.Width;
            int height = btn.Size.Height;
            btn.Size = new Size(width - 20, height - 12);
            btn.Location = new Point(btn.Location.X + 10, btn.Location.Y + 6);
            btn.Update();*/

            /*Padding p = gifsBtn.Margin;
            gifsBtn.Size = new Size(217,117);
            gifsBtn.Margin = new Padding(p.Left + 7, p.Top, p.Right + 7, p.Bottom);*/
        }

        private void gifsBtn_MouseHover(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int width = btn.Size.Width;
            int height = btn.Size.Height;
            btn.Size = new Size(width + 20, height + 12);
            btn.Update();
            btn.Location = new Point(btn.Location.X - 10, btn.Location.Y - 6);
            Padding p = gifsBtn.Margin;
            //gifsBtn.Size = new Size(231, 123);
            //gifsBtn.Margin = new Padding(p.Left - 20, p.Top, p.Right, p.Bottom);
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
                                flowLayoutPanel1.Controls.Remove(delPb);
                            }
                            catch { }
                        }
                        //refreshFolder();
                    }
                }
                catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Text = "" + mainDi.Name; prevPB = null;
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
            button4.Text = "" + nextFi.Name; disposePb.Clear();

            playListDi = new DirectoryInfo(mainDi + "\\PlayLists");
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());

                    button3.Tag = img1;
                    disposePb.Add(img1);
                    if (button3.Image != null)
                        button3.Image.Dispose();
                    button3.Image = resizedImage(img1, 0, 50, 0, 0);
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

                    if (button4.Image != null)
                        button4.Image.Dispose();
                    button4.Image = resizedImage(img2, 0, 50, 0, 0);
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
            isRefresh = true;
            if (globalBtn != null) globalBtn.BackColor = lightBackColor;
            loadMiniImages();
            VideoPlayer_Load(null, null);
            isRefresh = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Text = "" + mainDi.Name; prevPB = null;
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
            button3.Text = "" + prevfi.Name;
            globalBtn.BackColor = darkBackColor; disposePb.Clear();

            playListDi = new DirectoryInfo(mainDi + "\\PlayLists");
            if (File.ReadAllText(prevfi.FullName + "\\disPic.txt") != "")
            {
                try
                {
                    Image img1 = Image.FromFile(File.ReadAllText(prevfi.FullName + "\\disPic.txt").Trim());
                    button3.Tag = img1;
                    disposePb.Add(img1);


                    if (button3.Image != null)
                        button3.Image.Dispose();
                    button3.Image = resizedImage(img1, 0, 50, 0, 0);
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

                    if (button4.Image != null)
                        button4.Image.Dispose();
                    button4.Image = resizedImage(img2, 0, 50, 0, 0);
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
            isRefresh = true;
            loadMiniImages();
            VideoPlayer_Load(null, null);
            if (globalBtn != null)
            {
                globalBtn.BackColor = lightBackColor;
            }
            isRefresh = false;
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {

            if (prevFlowLayoutPanel != null)
            {
                foreach (Control tempPanel1 in prevFlowLayoutPanel.Parent.Controls)
                {
                    tempPanel1.Margin = new Padding(15, 0, 0, 0);
                }

                prevVidDetails.Size = new Size((type == "Gif Vid" || type == "Affinity") ? 386:(axWindowsMediaPlayer1.Width == 462 ? 391:523), (type == "Gif Vid" || type == "Affinity") ? 45 : (axWindowsMediaPlayer1.Width == 462 ? 40 : 48));
                prevVidDetails.ForeColor = Color.White;
                prevFlowLayoutPanel.Region = null;
                prevFlowLayoutPanel.Size = new Size((type == "Gif Vid" || type == "Affinity") ? 386 : (axWindowsMediaPlayer1.Width == 462 ? 391 : 523), (type == "Gif Vid" || type == "Affinity") ? 267 : (axWindowsMediaPlayer1.Width == 462 ? 264 : 348));
                prevFlowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, prevFlowLayoutPanel.Width, prevFlowLayoutPanel.Height, 12, 12));
                prevVidDetails.Padding = new Padding(0, 3, 0, 0);
                prevFlowLayoutPanel.Controls.Clear();
                prevFlowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width - 23, (type == "Gif Vid" || type == "Affinity" || axWindowsMediaPlayer1.Width == 462) ? 279 : 361);

                prevFlowLayoutPanel.Controls.Clear();

                prevFlowLayoutPanel.Controls.Add(prevPb);
                prevFlowLayoutPanel.Controls.Add(prevVidDetails);
                prevFlowLayoutPanel = null;
                isHoveredOverPb = false;
                timer1.Stop();
            }
            GC.Collect();
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            sortBySize = true;
            sortByDate = false;
            bySort = true;
            isRefresh = true;
            if (type == "Pictures")
                picturesBtn_Click(null, null);
            else if (type == "4K")
                fourKBtn_Click(null, null);
            else if (type == "Gif Vid")
                shortVideosBtn_Click(null, null);
            else if (type == "Affinity")
                bsButton_Click(null, null);
            else if (type == "Gifs")
                gifsBtn_Click(null, null);
            isRefresh = false;
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
            isRefresh = true;
            fourKBtn_Click(null, null);
            isRefresh = false;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

            FileInfo fi = null;
            if (globalBtn.Text.Equals("Videos"))
            {
                fi = new FileInfo(mainDi.FullName);
            }

            try { if (fi != null) { File.Delete(fi.FullName + "\\priority.txt"); isRefresh = true;  videosBtn_Click(null, null); isRefresh = false; } } catch { }
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label pb = (Label)contextMenuStrip1.SourceControl;

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
            foreach(Image img in miniImages.Values)
            {
                img.Dispose();
            }
            foreach (Image img in largeImages.Values)
            {
                img.Dispose();
            }
            if (button3.Image != null)
                button3.Image.Dispose();
            if (button4.Image != null)
                button4.Image.Dispose();
            Controls.Clear();
            controlDisposer();
            axWindowsMediaPlayer1.Dispose();

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

        public Image setDefaultPic(FileInfo fi, params PictureBox[] picBox)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(fi.DirectoryName + "\\imgPB");

            String fileName = fi.Name;
            if (!File.Exists(fi.FullName))
                return null;

            if (File.Exists(di.FullName + "\\resized_" + fi.Name + ".jpg"))
            {
                if (File.Exists(di.FullName + "\\" + fi.Name + "_1.jpg") && !File.Exists(di.FullName + "\\resized_" + fi.Name + "_1.jpg"))
                {
                    ResizeImage(di.FullName + "\\" + fi.Name + "_1.jpg", di.FullName + "\\resized_" + fileName + "_1.jpg", 0, 292, 515, 0);
                    try
                    {
                        File.Delete(di.FullName + "\\" + fi.Name + "_1.jpg");
                    }
                    catch { }
                }

                if (File.Exists(di.FullName + "\\" + fi.Name + ".jpg"))
                {
                    try
                    {
                        File.Delete(di.FullName + "\\resized_" + fi.Name + ".jpg");
                    }
                    catch { }
                }
                else
                {
                    bool hasSecPic = false;
                    if (File.Exists(di.FullName + "\\resized_" + fi.Name + "_1.jpg"))
                        hasSecPic = true;
                    Random r = new Random();
                    int temp = r.Next(2);
                    if (picBox.Length > 0)
                        picBox[0].ImageLocation = di.FullName + "\\resized_" + fi.Name + (temp == 0 || !hasSecPic ? "" : "_1") + ".jpg";
                    return Image.FromFile(di.FullName + "\\resized_" + fi.Name + (temp == 0 || !hasSecPic ? "" : "_1") + ".jpg");
                }
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
                        if (type == "Gif Vid")
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
                        if(File.Exists(di.FullName + "\\resized_" + fileName + ".jpg"))return Image.FromFile(di.FullName + "\\resized_" + fileName + ".jpg");
                        return null;
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
                try
                {
                    engine.GetThumbnail(inputFile, outputFile, options);
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
                try
                {
                    this.Close();
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    Environment.Exit(0);
                }
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
                    ctrl = !ctrl;
                    divider.BackColor = ctrl == true ? mouseClickColor : kindaDark;
                }
                else if (Control.ModifierKeys == Keys.Control)
                {
                    if (mutltiSelect)
                    {
                        foreach (PictureBox pb in deletePb)
                        {
                            pb.BackColor = darkBackColor;
                        }
                        if (prevPB != null)
                        {
                            prevPB.BackColor = selectedPbColor;
                            deletePb.Add(prevPB);
                        }
                    }
                    mutltiSelect = !mutltiSelect;
                    button1.Text = trackBar1.Value.ToString();
                }
                else if (ctrl && keyCode == Keys.Up)
                {
                    int idx = typeButtons.IndexOf(globalBtn);
                    isEnlarged[globalBtn.Text] = false;
                    enlargeLeave(globalBtn, type);
                    globalBtn.ForeColor = Color.White;
                    globalBtn = null;
                    idx = idx - 1 < 0 ? typeButtons.Count - 1 : idx - 1;
                    type = typeButtons.ElementAt(idx).Text;
                    VideoPlayer_Load(null, null);
                    isEnlarged[globalBtn.Text] = false;
                    enlargeEnter(globalBtn);
                    isEnlarged[globalBtn.Text] = true;
                }
                else if (ctrl && keyCode == Keys.Down)
                {
                    int idx = typeButtons.IndexOf(globalBtn);
                    isEnlarged[globalBtn.Text] = false;
                    enlargeLeave(globalBtn, type);
                    globalBtn.ForeColor = Color.White;
                    globalBtn = null;
                    idx = idx + 1 >= typeButtons.Count ? 0 : idx + 1;
                    type = typeButtons.ElementAt(idx).Text;
                    VideoPlayer_Load(null, null);
                    isEnlarged[globalBtn.Text] = false;
                    enlargeEnter(globalBtn);
                    isEnlarged[globalBtn.Text] = true;
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
                    {
                        Application.RemoveMessageFilter(this);

                        this.Hide();
                        Explorer.wmp.Location = new Point(0, 28);
                        Explorer.wmp.setRefPb(prevPB, videosPb, this);
                        Explorer.wmp.axWindowsMediaPlayer1.URL = prevPB.Name;
                        Explorer.wmp.axWindowsMediaPlayer1.Name = prevPB.Name;

                        Double currPosition = (1.0 / 9.0) * inWmpDuration;

                        if (File.Exists(mainDi.FullName + "\\resume.txt"))
                        {
                            String[] resumeFile = File.ReadAllLines(mainDi.FullName + "\\resume.txt");
                            FileInfo fi = new FileInfo(prevPB.Name);
                            foreach (String str in resumeFile)
                                if (str.Contains("@@" + fi.Name + "@@!"))
                                {
                                    currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                    break;
                                }
                        }

                        Explorer.wmp.calculateDuration(currPosition);
                        Explorer.wmp.Show();
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                    }
                }
                else if (keyCode == Keys.Escape || keyCode == Keys.Back)
                {

                    expBtn_Click(null, null);
                }
                else if (keyCode == Keys.Add)
                {
                    trackBar1.Value = trackBar1.Value + 2 > 100 ? 100 : trackBar1.Value + 2;
                    Explorer.globalVol = trackBar1.Value;
                    button1.Text = trackBar1.Value.ToString();
                }
                else if (keyCode == Keys.Subtract)
                {
                    trackBar1.Value = trackBar1.Value - 2 < 0 ? 0 : trackBar1.Value - 2;
                    Explorer.globalVol = trackBar1.Value;
                    button1.Text = trackBar1.Value.ToString();
                }
                else if (keyCode == Keys.M)
                {
                    trackBar1.Value = 0;
                    Explorer.globalVol = trackBar1.Value;
                    button1.Text = 0 + "";
                }
                else if (keyCode == Keys.Delete)
                {
                    if (type == "Videos" || type == "Gif Vid" || type == "Affinity")
                        toolStripMenuItem1_Click(null, null);
                }
                else if (keyCode == Keys.NumPad1 || keyCode == Keys.NumPad6 || keyCode == Keys.NumPad2 || keyCode == Keys.NumPad3 || keyCode == Keys.NumPad4 || keyCode == Keys.NumPad5)
                {
                    isEnlarged[globalBtn.Text] = false;
                    enlargeLeave(globalBtn, type);
                    if (keyCode == Keys.NumPad1)
                    {
                        int idx = 0;
                        globalBtn.ForeColor = Color.White;
                        globalBtn = null;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad2)
                    {
                        int idx = 1;
                        globalBtn.ForeColor = Color.White;
                        globalBtn = null;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad3)
                    {
                        int idx = 2;
                        globalBtn.ForeColor = Color.White;
                        globalBtn = null;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad4)
                    {
                        int idx = 3;
                        globalBtn.ForeColor = Color.White;
                        globalBtn = null;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad5)
                    {
                        int idx = 4;
                        globalBtn.ForeColor = Color.White;
                        globalBtn = null;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }
                    else if (keyCode == Keys.NumPad6)
                    {
                        int idx = 5;
                        globalBtn.ForeColor = Color.White;
                        globalBtn = null;
                        type = typeButtons.ElementAt(idx).Text;
                        VideoPlayer_Load(null, null);
                    }

                    isEnlarged[globalBtn.Text] = false;
                    enlargeEnter(globalBtn);
                    isEnlarged[globalBtn.Text] = true;
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
                    globalDetails.BackColor = darkBackColor;
                    prevPB = videosPb.ElementAt(0);
                    prevPB.BackColor = mouseClickColor;
                    //allVidDet.ElementAt(0).ForeColor = mouseClickColor;
                    Font myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    allVidDet.ElementAt(0).BackColor = mouseClickColor;
                    globalDetails.Font = myfont;
                    return;
                }
                prevPB.BackColor = lightBackColor;
                int index = videosPb.IndexOf(prevPB) + idx;
                if (index < 0)
                {
                    globalDetails.ForeColor = Color.White;
                    globalDetails.BackColor = darkBackColor;
                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(videosPb.Count - 1);
                    prevPB = videosPb.ElementAt(videosPb.Count - 1);
                    //allVidDet.ElementAt(videosPb.Count - 1).ForeColor = mouseClickColor;
                    allVidDet.ElementAt(videosPb.Count - 1).BackColor = mouseClickColor;
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    index = videosPb.Count - 1;
                }
                else if (index >= videosPb.Count)
                {
                    globalDetails.ForeColor = Color.White;
                    globalDetails.BackColor = darkBackColor;
                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(0);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    prevPB = videosPb.ElementAt(0);
                    //allVidDet.ElementAt(0).ForeColor = mouseClickColor;
                    allVidDet.ElementAt(0).BackColor = mouseClickColor;
                    index = 0;
                }
                else
                {
                    globalDetails.ForeColor = Color.White;
                    globalDetails.BackColor = darkBackColor;
                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(index);
                    myfont = new Font("Comic Sans MS", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    prevPB = videosPb.ElementAt(index);
                    //allVidDet.ElementAt(index).ForeColor = mouseClickColor;
                    allVidDet.ElementAt(index).BackColor = mouseClickColor;
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
                prevPB.BackColor = darkBackColor;
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
                    allVidDet.ElementAt(0).BackColor = mouseClickColor;
                    prevPB = shortVideosPb.ElementAt(0);
                    prevPB.BackColor = mouseClickColor;
                    Font myfont = new Font("Comic Sans MS", 8, FontStyle.Bold);
                    globalDetails.Font = myfont;
                    return;
                }
                prevPB.BackColor = lightBackColor;
                int index = shortVideosPb.IndexOf(prevPB) + idx;
                if (index < 0)
                {
                    globalDetails.BackColor = lightBackColor;

                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    index = shortVideosPb.Count - 1;
                    prevPB = shortVideosPb.ElementAt(shortVideosPb.Count - 1);
                    globalDetails = allVidDet.ElementAt(shortVideosPb.Count - 1);
                    myfont = new Font("Comic Sans MS", 8, FontStyle.Bold);
                    globalDetails.Font = myfont;
                    allVidDet.ElementAt(shortVideosPb.Count - 1).BackColor = mouseClickColor;
                }
                else if (index >= shortVideosPb.Count)
                {
                    index = 0;
                    prevPB = shortVideosPb.ElementAt(0);
                    globalDetails.BackColor = lightBackColor;
                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(index);
                    myfont = new Font("Comic Sans MS", 8, FontStyle.Bold);
                    globalDetails.Font = myfont;
                    allVidDet.ElementAt(index).BackColor = mouseClickColor;
                }
                else
                {
                    prevPB = shortVideosPb.ElementAt(index);
                    globalDetails.BackColor = lightBackColor;
                    Font myfont = new Font("Segoe UI", 9, FontStyle.Regular);
                    globalDetails.Font = myfont;
                    globalDetails = allVidDet.ElementAt(index);
                    myfont = new Font("Comic Sans MS", 8, FontStyle.Bold);
                    globalDetails.Font = myfont;
                    allVidDet.ElementAt(index).BackColor = mouseClickColor;
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
                prevPB.BackColor = darkBackColor;
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
