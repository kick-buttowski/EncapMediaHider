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
        public static Explorer games;
        public static Boolean disCef = false;
        public static TextBox globalCalcTb = null;
        public static List<String> staleDeletes = new List<string>();

        Color tempColor = Color.Orange;

        public Calculator()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.backgroundWorker1.RunWorkerAsync(100);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            //txtDisplay.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, txtDisplay.Width, txtDisplay.Height, 20, 20));
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height,10, 10));
            btnReset.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnReset.Width, btnReset.Height,10, 10));
            btnDiv.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDiv.Width, btnDiv.Height,10, 10));
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height,10, 10));
            btnCopy.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCopy.Width, btnCopy.Height,10, 10));
            btn7.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn7.Width, btn7.Height,10, 10));
            btn8.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn8.Width, btn8.Height,10, 10));
            btn9.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn9.Width, btn9.Height,10, 10));
            //panel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 20, 20));

            btnMul.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnMul.Width, btnMul.Height,10, 10));
            btnSub.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSub.Width, btnSub.Height,10, 10));
            btn1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn1.Width, btn1.Height,10, 10));
            btn2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn2.Width, btn2.Height,10, 10));
            btn3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn3.Width, btn3.Height,10, 10));
            btn4.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn4.Width, btn4.Height,10, 10));
            btn5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn5.Width, btn5.Height,10, 10));
            btn6.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn6.Width, btn6.Height,10, 10));
            btnAdd.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAdd.Width, btnAdd.Height,10, 10));
            btn0.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn0.Width, btn0.Height,10, 10));
            btnDecimal.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDecimal.Width, btnDecimal.Height,10, 10));
            btnRes.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRes.Width, btnRes.Height,10, 10));
            String[] defColor = null;
            if (File.Exists("F:\\Calculator\\ThemeColor.txt"))
                defColor = File.ReadAllText("F:\\Calculator\\ThemeColor.txt").Split(',');
            else
                defColor = new string[]{  "50", "133", "250"};
            tempColor = Color.FromArgb(int.Parse(defColor[0]), int.Parse(defColor[1]), int.Parse(defColor[2]));
            /*btnAdd.BackColor = tempColor;
            btnSub.BackColor = tempColor;
            btnMul.BackColor = tempColor;
            btnDiv.BackColor = tempColor;
            btnClear.BackColor = tempColor;
            btnReset.BackColor = tempColor;
            btnCopy.BackColor = tempColor;*/

            button2.FlatAppearance.MouseOverBackColor = this.BackColor;
            button2.FlatAppearance.MouseDownBackColor= this.BackColor;

            button7.FlatAppearance.MouseOverBackColor = this.BackColor;
            button7.FlatAppearance.MouseDownBackColor = this.BackColor;
            button2.BackColor = tempColor;
            button6.BackColor = tempColor;
            button7.BackColor = tempColor;
            panel1.BackColor = this.BackColor;
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
            timer1.Enabled = true;
            if (tbx.Text.Equals("Clear"))
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

                    timer1.Enabled = false;
                    foreach (String fi in ofd.FileNames)
                    {
                        WMP wmp = new WMP(null, null, null, null);
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
                timer1.Enabled = false;
                System.Diagnostics.Process.Start("firefox.exe", "https://www.fotojet.com/apps/?entry=collage");

            }
            else if (tbx.Text.Equals("firefox"))
            {
                timer1.Enabled = false;
                System.Diagnostics.Process.Start("firefox.exe", "https://www.google.com");

            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            v = new Explorer(this, false);
            v.Explorer_Load(null, null);
            btnRes.BackColor = tempColor;

            panel1.BackColor = tempColor;
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

        private void Calculator_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private bool mouseDown = false;
        private Point lastLocation;
        private void Calculator_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Calculator_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Calculator_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = "";
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 0)
            txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length-1);
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnDiv_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 0) {
                string type = txtDisplay.Text.Substring(txtDisplay.Text.Length - 1);
                if (type == "0" || type == "1" || type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8" || type == "9")
                    txtDisplay.Text = txtDisplay.Text + "/";
                else
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "/";
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnMul_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 0)
            {
                string type = txtDisplay.Text.Substring(txtDisplay.Text.Length - 1);
                if (type == "0" || type == "1" || type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8" || type == "9")
                    txtDisplay.Text = txtDisplay.Text + "*";
                else
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "*";
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnSub_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 0)
            {
                string type = txtDisplay.Text.Substring(txtDisplay.Text.Length - 1);
                if (type == "0" || type == "1" || type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8" || type == "9")
                    txtDisplay.Text = txtDisplay.Text + "-";
                else
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "-";
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 0)
            {
                string type = txtDisplay.Text.Substring(txtDisplay.Text.Length - 1);
                if (type == "0" || type == "1" || type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8" || type == "9")
                    txtDisplay.Text = txtDisplay.Text + "+";
                else
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "+";
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "7";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "8";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "9";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "4";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "5";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "6";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "1";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "2";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "3";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "00";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Text = txtDisplay.Text + "0";
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnDecimal_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 0)
            {
                string type = txtDisplay.Text.Substring(txtDisplay.Text.Length - 1);
                if (type == "0" || type == "1" || type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8" || type == "9")
                    txtDisplay.Text = txtDisplay.Text + ".";
                else
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + ".";
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnRes_Click_1(object sender, EventArgs e)
        {
            string type = txtDisplay.Text.Replace("\r\n", "");
            txtDisplay.Focus();
            foreach (char tmp in type.ToCharArray())
            {
                if (tmp == '0' || tmp == '1' || tmp == '2' || tmp == '3' || tmp == '4' || tmp == '5' || tmp == '6' || tmp == '7' || tmp == '8' ||
                    tmp == '9' || tmp == '+' || tmp == '-' || tmp == '*' || tmp == '/' || tmp == '.' || tmp == '%')
                {

                }
                else
                {
                    txtDisplay.Text = "Invalid syn";
                    return;
                }
            }

            DataTable dt = new DataTable();
            var v = dt.Compute(type, "");
            String res = v.ToString();
            if (res.Contains("."))
            {
                txtDisplay.Text = Math.Round(decimal.Parse(res), 3).ToString();
            }
            else
            {
                txtDisplay.Text = res;
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void btnCopy_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            if (txtDisplay.Text.Length > 1)
            {
                string type = txtDisplay.Text.Substring(txtDisplay.Text.Length - 1);
                if (type == "0" || type == "1" || type == "2" || type == "3" || type == "4" || type == "5" || type == "6" || type == "7" || type == "8" || type == "9")
                    txtDisplay.Text = txtDisplay.Text + "%";
                else
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "%";
            }
            txtDisplay.SelectionStart = txtDisplay.Text.Length;
            txtDisplay.SelectionLength = 0;
        }

        private void txtDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnRes_Click_1(null, null);
            }
            else if(e.KeyCode == Keys.C)
            {
                txtDisplay.Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TextBox tbx = txtDisplay;

            if ((tbx.Text.Equals("608442786042") || tbx.Text.Equals("/**/-+")) && (btnRes.BackColor == tempColor) && !v.Visible)
            {
                    txtDisplay.Text = "";
                    this.Hide();
                    //SetMonitorState(2);
                    v.Show();
            }
            else if (tbx.Text.Equals("/**/+-"))
            {
                txtDisplay.Text = "";
                this.Hide();
                //SetMonitorState(2);
                games = new Explorer(this, true);
                games.Explorer_Load(null, null);
                btnRes.BackColor = tempColor;

                panel1.BackColor = tempColor;
                games.Show();
            }
            //tbx.Focus();
        }

        private void Calculator_Activated(object sender, EventArgs e)
        {
            txtDisplay.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
