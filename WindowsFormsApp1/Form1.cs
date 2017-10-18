﻿using System;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        bool bIdentify = true;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        int cbRegTmp = 0;
        int iFid = 1;
        private int mfpWidth = 0;
        private int mfpHeight = 0;

        LocalDb ldb = null;//本地db库连接

        const int MESSAGE_CAPTURED_OK = 0x0400+200;//消息统一采用4位16进制的数

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);




        public Form1()
        {
            InitializeComponent();
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

        }


        //录入指纹
        private void button_Click_EnrolledFingerprint(object sender, EventArgs e){
            if (!IsRegister)
            {
                IsRegister = true;
                RegisterCount = 0;
                cbRegTmp = 0;
                textTips.Text = "将手指放在指纹感应器上,使用同一手指的不同区块重复3次此操作，直到指纹成功录入";
            }
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

            //初始化数据库
            ldb= new LocalDb();
            ldb.ConnLocalDb();
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
                        this.fingerprintImg.Image = bmp;
                        if (IsRegister)
                        {
                            int ret = zkfp.ZKFP_ERR_OK;
                            int fid = 0, score = 0;
                            ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                            if (zkfp.ZKFP_ERR_OK == ret)
                            {
                                textTips.Text = "当前手指已录入成功 " + fid + "!";
                                return;
                            }
                            if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmps[RegisterCount - 1]) <= 0)
                            {
                                textTips.Text = "将手指放在指纹感应器上,使用同一手指的不同区块重复此操作，直到指纹成功录入";
                                return;
                            }
                            Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);
                            String strBase64 = zkfp2.BlobToBase64(CapTmp, cbCapTmp);
                            byte[] blob = zkfp2.Base64ToBlob(strBase64);
                            RegisterCount++;
                            if (RegisterCount >= REGISTER_FINGER_COUNT)
                            {
                                RegisterCount = 0;
                                if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBMerge(mDBHandle, RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref cbRegTmp)) &&
                                       zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBAdd(mDBHandle, iFid, RegTmp)))
                                {
                                    iFid++;
                                    textTips.Text = "指纹录入成功！";
                                }
                                else
                                {
                                    textTips.Text = "指纹录入失败 code=" + ret;
                                }
                                IsRegister = false;
                                return;
                            }
                            else
                            {
                                textTips.Text = "还需按压" + (REGISTER_FINGER_COUNT - RegisterCount) + "次";
                            }
                        }
                        else
                        {
                            if (cbRegTmp <= 0)
                            {
                                textTips.Text = "使用前，请录入您的指纹!";
                                return;
                            }
                            if (bIdentify)
                            {
                                int ret = zkfp.ZKFP_ERR_OK;
                                int fid = 0, score = 0;
                                ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                                if (zkfp.ZKFP_ERR_OK == ret)
                                {
                                    textTips.Text = "指纹识别成功, 识别ID： " + fid + ",识别分数:" + score + "!";
                                    return;
                                }
                                else
                                {
                                    textTips.Text = "指纹识别失败, 错误: " + ret;
                                    return;
                                }
                            }
                            else
                            {
                                int ret = zkfp2.DBMatch(mDBHandle, CapTmp, RegTmp);
                                if (0 < ret)
                                {
                                    textTips.Text = "指纹识别成功, score=" + ret + "!";
                                    return;
                                }
                                else
                                {
                                    textTips.Text = "指纹识别失败, ret= " + ret;
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

            int ret = zkfp.ZKFP_ERR_OK;
            //打开设备
            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(comboBox1.SelectedIndex)))
            {
                textTips.Text = "打开设备失败\n";
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                textTips.Text = "数据库初始化失败\n";
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }


            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;
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
            textTips.AppendText("连接指纹仪成功！！");

        }


        //测试本地数据库
        private void LocalDb_Click(object sender, EventArgs e)
        {
            int fingerid = 123;
            int fingerindex = 123;

            string _sql = "select * from template where 1=1";
            OleDbDataReader reader = ldb.ReadList(_sql);
            while (reader.Read())
            {
                //判断base64字符串是否为空，如果为空就跳过
                if (reader[1] is DBNull)
                    continue;
                string t0 = reader[0].ToString();//1305531011,1508335369，autoid
                string t1 = reader[1].ToString();//122 ,userid
                string t2 = reader[2].ToString();//6 ,fingerindex
                string t3 = reader[3].ToString();//空 template_10
                string t4 = reader[4].ToString();//base64字符串  template_9

                //转换成可读
                int autoid = (int)reader[0];
                byte[] tmp = Convert.FromBase64String(t4);
                //axZKFPEngX1.AddRegTemplateToFPCacheDB(fpcHandle, autoid, tmp);
                MessageBox.Show(t0 + "," + t1 + "," + t2 + "," + t3 + "," + t4 + "," + tmp, "提示");
            }
            //插入数据
            ldb.AddFingerprint();

            MessageBox.Show("zaiz", "提示");
        

         
            return;



        }
    }




}