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

    public partial class Cart : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        decimal grandTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {
                    getCartItems();
                }
            }

        }

        void getCartItems()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Cart_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCartItem.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                rCartItem.FooterTemplate = null;
                rCartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rCartItem.DataBind();
        }



        protected void rCartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            con = new SqlConnection(Connection.GetConnectionString());
          
            if (e.CommandName == "delete")
            {
                //  con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Cart_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
               cmd.Parameters.AddWithValue("@Quantity", "Quantity");
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product Deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                   
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
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;

            }
            if (e.CommandName == "Update")
            {
                //  con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("Cart_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                cmd.Parameters.AddWithValue("@Quantity", "Quantity");
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product Updated successfully!";
                    lblMsg.CssClass = "alert alert-success";

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

 

        protected void rCartItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("lblTotalPrice") as Label;
                Label productPrice = e.Item.FindControl("lblPrice") as Label;
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;
                decimal calTotalPrice = Convert.ToDecimal(productPrice.Text) * Convert.ToDecimal(quantity.Text);
                totalPrice.Text = calTotalPrice.ToString();
                grandTotal += calTotalPrice;
            }
            Session["grandTotalPrice"] = grandTotal;
        }
        //Custom template class to add controls to the repeater's header, item and footer sections.
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }

            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Your Cart is empty.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Continue Shopping</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }

      

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            //string p = Session["userId"].ToString();
            //con = new SqlConnection(Connection.GetConnectionString());
            //cmd = new SqlCommand("Cart_Crud", con);
            //cmd.Parameters.AddWithValue("@Action", "DELETE");
            //cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            ////     cmd.Parameters.AddWithValue("@ProductId", Session["ProductId"]);
            //cmd.CommandType = CommandType.StoredProcedure;
            //try
            //{
            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    lblMsg.Visible = true;
            //    lblMsg.Text = " Deleted successfully!";
            //    lblMsg.CssClass = "alert alert-success";

            //    //  getProducts();
            //}
            //catch (Exception ex)
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = "Error-" + ex.Message;
            //    lblMsg.CssClass = "alert alert-danger";
            //}
            //finally
            //{
            //    con.Close();

            //}
        }
        protected void lbUpdateCart_Click(object sender, EventArgs e)
        {
          //  TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;
            string p = Session["userId"].ToString();
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("Cart_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@UserId", Session["useId"]);
            cmd.Parameters.AddWithValue("@ProductId", Session["ProductId"]);
            cmd.Parameters.AddWithValue("@Quantity", "Quantity");
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                lblMsg.Visible = true;
                lblMsg.Text = " Update successfully!";
                lblMsg.CssClass = "alert alert-success";

                //  getProducts();
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

        protected void lbCheckout_Click(object sender, EventArgs e)
        {

        }
       
    }
}