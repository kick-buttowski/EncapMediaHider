﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class PicViewer : Form, IMessageFilter
    {
        public String path;
        VideoPlayer f3;
        public static String globalPic;
        double zoomWidth, zoomHeight;
        public PictureBox pbb;
        List<PictureBox> spb;
        Image img = null;
        Boolean mouseDown = false, zoomed = false;
        private int _xPos;
        private int _yPos;
        int widthh, heightt;

        public Boolean IsGif = false;

        public PicViewer(String path, VideoPlayer f3, List<PictureBox> spb, int width, int height, Boolean IsGif)
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            this.f3 = f3;
            this.spb = spb;
            this.path = path;
            globalPic = path;
            this.FormBorderStyle = FormBorderStyle.None;
            picPanel.Dock = DockStyle.Fill;
            this.Width = width;
            this.Height = height;
            picPanel.Width = width;
            picPanel.Height = height;
            this.IsGif = IsGif;
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

            if (VideoPlayer.wmpSide1 != null)
            {
                VideoPlayer.wmpSide1.controlDisposer();
                VideoPlayer.wmpSide1.Dispose();
                VideoPlayer.wmpSide1.Close();
            }
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

            pbb.Location = new Point((picPanel.Width / 2) - (pbb.Width / 2),
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
            };

            pbb.MouseUp += (s, args) =>
            {
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
            };

            picPanel.Controls.Add(pbb);
        }

        private void picPanel_MouseClick(object sender, MouseEventArgs e)
        {
            closingProcess();
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
                                picPanel.Controls.Clear();
                                setPic();
                                VideoPlayer.wmpSide1.controlDisposer();
                                VideoPlayer.wmpSide1.fillUpFP1(VideoPlayer.wmpSide1.vidPb, VideoPlayer.wmpSide1.typeImg);
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
                                picPanel.Controls.Clear();
                                setPic();
                                VideoPlayer.wmpSide1.controlDisposer();
                                VideoPlayer.wmpSide1.fillUpFP1(VideoPlayer.wmpSide1.vidPb, VideoPlayer.wmpSide1.typeImg);
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