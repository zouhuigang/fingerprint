using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
//using ADOX;


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

        //创建mdb   
        /*public static bool CreateMDBDataBase(string mdbPath)
        {

            try

            {

                ADOX.CatalogClass cat = new ADOX.CatalogClass();

                cat.Create(
                "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + mdbPath + ";");

                cat = null;

                return true;

            }
            //C#操作Access之创建mdb库
            catch
            {

                return false;

            }

        }
        */

    


        /*插入数据库
         fingerindex:哪根手指
         remote_userid:远程数据库中的用户ID

             */
        public int AddFingerprint(int fingerid, int fingerindex,string template_10,string template_9,string realname) {
            //返回刚刚插入的id
            string query2 = "Select @@Identity";
            int Last_ID;

            cmd = new OleDbCommand("insert into template(userid,fingerindex,template_10,template_9,realname) values(@userid,@fingerindex,@template,@template1,@realname)", conn);
            ((OleDbParameter)cmd.Parameters.Add("@userid", OleDbType.Integer)).Value = fingerid;//fingerid
            ((OleDbParameter)cmd.Parameters.Add("@fingerindex", OleDbType.Integer)).Value = fingerindex;//fingerindex
            ((OleDbParameter)cmd.Parameters.Add("@template", OleDbType.BSTR)).Value = template_10;
            ((OleDbParameter)cmd.Parameters.Add("@template1", OleDbType.BSTR)).Value = template_9;
            ((OleDbParameter)cmd.Parameters.Add("@realname", OleDbType.BSTR)).Value = realname;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
                cmd.CommandText = query2;
                Last_ID = (int)cmd.ExecuteScalar();
            }
            catch (Exception zxz)
            {
                //MessageBox.Show(zxz.ToString(), "错误");
                conn.Close();
                return 0;
            }
            conn.Close();

            return Last_ID;
        }

        //删除数据库对应的id
        public bool DelFingerprint(string className,int audoid) {

            try
            {
                var where = "where autoid="+ audoid;
                var sql = string.Format("delete from {0} {1}", className,
                     where);

                OleDbDataAdapter da = new OleDbDataAdapter();

                da.DeleteCommand = new OleDbCommand(sql, conn);
                da.DeleteCommand.CommandText = sql;

                conn.Open();
                da.DeleteCommand.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
            finally
            {
                conn.Close();
            }


        }

        //得到一个指纹的具体信息
        public struct UserInfo
        {
            public int Autoid;
            public string Realname;
            public string userid;
            public string Fingerindex;
        }

        public UserInfo GetUserInfo(int autoid) {
            UserInfo p1 = new UserInfo();
            string _sql = "select realname,fingerindex,userid from template where autoid=" + autoid;
            OleDbDataReader reader = ReadList(_sql);
            while (reader.Read())
            {
                string t2 = reader[1].ToString();
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
                p1.Autoid = autoid;
                p1.Realname = reader[0].ToString();
                p1.Fingerindex = t2;
                p1.userid= reader[2].ToString();
            }

            
            return p1;
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
        public void Close()
        {
            conn.Close();
        }

        //end class
    }
}
