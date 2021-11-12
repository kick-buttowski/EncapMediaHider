using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{ 
    public partial class Calculator : Form
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

        public static String globalFilt = "";
        public static String globalStr = "", globalType = "Videos";
        public static Button globalTypeButton = new Button(), globalDirButton = null;
        public static Boolean entered = true, pattern = false;
        const string divideByZero = "Err!";
        const string syntaxErr = "SYNTAX ERROR!";
        bool decimalPointActive = false;
        public static Explorer v;
        public static Boolean disCef = false;
        public static TextBox globalCalcTb = null;


        public Calculator()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.backgroundWorker1.RunWorkerAsync(100);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            txtDisplay.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, txtDisplay.Width, txtDisplay.Height, 20, 20));
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height,15, 15));
            btnReset.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnReset.Width, btnReset.Height,15, 15));
            btnDiv.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDiv.Width, btnDiv.Height,15, 15));
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height,15, 15));
            btnCopy.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCopy.Width, btnCopy.Height,15, 15));
            btn7.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn7.Width, btn7.Height,15, 15));
            btn8.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn8.Width, btn8.Height,15, 15));
            btn9.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn9.Width, btn9.Height,15, 15));

            btnMul.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnMul.Width, btnMul.Height,15, 15));
            btnSub.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSub.Width, btnSub.Height,15, 15));
            btn1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn1.Width, btn1.Height,15, 15));
            btn2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn2.Width, btn2.Height,15, 15));
            btn3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn3.Width, btn3.Height,15, 15));
            btn4.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn4.Width, btn4.Height,15, 15));
            btn5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn5.Width, btn5.Height,15, 15));
            btn6.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn6.Width, btn6.Height,15, 15));
            btnAdd.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAdd.Width, btnAdd.Height,15, 15));
            btn0.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn0.Width, btn0.Height,15, 15));
            btnDecimal.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDecimal.Width, btnDecimal.Height,15, 15));
            btnRes.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRes.Width, btnRes.Height,15, 15));

            this.Location = new Point(1890, 30);
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDisplay.Text)) return;
            Clipboard.SetText(txtDisplay.Text);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            PreCheck_ButtonClick();
            previousOperation = Operation.None;
            txtDisplay.Clear();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            decimalPointActive = false;
            PreCheck_ButtonClick();
            if (txtDisplay.Text.Length > 0)
            {
                double d;
                if (!double.TryParse(txtDisplay.Text[txtDisplay.Text.Length - 1].ToString(), out d))
                {
                    previousOperation = Operation.None;
                }

                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
            if (txtDisplay.Text.Length == 0)
            {
                previousOperation = Operation.None;
            }
            if (previousOperation != Operation.None)
            {
                currentOperation = previousOperation;
            }
        }

        private void BtnDiv_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Div;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void BtnMul_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Mul;
            PerformCalculation(previousOperation);
            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void BtnSub_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0 || previousOperation == Operation.Sub) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Sub;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            PreCheck_ButtonClick();
            currentOperation = Operation.Add;
            PerformCalculation(previousOperation);

            previousOperation = currentOperation;
            EnableOperatorButtons(false);
            txtDisplay.Text += (sender as Button).Text;
        }

        private void PerformCalculation(Operation previousOperation)
        {
            try
            {
                if (previousOperation == Operation.None)
                    return;
                List<double> lstNums = null;

                switch (previousOperation)
                {
                    case Operation.Add:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Add;
                            return;
                        }
                        lstNums = txtDisplay.Text.Split('+').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] + lstNums[1]).ToString();
                        break;
                    case Operation.Sub:
                        int idx = txtDisplay.Text.LastIndexOf('-'); // To handle ex: -9-2
                        if (idx > 0)
                        {
                            var op1 = Convert.ToDouble(txtDisplay.Text.Substring(0, idx));
                            var op2 = Convert.ToDouble(txtDisplay.Text.Substring(idx + 1));
                            txtDisplay.Text = (op1 - op2).ToString();
                        }
                        break;
                    case Operation.Mul:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Mul;
                            return;
                        }
                        lstNums = txtDisplay.Text.Split('*').Select(double.Parse).ToList();
                        txtDisplay.Text = (lstNums[0] * lstNums[1]).ToString();
                        break;
                    case Operation.Div:
                        if (currentOperation == Operation.Sub)
                        {
                            currentOperation = Operation.Div;
                            return;
                        }
                        try
                        {
                            lstNums = txtDisplay.Text.Split('/').Select(double.Parse).ToList();
                            if (lstNums[1] == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            txtDisplay.Text = (lstNums[0] / lstNums[1]).ToString();
                        }
                        catch (DivideByZeroException)
                        {
                            txtDisplay.Text = divideByZero;
                        }
                        break;
                    case Operation.None:
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                txtDisplay.Text = syntaxErr;
            }
        }

        private void BtnNum_Click(object btn, EventArgs e)
        {
            if (txtDisplay.Text == syntaxErr || txtDisplay.Text == divideByZero)
            {
                txtDisplay.Text = string.Empty;
            }
            EnableOperatorButtons();
            PreCheck_ButtonClick();
            txtDisplay.Text += (btn as Button).Text;
        }

        private void PreCheck_ButtonClick()
        {
            if (txtDisplay.Text == divideByZero || txtDisplay.Text == syntaxErr)
                txtDisplay.Clear();
            if (previousOperation != Operation.None)
            {
                EnableOperatorButtons();
            }
        }

        private void EnableOperatorButtons(bool enable = true)
        {
            btnMul.Enabled = enable;
            btnDiv.Enabled = enable;
            btnAdd.Enabled = enable;
            if (!enable)
            {
                decimalPointActive = false;
            }
            //btnSub.Enabled = enable;
        }
        enum Operation
        {
            Add,
            Sub,
            Mul,
            Div,
            None
        }

        Operation previousOperation = Operation.None;
        Operation currentOperation = Operation.None;

        private void BtnRes_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength == 0) return;
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.None;
        }

        private void BtnDecimal_Click(object sender, EventArgs e)
        {
            if (decimalPointActive) return;
            if (txtDisplay.Text == syntaxErr || txtDisplay.Text == divideByZero)
            {
                txtDisplay.Text = string.Empty;
            }
            EnableOperatorButtons();
            PreCheck_ButtonClick();
            txtDisplay.Text += (sender as Button).Text;
            decimalPointActive = true;
        }


        public static void deleteTxtFile(DirectoryInfo di)
        {
            foreach (DirectoryInfo di1 in di.GetDirectories())
            {
                foreach (FileInfo fi in di1.GetFiles())
                {
                    if (fi.Name.EndsWith(".txt") && !fi.Name.Contains("disPic"))
                    {
                        fi.Delete();
                    }
                }
                deleteTxtFile(di1);
            }
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            if (pattern)
            {
                if (tbx.Text.Equals("6633+--"))
                {
                    tbx.Text = "";
                    this.Hide();
                    //SetMonitorState(2);
                    v.Show();
                    pattern = !pattern;
                }
            }

            if (tbx.Text.Equals("608442786042") || tbx.Text.Equals("/**/-+") || tbx.Text.Equals("6633+--"))
            {
                if (!v.Visible)
                {
                    txtDisplay.Text = "";
                    this.Hide();
                    //SetMonitorState(2);
                    v.Show();
                }
            }
            else if (tbx.Text.Contains("608442786078"))
            {
                //SetMonitorState(2);

                if (tbx.Text.Contains("**"))
                {
                    v = new Explorer(this);
                    this.Hide();
                    String searchTxt = tbx.Text.ToLower().Replace("608442786078", "").Replace("**", "");
                    tbx.Text = "";
                    v.searchText = searchTxt;
                    v.flowLayoutPanel1.Controls.Clear();
                    GC.Collect();
                    v.Explorer_Load(null, null);
                    v.ShowDialog();
                }
            }
            else if (tbx.Text.Equals("Clear"))
            {
                DirectoryInfo di = new DirectoryInfo("F:\\Calculator");
                    deleteTxtFile(di);
                di = new DirectoryInfo("H:\\vivado\\rand_name\\rand_name.ir");
                deleteTxtFile(di);
            }
            else if (tbx.Text.Equals("trimm") || tbx.Text.Equals("trim"))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All files (*.*)|*.*";

                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    foreach (String fi in ofd.FileNames)
                    {
                        WMP wmp = new WMP(null);
                        FileInfo fileInfo = new FileInfo(fi);
                        wmp.axWindowsMediaPlayer1.URL = fileInfo.FullName;
                        wmp.axWindowsMediaPlayer1.Name = fileInfo.FullName;
                        wmp.Location = new Point(298, 50);
                        wmp.calculateDuration(0);

                        TranspBack transpBack = new TranspBack(wmp, null, null, null);
                        transpBack.Show();
                        wmp.Show();
                    }
                }
            }
            else if (tbx.Text.Equals("collage"))
            {
                System.Diagnostics.Process.Start("firefox.exe", "https://www.fotojet.com/apps/?entry=collage");
            
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            v = new Explorer(this);
            v.Explorer_Load(null, null);
            button2_Click(null, null);
            if (txtDisplay.Text.Equals("608442786042") || txtDisplay.Text.Equals("/**/-+") || txtDisplay.Text.Equals("6633+--"))
            {
                if (!v.Visible)
                {
                    txtDisplay.Text = "";
                    this.Hide();
                    //SetMonitorState(2);
                    v.Show();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void Calculator_DragEnter(object sender, DragEventArgs e)
        {
            Thread lala = new Thread(() =>
            {
                e.Effect = DragDropEffects.Copy;
                object data = e.Data.GetData("UniformResourceLocator");
                MemoryStream ms = data as MemoryStream;
                byte[] bytes = ms.ToArray();
                Encoding encod = Encoding.ASCII;
                String url = encod.GetString(bytes);
                string htmlCode = "";
                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(url);
                }

                htmlCode = htmlCode.Substring(htmlCode.IndexOf("contentUrl") + 12);

                String downUrl = htmlCode.Substring(0, htmlCode.IndexOf("</span"));
                using (WebClient wc = new WebClient())
                {
                    int startPoitn = downUrl.IndexOf(".jpg");
                    String fileName = "E:\\Git\\" + downUrl.Substring(startPoitn - 9, 13);
                    wc.DownloadFileAsync(
                        new System.Uri(downUrl),
                        fileName
                    );
                }
            });
            lala.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pattern = !pattern;
            if (pattern)
                button2.BackColor = Color.FromArgb(255, 192, 128);
            else
                button2.BackColor = Color.FromArgb(0,0,0);
            txtDisplay.Focus();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
