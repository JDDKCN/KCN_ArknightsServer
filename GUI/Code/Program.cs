using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static GUI.Msg;

namespace SMSBoomGUI
{
    internal static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string TempPath = System.IO.Path.GetTempPath();
            Directory.CreateDirectory(TempPath + "KCN");

            string path = ".\\skin";
            if (Directory.Exists(path)) 
            {
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            string INIPath = ".\\skin\\info.ini";
            if (!File.Exists(INIPath))
            {
                StreamWriter sw = new StreamWriter(INIPath, false);
                sw.WriteLine("[Image]");
                sw.WriteLine("BgFile="+ ".\\skin\\bg.jpg");
                sw.WriteLine("InfoFile=" + ".\\skin\\info.jpg");
                sw.WriteLine("TEFile=" + ".\\skin\\bg.jpg");
                sw.Close();
            }

            //声明互斥体 使程序只能启动一个
            Mutex mutex = new Mutex(false, "KCNGUI");
            //判断互斥体是否在使用中
            bool Runing = !mutex.WaitOne(0, false);
            if (!Runing)
            {
                //json读取
                StreamReader reader = File.OpenText(".\\config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                string ArkServerPath = jsonObject["ArkServerPath"].ToString();
                string JavaPath = jsonObject["JavaPath"].ToString(); 
                string MySQLPath = jsonObject["MySQLPath"].ToString(); 
                reader.Close();

                if (!File.Exists(ArkServerPath + "\\hypergryph-1.9.3.jar"))
                {
                    var ret = MsgShow("未找到服务端文件！！\n若文件丢失，请尝试重新安装。", "Error - 文件缺失", true);
                    if (ret)
                        Application.Exit();
                    else
                        Application.Exit();
                    return;
                }

                if (!File.Exists(MySQLPath + "\\bin\\mysqld.exe"))
                {
                    var ret = MsgShow("未找到数据库主程序！！\n若文件丢失，请尝试重新安装。", "Error - 文件缺失", true);
                    if (ret)
                        Application.Exit();
                    else
                        Application.Exit();
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Home());
            }
            else
            {
                MsgShow("已经有一个程序在运行！", "Error", true);
                Application.Exit();
                return;
            }
        }
    }
}
