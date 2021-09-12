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

namespace MediaPlayer
{
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
        public String searchText = "";

        CustomToolTip tip = null;
        public static DirectoryInfo[] pardirectory = new DirectoryInfo[2];
        List<Button> folderButt = new List<Button>(), typeButtons = new List<Button>();
        Label countFiles = new Label();
        Calculator calc;
        public Button butt1 = new Button(), globalTypeButton = null;
        public List<String> typeList = new List<String>();
        public static int globalVol = 15;
        public static List<String> dirs = new List<string>();
        public static List<String> delFiles = new List<String>();
        Dictionary<String, int> namePriorityPairs = new Dictionary<string, int>();
        Dictionary<String, List<String>> viewMap = new Dictionary<string, List<string>>();
        List<PictureBox> disposableBoxes = new List<PictureBox>();
        Boolean useAndThrow = true;
        Button typeName = new Button();
        public static PictureBox selectedPb = null;
        List<Label> grpLabels = new List<Label>();
        Label globalLabel = null;
        Boolean ctrl = false;
        List<String> folders = new List<string>();
        Boolean compact = true;
        public static WmpOnTop wmpOnTop = null;
        //PopupVideosPlayer popupVideosPlayer;
        //public static Random rr = new Random();
        //static int rand1 = Explorer.rr.Next(0, 256);
        //static int[] color = Rand_Color(rand1, 0.5, 0.25);
        public static int popUpY = 230;
        public static Color globColor = Color.FromArgb(255, 128, 0);

        Color darkBackColor = Color.FromArgb(0, 0, 0);
        Color lightBackColor = Color.FromArgb(31, 31, 31);
        Color mouseHoverColor = Color.FromArgb(255, 128, 0);
        Color mouseClickColor = Color.FromArgb(255, 128, 0);
        Color selectedPbColor = Color.FromArgb(255, 128, 0);
        Dictionary<String, Image> miniImages = new Dictionary<String, Image>(), largeImages = new Dictionary<string, Image>();

        ComponentResourceManager resources = null;


        private void enlargeEnter(Button b)
        {
            //if (largeImages.Count == 0 || isEnlarged[b.Text])
              //  return;
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
            //if (miniImages.Count == 0 || isEnlarged[b.Text])
              //  return;
            try
            {
                b.Image = miniImages[typee];
                Padding p = b.Margin;
                b.Region = null;
                b.Margin = new Padding(p.Left + 20, p.Top, p.Right, p.Bottom);
                b.Size = new Size(b.Width - 40, b.Height -20);
                b.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 25, 25));
                //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top + 10, label1.Margin.Right, label1.Margin.Bottom);
            }
            catch { }
        }

        public Explorer(Calculator calc)
        {
            this.calc = calc;
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            calcButton.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, calcButton.Width, calcButton.Height, 20, 20));
            resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayer));
            button11.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button11.Width, button11.Height, 15,15));
            button5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button5.Width, button5.Height, 5, 5));
            textBox3.Font = new Font("Arial", 13, FontStyle.Regular);
            flowLayoutPanel1.AutoScroll = true;
            textBox3.Select();
            this.DoubleBuffered = true;

            DirectoryInfo directory1 = new DirectoryInfo("E:\\VS Code\\Win Forms\\bin");
            DirectoryInfo directory2 = new DirectoryInfo("E:\\VS Code\\Win Forms\\build ");
            pardirectory[1] = directory1;
            pardirectory[0] = directory2;
            if (!File.Exists(directory1.FullName + "\\ThemeColor.txt"))
                File.Create(directory1.FullName + "\\ThemeColor.txt");
            foreach (DirectoryInfo directory in pardirectory)
                foreach (DirectoryInfo di in directory.GetDirectories())
                {
                    if (!File.Exists(di.FullName + "\\disPic.txt"))
                        File.Create(di.FullName + "\\disPic.txt");
                }
            typeList.Add("Gifs");
            typeList.Add("Gif Vid");
            typeList.Add("4K");
            typeList.Add("Pictures");
            typeList.Add("Videos");
            setTheme();

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
                    //label1.Margin = new Padding(label1.Margin.Left, label1.Margin.Top + 10, label1.Margin.Right, label1.Margin.Bottom);
                }
                catch { }
            };
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


        private void loadMiniImages()
        {
            foreach (DirectoryInfo directory in pardirectory)
                foreach (DirectoryInfo di in directory.GetDirectories())
                {
                    if(File.Exists(pardirectory[1].FullName + "\\" + di.Name.Substring(1) + ".jpg"))
                    {
                        Image img = Image.FromFile(pardirectory[1].FullName + "\\" + di.Name.Substring(1) + ".jpg");
                        miniImages.Add(di.Name.Substring(1), resizedImage(img, 0, 0, 0, 107));
                        largeImages.Add(di.Name.Substring(1), resizedImage(img, 0, 0, 0, 127));
                        img.Dispose();
                    }    

                }

            if(File.Exists(pardirectory[1].FullName + "\\Stack.jpg"))
            {

                Image img = Image.FromFile(pardirectory[1].FullName + "\\Stack.jpg");
                miniImages.Add("Calc", resizedImage(img, 0, 0, 0, 126));
                largeImages.Add("Calc", resizedImage(img, 0, 0, 0, 142));
                calcButton.Image = miniImages["Calc"];

                Image img1 = resizedImage(img, 0, 0, 0, popUpY);
                calcButton.Tag = img1;
                int yLoc = calcButton.Location.Y + (calcButton.Height / 2) - (popUpY / 2);
                if (1080 - calcButton.Location.Y <= (popUpY/2))
                {
                    yLoc = yLoc - (popUpY / 2) + 10;
                }
                else if (calcButton.Location.Y - (popUpY/2) < 0)
                {
                    tip = new CustomToolTip(img1.Width, popUpY, calcButton.Location.X + calcButton.Size.Width + 25, calcButton.Location.Y);
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
            String[] defColor = File.ReadAllText(pardirectory[1].FullName + "\\ThemeColor.txt").Split(',');
            if (defColor.Length == 3)
            {
                mouseHoverColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                mouseClickColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                selectedPbColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
                globColor = Color.FromArgb(int.Parse(defColor[0].Trim()), int.Parse(defColor[1].Trim()), int.Parse(defColor[2].Trim()));
            }
            this.BackColor = Color.FromArgb(59, 59, 59);
            flowLayoutPanel1.BackColor = darkBackColor;
            flowLayoutPanel3.BackColor = darkBackColor;
            flowLayoutPanel4.BackColor = darkBackColor;
            calcButton.BackColor = lightBackColor;
            calcButton.ForeColor = Color.White;
            calcButton.FlatAppearance.MouseOverBackColor = mouseClickColor;
            searchLabel.BackColor = darkBackColor;
            searchLabel.ForeColor = Color.White;
            typelbl.BackColor = darkBackColor;
            typelbl.ForeColor = Color.White;
            button11.BackColor = lightBackColor;
            button11.ForeColor = Color.White;
            textBox3.BackColor = lightBackColor;
            textBox3.ForeColor = Color.White;
            button2.BackColor = lightBackColor;
            button3.BackColor = lightBackColor;
            button4.BackColor = lightBackColor;
            button11.BackColor = lightBackColor;
            button5.BackColor = lightBackColor;
            button11.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button11.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button5.FlatAppearance.MouseOverBackColor = mouseClickColor;
            button5.FlatAppearance.MouseDownBackColor = mouseClickColor;
            button5.BackColor = mouseClickColor;
            foreach (Button  b in folderButt)
            {

                b.ForeColor = Color.White;
                b.BackColor = lightBackColor;
                b.FlatAppearance.MouseOverBackColor = mouseHoverColor;
                b.FlatAppearance.MouseDownBackColor = mouseClickColor;
            }

            foreach (Button b in typeButtons)
            {

                b.ForeColor = Color.White;
                b.BackColor = lightBackColor;
                b.FlatAppearance.MouseOverBackColor = mouseHoverColor;
                b.FlatAppearance.MouseDownBackColor = mouseClickColor;
            }


            if (Calculator.globalDirButton != null)
                Calculator.globalDirButton.BackColor = mouseClickColor;
            if (Calculator.globalTypeButton != null)
                Calculator.globalTypeButton.BackColor = mouseClickColor;

            if(globalLabel!=null) globalLabel.ForeColor = mouseHoverColor;
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
            double r=0, g=0, b=0;
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
            butt2.Margin = new Padding(32, 4, 0, 0);
            butt2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, butt2.Width, butt2.Height, 15, 15));
            butt2.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            butt2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            if (butt2.Text.Contains(Calculator.globalFilt))
            {
                butt2.BackColor = mouseClickColor;
                Calculator.globalDirButton = butt2;
            }
            else
            {
                butt2.BackColor = lightBackColor;
            }

            butt2.FlatAppearance.BorderSize = 0;
            butt2.FlatAppearance.MouseOverBackColor = mouseHoverColor;
            butt2.FlatAppearance.MouseDownBackColor = mouseClickColor;
            butt2.MouseClick += (s, a) =>
            {
                Calculator.globalDirButton.BackColor = lightBackColor;
                Calculator.globalDirButton = butt2;
                butt2.BackColor = mouseClickColor;
                Calculator.globalFilt = butt2.Text;
                disposeAndLoad();
            };
            butt2.MouseEnter += (s, a) =>
            {
                enlargeEnter(butt2);
            };

            butt2.MouseLeave += (s, a) =>
            {
                enlargeLeave(butt2, butt2.Text);
            };

            try
            {
                butt2.Image = miniImages[butt2.Text];
            }
            catch { }


            folderButt.Add(butt2);
            flowLayoutPanel3.Controls.Add(butt2);

            if (File.Exists(pardirectory[1].FullName + "\\" + butt2.Text + ".jpg"))
            {
                Image img = Image.FromFile(pardirectory[1].FullName + "\\" + butt2.Text + ".jpg");
                Image img1 = resizedImage(img, 0, 0, 0, popUpY);
                butt2.Tag = img1;
                int yLoc = butt2.Location.Y + (butt2.Height/2) - (popUpY / 2);
                if (1080 - butt2.Location.Y <= popUpY / 2)
                {
                    yLoc = yLoc - (popUpY / 2) + 10;
                }
                else if (butt2.Location.Y - (popUpY / 2) < 0)
                {
                    tip = new CustomToolTip(img1.Width, popUpY, butt2.Location.X + butt2.Size.Width + 25, butt2.Location.Y + (butt2.Height/2));
                }
                else
                {
                    if (img.Width > img.Height)
                        tip = new CustomToolTip(img1.Width, popUpY, butt2.Location.X + butt2.Size.Width + 25, yLoc);
                    else
                        tip = new CustomToolTip(300, 450, 10, yLoc);
                }

                tip.SetToolTip(butt2, butt2.Text);
                img.Dispose();
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
                StreamReader sr = new StreamReader(subDi.FullName + "\\priority.txt");

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
                File.WriteAllText(subDi.FullName + "\\priority.txt", priorStr);
            }
            catch { }

            this.Hide();
            VideoPlayer v = new VideoPlayer(Key, this, Calculator.globalType, prevFi, nextFi, folders);
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

        public void Explorer_Load(object sender, EventArgs e)
        {
            GC.Collect();

            namePriorityPairs.Clear();
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
                            Calculator.globalFilt = di.Name.Substring(1);
                        }

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

                        foreach (KeyValuePair<String, int> keyValuePair in namePriorityPairs)
                        {
                            if (searchText.Length > 0)
                                if (!keyValuePair.Key.Replace(subDi.FullName, "").ToLower().Contains(searchText.ToLower()))
                                {
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

                            PictureBox dirPb = new PictureBox();
                            dirPb.Margin = new Padding(0, 11, 0, 0);
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
                                    dirPb.Margin = new Padding(25, 0, 0, 13);
                                    vidDetails.Size = new Size(180, 450);
                                    vidDetails.Padding = new Padding(10, 0, 0, 0);
                                    vidDetails.Margin = new Padding(0, 20, 35, 0);
                                    vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                                }
                            }
                            else
                            {
                                if (compact)
                                {
                                    dirPb.Size = new Size(380, 246);
                                    dirPb.Margin = new Padding(12, 0, 0, 9);
                                    vidDetails.Size = new Size(140, 246);
                                    vidDetails.Padding = new Padding(0, 0, 0, 0);
                                    vidDetails.Margin = new Padding(7, 18, 0, 0);
                                    vidDetails.Font = new Font("Consolas", 7, FontStyle.Regular);
                                }
                                else
                                {
                                    dirPb.Size = new Size(505, 327);
                                    dirPb.Margin = new Padding(50, 0, 0, 8);
                                    vidDetails.Size = new Size(180, 327);
                                    vidDetails.Padding = new Padding(10, 0, 0, 0);
                                    vidDetails.Margin = new Padding(15, 5, 50, 0);
                                    vidDetails.Font = new Font("Consolas", 9, FontStyle.Regular);
                                }
                            }

                            dirPb.SizeMode = PictureBoxSizeMode.Zoom;
                            dirPb.Cursor = Cursors.Hand;
                            dirPb.ContextMenuStrip = contextMenuStrip1;
                            dirPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dirPb.Width, dirPb.Height, 70, 70));

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

                                Bitmap bmp = new Bitmap(image);
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
                            try { flowLayoutPanel1.Controls.Add(dirPb); } catch { }

                            vidDetails.Text = vidDetText;
                            vidDetails.BackColor = darkBackColor;
                            vidDetails.ForeColor = Color.White;
                            vidDetails.TextAlign = ContentAlignment.TopLeft;
                            vidDetails.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, vidDetails.Width, vidDetails.Height, 20, 20));

                            flowLayoutPanel1.Controls.Add(vidDetails);
                            grpLabels.Add(vidDetails);

                            disposableBoxes.Add(dirPb);

                            dirPb.MouseClick += (s, args) =>
                            {
                                if (selectedPb == null)
                                {
                                    globalLabel = vidDetails;
                                    globalLabel.ForeColor = mouseHoverColor;
                                    selectedPb = dirPb;
                                    selectedPb.BackColor = selectedPbColor;
                                    Font myfont1 = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                                    globalLabel.Font = myfont1;
                                    return;
                                }
                                if (globalLabel == null)
                                {
                                    globalLabel = vidDetails;
                                    globalLabel.ForeColor = mouseHoverColor;

                                    Font myfont1 = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                                    globalLabel.Font = myfont1;
                                }

                                selectedPb.BackColor = lightBackColor;
                                Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;


                                Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                                globalLabel.Font = myfont;
                                globalLabel.ForeColor = Color.White;
                                selectedPb = dirPb;
                                globalLabel = vidDetails;

                                myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                                globalLabel.Font = myfont;
                                selectedPb.BackColor = selectedPbColor;
                                globalLabel.ForeColor = mouseHoverColor;

                            };

                            dirPb.MouseDoubleClick += (s, args) =>
                            {
                                int idx = disposableBoxes.IndexOf(dirPb);
                                String prevStr = disposableBoxes.ElementAt((idx - 1 < 0) ? (disposableBoxes.Count - 1) : (idx - 1)).Name;
                                String nextStr = disposableBoxes.ElementAt((idx + 1 >= disposableBoxes.Count) ? (0) : (idx + 1)).Name;
                                pbClick(keyValuePair.Key, subDi, prevStr, nextStr);
                            };
                        }
                    }
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
                flowLayoutPanel4.Controls.Remove(typelbl);
                foreach (String type in typeList)
                {
                    Button butt = new Button();
                    butt.Text = type;
                    butt.Font = new Font("Consolas", 10, FontStyle.Bold);
                    butt.Cursor = System.Windows.Forms.Cursors.Hand;
                    butt.ForeColor = Color.White;
                    butt.BackColor = lightBackColor;
                    butt.Size = new Size(150, 45);
                    butt.Margin = new Padding(0, 0, 3, 0);
                    butt.FlatStyle = FlatStyle.Flat;
                    butt.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, butt.Width, butt.Height, 5, 5));
                    butt.Image = ((System.Drawing.Image)(resources.GetObject("fourKBtn.Image")));
                    butt.ImageAlign = ContentAlignment.MiddleLeft;
                    if (butt.Text.Equals(Calculator.globalType))
                    {
                        Calculator.globalTypeButton = butt;
                        butt.BackColor = mouseClickColor;
                    }
                    else
                    {
                        butt.BackColor = lightBackColor;
                    }
                    butt.FlatAppearance.BorderSize = 0;
                    butt.FlatAppearance.MouseOverBackColor = mouseHoverColor;
                    butt.FlatAppearance.MouseDownBackColor = mouseClickColor;
                    butt.MouseClick += (s, a) =>
                    {
                        this.butt1.Text = butt.Text;
                        Calculator.globalTypeButton.BackColor = lightBackColor;
                        Calculator.globalType = butt.Text;
                        if (Calculator.globalType.Contains("Short"))
                            typeName.Text = "Gif vid";
                        else
                            typeName.Text = Calculator.globalType;
                        butt.BackColor = mouseClickColor;
                        Calculator.globalTypeButton = butt;
                        textBox3.Focus();
                        textBox3.Select();
                    };
                    typeButtons.Add(butt);
                    flowLayoutPanel4.Controls.Add(butt);
                }
                useAndThrow = false;
                flowLayoutPanel4.Controls.Add(typelbl);
                //flowLayoutPanel4.Controls.Add(textBox3);
                //flowLayoutPanel4.Controls.Add(searchLabel);
            }
        }

        private void disposeAndLoad()
        {
            flowLayoutPanel1.Controls.Clear();
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
            this.Explorer_Load(null, null);
        }


        private void Explorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close")) { }
            else
            {
                Application.Exit();
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
                if (Directory.Exists((Calculator.globalDirButton.Text.Contains("Best") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + i + Calculator.globalDirButton.Text))
                {
                    PopUpTextBox popUpTextBox = new PopUpTextBox();
                    popUpTextBox.ShowDialog();
                    if (popUpTextBox.fileName.Length > 0 && !Directory.Exists((Calculator.globalDirButton.Text.Contains("Best") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + i + Calculator.globalDirButton.Text + "\\" + popUpTextBox.fileName))
                    {
                        Directory.CreateDirectory((Calculator.globalDirButton.Text.Contains("Best") ? pardirectory[0] : pardirectory[1]).FullName + "\\" + i + Calculator.globalDirButton.Text + "\\" + popUpTextBox.fileName);
                    }
                    break;
                }
            }
            disposeAndLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctrl = !ctrl;
            button11.Text = "Taskbar Control " + (ctrl == true ? "On" : "Off");
            button11.BackColor = (ctrl == true ? mouseClickColor : lightBackColor);
            textBox3.Select();
            // Update the text box color if the user clicks OK 
            /*if (colorDialog1.ShowDialog() == DialogResult.OK)
                mouseClickColor = colorDialog1.Color;


            disposeAndLoad();*/
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
                button11.Text = "Taskbar Control " + (ctrl == true ? "On" : "Off");
                button11.BackColor = (ctrl == true ? mouseClickColor : lightBackColor);
                textBox3.Select();
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Enter)
            {
                textBox3.Text = "";
                textBox3.Text = searchText;
                disposeAndLoad();
            }
            else if (e.KeyCode == Keys.Escape || (textBox3.Text.Trim().Length == 0 && e.KeyCode == Keys.Back))
                closeForm();
            else if (ctrl && e.KeyCode == Keys.Left)
                try
                {
                    Button tempBut = Calculator.globalDirButton;
                    int index = folderButt.IndexOf(tempBut);
                    tempBut = index == 0 ? folderButt[folderButt.Count - 1] : folderButt[index - 1];
                    Calculator.globalDirButton.BackColor = lightBackColor;
                    Calculator.globalDirButton = tempBut;
                    tempBut.BackColor = mouseClickColor;
                    Calculator.globalFilt = tempBut.Text;
                    disposeAndLoad();
                }
                catch { }

            else if (ctrl && e.KeyCode == Keys.Right)
                try
                {
                    Button tempBut = Calculator.globalDirButton;
                    int index = folderButt.IndexOf(tempBut);
                    tempBut = index == folderButt.Count - 1 ? folderButt[0] : folderButt[index + 1];
                    Calculator.globalDirButton.BackColor = lightBackColor;
                    Calculator.globalDirButton = tempBut;
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
            else if (ctrl && e.KeyCode == Keys.Up)
                try
                {
                    Button tempBut = Calculator.globalTypeButton;
                    int index = typeButtons.IndexOf(tempBut);
                    tempBut = index == 0 ? typeButtons[typeButtons.Count - 1] : typeButtons[index - 1];
                    Calculator.globalTypeButton.BackColor = lightBackColor;
                    Calculator.globalTypeButton = tempBut;
                    tempBut.BackColor = mouseClickColor;
                    Calculator.globalType = tempBut.Text;
                }
                catch { }

            else if (ctrl && e.KeyCode == Keys.Down)
                try
                {
                    Button tempBut = Calculator.globalTypeButton;
                    int index = typeButtons.IndexOf(tempBut);
                    tempBut = index == typeButtons.Count - 1 ? typeButtons[0] : typeButtons[index + 1];
                    Calculator.globalTypeButton.BackColor = lightBackColor;
                    Calculator.globalTypeButton = tempBut;
                    tempBut.BackColor = mouseClickColor;
                    Calculator.globalType = tempBut.Text;
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
                        globalLabel.ForeColor = mouseHoverColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;

                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;
                        return;
                    }
                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = mouseHoverColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                    }
                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) - 1 < 0)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.Count - 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.Count - 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = mouseHoverColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) - 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) - 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = mouseHoverColor;
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
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        return;
                    }

                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = mouseHoverColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                    }

                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) + 1 >= disposableBoxes.Count)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(0);
                        globalLabel = grpLabels.ElementAt(0);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        globalLabel.ForeColor = mouseHoverColor;
                        selectedPb.BackColor = selectedPbColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) + 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) + 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = mouseHoverColor;
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
                        globalLabel.ForeColor = mouseHoverColor;
                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;

                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        return;
                    }
                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = mouseHoverColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                    }
                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) - (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)) < 0)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.Count - 1);
                        globalLabel = grpLabels.ElementAt(grpLabels.Count - 1);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = mouseHoverColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) - (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) - (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = mouseHoverColor;
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
                        globalLabel.ForeColor = mouseHoverColor;
                        selectedPb = disposableBoxes.ElementAt(0);
                        selectedPb.BackColor = selectedPbColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        return;
                    }

                    if (globalLabel == null)
                    {
                        globalLabel = grpLabels.ElementAt(0);
                        globalLabel.ForeColor = mouseHoverColor;
                        Font myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                    }

                    selectedPb.BackColor = lightBackColor;
                    Boolean wide = selectedPb.Width > selectedPb.Height ? true : false;
                    if (disposableBoxes.IndexOf(selectedPb) + (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)) >= disposableBoxes.Count)
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(0);
                        globalLabel = grpLabels.ElementAt(0);
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        globalLabel.ForeColor = mouseHoverColor;
                        selectedPb.BackColor = selectedPbColor;
                    }
                    else
                    {
                        globalLabel.ForeColor = Color.White;
                        Font myfont = new Font("Consolas", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb = disposableBoxes.ElementAt(disposableBoxes.IndexOf(selectedPb) + (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        globalLabel = grpLabels.ElementAt(grpLabels.IndexOf(globalLabel) + (compact ? (wide ? 3 : 4) : (wide ? 2 : 3)));
                        myfont = new Font("Comic Sans MS", compact ? 7 : 9, FontStyle.Regular);
                        globalLabel.Font = myfont;
                        selectedPb.BackColor = selectedPbColor;
                        globalLabel.ForeColor = mouseHoverColor;
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

            Application.Exit();
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
                button5.BackColor = mouseClickColor;
            }
            else
            {
                button5.BackColor = lightBackColor;
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