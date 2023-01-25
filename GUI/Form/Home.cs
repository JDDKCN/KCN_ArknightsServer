using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI;
using static GUI.Msg;
using KCNAPI;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SMSBoomGUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        #region 窗体边框阴影效果

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        public void LoginForm()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //API函数加载，实现窗体边框阴影效果
        }

        #endregion

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

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }
        #endregion

        #region 鼠标事件

        //窗体大小改变
        const int WM_NCHITTEST = 0x0084;
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
            }
        }

        //引用
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        //常量
        public const int WM_SYSCOMMAND = 0x0112;

        //窗体移动
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        //改变窗体大小
        public const int WMSZ_LEFT = 0xF001;
        public const int WMSZ_RIGHT = 0xF002;
        public const int WMSZ_TOP = 0xF003;
        public const int WMSZ_TOPLEFT = 0xF004;
        public const int WMSZ_TOPRIGHT = 0xF005;
        public const int WMSZ_BOTTOM = 0xF006;
        public const int WMSZ_BOTTOMLEFT = 0xF007;
        public const int WMSZ_BOTTOMRIGHT = 0xF008;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.BackColor = Color.SkyBlue;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox4.BackColor = Color.White;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.BackColor = Color.DarkGray;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_MouseDown_1(object sender, MouseEventArgs e)
        {
            pictureBox5.BackColor = Color.DarkGray;
        }

        private void pictureBox5_MouseLeave_1(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Transparent;
        }

        private void pictureBox5_MouseMove_1(object sender, MouseEventArgs e)
        {
            pictureBox5.BackColor = Color.White;
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox6.BackColor = Color.SkyBlue;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox6.BackColor = Color.DarkGray;
        }

        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox10.BackColor = Color.SkyBlue;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.BackColor = Color.Transparent;
        }

        private void pictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox10.BackColor = Color.DarkGray;
        }

        private void pictureBox11_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox11.BackColor = Color.SkyBlue;
        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.BackColor = Color.Transparent;
        }

        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox11.BackColor = Color.DarkGray;
        }

        #endregion

        #region 绘制程序Panel
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

        private void Home_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region 窗体初始化

        string ImgFile;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoginForm();//加载程序外阴影
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);//限制最大化窗体大小
            this.MinimumSize = new Size(this.Width, this.Height);//窗体改变大小时最小限定在初始化大小
            try
            {
                FilesINI ConfigINI = new FilesINI();
                ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
                this.BackgroundImage = Image.FromFile(ImgFile);
            }
            catch (Exception ex)
            {
               GUI.Msg.MsgShow("未能加载背景图片: \n" + ex.Message, "Error - 资源加载失败", true);
            }
        }

        #endregion

        #region 用户交互
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/475547854/");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                Class2.MySQL("false");
                Class2.Server();
            }
            catch (Exception ex)
            {
                GUI.Msg.MsgShow("启动失败！ \n" + ex.Message, "Error - 启动失败", true);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                Class2.Stop();
            }
            catch (Exception ex)
            {
                GUI.Msg.MsgShow("启动失败！ \n" + ex.Message, "Error - 启动失败", true);
            }

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

         }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Setting frm = new Setting();
            frm.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            try
            {
                Class2.MySQL("");
            }
            catch (Exception ex)
            {
                GUI.Msg.MsgShow("启动失败！ \n" + ex.Message, "Error - 启动失败", true);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            try
            {
                Class2.Server("");
            }
            catch (Exception ex)
            {
                GUI.Msg.MsgShow("启动失败！ \n" + ex.Message, "Error - 启动失败", true);
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            TextEditor frm = new TextEditor();
            frm.ShowDialog();
        }

        #endregion

    }
}
