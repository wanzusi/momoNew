<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddEditPosAccount.aspx.cs" Inherits="AddPosOwnerKYC" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="section">
        <div class="text-center">
            <h5 class="card-title">CREATE/EDIT POS ACCOUNT</h5>
        </div>
    </section>

    <section class="section">
        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
        <div class="col-lg-8" >
    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">ACCOUNT DETAILS</h5>
 
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Code</label>
            <asp:TextBox ID="txtVendorCode" runat="server" class="form-control"  Enabled="false"/>
          </div>
          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">Trading Name</label>
            <asp:TextBox ID="txtFname" runat="server"  class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Address</label>
            <asp:TextBox ID="txtLname" runat="server"  class="form-control" />
          </div>
     
          <div class="col-md-6">
            <label for="email" class="form-label">UserName</label>
            <asp:TextBox ID="txtUsername" runat="server"  class="form-control" />
          </div>

          <div class="col-md-6">
            <label for="email" class="form-label">Password</label>
            <asp:TextBox ID="txtPassword" runat="server"  class="form-control" />
          </div>

              <div class="col-md-6">
            <label for="email" class="form-label">Spid</label>
            <asp:TextBox ID="txtSpid" runat="server"  class="form-control" />
          </div>
           
      
             <div class="col-md-2">
                <asp:Button ID="btnOK" runat="server" class="btn btn-primary  w-100"  OnClick="btnOK_Click"  Text="SAVE DETAILS" />
            </div>

            <div class="container">
                <asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
              
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblVendorCode" runat="server" Text="0" Visible="False"></asp:Label>
            </div>

          </div>

           

        </div>
  

  </div>
  </div>
</asp:View>
            </asp:MultiView>
    </section>




</asp:Content>

