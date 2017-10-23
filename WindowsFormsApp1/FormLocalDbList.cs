using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Sample;
using System.IO;
//using libzkfpcsharp;

namespace WindowsFormsApp1
{
    public partial class FormLocalDbList : Form
    {

        LocalDb ldb = null;//本地db库连接

        public FormLocalDbList()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           

            //初始化数据库
            ldb = new LocalDb();
            ldb.ConnLocalDb();

            //初始化数据
            GetList();

        }

       


        private void GetList() {

            //构建表头
            this.listView1.Clear();
            this.listView1.Columns.Add("指纹编号", 120, HorizontalAlignment.Left);
            this.listView1.Columns.Add("用户编号", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("真实姓名", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("手指编号", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("template_10", 80, HorizontalAlignment.Left);
            this.listView1.Columns.Add("template_9", 80, HorizontalAlignment.Left);

            /*
             // The View property must be set to Details for the 
	// subitems to be visible.
	myListView.View = View.Details;
    加了下面这句，才能够看到
             */
            this.listView1.View = System.Windows.Forms.View.Details;//SmallIcon

            this.listView1.LabelEdit = true;              //允许用户编辑文本项。
            this.listView1.GridLines = true;              //显示行列的网格线。
            this.listView1.FullRowSelect = true;//选择一整行,请将ListView 的 FullRowSelect 属性设置为true

            string _sql = "select * from template where 1=1";
            OleDbDataReader reader = ldb.ReadList(_sql);


            //解析出图片，存入ImageList中
            //ImageList imageListSmall = new ImageList();   //产生图像对象
             //imageListSmall.ImageSize = new Size(200, 200);
             //imageListSmall.Images.Clear();
       

            int i = 0;
            while (reader.Read())
            {
                //判断template_9,base64字符串是否为空，如果为空就跳过
                if (reader[4] is DBNull)
                    continue;
                string t0 = reader[0].ToString();//1305531011,1508335369，autoid
                string t1 = reader[1].ToString();//122 ,userid //登记的ID,reader["code"].ToString();
                string t2 = reader[2].ToString();//6 ,fingerindex,哪根手指
                string t3 = reader[3].ToString();//空 template_10,算法10得到的指纹图片
                string t4 = reader[4].ToString();//base64字符串  template_9 算法9得到的指纹图片
                string t5 = reader[5].ToString();//真实姓名
                //转换成可读
                int autoid = (int)reader[0];


             
                switch (t2.Trim())
                {
                    case "0": t2 = "左手小拇指"; break;
                    case "1": t2 = "左手无名指"; break;
                    case "2": t2 = "左手中指"; break;
                    case "3": t2 = "左手食指"; break;
                    case "4": t2 = "左手大拇指"; break;
                    case "5": t2 = "右手大拇指"; break;
                    case "6": t2 = "右手食指"; break;
                    case "7": t2 = "右手中指"; break;
                    case "8": t2 = "右手无名指"; break;
                    case "9": t2 = "右手小拇指"; break;
                    default: t2 = "左手小拇指"; break;
                }



                //listView中的ImageList追加成图片
                /*byte[] tmp = Convert.FromBase64String(t4);
                //byte[] tmp = zkfp2.Base64ToBlob(t4);
                MemoryStream ms = new MemoryStream();
                BitmapFormat.GetBitmap(tmp, 100, 100, ref ms);
                Bitmap bmp = new Bitmap(ms);
                this.imageList1.Images.Add(bmp);*/



                //构建一个ListView的数据，存入数据库数据，以便添加到listView1的行数据中
                ListViewItem lt = new ListViewItem();//一行数据
                lt.ImageIndex = 0;//0就是imagelist的第一个图片，以此类推
                lt.Text = t0;
                lt.SubItems.Add(t1);   //lvi.SubItems.Add("第2列,第"+i+"行");
                lt.SubItems.Add(t5);
                lt.SubItems.Add(t2);
                lt.SubItems.Add(t3);
                lt.SubItems.Add(t4);
                this.listView1.Items.Add(lt);



                i++;

                



            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

            ldb.Close();

        }

        //双击复制
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView listview = (ListView)sender;
            ListViewItem lstrow = listview.GetItemAt(e.X, e.Y);
            System.Windows.Forms.ListViewItem.ListViewSubItem lstcol = lstrow.GetSubItemAt(e.X, e.Y);
            string strText = lstcol.Text;
            try
            {
                Clipboard.SetDataObject(strText);
                string info = string.Format("内容【{0}】已经复制到剪贴板", strText);
                MessageBox.Show(info, "提示");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //删除当前指纹
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                string autoid = listView1.SelectedItems[0].Text;
                //string type = listView1.SelectedItems[0].SubItems[1].Text;
                if (MessageBox.Show("确定删除该指纹" + autoid + "？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var s = ldb.DelFingerprint("template", int.Parse(autoid));
                    if (s)
                    {
                        MessageBox.Show("删除成功", "提示");
                        this.listView1.Items.Remove(this.listView1.SelectedItems[0]);

                    }
                    else {
                        MessageBox.Show("删除失败", "提示");

                    }

                    
                }

            }


            return;

            



        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            else
            {
                //复制信息
                string t0 = listView1.SelectedItems[0].Text;
                string t1 = listView1.SelectedItems[0].SubItems[1].Text;
                string t2 = listView1.SelectedItems[0].SubItems[2].Text;
                string t3 = listView1.SelectedItems[0].SubItems[3].Text;
                string t4 = listView1.SelectedItems[0].SubItems[4].Text;

                string strText = t0+","+t1 + "," + t2 + "," + t3 + "," + t4;
                try
                {
                    Clipboard.SetDataObject(strText);
                    string info = string.Format("内容【{0}】已经复制到剪贴板", strText);
                    MessageBox.Show(info, "提示");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }

            
        }
    }
}
