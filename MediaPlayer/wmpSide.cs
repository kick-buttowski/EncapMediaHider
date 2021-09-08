using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class wmpSide : Form
    {
        public List<PictureBox> dispPb = new List<PictureBox>();
        Boolean isShort = false;
        WMP wmp = null; 
        public List<PictureBox> vidPb = new List<PictureBox>();
        public Boolean typeImg = false;
        public PictureBox globalPb = null;
        PicViewer picViewer = null;
        public wmpSide(WMP wmp, PicViewer picViewer, Boolean isShort)
        {
            InitializeComponent();
            this.wmp = wmp;
            this.picViewer = picViewer;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.isShort = isShort;
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
            smallPb.Image = File.Exists(vidPb[j < 0 ? (vidPb.Count + j) : j].ImageLocation)?Image.FromFile(vidPb[j < 0 ? (vidPb.Count + j) : j].ImageLocation):null;
            smallPb.Cursor = Cursors.Hand;
            if (vidPb[j < 0 ? (vidPb.Count + j) : j].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
            {
                globalPb = smallPb;
                smallPb.Size = new Size(255, (int)(255 / 1.7777));
                smallPb.Margin = new Padding(20, 30, 40, 30);
            }
            else
                smallPb.Size = new Size(315, (int)(315 / 1.7777));
            smallPb.SizeMode = PictureBoxSizeMode.Zoom;
            smallPb.Name = vidPb[j < 0 ? (vidPb.Count + j) : j].Name;
            smallPb.MouseClick += (s, args) =>
            {
                wmp.axWindowsMediaPlayer1.URL = smallPb.Name;
                wmp.axWindowsMediaPlayer1.Name = smallPb.Name;
                wmp.textBox5.Text = Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).Substring(0, Path.GetFileName(wmp.axWindowsMediaPlayer1.Name).IndexOf("placeholdeerr")).Replace("Reso^ ", "Reso:").Replace("Dura^ ", "\t\tDura:").Replace("Size^ ", "\tSize:");
                if (isShort)
                    wmp.calculateDuration(0, true);
                else
                    wmp.calculateDuration(0);

                controlDisposer();
                fillUpFP1(vidPb);
            };

            dispPb.Add(smallPb);
            flowLayoutPanel1.Controls.Add(smallPb);
        }

        public void fillUpFP1(List<PictureBox> vidPb, params Boolean[] typeImg)
        {
            this.vidPb = vidPb;
            int prevX = 0, prevY = 0;
            if(typeImg.Length>0) this.typeImg = typeImg[0];
            if (vidPb != null)
            {
                for(int i=0; i<vidPb.Count; i++)
                {
                    if (wmp != null && vidPb[i].Name.Equals(wmp.axWindowsMediaPlayer1.Name))
                    {
                        List<int> seq = new List<int>();
                        for (int j = i - 2; j < i; j++)
                            seq.Add(j < 0 ? vidPb.Count+j : j);

                        for (int j = i; j < i+3 ; j++)
                            seq.Add(j >= vidPb.Count ? j-vidPb.Count : j);

                        for(int j= 0; j < seq.Count; j++)
                        {
                            if(seq[j]<0)
                            {
                                seq.RemoveAt(j);
                                seq.Insert(j, 0);
                            }

                            if (seq[j]>= vidPb.Count)
                            {
                                seq.RemoveAt(j);
                                seq.Insert(j, vidPb.Count-1);
                            }
                        }    
                        foreach (int j in seq)
                            vidPicBox(vidPb, j);
                    }
                    /*else if (form2 != null && vidPb[i].Name.Equals(form2.globalPic))
                    {
                        int noOfSet = typeImg[0] ? 5 : 2;
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
                            smallPb.Image = Image.FromFile(vidPb[j < 0 ? (vidPb.Count + j) : j].ImageLocation);
                            smallPb.Cursor = Cursors.Hand;
                            smallPb.Margin = new Padding(3, 0, 3, 3);
                            if (vidPb[j < 0 ? (vidPb.Count + j) : j].Name.Equals(form2.globalPic))
                            {
                                globalPb = smallPb;
                                Double ratio = (Double)smallPb.Image.Width / (Double)smallPb.Image.Height;
                                if (ratio > 1)
                                {
                                    smallPb.Size = new Size((int)(85 * ratio), 85);
                                    smallPb.Margin = new Padding(20, 15, 20, 30);
                                }
                                else
                                {
                                    smallPb.Size = new Size((int)(175 * ratio), 175);
                                    smallPb.Margin = new Padding(15, 15, 7, 15);
                                }
                            }
                            else
                            {
                                Double ratio = (Double)smallPb.Image.Width/ (Double)smallPb.Image.Height;
                                if (ratio > 1)
                                    smallPb.Size = new Size((int)(110 * ratio), 110);
                                else
                                    smallPb.Size = new Size((int)(213 * ratio), 213);
                            }
                            smallPb.SizeMode = PictureBoxSizeMode.Zoom;
                            smallPb.Name = vidPb[j < 0 ? (vidPb.Count + j) : j].Name;
                            smallPb.MouseClick += (s, args) =>
                            {

                                form2.globalPic = smallPb.Name;
                                form2.browser.Load(smallPb.Name);
                                controlDisposer();
                                fillUpFP1(vidPb, typeImg);
                            };

                            dispPb.Add(smallPb);
                            flowLayoutPanel1.Controls.Add(smallPb);
                        }
                    }*/
                    else if (picViewer != null && vidPb[i].Name.Equals(PicViewer.globalPic))
                    {
                        int noOfSet = typeImg[0] ? 5 : 2;
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
                            smallPb.Image = Image.FromFile(vidPb[j < 0 ? (vidPb.Count + j) : j].ImageLocation);
                            smallPb.Cursor = Cursors.Hand;
                            if (vidPb[j < 0 ? (vidPb.Count + j) : j].Name.Equals(PicViewer.globalPic))
                            {
                                globalPb = smallPb;
                                Double ratio = (Double)smallPb.Image.Width / (Double)smallPb.Image.Height;
                                if (ratio > 1)
                                {
                                    smallPb.Size = new Size((int)(110 * ratio), 110);
                                    smallPb.Margin = new Padding(3, 0, 3, 3);
                                }
                                else
                                {
                                    smallPb.Size = new Size((int)(243 * ratio), 243);
                                    smallPb.Margin = new Padding(0, 10, 0, 10);
                                }

                                smallPb.MouseEnter += (s, args) =>
                                {
                                    picViewer.fullZoomOut();
                                    prevX = 0;
                                    prevY = 0;
                                    picViewer.zoomIn();
                                };

                                smallPb.MouseMove += (s, args) =>
                                {
                                    if (args.X - prevX >= 2 || args.X - prevX <= -2 || args.Y - prevY >= 2 || args.Y - prevY <= -2)
                                    {
                                        picViewer.zoomScroll(args.X, args.Y, smallPb.Width, smallPb.Height);
                                        prevX = args.X;
                                        prevY = args.Y;
                                    }
                                };

                                smallPb.MouseLeave += (s, args) =>
                                {
                                    picViewer.fullZoomOut();
                                    prevX = 0;
                                    prevY = 0;
                                    return;
                                };
                            }
                            else
                            {
                                Double ratio = (Double)smallPb.Image.Width / (Double)smallPb.Image.Height;
                                if (ratio > 1)
                                {
                                    smallPb.Size = new Size((int)(85 * ratio), 85);
                                    smallPb.Margin = new Padding(20, 15, 20, 30);
                                }
                                else
                                {
                                    smallPb.Size = new Size((int)(185 * ratio), 185);
                                    smallPb.Margin = new Padding(20, 10, 7, 10);
                                }
                            }
                            smallPb.SizeMode = PictureBoxSizeMode.Zoom;
                            smallPb.Name = vidPb[j < 0 ? (vidPb.Count + j) : j].Name;
                            smallPb.MouseClick += (s, args) =>
                            {
                                if (picViewer.pbb.Image != null)
                                {
                                    picViewer.pbb.Image.Dispose();
                                    picViewer.pbb.Dispose();
                                }
                                picViewer.picPanel.Controls.Clear();
                                PicViewer.globalPic = smallPb.Name;
                                picViewer.path = smallPb.Name;
                                picViewer.setPic();
                                controlDisposer();
                                fillUpFP1(vidPb, typeImg);
                            };

                            dispPb.Add(smallPb);
                            flowLayoutPanel1.Controls.Add(smallPb);
                        }
                    }
                }
            }
        }
    }
}
