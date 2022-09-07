using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie_Project.Admin
{
    public partial class contect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con;   //used for sql connection
            SqlCommand cmd;
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("select * from Book", con);       //sql command by stored procedure name

            con.Open();
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            con.Close();
        }
    }
}