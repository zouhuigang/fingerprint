using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.OleDb;


namespace WindowsFormsApp1
{
    class LocalDb
    {
        //初始化变量
        private OleDbConnection conn = null;
        private OleDbCommand cmd = null;
        private OleDbDataReader reader = null;

        //定义返回类型

        //连接本地库
        public void ConnLocalDb() {
            //初始化数据库
            string connstr = ConfigurationManager.ConnectionStrings["zxz"].ConnectionString;
            conn = new OleDbConnection(connstr);


        }


        //插入数据库
        public Boolean AddFingerprint() {
            cmd = new OleDbCommand("insert into template(userid,fingerindex,template_10,template_9) values(@userid,@fingerindex,@template,@template1)", conn);
            ((OleDbParameter)cmd.Parameters.Add("@userid", OleDbType.Integer)).Value = 2111;//fingerid
            ((OleDbParameter)cmd.Parameters.Add("@fingerindex", OleDbType.Integer)).Value = 2222;//fingerindex
            ((OleDbParameter)cmd.Parameters.Add("@template", OleDbType.BSTR)).Value = "2333";
            ((OleDbParameter)cmd.Parameters.Add("@template1", OleDbType.BSTR)).Value = "2444";

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception zxz)
            {
                //MessageBox.Show(zxz.ToString(), "错误");
                conn.Close();
                return false;
            }
            conn.Close();

            return true;
        }


        public OleDbDataReader ReadList(string SQL) {
            cmd = new OleDbCommand(SQL, conn);
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            OleDbDataReader Rs = cmd.ExecuteReader();
            return Rs;
        }

        //关闭数据库连接
        public void close()
        {
            conn.Close();
        }

        //end class
    }
}
