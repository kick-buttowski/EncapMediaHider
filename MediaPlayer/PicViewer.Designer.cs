
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.hideBtnH = new System.Windows.Forms.Button();
            this.hideBtnW = new System.Windows.Forms.Button();
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(297, 1055);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // hideBtnH
            // 
            this.hideBtnH.BackColor = System.Drawing.Color.Black;
            this.hideBtnH.FlatAppearance.BorderSize = 0;
            this.hideBtnH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.hideBtnH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.hideBtnH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtnH.Image = global::Calculator.Properties.Resources.more__1_;
            this.hideBtnH.Location = new System.Drawing.Point(329, 221);
            this.hideBtnH.Margin = new System.Windows.Forms.Padding(0);
            this.hideBtnH.Name = "hideBtnH";
            this.hideBtnH.Size = new System.Drawing.Size(11, 33);
            this.hideBtnH.TabIndex = 0;
            this.hideBtnH.UseVisualStyleBackColor = false;
            this.hideBtnH.Visible = false;
            this.hideBtnH.Click += new System.EventHandler(this.hideBtnH_Click);
            // 
            // hideBtnW
            // 
            this.hideBtnW.BackColor = System.Drawing.Color.Black;
            this.hideBtnW.FlatAppearance.BorderSize = 0;
            this.hideBtnW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.hideBtnW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.hideBtnW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideBtnW.Image = global::Calculator.Properties.Resources.more__2_;
            this.hideBtnW.Location = new System.Drawing.Point(942, 496);
            this.hideBtnW.Name = "hideBtnW";
            this.hideBtnW.Size = new System.Drawing.Size(33, 10);
            this.hideBtnW.TabIndex = 2;
            this.hideBtnW.UseVisualStyleBackColor = false;
            this.hideBtnW.Visible = false;
            this.hideBtnW.Click += new System.EventHandler(this.hideBtnH_Click);
            // 
            // PicViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(5)))));
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.hideBtnW);
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
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button hideBtnH;
        public System.Windows.Forms.Button hideBtnW;
    }
}