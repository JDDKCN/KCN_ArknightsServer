using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static GUI.Msg;
using KCNAPI;

namespace SMSBoomGUI
{
    public partial class TextEditor : Form
    {
        public TextEditor()
        {
            InitializeComponent();
        }

        #region 绘制圆角窗体
        private void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            path = getRoundRectPath(rect, 50);
            this.Region = new Region(path);
        }
        private GraphicsPath getRoundRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle arcRect = new Rectangle(rect.Location, new Size(radius, radius));
            //左上角
            path.AddArc(arcRect, 180, 90);//从180度开始，顺时针,滑过90度
            //右上角
            arcRect.X = rect.Right - radius;
            path.AddArc(arcRect, 270, 90); //
            //右下角
            arcRect.Y = rect.Bottom - radius;
            path.AddArc(arcRect, 0, 90);
            //左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }

        private void SetProxy_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        #endregion

        #region 绘制程序Panel白线及颜色
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            panel1.BackColor = Color.FromArgb(150, 131, 175, 200);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel1.ClientRectangle,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                            panel2.ClientRectangle,
                            Color.White,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            1,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            1,
                            ButtonBorderStyle.Solid);

        }

        #endregion

        #region 鼠标事件
        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }
        //关闭picturebox的鼠标操作
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }


        #endregion

        #region 功能实现

        /// <summary>
        /// 保存txt
        /// </summary>
        private void Write()
        {
            try
            {
                string Path1 = ".\\KCN.txt";

                StreamWriter sw = new StreamWriter(Path1, false);
                sw.WriteLine(richTextBox1.Text);
                sw.Close();
                MsgShow("保存成功！", "提示", true);

            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error",true);
            }

        }

        /// <summary>
        /// 打开文件
        /// </summary>
        private void Open()
        {
            //打开文件
            OpenFileDialog ofd = new OpenFileDialog();      //声明打开文件
            ofd.Title = "请选择打开的文件";
            ofd.Filter = "txt文件(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)  //窗体打开成功
            {
                try
                {
                    string reader2 = File.ReadAllText(ofd.FileName);
                    richTextBox1.Text = reader2;
                }
                catch (System.Exception ex)
                {
                    MsgShow("程序出错！\n" + ex.Message, "Error",true);
                }
            }

        }

        /// <summary>
        /// 文件另存为
        /// </summary>
        private void Save()
        {
            try
            {
                string str = System.Environment.CurrentDirectory;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "";
                sfd.InitialDirectory = str;
                sfd.Filter = "文本文件| *.txt";
                sfd.ShowDialog();
                string Path = sfd.FileName;
                if(Path != "")
                {
                    StreamWriter sw = new StreamWriter(Path, false);
                    sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                    MsgShow("保存成功！", "提示", true);
                }

            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error",true);
            }

        }

        /// <summary>
        /// 读json改IP
        /// </summary>
        private void Json_IP()
        {

            //json读取
            StreamReader reader = File.OpenText(".\\config.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            string ArkServerPath = jsonObject["ArkServerPath"].ToString(); // 类似
            reader.Close();

            //json写入
            string JsonPath = ArkServerPath + "\\config.json";
            string JsonString = File.ReadAllText(JsonPath, Encoding.UTF8);
            JObject jobject = JObject.Parse(JsonString);
            jobject["server"]["url"] = "http://" + richTextBox1.Text + ":38660";
            string convertString = Convert.ToString(jobject);
            File.WriteAllText(JsonPath, convertString);
            Close();
            MsgShow("已将IP修改为：\n" + "http://" + richTextBox1.Text + ":38660", "提示");

        }

        #endregion

        #region 剪切板右键菜单

        private void 文字编辑器V100BetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                RichTextBox txtBox = (RichTextBox)contextMenuStrip1.SourceControl;
                int index = txtBox.SelectionStart;  //记录下粘贴前的光标位置
                string text = txtBox.Text;
                //删除选中的文本
                text = text.Remove(txtBox.SelectionStart, txtBox.SelectionLength);
                //在当前光标输入点插入剪切板内容
                text = text.Insert(txtBox.SelectionStart, Clipboard.GetText());
                txtBox.Text = text;
                //重设光标位置
                txtBox.SelectionStart = index;
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            //没有选择文本时，复制菜单禁用
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
                复制ToolStripMenuItem.Enabled = true;
            else
                复制ToolStripMenuItem.Enabled = false;
            //剪切板没有文本内容时，粘贴菜单禁用
            if (Clipboard.ContainsText())
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
            else
                粘贴ToolStripMenuItem.Enabled = false;
        }

        #endregion

        #region 窗体初始化
        private void SetProxy_Load(object sender, EventArgs e)
        {
            try
            {
                string ImgFile;
                FilesINI ConfigINI = new FilesINI();
                ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
                this.BackgroundImage = Image.FromFile(ImgFile);
            }
            catch
            {

            }

            richTextBox1.ContextMenuStrip = contextMenuStrip1;
            文字编辑器V100BetaToolStripMenuItem.Enabled = false;

        }
        #endregion

        #region 用户交互
        private void button1_Click(object sender, EventArgs e)
        {
            Json_IP();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class2.IpConfig();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion
    }
}
