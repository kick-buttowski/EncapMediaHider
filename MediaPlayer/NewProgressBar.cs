using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MediaPlayer
{
    public class NewProgressBar : ProgressBar
    {
        public NewProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            /*SolidBrush brush;
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;

            brush = new SolidBrush(this.BackColor);
            
            e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);*/

            /*LinearGradientBrush brush = null;
            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            double scaleFactor = (((double)Value - (double)Minimum) / ((double)Maximum - (double)Minimum));

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);

            rec.Width = (int)(rec.Width * scaleFactor) + 1;
            rec.Height -= 4;
            brush = new LinearGradientBrush(rec, this.ForeColor, this.BackColor, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);*/

            Graphics graph = e.Graphics;
            double scaleFactor = (((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum));
            int sliderWidth = (int)(this.Width * scaleFactor);
            Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, this.Height);
            using (var brushSlider = new SolidBrush(this.ForeColor))
            {
                
                //Painting
                if (sliderWidth > 1) //Slider
                    graph.FillRectangle(brushSlider, rectSlider);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Graphics graph = pevent.Graphics;
            Rectangle rectChannel = new Rectangle(0, 0, this.Width, this.Height);
            using (var brushChannel = new SolidBrush(this.BackColor))
            {
                
                //Painting
                graph.Clear(this.Parent.BackColor);//Surface
                graph.FillRectangle(brushChannel, rectChannel);//Channel
            }
        }

    }
}
