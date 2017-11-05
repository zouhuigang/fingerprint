using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libzkfpcsharp;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using Sample;
using System.Data.OleDb;
using System.Media;
using CCWin;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : CCSkinMain
    {
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool bIdentify = true;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        int cbRegTmp = 0; //内存中有多少条指纹
        private int mfpWidth = 0;
        private int mfpHeight = 0;

        LocalDb ldb = null;//本地db库连接
        Form2 frm2 = null;//登记指纹

        //易联云打印类
        printGPRS f = new printGPRS();
        http goPrint = new http();

        const int MESSAGE_CAPTURED_OK = 0x0400+200;//消息统一采用4位16进制的数

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);




        public Form1()
        {
            InitializeComponent();
        }

        //播放声音文件
        private void playVoice(string wavFileName) {
            SoundPlayer p = new SoundPlayer();
            p.SoundLocation = Application.StartupPath + "//voice//"+ wavFileName + ".wav";
            p.Load();
            p.Play();
        }

        //读取设备,识别多个设备,zk4500
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            //连接zk4500指纹仪
            int ret = zkfperrdef.ZKFP_ERR_OK;
            if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
            {
                int nCount = zkfp2.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        comboBox1.Items.Add(i.ToString());
                    }
                    comboBox1.SelectedIndex = 0;
                    textTips.Text = "连接设备数"+nCount+"\n";
                    conZk.Enabled = false;
                    connDevice.Enabled = true;
                    bnClose.Enabled = false;
                    button1.Enabled = false;

                }
                else
                {
                    zkfp2.Terminate();
                    MessageBox.Show("没有设备连接!");
                }
            }
            else
            {
                MessageBox.Show("连接指纹仪失败，返回状态码:" + ret + " !");
            }

            

        }

        //断开连接
        private void button1_Click_1(object sender, EventArgs e)
        {
            bIsTimeToDie = true;
            RegisterCount = 0;
            Thread.Sleep(1000);
            int CloseState=zkfp2.CloseDevice(mDevHandle);
            if (CloseState == 0)
            {
                textTips.Text = "关闭指纹仪成功！！" + CloseState;
                conZk.Enabled = false;
                connDevice.Enabled = true;
                bnClose.Enabled = false;
                button1.Enabled = false;

            }
            else {
                textTips.Text = "关闭指纹仪失败，错误码:" + CloseState;
            }

          
        }


        //录入指纹
        private void button_Click_EnrolledFingerprint(object sender, EventArgs e){
            /*if (!IsRegister)
            {
                IsRegister = true;
                RegisterCount = 0;
                cbRegTmp = 0;
                textTips.Text = "将手指放在指纹感应器上,使用同一手指的不同区块重复3次此操作，直到指纹成功录入";
            }*/
            //this.Hide();
            frm2 = new Form2();
            frm2.Show();
            frm2.SetIsRegister(true);
            playVoice("frist");


        }


        //指纹图像显示
        private void fingerprintImg_Click(object sender, EventArgs e)
        {
           
        }

        //返回指纹仪扫描到的图像，然后通过消息传给form表单
        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                   SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
                Thread.Sleep(200);
            }
        }

        //加载窗口前，Load运行此方法
        private void Form1_Load(object sender, EventArgs e)
        {
            FormHandle = this.Handle;

            //初始化数据库,通过微软的access创建数据库zxz_data.mdb
            //bool s= WindowsFormsApp1.LocalDb.CreateMDBDataBase("zxz_data.mdb");//创建数据库,第一次使用
            ldb = new LocalDb();
            ldb.ConnLocalDb();
            connDevice.Enabled = false;
            bnClose.Enabled = false;
            button1.Enabled = false;

            
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    {
                        MemoryStream ms = new MemoryStream();
                        BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                        Bitmap bmp = new Bitmap(ms);
                        bool IsRegister=frm2.GetIsRegister();
                        if (IsRegister)
                        {
                            frm2.SetImg(bmp);
                            textTips.Text = "开始注册";
                            if (IsRegister)
                            {
                                int ret = zkfp.ZKFP_ERR_OK;
                                int fid = 0, score = 0;
                                ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                                if (zkfp.ZKFP_ERR_OK == ret)
                                {
                                    frm2.SetTips("当前手指已录入成功 " + fid + "!");
                                    return;
                                }

                                int userid= frm2.GetUserId();
                                string realname= frm2.GetRealName();
                                if (userid==0 || realname=="") {
                                    MessageBox.Show("请先填写学生的ID和真实姓名，Id可以从排课系统后台看到!", "提示");
                                    return;
                                }

                                int fingerindex=frm2.GetFingerIndex();

                                if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmps[RegisterCount - 1]) <= 0)
                                {
                                    //将手指放在指纹感应器上,使用同一手指的不同区块重复此操作，直到指纹成功录入
                                    frm2.SetTips("使用同一手指的不同区块重复此操作");
                                    playVoice("try");
                                    return;
                                }

                                Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);
                                String strBase64 = zkfp2.BlobToBase64(CapTmp, cbCapTmp);
                                byte[] blob = zkfp2.Base64ToBlob(strBase64);
                                RegisterCount++;
                                if (RegisterCount >= REGISTER_FINGER_COUNT)
                                {
                                    RegisterCount = 0;
                                    if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBMerge(mDBHandle, RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref cbRegTmp)))
                                    {
                                        
                                        //插入数据到本地数据库
                                        int last_id  = ldb.AddFingerprint(userid, fingerindex, "", strBase64, realname);
                                        if (last_id!=0)
                                        {
                                            if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBAdd(mDBHandle, last_id, RegTmp))){
                                                frm2.SetTips("指纹存储成功！");
                                                playVoice("success");
                                                MessageBox.Show("指纹存储成功!", "提示");
                                            }
                                            else{
                                                var s=ldb.DelFingerprint("template", last_id);
                                                if (!s) {//再删除一遍
                                                    ldb.DelFingerprint("template", last_id);
                                                }
                                                frm2.SetTips("指纹录入失败02");
                                                playVoice("fail");
                                                MessageBox.Show("指纹录入失败02", "提示");
                                            }
                                        
                                        }
                                        else
                                        {
                                            frm2.SetTips("指纹存储失败！");
                                            playVoice("fail");
                                            MessageBox.Show("指纹存储失败!", "提示");
                                        }

                                    }
                                    else
                                    {
                                        frm2.SetTips("指纹录入失败 code=" + ret);
                                        playVoice("fail");
                                    }
                                    IsRegister = false;
                                    return;
                                }
                                else
                                {
                                    frm2.SetTips("还需按压" + (REGISTER_FINGER_COUNT - RegisterCount) + "次");
                                    playVoice("try");
                                }
                            }

                            return;
                        }
                        else
                        {
                            this.fingerprintImg.Image = bmp;
                            if (cbRegTmp <= 0)
                            {
                                textTips.Text = "指纹库指纹条数为0，请先录入指纹!";
                                playVoice("dd");
                                return;
                            }
                            if (bIdentify)
                            {//1:1识别
                                int ret = zkfp.ZKFP_ERR_OK;
                                int fid = 0, score = 0;
                                ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                                if (zkfp.ZKFP_ERR_OK == ret)
                                {
                                    var userInfo = ldb.GetUserInfo(fid);
                                    if (userInfo.Autoid==0) {
                                        textTips.Text = "当前指纹已删除！";
                                        return;
                                    }
                                    textTips.Text = "1:1指纹识别成功, 识别ID： " + fid + ",姓名:"+userInfo.Realname+ ",手指:" + userInfo.Fingerindex + ",识别分数:" + score + "!";
                                    
                                    //textTips.Text = f.SendGprsPrintContent("邹慧刚");c#直接发送请求给打印机
                                    string resultJson = goPrint.SendPrint(userInfo.userid);//请求golang服务器，简介发送打印请求，同时判断是否成功
                                    
                                    //解析json字符串
                                    var rs = JsonConvert.DeserializeObject<JsonAnooc>(resultJson);//result为上面的Json数据 
                                    if (rs.status == 200)
                                    {
                                        textTips.Text = "发送打印请求成功!返回信息:"+ resultJson;
                                        playVoice("line");
                                    }
                                    else {
                                        textTips.Text = "发送打印请求失败!返回信息:" + resultJson;
                                        playVoice("di");
                                    }

                                    return;
                                }
                                else
                                {
                                    textTips.Text = "1:1指纹识别失败, 错误: " + ret;
                                    return;
                                }
                            }
                            else
                            {//1:N识别
                                int ret = zkfp2.DBMatch(mDBHandle, CapTmp, RegTmp);
                                if (0 < ret)
                                {
                                    textTips.Text = "1:N指纹识别成功, score=" + ret + "!";
                                    playVoice("print");
                                    return;
                                }
                                else
                                {
                                    textTips.Text = "1:N指纹识别失败, ret= " + ret;
                                    return;
                                }
                            }
                        }
                    }
                    break;

                default:
                    base.DefWndProc(ref m);//程序初始化，会到这里
                    break;
            }
        }



        //连接设备
        private void connDevice_Click(object sender, EventArgs e)
        {

            //初始化表单2,因为用到了里面的IsRegister参数
            frm2 = new Form2();

            int ret = zkfp.ZKFP_ERR_OK;
            //打开设备
            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(comboBox1.SelectedIndex)))
            {
                textTips.Text = "打开设备失败\n";
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                textTips.Text = "数据库初始化失败\n"+ mDBHandle;
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }

            //清空占用的内存
            if ((ret = zkfp2.DBClear(mDBHandle)) != zkfperrdef.ZKFP_ERR_OK) {
                textTips.Text = "内存格式化失败\n" + mDBHandle;
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }


            RegisterCount = 0;
            cbRegTmp = 0;
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];

            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
            //string txt=zkfp2.SensorSN;

            //加载数据库到内存中
            int s= LocalDbToMemory();
            textTips.AppendText("连接指纹仪成功,读取到"+ s+"条指纹记录");
            conZk.Enabled = false;
            connDevice.Enabled = false;
            bnClose.Enabled = true;
            button1.Enabled = true;


        }

        


        //加载本地指纹库到内存中
        private int LocalDbToMemory(){
           
            string _sql = "select * from template where 1=1";
            OleDbDataReader reader = ldb.ReadList(_sql);
            while (reader.Read())
            {
                //判断template_9,base64字符串是否为空，如果为空就跳过
                if (reader[4] is DBNull)
                    continue;
                string t0 = reader[0].ToString();//1305531011,1508335369，autoid
                string t1 = reader[1].ToString();//122 ,userid //登记的ID
                string t2 = reader[2].ToString();//6 ,fingerindex,哪根手指
                string t3 = reader[3].ToString();//空 template_10,算法10得到的指纹图片
                string t4 = reader[4].ToString();//base64字符串  template_9 算法9得到的指纹图片

                //转换成可读
                int autoid = (int)reader[0];
                byte[] tmp = Convert.FromBase64String(t4);
                //axZKFPEngX1.AddRegTemplateToFPCacheDB(fpcHandle, autoid, tmp);
                zkfp2.DBAdd(mDBHandle, autoid, tmp);//将本地数据库的指纹，添加进内存中识别
                cbRegTmp++;
                //MessageBox.Show(t0 + "," + t1 + "," + t2 + "," + t3 + "," + t4 + "," + tmp, "提示");
            }

            ldb.Close();
        
            return cbRegTmp;



        }


        //form关闭
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int CloseState = zkfp2.CloseDevice(mDevHandle);
            if (CloseState == 0)
            {
                textTips.Text = "关闭指纹仪成功！！" + CloseState;
            }
            else
            {
                textTips.Text = "关闭指纹仪失败，错误码:" + CloseState;
            }
        }

        private void LocalDb_Click(object sender, EventArgs e)
        {
            FormLocalDbList frm3 = new FormLocalDbList();
            frm3.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /* this.linkLabel1.Links[this.linkLabel1.Links.IndexOf(e.Link)].Visited = true;
             string targetUrl = e.Link.LinkData as string;
             if (string.IsNullOrEmpty(targetUrl))
                 MessageBox.Show("没有链接地址！");
             else
                 System.Diagnostics.Process.Start("iexplore.exe", targetUrl);*/
            System.Diagnostics.Process.Start("https://www.anooc.com/admin/index");  //利用Process.Start来打开
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.anooc.com/edu/i-1");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.anooc.com/edu/teacher/index");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.anooc.com/");
        }
    }




}
