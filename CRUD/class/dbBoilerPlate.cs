using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public class dbBoilerPlate
    {
        static string connectionString = @"Data Source=DESKTOP-PN40P7Q\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True"; //pa check ung connection string kasi baka iba sa inyo... thank you po
        SqlCommand cmd;
        SqlDataAdapter adapter;

        public void dtTable(string sql, DataGridView data)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            data.DataSource = dt;
            con.Close();
        }
        public void dtQueries(string sql, string fname, string lname, int age, string email)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@fname",fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@email", email);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
