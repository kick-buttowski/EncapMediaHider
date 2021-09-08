using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class CropImage : Form
    {
        Bitmap targetBitmap; 
        private bool _canDraw;
        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        public Pen cropPen;
        public DashStyle cropDashStyle = DashStyle.DashDot;
        FileInfo saveFi = null;
        double zoom = 1.0;

        private void setBestRes(Double Width, Double Height, Double ratio, PictureBox pbb)
        {
            int bestHeight = this.Height;
            int bestWidth = this.Width;

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
            Bitmap bm;
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

        public CropImage(Image img, String savePath)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            picCroppedPicture.Hide();
            setPic(picOriginalPicture, new Bitmap(img));
            saveFi = new FileInfo(savePath);

            picOriginalPicture.MouseWheel += (senderr, argss) =>
            {
                zoom = zoom + (argss.Delta > 0 ? 0.20 : -0.08);
                picOriginalPicture.Image = zoomPic(img, zoom);
                SetDisplayRectLocation(0, 500);
                AdjustFormScrollbars(true);
                picOriginalPicture.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                picOriginalPicture.SizeMode = PictureBoxSizeMode.AutoSize;
                int locX = (this.Width / 2) - (picOriginalPicture.Image.Width / 2);
                int locY = (this.Height / 2) - (picOriginalPicture.Image.Height / 2);
                picOriginalPicture.Location = new Point(locX > 0 ? locX : 0,
                      locY > 0 ? locY : 0);
                picOriginalPicture.Size = new Size(picOriginalPicture.Image.Width, picOriginalPicture.Image.Height);
            };
        }

        private void picOriginalPicture_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                CropImage_MouseClick(null, null);
                return;
            }
            picOriginalPicture.Refresh();
        }

        private void picOriginalPicture_MouseDown(object sender, MouseEventArgs e)
        {
            _canDraw = true;
            cropX = e.X;
            cropY = e.Y;
        }

        private void picOriginalPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_canDraw) 
                return;
            picOriginalPicture.Refresh();
            cropWidth = e.X - cropX;
            cropHeight = e.Y - cropY;
            Double rat = (Double)cropWidth / (Double)cropHeight;
            Double rRat = (Double)cropHeight / (Double)cropWidth;
            label1.Text = "X:" + cropWidth + "\nY:" + cropHeight + "\nX/Y:" + Math.Round(rat,4) + "\nY/X:" + Math.Round(rRat,4);
            label1.Refresh();
            picOriginalPicture.Refresh();
        }

        private void picOriginalPicture_MouseUp(object sender, MouseEventArgs e)
        {
            _canDraw = false;
        }

        private void picOriginalPicture_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(81, 160, 213), 3))
            {
                pen.DashStyle = DashStyle.DashDot;
                e.Graphics.DrawRectangle(pen, new Rectangle(cropX, cropY, cropWidth, cropHeight));
            }
        }

        private void CropImage_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (picCroppedPicture.Image != null)
                    picCroppedPicture.Image.Dispose();

                if (picOriginalPicture.Image != null)
                    picOriginalPicture.Image.Dispose();
                picOriginalPicture.Dispose();
                picCroppedPicture.Dispose();
                cropPen.Dispose();
                this.Dispose();
                this.Close();
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (picCroppedPicture.Visible == true)
            {
                if (picCroppedPicture.Image != null)
                    picCroppedPicture.Image.Dispose();
                picOriginalPicture.Show();
                picCroppedPicture.Hide();
            }
            else
            {
                if (cropWidth < 1)
                {
                    return;
                }
                //                Rectangle rect = new Rectangle(cropX+28, cropY+73, cropWidth+57, cropHeight+20);
                Rectangle rect = new Rectangle(cropX , cropY , cropWidth , cropHeight );
                Bitmap OriginalImage = new Bitmap(picOriginalPicture.Image, picOriginalPicture.Width, picOriginalPicture.Height);

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
                picOriginalPicture.Hide();
                picCroppedPicture.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            VideoPlayer.isCropped = true;
            if (targetBitmap == null)
                return;
            String ext = saveFi.Extension;
            for (int i = 0; i < 100; i++) {
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

        public void setPic(PictureBox pb, Bitmap img)
        {
            pb.Image = img;
            pb.Size = img.Size;
            pb.Anchor = AnchorStyles.None;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            Double Width = pb.Image.Width;
            Double Height = pb.Image.Height;

            if(Width*1.25 >= 1600 || Height * 1.25 >= 1000)
            {

            }
            else
            {
                Width = Width * 1.25;
                Height = Height * 1.25;
            }
            Double ratio = (Double)(Height / Width);
            setBestRes(Width, Height, ratio, pb);

            pb.Location = new Point((this.Width / 2) - (pb.Width / 2),
                      (this.Height / 2) - (pb.Height / 2));
        }


        public static Bitmap RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            Graphics gfx = Graphics.FromImage(bmp);

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.SmoothingMode = SmoothingMode.HighQuality;

            gfx.DrawImage(img, new Point(0, 0));

            gfx.Dispose();
            return bmp;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            targetBitmap = new Bitmap(picOriginalPicture.Image);
            targetBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picOriginalPicture.Image.Dispose();
            setPic(picOriginalPicture, targetBitmap);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            targetBitmap = new Bitmap(picOriginalPicture.Image);
            targetBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            picOriginalPicture.Image.Dispose();
            setPic(picOriginalPicture, targetBitmap);
        }
    }
}
