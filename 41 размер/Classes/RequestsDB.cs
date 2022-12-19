using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace _41_размер.Classes
{
    class RequestsDB
    {
        static string StrCon = "host=localhost;uid=root;pwd=123;database=trade";
        public static DataTable SelectTable(string Table, string Where = "", string Atr = "*")
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlConnection con = new MySqlConnection(StrCon);
                con.Open();
                string q = "Select " + Atr + " from `" + Table+"` ";
                if (Where != "")
                {
                    q += " Where " + Where;
                }
                MySqlCommand cmd = new MySqlCommand(q, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dt);
                return dt;
            }
            catch (Exception) { return dt; }
        }
    }
}
