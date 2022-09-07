<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Foodie_Project.User.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <!-- book section -->
  <section class="book_section layout_padding">
    <div class="container">
      <div class="heading_container">
        <h2>
          Book A Table
        </h2>
      </div>
      <div class="row">
        <div class="col-md-6">
          <div class="form_container">
            <form action="">
              <%--<div>
                <input type="text" class="form-control" placeholder="Your Name" />
              </div>--%>
                <div class="form-group">
                    
                 <div>
               <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                 placeholder="Enter Your Name"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                   ErrorMessage="Name is required" ForeColor="Red" Display="Dynamic" 
                     SetFocusOnError="true" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                 <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                       </div>
                       </div>
              <%--<div>
                <input type="text" class="form-control" placeholder="Phone Number" />
              </div>--%>
                <div class="form-group">
                    
                 <div>
               <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"
                 placeholder="Enter Phone Number"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                   ErrorMessage="Phone Number is required" ForeColor="Red" Display="Dynamic" 
                     SetFocusOnError="true" ControlToValidate="txtPhoneNumber"></asp:RequiredFieldValidator>
                 <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                       </div>
                       </div>
              <%--<div>
                <input type="email" class="form-control" placeholder="Your Email" />
              </div>--%>
                 <div class="form-group">
                    
                 <div>
               <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                 placeholder="Enter Email ID"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                   ErrorMessage="Email ID is required" ForeColor="Red" Display="Dynamic" 
                     SetFocusOnError="true" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                 <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                       </div>
                       </div>
              <%--<div>
                <select class="form-control nice-select wide">
                  <option value="" disabled selected>
                    How many persons?
                  </option>
                  <option value="">
                    2
                  </option>
                  <option value="">
                    3
                  </option>
                  <option value="">
                    4
                  </option>
                  <option value="">
                    5
                  </option>
                </select>
              </div>--%>
                <div class="form-group">
                                                                   
                                                                   <div>
                                                                       <asp:TextBox ID="txtPersons" runat="server" CssClass="form-control"
                                                                           placeholder="Enter Persons Number"></asp:TextBox>
                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                                           ErrorMessage="Persons is required" ForeColor="Red" Display="Dynamic" 
                                                                           SetFocusOnError="true" ControlToValidate="txtPersons"></asp:RequiredFieldValidator>
                                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                                           ErrorMessage="Persons must be non negative" ForeColor="Red" Display="Dynamic"
                                                                           SetFocusOnError="true" ControlToValidate="txtPersons"
                                                                           ValidationExpression="^([1-9]\d*|0)$"></asp:RegularExpressionValidator>
                                                                   </div>
                                                               </div>
<%--              <div>
                <input type="date" class="form-control">
              </div>
                <div>
                    <asp:TextBox ID="selectdate" runat="server" CssClass="form-control"
                                                                           placeholder="Select Calender"  ></asp:TextBox>
                    <input type="date" class="form-control">
                </div>--%>
                <%--<div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" Height="17px"
            ImageUrl="~/image/No_image.png" onclick="ImageButton1_Click"Width="21px" />
<asp:Calendar ID="Calendar1" runat="server"
            onselectionchanged="Calendar1_SelectionChanged" Visible="False">
        </asp:Calendar>
    </div>--%>
                <%--<div>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                   <asp:ImageButton ID="ImageButton1" runat="server" Height="17px" 
                       ImageUrl="~/image/calender.jpg" onclick="ImageButton1_Click"Width="21px" />
                    </div>--%>
                <%--<div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" Height="17px"
            ImageUrl="~/image/calender.jpg" onclick="ImageButton1_Click"Width="21px" />
<asp:Calendar ID="Calendar1" runat="server"
            onselectionchanged="Calendar1_SelectionChanged" Visible="False">
        </asp:Calendar>
    </div>--%>
                <div>  
      
        <asp:TextBox ID="TextBox1" runat="server" Height="28px" ReadOnly="True"></asp:TextBox>  
   
        <asp:ImageButton ID="ImageButton1" runat="server" Height="34px" ImageUrl="~/Images/Calendar-256.png" OnClick="ImageButton1_Click" style="margin-left: 0px" Width="33px" />  
        <br />  
        <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" ShowGridLines="True" Width="220px">  
            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />  
            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />  
            <OtherMonthDayStyle ForeColor="#CC9966" />  
            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />  
            <SelectorStyle BackColor="#FFCC66" />  
            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />  
            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />  
        </asp:Calendar>  
      
    </div>  
              <div class="btn_box">
                <%--<button>
                  Book Now
                </button>--%>
                  <asp:Button ID="Button1" runat="server" Text="Book Now"  BackColor="#FFCC00" ForeColor="Black" OnClick="Button1_Click"/>
              </div>
            </form>
          </div>
        </div>
        <div class="col-md-6">
          <div class="map_container ">
            <div id="googleMap"></div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- end book section -->

</asp:Content>
