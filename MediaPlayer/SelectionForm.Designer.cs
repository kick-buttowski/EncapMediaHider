namespace MediaPlayer
{
	partial class SelectionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionForm));
			this.SuspendLayout();
			// 
			// cropBtn
			// 
			// 
			// qlibToolsep5
			// 
			// selectionCopyBtn
			// 
			// 
			// selectionSelectAllBtn
			// 
			// 
			// editSelectionBtn
			// 
			// cutBtn
			// 
			// 
			// SelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Red;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(128, 128);
			this.ControlBox = false;
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectionForm";
			this.Opacity = 0.5D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "SelectionForm";
			this.TransparencyKey = System.Drawing.Color.Red;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ToolStripMenuItem cropBtn;
		private System.Windows.Forms.ToolStripMenuItem selectionCopyBtn;
		private System.Windows.Forms.ToolStripMenuItem selectionSelectAllBtn;
		private System.Windows.Forms.ToolStripMenuItem editSelectionBtn;
		private System.Windows.Forms.ToolStripMenuItem cutBtn;
	}
}