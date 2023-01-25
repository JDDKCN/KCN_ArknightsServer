using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMSBoomGUI;
using static GUI.Msg;
using KCNAPI;


namespace GUI
{
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }

        #region 绘制程序Panel

        private void panel1_Paint_1(object sender, PaintEventArgs e)
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

        #endregion

        #region 窗体Load
        private void UserControl1_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromArgb(0, 167, 220, 224);//ARGB，第一个为调节不透明度
            //this.AutoScrollMinSize = new Size(ClientRectangle.Width, ClientRectangle.Height);
            Setting pform = this.ParentForm as Setting;
            pform.pictureBox4.BackColor = Color.SkyBlue;
            Json_Rad();
        }
        #endregion

        #region Json解析
        private void Json_Rad()
        {
            try
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

                richEdit502.Text = (string)jobject["server"]["url"];
                richEdit501.Text = (string)jobject["server"]["http"];
                richEdit503.Text = (string)jobject["server"]["https"];
                richEdit504.Text = (string)jobject["server"]["captcha"];
                richEdit505.Text = (string)jobject["server"]["enableServer"];
                richEdit506.Text = (string)jobject["server"]["closeMessage"];
                richEdit507.Text = (string)jobject["server"]["GMKey"];

                richEdit508.Text = (string)jobject["database"]["host"];
                richEdit509.Text = (string)jobject["database"]["port"];
                richEdit5010.Text = (string)jobject["database"]["dbname"];
                richEdit5011.Text = (string)jobject["database"]["username"];
                richEdit5012.Text = (string)jobject["database"]["password"];
                richEdit5013.Text = (string)jobject["database"]["extra"];

                richEdit5014.Text = (string)jobject["shop"]["enableDiamondShop"];

            }
            catch (System.Exception ex)
            {
                GUI.Msg.MsgShow("程序出错！\n" + ex.Message, "Error", true);
            }

        }

        private void Json_Wri(string Pa1, string Pa2, string Na1)
        {

            //json读取
            StreamReader reader = File.OpenText(".\\config.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            string ArkServerPath = jsonObject["ArkServerPath"].ToString();
            reader.Close();

            //json写入
            string JsonPath = ArkServerPath + "\\config.json";
            string JsonString = File.ReadAllText(JsonPath, Encoding.UTF8);
            JObject jobject = JObject.Parse(JsonString);
            jobject[Pa1][Pa2] = Na1;
            string convertString = Convert.ToString(jobject);
            File.WriteAllText(JsonPath, convertString);

        }

        private void All()
        {
            Json_Wri("server", "url", "http://192.168.1.1:38660");
            Json_Wri("server", "http", "38660");
            Json_Wri("server", "https", "38650");
            Json_Wri("server", "captcha", "false");
            Json_Wri("server", "enableServer", "true");
            Json_Wri("server", "closeMessage", "服务器目前暂未开放");
            Json_Wri("server", "GMKey", "$Ts!7!O@#$KpV%~~3K~%%3O8E~0O#B36J2945OW!J/%t@Fw73V7J%%B$AK!%Rs4h");
            Json_Wri("database", "host", "localhost");
            Json_Wri("database", "port", "62814");
            Json_Wri("database", "dbname", "arknights");
            Json_Wri("database", "username", "root");
            Json_Wri("database", "password", "Arknights");
            Json_Wri("database", "extra", "useSSL=false&useUnicode=true&characterEncoding=utf-8&autoReconnect=true&allowMultiQueries=true&allowPublicKeyRetrieval=true&serverTimezone=Asia/Shanghai");
            Json_Wri("shop", "enableDiamondShop", "True");
            Json_Rad();
        }

        #endregion

        #region 用户交互
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "url", richEdit502.Text);
            Json_Rad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "http", richEdit501.Text);
            Json_Rad();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "https", richEdit503.Text);
            Json_Rad();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "captcha", richEdit504.Text);
            Json_Rad();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "enableServer", richEdit505.Text);
            Json_Rad();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "closeMessage", richEdit506.Text);
            Json_Rad();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Json_Wri("server", "GMKey", richEdit507.Text);
            Json_Rad();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Json_Wri("database", "host", richEdit508.Text);
            Json_Rad();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Json_Wri("database", "port", richEdit509.Text);
            Json_Rad();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Json_Wri("database", "dbname", richEdit5010.Text);
            Json_Rad();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Json_Wri("database", "username", richEdit5011.Text);
            Json_Rad();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Json_Wri("database", "password", richEdit5012.Text);
            Json_Rad();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Json_Wri("database", "extra", richEdit5013.Text);
            Json_Rad();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            Json_Wri("shop", "enableDiamondShop", richEdit5014.Text);
            Json_Rad();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            var ret = MsgShow("恢复默认config？\n若误编辑了config文件，请点击确定还原。", "还原配置文件 - 提示", true);
            if (ret)
                All();
            else
                return;

        }

        private void button16_Click(object sender, EventArgs e)
        {
            var ret = MsgShow("您真的要删除数据库文件吗？\n数据无价，谨慎操作。请点击确定删除。", "删库 - 提示", true);
            if (ret)
                Class2.DelData();
            else
                return;

        }

        #endregion
    }
}
