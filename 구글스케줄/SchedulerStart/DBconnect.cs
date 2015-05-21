using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Windows;

namespace SchedulerStart
{
    class DBconnect
    {
        static string connectstring = "Server=localhost;Database=Idpass; Uid=root; Pwd=guswlsdl";
        /// <summary>
        ///  insert DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pass"></param>
        public void inputDB(string id, string color, string username, string image)
        {

            DataTable dt = new DataTable();
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "Select * from accountinfo";

            MySqlDataReader read = com.ExecuteReader();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;
            dt.Load(read);

            foreach (DataRow dr in dt.Rows)
            {
                //dr[2]<-color 필드
                if (dr[1].ToString() == color)
                {
                    MessageBox.Show("이미 색상이 존재합니다.");
                    return;
                }
            }

            com.CommandText = "insert into accountinfo (id,color,username,image) value('" + id.Replace("\r", "").Replace("\n", "") + "', '" + color.Replace("\r", "").Replace("\n", "") + "','" + username.Replace("\r", "").Replace("\n", "") + "','" + image + "')";
            try
            {
                com.ExecuteNonQuery();
                MessageBox.Show("등록되었습니다.");
            }
            catch
            {
                MessageBox.Show("등록에 실패하였습니다.");
            }

            myconnect.Close();

        }
        /// <summary>
        /// Select DB
        /// </summary>
        /// <returns>Account Datatable</returns>
        public static DataTable loadDB()
        {
            DataTable dt = new DataTable();
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "Select * from accountinfo";
            
            MySqlDataReader read = com.ExecuteReader();
            
            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;

            dt.Load(read);

            myconnect.Close();

            


            return dt;
        }

        public Boolean SearchDB(string user_id, string index, string username)
        {
            DataTable dt = new DataTable();
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "Select * from accountinfo";

            MySqlDataReader read = com.ExecuteReader();
            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;
            dt.Load(read);

            foreach (DataRow dr in dt.Rows)
            {
                //dr[2]<-color 필드
                if (dr[0].ToString() == user_id)
                {
                    MessageBox.Show("이미 존재하는 아이디입니다.");
                    return false;
                }
                if (dr[1].ToString() == index)
                {
                    MessageBox.Show("이미 존재하는 색상입니다.");
                    return false;
                }
                if (dr[2].ToString() == username)
                {
                    MessageBox.Show("이미 존재하는 UserName 입니다.");
                    return false;
                }   
            }
            return true;
        }
        public static int loadStyle(string eventEmail)
        {

            int styleindex = 0;
            DataTable dt = new DataTable();
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "SELECT * FROM accountinfo WHERE id='" + eventEmail + "'";

            
            MySqlDataReader read = com.ExecuteReader();

            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;


            dt.Load(read);
            try
            {
                styleindex = int.Parse(dt.Rows[0][1].ToString());
            }
            catch { }
            myconnect.Close();

            return styleindex;


        }

        public static string loadUserName(string eventEmail)
        {
            string userName = "";
            DataTable dt = new DataTable();
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "SELECT * FROM accountinfo WHERE id='" + eventEmail + "'";


            MySqlDataReader read = com.ExecuteReader();

            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;


            dt.Load(read);
            try
            {
                userName = dt.Rows[0][2].ToString();
            }
            catch { }
            myconnect.Close();

            return userName;
        }

        public static string loadUserColorindex(string username)
        {
            string index="";
            DataTable dt = new DataTable();
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "SELECT * FROM accountinfo WHERE username='" + username + "'";


            MySqlDataReader read = com.ExecuteReader();

            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;


            dt.Load(read);
            try
            {
                index = dt.Rows[0][0].ToString();
            }
            catch { }
            myconnect.Close();

            return index;
        }

        public static List<int> UsedColorindex()
        {
            DataTable dt = new DataTable();
            List<int> indexlist = new List<int>();
            
            MySqlConnection myconnect = new MySqlConnection(connectstring);
            myconnect.Open();

            MySqlCommand com = new MySqlCommand();
            com.Connection = myconnect;

            com.CommandText = "Select * from accountinfo";
            MySqlDataReader read = com.ExecuteReader();

            DataSet dataset = new DataSet();
            dataset.Tables.Add(dt);
            dataset.EnforceConstraints = false;


            dt.Load(read);

            foreach(DataRow row in dt.Rows)
            {
                indexlist.Add(int.Parse(row["color"].ToString()));
            }
            myconnect.Close();

            return indexlist;
        }

    }
}
