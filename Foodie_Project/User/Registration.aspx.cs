using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie_Project.User
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;      //passing querry
        SqlDataAdapter sda;   //query
        DataTable dt;           //it is used to store the temporary data inside dt object

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null) // && Session["userId"]!=null)
                {
                    getUserDetails();
                }
                else if (Session["userId"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int userId = Convert.ToInt32(Request.QueryString["id"]);              //it stores the value of hidden field value and default value is 0
            con = new SqlConnection(Connection.GetConnectionString());//calling the method for getting the connection
            cmd = new SqlCommand("User_Crud", con);       //sql command by stored procedure name
            cmd.Parameters.AddWithValue("@Action", userId == 0 ? "INSERT" : "UPDATE");  //here we are using ternary operator //here it is checking if it is 0 then insert if we want to update then we are passing the category id then update goes on
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text.Trim());//if checkbox is checked then it will pass true and this value is  passed in this parameter
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)  //it will check that we has the image
            {
                if (Utils.IsValidExtension(fuUserImage.FileName))  //passing the file name if it has valid extension
                {
                    Guid obj = Guid.NewGuid(); //it will get the unique no or id it is a combination of string and number
                    fileExtension = Path.GetExtension(fuUserImage.FileName);//get extension is a static class
                    imagePath = "Images/User/" + obj.ToString() + fileExtension; //where we want to store that file that we have uploaded 
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath); //5th parameter of sp we are storing the image path
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select .jpg, .jpeg or .png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userId == 0 ?
                        "registration is successful! <b><a href='Login.aspx'>Click here</a></b> to do login" :
                        "details updated successful! <b><a href='Profile.aspx'>Can check here</a></b>";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b>" + "<b>" + actionName + "</b>";
                    //lblMsg.Text = "<b>" + actionName + "</b>";
                    lblMsg.CssClass = "alert alert-success";
                    //getProducts();
                    // Clear();
                    if (userId !=0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                    }
                    Clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> username already exist, try new one..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }

                }

                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }

                finally
                {
                    con.Close();
                }
            }
        }

        void getUserDetails()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
            cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPostCode.Text = dt.Rows[0]["PostCode"].ToString();
                imgUser.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString())
                    ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgUser.Height = 200;
                imgUser.Width = 200;
                txtPassword.TextMode = TextBoxMode.SingleLine;
                txtPassword.ReadOnly = true;
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
            }
            lblHeaderMsg.Text = "<h2>Edit Profile</h2>";
            btnRegister.Text = "Update";
            lblAlreadyUser.Text = "";
        }
        private void Clear()
        {
            txtName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty; 
            txtPostCode.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }
}