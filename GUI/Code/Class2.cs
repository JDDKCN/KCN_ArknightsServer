/*KCNAPI Arknights命令集解释
 * 
 * MySQL：运行数据库
 * Server：运行服务端 用法 Server("false"); //false为不显示命令行窗口
 * Stop：杀死所有服务
 * IpConfig：查看本机IP
 * DelData：删库
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static KCNAPI.Global;

namespace KCNAPI
{
    public class Class2
    {
        public static void Ver(string Path)
        {
            Console.Write("[Info]", Console.ForegroundColor = ConsoleColor.Green);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("dll库 V1.1.0beta 测试程序不代表正式品质");
            c.Log("启动 " + Path);
        }

        public static void Server(string Scr="")
        {
            string TempPath = System.IO.Path.GetTempPath();
            string ArkServer = "\\KCN\\ArkServer.cmd";//服务端运行脚本
            string str = System.Environment.CurrentDirectory;
            Directory.CreateDirectory(TempPath + "KCN");


            //json读取
            StreamReader reader = File.OpenText(".\\config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                string JsonPath = jsonObject["ArkServerPath"].ToString(); // 类似
                string JavaPath = jsonObject["JavaPath"].ToString(); // 类似
                reader.Close();
            //json写入
            string IPPath = JsonPath + "\\config.json";
            string JsonString = File.ReadAllText(IPPath, Encoding.UTF8);
            JObject jobject = JObject.Parse(JsonString);
           string IP = (string)jobject["server"]["url"];

            Ver("Ark服务端");

            StreamWriter sw = new StreamWriter(TempPath + ArkServer, false);

            sw.WriteLine("@echo off");
            sw.WriteLine("title Loading...");
            sw.WriteLine("%1 mshta vbscript:CreateObject(\"Shell.Application\").ShellExecute(\"cmd.exe\",\" / c % ~s0::\",\"\",\"runas\",1)(window.close)&&exit");
            sw.WriteLine("cd /d %~dp0");
            sw.WriteLine("chcp 65001");
            sw.WriteLine("cls");

            sw.WriteLine("@echo off");

            sw.WriteLine("echo Made By KCN");

            sw.WriteLine("SET NAME=ArkServer");

            sw.WriteLine("TITLE %NAME%");

            sw.WriteLine("REM COLOR C");

            sw.WriteLine("set mod=%1");

            sw.WriteLine("cd /d " + str + JsonPath);

            sw.WriteLine("set path=%path%;" + JavaPath);

            sw.WriteLine("echo 游戏服务器启动在: " + IP);

            sw.WriteLine("java -jar hypergryph-1.9.3.jar");

            sw.WriteLine("pause & exit");

            sw.Close();

            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + ArkServer);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + ArkServer;
                proc.StartInfo.Arguments = string.Format("10");
                if(Scr=="false")//设置DOS窗口不显示
                {
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                proc.Start();
            }
            catch (Exception ex)
            {
                c.Log(ex.Message);
                MessageBox.Show("出错了!\n"+ex, "Error");
                return;
            }

        }

        public static void MySQL(string Scr = "")
        {
            string TempPath = System.IO.Path.GetTempPath();
            string MySQL = "\\KCN\\MySQL.cmd";//服务端运行脚本
            string str = System.Environment.CurrentDirectory;
            Directory.CreateDirectory(TempPath + "KCN");

            //json读取
            StreamReader reader = File.OpenText(".\\config.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            string JsonPath = jsonObject["MySQLPath"].ToString(); // 类似
            reader.Close();

            Ver("Ark数据库");

            StreamWriter sw = new StreamWriter(TempPath + MySQL, false);
            sw.WriteLine("@echo off");
            sw.WriteLine("title Loading...");
            sw.WriteLine("%1 mshta vbscript:CreateObject(\"Shell.Application\").ShellExecute(\"cmd.exe\",\" / c % ~s0::\",\"\",\"runas\",1)(window.close)&&exit");
            sw.WriteLine("cd /d %~dp0");
            sw.WriteLine("chcp 65001");
            sw.WriteLine("cls");

            sw.WriteLine("@echo off");

            sw.WriteLine("echo Made By KCN");

            sw.WriteLine("SET NAME=MySQL");

            sw.WriteLine("TITLE %NAME%");

            sw.WriteLine("REM COLOR C");

            sw.WriteLine("set mod=%1");

            sw.WriteLine("cd /d " + str + JsonPath);

            sw.WriteLine("echo MySQL Running...");

            sw.WriteLine("bin\\mysqld.exe --defaults-file=\"my.ini\"");

            sw.WriteLine("pause & exit");

            sw.Close();

            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + MySQL);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + MySQL;
                proc.StartInfo.Arguments = string.Format("10");
                if (Scr == "false")//设置DOS窗口不显示
                {
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                proc.Start();
            }
            catch (Exception ex)
            {
                c.Log(ex.Message);
                MessageBox.Show("出错了!\n" + ex, "Error");
                return;
            }

        }

        public static void DelData(string Scr = "")
        {
            string TempPath = System.IO.Path.GetTempPath();
            string MySQL = "\\KCN\\MySQL.cmd";//服务端运行脚本
            string str = System.Environment.CurrentDirectory;
            Directory.CreateDirectory(TempPath + "KCN");

            //json读取
            StreamReader reader = File.OpenText(".\\config.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
            string JsonPath = jsonObject["MySQLPath"].ToString(); // 类似
            reader.Close();

            Ver("Ark数据库");

            StreamWriter sw = new StreamWriter(TempPath + MySQL, false);
            sw.WriteLine("@echo off");
            sw.WriteLine("title Loading...");
            sw.WriteLine("%1 mshta vbscript:CreateObject(\"Shell.Application\").ShellExecute(\"cmd.exe\",\" / c % ~s0::\",\"\",\"runas\",1)(window.close)&&exit");
            sw.WriteLine("cd /d %~dp0");
            sw.WriteLine("chcp 65001");
            sw.WriteLine("cls");

            sw.WriteLine("@echo off");

            sw.WriteLine("echo Made By KCN");

            sw.WriteLine("SET NAME=MySQL");

            sw.WriteLine("TITLE %NAME%");

            sw.WriteLine("REM COLOR C");

            sw.WriteLine("set mod=%1");

            sw.WriteLine("cd /d " + str + JsonPath);

            sw.WriteLine("echo Delete Database");

            sw.WriteLine("bin\\mysqld.exe DROP DATABASE [IF EXISTS] arknights");

            sw.WriteLine("echo OK...");

            sw.WriteLine("pause & exit");

            sw.Close();

            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + MySQL);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + MySQL;
                proc.StartInfo.Arguments = string.Format("10");
                if (Scr == "false")//设置DOS窗口不显示
                {
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                proc.Start();
            }
            catch (Exception ex)
            {
                c.Log(ex.Message);
                MessageBox.Show("出错了!\n" + ex, "Error");
                return;
            }

        }

        public static void Stop()
        {
            string TempPath = System.IO.Path.GetTempPath();
            string Close1 = "\\KCN\\ArkClose.cmd";//停止服务-1
            Directory.CreateDirectory(TempPath + "KCN");

            Ver("停止服务");
            StreamWriter sw = new StreamWriter(TempPath + Close1, false);

            sw.WriteLine("@echo off");
            sw.WriteLine("title Loading...");
            sw.WriteLine("%1 mshta vbscript:CreateObject(\"Shell.Application\").ShellExecute(\"cmd.exe\",\" / c % ~s0::\",\"\",\"runas\",1)(window.close)&&exit");
            sw.WriteLine("cd /d %~dp0");
            sw.WriteLine("chcp 65001");
            sw.WriteLine("cls");


            sw.WriteLine("@echo off");

            sw.WriteLine("echo Made By KCN");

            sw.WriteLine("SET NAME=停止服务");

            sw.WriteLine("TITLE %NAME%");

            sw.WriteLine("REM COLOR C");

            sw.WriteLine("set mod=%1");

            sw.WriteLine("echo 正在关闭服务...");

            sw.WriteLine("taskkill /f /im mysqld.exe");

            sw.WriteLine("choice /t 1 /d y /n >nul");

            sw.WriteLine("taskkill /f /im java.exe");

            sw.WriteLine("choice /t 1 /d y /n >nul");

            sw.WriteLine("taskkill /f /im cmd.exe");

            sw.WriteLine("mshta vbscript:msgbox(\"运行完毕。\",64,\"提示\")(window.close)");

            sw.Close();

            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + Close1);//this is where testChange.bat lies
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + Close1;
                proc.StartInfo.Arguments = string.Format("10");//this is argument
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                c.Log(ex.Message);
                MessageBox.Show("出错了!\n "+ex, "Error");
                return;
            }

        }
        /// <summary>
        /// 查看本机IP，调用CMD
        /// </summary>
        public static void IpConfig()
        {
            string TempPath = System.IO.Path.GetTempPath();
            string Close1 = "\\KCN\\IpConfig.cmd";//停止服务-1
            Directory.CreateDirectory(TempPath + "KCN");

            Ver("停止服务");
            StreamWriter sw = new StreamWriter(TempPath + Close1, false);

            sw.WriteLine("@echo off");

            sw.WriteLine("echo Made By KCN");

            sw.WriteLine("SET NAME=IPConfig");

            sw.WriteLine("TITLE %NAME%");

            sw.WriteLine("REM COLOR C");

            sw.WriteLine("set mod=%1");

            sw.WriteLine("ipconfig");

            sw.WriteLine("pause&exit");

            sw.Close();

            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + Close1);//this is where testChange.bat lies
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + Close1;
                proc.StartInfo.Arguments = string.Format("10");//this is argument
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                c.Log(ex.Message);
                MessageBox.Show("出错了!\n " + ex, "Error");
                return;
            }

        }


    }
}
