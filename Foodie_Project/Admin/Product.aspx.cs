using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foodie_Project.Admin
{
    public partial class Product : System.Web.UI.Page
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
                Session["breadCrum"] = "Product";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getProducts();
                }
               
            }
            lblMsg.Visible = false;
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int productId = Convert.ToInt32(hdnId.Value);              //it stores the value of hidden field value and default value is 0
            con = new SqlConnection(Connection.GetConnectionString());//calling the method for getting the connection
            SqlCommand cmd = new SqlCommand("Product_Crud", con);       //sql command by stored procedure name
            cmd.Parameters.AddWithValue("@Action", productId == 0 ? "INSERT" : "UPDATE");  //here we are using ternary operator //here it is checking if it is 0 then insert if we want to update then we are passing the category id then update goes on
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryId", ddlCategories.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);//if checkbox is checked then it will pass true and this value is  passed in this parameter
            if (fuProductImage.HasFile)  //it will check that we has the image
            {
                if (Utils.IsValidExtension(fuProductImage.FileName))  //passing the file name if it has valid extension
                {
                    Guid obj = Guid.NewGuid(); //it will get the unique no or id it is a combination of string and number
                    fileExtension = Path.GetExtension(fuProductImage.FileName);//get extension is a static class
                    imagePath = "Images/Product/" + obj.ToString() + fileExtension; //where we want to store that file that we have uploaded 
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + obj.ToString() + fileExtension);
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
                    actionName = productId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product" + actionName + "successfull";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();
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
        private void getProducts()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }
        private void Clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtPrice.Text = string.Empty;
            ddlCategories.ClearSelection();
            cbIsActive.Checked = false;
            hdnId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imgProduct.ImageUrl = string.Empty;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            con = new SqlConnection(Connection.GetConnectionString());
            if (e.CommandName == "edit")
            {

                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                ddlCategories.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ?
                    "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgProduct.Height = 200;
                imgProduct.Width = 200;
                hdnId.Value = dt.Rows[0]["ProductId"].ToString();
                btnAddOrUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                //  con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product Deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();
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

        protected void rProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsActive = e.Item.FindControl("lblIsActive") as Label;
                Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;
                if (lblIsActive.Text == "True")
                {
                    lblIsActive.Text = "Active";
                    lblIsActive.CssClass = "badge badge-success";
                }
                else
                {
                    lblIsActive.Text = "In-Active";
                    lblIsActive.CssClass = "badge badge-danger";
                }
                if(Convert.ToInt32(lblQuantity.Text)<=5)
                {
                    lblQuantity.CssClass = "badge badge-danger";
                    lblQuantity.ToolTip = "Item about to be 'Out of stock'!";
                }
            }
        }
    }
}