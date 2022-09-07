using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie_Project.User
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con;   //used for sql connection
        SqlCommand cmd;      //passing querry
        SqlDataAdapter sda;   //query
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategroies();
               
            }
        }

        private void getCategroies()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Category_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "ACTIVECAT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }
    }
}