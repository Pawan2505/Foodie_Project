using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace Foodie_Project.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection con;   //used for sql connection
        SqlCommand cmd;      //passing querry
        SqlDataAdapter sda;   //query
        DataTable dt;           //it is used to store the temporary data inside dt object
       // public bool IsValidToExecute { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//if it is not loaded for the first time then execute
            {
                Session["breadCrum"] = "Category";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getCategroies();
                }
               
            }
            lblMsg.Visible = false;

        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
           bool isValidToExecute = false;
            int CategoryId = Convert.ToInt32(hdnId.Value);              //it stores the value of hidden field value and default value is 0
            con = new SqlConnection(Connection.GetConnectionString());//calling the method for getting the connection
            SqlCommand cmd = new SqlCommand("Category_Crud", con);       //sql command by stored procedure name
            cmd.Parameters.AddWithValue("@Action", CategoryId == 0 ? "INSERT" : "UPDATE");  //here we are using ternary operator //here it is checking if it is 0 then insert if we want to update then we are passing the category id then update goes on
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);//if checkbox is checked then it will pass true and this value is  passed in this parameter
            if (fuCategoryImage.HasFile)  //it will check that we has the image
            {
                if (Utils.IsValidExtension(fuCategoryImage.FileName))  //passing the file name if it has valid extension
                {
                    Guid obj = Guid.NewGuid(); //it will get the unique no or id it is a combination of string and number
                    fileExtension = Path.GetExtension(fuCategoryImage.FileName);//get extension is a static class
                    imagePath = "Images/Category/" + obj.ToString() + fileExtension; //where we want to store that file that we have uploaded 
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + obj.ToString() + fileExtension);
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
                    cmd.BeginExecuteNonQuery();
                    actionName = CategoryId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category" + actionName + "successfully";
                    lblMsg.CssClass = "alert alert-success";
                    getCategroies();
                    Clear();
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

        private void getCategroies()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Category_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
           rCategory.DataSource = dt;
           rCategory.DataBind();
        }

        private void Clear()
        {
            txtName.Text = string.Empty;
            cbIsActive.Checked = false;
            hdnId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgCategory.ImageUrl = string.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            con = new SqlConnection(Connection.GetConnectionString());
            if (e.CommandName == "edit")
            {
                
                cmd = new SqlCommand("Category_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@CategoryId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtName.Text = dt.Rows[0]["Name"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imgCategory.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? 
                    "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgCategory.Height = 200;
                imgCategory.Width = 200;
                hdnId.Value = dt.Rows[0]["CategoryId"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if(e.CommandName == "delete")
            {
              //  con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Category_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@CategoryId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category Deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getCategroies();
                }
                catch(Exception ex)
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

        protected void rCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl = e.Item.FindControl("lblIsActive") as Label;
                if(lbl.Text == "True")
                {
                    lbl.Text = "Active";
                    lbl.CssClass = "badge badge-success";
                }
                else
                {
                    lbl.Text = "In-Active";
                    lbl.CssClass = "badge badge-danger";
                }
            }
        }
    }
}