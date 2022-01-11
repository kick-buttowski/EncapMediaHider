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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class PicViewer : Form, IMessageFilter
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

        Bitmap targetBitmap;
        private bool _canDraw, _toCrop = false;
        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        public Pen cropPen;
        public DashStyle cropDashStyle = DashStyle.DashDot;
        FileInfo saveFi = null;

        public String path;
        VideoPlayer f3;
        public static String globalPic;
        double zoomWidth, zoomHeight;
        public PictureBox pbb;
        List<PictureBox> spb;
        Image img = null;
        Boolean mouseDown = false, zoomed = false, singleShown = false;
        private int _xPos;
        private int _yPos;
        int widthh, heightt;
        List<PictureBox> dispPb = new List<PictureBox>(), vidPb = new List<PictureBox>();
        Boolean[] typeImg = null;
        public Boolean IsGif = false;
        public Point originalPoint = new Point(0, 0);
        public Size originalSize = new Size(0,0);


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

        public void fillUpFP1(List<PictureBox> vidPb, params Boolean[] typeImg)
        {
            this.vidPb = vidPb;
            this.typeImg = typeImg;
            flowLayoutPanel1.Controls.Clear();
            int prevX = 0, prevY = 0;
            if (vidPb != null)
            {
                for (int i = 0; i < vidPb.Count; i++)
                {
                    if (vidPb[i].Name.Equals(PicViewer.globalPic))
                    {
                        int noOfSet = typeImg[0] ? 6 : 2;
                        List<int> seq = new List<int>();
                        for (int j = i - noOfSet; j < i; j++)
                            seq.Add(j < 0 ? vidPb.Count + j : j);

                        for (int j = i; j < i + noOfSet + 1; j++)
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
                        {
                            PictureBox smallPb = new PictureBox();
                            String name = vidPb[j < 0 ? (vidPb.Count + j) : j].Name;
                            try { 
                            smallPb.Image = Image.FromFile(name.Contains("\\kkkk\\") ?
                                name.Replace("\\kkkk\\", "\\kkkk\\imgPB\\") : (name.Contains("\\Gifs\\") ?
                                name.Replace("\\Gifs\\", "\\Gifs\\imgPB\\") : name.Replace("\\Pics\\", "\\Pics\\imgPB\\")));
                        }catch { }
                            if (vidPb[j < 0 ? (vidPb.Count + j) : j].Name.Equals(PicViewer.globalPic))
                            {
                                Double wratio = (Double)smallPb.Image.Width / (Double)smallPb.Image.Height;
                                Double lratio = (Double)smallPb.Image.Height / (Double)smallPb.Image.Width;
                                if (wratio > 1)
                                {
                                    smallPb.Size = new Size(152, (int)(152*lratio));
                                    smallPb.Margin = new Padding(10, 4, 0, 0);
                                }
                                else
                                {
                                    smallPb.Size = new Size((int)(235 * wratio), 235);
                                    smallPb.Margin = new Padding(7, 5, 0, 5);
                                }

                                smallPb.MouseEnter += (s, args) =>
                                {
                                    fullZoomOut();
                                    prevX = 0;
                                    prevY = 0;
                                    zoomIn();
                                };

                                smallPb.MouseMove += (s, args) =>
                                {
                                    if (args.X - prevX >= 2 || args.X - prevX <= -2 || args.Y - prevY >= 2 || args.Y - prevY <= -2)
                                    {
                                        zoomScroll(args.X, args.Y, smallPb.Width, smallPb.Height);
                                        prevX = args.X;
                                        prevY = args.Y;
                                    }
                                };

                                smallPb.MouseLeave += (s, args) =>
                                {
                                    fullZoomOut();
                                    prevX = 0;
                                    prevY = 0;
                                    return;
                                };

                            }
                            else
                            {
                                if (singleShown)
                                    continue;
                                Double wratio = (Double)smallPb.Image.Width / (Double)smallPb.Image.Height;
                                Double lratio = (Double)smallPb.Image.Height / (Double)smallPb.Image.Width;
                                if (wratio > 1)
                                {
                                    smallPb.Size = new Size(130, (int)(130 * lratio));
                                    smallPb.Margin = new Padding(9, 12, 0, 0);
                                }
                                else
                                {
                                    smallPb.Size = new Size((int)(186 * wratio), 186);
                                    smallPb.Margin = new Padding(25, 4, 0, 2);
                                }
                            }
                            smallPb.SizeMode = PictureBoxSizeMode.Zoom;
                            smallPb.Name = vidPb[j < 0 ? (vidPb.Count + j) : j].Name;
                            smallPb.MouseClick += (s, args) =>
                            {
                                if (pbb.Image != null)
                                {
                                    pbb.Image.Dispose();
                                    pbb.Dispose();
                                }
                                //picPanel.Controls.Clear();
                                PicViewer.globalPic = smallPb.Name;
                                path = smallPb.Name;
                                setPic();
                                controlDisposer();
                                fillUpFP1(vidPb, typeImg);
                            };

                            smallPb.MouseEnter += (s, args) => {

                                smallPb.Region = null;
                                if (PicViewer.globalPic == smallPb.Name)
                                {
                                    smallPb.Size = new Size((int)(smallPb.Width * 1.05), (int)(smallPb.Height * 1.05));
                                    if (smallPb.Width < smallPb.Height)
                                    {
                                        int left = (flowLayoutPanel1.Width - smallPb.Width) / 2;
                                        smallPb.Margin = new Padding(left, 0, smallPb.Margin.Right, 0);
                                    }
                                    else
                                    {
                                        int top = (flowLayoutPanel1.Height - smallPb.Height) / 2;
                                        smallPb.Margin = new Padding(5, top, 0, 0);
                                    }
                                }
                                else
                                {
                                    smallPb.Size = new Size((int)(smallPb.Width * 1.10), (int)(smallPb.Height * 1.10));
                                    if (smallPb.Width < smallPb.Height)
                                    {
                                        int left = (flowLayoutPanel1.Width - smallPb.Width) / 2;
                                        smallPb.Margin = new Padding(left, 0, smallPb.Margin.Right, 0);
                                    }
                                    else
                                    {
                                        int top = (flowLayoutPanel1.Height - smallPb.Height) / 2;
                                        smallPb.Margin = new Padding(5, top, 0, 0);
                                    }
                                }
                                smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 15, 15));
                            };


                            smallPb.MouseLeave += (s, a) =>
                            {
                                smallPb.Region = null;
                                Double wratio = (Double)smallPb.Image.Width / (Double)smallPb.Image.Height;
                                Double lratio = (Double)smallPb.Image.Height / (Double)smallPb.Image.Width;
                                if (PicViewer.globalPic == smallPb.Name)
                                {
                                    if (smallPb.Width < smallPb.Height)
                                    {
                                        smallPb.Size = new Size((int)(235.0 * (wratio)), 235);
                                        smallPb.Margin = new Padding(7, 5, 0, 5);
                                    }
                                    else
                                    {
                                        smallPb.Size = new Size(152, (int)(152 * lratio));
                                        smallPb.Margin = new Padding(10, 4, 0, 0);

                                    }
                                }
                                else
                                {
                                    if (smallPb.Width < smallPb.Height)
                                    {
                                        smallPb.Size = new Size((int)(186.0 * (wratio)), 186);
                                        smallPb.Margin = new Padding(25, 4, 0, 2);
                                    }
                                    else
                                    {
                                        smallPb.Size = new Size(130, (int)(130.0 * lratio));
                                        smallPb.Margin = new Padding(9, 12, 0, 0);

                                    }
                                }
                                smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 15, 15));
                            };
                            smallPb.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, smallPb.Width, smallPb.Height, 15, 15));
                            dispPb.Add(smallPb);
                            flowLayoutPanel1.Controls.Add(smallPb);
                        }

                    }
                }
            }
        }

        public PicViewer(String path, VideoPlayer f3, List<PictureBox> spb, int width, int height, Boolean IsGif)
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            this.f3 = f3;
            this.spb = spb;
            this.path = path;
            globalPic = path;
            this.FormBorderStyle = FormBorderStyle.None;
            this.IsGif = IsGif;
            crop.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, crop.Width, crop.Height, 6, 6));
            ccBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, crop.Width, crop.Height, 6, 6));
            cBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, crop.Width, crop.Height, 6, 6));
            saveBtn.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, crop.Width, crop.Height, 6, 6));
            preview.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, crop.Width, crop.Height, 6, 6));
            setPic();
        }

        private void closingProcess()
        {
            f3.Show();
            if (pbb != null && pbb.Image != null)
            {
                pbb.Image.Dispose();
                pbb.Dispose();
            }

            controlDisposer();
            Application.RemoveMessageFilter(this);
            this.Dispose();
            this.Close();
            GC.Collect();
        }

        private Image zoomPic(Image img, Double zoom)
        {
            Bitmap bm;
            try
            {
                bm = new Bitmap(img, Convert.ToInt32((int)(pbb.Width * zoom)), Convert.ToInt32((int)(pbb.Height * zoom)));
                Graphics gpu = Graphics.FromImage(bm);
                gpu.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
            catch { return img; }
            GC.Collect();
            return bm;
        }
 

        private void setBestRes(Double Width, Double Height, Double ratio, PictureBox pbb)
        {
            int bestHeight = picPanel.Height;
            int bestWidth = picPanel.Width;
            if (IsGif)
            {
                Width = 2.5 * Width;
                Height = 2.5 * Height;
            }

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

        public void zoomIn()
        {
            pbb.Size = new Size((int)((Double)pbb.Image.Width * 1.20), (int)((Double)pbb.Image.Height * 1.20));
            pbb.Location = new Point(pbb.Width > picPanel.Width ? 0 : (picPanel.Width / 2) - (pbb.Width / 2), 0);
            SetDisplayRectLocation(0, 500);
            AdjustFormScrollbars(true);
            pbb.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }

        public void zoomScroll(Double x, Double y, Double width, Double height)
        {
            
            Double maxScrollY = pbb.Height;
            Double maxScrollX = pbb.Width;
            x = (x * (maxScrollX / width)) - ((Double)picPanel.Width / 2.0);
            y = (y * (maxScrollY / height)) - ((Double)picPanel.Height / 2.0);

            VScrollProperties vs = picPanel.VerticalScroll;
            HScrollProperties hs = picPanel.HorizontalScroll;
            try
            {
                picPanel.VerticalScroll.Value = (int)y < 0 ? vs.Minimum : (int)y;
                picPanel.HorizontalScroll.Value = (int)x < 0 ? hs.Minimum : (int)x;

                picPanel.AutoScrollPosition = new Point((int)x < 0 ? hs.Minimum : (int)x, (int)y < 0 ? vs.Minimum : (int)y);
            }
            catch
            {
                picPanel.VerticalScroll.Value = vs.Maximum - vs.LargeChange + 1;
                picPanel.HorizontalScroll.Value = hs.Maximum - hs.LargeChange + 1;
                picPanel.AutoScrollPosition = new Point(picPanel.HorizontalScroll.Value, picPanel.VerticalScroll.Value);
            }
            zoomed = true;
        }

        public void fullZoomOut()
        {
            Double Width = widthh;
            Double Height = heightt;
            Double ratio = (Double)(Height / Width);
            setBestRes(Width, Height, ratio, pbb);
            zoomed = false;
            pbb.Location = new Point((picPanel.Width / 2) - (pbb.Width / 2),
                      (picPanel.Height / 2) - (pbb.Height / 2));
        }

        public void setPic()
        {
            zoomed = false;
            picPanel.AutoScroll = true;
            pbb = new PictureBox();
            img = Image.FromFile(path);
            this.heightt = img.Height;
            this.widthh = img.Width;
            pbb.Image = img;
            pbb.Name = path;
            pbb.Anchor = AnchorStyles.None;
            pbb.SizeMode = PictureBoxSizeMode.StretchImage;
            globalPic = pbb.Name;
            Double Width = pbb.Image.Width;
            Double Height = pbb.Image.Height;
            Double ratio = (Double)(Height / Width);
            setBestRes(Width, Height, ratio, pbb);

            pbb.Location = new Point((picPanel.Width / 2) - (pbb.Width / 2) + (pbb.Width>pbb.Height?0:50),
                      (picPanel.Height / 2) - (pbb.Height / 2));

            zoomWidth = pbb.Width * 0.25;
            zoomHeight = pbb.Height * 0.25;
            /*
            if (IsGif)
            {
                pbb.Size = new Size((int)(pbb.Width + 2*zoomWidth), (int)(pbb.Height + 2*zoomHeight));

                SetDisplayRectLocation(0, 500);
                AdjustFormScrollbars(true);
                pbb.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                int x, y;

                if (pbb.Width < picPanel.Width) x = (int)((double)(picPanel.Width - pbb.Width) / (double)2);
                else x = -picPanel.HorizontalScroll.Value;

                if (pbb.Height < picPanel.Height) y = (int)((double)(picPanel.Height - pbb.Height) / (double)2);
                else y = -picPanel.VerticalScroll.Value;

                zoomed = true;
                pbb.Location = new Point(x, y);
            }*/

            pbb.MouseDoubleClick += (senderr, argss) =>
            {
                closingProcess();
                return;
            };

            pbb.MouseClick += (senderr, argss) =>
            {
                if (argss.Button == MouseButtons.Right)
                {
                    fullZoomOut();
                    return;
                }
            };

            pbb.Paint += (s, a) =>
            {
                if (_toCrop)
                {
                    using (Pen pen = new Pen(Explorer.mouseClickColor, 2))
                    {
                        pen.DashStyle = DashStyle.DashDot;
                        a.Graphics.DrawRectangle(pen, new Rectangle(_xPos, _yPos, cropWidth, cropHeight));
                    }
                }
            };

            pbb.MouseDown += (senderr, argss) =>
            {
                if (argss.Button == MouseButtons.Right)
                {
                    return;
                }
                if (argss.Button == MouseButtons.Middle)
                {
                    Bitmap bmp = new Bitmap(pbb.Image);
                    for (int i = 0; i < 100; i++)
                    {
                        FileInfo mainDi = new FileInfo(path);
                        if (!File.Exists(mainDi.DirectoryName + "\\" + i + " - " + mainDi.Name))
                        {
                            bmp.Save(mainDi.DirectoryName + "\\" + i + " - " + mainDi.Name);
                            break;
                        }
                    }
                    return;
                }

                _canDraw = true;
                if (zoomed)
                {
                    pbb.Cursor = Cursors.Hand;
                    mouseDown = true;
                }

                _xPos = argss.X;
                _yPos = argss.Y;
            };

            pbb.MouseMove += (s, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    if (_canDraw && _toCrop)
                    {
                        pbb.Refresh();
                        cropWidth = args.X - _xPos;
                        cropHeight = args.Y - _yPos;
                        Double rat = (Double)cropWidth / (Double)cropHeight;
                        Double rRat = (Double)cropHeight / (Double)cropWidth;
                        label1.Text = "X: " + cropWidth + "\nY: " + cropHeight + "\nX/Y: " + Math.Round(rat, 4) + "\nY/X: " + Math.Round(rRat, 4);
                        label1.Refresh();
                        pbb.Refresh();
                    }
                    else
                    {
                        /*pbb.Top = args.Y + pbb.Top - _yPos;
                        pbb.Left = args.X + pbb.Left - _xPos;*/
                        int newX = _xPos - PointToClient(Cursor.Position).X;
                        int newY = _yPos - PointToClient(Cursor.Position).Y;

                        if (newX > picPanel.HorizontalScroll.Minimum)
                        {
                            picPanel.HorizontalScroll.Value = newX < picPanel.HorizontalScroll.Maximum ? newX : picPanel.HorizontalScroll.Maximum;
                        }
                        else
                        {
                            picPanel.HorizontalScroll.Value = picPanel.HorizontalScroll.Minimum;
                        }

                        if (newY > picPanel.VerticalScroll.Minimum)
                        {
                            picPanel.VerticalScroll.Value = newY < picPanel.VerticalScroll.Maximum ? newY : picPanel.VerticalScroll.Maximum;
                        }
                        else
                        {
                            picPanel.VerticalScroll.Value = picPanel.VerticalScroll.Minimum;
                        }
                        picPanel.Update();
                    }
                }
            };

            pbb.MouseUp += (s, args) =>
            {
                if (_toCrop)
                {
                    _canDraw = false;
                    return;
                }
                if (args.Button == MouseButtons.Right)
                {
                    return;
                }
                if (zoomed)
                {
                    mouseDown = false;
                    pbb.Cursor = Cursors.Default;
                }
                else
                {
                    Double wid = pbb.Width, hig = pbb.Height;
                    zoomIn();
                    zoomScroll(args.X, args.Y, wid, hig);
                }
            };

            pbb.MouseWheel += (senderr, argss) =>
            {
                if (argss.Delta > 0)
                {
                    pbb.Size = new Size((int)(pbb.Width + zoomWidth), (int)(pbb.Height + zoomHeight));
                }
                else
                {
                    pbb.Size = new Size((int)(pbb.Width - zoomWidth), (int)(pbb.Height - zoomHeight));
                }

                SetDisplayRectLocation(0, 500);
                AdjustFormScrollbars(true);
                pbb.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                int x, y;

                if (pbb.Width < picPanel.Width) x = (int)((double)(picPanel.Width - pbb.Width) / (double)2);
                else x = -picPanel.HorizontalScroll.Value;

                if (pbb.Height < picPanel.Height) y = (int)((double)(picPanel.Height - pbb.Height) / (double)2);
                else y = -picPanel.VerticalScroll.Value;

                zoomed = true;
                pbb.Location = new Point(x, y);
                editBtnH.BringToFront();
            };

            picPanel.Controls.Add(pbb);
        }

        private void crop_Click(object sender, EventArgs e)
        {
            _toCrop = !_toCrop;
            if (_toCrop)
            {
                if (picCroppedPicture.Image != null)
                    picCroppedPicture.Image.Dispose();
                picCroppedPicture.Visible = false ;
                pbb.Visible = true ;
                crop.BackColor = Explorer.mouseClickColor;
            }
            else
            {
                crop.BackColor = Color.FromArgb(45, 45, 45);
                if (picCroppedPicture.Image != null)
                    picCroppedPicture.Image.Dispose();
                if (pbb.Image != null)
                    pbb.Image.Dispose();
                picCroppedPicture.Visible = false;
                
                Bitmap bmp = new Bitmap(Image.FromFile(path));
                setPic(pbb, bmp);
                pbb.Visible = true;
            }
            if (preview.BackColor == Explorer.mouseClickColor)
            {
                preview.BackColor = Color.FromArgb(45, 45, 45);
            }
            label1.Text = "X:\nY:\nX / Y:\nY / X:";
        }

        public void setPic(PictureBox pb, Bitmap img)
        {
            pb.Image = img;
            pb.Size = img.Size;
            pb.Anchor = AnchorStyles.None;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            Double Width = pb.Image.Width;
            Double Height = pb.Image.Height;

            if (Width * 1.25 >= 1600 || Height * 1.25 >= 1000)
            {

            }
            else
            {
                Width = Width * 1.25;
                Height = Height * 1.25;
            }
            Double ratio = (Double)(Height / Width);
            setBestRes(Width, Height, ratio, pb);
            pb.Location = new Point((this.Width / 2) - (pb.Width / 2) + (pb.Width > pb.Height ? 0 : 40),
                      (this.Height / 2) - (pb.Height / 2));
        }

        private void preview_Click(object sender, EventArgs e)
        {
            if (!_toCrop)
                return;
            if (preview.BackColor == Explorer.mouseClickColor)
            {
                preview.BackColor = Color.FromArgb(45, 45, 45);
                if (picCroppedPicture.Image != null)
                    picCroppedPicture.Image.Dispose();
                picCroppedPicture.Visible = false;
                pbb.Visible = true;
            }
            else
            {
                if (cropWidth < 1 || cropHeight < 1)
                {
                    return;
                }
                preview.BackColor = Explorer.mouseClickColor;
                //                Rectangle rect = new Rectangle(cropX+28, cropY+73, cropWidth+57, cropHeight+20);
                Rectangle rect = new Rectangle(_xPos, _yPos, cropWidth, cropHeight);
                Bitmap OriginalImage = new Bitmap(pbb.Image, pbb.Width, pbb.Height);

                targetBitmap = new Bitmap(cropWidth, cropHeight);

                Graphics g = Graphics.FromImage(targetBitmap);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
                /* Bitmap bp = new Bitmap(picOriginalPicture.Image);
                 targetBitmap = bp.Clone(rect, bp.PixelFormat);*/

                if (picCroppedPicture.Image != null)
                    picCroppedPicture.Image.Dispose();

                setPic(picCroppedPicture, targetBitmap);
                picCroppedPicture.Visible = true;
                pbb.Visible = false;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            VideoPlayer.isCropped = true;
            if (targetBitmap == null)
                return;
            saveFi = new FileInfo(path);
            String ext = saveFi.Extension;
            for (int i = 0; i < 100; i++)
            {
                if (File.Exists(saveFi.DirectoryName + "\\_cropped" + i + saveFi.Name.Substring(saveFi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1)))
                    continue;
                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(saveFi.DirectoryName + "\\_cropped" + i + saveFi.Name.Substring(saveFi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1), FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        byte[] bytes;
                        switch (ext)
                        {
                            case ".png":
                                targetBitmap.Save(memory, ImageFormat.Png);
                                bytes = memory.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                break;
                            case ".jpg":
                            case ".jpeg":
                            case ".jpe":
                            case ".jfif":
                            case ".exif":
                                targetBitmap.Save(memory, ImageFormat.Jpeg);
                                bytes = memory.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                break;
                            case ".gif":
                                targetBitmap.Save(memory, ImageFormat.Gif);
                                bytes = memory.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                break;
                            case ".bmp":
                            case ".dib":
                            case ".rle":
                                targetBitmap.Save(memory, ImageFormat.Bmp);
                                bytes = memory.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                break;
                            case ".tiff":
                            case ".tif":
                                targetBitmap.Save(memory, ImageFormat.Tiff);
                                bytes = memory.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                break;
                        }
                    }
                }
                break;
                /*if (File.Exists(saveFi.DirectoryName + "\\_cropped" + i + saveFi.Name.Substring(saveFi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1)))
                    continue;
                targetBitmap.Save(saveFi.DirectoryName + "\\_cropped" + i + saveFi.Name.Substring(saveFi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1), ImageFormat.Png);
                break;*/

                /*ImageCodecInfo myImageCodecInfo;
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                myEncoderParameters = new EncoderParameters(1);
                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;

                targetBitmap.Save(saveFi.DirectoryName + "\\_cropped" + i + saveFi.Name.Substring(saveFi.Name.IndexOf("placeholdderr") + "placeholdderr".Length + 1), myImageCodecInfo, myEncoderParameters);*/

            }
        }

        private void cBtn_Click(object sender, EventArgs e)
        {
            targetBitmap = new Bitmap(pbb.Image);
            targetBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pbb.Image.Dispose();
            setPic(pbb, targetBitmap);
        }

        private void ccBtn_Click(object sender, EventArgs e)
        {
            targetBitmap = new Bitmap(pbb.Image);
            targetBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pbb.Image.Dispose();
            setPic(pbb, targetBitmap);
        }

        private void editBtnH_Click(object sender, EventArgs e)
        {
            editPanel.Visible = !editPanel.Visible;
            label1.Visible = !label1.Visible;
        }

        private void picPanel_MouseClick(object sender, MouseEventArgs e)
        {
            closingProcess();
        }

        private void hideBtnH_Click(object sender, EventArgs e)
        {
            if (singleShown)
            {
                flowLayoutPanel1.Region = null;
                flowLayoutPanel1.Location = originalPoint;
                flowLayoutPanel1.Size = originalSize;
            }
            else
            {
                flowLayoutPanel1.Region = null;
                    if(originalSize.Width > originalSize.Height)
                    {
                    flowLayoutPanel1.Size = new Size(170,originalSize.Height);
                        flowLayoutPanel1.Location = new Point(887, originalPoint.Y);
                    }
                    else
                {
                    flowLayoutPanel1.Size = new Size(originalSize.Width, 259);
                    flowLayoutPanel1.Location = new Point(originalPoint.X, 407);
                    }
            }
            path = globalPic;
            singleShown = !singleShown;
            controlDisposer();
            fillUpFP1(vidPb, typeImg);
            flowLayoutPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height, 15, 15));
        }

        private void PicViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            closingProcess();
        }

        private const UInt32 WM_KEYDOWN = 0x0100;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;

                if (keyCode == Keys.Left)
                {
                    for (int i = 0; i < spb.Count; i++)
                    {
                        if (spb[i].Name.Equals(path))
                        {
                            path = spb[i - 1 < 0 ? spb.Count - 1 : i - 1].Name;
                            if (pbb.Image != null)
                            {
                                pbb.Image.Dispose();
                                pbb.Dispose();
                                //picPanel.Controls.Clear();
                                setPic();
                                controlDisposer();
                                fillUpFP1(vidPb, typeImg);
                            }
                            return true;
                        }
                    }
                }
                else if (keyCode == Keys.Right)
                {
                    for (int i = 0; i < spb.Count; i++)
                    {
                        if (spb[i].Name.Equals(path))
                        {
                            path = spb[i + 1 == spb.Count ? 0 : i + 1].Name;
                            if (pbb.Image != null)
                            {
                                pbb.Image.Dispose();
                                pbb.Dispose();
                                //picPanel.Controls.Clear();
                                setPic();
                                controlDisposer();
                                fillUpFP1(vidPb, typeImg);
                            }
                            return true;
                        }
                    }
                }
                else if (keyCode == Keys.Add && img != null)
                {
                        pbb.Size = new Size((int)(pbb.Width + zoomWidth), (int)(pbb.Height + zoomHeight));

                    pbb.Location = new Point((picPanel.Width / 2) - (pbb.Width / 2), 0);
                    SetDisplayRectLocation(0, 500);
                    AdjustFormScrollbars(true);
                    pbb.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                    zoomed = true;
                }
                else if (keyCode == Keys.Subtract && img != null)
                {
                        pbb.Size = new Size((int)(pbb.Width - zoomWidth), (int)(pbb.Height - zoomHeight));

                    pbb.Location = new Point((picPanel.Width / 2) - (pbb.Width / 2), 0);
                    SetDisplayRectLocation(0, 500);
                    AdjustFormScrollbars(true);
                    pbb.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                    zoomed = true;
                }
                else if (keyCode == Keys.Back || keyCode == Keys.Escape)
                {

                    closingProcess();
                }
                return true;
            }

            return false;
        }

    }
}
