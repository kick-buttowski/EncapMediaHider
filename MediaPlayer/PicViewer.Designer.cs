
namespace MediaPlayer
{
    partial class PicViewer
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
            this.components = new System.ComponentModel.Container();
            this.picPanel = new System.Windows.Forms.Panel();
            this.picCroppedPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ccBtn = new System.Windows.Forms.Button();
            this.cBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.crop = new System.Windows.Forms.Button();
            this.editBtnH = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.hideBtnW = new System.Windows.Forms.Button();
            this.hideBtnH = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.Button();
            this.editPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picCroppedPicture)).BeginInit();
            this.editPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPanel
            // 
            this.picPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(5)))));
            this.picPanel.Location = new System.Drawing.Point(0, 0);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(1920, 1080);
            this.picPanel.TabIndex = 0;
            this.picPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPanel_MouseClick);
            // 
            // picCroppedPicture
            // 
            this.picCroppedPicture.Location = new System.Drawing.Point(1066, 382);
            this.picCroppedPicture.Name = "picCroppedPicture";
            this.picCroppedPicture.Size = new System.Drawing.Size(100, 50);
            this.picCroppedPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCroppedPicture.TabIndex = 47;
            this.picCroppedPicture.TabStop = false;
            this.picCroppedPicture.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1680, 801);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 76);
            this.label1.TabIndex = 46;
            this.label1.Text = "X:\r\nY:\r\nX/Y:\r\nY/X:";
            // 
            // ccBtn
            // 
            this.ccBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ccBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ccBtn.FlatAppearance.BorderSize = 0;
            this.ccBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ccBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ccBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ccBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ccBtn.ForeColor = System.Drawing.Color.White;
            this.ccBtn.Image = global::Calculator.Properties.Resources.rotate;
            this.ccBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ccBtn.Location = new System.Drawing.Point(2, 102);
            this.ccBtn.Margin = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.ccBtn.Name = "ccBtn";
            this.ccBtn.Size = new System.Drawing.Size(91, 32);
            this.ccBtn.TabIndex = 44;
            this.ccBtn.Text = "-90°";
            this.ccBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ccBtn.UseVisualStyleBackColor = false;
            this.ccBtn.Click += new System.EventHandler(this.ccBtn_Click);
            // 
            // cBtn
            // 
            this.cBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.cBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cBtn.FlatAppearance.BorderSize = 0;
            this.cBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.cBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.cBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBtn.ForeColor = System.Drawing.Color.White;
            this.cBtn.Image = global::Calculator.Properties.Resources.rotate;
            this.cBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBtn.Location = new System.Drawing.Point(2, 68);
            this.cBtn.Margin = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.cBtn.Name = "cBtn";
            this.cBtn.Size = new System.Drawing.Size(91, 32);
            this.cBtn.TabIndex = 43;
            this.cBtn.Text = "+90°";
            this.cBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cBtn.UseVisualStyleBackColor = false;
            this.cBtn.Click += new System.EventHandler(this.cBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.saveBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.saveBtn.FlatAppearance.BorderSize = 0;
            this.saveBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.saveBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.ForeColor = System.Drawing.Color.White;
            this.saveBtn.Image = global::Calculator.Properties.Resources.save_file;
            this.saveBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveBtn.Location = new System.Drawing.Point(2, 136);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Padding = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.saveBtn.Size = new System.Drawing.Size(91, 32);
            this.saveBtn.TabIndex = 42;
            this.saveBtn.Text = "Save";
            this.saveBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // crop
            // 
            this.crop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.crop.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.crop.FlatAppearance.BorderSize = 0;
            this.crop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.crop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.crop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crop.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crop.ForeColor = System.Drawing.Color.White;
            this.crop.Image = global::Calculator.Properties.Resources.crop;
            this.crop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crop.Location = new System.Drawing.Point(2, 0);
            this.crop.Margin = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.crop.Name = "crop";
            this.crop.Size = new System.Drawing.Size(91, 32);
            this.crop.TabIndex = 41;
            this.crop.Text = "Crop";
            this.crop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.crop.UseVisualStyleBackColor = false;
            this.crop.Click += new System.EventHandler(this.crop_Click);
            // 
            // editBtnH
            // 
            this.editBtnH.BackColor = System.Drawing.Color.Black;
            this.editBtnH.FlatAppearance.BorderSize = 0;
            this.editBtnH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.editBtnH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.editBtnH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editBtnH.Image = global::Calculator.Properties.Resources.more__1_;
            this.editBtnH.Location = new System.Drawing.Point(1108, 814);
            this.editBtnH.Margin = new System.Windows.Forms.Padding(0);
            this.editBtnH.Name = "editBtnH";
            this.editBtnH.Size = new System.Drawing.Size(11, 33);
            this.editBtnH.TabIndex = 3;
            this.editBtnH.UseVisualStyleBackColor = false;
            this.editBtnH.Visible = false;
            this.editBtnH.Click += new System.EventHandler(this.editBtnH_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(297, 1055);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // hideBtnW
            // 
            this.hideBtnW.BackColor = System.Drawing.Color.Black;
            this.hideBtnW.FlatAppearance.BorderSize = 0;
            this.hideBtnW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.hideBtnW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.hideBtnW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtnW.Image = global::Calculator.Properties.Resources.more__2_;
            this.hideBtnW.Location = new System.Drawing.Point(937, 521);
            this.hideBtnW.Name = "hideBtnW";
            this.hideBtnW.Size = new System.Drawing.Size(33, 10);
            this.hideBtnW.TabIndex = 2;
            this.hideBtnW.UseVisualStyleBackColor = false;
            this.hideBtnW.Visible = false;
            this.hideBtnW.Click += new System.EventHandler(this.hideBtnH_Click);
            // 
            // hideBtnH
            // 
            this.hideBtnH.BackColor = System.Drawing.Color.Black;
            this.hideBtnH.FlatAppearance.BorderSize = 0;
            this.hideBtnH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.hideBtnH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.hideBtnH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtnH.Image = global::Calculator.Properties.Resources.more__1_;
            this.hideBtnH.Location = new System.Drawing.Point(1066, 237);
            this.hideBtnH.Margin = new System.Windows.Forms.Padding(0);
            this.hideBtnH.Name = "hideBtnH";
            this.hideBtnH.Size = new System.Drawing.Size(11, 33);
            this.hideBtnH.TabIndex = 0;
            this.hideBtnH.UseVisualStyleBackColor = false;
            this.hideBtnH.Visible = false;
            this.hideBtnH.Click += new System.EventHandler(this.hideBtnH_Click);
            // 
            // preview
            // 
            this.preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.preview.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.preview.FlatAppearance.BorderSize = 0;
            this.preview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.preview.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.preview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.preview.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preview.ForeColor = System.Drawing.Color.White;
            this.preview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.preview.Location = new System.Drawing.Point(2, 34);
            this.preview.Margin = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(91, 32);
            this.preview.TabIndex = 46;
            this.preview.Text = "Preview";
            this.preview.UseVisualStyleBackColor = false;
            this.preview.Click += new System.EventHandler(this.preview_Click);
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add(this.crop);
            this.editPanel.Controls.Add(this.preview);
            this.editPanel.Controls.Add(this.cBtn);
            this.editPanel.Controls.Add(this.ccBtn);
            this.editPanel.Controls.Add(this.saveBtn);
            this.editPanel.Location = new System.Drawing.Point(1368, 795);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(95, 171);
            this.editPanel.TabIndex = 48;
            // 
            // PicViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(5)))));
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.editBtnH);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editPanel);
            this.Controls.Add(this.hideBtnW);
            this.Controls.Add(this.picCroppedPicture);
            this.Controls.Add(this.hideBtnH);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.picPanel);
            this.Name = "PicViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PicViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PicViewer_FormClosing);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPanel_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.picCroppedPicture)).EndInit();
            this.editPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button hideBtnH;
        public System.Windows.Forms.Button hideBtnW;
        public System.Windows.Forms.Button editBtnH;
        private System.Windows.Forms.Button crop;
        private System.Windows.Forms.Button ccBtn;
        private System.Windows.Forms.Button cBtn;
        private System.Windows.Forms.Button saveBtn;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox picCroppedPicture;
        private System.Windows.Forms.Button preview;
        public System.Windows.Forms.FlowLayoutPanel editPanel;
    }
}