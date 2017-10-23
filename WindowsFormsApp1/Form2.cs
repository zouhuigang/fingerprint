using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using Sample;
using System.Data.OleDb;
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        LocalDb ldb = null;//本地db库连接
        IntPtr mDBHandle = IntPtr.Zero; //本地数据库句柄
        IntPtr Form2Handle = IntPtr.Zero; //win窗口句柄

        bool IsRegister = false;//是否开始注册


        public Form2()
        {
            InitializeComponent();
        }
        const int MESSAGE_CAPTURED_OK = 0x0400 + 201;//消息统一采用4位16进制的数

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);



        //form2的析构函数
        private void init(object sender, EventArgs e)
        {
            Form2Handle = this.Handle;
            //初始化数据库
            ldb = new LocalDb();
            ldb.ConnLocalDb();

            //指纹登记初始化
            this.toolStripStatusLabel1.Text = "请将手指放在指纹感应器上!";



        }



        //键盘只能输入数字
        private void textBox3_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            //阻止从键盘输入键

            e.Handled = true;

            if ((e.KeyChar >= '0' && e.KeyChar <= '9')||(e.KeyChar == 8)){
                e.Handled = false;

            }

        }


        //得到form1中的图像信息
        public void SetImg(Bitmap bmp)
        {
            this.pictureBox1.Image = bmp;
        }

        //设置底部提示信息
        public void SetTips(string msg) {
            this.toolStripStatusLabel1.Text = msg;
        }

        //得到userid
        public int GetUserId() {
            string str = useridText.Text;
            if (str == null|| str.Trim().Equals("")) {
                str = "0";
            }
            int userid = int.Parse(str);

            return userid;
        }

        //得到学生真实姓名
        public string GetRealName() {
            string str = textBox1.Text;
            if (str == null || str.Trim().Equals(""))
            {
                str = "";
            }

            return str;
        }

        //得到手指的编号
        public int GetFingerIndex()
        {
            int fingerindex = 0;
            switch (comboBox1.Text.Trim())
            {
                case "左手小拇指": fingerindex = 0; break;
                case "左手无名指": fingerindex = 1; break;
                case "左手中指": fingerindex = 2; break;
                case "左手食指": fingerindex = 3; break;
                case "左手大拇指": fingerindex = 4; break;
                case "右手大拇指": fingerindex = 5; break;
                case "右手食指": fingerindex = 6; break;
                case "右手中指": fingerindex = 7; break;
                case "右手无名指": fingerindex = 8; break;
                case "右手小拇指": fingerindex = 9; break;
                default: fingerindex = 0; break;
            }

            return fingerindex;

        }


        //form2关闭前调用
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRegister = false;
        }

        public void SetIsRegister(bool s)
        {
             IsRegister = s;
        }
        public bool GetIsRegister()
        {
            return IsRegister;
        }


        private void Enrolled_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
