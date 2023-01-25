using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMSBoomGUI;

namespace GUI
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }


        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            groupBox1.BackColor = Color.FromArgb(100, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                            groupBox1.ClientRectangle,
                            Color.White,
                            2,//左
                            ButtonBorderStyle.Solid,
                            Color.White,
                            0,//上
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,//右
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,//下
                            ButtonBorderStyle.Solid);

        }


        private void label6_Paint(object sender, PaintEventArgs e)
        {
            //label6.BackColor = Color.FromArgb(100, 167, 220, 224);//ARGB，第一个为调节不透明度

        }


        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(100, 0, 0, 0);//ARGB，第一个为调节不透明度

            Setting pform = this.ParentForm as Setting;
            pform.pictureBox4.BackColor = Color.SkyBlue;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/475547854/");
        }
    }
}
