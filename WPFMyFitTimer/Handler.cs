using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMyFitTimer
{
    class Handler
    {
        private string connectionString;
        private SqlConnection con;
        public Handler()
        {
            connectionString = Properties.Settings.Default.connectionString;
        }

        public void OpenConnection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public void InsertDB(string cmd)
        {
            new SqlCommand(cmd, con).ExecuteNonQuery();
        }

        public List<string> QueryDB(string cmd)
        {
            List<string> newList = new List<string>();
            using(SqlDataReader reader =  new SqlCommand(cmd, con).ExecuteReader())
            {
                while (reader.Read())
                {
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        newList.Add(reader.GetValue(i).ToString());
                    }
                }
            }

            return newList;
        } 
    }
}
