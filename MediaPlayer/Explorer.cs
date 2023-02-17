using AxWMPLib;
using MediaToolkit;
using MediaPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Imaging;
using MediaToolkit.Model;
using MediaToolkit.Options;
using WMPLib;

namespace MediaPlayer
{
    /*class MyRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected) base.OnRenderMenuItemBackground(e);
            else
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(Brushes.Beige, rc);
                e.Graphics.DrawRectangle(Pens.Black, 1, 0, rc.Width - 2, rc.Height - 1);
            }
        }
    }*/

    public partial class Explorer : Form
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

        public static Explorer staticExp = null;
        FlowLayoutPanel flowLayoutPanel = null;
        Label dupeLabel2 = null, playRandom = null, shorts = null, affLbl = null;
        List<FlowLayoutPanel> flowLayoutPanels = new List<FlowLayoutPanel>();
        public String searchText = "";
        CustomToolTip tip = null;
        public static DirectoryInfo[] pardirectory = new DirectoryInfo[2];
        List<Button> folderButt = new List<Button>(), typeButtons = new List<Button>();
        Label countFiles = new Label();
        Calculator calc;
        //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private Point flowPanel1Loc = new Point(288,44);
        private Size stackedSizePbDb = new Size(386, 219), unStackedSizePbDb = new Size(458, 260), stackedSizeDbBtn = new Size(673, 44), unStackedSizeDbBtn = new Size(780, 44)
            , stackedSizeRandBtn = new Size(217, 44), unStackedSizeRandBtn = new Size(258, 44);
        public static Boolean stackedDb = false, isHoveredOverPb = false, playedRandom = false, playedShorts = false, inDb = true;
        List<String> randomFilesPreloaded, shortFilesPreloaded, affinityFilePreloaded;
        List <PictureBox> tempRandomFilesPreloaded, tempShortFilesPreloaded, tempAffinityFilesPreloaded;
        public Button butt1 = new Button(), globalTypeButton = null;
        List<String> resumeFiles = new List<string>(), newFilesDb = new List<string>();
        public List<String> typeList = new List<String>();
        public static int globalVol = 15;
        public static List<String> dirs = new List<string>();
        public static List<String> delFiles = new List<String>();
        Dictionary<String, int> namePriorityPairs = new Dictionary<string, int>();
        Dictionary<String, List<String>> viewMap = new Dictionary<string, List<string>>();
        //public static Dictionary<String, Boolean> videoHoverEnabler = new Dictionary<String, Boolean>();
        public Dictionary<String, List<String>> allFoldersDict = new Dictionary<string, List<string>>();
        List<PictureBox> disposableBoxes = new List<PictureBox>();
        public Boolean useAndThrow = true, isGames = false;
        Button typeName = new Button();
        public static PictureBox selectedPb = null;
        List<Label> grpLabels = new List<Label>();
        Label globalLabel = null, sizeInfo = new Label();
        Boolean ctrl = false;
        List<String> folders = new List<string>();
        Boolean compact = true, dashBoardRefresh = true;
        public static WmpOnTop wmpOnTop = null;
        public static WmpOnTop wmpOnTopDummy = new WmpOnTop();
        public List<PictureBox> videosPb = new List<PictureBox>();
        List<Label> vidDetailsStack = new List<Label>();
        List<String> randomFiles = new List<string>();
        //PopupVideosPlayer popupVideosPlayer;
        //public static Random rr = new Random();
        //static int rand1 = Explorer.rr.Next(0, 256);
        //static int[] color = Rand_Color(rand1, 0.5, 0.25);
        public static int popUpY = 320;
        int padding = 0;
        Size globalSize = new Size(), globalSizePanel = new Size();
        Double inWmpDuration = 0, loc = 0, prevX = -12;
        NewProgressBar newProgressBar = new NewProgressBar();
        double whereAt = 1.0;
        public static WMP wmp = new WMP(null, null, null, null);
        public static Color globColor = Color.FromArgb(255, 128, 0);

        public static Color darkBackColor = Color.FromArgb(24, 24, 24);
        public static Color lightBackColor = Color.FromArgb(45, 45, 45);
        public static Color whiteBackColor = Color.FromArgb(255, 254, 232);
        public static Color kindaDark = Color.FromArgb(64, 64, 64);
        public static Color mouseHoverColor = Color.FromArgb(255, 128, 0);
        public static Color mouseClickColor = Color.FromArgb(255, 128, 0);
        public static Color selectedPbColor = Color.FromArgb(255, 128, 0);
        Dictionary<String, Image> miniImages = new Dictionary<String, Image>(), largeImages = new Dictionary<string, Image>();
        Dictionary<String, bool> isEnlarged = new Dictionary<string, bool>();
        FlowLayoutPanel prevFlowLayoutPanel = null;
        public PictureBox prevPb = null;

        WindowsMediaPlayer wmpDura = new WindowsMediaPlayerClass();
        Label prevVidDetails = null;

        DirectoryInfo gamesDirectory = new DirectoryInfo("E:\\Softwares\\Games");
        public static DirectoryInfo directory3 = new DirectoryInfo("I:\\ubuntu\\home\\xdm\\bin\\build");
        public static DirectoryInfo directory4 = new DirectoryInfo("G:\\softwares\\proteous\\DEMO to PRO\\BIN\\ETC\\build");
        ComponentResourceManager resources = null;
        private void enlargeEnter(Button b)
        {
            if (largeImages.Count == 0 || isEnlarged[b.Text])
                return;
            try
            {
                b.Image = largeImages[b.Text];
                b.ForeColor = mouseClickColor;
                Padding p = b.Margin;
                b.Region = null;
                b.Size = new Size(b.Width + 40, b.Height + 20);
                b.Margin = new Padding(p.Left - 20, p.Top, p.Right, p.Bottom);
                b.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 25, 25));
                //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top - 10, label1.Margin.Right, label1.Margin.Bottom);
            }
            catch { }
        }

        private void enlargeLeave(Button b, String typee)
        {
            if (miniImages.Count == 0 || isEnlarged[b.Text])
                return;
            try
            {
                b.Image = miniImages[typee];
                Padding p = b.Margin;
                b.Region = null;
                b.Margin = new Padding(p.Left + 20, p.Top, p.Right, p.Bottom);
                b.Size = new Size(b.Width - 40, b.Height - 20);
                b.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 25, 25));
                //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top + 10, label1.Margin.Right, label1.Margin.Bottom);
            }
            catch { }
        }

        private double GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return Math.Round(((double)drive.AvailableFreeSpace / 1000000000.0), 2);
                }
            }
            return -1;
        }

        public Explorer(Calculator calc, Boolean isGames)
        {
            this.calc = calc;
            InitializeComponent();
            newProgressBar.Value = 0;
            newProgressBar.BackColor = Color.White;
            newProgressBar.Margin = new Padding(0);
            this.backgroundWorker1.RunWorkerAsync(100);
            this.isGames = isGames;
            //timer.Interval = 800;
            this.Location = new Point(0, 0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            calcButton.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, calcButton.Width, calcButton.Height, 20, 20));
            //flowLayoutPanel3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel3.Width, flowLayoutPanel3.Height, 20, 20));
            resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayer));

            videos.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, videos.Width, videos.Height, 5, 5));
            pictures.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pictures.Width, pictures.Height, 5, 5));
            fourK.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, fourK.Width, fourK.Height, 5, 5));
            shortVideos.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, shortVideos.Width, shortVideos.Height, 5, 5));
            gifs.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, gifs.Width, gifs.Height, 5, 5));
            affinity.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, affinity.Width, affinity.Height, 5, 5));

            newFolder.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, newFolder.Width, newFolder.Height, 5, 5));
            //move.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, move.Width, move.Height, 5, 5));
            reset.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, reset.Width, reset.Height, 5, 5));
            refresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, refresh.Width, refresh.Height, 5, 5));
            navController.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, navController.Width, navController.Height, 5, 5));
            stack.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, stack.Width, stack.Height, 5, 5));
            delete.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, delete.Width, delete.Height, 5, 5));
            theme.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, theme.Width, theme.Height, 5, 5));

            dashBoard.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dashBoard.Width, dashBoard.Height, 5, 5));

            textBox3.Font = new Font("Segoe UI", 19, FontStyle.Regular);
            flowLayoutPanel1.AutoScroll = true;
            textBox3.Select();
            this.DoubleBuffered = true;
            hoverPointer.Visible = false;
            DirectoryInfo directory2 = new DirectoryInfo("C:\\Users\\Harsha Vardhan\\Downloads\\Video");
            DirectoryInfo directory1 = new DirectoryInfo("E:\\VS Code\\CSS");

            if (Directory.Exists("F:\\Calculator"))
            {
                directory1 = new DirectoryInfo("F:\\Calculator");
            }
            if (Directory.Exists("H:\\vivado\\rand_name\\rand_name.ir"))
            {
                directory2 = new DirectoryInfo("H:\\vivado\\rand_name\\rand_name.ir");
            }
            if (Directory.Exists(Explorer.directory3.FullName) && !File.Exists(Explorer.directory3.FullName + "\\resumeDb.txt"))
            {
                File.Create(Explorer.directory3.FullName + "\\resumeDb.txt");
            }
            if (Directory.Exists(Explorer.directory3.FullName) && !File.Exists(Explorer.directory3.FullName + "\\newFiles.txt"))
            {
                File.Create(Explorer.directory3.FullName + "\\newFiles.txt");
            }
            pardirectory[1] = directory1;
            pardirectory[0] = directory2;
            if (!File.Exists(directory1.FullName + "\\ThemeColor.txt"))
                File.Create(directory1.FullName + "\\ThemeColor.txt");

            if (Directory.Exists(gamesDirectory.FullName))
                foreach (DirectoryInfo directory in gamesDirectory.GetDirectories())
                    foreach (DirectoryInfo di in directory.GetDirectories())
                    {
                        if (!File.Exists(di.FullName + "\\disPic.txt"))
                            File.Create(di.FullName + "\\disPic.txt");
                    }

            if (Directory.Exists(directory3.FullName))
                foreach (DirectoryInfo di in directory3.GetDirectories())
            {
                if (!File.Exists(di.FullName + "\\disPic.txt"))
                    File.Create(di.FullName + "\\disPic.txt");
            }

            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.volume = 0;
            axWindowsMediaPlayer1.settings.setMode("loop", true);

            //wmp.Location = new Point(0, 28);

            typeList.Add("Gifs");
            typeList.Add("Gif Vid");
            typeList.Add("4K");
            typeList.Add("Pictures");
            typeList.Add("Videos");
            setTheme();

            sizeInfo.Text = pardirectory[0].FullName.Substring(0, pardirectory[0].FullName.IndexOf("\\") - 1) + ": " +
                (GetTotalFreeSpace(pardirectory[0].FullName.Substring(0, pardirectory[0].FullName.IndexOf("\\")) + "\\").ToString() + "GB")
                 + "     " + pardirectory[1].FullName.Substring(0, pardirectory[1].FullName.IndexOf("\\") - 1) + ": " +
                (GetTotalFreeSpace(pardirectory[1].FullName.Substring(0, pardirectory[1].FullName.IndexOf("\\")) + "\\").ToString() + "GB");
            loadMiniImages();
            calcButton.MouseEnter += (s, a) =>
            {
                try
                {
                    calcButton.Image = largeImages[calcButton.Text];
                    calcButton.ForeColor = mouseClickColor;
                    Padding p = calcButton.Margin;
                    calcButton.Region = null;
                    calcButton.Size = new Size(calcButton.Width + 30, calcButton.Height + 15);
                    calcButton.Margin = new Padding(p.Left - 18, p.Top - 8, p.Right, p.Bottom);
                    calcButton.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, calcButton.Width, calcButton.Height, 20, 20));
                    hoverPointer.Visible = true;
                    hoverPointer.Location = new Point(0, calcButton.Location.Y + ((calcButton.Size.Height - hoverPointer.Size.Height) / 2) - 3);
                    //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top - 10, label1.Margin.Right, label1.Margin.Bottom);
                }
                catch { }
            };

            calcButton.MouseLeave += (s, a) =>
            {
                try
                {
                    calcButton.Image = miniImages[calcButton.Text];
                    Padding p = calcButton.Margin;
                    calcButton.Region = null;
                    calcButton.Margin = new Padding(p.Left + 18, p.Top + 8, p.Right, p.Bottom);
                    calcButton.Size = new Size(calcButton.Width - 30, calcButton.Height - 15);
                    calcButton.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, calcButton.Width, calcButton.Height, 20, 20));
                    hoverPointer.Visible = false;
                    //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top + 10, label1.Margin.Right, label1.Margin.Bottom);
                }
                catch { }
            };
            videos.ForeColor = mouseClickColor;
            Calculator.globalTypeButton = videos;

            typeButtons.Add(videos);
            typeButtons.Add(pictures);
            typeButtons.Add(fourK);
            typeButtons.Add(affinity);
            typeButtons.Add(shortVideos);
            typeButtons.Add(gifs);
        }


        private Bitmap resizedImage(Image img, params int[] numbers)
        {
            if (img == null)
                return null;
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


        private void loadMiniImages()
        {
            foreach (DirectoryInfo directory in pardirectory)
                foreach (DirectoryInfo di in directory.GetDirectories())
                {
                    if (File.Exists(directory.FullName + "\\" + di.Name.Trim().Substring(1).Trim() + ".jpg"))
                    {
                        Image img = Image.FromFile(directory.FullName + "\\" + di.Name.Substring(1).Trim() + ".jpg");
                        miniImages.Add(di.Name.Substring(1), resizedImage(img, 0, 0, 0, 107));
                        largeImages.Add(di.Name.Substring(1), resizedImage(img, 0, 0, 0, 127));
                        img.Dispose();
                    }

                }

            if (File.Exists(pardirectory[1].FullName + "\\Stack.jpg"))
            {

                Image img = Image.FromFile(pardirectory[1].FullName + "\\Stack.jpg");
                miniImages.Add("Calc", resizedImage(img, 0, 0, 0, 126));
                largeImages.Add("Calc", resizedImage(img, 0, 0, 0, 142));
                calcButton.Image = miniImages["Calc"];

                Image img1 = resizedImage(img, 0, 0, 0, popUpY);
                calcButton.Tag = img1;
                int yLoc = calcButton.Location.Y + (calcButton.Height / 2) - (popUpY / 2);
                if (1080 - calcButton.Location.Y <= (popUpY / 2))
                {
                    yLoc = yLoc - (popUpY / 2) + 10;
                    tip = new CustomToolTip(img1.Width, popUpY, calcButton.Location.X + calcButton.Size.Width + 25, yLoc);
                }
                else if (calcButton.Location.Y - (popUpY / 2) < 0)
                {
                    tip = new CustomToolTip(img1.Width, popUpY, calcButton.Location.X + calcButton.Size.Width + 25, calcButton.Location.Y - calcButton.Margin.Top);
                }
                else
                {
                    if (img.Width > img.Height)
                        tip = new CustomToolTip(img1.Width, popUpY, calcButton.Location.X + calcButton.Size.Width + 25, yLoc);
                    else
                        tip = new CustomToolTip(300, 450, 10, yLoc);
                }

                tip.SetToolTip(calcButton, calcButton.Text);
                img.Dispose();
            }
        }

        private void setTheme()
        {
            mouseHoverColor = globColor;
            mouseClickColor = globColor;
            selectedPbColor = globColor;
            newProgressBar.ForeColor = globColor;
            String[] defColor = File.ReadAllText(pardirectory[1].FullName + "\\ThemeColor.txt").Split(',');
            if (defColor.Length == 3)
            {
                mouseHoverColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                mouseClickColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                selectedPbColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                newProgressBar.ForeColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                globColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
            }
            this.BackColor = lightBackColor;
            flowLayoutPanel1.BackColor = darkBackColor;
            flowLayoutPanel3.BackColor = lightBackColor;
            flowLayoutPanel4.BackColor = lightBackColor;
            flowLayoutPanel2.BackColor = lightBackColor;
            panel1.BackColor = lightBackColor;
            pointer.BackColor = mouseClickColor;
            hoverPointer.BackColor = mouseClickColor;
            calcButton.BackColor = lightBackColor;
            calcButton.ForeColor = Color.White;
            calcButton.FlatAppearance.MouseOverBackColor = mouseClickColor;
            searchLabel.BackColor = darkBackColor;
            searchLabel.ForeColor = Color.White;
            textBox3.BackColor = darkBackColor;
            textBox3.ForeColor = Color.White;
            button2.BackColor = darkBackColor;
            button3.BackColor = darkBackColor;
            button4.BackColor = darkBackColor;

            button1.BackColor = lightBackColor;
            button5.BackColor = lightBackColor;
            button6.BackColor = lightBackColor;

            divider.BackColor = kindaDark;
            sizeInfo.BackColor = kindaDark;

            videos.BackColor = darkBackColor;
            videos.ForeColor = Color.White;
            videos.FlatAppearance.MouseOverBackColor = kindaDark;
            videos.FlatAppearance.MouseDownBackColor = kindaDark;

            pictures.BackColor = darkBackColor;
            pictures.ForeColor = Color.White;
            pictures.FlatAppearance.MouseOverBackColor = kindaDark;
            pictures.FlatAppearance.MouseDownBackColor = kindaDark;

            fourK.BackColor = darkBackColor;
            fourK.ForeColor = Color.White;
            fourK.FlatAppearance.MouseOverBackColor = kindaDark;
            fourK.FlatAppearance.MouseDownBackColor = kindaDark;

            shortVideos.BackColor = darkBackColor;
            shortVideos.ForeColor = Color.White;
            shortVideos.FlatAppearance.MouseOverBackColor = kindaDark;
            shortVideos.FlatAppearance.MouseDownBackColor = kindaDark;

            affinity.BackColor = darkBackColor;
            affinity.ForeColor = Color.White;
            affinity.FlatAppearance.MouseOverBackColor = kindaDark;
            affinity.FlatAppearance.MouseDownBackColor = kindaDark;

            gifs.BackColor = darkBackColor;
            gifs.ForeColor = Color.White;
            gifs.FlatAppearance.MouseOverBackColor = kindaDark;
            gifs.FlatAppearance.MouseDownBackColor = kindaDark;

            newFolder.BackColor = darkBackColor;
            newFolder.ForeColor = Color.White;
            newFolder.FlatAppearance.MouseOverBackColor = kindaDark;
            newFolder.FlatAppearance.MouseDownBackColor = kindaDark;

            reset.BackColor = darkBackColor;
            reset.ForeColor = Color.White;
            reset.FlatAppearance.MouseOverBackColor = kindaDark;
            reset.FlatAppearance.MouseDownBackColor = kindaDark;

            refresh.BackColor = darkBackColor;
            refresh.ForeColor = Color.White;
            refresh.FlatAppearance.MouseOverBackColor = kindaDark;
            refresh.FlatAppearance.MouseDownBackColor = kindaDark;

            theme.BackColor = darkBackColor;
            theme.ForeColor = Color.White;
            theme.FlatAppearance.MouseOverBackColor = kindaDark;
            theme.FlatAppearance.MouseDownBackColor = kindaDark;

            /*move.BackColor = darkBackColor;
            move.ForeColor = Color.White;
            move.FlatAppearance.MouseOverBackColor = kindaDark;
            move.FlatAppearance.MouseDownBackColor = kindaDark;*/

            delete.BackColor = darkBackColor;
            delete.ForeColor = Color.White;
            delete.FlatAppearance.MouseOverBackColor = kindaDark;
            delete.FlatAppearance.MouseDownBackColor = kindaDark;

            navController.BackColor = darkBackColor;
            navController.ForeColor = Color.White;
            navController.FlatAppearance.MouseOverBackColor = kindaDark;
            navController.FlatAppearance.MouseDownBackColor = kindaDark;


            dashBoard.BackColor = darkBackColor;
            dashBoard.ForeColor = Color.White;
            dashBoard.FlatAppearance.MouseOverBackColor = kindaDark;
            dashBoard.FlatAppearance.MouseDownBackColor = kindaDark;

            stack.BackColor = darkBackColor;
            stack.ForeColor = Color.White;
            stack.FlatAppearance.MouseOverBackColor = kindaDark;
            stack.FlatAppearance.MouseDownBackColor = kindaDark;

            button3.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button3.FlatAppearance.MouseDownBackColor = mouseClickColor;

            button4.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button4.FlatAppearance.MouseDownBackColor = mouseClickColor;
            foreach (Button b in folderButt)
            {

                b.ForeColor = Color.White;
                b.BackColor = lightBackColor;
                b.FlatAppearance.MouseOverBackColor = mouseHoverColor;
                b.FlatAppearance.MouseDownBackColor = mouseClickColor;
            }

            /*foreach (Button b in typeButtons)
            {

                b.ForeColor = Color.White;
                b.BackColor = darkBackColor;
                b.FlatAppearance.MouseOverBackColor = mouseHoverColor;
                b.FlatAppearance.MouseDownBackColor = mouseClickColor;
            }*/


            if (Calculator.globalDirButton != null)
                Calculator.globalDirButton.BackColor = mouseClickColor;
            if (Calculator.globalTypeButton != null)
                Calculator.globalTypeButton.BackColor = mouseClickColor;

            if (globalLabel != null) globalLabel.ForeColor = mouseHoverColor;
            if (selectedPb != null) selectedPb.BackColor = selectedPbColor;
        }
        /*protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x2000000;
                return cp;
            }
        }*/

        static public int[] Rand_Color(double h, double s, double v)
        {
            double r = 0, g = 0, b = 0;
            int h_i = (int)(h * 6);
            double f = h * 6 - h_i;
            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);
            if (h_i == 0)
            {
                r = v;
                g = t;
                b = p;
            }
            else if (h_i == 1)
            {
                r = q;
                g = v;
                b = p;
            }
            else if (h_i == 2)
            {
                r = p;
                g = v;
                b = t;
            }
            else if (h_i == 3)
            {
                r = p;
                g = q;
                b = v;
            }
            else if (h_i == 4)
            {
                r = t;
                g = p;
                b = v;
            }
            else if (h_i == 5)
            {
                r = v;
                g = p;
                b = q;
            }
            int[] ret = new int[] { (int)(r * 256), (int)(g * 256), (int)(b * 256) };
            return ret;
        }

        public void Load_Button(String buttonName)
        {
            Boolean firstTime = false;
            Button butt2 = new Button();
            butt2.Text = buttonName;
            butt2.Font = new Font("Consolas", 9, FontStyle.Bold);
            butt2.Cursor = System.Windows.Forms.Cursors.Hand;
            butt2.ForeColor = Color.White;
            butt2.BackColor = lightBackColor;
            butt2.Size = new Size(221, 105);
            butt2.FlatStyle = FlatStyle.Flat;
            butt2.TextAlign = ContentAlignment.BottomRight;
            butt2.Region = null;
            butt2.Margin = new Padding(28, 4, 0, 0);
            butt2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, butt2.Width, butt2.Height, 15, 15));
            butt2.Image = ((System.Drawing.Image)(resources.GetObject("fourKBtn.Image")));
            butt2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;

            butt2.FlatAppearance.MouseOverBackColor = kindaDark;
            butt2.FlatAppearance.MouseDownBackColor = kindaDark;
            try
            {
                isEnlarged.Add(butt2.Text, false);
            }
            catch { }
            if (butt2.Text.Contains(Calculator.globalFilt))
            {
                butt2.BackColor = mouseClickColor;
                Calculator.globalDirButton = butt2;
                firstTime = true;

            }
            else
            {
                butt2.BackColor = lightBackColor;
            }

            butt2.FlatAppearance.BorderSize = 0;
            butt2.MouseClick += (s, a) =>
            {
                inDb = false;
                if (Calculator.globalDirButton != null && isEnlarged[Calculator.globalDirButton.Text] && Calculator.globalDirButton.Text != butt2.Text)
                {
                    Calculator.globalDirButton.ForeColor = Color.White;
                    isEnlarged[Calculator.globalDirButton.Text] = false;
                    enlargeLeave(Calculator.globalDirButton, Calculator.globalDirButton.Text);
                }
                Calculator.globalDirButton = butt2;
                Calculator.globalFilt = butt2.Text;
                isEnlarged[butt2.Text] = true;
                butt2.ForeColor = mouseClickColor;
                disposeAndLoad();
                hoverPointer.Visible = false;
                pointer.Location = new Point(0, butt2.Location.Y + ((butt2.Size.Height - pointer.Size.Height) / 2) - 43);

                if (axWindowsMediaPlayer1.URL.Length != 0 && !flowLayoutPanel3.Controls.Contains(axWindowsMediaPlayer1))
                {
                    calcButton.Hide();
                    axWindowsMediaPlayer1.Size = new Size(calcButton.Width + 24, calcButton.Height + 35);
                    axWindowsMediaPlayer1.Margin = new Padding(0, 8, 0, 0);
                    flowLayoutPanel3.Controls.Add(axWindowsMediaPlayer1);
                    flowLayoutPanel3.Controls.SetChildIndex(axWindowsMediaPlayer1, flowLayoutPanel3.Controls.IndexOf(calcButton));
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    axWindowsMediaPlayer1.settings.rate = 1.40;
                }
            };
            butt2.MouseEnter += (s, a) =>
            {
                enlargeEnter(butt2);
                butt2.ForeColor = mouseClickColor;
                hoverPointer.Visible = true;
                hoverPointer.Location = new Point(0, butt2.Location.Y + ((butt2.Size.Height - hoverPointer.Size.Height) / 2) - 43);
            };

            butt2.MouseLeave += (s, a) =>
            {
                enlargeLeave(butt2, butt2.Text);
                if (Calculator.globalDirButton.Text != butt2.Text)
                    butt2.ForeColor = Color.White;
                hoverPointer.Visible = false;
            };

            try
            {
                butt2.Image = miniImages[butt2.Text];
            }
            catch { }


            folderButt.Add(butt2);
            flowLayoutPanel3.Controls.Add(butt2);

            if (firstTime)
                pointer.Location = new Point(0, butt2.Location.Y + ((butt2.Size.Height - pointer.Size.Height) / 2) + 7);

            if (File.Exists(pardirectory[1].FullName + "\\" + butt2.Text.Trim() + ".jpg") || File.Exists(pardirectory[0].FullName + "\\" + butt2.Text.Trim() + ".jpg"))
            {
                Image img = Image.FromFile(File.Exists(pardirectory[0].FullName + "\\" + butt2.Text.Trim() + ".jpg") ? pardirectory[0].FullName + "\\" + butt2.Text.Trim() + ".jpg" : pardirectory[1].FullName + "\\" + butt2.Text.Trim() + ".jpg");
                Image img1 = resizedImage(img, 0, 0, 0, popUpY);
                butt2.Tag = img1;
                int yLoc = butt2.Location.Y + (butt2.Height / 2) - (popUpY / 2);
                if (1080 - butt2.Location.Y <= popUpY / 2)
                {
                    yLoc = yLoc - (popUpY / 2) + 10;
                }
                else if (butt2.Location.Y - (popUpY / 2) < 0)
                {
                    tip = new CustomToolTip(img1.Width, popUpY, butt2.Location.X + butt2.Size.Width + 25, butt2.Location.Y + (butt2.Height / 2));
                }
                else
                {
                    if (img.Width > img.Height)
                        tip = new CustomToolTip(img1.Width, popUpY, butt2.Location.X + butt2.Size.Width + 30, yLoc + 17);
                    else
                        tip = new CustomToolTip(300, 450, 10, yLoc);
                }

                tip.SetToolTip(butt2, butt2.Text);
                img.Dispose();
                tip.InitialDelay = 280;
                tip.AutoPopDelay = 6000;
            }

        }

        public long[] noOfFiles(DirectoryInfo videoDi)
        {
            long[] noOfVid = { 0, 0 };
            foreach (FileInfo fi in videoDi.GetFiles())
            {
                if (!fi.Name.ToLower().EndsWith(".txt"))
                {
                    noOfVid[0]++;
                    noOfVid[1] = noOfVid[1] + fi.Length;
                }
            }
            return noOfVid;
        }

        public String sizeStr(long size)
        {
            String siz = "";
            double gbd = (double)(size) / (1000.0 * 1000.0 * 1000.0);
            gbd = Math.Round(gbd, 2);
            double mbd = (double)(size) / (1000.0 * 1000.0);
            mbd = Math.Round(mbd, 2);
            long kbd = size / (1000);

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

            return siz;
        }

        public void pbClick(String Key, DirectoryInfo subDi, String prevStr, String nextStr)
        {
            GC.Collect();
            DirectoryInfo prevFi = new DirectoryInfo(prevStr);
            DirectoryInfo nextFi = new DirectoryInfo(nextStr);
            String priorStr = "", line;
            try
            {
                String fileName = subDi.FullName.Equals(directory3.FullName) ? pardirectory[0].GetDirectories().ElementAt(0).FullName : subDi.FullName;
                StreamReader sr = new StreamReader(fileName + "\\priority.txt");

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(Key))
                    {
                        int priority = int.Parse(line.Substring(0, line.IndexOf("-")));
                        line = (priority + 1) + "-" + Key;
                    }
                    priorStr = priorStr + line + "\n";
                }
                sr.Close();
                File.WriteAllText(fileName + "\\priority.txt", priorStr);
            }
            catch { }

            this.Hide();
            VideoPlayer v = new VideoPlayer(Key, this, Calculator.globalType, prevFi, nextFi, folders);
            Explorer.staticExp = null;
            v.Show();
        }

        private void createSmallResFiles(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.ToLower().EndsWith(".txt")) continue;
                if (!File.Exists(di.FullName + "\\ImgPB\\" + fi.Name))
                    ResizeImage(fi.FullName, di.FullName + "\\ImgPB\\" + fi.Name, 195, 0, 262, 0);
            }
        }

        private void ResizeImage(string SoucePath, string DestPath, params int[] numbers)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(SoucePath);

            int width = img.Width, destWidth;
            int height = img.Height, destHeight;
            if (width > height)
            {
                Double ratio = (Double)width / (Double)height;
                destHeight = (int)(numbers.Length == 4 ? (int)((Double)numbers[2] / ratio) : (int)(390 / ratio));
                destWidth = (int)(numbers.Length == 4 ? numbers[2] : 390);
            }
            else
            {
                Double ratio = (Double)width / (Double)height;
                destWidth = (int)(numbers.Length == 4 ? (int)((double)numbers[0]) : 257);
                destHeight = numbers.Length == 4 ? (int)((double)numbers[0] / ratio) : (int)(257 / ratio);
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

            bmp.Save(DestPath, myImageCodecInfo, myEncoderParameters);
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

        private Image setDefaultPic(FileInfo fi, params PictureBox[] picBox)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(fi.DirectoryName + "\\imgPB");

            String fileName = fi.Name;
            if (!File.Exists(fi.FullName))
                return null;

            if (File.Exists(di.FullName + "\\resized_" + fi.Name + ".jpg"))
            {
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
                    if (picBox.Length > 0)
                        picBox[0].ImageLocation = di.FullName + "\\resized_" + fi.Name + ".jpg";
                    return Image.FromFile(di.FullName + "\\resized_" + fi.Name + ".jpg");
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
                        ResizeImage(file.FullName, di.FullName + "\\resized_" + fileName + ".jpg", 0, 292, 515, 0);
                        try
                        {
                            File.Delete(file.FullName);
                        }
                        catch { }
                        if (picBox.Length > 0)
                            picBox[0].ImageLocation = di.FullName + "\\resized_" + fileName + ".jpg";
                        if (File.Exists(di.FullName + "\\resized_" + fileName + ".jpg")) return Image.FromFile(di.FullName + "\\resized_" + fileName + ".jpg");
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
        /*
        public void pbClick(PictureBox pb, Boolean isShort, String file)
        {
            String fileName = pb.Name;
            IWMPMedia mediainfo = wmpDura.newMedia(fileName);
            Double duration = mediainfo.duration;
            Point controlLoc = this.PointToScreen(pb.Location);
            VideoPlayer.popUpVideoWidth = stackedDb ? 436.0 : (unStackedSizePbDb.Width + 60.0);
            VideoPlayer.popUpVideoHeight = stackedDb ? 247.0 : (unStackedSizePbDb.Height + 33.0);
            int x = flowLayoutPanel1.Location.X + pb.Location.X - (((int)VideoPlayer.popUpVideoWidth - pb.Width) / 2);
            int y = flowLayoutPanel1.Location.Y + pb.Location.Y - (((int)VideoPlayer.popUpVideoHeight - pb.Height) / 2);
            if (x + (stackedDb ? 436.0 : (unStackedSizePbDb.Width + 60.0)) > 1920)
            {
                x = x - ((x + (int)(stackedDb ? 436.0 : (unStackedSizePbDb.Width + 60.0))) - 1920) - 2;
            }
            if (x < 0) x = 2;

            if (y < 0)
            {
                y = 1;
            }
            else
            if (y + (stackedDb ? 247.0 : (unStackedSizePbDb.Height + 33.0)) > 1080)
            {
                y = y - ((y + (int)(stackedDb ? 247.0 : (unStackedSizePbDb.Height + 33.0))) - 1080) - 2;
            }

            VideoPlayer.relativeLoc = new Point(x, y);
            VideoPlayer.stepWise = 8;
            if (VideoPlayer.miniVideoPlayer == null)
            {
                VideoPlayer.miniVideoPlayer = new miniVideoPlayer(videosPb, this);
            }
            else VideoPlayer.miniVideoPlayer.setVideosPb(videosPb, this);
            VideoPlayer.miniVideoPlayer.setData(pb, new FileInfo(fileName), null);
            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.enableContextMenu = false;

            //VideoPlayer.miniVideoPlayer.Region = null;
            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Region = null;
            VideoPlayer.miniVideoPlayer.Location = new Point(pb.Location.X + flowLayoutPanel1.Location.X, pb.Location.Y + flowLayoutPanel1.Location.Y);
            //VideoPlayer.miniVideoPlayer.Location = relativeLoc;
            VideoPlayer.miniVideoPlayer.pastPos = 0;
            //VideoPlayer.miniVideoPlayer.Size = new Size(582, 330);
            VideoPlayer.miniVideoPlayer.Size = pb.Size;
            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Size = new Size(pb.Size.Width, pb.Size.Height - 3);
            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Location = new Point(0, 4);
            VideoPlayer.miniVideoPlayer.newProgressBar.Location = new Point(0, -3);
            VideoPlayer.miniVideoPlayer.newProgressBar.Size = new Size(pb.Size.Width, 10);
            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.URL = fileName;
            //VideoPlayer.miniVideoPlayer.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, VideoPlayer.miniVideoPlayer.Width, VideoPlayer.miniVideoPlayer.Height, 20, 20));
            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Width, VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Height, 20, 20));
            //VideoPlayer.miniVideoPlayer.duration = VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.currentMedia.duration;
            //VideoPlayer.miniVideoPlayer.newProgressBar.Maximum = (int)VideoPlayer.miniVideoPlayer.duration;
            VideoPlayer.isShort = isShort;
            VideoPlayer.miniVideoPlayer.Show();
            if ((file.Substring(file.IndexOf("@@!") + 3)) != "0" && isShort)
            {
                if (File.Exists(Explorer.directory3.FullName + "\\resumeDb.txt"))
                {
                    List<String> temp = File.ReadAllLines(Explorer.directory3.FullName + "\\resumeDb.txt").ToList();

                    foreach(String str in temp)
                    {
                        if(str.Contains(file.Substring(0,file.IndexOf("@@!") + 3)))
                            VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = double.Parse(str.Substring(file.LastIndexOf("@@!") + 3));
                    }
                }
            }
            VideoPlayer.miniVideoPlayer.newProgressBar.Maximum = (int)duration;
            if((int)VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Ctlcontrols.currentPosition < (int)duration)
            VideoPlayer.miniVideoPlayer.newProgressBar.Value = (int)VideoPlayer.miniVideoPlayer.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
        }*/

        public List<String> FilesDashboard(Boolean allFiles, Boolean isShort, Boolean isAff)
        {
            List<String> priorList = new List<string>();
            DirectoryInfo parDir = new DirectoryInfo("F:\\Calculator");
            DirectoryInfo directory2 = new DirectoryInfo("H:\\vivado\\rand_name\\rand_name.ir");
            DirectoryInfo directory3 = new DirectoryInfo("G:\\projects");
            List<DirectoryInfo> supPar = new List<DirectoryInfo>();

            DirectoryInfo directory22 = new DirectoryInfo("C:\\Users\\Harsha Vardhan\\Downloads\\Video");
            DirectoryInfo directory11 = new DirectoryInfo("E:\\VS Code\\CSS");

            if (Directory.Exists("I:\\ubuntu\\home\\xdm\\bin")) 
                supPar.Add(new DirectoryInfo("I:\\ubuntu\\home\\xdm\\bin"));
            else 
                supPar.Add(directory22);

            if (Directory.Exists(parDir.FullName)) 
                supPar.Add(directory2);
            else 
                supPar.Add(directory11);

            if (Directory.Exists(directory2.FullName)) 
                supPar.Add(parDir);

            if (Directory.Exists(directory3.FullName))
                supPar.Add(directory3);

            if (Directory.Exists(directory3.FullName))
                supPar.Add(directory3);

            if (Directory.Exists("G:\\softwares\\proteous\\DEMO to PRO\\BIN\\ETC"))
                supPar.Add(new DirectoryInfo("G:\\softwares\\proteous\\DEMO to PRO\\BIN\\ETC"));

            if (supPar.Count == 0)
                return null;
            List<String> files = new List<string>();

            foreach (DirectoryInfo di0 in supPar)
                foreach (DirectoryInfo di in di0.GetDirectories())
                {
                    foreach (DirectoryInfo di2 in di.GetDirectories())
                    {
                        DirectoryInfo di3 = di2;
                        if (isShort)
                        {
                            if (!Directory.Exists(di2.FullName + "\\Pics\\GifVideos"))
                                continue;
                            di3 = new DirectoryInfo(di2.FullName + "\\Pics\\GifVideos");
                        }
                        if (isAff)
                        {
                            if (!Directory.Exists(di2.FullName + "\\Pics\\Affinity"))
                                continue;
                            di3 = new DirectoryInfo(di2.FullName + "\\Pics\\Affinity");
                        }
                        Random r = new Random();
                        List<FileInfo> tempFileList = di3.GetFiles().ToList();
                        foreach (FileInfo fiii in di3.GetFiles())
                        {
                            if (!fiii.FullName.EndsWith(".txt"))
                            {
                                String temp = tempFileList.ElementAt(r.Next(tempFileList.Count)).FullName;
                                while (temp.EndsWith(".txt"))
                                    temp = tempFileList.ElementAt(r.Next(tempFileList.Count)).FullName;
                                files.Add(temp + "@@!0");
                                break;
                            }
                            else
                                tempFileList.Remove(fiii);
                        }

                    }
                }

            if (allFiles)
                return files;

            List<String> tempFiles = new List<string>();
            int threhosld = 0;
            Random random1 = new Random();
            int rand = random1.Next(files.Count);
            int thresholdOthers = 19, thresholdBest = 9, threshold = 6, thresholdBestofBest = 10;

            while (thresholdOthers > 0 || thresholdBest > 0 || threshold > 0 || thresholdBestofBest > 0)
            {
                threhosld = 3;
                while (threhosld > 0 && thresholdBestofBest > 0)
                {
                    while (tempFiles.Contains(files.ElementAt(rand)) || rand > 19)
                    {
                        rand = random1.Next(files.Count);
                    }
                    tempFiles.Add(files.ElementAt(rand));
                    threhosld--;
                    thresholdBestofBest--;
                }

                threhosld = 3;
                while (threhosld > 0 && thresholdBest > 0)
                {
                    while (tempFiles.Contains(files.ElementAt(rand)) || rand < 20 || rand > 40)
                    {
                        rand = random1.Next(files.Count);
                    }
                    tempFiles.Add(files.ElementAt(rand));
                    threhosld--;
                    thresholdBest--;
                }

                threhosld = 3;
                while (threhosld > 0 && thresholdOthers > 0)
                {
                    while (tempFiles.Contains(files.ElementAt(rand)) || rand <= 53)
                    {
                        rand = random1.Next(files.Count);
                    }
                    tempFiles.Add(files.ElementAt(rand));
                    threhosld--;
                    thresholdOthers--;
                }

                threhosld = 3;
                while (threhosld > 0 && threshold > 0)
                {
                    while (tempFiles.Contains(files.ElementAt(rand)) || rand <= 40 || rand > 53)
                    {
                        rand = random1.Next(files.Count);
                    }
                    tempFiles.Add(files.ElementAt(rand));
                    threhosld--;
                    threshold--;
                }

            }
            return tempFiles;
        }
        public void Explorer_Load(object sender, EventArgs e)
        {
            GC.Collect();

            namePriorityPairs.Clear();

            if (!isGames)
            {
                if (useAndThrow)
                {
                    foreach (DirectoryInfo directory in pardirectory)
                        foreach (DirectoryInfo di in directory.GetDirectories().OrderBy(f => f.Name).ToList())
                        {
                            Load_Button(di.Name.Substring(1));
                            if (!File.Exists(di.FullName + "\\priority.txt"))
                            {
                                FileStream fi = File.Create(di.FullName + "\\priority.txt");
                                fi.Close();
                            }
                            if (Calculator.globalFilt.Length == 0)
                            {
                                //Calculator.globalFilt = di.Name.Substring(1);
                            }
                            List<String> list = new List<String>();
                            foreach (String str in File.ReadAllLines(di.FullName + "\\priority.txt").ToList())
                                list.Add(str.Substring(str.IndexOf("-") + 1));

                            allFoldersDict.Add(di.Name, list);
                        }

                    /*countFiles = new Label();
                    countFiles.Text = "No of folders: ";
                    countFiles.Font = new Font("Consolas", 11, FontStyle.Bold);
                    countFiles.BackColor = darkBackColor;
                    countFiles.Size = new Size(250, 20);
                    countFiles.ForeColor = Color.White;
                    countFiles.TextAlign = ContentAlignment.MiddleCenter;
                    countFiles.Margin = new Padding(10, 8, 0, 0);*/
                    //flowLayoutPanel3.Controls.AddcountFiles);
                }

                countFiles.Text = "No of folders: 0";


                foreach (DirectoryInfo directory in pardirectory)
                {
                    foreach (DirectoryInfo subDi in directory.GetDirectories().OrderBy(f => f.Name).ToList())
                    {
                        if (subDi.Name.Substring(1).Equals(Calculator.globalFilt))
                        {
                            countFiles.Text = "No of folders: " + subDi.GetDirectories().Length;
                            String writePriorStr = "";
                            StreamReader sr1 = new StreamReader(subDi.FullName + "\\priority.txt");

                            foreach (DirectoryInfo di in subDi.GetDirectories().OrderByDescending(f => f.GetFiles().Sum(k => k.Length)).ToList())
                            {
                                if (!File.Exists(di.FullName + "\\disPic.txt"))
                                {
                                    FileStream fs = File.Create(di.FullName + "\\disPic.txt");
                                    fs.Close();
                                }

                                if (!File.Exists(di.FullName + "\\disGifPic.txt"))
                                {
                                    FileStream fs = File.Create(di.FullName + "\\disGifPic.txt");
                                    fs.Close();
                                }

                                if (!File.Exists(di.FullName + "\\links.txt"))
                                {
                                    FileStream fi = File.Create(di.FullName + "\\links.txt");
                                    fi.Close();
                                }

                                int priority = 0;
                                String singLine = "";
                                while ((singLine = sr1.ReadLine()) != null)
                                {
                                    if (singLine.Contains(di.FullName))
                                    {
                                        priority = int.Parse(singLine.Replace("-" + di.FullName, ""));
                                        break;
                                    }
                                }
                                writePriorStr = writePriorStr + priority + "-" + di.FullName + "\n";
                                namePriorityPairs.Add(di.FullName, priority);
                            }
                            if (subDi.Name.Contains("Best of the Best") && Directory.Exists(directory3.FullName))
                            {
                                foreach (DirectoryInfo di in directory3.GetDirectories().OrderByDescending(f => f.GetFiles().Sum(k => k.Length)).ToList())
                                {
                                    if (!File.Exists(di.FullName + "\\disPic.txt"))
                                    {
                                        FileStream fs = File.Create(di.FullName + "\\disPic.txt");
                                        fs.Close();
                                    }
                                    if (!File.Exists(di.FullName + "\\disGifPic.txt"))
                                    {
                                        FileStream fs = File.Create(di.FullName + "\\disGifPic.txt");
                                        fs.Close();
                                    }

                                    if (!File.Exists(di.FullName + "\\links.txt"))
                                    {
                                        FileStream fi = File.Create(di.FullName + "\\links.txt");
                                        fi.Close();
                                    }

                                    int priority = 0;
                                    String singLine = "";
                                    while ((singLine = sr1.ReadLine()) != null)
                                    {
                                        if (singLine.Contains(di.FullName))
                                        {
                                            priority = int.Parse(singLine.Replace("-" + di.FullName, ""));
                                            break;
                                        }
                                    }
                                    writePriorStr = writePriorStr + priority + "-" + di.FullName + "\n";
                                    namePriorityPairs.Add(di.FullName, priority);
                                }
                            }
                            else if (subDi.Name.Contains("Channels") && Directory.Exists(directory4.FullName))
                            {
                                foreach (DirectoryInfo di in directory4.GetDirectories().OrderByDescending(f => f.GetFiles().Sum(k => k.Length)).ToList())
                                {
                                    if (!File.Exists(di.FullName + "\\disPic.txt"))
                                    {
                                        FileStream fs = File.Create(di.FullName + "\\disPic.txt");
                                        fs.Close();
                                    }
                                    if (!File.Exists(di.FullName + "\\disGifPic.txt"))
                                    {
                                        FileStream fs = File.Create(di.FullName + "\\disGifPic.txt");
                                        fs.Close();
                                    }

                                    if (!File.Exists(di.FullName + "\\links.txt"))
                                    {
                                        FileStream fi = File.Create(di.FullName + "\\links.txt");
                                        fi.Close();
                                    }

                                    int priority = 0;
                                    String singLine = "";
                                    while ((singLine = sr1.ReadLine()) != null)
                                    {
                                        if (singLine.Contains(di.FullName))
                                        {
                                            priority = int.Parse(singLine.Replace("-" + di.FullName, ""));
                                            break;
                                        }
                                    }
                                    writePriorStr = writePriorStr + priority + "-" + di.FullName + "\n";
                                    namePriorityPairs.Add(di.FullName, priority);
                                }
                            }
                            sr1.Close();

                            try { File.WriteAllText(subDi.FullName + "\\priority.txt", writePriorStr); } catch { }
                            namePriorityPairs = namePriorityPairs.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                            Label dirName = new Label();
                            dirName.Text = Calculator.globalFilt;
                            dirName.Font = new Font("Arial", 35, FontStyle.Bold);
                            dirName.BackColor = lightBackColor;
                            dirName.Size = new Size(1300, 80);
                            dirName.ForeColor = Color.White;
                            dirName.TextAlign = ContentAlignment.MiddleLeft;
                            //flowLayoutPanel1.Controls.Add(dirName);

                            Label spaceBar = new Label();
                            spaceBar.BackColor = darkBackColor;
                            spaceBar.Size = new Size(1500, 20);
                            flowLayoutPanel1.Controls.Add(spaceBar);

                            typeName.Text = Calculator.globalType;
                            typeName.Font = new Font("Arial", 35, FontStyle.Bold);
                            typeName.BackColor = lightBackColor;
                            typeName.ForeColor = Color.White;
                            typeName.Size = new Size(275, 80);
                            typeName.FlatStyle = FlatStyle.Flat;
                            typeName.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, typeName.Width, typeName.Height, 20, 20));
                            typeName.FlatAppearance.BorderSize = 0;
                            //flowLayoutPanel1.Controls.Add(typeName);

                            for (int i=0;i<namePriorityPairs.Count; i++)
                            {
                                int max = 4;
                                FlowLayoutPanel indFlowLayoutPanel = new FlowLayoutPanel();
                                indFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                                indFlowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                                indFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);

                                if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                                {
                                    if (compact)
                                    {
                                        indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 22, 357 + 18);
                                        max = 4;
                                    }
                                    else
                                    {
                                        indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 22, 450 + 18);
                                        max = 3;
                                    }
                                }
                                else
                                {
                                    if (compact)
                                    {
                                        indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 22, 246 + 30);
                                        max = 3;
                                    }
                                    else
                                    {
                                        indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width - 22, 327 + 30);
                                        max = 2;
                                    }
                                }
                                for (int k = 0; k < max; k++)
                                {
                                    if (i + k >= namePriorityPairs.Count)
                                        break;
                                    KeyValuePair<String, int> keyValuePair = namePriorityPairs.ElementAt(i + k);
                                    if (searchText.Length > 0)
                                        if (!keyValuePair.Key.Replace(subDi.FullName, "").ToLower().Contains(searchText.ToLower()))
                                        {
                                            max = max + 1;
                                            continue;
                                        }

                                    folders.Add(keyValuePair.Key);
                                    String vidDetText = "\n" + keyValuePair.Key.Substring(keyValuePair.Key.LastIndexOf("\\") + 1) + "\n";
                                    DirectoryInfo videoDi = new DirectoryInfo(keyValuePair.Key);
                                    long[] number = noOfFiles(videoDi);
                                    vidDetText = vidDetText + "\n" + "No of Videos - " + number[0];
                                    String size = sizeStr(number[1]);
                                    vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                                    if (!Directory.Exists(keyValuePair.Key + "\\Pics"))
                                        Directory.CreateDirectory(keyValuePair.Key + "\\Pics");
                                    videoDi = new DirectoryInfo(keyValuePair.Key + "\\Pics");
                                    number = noOfFiles(videoDi);
                                    vidDetText = vidDetText + "\n" + "No of Pictures - " + number[0];
                                    size = sizeStr(number[1]);
                                    vidDetText = vidDetText + "\n" + "Size - " + size + "\n";

                                    if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3") || !compact)
                                    {
                                        if (!Directory.Exists(keyValuePair.Key + "\\Pics\\kkkk"))
                                            Directory.CreateDirectory(keyValuePair.Key + "\\Pics\\kkkk");
                                        videoDi = new DirectoryInfo(keyValuePair.Key + "\\Pics\\kkkk");
                                        number = noOfFiles(videoDi);
                                        vidDetText = vidDetText + "\n" + "No of 4K Pics - " + number[0];
                                        size = sizeStr(number[1]);
                                        vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                                        if (!Directory.Exists(keyValuePair.Key + "\\Pics\\GifVideos"))
                                            Directory.CreateDirectory(keyValuePair.Key + "\\Pics\\GifVideos");
                                        videoDi = new DirectoryInfo(keyValuePair.Key + "\\Pics\\GifVideos");
                                        number = noOfFiles(videoDi);
                                        vidDetText = vidDetText + "\n" + "No of Gif Videos - " + number[0];
                                        size = sizeStr(number[1]);
                                        vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                                        if (!Directory.Exists(keyValuePair.Key + "\\Pics\\Gifs"))
                                            Directory.CreateDirectory(keyValuePair.Key + "\\Pics\\Gifs");
                                        videoDi = new DirectoryInfo(keyValuePair.Key + "\\Pics\\Gifs");
                                        number = noOfFiles(videoDi);
                                        vidDetText = vidDetText + "\n" + "No of Gifs - " + number[0];
                                        size = sizeStr(number[1]);
                                        vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                                    }
                                    vidDetText = vidDetText + "\n" + "Priority - " + keyValuePair.Value;

                                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                                    flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

                                    PictureBox dirPb = new PictureBox();
                                    dirPb.Margin = new Padding(0, 8, 0, 0);
                                    dirPb.Name = keyValuePair.Key;

                                    /*DirectoryInfo picsDir = new DirectoryInfo(keyValuePair.Key + "\\Pics\\imgPB");
                                    try
                                    {
                                        foreach (FileInfo fi in picsDir.GetFiles())
                                        {
                                            fi.Delete();
                                        }
                                    }
                                    catch { }*/

                                    /* DirectoryInfo picsDir = new DirectoryInfo(keyValuePair.Key + "\\Pics");
                                     createSmallResFiles(picsDir);
                                     picsDir = new DirectoryInfo(keyValuePair.Key + "\\Pics\\kkkk");
                                     createSmallResFiles(picsDir);*/

                                    /*DirectoryInfo picsDir = new DirectoryInfo(keyValuePair.Key + "\\Pics\\Gifs\\imgPB");
                                    foreach (FileInfo fi in picsDir.GetFiles())
                                    {
                                        Image image = Image.FromFile(fi.FullName);
                                        Bitmap bmp = new Bitmap(image);
                                        image.Dispose();
                                        ImageCodecInfo myImageCodecInfo;
                                        System.Drawing.Imaging.Encoder myEncoder;
                                        EncoderParameter myEncoderParameter;
                                        EncoderParameters myEncoderParameters;
                                        myImageCodecInfo = GetEncoderInfo("image/jpeg");
                                        myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                        myEncoderParameters = new EncoderParameters(1);
                                        myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                                        myEncoderParameters.Param[0] = myEncoderParameter;
                                        File.Delete(fi.FullName);
                                        bmp.Save(fi.FullName, myImageCodecInfo, myEncoderParameters);
                                        bmp.Dispose();
                                    }*/

                                    Label vidDetails = new Label();
                                    dirPb.BackColor = lightBackColor;
                                    vidDetails.BackColor = lightBackColor;
                                    flowLayoutPanel.BackColor = lightBackColor;
                                    if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                                    {
                                        if (compact)
                                        {
                                            dirPb.Size = new Size(240, 357);
                                            vidDetails.Size = new Size(135, 337);
                                            dirPb.Margin = new Padding(0, 0, 0, 0);
                                            vidDetails.Padding = new Padding(5, 0, 0, 0);
                                            vidDetails.Margin = new Padding(0, 11, 0, 0);
                                            vidDetails.Font = new Font("Consolas", 7, FontStyle.Regular);

                                            flowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
                                            flowLayoutPanel.Margin = new Padding(32, 0, 0, 0);
                                            flowLayoutPanel.Size = new Size(dirPb.Width + vidDetails.Width, dirPb.Height);
                                        }
                                        else
                                        {
                                            dirPb.Size = new Size(300, 450);
                                            dirPb.Margin = new Padding(0, 0, 0, 0);
                                            vidDetails.Size = new Size(175, 405);
                                            vidDetails.Padding = new Padding(7, 0, 0, 0);
                                            vidDetails.Margin = new Padding(0, 20, 0, 0);
                                            vidDetails.Font = new Font("Consolas", 8, FontStyle.Regular);

                                            flowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
                                            flowLayoutPanel.Margin = new Padding(65, 0, 0, 0);
                                            flowLayoutPanel.Size = new Size(dirPb.Width + vidDetails.Width, dirPb.Height);
                                        }
                                    }
                                    else
                                    {
                                        dirPb.BackColor = lightBackColor;
                                        vidDetails.BackColor = darkBackColor;
                                        flowLayoutPanel.BackColor = darkBackColor;
                                        if (compact)
                                        {
                                            dirPb.Size = new Size(380, 246);
                                            dirPb.Margin = new Padding(0, 0, 0, 9);
                                            vidDetails.Size = new Size(140, 223);
                                            vidDetails.Padding = new Padding(0, 0, 0, 0);
                                            vidDetails.Margin = new Padding(1, 13, 0, 0);
                                            vidDetails.Font = new Font("Consolas", 7, FontStyle.Regular);

                                            flowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
                                            flowLayoutPanel.Margin = new Padding(18, 0, 0, 0);
                                            flowLayoutPanel.Size = new Size(dirPb.Width + vidDetails.Width + 1, dirPb.Height + 32);
                                        }
                                        else
                                        {
                                            dirPb.Size = new Size(505, 327);
                                            dirPb.Margin = new Padding(0, 0, 0, 8);
                                            vidDetails.Size = new Size(180, 310);
                                            vidDetails.Padding = new Padding(8, 0, 0, 0);
                                            vidDetails.Margin = new Padding(2, 10, 0, 0);
                                            vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);

                                            flowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
                                            flowLayoutPanel.Margin = new Padding(90, 0, 0, 0);
                                            flowLayoutPanel.Size = new Size(dirPb.Width + vidDetails.Width + 2, dirPb.Height + 65);
                                        }
                                    }

                                    dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                                    dirPb.Cursor = Cursors.Arrow;
                                    dirPb.ContextMenuStrip = contextMenuStrip1;
                                    dirPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dirPb.Width, dirPb.Height, 30, 30));

                                    String img = File.ReadAllText(keyValuePair.Key + "\\disPic.txt").Trim();
                                    if (img != "")
                                    {
                                        img = img.Substring(img.IndexOf("Pics"));
                                        img = keyValuePair.Key + "\\" + img;
                                    }

                                    File.WriteAllText(keyValuePair.Key + "\\disPic.txt", img);

                                    if (File.Exists(img))
                                    {
                                        Image image = Image.FromFile(img);

                                        //Bitmap bmp = new Bitmap(image);
                                        /*image.Dispose();

                                        ImageCodecInfo myImageCodecInfo;
                                        System.Drawing.Imaging.Encoder myEncoder;
                                        EncoderParameter myEncoderParameter;
                                        EncoderParameters myEncoderParameters;
                                        myImageCodecInfo = GetEncoderInfo("image/jpeg");
                                        myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                        myEncoderParameters = new EncoderParameters(1);
                                        myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                                        myEncoderParameters.Param[0] = myEncoderParameter;
                                        File.Delete(img);
                                        bmp.Save(img, myImageCodecInfo, myEncoderParameters);*/

                                        dirPb.Image = image;

                                        if (!Directory.Exists(keyValuePair.Key + "\\imgPB)"))
                                        {
                                            Directory.CreateDirectory(keyValuePair.Key + "\\imgPB");
                                        }
                                        if (File.Exists(keyValuePair.Key + "\\imgPB\\smallRes.png"))
                                        {
                                            File.Delete(keyValuePair.Key + "\\imgPB\\smallRes.png");
                                        }
                                    }
                                    try
                                    {
                                        flowLayoutPanel.Controls.Add(dirPb);
                                        //flowLayoutPanel1.Controls.Add(dirPb); 
                                    }
                                    catch { }

                                    vidDetails.Text = vidDetText;
                                    vidDetails.ForeColor = Color.White;
                                    vidDetails.TextAlign = ContentAlignment.TopLeft;
                                    //vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 5, 5));

                                    //flowLayoutPanel1.Controls.Add(vidDetails);
                                    flowLayoutPanel.Controls.Add(vidDetails);
                                    flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 30, 30));
                                    indFlowLayoutPanel.Controls.Add(flowLayoutPanel);
                                    grpLabels.Add(vidDetails);

                                    disposableBoxes.Add(dirPb);

                                    dirPb.MouseClick += (s, args) =>
                                    {
                                        if (selectedPb == null)
                                        {
                                            globalLabel = vidDetails;
                                            globalLabel.ForeColor = selectedPbColor;
                                            selectedPb = dirPb;
                                            selectedPb.BackColor = selectedPbColor;
                                            Font myfont1 = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                                            globalLabel.Font = myfont1;
                                            return;
                                        }
                                        if (globalLabel == null)
                                        {
                                            globalLabel = vidDetails;
                                            globalLabel.ForeColor = selectedPbColor;

                                            Font myfont1 = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                                            globalLabel.Font = myfont1;
                                        }

                                        selectedPb.BackColor = lightBackColor;
                                        Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;

                                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                                        globalLabel.Font = myfont;
                                        globalLabel.ForeColor = Color.White;
                                        selectedPb = dirPb;
                                        selectedPb.BackColor = selectedPbColor;
                                        globalLabel = vidDetails;

                                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                                        globalLabel.Font = myfont;
                                        globalLabel.ForeColor = selectedPbColor;

                                    };
                                    Image imgg = null;
                                    int queue = 0;

                                    Random r = new Random();
                                    String[] arrGif = File.ReadAllLines(keyValuePair.Key + "\\disGifPic.txt");
                                    String gif = "";

                                    dirPb.MouseEnter += (s, args) =>
                                    {
                                        //timer1.Interval = 10;
                                        //timer1.Start();
                                        //queue = 1;
                                        //floatingGif.Size = new Size((int)(dirPb.Size.Height * 1.7777), dirPb.Size.Height);
                                        //floatingGif.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, floatingGif.Width, floatingGif.Height, 12, 12));
                                        //floatingGif.Location = new Point(dirPb.Location.X + dirPb.Size.Width, dirPb.Location.Y);
                                        //floatingGif.Show();
                                        
                                        globalSize = dirPb.Size;
                                        globalSizePanel = flowLayoutPanel.Size;
                                        padding = (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3")) ? 
                                        flowLayoutPanel.Margin.Left:(flowLayoutPanel.Margin.Bottom-4);
                                        imgg = dirPb.Image;
                                        dirPb.SizeMode = PictureBoxSizeMode.CenterImage;
                                        if (compact)
                                        {
                                            gif = arrGif.Count() != 0 ?
                                                arrGif[r.Next(0, arrGif.Count())] : "";
                                            if (gif != "" && File.Exists(gif))
                                            {
                                                dirPb.Region = null;
                                                flowLayoutPanel.Region = null;
                                                if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                                                {
                                                    for (int z = 0; z < dirPb.Parent.Parent.Controls.Count; z++)
                                                    {
                                                        //if (z != 0)
                                                        dirPb.Parent.Parent.Controls[z].Margin = new Padding(0);
                                                        /*if (z== dirPb.Parent.Parent.Controls.Count-1 && dirPb.Parent == dirPb.Parent.Parent.Controls[z])
                                                        {
                                                            dirPb.Parent.Parent.Controls[z-1].Controls[1].Hide();
                                                            dirPb.Parent.Parent.Controls[z - 1].Size = globalSize;
                                                        }
                                                        else if(dirPb.Parent == dirPb.Parent.Parent.Controls[z])
                                                        {
                                                            dirPb.Parent.Parent.Controls[z + 1].Controls[1].Hide();
                                                            dirPb.Parent.Parent.Controls[z + 1].Size = globalSize;
                                                        }*/
                                                    }
                                                    dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                                                    dirPb.Image = Image.FromFile(gif);
                                                    dirPb.Size = new Size(globalSizePanel.Width + ((dirPb.Parent.Parent.Controls.Count) * padding), dirPb.Height);
                                                    dirPb.Parent.Size = new Size(globalSizePanel.Width + ((dirPb.Parent.Parent.Controls.Count) * padding), dirPb.Parent.Height);
                                                    vidDetails.Hide();
                                                }
                                                else
                                                {
                                                    dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                                                    dirPb.Image = Image.FromFile(gif);
                                                    dirPb.Size = new Size(globalSizePanel.Width, globalSizePanel.Height + padding);
                                                    vidDetails.Hide();
                                                }
                                            }
                                            else
                                                dirPb.Image = resizedImage(imgg, 248, 0, 388, 0);
                                        }
                                        else
                                        {
                                            gif = arrGif.Count() != 0 ?
                                                arrGif[r.Next(0, arrGif.Count())] : "";
                                            if (gif != "" && File.Exists(gif))
                                            {
                                                dirPb.Region = null;
                                                flowLayoutPanel.Region = null;
                                                if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                                                {
                                                    for (int z = 0; z < dirPb.Parent.Parent.Controls.Count; z++)
                                                    {
                                                        //if (z != 0)
                                                        dirPb.Parent.Parent.Controls[z].Margin = new Padding(0);
                                                        /*if (z== dirPb.Parent.Parent.Controls.Count-1 && dirPb.Parent == dirPb.Parent.Parent.Controls[z])
                                                        {
                                                            dirPb.Parent.Parent.Controls[z-1].Controls[1].Hide();
                                                            dirPb.Parent.Parent.Controls[z - 1].Size = globalSize;
                                                        }
                                                        else if(dirPb.Parent == dirPb.Parent.Parent.Controls[z])
                                                        {
                                                            dirPb.Parent.Parent.Controls[z + 1].Controls[1].Hide();
                                                            dirPb.Parent.Parent.Controls[z + 1].Size = globalSize;
                                                        }*/
                                                    }
                                                    dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                                                    dirPb.Image = Image.FromFile(gif);
                                                    dirPb.Size = new Size(globalSizePanel.Width + ((dirPb.Parent.Parent.Controls.Count) * padding), dirPb.Height);
                                                    dirPb.Parent.Size = new Size(globalSizePanel.Width + ((dirPb.Parent.Parent.Controls.Count) * padding), dirPb.Parent.Height);
                                                    vidDetails.Hide();
                                                }
                                                else
                                                {
                                                    dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                                                    dirPb.Image = Image.FromFile(gif);
                                                    dirPb.Size = new Size(globalSizePanel.Width, globalSizePanel.Height + padding);
                                                    vidDetails.Hide();

                                                }
                                            }
                                            else
                                                dirPb.Image = resizedImage(imgg, 308, 0, 513, 0);
                                        }
                                    };

                                    /*dirPb.MouseHover += (s1, a1) =>
                                    {
                                        dirPb.Image = resizedImage(imgg, 241, 0, 381, 0);
                                        timer1.Interval = 10;
                                        timer1.Enabled = true;
                                        timer1.Tick += (s2, a2) =>
                                        {
                                            if (queue > 10)
                                            {
                                                timer1.Enabled = false;
                                                return;
                                            }

                                            if (dirPb.Image != null) dirPb.Image.Dispose();
                                            if (compact)
                                            {
                                                dirPb.Image = resizedImage(imgg, 241 + queue, 0, 381 + queue, 0);
                                            }
                                            else
                                            {
                                                dirPb.Image = resizedImage(imgg, 308, 0, 513, 0);
                                            }
                                            queue++;
                                        };
                                    };*/


                                    dirPb.MouseLeave += (s, args) =>
                                    {

                                    //queue = 0;
                                    //timer1.Enabled = false;
                                    //floatingGif.Hide(); 
                                    //floatingGif.Region = null;

                                    if (dirPb.Image != null)
                                        {
                                            dirPb.Image.Dispose();
                                            GC.Collect();
                                            dirPb.Image = imgg;

                                            if (gif != "" && File.Exists(gif))
                                            {
                                                if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                                                {
                                                    for (int z = 0; z < dirPb.Parent.Parent.Controls.Count; z++)
                                                    {
                                                        //if (z != 0)
                                                        dirPb.Parent.Parent.Controls[z].Margin = new Padding(padding, 0, 0, 0);
                                                        /*if (z == dirPb.Parent.Parent.Controls.Count - 1 && dirPb.Parent == dirPb.Parent.Parent.Controls[z])
                                                        {
                                                            dirPb.Parent.Parent.Controls[z - 1].Controls[1].Show();
                                                            dirPb.Parent.Parent.Controls[z - 1].Size = dirPb.Parent.Size;
                                                        }
                                                        else if (dirPb.Parent == dirPb.Parent.Parent.Controls[z])
                                                        {
                                                            dirPb.Parent.Parent.Controls[z + 1].Controls[1].Show();
                                                            dirPb.Parent.Parent.Controls[z + 1].Size = dirPb.Parent.Size;
                                                        }*/
                                                    }

                                                }
                                                dirPb.Parent.Size = globalSizePanel;
                                                dirPb.Size = globalSize;
                                                dirPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dirPb.Width, dirPb.Height, 30, 30));
                                                flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 30, 30));
                                            }
                                            vidDetails.Show();
                                        }
                                        dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                                    };

                                    dirPb.MouseDoubleClick += (s, args) =>
                                    {
                                        int idx = disposableBoxes.IndexOf(dirPb);
                                        String prevStr = disposableBoxes.ElementAt((idx - 1 < 0) ? (disposableBoxes.Count - 1) : (idx - 1)).Name;
                                        String nextStr = disposableBoxes.ElementAt((idx + 1 >= disposableBoxes.Count) ? (0) : (idx + 1)).Name;
                                        pbClick(keyValuePair.Key, subDi, prevStr, nextStr);
                                    };
                                }

                                if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                                {
                                    if (compact)
                                    {
                                        i = i + 3;
                                    }
                                    else
                                    {
                                        i = i + 2;
                                    }
                                }
                                else
                                {
                                    if (compact)
                                    {
                                        i = i + 2;
                                    }
                                    else
                                    {
                                        i = i + 1;
                                    }
                                }
                                flowLayoutPanel1.Controls.Add(indFlowLayoutPanel);
                            }
                        }

                    }
                }

                hoverPointer.Visible = true;
                pointer.Visible = true;
                if (flowLayoutPanel1.Controls.Count == 0)
                {

                    hoverPointer.Visible = false;
                    pointer.Visible = false;
                    //flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Size.Width+24, flowLayoutPanel1.Size.Height + flowLayoutPanel4.Size.Height+5 + flowLayoutPanel2.Height);
                    flowLayoutPanel1.Size = new Size(1945, 1080);
                    flowLayoutPanel1.Location = stackedDb ? flowPanel1Loc : new Point(0, 44);
                    flowLayoutPanel1.BringToFront();


                    if (useAndThrow)
                    {
                        if (dupeLabel2 == null) dupeLabel2 = new Label();
                        dupeLabel2.Text = "Dashboard Refresh";
                        dupeLabel2.Image = global::Calculator.Properties.Resources.icons8_dashboard_32;
                        dupeLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        dupeLabel2.Font = new Font("Consolas", 15, FontStyle.Bold);
                        dupeLabel2.BackColor = lightBackColor;
                        dupeLabel2.Size = stackedDb ? stackedSizeDbBtn : unStackedSizeDbBtn;
                        dupeLabel2.ForeColor = Color.White;
                        dupeLabel2.TextAlign = ContentAlignment.MiddleCenter;
                        dupeLabel2.Margin = new Padding(8, 0, 0, 0);
                        dupeLabel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dupeLabel2.Width, dupeLabel2.Height, 8, 8));
                        dupeLabel2.MouseEnter += (s, args) =>
                        {
                            dupeLabel2.ForeColor = mouseClickColor;
                            flowLayoutPanel1_MouseEnter(s, args);
                        };
                        dupeLabel2.MouseLeave += (s, args) =>
                        {
                            dupeLabel2.ForeColor = Color.White;
                        };
                        dupeLabel2.MouseClick += (s, a) =>
                        {
                            dashBoard_Click(null, null);
                        };
                        //if (!stackedDb) flowLayoutPanel1.Controls.Add(menuBtn2);
                        //flowLayoutPanel1.Controls.Add(dupeLabel2);

                        if (playRandom == null) playRandom = new Label();
                        playRandom.Text = "Play Random ";
                        playRandom.Font = new Font("Consolas", 12, FontStyle.Bold);
                        playRandom.BackColor = lightBackColor;
                        playRandom.Size = stackedDb ? stackedSizeRandBtn : unStackedSizeRandBtn;
                        playRandom.ForeColor = Color.White;
                        playRandom.TextAlign = ContentAlignment.MiddleRight;
                        playRandom.Margin = new Padding(25, 0, 25, 0);
                        playRandom.Image = global::Calculator.Properties.Resources.random__1_;
                        playRandom.ImageAlign = ContentAlignment.MiddleLeft;
                        playRandom.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, playRandom.Width, playRandom.Height, 8, 8));
                        playRandom.MouseEnter += (s, args) =>
                        {
                            playRandom.ForeColor = mouseClickColor;
                            flowLayoutPanel1_MouseEnter(s, args);
                        };
                        playRandom.MouseLeave += (s, args) =>
                        {
                            playRandom.ForeColor = Color.White;
                        };
                        playRandom.MouseClick += (s, a) =>
                        {
                            Random random = new Random();
                            PictureBox pb = tempRandomFilesPreloaded.ElementAt(random.Next(tempRandomFilesPreloaded.Count));
                            this.Hide();
                            staticExp = this;

                            wmp.setRefPb(pb, tempRandomFilesPreloaded, null, false);
                            wmp.axWindowsMediaPlayer1.URL = pb.Name;
                            wmp.axWindowsMediaPlayer1.Name = pb.Name;
                            wmp.Location = new Point(0, 28);
                            wmp.calculateDuration(0);
                            wmp.Show();
                            this.backgroundWorker1.RunWorkerAsync(100);
                        };
                        //flowLayoutPanel1.Controls.Add(playRandom);

                        if (shorts == null) shorts = new Label();
                        shorts.Text = "Play Shorts";
                        shorts.Font = new Font("Consolas", 12, FontStyle.Bold);
                        shorts.BackColor = lightBackColor;
                        shorts.Size = stackedDb ? stackedSizeRandBtn : unStackedSizeRandBtn;
                        shorts.ForeColor = Color.White;
                        shorts.TextAlign = ContentAlignment.MiddleRight;
                        shorts.Margin = new Padding(0, 0, 25, 0);
                        shorts.Image = global::Calculator.Properties.Resources.random__1_;
                        shorts.ImageAlign = ContentAlignment.MiddleLeft;
                        shorts.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, shorts.Width, shorts.Height, 8, 8));
                        shorts.MouseEnter += (s, args) =>
                        {
                            shorts.ForeColor = mouseClickColor;
                            flowLayoutPanel1_MouseEnter(s, args);
                        };
                        shorts.MouseLeave += (s, args) =>
                        {
                            shorts.ForeColor = Color.White;
                        };
                        shorts.MouseClick += (s, a) =>
                        {
                            Random random = new Random();

                            PictureBox pb = tempShortFilesPreloaded.ElementAt(random.Next(tempShortFilesPreloaded.Count));

                            this.Hide();
                            staticExp = this;
                            wmp.setRefPb(pb, tempShortFilesPreloaded, null, false);
                            wmp.axWindowsMediaPlayer1.URL = pb.Name;
                            wmp.axWindowsMediaPlayer1.Name = pb.Name;
                            wmp.Location = new Point(0, 28);
                            wmp.repeat = true;
                            wmp.calculateDuration(0);
                            wmp.Show();
                            this.backgroundWorker1.RunWorkerAsync(100);
                        };


                        if (affLbl == null) affLbl = new Label();
                        affLbl.Text = "Play Best";
                        affLbl.Font = new Font("Consolas", 12, FontStyle.Bold);
                        affLbl.BackColor = lightBackColor;
                        affLbl.Size = stackedDb ? stackedSizeRandBtn : unStackedSizeRandBtn;
                        affLbl.ForeColor = Color.White;
                        affLbl.TextAlign = ContentAlignment.MiddleRight;
                        affLbl.Margin = new Padding(0, 0, 25, 0);
                        affLbl.Image = global::Calculator.Properties.Resources.random__1_;
                        affLbl.ImageAlign = ContentAlignment.MiddleLeft;
                        affLbl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, affLbl.Width, affLbl.Height, 8, 8));
                        affLbl.MouseEnter += (s, args) =>
                        {
                            affLbl.ForeColor = mouseClickColor;
                            flowLayoutPanel1_MouseEnter(s, args);
                        };
                        affLbl.MouseLeave += (s, args) =>
                        {
                            affLbl.ForeColor = Color.White;
                        };
                        affLbl.MouseClick += (s, a) =>
                        {
                            Random random = new Random();

                            PictureBox pb = tempAffinityFilesPreloaded.ElementAt(random.Next(tempShortFilesPreloaded.Count));

                            this.Hide();
                            staticExp = this;
                            wmp.setRefPb(pb, tempAffinityFilesPreloaded, null, false);
                            wmp.axWindowsMediaPlayer1.URL = pb.Name;
                            wmp.axWindowsMediaPlayer1.Name = pb.Name;
                            wmp.Location = new Point(0, 28);
                            wmp.repeat = true;
                            wmp.calculateDuration(0);
                            wmp.Show();
                            this.backgroundWorker1.RunWorkerAsync(100);
                        };

                        //flowLayoutPanel1.Controls.Add(shorts);

                        //flowLayoutPanel1.Controls.Add(button4);
                        //flowLayoutPanel1.Controls.Add(button3);
                        //flowLayoutPanel1.Controls.Add(button2);

                        if (flowLayoutPanel == null) flowLayoutPanel = new FlowLayoutPanel();
                        flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                        flowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                        flowLayoutPanel.Margin = new Padding(0, 3, 0, 8);
                        flowLayoutPanel.Size = new Size(flowLayoutPanel1.Width, shorts.Height + 2);
                        flowLayoutPanel.Location = new Point(stackedDb ? 288 : 0, 0);

                        if (!stackedDb) flowLayoutPanel.Controls.Add(menuBtn2);
                        flowLayoutPanel.Controls.Add(dupeLabel2);
                        flowLayoutPanel.Controls.Add(playRandom);
                        flowLayoutPanel.Controls.Add(affLbl);
                        flowLayoutPanel.Controls.Add(shorts);
                        flowLayoutPanel.Controls.Add(button6);
                        flowLayoutPanel.Controls.Add(button5);
                        flowLayoutPanel.Controls.Add(button1);
                    }

                    this.Controls.Add(flowLayoutPanel);
                    flowLayoutPanel.BringToFront();

                    Label resumeLabel = new Label();
                    resumeLabel.Text = "Recent";
                    resumeLabel.Font = new Font("Consolas", 26, FontStyle.Bold);
                    resumeLabel.BackColor = flowLayoutPanel1.BackColor;
                    resumeLabel.Size = new Size(flowLayoutPanel1.Size.Width - 50, 44);
                    resumeLabel.ForeColor = Color.White;
                    resumeLabel.TextAlign = ContentAlignment.MiddleLeft;
                    resumeLabel.Margin = new Padding(15, 10, 0, 15);
                    resumeLabel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, resumeLabel.Width, resumeLabel.Height, 8, 8));
                    resumeLabel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);

                    if (dashBoardRefresh)
                    {
                        if (File.Exists(Explorer.directory3.FullName + "\\resumeDb.txt"))
                        {
                            resumeFiles = File.ReadAllLines(Explorer.directory3.FullName + "\\resumeDb.txt").ToList();
                        }
                    }
                    Label newLabel = new Label();
                    newLabel.Text = "Newly Added";
                    newLabel.Font = new Font("Consolas", 26, FontStyle.Bold);
                    newLabel.BackColor = flowLayoutPanel1.BackColor;
                    newLabel.Size = new Size(flowLayoutPanel1.Size.Width - 50, 44);
                    newLabel.ForeColor = Color.White;
                    newLabel.TextAlign = ContentAlignment.MiddleLeft;
                    newLabel.Margin = new Padding(15, 10, 0, 15);
                    newLabel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, resumeLabel.Width, resumeLabel.Height, 8, 8));
                    newLabel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);

                    if (dashBoardRefresh)
                    {
                        if (File.Exists(Explorer.directory3.FullName + "\\newFiles.txt"))
                        {
                            newFilesDb = File.ReadAllLines(Explorer.directory3.FullName + "\\newFiles.txt").ToList();
                        }
                    }
                    if (newFilesDb.Count > 0)
                    {
                        flowLayoutPanel1.Controls.Add(newLabel);
                        FillUpDashboard(newFilesDb, true);
                    }

                    flowLayoutPanel1.Controls.Add(resumeLabel);
                    FillUpDashboard(resumeFiles, true);

                    Label DashboardLabel = new Label();
                    DashboardLabel.Text = "Dashboard";
                    DashboardLabel.Font = new Font("Consolas", 26, FontStyle.Bold);
                    DashboardLabel.BackColor = flowLayoutPanel1.BackColor;
                    DashboardLabel.Size = new Size(flowLayoutPanel1.Size.Width - 50, 44);
                    DashboardLabel.ForeColor = Color.White;
                    DashboardLabel.TextAlign = ContentAlignment.MiddleLeft;
                    DashboardLabel.Margin = new Padding(15, 20, 0, 15);
                    DashboardLabel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, DashboardLabel.Width, DashboardLabel.Height, 8, 8));
                    DashboardLabel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
                    flowLayoutPanel1.Controls.Add(DashboardLabel);

                    if (dashBoardRefresh)
                    {
                        randomFiles = FilesDashboard(false, false, false);
                    }
                    FillUpDashboard(randomFiles, false);
                    flowLayoutPanel1.Controls.Add(axWindowsMediaPlayer1);
                    axWindowsMediaPlayer1.Size = new Size((stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width) + 74, (int)(((stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width) + 74) * 0.567));
                    newProgressBar.Size = new Size((stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width) + 74, 3);
                    //videoHoverEnabler.Clear();
                    dashBoardRefresh = false;

                }

                if (useAndThrow)
                {
                    /*Button dummy = new Button();
                    dummy.Text = "";
                    dummy.Font = new Font("Consolas", 13, FontStyle.Bold);
                    dummy.BackColor = darkBackColor;
                    dummy.Size = new Size(290, 27);
                    dummy.Margin = new Padding(0, 3, 3, 3);
                    dummy.FlatStyle = FlatStyle.Flat;
                    dummy.FlatAppearance.MouseOverBackColor = darkBackColor;
                    dummy.FlatAppearance.MouseDownBackColor = darkBackColor;
                    dummy.FlatAppearance.BorderSize = 0;
                    dummy.MouseClick += (s, a) =>
                    {
                        textBox3.Focus();
                    };*/
                    //flowLayoutPanel3.Controls.Add(dummy);

                    //flowLayoutPanel4.Controls.Remove(searchLabel);
                    //flowLayoutPanel4.Controls.Remove(textBox3);
                    /*foreach (String type in typeList)
                    {
                        Button butt = new Button();
                        butt.Text = type;
                        butt.Font = new Font("Consolas", 10, FontStyle.Bold);
                        butt.Cursor = System.Windows.Forms.Cursors.Arrow;
                        butt.ForeColor = Color.White;
                        butt.BackColor = darkBackColor;
                        butt.Size = new Size(214, 46);
                        butt.Margin = new Padding(0, 1, 2, 0);
                        butt.FlatStyle = FlatStyle.Flat;
                        butt.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, butt.Width, butt.Height, 5, 5));
                        //butt.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
                        butt.ImageAlign = ContentAlignment.MiddleLeft;
                        if (butt.Text.Equals(Calculator.globalType))
                        {
                            Calculator.globalTypeButton = butt;
                            butt.ForeColor = mouseClickColor;
                        }
                        else
                        {
                            butt.ForeColor = Color.White;
                            butt.BackColor = darkBackColor;
                        }
                        butt.FlatAppearance.BorderSize = 0;
                        butt.FlatAppearance.MouseOverBackColor = darkBackColor;
                        butt.FlatAppearance.MouseDownBackColor = darkBackColor;
                        butt.TextAlign = ContentAlignment.MiddleRight;

                        butt.Image = ((System.Drawing.Image)(resources.GetObject("video-player.Image")));
                        butt.ImageAlign = ContentAlignment.MiddleLeft;
                        butt.MouseClick += (s, a) =>
                        {
                            this.butt1.Text = butt.Text;
                            Calculator.globalTypeButton.BackColor = darkBackColor;
                            Calculator.globalTypeButton.ForeColor = Color.White;
                            Calculator.globalType = butt.Text;
                            if (Calculator.globalType.Contains("Short"))
                                typeName.Text = "Gif vid";
                            else
                                typeName.Text = Calculator.globalType;
                            butt.ForeColor = mouseClickColor;
                            Calculator.globalTypeButton = butt;
                            textBox3.Select();
                            textBox3.Focus();
                        };
                        typeButtons.Add(butt);
                        flowLayoutPanel4.Controls.Add(butt);
                    }
                    //flowLayoutPanel4.Controls.Add(textBox3);
                    //flowLayoutPanel4.Controls.Add(searchLabel);
                    */
                    useAndThrow = false;
                    /*if (Calculator.globalDirButton != null)
                    {
                        enlargeEnter(Calculator.globalDirButton);
                        isEnlarged[Calculator.globalDirButton.Text] = true;
                    }*/
                }
            }
            else
            {
                button1.Hide();
                button5.Hide();
                button6.Hide();
                if (useAndThrow)
                {
                    Label dirName = new Label();
                    dirName.Text = gamesDirectory.GetDirectories().ElementAt(0).Name.Substring(1);
                    dirName.Font = new Font("Consolas", 21, FontStyle.Regular);
                    dirName.BackColor = flowLayoutPanel1.BackColor;
                    dirName.Margin = new Padding(5, 10, 0, 0);
                    dirName.Size = new Size(270, 120);
                    dirName.Cursor = Cursors.Hand;
                    dirName.ForeColor = Color.White;
                    dirName.TextAlign = ContentAlignment.MiddleCenter;
                    dirName.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dirName.Width, dirName.Height, 15, 15));
                    flowLayoutPanel3.Controls.Add(dirName);
                    useAndThrow = !useAndThrow;
                }
                countFiles.Text = "No of folders: 0";
                foreach (DirectoryInfo subDi in gamesDirectory.GetDirectories().OrderBy(f => f.Name).ToList())
                {
                    Label spaceBar = new Label();
                    spaceBar.BackColor = darkBackColor;
                    spaceBar.Size = new Size(1500, 20);
                    flowLayoutPanel1.Controls.Add(spaceBar);

                    typeName.Text = Calculator.globalType;
                    typeName.Font = new Font("Arial", 35, FontStyle.Bold);
                    typeName.BackColor = lightBackColor;
                    typeName.ForeColor = Color.White;
                    typeName.Size = new Size(275, 80);
                    typeName.FlatStyle = FlatStyle.Flat;
                    typeName.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, typeName.Width, typeName.Height, 20, 20));
                    typeName.FlatAppearance.BorderSize = 0;
                    //flowLayoutPanel1.Controls.Add(typeName);

                    foreach (DirectoryInfo subFolder in subDi.GetDirectories())
                    {

                        folders.Add(subFolder.FullName);
                        String vidDetText = "\n" + subFolder.FullName.Substring(subFolder.FullName.LastIndexOf("\\") + 1) + "\n";
                        DirectoryInfo videoDi = new DirectoryInfo(subFolder.FullName);
                        long[] number = noOfFiles(videoDi);
                        vidDetText = vidDetText + "\n" + "No of Videos - " + number[0];
                        String size = sizeStr(number[1]);
                        vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                        if (!Directory.Exists(subFolder.FullName + "\\Pics"))
                            Directory.CreateDirectory(subFolder.FullName + "\\Pics");
                        videoDi = new DirectoryInfo(subFolder.FullName + "\\Pics");
                        number = noOfFiles(videoDi);
                        vidDetText = vidDetText + "\n" + "No of Pictures - " + number[0];
                        size = sizeStr(number[1]);
                        vidDetText = vidDetText + "\n" + "Size - " + size + "\n";

                        if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3") || !compact)
                        {
                            if (!Directory.Exists(subFolder.FullName + "\\Pics\\kkkk"))
                                Directory.CreateDirectory(subFolder.FullName + "\\Pics\\kkkk");
                            videoDi = new DirectoryInfo(subFolder.FullName + "\\Pics\\kkkk");
                            number = noOfFiles(videoDi);
                            vidDetText = vidDetText + "\n" + "No of 4K Pics - " + number[0];
                            size = sizeStr(number[1]);
                            vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                            if (!Directory.Exists(subFolder.FullName + "\\Pics\\GifVideos"))
                                Directory.CreateDirectory(subFolder.FullName + "\\Pics\\GifVideos");
                            videoDi = new DirectoryInfo(subFolder.FullName + "\\Pics\\GifVideos");
                            number = noOfFiles(videoDi);
                            vidDetText = vidDetText + "\n" + "No of Gif Videos - " + number[0];
                            size = sizeStr(number[1]);
                            vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                            if (!Directory.Exists(subFolder.FullName + "\\Pics\\Gifs"))
                                Directory.CreateDirectory(subFolder.FullName + "\\Pics\\Gifs");
                            videoDi = new DirectoryInfo(subFolder.FullName + "\\Pics\\Gifs");
                            number = noOfFiles(videoDi);
                            vidDetText = vidDetText + "\n" + "No of Gifs - " + number[0];
                            size = sizeStr(number[1]);
                            vidDetText = vidDetText + "\n" + "Size - " + size + "\n";
                        }
                        vidDetText = vidDetText + "\n" + "Priority - " + 0;

                        PictureBox dirPb = new PictureBox();
                        dirPb.Margin = new Padding(0, 8, 0, 0);
                        dirPb.Name = subFolder.FullName;


                        /*DirectoryInfo picsDir = new DirectoryInfo(subFolder.FullName + "\\Pics\\imgPB");
                        try
                        {
                            foreach (FileInfo fi in picsDir.GetFiles())
                            {
                                fi.Delete();
                            }
                        }
                        catch { }*/

                        /* DirectoryInfo picsDir = new DirectoryInfo(subFolder.FullName + "\\Pics");
                         createSmallResFiles(picsDir);
                         picsDir = new DirectoryInfo(subFolder.FullName + "\\Pics\\kkkk");
                         createSmallResFiles(picsDir);*/

                        /*DirectoryInfo picsDir = new DirectoryInfo(subFolder.FullName + "\\Pics\\Gifs\\imgPB");
                        foreach (FileInfo fi in picsDir.GetFiles())
                        {
                            Image image = Image.FromFile(fi.FullName);
                            Bitmap bmp = new Bitmap(image);
                            image.Dispose();
                            ImageCodecInfo myImageCodecInfo;
                            System.Drawing.Imaging.Encoder myEncoder;
                            EncoderParameter myEncoderParameter;
                            EncoderParameters myEncoderParameters;
                            myImageCodecInfo = GetEncoderInfo("image/jpeg");
                            myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            myEncoderParameters = new EncoderParameters(1);
                            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            File.Delete(fi.FullName);
                            bmp.Save(fi.FullName, myImageCodecInfo, myEncoderParameters);
                            bmp.Dispose();
                        }*/

                        Label vidDetails = new Label();
                        if (subDi.Name.Substring(0, 1).Equals("z") || subDi.Name.Substring(0, 1).Equals("0") || subDi.Name.Substring(0, 1).Equals("2") || subDi.Name.Substring(0, 1).Equals("3"))
                        {
                            if (compact)
                            {
                                dirPb.Size = new Size(240, 357);
                                vidDetails.Size = new Size(135, 337);
                                dirPb.Margin = new Padding(20, 0, 0, 13);
                                vidDetails.Padding = new Padding(5, 0, 0, 0);
                                vidDetails.Margin = new Padding(0, 11, 0, 0);
                                vidDetails.Font = new Font("Consolas", 7, FontStyle.Regular);
                            }
                            else
                            {
                                dirPb.Size = new Size(300, 450);
                                dirPb.Margin = new Padding(24, 0, 0, 13);
                                vidDetails.Size = new Size(180, 405);
                                vidDetails.Padding = new Padding(7, 0, 0, 0);
                                vidDetails.Margin = new Padding(0, 20, 35, 0);
                                vidDetails.Font = new Font("Consolas", 8, FontStyle.Regular);
                            }
                        }
                        else
                        {
                            if (compact)
                            {
                                dirPb.Size = new Size(380, 246);
                                dirPb.Margin = new Padding(12, 0, 0, 9);
                                vidDetails.Size = new Size(140, 223);
                                vidDetails.Padding = new Padding(0, 0, 0, 0);
                                vidDetails.Margin = new Padding(1, 13, 0, 0);
                                vidDetails.Font = new Font("Consolas", 7, FontStyle.Regular);
                            }
                            else
                            {
                                dirPb.Size = new Size(505, 327);
                                dirPb.Margin = new Padding(50, 0, 0, 8);
                                vidDetails.Size = new Size(180, 310);
                                vidDetails.Padding = new Padding(8, 0, 0, 0);
                                vidDetails.Margin = new Padding(2, 10, 50, 0);
                                vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                            }
                        }

                        dirPb.BackColor = lightBackColor;
                        dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                        dirPb.Cursor = Cursors.Hand;
                        dirPb.ContextMenuStrip = contextMenuStrip1;
                        dirPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dirPb.Width, dirPb.Height, 30, 30));

                        String img = File.ReadAllText(subFolder.FullName + "\\disPic.txt").Trim();
                        if (img != "")
                        {
                            img = img.Substring(img.IndexOf("Pics"));
                            img = subFolder.FullName + "\\" + img;
                        }

                        File.WriteAllText(subFolder.FullName + "\\disPic.txt", img);

                        if (File.Exists(img))
                        {
                            Image image = Image.FromFile(img);

                            //Bitmap bmp = new Bitmap(image);
                            /*image.Dispose();

                            ImageCodecInfo myImageCodecInfo;
                            System.Drawing.Imaging.Encoder myEncoder;
                            EncoderParameter myEncoderParameter;
                            EncoderParameters myEncoderParameters;
                            myImageCodecInfo = GetEncoderInfo("image/jpeg");
                            myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            myEncoderParameters = new EncoderParameters(1);
                            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            File.Delete(img);
                            bmp.Save(img, myImageCodecInfo, myEncoderParameters);*/

                            dirPb.Image = image;

                            if (!Directory.Exists(subFolder.FullName + "\\imgPB)"))
                            {
                                Directory.CreateDirectory(subFolder.FullName + "\\imgPB");
                            }
                            if (File.Exists(subFolder.FullName + "\\imgPB\\smallRes.png"))
                            {
                                File.Delete(subFolder.FullName + "\\imgPB\\smallRes.png");
                            }
                        }
                        try { flowLayoutPanel1.Controls.Add(dirPb); } catch { }

                        vidDetails.Text = vidDetText;
                        vidDetails.BackColor = darkBackColor;
                        vidDetails.ForeColor = Color.White;
                        vidDetails.TextAlign = ContentAlignment.TopLeft;
                        vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 5, 5));

                        flowLayoutPanel1.Controls.Add(vidDetails);
                        grpLabels.Add(vidDetails);

                        disposableBoxes.Add(dirPb);

                        dirPb.MouseClick += (s, args) =>
                        {
                            if (selectedPb == null)
                            {
                                globalLabel = vidDetails;
                                globalLabel.ForeColor = selectedPbColor;
                                selectedPb = dirPb;
                                selectedPb.BackColor = selectedPbColor;
                                Font myfont1 = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                                globalLabel.Font = myfont1;
                                return;
                            }
                            if (globalLabel == null)
                            {
                                globalLabel = vidDetails;
                                globalLabel.ForeColor = selectedPbColor;

                                Font myfont1 = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                                globalLabel.Font = myfont1;
                            }

                            selectedPb.BackColor = lightBackColor;
                            Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;

                            Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                            globalLabel.Font = myfont;
                            globalLabel.ForeColor = Color.White;
                            selectedPb = dirPb;
                            selectedPb.BackColor = selectedPbColor;
                            globalLabel = vidDetails;

                            myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                            globalLabel.Font = myfont;
                            globalLabel.ForeColor = selectedPbColor;

                        };
                        Image imgg = null;
                        int queue = 0;


                        dirPb.MouseEnter += (s, args) =>
                        {
                            //timer1.Interval = 10;
                            //timer1.Start();
                            //queue = 1;
                            imgg = dirPb.Image;
                            dirPb.SizeMode = PictureBoxSizeMode.CenterImage;
                            if (compact)
                            {
                                dirPb.Image = resizedImage(imgg, 248, 0, 388, 0);
                            }
                            else
                            {
                                dirPb.Image = resizedImage(imgg, 308, 0, 513, 0);
                            }
                        };

                        /*dirPb.MouseHover += (s1, a1) =>
                        {
                            dirPb.Image = resizedImage(imgg, 241, 0, 381, 0);
                            timer1.Interval = 10;
                            timer1.Enabled = true;
                            timer1.Tick += (s2, a2) =>
                            {
                                if (queue > 10)
                                {
                                    timer1.Enabled = false;
                                    return;
                                }

                                if (dirPb.Image != null) dirPb.Image.Dispose();
                                if (compact)
                                {
                                    dirPb.Image = resizedImage(imgg, 241 + queue, 0, 381 + queue, 0);
                                }
                                else
                                {
                                    dirPb.Image = resizedImage(imgg, 308, 0, 513, 0);
                                }
                                queue++;
                            };
                        };*/


                        dirPb.MouseLeave += (s, args) =>
                        {

                            //queue = 0;
                            //timer1.Enabled = false;
                            if (dirPb.Image != null)
                            {
                                dirPb.Image.Dispose();
                                dirPb.Image = imgg;
                            }
                            dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                        };

                        dirPb.MouseDoubleClick += (s, args) =>
                        {
                            int idx = disposableBoxes.IndexOf(dirPb);
                            String prevStr = disposableBoxes.ElementAt((idx - 1 < 0) ? (disposableBoxes.Count - 1) : (idx - 1)).Name;
                            String nextStr = disposableBoxes.ElementAt((idx + 1 >= disposableBoxes.Count) ? (0) : (idx + 1)).Name;
                            pbClick(subFolder.FullName, subDi, prevStr, nextStr);
                        };
                    }
                }

            }
        }

        private void FillUpDashboard(List<String> randomFiles, Boolean isShort)
        {
            List<Label> meta = new List<Label>();
            for (int k= 0; k < randomFiles.Count; k++)
            {
                    FlowLayoutPanel indFlowLayoutPanel = new FlowLayoutPanel();
                    indFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                    indFlowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                    indFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);
                    indFlowLayoutPanel.Size = new Size(flowLayoutPanel1.Width, (stackedDb ? stackedSizePbDb.Height : unStackedSizePbDb.Height) + 73);

                indFlowLayoutPanel.MouseEnter += new EventHandler(flowLayoutPanel1_MouseEnter);
                for (int j = 0; j < 4; j++) {
                    if (k + j == randomFiles.Count)
                        break;
                    String fileName = randomFiles[k + j];
                    if (!File.Exists(fileName.Substring(0, fileName.IndexOf("@@!"))))
                        continue;
                    PictureBox pb = new PictureBox();
                    pb.Dock = DockStyle.Top;
                    pb.Name = fileName.Substring(0, fileName.IndexOf("@@!"));

                    //videoHoverEnabler.Add(fileName, false);
                    pb.Size = stackedDb ? stackedSizePbDb : unStackedSizePbDb;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    //pb.ContextMenuStrip = contextMenuStrip1;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.BackColor = lightBackColor;
                    pb.Image = setDefaultPic(new FileInfo(fileName.Substring(0, fileName.IndexOf("@@!"))), pb);
                    pb.Margin = new Padding(0);

                    videosPb.Add(pb);
                    Label vidDetails = new Label();
                    String title = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    try
                    {
                        title = title.Substring(0, title.IndexOf("Dura")).Replace("^", "").Substring(title.IndexOf("Reso") + 4).Trim() + "\n"
                        + title.Substring(title.IndexOf("placeholdeerr") + "placeholdeerr".Length);
                    }
                    catch { }
                    vidDetails.Name = fileName.Substring(0, fileName.IndexOf("@@!"));
                    String file = (fileName.Contains("placeholdeerr") ? fileName.Replace("placeholdeerr00", "\n" + Directory.GetParent(fileName).Parent.Name.Substring(1) + "." + Directory.GetParent(fileName).Name + ".").Replace("placeholdeerr0", "\n" + Directory.GetParent(fileName).Parent.Name.Substring(1) + "." + Directory.GetParent(fileName).Name + ".")
                            .Replace("placeholdeerr", "\n" + Directory.GetParent(fileName).Parent.Name.Substring(1).Replace("uild", "Build") + "." + Directory.GetParent(fileName).Name + ".").Replace("Reso^ ", "Reso:").Replace("Dura^ ", "Dura:").Replace("Size^ ", "Size:").Substring(fileName.IndexOf("Reso")) : "");
                    if (fileName.Contains("placeholdeerr")) vidDetails.Text = file.Substring(0, file.IndexOf("@@!"));
                    vidDetails.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    vidDetails.BackColor = lightBackColor;
                    vidDetails.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, 50);
                    vidDetails.ForeColor = Color.White;
                    vidDetails.TextAlign = ContentAlignment.TopCenter;
                    vidDetails.Padding = new Padding(0, 3, 0, 0);
                    vidDetails.Margin = new Padding(0);
                    vidDetails.ContextMenuStrip = contextMenuStrip1;
                    vidDetailsStack.Add(vidDetails);
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

                            if (loc - prevX > (stackedDb ? 4 : 6) || loc - prevX < -1 * (stackedDb ? 4 : 6))
                            {
                                prevX = loc;
                                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = ((double)loc / (double)vidDetails.Width) * inWmpDuration;
                                newProgressBar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                            }
                        }
                    };

                    vidDetails.MouseClick += (s, e) =>
                    {
                        if (isHoveredOverPb)
                        {
                            this.Hide();
                            wmp.Location = new Point(0, 28);
                            staticExp = this;
                            wmp.setRefPb(pb, videosPb, null);
                            wmp.axWindowsMediaPlayer1.URL = pb.Name;
                            wmp.axWindowsMediaPlayer1.Name = pb.Name;
                            wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                            wmp.Show();
                            axWindowsMediaPlayer1.Ctlcontrols.pause(); flowLayoutPanel1_MouseEnter(null, null);
                        }
                    };

                    vidDetails.MouseLeave += (s, e) =>
                    {
                        whereAt = 1.0;
                        while ((whereAt / 9.0) * inWmpDuration < axWindowsMediaPlayer1.Ctlcontrols.currentPosition)
                        {
                            whereAt++;
                        }
                        axWindowsMediaPlayer1.settings.rate = 1.35;
                        timer1.Start();
                    };
                    //vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 4, 4));

                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                    flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
                    flowLayoutPanel.BackColor = flowLayoutPanel1.BackColor;
                    flowLayoutPanel.Margin = new Padding(17, 0, 0, 0);
                    flowLayoutPanel.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, (stackedDb ? stackedSizePbDb.Height : unStackedSizePbDb.Height) + 53);
                    flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));
                    flowLayoutPanel.Controls.Add(pb);
                    flowLayoutPanel.Controls.Add(vidDetails);
                    indFlowLayoutPanel.Controls.Add(flowLayoutPanel);
                    flowLayoutPanels.Add(flowLayoutPanel);

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

                        foreach(Control tempPanel1 in flowLayoutPanel.Parent.Controls)
                        {
                            tempPanel1.Margin = new Padding(2, 0, 0, 0);
                        }
                        //flowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width, (stackedDb ? stackedSizePbDb.Height : unStackedSizePbDb.Height) + 90);
                        vidDetails.Size = new Size(flowLayoutPanel.Width + 74, 50);
                        //vidDetails.Padding = new Padding(0, 5, 0, 0);
                        //vidDetails.ForeColor = vidDetails.BackColor;
                        newProgressBar.Size = new Size(flowLayoutPanel.Width + 74, 3);
                        flowLayoutPanel.Region = null;
                        flowLayoutPanel.Size = new Size(flowLayoutPanel.Width + 74, (int)((flowLayoutPanel.Width + 74) * 0.567) + 29);
                        flowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel.Width, flowLayoutPanel.Height, 12, 12));

                        flowLayoutPanel.Controls.Clear();

                        whereAt = 1.0;
                        Boolean entered = false;
                        Double currPosition = 0;
                        axWindowsMediaPlayer1.URL = pb.Name;
                        axWindowsMediaPlayer1.settings.rate = 1.35;
                        newProgressBar.Maximum = (int)inWmpDuration;
                        FileInfo fi = new FileInfo(pb.Name);
                        if (File.Exists(Explorer.directory3.FullName + "\\resumeDb.txt"))
                        {
                            List<String> temp = File.ReadAllLines(Explorer.directory3.FullName + "\\resumeDb.txt").ToList();

                            foreach (String str in temp)
                            {
                                if (str.Contains(pb.Name))
                                {
                                    currPosition = double.Parse(str.Substring(str.LastIndexOf("@@!") + 3));
                                    entered = true;
                                    break;
                                }
                            }
                        }

                        if (!entered && File.Exists(fi.DirectoryName + "\\resume.txt"))
                        {
                            String[] resumeFile = File.ReadAllLines(fi.DirectoryName + "\\resume.txt");
                            foreach (String str in resumeFile)
                                if (str.Contains("@@" + fi.Name + "@@!"))
                                {
                                    currPosition = Double.Parse(str.Substring(str.IndexOf("@@!") + 3));
                                    while ((whereAt / 9.0) * inWmpDuration < currPosition)
                                    {
                                        whereAt++;
                                    }
                                }
                        }

                        if (currPosition == 0)
                        {
                            whereAt = 1.0;
                            currPosition = (whereAt / 9.0) * inWmpDuration;
                            whereAt++;
                        }
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition = currPosition;
                        newProgressBar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                        //VideoPlayer.axWindowsMediaPlayer1.URL = pb.Name;
                        axWindowsMediaPlayer1.Margin = new Padding(0);
                    flowLayoutPanel.Controls.Add(axWindowsMediaPlayer1);
                        flowLayoutPanel.Controls.Add(newProgressBar);
                        flowLayoutPanel.Controls.Add(vidDetails);
                        if (!entered)
                        {
                            timer1.Start();
                        }
                    /*int idx = videosPb.IndexOf(pb);
                    pb.Parent.Margin = new Padding(0);
                    videosPb.ElementAt(idx - 1).Parent.Margin = new Padding(0);
                    videosPb.ElementAt(idx + 1).Parent.Margin = new Padding(0);
                    flowLayoutPanel.Region = null;
                    pb.Size = new Size(pb.Width + 20, pb.Height + 20);
                    flowLayoutPanel.Size = new Size(flowLayoutPanel.Width + 30, flowLayoutPanel.Height + 30);
                    /*int index = flowLayoutPanels.IndexOf(flowLayoutPanel);
                    flowLayoutPanels.ElementAt(index).Region = null;
                    flowLayoutPanels.ElementAt(index - 1).Margin = new Padding(0);
                    flowLayoutPanels.ElementAt(index + 1).Margin = new Padding(0);
                    flowLayoutPanels.ElementAt(index).Size = new Size(flowLayoutPanels.ElementAt(index).Width + 40, flowLayoutPanels.ElementAt(index).Height + 40);
                    axWindowsMediaPlayer1.Size = new Size(axWindowsMediaPlayer1.Width+ 40, axWindowsMediaPlayer1.Height + 40);*/

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

                    /*if (meta.Count == 4)
                    {
                        foreach (Label label in meta)
                        {
                            flowLayoutPanel1.Controls.Add(label);
                        }
                        meta.Clear();
                    }*/
                }
                k = k + 3;
                flowLayoutPanel1.Controls.Add(indFlowLayoutPanel);
            }
            /* for (int y = 0; y < 4 - meta.Count; y++)
             {
                 PictureBox pb = new PictureBox();
                 pb.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, 1);
                 flowLayoutPanel1.Controls.Add(pb);
             }
             foreach (Label label in meta)
             {
                 flowLayoutPanel1.Controls.Add(label);
             }*/
        }

        private void disposeAndLoad()
        {
            flowLayoutPanel1.Controls.Clear();
            //flowLayoutPanel.Controls.Clear();
            this.Controls.Remove(flowLayoutPanel);
            flowLayoutPanel1.Size = new Size(1655, 988);
            flowLayoutPanel1.Location = new Point(288, 93);
            /*button4.Location = new Point(1724, -1);
            button3.Location = new Point(1791, -1);
            button2.Location = new Point(1858, -1);

            button2.BackColor = darkBackColor;
            button3.BackColor = darkBackColor;
            button4.BackColor = darkBackColor;
            this.Controls.Add(button4);
            this.Controls.Add(button3);
            this.Controls.Add(button2);*/
            folders.Clear();
            selectedPb = null;
            grpLabels.Clear();

            foreach (PictureBox pb in disposableBoxes)
            {
                try
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
                catch { }
            }
            GC.Collect();
            disposableBoxes.Clear();
            viewMap.Clear();
            textBox3.Focus();
            textBox3.Select();
            vidDetailsStack.Clear();
            this.Explorer_Load(null, null);
        }


        private void Explorer_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void closeForm()
        {
            this.Hide();
            if (calc != null)
            {
                calc.Show();
            }
        }

        private void resetPriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 20; i++)
            {
                if (File.Exists((Calculator.globalDirButton.Text.Contains("Best") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + i + Calculator.globalDirButton.Text + "\\priority.txt"))
                {
                    FileInfo fi = new FileInfo((Calculator.globalDirButton.Text.Contains("Best") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + i + Calculator.globalDirButton.Text + "\\priority.txt");
                    fi.Delete();
                    FileStream fs = File.Create((Calculator.globalDirButton.Text.Contains("Best") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + i + Calculator.globalDirButton.Text + "\\priority.txt");
                    fs.Close();
                    break;
                }
            }
            disposeAndLoad();
        }

        private void Explorer_Enter(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closeForm();
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (Directory.Exists((Calculator.globalDirButton.Text.Contains("Best of the") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + (Calculator.globalDirButton.Text.Contains("Best of the") ? "z" : i.ToString()) + Calculator.globalDirButton.Text))
                {
                    PopUpTextBox popUpTextBox = new PopUpTextBox();
                    popUpTextBox.ShowDialog();
                    if (popUpTextBox.fileName.Length > 0 && !Directory.Exists((Calculator.globalDirButton.Text.Contains("Best of the") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + (Calculator.globalDirButton.Text.Contains("Best of the") ? "z" : i.ToString()) + Calculator.globalDirButton.Text + "\\" + popUpTextBox.fileName))
                    {
                        Directory.CreateDirectory((Calculator.globalDirButton.Text.Contains("Best of the") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + (Calculator.globalDirButton.Text.Contains("Best of the") ? "z" : i.ToString()) + Calculator.globalDirButton.Text + "\\" + popUpTextBox.fileName);
                    }
                    break;
                }
            }
            disposeAndLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        public void uiReload()
        {

            flowLayoutPanel1.Invalidate();
            flowLayoutPanel1.Update();
            flowLayoutPanel1.Refresh();
            Application.DoEvents();
        }


        private void textBox1_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            searchText = textBox3.Text.Trim();
            if ((selectedPb == null && e.KeyCode == Keys.Enter) || (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter))
            {
                textBox3.Text = "";
                textBox3.Text = searchText;
                disposeAndLoad();
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                ctrl = !ctrl;
                divider.BackColor = ctrl == true ? mouseClickColor : kindaDark;
                textBox3.Select();
            }
            else if (e.KeyCode == Keys.Escape || (textBox3.Text.Trim().Length == 0 && e.KeyCode == Keys.Back))
                closeForm();
            else if (ctrl && e.KeyCode == Keys.Up)
                try
                {
                    if (Calculator.globalDirButton != null && isEnlarged[Calculator.globalDirButton.Text])
                    {
                        Calculator.globalDirButton.BackColor = Color.White;
                        isEnlarged[Calculator.globalDirButton.Text] = false;
                        enlargeLeave(Calculator.globalDirButton, Calculator.globalDirButton.Text);
                    }
                    Button tempBut = Calculator.globalDirButton;
                    int index = folderButt.IndexOf(tempBut);
                    tempBut = index == 0 ? folderButt[folderButt.Count - 1] : folderButt[index - 1];
                    Calculator.globalDirButton.BackColor = lightBackColor;
                    Calculator.globalDirButton = tempBut;
                    enlargeEnter(Calculator.globalDirButton);
                    isEnlarged[Calculator.globalDirButton.Text] = true;
                    tempBut.BackColor = mouseClickColor;
                    Calculator.globalFilt = tempBut.Text;
                    disposeAndLoad();
                }
                catch { }

            else if (ctrl && e.KeyCode == Keys.Down)
                try
                {
                    if (Calculator.globalDirButton != null && isEnlarged[Calculator.globalDirButton.Text])
                    {
                        Calculator.globalDirButton.BackColor = Color.White;
                        isEnlarged[Calculator.globalDirButton.Text] = false;
                        enlargeLeave(Calculator.globalDirButton, Calculator.globalDirButton.Text);
                    }
                    Button tempBut = Calculator.globalDirButton;
                    int index = folderButt.IndexOf(tempBut);
                    tempBut = index == folderButt.Count - 1 ? folderButt[0] : folderButt[index + 1];
                    Calculator.globalDirButton.BackColor = lightBackColor;
                    Calculator.globalDirButton = tempBut;
                    enlargeEnter(Calculator.globalDirButton);
                    isEnlarged[Calculator.globalDirButton.Text] = true;
                    tempBut.BackColor = mouseClickColor;
                    Calculator.globalFilt = tempBut.Text;
                    disposeAndLoad();
                }
                catch { }
            else if (e.KeyCode == Keys.PageUp)
                try
                {
                    int scrollPos = flowLayoutPanel1.VerticalScroll.Value - 850 < 0 ? 0 : flowLayoutPanel1.VerticalScroll.Value - 850;
                    flowLayoutPanel1.VerticalScroll.Value = scrollPos;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, scrollPos);
                }
                catch { }

            else if (e.KeyCode == Keys.PageDown) try
                {
                    int scrollPos = flowLayoutPanel1.VerticalScroll.Value + 850;
                    flowLayoutPanel1.VerticalScroll.Value = scrollPos;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, scrollPos);
                }
                catch { }
            else if (ctrl && e.KeyCode == Keys.Left)
                try
                {
                    Button tempBut = Calculator.globalTypeButton;
                    int index = typeButtons.IndexOf(tempBut);
                    tempBut = index == 0 ? typeButtons[typeButtons.Count - 1] : typeButtons[index - 1];
                    if (tempBut.Text.Contains("Short"))
                        typeName.Text = "Gif Vid";
                    else if (tempBut.Text.Contains("4K"))
                        typeName.Text = "4K";
                    else
                        typeName.Text = tempBut.Text;
                    Calculator.globalTypeButton.ForeColor = Color.White;
                    Calculator.globalTypeButton = tempBut;
                    tempBut.ForeColor = mouseClickColor;
                    Calculator.globalType = typeName.Text;
                }
                catch { }

            else if (ctrl && e.KeyCode == Keys.Right)
                try
                {
                    Button tempBut = Calculator.globalTypeButton;
                    int index = typeButtons.IndexOf(tempBut);
                    tempBut = (index == typeButtons.Count - 1) ? typeButtons[0] : typeButtons[index + 1];

                    if (tempBut.Text.Contains("Short"))
                        typeName.Text = "Gif Vid";
                    else if (tempBut.Text.Contains("4K"))
                        typeName.Text = "4K";
                    else
                        typeName.Text = tempBut.Text;

                    Calculator.globalTypeButton.ForeColor = Color.White;
                    Calculator.globalTypeButton = tempBut;
                    tempBut.ForeColor = mouseClickColor;
                    Calculator.globalType = typeName.Text;
                }
                catch { }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Up)
                try
                {
                    int scrollPos = flowLayoutPanel1.VerticalScroll.Value - 80 < 0 ? 0 : flowLayoutPanel1.VerticalScroll.Value - 80;
                    flowLayoutPanel1.VerticalScroll.Value = scrollPos;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, scrollPos);
                }
                catch { }

            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Down) try
                {
                    int scrollPos = flowLayoutPanel1.VerticalScroll.Value + 80;
                    flowLayoutPanel1.VerticalScroll.Value = scrollPos;
                    flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, scrollPos);
                }
                catch { }
            else if (e.KeyCode == Keys.Left)
                try
                {
                    if (selectedPb == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;

                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;
                        return;
                    }
                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                    }
                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) - 1 < 0)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.Count - 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.Count - 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = selectedPbColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) - 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) - 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = selectedPbColor;
                    }
                    int index = wide ? disposableBoxes.IndexOf(selectedPb) / 2 : (disposableBoxes.IndexOf(selectedPb)) / 3;

                    Point controlLoc = new Point(0, 0);
                    int mod = selectedPb.Location.Y + selectedPb.Height;
                    if (mod > 1100 || selectedPb.Location.Y < 0)
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                        controlLoc = this.PointToScreen(selectedPb.Location);
                        flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - (wide ? 165 : 85);
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - (wide ? 165 : 85));
                    }

                    if ((index == 0))
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                    }
                }
                catch { }

            else if (e.KeyCode == Keys.Right) try
                {
                    if (selectedPb == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = mouseHoverColor;
                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        return;
                    }

                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = mouseHoverColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                    }

                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) + 1 >= disposableBoxes.Count)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(0);
                        globalLabel = grpLabels.ElementAt(0);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        globalLabel.ForeColor = selectedPbColor;
                        selectedPb.BackColor = selectedPbColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) + 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) + 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = selectedPbColor;
                    }


                    int index = wide ? disposableBoxes.IndexOf(selectedPb) / 2 : (disposableBoxes.IndexOf(selectedPb)) / 3;

                    Point controlLoc = new Point(0, 0);
                    int mod = selectedPb.Location.Y + selectedPb.Height;
                    if (mod > 1100 || selectedPb.Location.Y < 0)
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                        controlLoc = this.PointToScreen(selectedPb.Location);
                        flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - (wide ? 165 : 85);
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - (wide ? 165 : 85));
                    }

                    if ((index == 0))
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                    }
                }
                catch { }
            else if (e.KeyCode == Keys.Up)
                try
                {
                    if (selectedPb == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = selectedPbColor;
                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;

                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        return;
                    }
                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                    }
                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) - (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)) < 0)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.Count - 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.Count - 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = selectedPbColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) - (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) - (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = selectedPbColor;
                    }

                    int index = wide ? disposableBoxes.IndexOf(selectedPb) / 2 : (disposableBoxes.IndexOf(selectedPb)) / 3;

                    Point controlLoc = new Point(0, 0);
                    int mod = selectedPb.Location.Y + selectedPb.Height;
                    if (mod > 1100 || selectedPb.Location.Y < 0)
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                        controlLoc = this.PointToScreen(selectedPb.Location);
                        flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - (wide ? 165 : 85);
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - (wide ? 165 : 85));
                    }

                    if ((index == 0))
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                    }
                }
                catch { }

            else if (e.KeyCode == Keys.Down) try
                {
                    if (selectedPb == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = selectedPbColor;
                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        return;
                    }

                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                    }

                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) + (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)) >= disposableBoxes.Count)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(0);
                        globalLabel = grpLabels.ElementAt(0);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        globalLabel.ForeColor = selectedPbColor;
                        selectedPb.BackColor = selectedPbColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 7, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) + (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) + (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        myfont = new Font("Comic Sans MS", compact ? 7 : 7, FontStyle.Bold);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = selectedPbColor;
                    }

                    int index = wide ? disposableBoxes.IndexOf(selectedPb) / 2 : (disposableBoxes.IndexOf(selectedPb)) / 3;

                    Point controlLoc = new Point(0, 0);
                    int mod = selectedPb.Location.Y + selectedPb.Height;
                    if (mod > 1100 || selectedPb.Location.Y < 0)
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);
                        controlLoc = this.PointToScreen(selectedPb.Location);
                        flowLayoutPanel1.VerticalScroll.Value = controlLoc.Y - (wide ? 165 : 85);
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, controlLoc.Y - (wide ? 165 : 85));
                    }

                    if ((index == 0))
                    {
                        flowLayoutPanel1.VerticalScroll.Value = 0;
                        flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.AutoScrollPosition.X, 0);

                    }
                }
                catch { }
            else if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.Text.Trim().Length == 0 && selectedPb != null)
                {
                    FileInfo tempDir = new FileInfo(selectedPb.Name);

                    DirectoryInfo directory = new DirectoryInfo(tempDir.DirectoryName);

                    int idx = disposableBoxes.IndexOf(selectedPb);
                    String prevStr = disposableBoxes.ElementAt((idx - 1 < 0) ? (disposableBoxes.Count - 1) : (idx - 1)).Name;
                    String nextStr = disposableBoxes.ElementAt((idx + 1 >= disposableBoxes.Count) ? (0) : (idx + 1)).Name;
                    pbClick(selectedPb.Name, directory, prevStr, nextStr);
                }
                else
                {
                    textBox3.Text = "";
                    textBox3.Text = searchText;

                    disposeAndLoad();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void Explorer_Deactivate(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            compact = !compact;
            if (compact)
            {
                stack.Text = "Un Stack";
            }
            else
            {
                stack.Text = "Stack";
            }
            disposeAndLoad();
        }

        private void Explorer_Activated(object sender, EventArgs e)
        {
            if (VideoPlayer.toRefresh && VideoPlayer.imagePBLink.Length > 0)
            {
                if (selectedPb.Image != null)
                    selectedPb.Image.Dispose();
                selectedPb.Image = Image.FromFile(VideoPlayer.imagePBLink);
            }
            VideoPlayer.toRefresh = false;

            if (VideoPlayer.toChangeTheme)
            {
                setTheme();
            }
            VideoPlayer.toChangeTheme = false;
            textBox3.Focus();
            axWindowsMediaPlayer1.Ctlcontrols.play();
            if(!flowLayoutPanel3.Controls.Contains(axWindowsMediaPlayer1))flowLayoutPanel1.Controls.Add(axWindowsMediaPlayer1);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            File.WriteAllText(pardirectory[1].FullName + "\\ThemeColor.txt",
                                colorDialog1.Color.R + "," + colorDialog1.Color.G + "," + colorDialog1.Color.B);
            setTheme();
        }

        private void calcButton_MouseEnter(object sender, EventArgs e)
        {
            calcButton.ForeColor = mouseClickColor;
        }


        private void button12_Click(object sender, EventArgs e)
        {

            ctrl = !ctrl;
            divider.BackColor = ctrl == true ? mouseClickColor : kindaDark;
            textBox3.Select();
        }

        private void newFolder_Click(object sender, EventArgs e)
        {
            PopUpTextBox popUpTextBox = new PopUpTextBox();
            for (int i = 0; i < 20; i++)
            {
                if (Directory.Exists((Calculator.globalDirButton.Text.Contains("Best of the") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + (Calculator.globalDirButton.Text.Contains("Best of the") ? "z" : i.ToString()) + Calculator.globalDirButton.Text))
                {
                    popUpTextBox.Location = new Point(flowLayoutPanel2.Location.X + newFolder.Location.X + 20, newFolder.Location.Y + newFolder.Size.Height + 5);
                    popUpTextBox.ShowDialog();
                    if (popUpTextBox.fileName.Length > 0 && !popUpTextBox.fileName.Trim().Equals("URL/Folder Name:") && !Directory.Exists((Calculator.globalDirButton.Text.Contains("Best of the") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + (Calculator.globalDirButton.Text.Contains("Best of the") ? "z" : i.ToString()) + Calculator.globalDirButton.Text + "\\" + popUpTextBox.fileName))
                    {
                        Directory.CreateDirectory((Calculator.globalDirButton.Text.Contains("Best of the") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + (Calculator.globalDirButton.Text.Contains("Best of the") ? "z" : i.ToString()) + Calculator.globalDirButton.Text + "\\" + popUpTextBox.fileName);
                    }
                    break;
                }
            }
            if (popUpTextBox.fileName.Length > 0)
                disposeAndLoad();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Random random = new Random();

            randomFilesPreloaded = FilesDashboard(true, false, false);
            tempRandomFilesPreloaded = new List<PictureBox>();
            int theme = 20;
            while (theme > 0)
            {
                int rand = random.Next(randomFilesPreloaded.Count);
                PictureBox tempPb = new PictureBox();
                tempPb.Name = randomFilesPreloaded.ElementAt(rand).Substring(0, randomFilesPreloaded.ElementAt(rand).IndexOf("@@!"));
                tempPb.Image = setDefaultPic(new FileInfo(tempPb.Name), tempPb);
                if (tempPb.Image != null) tempPb.Image.Dispose();
                tempRandomFilesPreloaded.Add(tempPb);
                randomFilesPreloaded.RemoveAt(rand);
                theme--;
            }

            shortFilesPreloaded = FilesDashboard(true, true, false);

            tempShortFilesPreloaded = new List<PictureBox>();
            theme = 30;
            while (theme > 0)
            {
                int rand = random.Next(shortFilesPreloaded.Count);
                PictureBox tempPb = new PictureBox();
                tempPb.Name = shortFilesPreloaded.ElementAt(rand).Substring(0, shortFilesPreloaded.ElementAt(rand).IndexOf("@@!"));
                tempPb.Image = setDefaultPic(new FileInfo(tempPb.Name), tempPb);
                if (tempPb.Image != null) tempPb.Image.Dispose();
                tempShortFilesPreloaded.Add(tempPb);
                shortFilesPreloaded.RemoveAt(rand);
                theme--;
            }


            affinityFilePreloaded = FilesDashboard(true, false, true);

            tempAffinityFilesPreloaded = new List<PictureBox>();
            theme = 30;
            while (theme > 0)
            {
                int rand = random.Next(affinityFilePreloaded.Count);
                PictureBox tempPb = new PictureBox();
                tempPb.Name = affinityFilePreloaded.ElementAt(rand).Substring(0, affinityFilePreloaded.ElementAt(rand).IndexOf("@@!"));
                tempPb.Image = setDefaultPic(new FileInfo(tempPb.Name), tempPb);
                if (tempPb.Image != null) tempPb.Image.Dispose();
                tempAffinityFilesPreloaded.Add(tempPb);
                affinityFilePreloaded.RemoveAt(rand);
                theme--;
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            disposeAndLoad();
        }

        private void axWindowsMediaPlayer1_MouseDownEvent(object sender, _WMPOCXEvents_MouseDownEvent e)
        {
            if (e.nButton == 2)
                return;
            /*if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            else
                axWindowsMediaPlayer1.Ctlcontrols.play();*/
            this.Hide();
            staticExp = this;
            wmp.Location = new Point(0, 28);
            wmp.setRefPb(prevPb, videosPb, null);
            wmp.axWindowsMediaPlayer1.URL = prevPb.Name;
            wmp.axWindowsMediaPlayer1.Name = prevPb.Name;
            wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
            wmp.Show();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            flowLayoutPanel1_MouseEnter(null, null);
        }

        private void menuBtn1_Click(object sender, EventArgs e)
        {
            /*calcButton.Show();
            axWindowsMediaPlayer1.Ctlcontrols.pause();*/
            if(!inDb)
            {
                dashBoard_Click(null, null);
                inDb = true;
            }

            Calculator.globalFilt = "";
            stackedDb = !stackedDb;
            dupeLabel2.Region = null;
            shorts.Region = null;
            playRandom.Region = null;
            //disposeAndLoad();
            if (stackedDb)
                flowLayoutPanel.Controls.Remove(menuBtn2);
            else {
                flowLayoutPanel.Controls.Add(menuBtn2);
                flowLayoutPanel.Controls.SetChildIndex(menuBtn2,0);
            }
            dupeLabel2.Size = stackedDb ? stackedSizeDbBtn : unStackedSizeDbBtn;
            playRandom.Size = stackedDb ? stackedSizeRandBtn : unStackedSizeRandBtn;
            shorts.Size = stackedDb ? stackedSizeRandBtn : unStackedSizeRandBtn;
            affLbl.Size = stackedDb ? stackedSizeRandBtn : unStackedSizeRandBtn;
            flowLayoutPanel.Location = new Point(stackedDb ? 288 : 0, 0);
            flowLayoutPanel1.Location = stackedDb ? flowPanel1Loc : new Point(0, 44);
            flowLayoutPanel.Size = new Size(flowLayoutPanel1.Width, shorts.Height + 2);
            flowLayoutPanel.BringToFront();

            foreach(PictureBox pb in videosPb)
            {
                pb.Parent.Region = null;
                pb.Parent.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, (stackedDb ? stackedSizePbDb.Height : unStackedSizePbDb.Height) + 50);
                pb.Size = stackedDb ? stackedSizePbDb : unStackedSizePbDb;
            }
            foreach(Label lbl in vidDetailsStack)
            {
                lbl.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, 50);
                lbl.Parent.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, lbl.Parent.Width, lbl.Parent.Height, 12, 12));
            }
            foreach(FlowLayoutPanel flowLayoutPanel in flowLayoutPanels)
            {
                flowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width, (stackedDb ? stackedSizePbDb.Height + 73: unStackedSizePbDb.Height + 73));
            }
            axWindowsMediaPlayer1.Size = new Size((stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width) + 74, (int)(((stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width) + 74) * 0.567));
            newProgressBar.Size = new Size((stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width) + 74, 3);
            hoverPointer.Visible = false;
            pointer.Location = new Point(0, dashBoard.Location.Y + ((dashBoard.Size.Height - pointer.Size.Height) / 2) - 3);
            dupeLabel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dupeLabel2.Width, dupeLabel2.Height, 8, 8));
            shorts.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, shorts.Width, shorts.Height, 8, 8));
            playRandom.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, playRandom.Width, playRandom.Height, 8, 8));
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                Label pb = (Label)contextMenuStrip1.SourceControl;
                if (File.Exists(Explorer.directory3.FullName + "\\resumeDb.txt"))
                {
                    String[] resumeFile = File.ReadAllLines(Explorer.directory3.FullName + "\\resumeDb.txt");
                    String fileStr = "";
                    foreach (String str in resumeFile)
                    {
                        if (!str.Contains(pb.Name + "@@!"))
                            fileStr = fileStr + str + "\n";
                    }
                    File.WriteAllText(Explorer.directory3.FullName + "\\resumeDb.txt", fileStr);
                }
            }
            catch { }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (whereAt >= 9.0) whereAt = 1.0;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (whereAt / 9.0) * inWmpDuration;
            newProgressBar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            whereAt++;
        }

        private void menuBtn2_Click(object sender, EventArgs e)
        {
            menuBtn1_Click(null, null);
        }

        private void theme_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            File.WriteAllText(pardirectory[1].FullName + "\\ThemeColor.txt",
                                colorDialog1.Color.R + "," + colorDialog1.Color.G + "," + colorDialog1.Color.B);
            setTheme();
        }

        private void reset_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 20; i++)
            {
                if (File.Exists(Calculator.globalDirButton.Text.Contains("Best of the") ? (pardirectory[0] + "\\z" + Calculator.globalDirButton.Text + "\\priority.txt") : (pardirectory[1].FullName + "\\" + i + Calculator.globalDirButton.Text + "\\priority.txt")))
                {
                    FileInfo fi = new FileInfo(Calculator.globalDirButton.Text.Contains("Best of the") ? (pardirectory[0] + "\\z" + Calculator.globalDirButton.Text + "\\priority.txt") : (pardirectory[1].FullName + "\\" + i + Calculator.globalDirButton.Text + "\\priority.txt"));
                    fi.Delete();
                    FileStream fs = File.Create(Calculator.globalDirButton.Text.Contains("Best of the") ? (pardirectory[0] + "\\z" + Calculator.globalDirButton.Text + "\\priority.txt") : (pardirectory[1].FullName + "\\" + i + Calculator.globalDirButton.Text + "\\priority.txt"));
                    fs.Close();
                    break;
                }
            }
            disposeAndLoad();
        }

        private void videos_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            Calculator.globalTypeButton.ForeColor = Color.White;
            if (b.Text.Contains("Short"))
                typeName.Text = "Gif Vid";
            else if (b.Text.Contains("4K"))
                typeName.Text = "4K";
            else
                typeName.Text = b.Text;

            Calculator.globalType = typeName.Text;
            Calculator.globalTypeButton = b;
            Calculator.globalTypeButton.ForeColor = mouseClickColor;

            textBox3.Focus();
        }

        private void dashBoard_Click(object sender, EventArgs e)
        {
            inDb = true;
            Calculator.globalFilt = "";
            dashBoardRefresh = true;
            if (Calculator.globalDirButton != null && isEnlarged[Calculator.globalDirButton.Text])
            {
                Calculator.globalDirButton.ForeColor = Color.White;
                isEnlarged[Calculator.globalDirButton.Text] = false;
                enlargeLeave(Calculator.globalDirButton, Calculator.globalDirButton.Text);
            }
            Calculator.globalFilt = "";
            foreach (PictureBox pb in videosPb)
            {
                try
                {
                    if (pb.Image != null)
                        pb.Image.Dispose();
                    pb.Dispose();
                }
                catch { }
            }

            videosPb.Clear();
            disposeAndLoad();

            hoverPointer.Visible = false;
            pointer.Location = new Point(0, dashBoard.Location.Y + ((dashBoard.Size.Height - pointer.Size.Height) / 2) - 3);
            calcButton.Show();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                Label pb = (Label)contextMenuStrip1.SourceControl;
                DirectoryInfo subDi = new DirectoryInfo(pb.Name);
                pbClick(subDi.Parent.FullName, subDi, subDi.Parent.FullName, subDi.Parent.FullName);
            }
            catch { }
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            if (prevFlowLayoutPanel != null)
            {
                foreach (Control tempPanel1 in prevFlowLayoutPanel.Parent.Controls)
                {
                    tempPanel1.Margin = new Padding(17, 0, 0, 0);
                }

                newProgressBar.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, 3);
                prevVidDetails.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, 50);
                prevVidDetails.ForeColor = Color.White;
                prevFlowLayoutPanel.Region = null;
                prevFlowLayoutPanel.Size = new Size(stackedDb ? stackedSizePbDb.Width : unStackedSizePbDb.Width, (stackedDb ? stackedSizePbDb.Height : unStackedSizePbDb.Height) + 53);
                prevFlowLayoutPanel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, prevFlowLayoutPanel.Width, prevFlowLayoutPanel.Height, 12, 12));
                prevVidDetails.Padding = new Padding(0, 3, 0, 0);
                prevFlowLayoutPanel.Controls.Clear();
                //prevFlowLayoutPanel.Parent.Size = new Size(flowLayoutPanel1.Width, (stackedDb ? stackedSizePbDb.Height : unStackedSizePbDb.Height) + 69);
                prevFlowLayoutPanel.Controls.Add(prevPb);
                prevFlowLayoutPanel.Controls.Add(prevVidDetails);
                prevFlowLayoutPanel = null;
                isHoveredOverPb = false;
                timer1.Stop();
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            GC.Collect();
        }

        private void calcButton_MouseLeave(object sender, EventArgs e)
        {
            calcButton.ForeColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            closeForm();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            disposeAndLoad();
        }


    }
}