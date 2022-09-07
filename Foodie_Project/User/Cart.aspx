<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Foodie_Project.User.Cart" %>
<%@ Import Namespace="Foodie_Project"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="book_section layout_padding">
        
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                 <h2>Your Shopping Cart
                </h2>
            </div>
        </div>
         <div class="container">
             <asp:Repeater ID="rCartItem" runat="server" OnItemCommand="rCartItem_ItemCommand" OnItemDataBound="rCartItem_ItemDataBound">
                 <HeaderTemplate>
                     <table class="table">
                         <thead>
                             <tr>
                                 <th>Name</th>
                                 <th>Image</th>
                                 <th>Unit Price</th>
                                 <th>Quantity</th>
                                 <th>Total Price</th>
                                 <th></th>
                             </tr>
                         </thead>
                         <tbody> 
                 </HeaderTemplate>
                 <ItemTemplate>
                     <tr>
                         <td>
                             <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                         </td>
                         <td>
                             <img width="60" src="<%# Utils.GetImageUrl(Eval("ImageUrl"))%>" alt=""/>
                         </td>
                         <td>₹<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                             <asp:HiddenField ID="hdnProductId" runat="server" Value='<%# Eval("ProductId") %>' />
                             <asp:HiddenField ID="hdnQuantity" runat="server" Value='<%# Eval("Qty") %>'/>
                             <asp:HiddenField ID="hdnPrdQuantity" runat="server" Value='<%# Eval("PrdQty") %>'/>
                         </td>
                         <td>
                             <div class="product__details__option">
                                 <div class="quantity">
                                     <%--<button class="btn minus-btn disabled" type="button">-</button>
                                     <input type="text" id="quantity" value="1" />
                                     <button class="btn plus-btn" type="button">+</button>--%>
                                     <div class="pro-qty">
                                         <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" AutoPostBack="true"    Text='<%# Eval("Quantity") %>'>
                                         </asp:TextBox>
                                         <asp:RegularExpressionValidator ID="revQuantity" runat="server" ErrorMessage="*" ForeColor="Red" 
                                             Font-Size="Small" ValidationExpression="[1-9]*" ControlToValidate="txtQuantity"
                                             Display="Dynamic" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>
                                     </div>
                                 </div>
                             </div>
                         </td>
                         <td>₹<asp:Label ID="lblTotalPrice" runat="server"></asp:Label></td>
                         <td>
                            <%-- <asp:LinkButton ID="lbDelete" runat="server" Text="Remove" CommandName="remove" 
                                 CommandArgument='<%# Eval("ProductId") %>'
                                 OnClientClick="return confirm('Do you want to remove this item from cart?');" OnClick="lbDelete_Click">
                                
                                 <i class="fa fa-close"></i> 
                             </asp:LinkButton>--%>
                               <div class="table-responsive">
                              <meta http-equiv="refresh" content="15" />
                             <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandName="delete"
                                                                                       CssClass="badge bg-danger" CommandArgument='<%# Eval("ProductId")%>'
                                                                                       OnClientClick="return confirm('Do you want to delete this Product?');"
                                                                                      CausesValidation="false" >
                                                                                        <i class="fa fa-close"></i>
                                                                                           </asp:LinkButton>
                                   <asp:LinkButton ID="LinkButton1" Text="Update" runat="server" CommandName="Update"
                                                                                       CssClass="badge bg-danger" CommandArgument='<%# Eval("ProductId")%>'
                                                                                       OnClientClick="return confirm('Do you want to update this Product?');"
                                                                                      CausesValidation="false" >
                                                                                        <i class="fa fa-close"></i>
                                                                                           </asp:LinkButton>
                            </div>
                         </td>
                     </tr>
                 </ItemTemplate>
                 <FooterTemplate>
                     <tr>
                         <td colspan="3"></td>
                         <td class="pl-lg-5">
                             <b>Grand Total:-</b>
                         </td>
                         <td>₹<% Response.Write(Session["grandTotalPrice"]); %></td>
                         <td></td>
                     </tr>
                     <tr>
                         <td colspan="2" class="continue__btn">
                             <a href="Menu.aspx" class="btn btn-info"><i class="fa fa-arrow-circle-left mr-2">Continue Shopping</i></a>

                         </td>
                         <td>
                             <asp:LinkButton ID="lbUpdateCart" runat="server" CommandName="updateCart" CssClass="btn btn-warning" OnClick="lbUpdateCart_Click">
                                 <i class="fa fa-refresh mr-2"></i>Update Cart
                             </asp:LinkButton>
                         </td>

                         <td>
                              <asp:LinkButton ID="lbCheckout" runat="server" CommandName="checkout" CssClass="btn btn-success" OnClick="lbCheckout_Click">
                                 Checkout<i class="fa fa-arrow-circle-right mr-2"></i>
                             </asp:LinkButton>
                         </td>
                     </tr>
                     </tbody>
                     </table>
                 </FooterTemplate>
             </asp:Repeater>
        </div>
    </section>

</asp:Content>
