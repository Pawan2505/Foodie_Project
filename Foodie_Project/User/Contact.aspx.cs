using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie_Project.User
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con;   //used for sql connection
            SqlCommand cmd;
            con = new SqlConnection(Connection.GetConnectionString());//calling the method for getting the connection
            cmd = new SqlCommand("Book_Crud", con);       //sql command by stored procedure name
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Persons", txtPersons.Text.Trim());
            cmd.Parameters.AddWithValue("@CreatedDate", TextBox1.Text.Trim());
           // cmd.Parameters("@selectdate").Value = Calendar1.SelectedDate;
            //cmd.Parameters.AddWithValue("");
            cmd.ExecuteNonQuery();
            Response.Write("Data Stored");
            con.Close();
        }

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    Calendar1.Visible = true;
        //}

        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{
        //    TextBox1.Text = Calendar1.SelectedDate.ToShortDateString();
        //    Calendar1.Visible = false;
        //}
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsWeekend || e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }
    }
}