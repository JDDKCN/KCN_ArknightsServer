using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMSBoomGUI;

namespace GUI
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        #region 绘制程序Panel

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel1.ClientRectangle,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel2.ClientRectangle,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid);

        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            groupBox1.BackColor = Color.FromArgb(100, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                            groupBox1.ClientRectangle,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid);

        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            groupBox2.BackColor = Color.FromArgb(100, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                            groupBox2.ClientRectangle,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid);


        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
           // label6.BackColor = Color.FromArgb(100, 167, 220, 224);//ARGB，第一个为调节不透明度

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            panel4.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel4.ClientRectangle,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid);

        }

        #endregion

        #region 窗体Load
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(100, 0, 0, 0);//ARGB，第一个为调节不透明度

            FilesINI ConfigINI = new FilesINI();
            string ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
            string ImgFile2 = ConfigINI.INIRead("Image", "InfoFile", ".\\skin\\info.ini");
            richEdit501.Text = ImgFile2;
            Json_IP();

            richEdit502.Text = ImgFile;
            Setting pform = this.ParentForm as Setting;
            pform.pictureBox4.BackColor = Color.SkyBlue;
        }
        #endregion

        #region 自定义程序背景
        private void BgSet(string Path)
        {
            //打开文件
            OpenFileDialog ofd = new OpenFileDialog();      //声明打开文件
            ofd.Title = "请选择作为背景的图片";
            ofd.Filter = "图片文件(*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)  //窗体打开成功
            {
                try
                {
                    string FilePath = ofd.FileName;
                    FilesINI ConfigINI = new FilesINI();
                    ConfigINI.INIWrite("Image", Path, FilePath, ".\\skin\\info.ini");
                    //this.BackgroundImage = Image.FromFile(ImgFile);
                    this.BackgroundImage = Image.FromFile(FilePath);
                    GUI.Msg.MsgShow("设置成功！ \n", "提示", true);
                }
                catch (System.Exception ex)
                {
                    GUI.Msg.MsgShow("程序出错！\n" + ex.Message, "Error", true);
                }
            }

        }
        #endregion

        #region Json解析
        private void Json_IP()
        {
            //json读取1
            StreamReader reader = File.OpenText(".\\config.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            string ArkServerPath = jsonObject["ArkServerPath"].ToString(); // 类似
            reader.Close();

            //json读取2
            string JsonPath = ArkServerPath + "\\config.json";
            string JsonString = File.ReadAllText(JsonPath, Encoding.UTF8);
            JObject jobject = JObject.Parse(JsonString);
            richEdit504.Text = (string)jobject["server"]["url"];
        }

        #endregion

        #region 用户交互
        private void button4_Click(object sender, EventArgs e)
        {
            TextEditor frm = new TextEditor();
            frm.ShowDialog();
            Json_IP();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BgSet("BgFile");
            FilesINI ConfigINI = new FilesINI();
            string ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
            richEdit502.Text = ImgFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BgSet("InfoFile");
            FilesINI ConfigINI = new FilesINI();
            string ImgFile = ConfigINI.INIRead("Image", "InfoFile", ".\\skin\\info.ini");
            richEdit501.Text = ImgFile;

        }

        #endregion
    }
}
