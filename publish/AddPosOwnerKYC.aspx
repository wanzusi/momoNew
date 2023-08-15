<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="AddPosOwnerKYC, App_Web_zulwb1bx" title="Untitled Page" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <section class="section">
        <div class="text-center">
            <h5 class="card-title">CREATE/EDIT AGENT KYC</h5>
        </div>
    </section>

    <section class="section">
        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                   <div class="row justify-content-center">
<div class="col-lg-8" >
    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Customer Details</h5>
 
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Code</label>
            <asp:TextBox ID="txtVendorCode" runat="server" class="form-control"  Enabled="false"/>
          </div>
          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">First Name</label>
            <asp:TextBox ID="txtFname" runat="server"  class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Last Name</label>
            <asp:TextBox ID="txtLname" runat="server"  class="form-control" />
          </div>
     
          <div class="col-md-6">
            <label for="email" class="form-label">Other Name</label>
            <asp:TextBox ID="txtOtherName" runat="server"  class="form-control" />
          </div>

          <div class="col-md-6">
            <label for="email" class="form-label">Date Of Birth</label>
            <asp:TextBox ID="txtDateofBirth" runat="server"  class="form-control" />
          </div>

              <div class="col-md-6">
            <label for="email" class="form-label">Contact One</label>
            <asp:TextBox ID="txtcontact1" runat="server"  class="form-control" />
          </div>
              <div class="col-md-6">
            <label for="email" class="form-label">Contact Two</label>
            <asp:TextBox ID="txtContact2" runat="server"  class="form-control" />
          </div>
              <div class="col-md-6">
            <label for="email" class="form-label">Gender</label>
          <asp:RadioButtonList ID="rbnGender" runat="server" BackColor="white" Font-Bold="True"
                                                                        RepeatDirection="Horizontal" CssClass="form-select">
                                                                        <asp:ListItem>MALE</asp:ListItem>
                                                                        <asp:ListItem>FEMALE</asp:ListItem>
                                                                    </asp:RadioButtonList>
          </div>
              <div class="col-md-6">
            <label for="email" class="form-label">Nationality</label>
            <asp:TextBox ID="txtNattionality" runat="server"  class="form-control" />
          </div>
              <div class="col-md-6">
            <label for="email" class="form-label">Address</label>
            <asp:TextBox ID="txtAddress" runat="server"  class="form-control" />
          </div>

               <div class="col-md-6">
            <label for="email" class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server"  class="form-control" />
          </div>

               <div class="col-md-6">
            <label for="email" class="form-label">Customer Type</label>
          <asp:DropDownList ID="cboCustomerType" runat="server" AutoPostBack="False" OnDataBound="cboCustomerType_DataBound"
                                                                        OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Style="font: menu" CssClass="form-select" >
                                                                    </asp:DropDownList>
          </div>

            
               <div class="col-md-6">
            <label for="email" class="form-label">Business Type</label>
           <asp:DropDownList ID="cboBusinessType" runat="server" AutoPostBack="False" OnDataBound="cboBusinessType_DataBound"
                                                                        OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Style="font: menu"
                                                                        class="form-select">
                                                                    </asp:DropDownList>
          </div>
            
               <div class="col-md-6">
            <label for="email" class="form-label">Trading Name</label>
            <asp:TextBox ID="txtTradingName" runat="server"  class="form-control" />
          </div>
            
               <div class="col-md-6">
            <label for="email" class="form-label">Company Reg No</label>
            <asp:TextBox ID="txtCompanyReg" runat="server"  class="form-control" />
          </div>
            
               <div class="col-md-6">
            <label for="email" class="form-label">TIN</label>
            <asp:TextBox ID="txtTin" runat="server"  class="form-control" />
          </div>

              <div class="col-md-6">
            <label for="email" class="form-label">Region</label>
            <asp:TextBox ID="txtRegion" runat="server"  class="form-control" />
          </div>
              <div class="col-md-6">
            <label for="email" class="form-label">District</label>
            <asp:TextBox ID="txtDistrict" runat="server"  class="form-control" />
          </div>
              <div class="col-md-6">
            <label for="email" class="form-label">Customer Id Type</label>
            <asp:DropDownList ID="cboCustomerIdType" runat="server" AutoPostBack="False" OnDataBound="cboCustomerIdType_DataBound"
                                                                        OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Style="font: menu"
                                                                        Width="60%">
                                                                        <asp:ListItem Value="0">Select Id Type</asp:ListItem>
                                                                        <asp:ListItem Value="1">National Id</asp:ListItem>
                                                                        <asp:ListItem Value="2">Voter's Card</asp:ListItem>
                                                                        <asp:ListItem Value="3">Residentail Id</asp:ListItem>
                                                                        <asp:ListItem Value="4">Company Id</asp:ListItem>
                                                                        <asp:ListItem Value="5">Others</asp:ListItem>
                                                                    </asp:DropDownList>
          </div>

            <div class="col-md-6">
            <label for="email" class="form-label">CustomerId No</label>
            <asp:TextBox ID="txtCustomerIdNo" runat="server"   class="form-control" />
          </div>

            

           <div class="col-md-6">
          <div class="form-check" style="margin-top:20px;">
            <label for="inputCity" class="form-label">Is KYC Active</label>
            <asp:CheckBox ID="chkIsActive" runat="server" Font-Bold="True" Text="" />
         
          </div>

          <div class="col-md-6">
            <label for="email" class="form-label">User</label>
            <asp:TextBox ID="txtUser" runat="server"  ReadOnly="True"  class="form-control" />
          </div>

  
        </div>
     

            <div class="col-md-6">
                <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary"  Text="Save Details" OnClick="btnOK_Click" style="font: menu" />
            </div>


        </div>
  
      
    </div>
  </div>
  </div>
</div>

                                <div class="container">
                                  <asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                                    <br />
                <asp:Label ID="lblVendorCode" runat="server" Text="0" Visible="False"></asp:Label>
                                </div>
                            </asp:View>
            </asp:MultiView>

    </section>





</asp:Content>

