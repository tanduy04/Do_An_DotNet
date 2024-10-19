using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;



namespace QL_CuaHangXeMay
{
    class DBConnect
    {

        public static string chuoiketnoi = "Data Source=DESKTOP-64EHLDT;Initial Catalog=QL_XeMay;Integrated Security=True";


        //public static string chuoiketnoi = "";


        //public static string chuoiketnoi = "";


        //public static string chuoiketnoi = "";

        public SqlConnection con = new SqlConnection();
        public DBConnect()
        {
            con = new SqlConnection(chuoiketnoi);
        }
        public void Open()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public int getNonQuery(string sqlquery)
        {
            Open();
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            int kq = cmd.ExecuteNonQuery();
            Close();
            return kq;
        }
        public DataTable getDataTable(string sqlquery)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
            da.Fill(ds);
            return ds.Tables[0];
        }
        public int getScalar(string sqlquery)
        {
            Open();
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            int kq = (int)cmd.ExecuteScalar();
            Close();
            return kq;
        }















        public int updateDataTable(DataTable dtnew, string chuoitv)
        {
            SqlDataAdapter da = new SqlDataAdapter(chuoitv, con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            int kq = da.Update(dtnew);
            return kq;
        }

    }
}
