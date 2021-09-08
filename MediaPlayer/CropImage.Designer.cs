namespace MediaPlayer
{
    partial class CropImage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picOriginalPicture = new System.Windows.Forms.PictureBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.picCroppedPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCroppedPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // picOriginalPicture
            // 
            this.picOriginalPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOriginalPicture.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picOriginalPicture.Location = new System.Drawing.Point(582, 55);
            this.picOriginalPicture.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.picOriginalPicture.Name = "picOriginalPicture";
            this.picOriginalPicture.Size = new System.Drawing.Size(910, 1033);
            this.picOriginalPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOriginalPicture.TabIndex = 0;
            this.picOriginalPicture.TabStop = false;
            this.picOriginalPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.picOriginalPicture_Paint);
            this.picOriginalPicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picOriginalPicture_MouseClick);
            this.picOriginalPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picOriginalPicture_MouseDown);
            this.picOriginalPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picOriginalPicture_MouseMove);
            this.picOriginalPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picOriginalPicture_MouseUp);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Black;
            this.button6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(34, 102);
            this.button6.TabIndex = 27;
            this.button6.Text = "Crop";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Black;
            this.button7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(0, 104);
            this.button7.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(34, 105);
            this.button7.TabIndex = 28;
            this.button7.Text = "Save";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // picCroppedPicture
            // 
            this.picCroppedPicture.Location = new System.Drawing.Point(401, 200);
            this.picCroppedPicture.Name = "picCroppedPicture";
            this.picCroppedPicture.Size = new System.Drawing.Size(100, 50);
            this.picCroppedPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCroppedPicture.TabIndex = 30;
            this.picCroppedPicture.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-3, 990);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 76);
            this.label1.TabIndex = 31;
            this.label1.Text = "X:\r\nY:\r\nX/Y:\r\nY/X:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(0, 211);
            this.button1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 119);
            this.button1.TabIndex = 34;
            this.button1.Text = "+90Deg";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(160)))), ((int)(((byte)(213)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(0, 350);
            this.button2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 119);
            this.button2.TabIndex = 35;
            this.button2.Text = "-90Deg";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CropImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1902, 1055);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picCroppedPicture);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.picOriginalPicture);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CropImage";
            this.Text = "Image Crop";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CropImage_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCroppedPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picOriginalPicture;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.PictureBox picCroppedPicture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

