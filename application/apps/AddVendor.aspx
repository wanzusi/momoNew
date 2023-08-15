<%@ Page Language="C#" MasterPageFile="~/NewAccounts.master" AutoEventWireup="true" CodeFile="AddVendor.aspx.cs" 
Inherits="AddVendor" 
Title="NEW VENDOR"

Culture="auto" 
UICulture="auto" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    
   <section class="section">
       <div class="text-center">
        <h5 class="card-title">CREATE AGENT</h5>

    </div>
   </section>
    
 <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
      <section class="section">
   <div class="row justify-content-center">
<div class="col-lg-10">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Agent Details</h5>
  
        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Code</label>
            <asp:TextBox ID="txtCode" runat="server" class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputPassword5" class="form-label"> Bill System Code</label>
            <asp:TextBox ID="txtBillSystemCode" runat="server"  class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Name</label>
            <asp:TextBox ID="txtName" runat="server"  class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputAddress2" class="form-label">Contact Name</label>
            <asp:TextBox ID="txtcontact" runat="server" class="form-control"/>
          </div>
          <div class="col-md-6">
            <label for="inputCity" class="form-label">Relationship Manager</label>
            <asp:TextBox ID="txtAccountManager" runat="server" class="form-control"/>
          </div>
          <div class="col-md-6">
            <label for="inputState" class="form-label">Relationship Representative</label>
               <asp:TextBox ID="txtAccountRep" runat="server" class="form-control" TextMode="MultiLine" ></asp:TextBox>
          </div>
            <asp:TextBox ID="minBalance" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>

   <h5 class="card-title">System Accessibility Details</h5>
            <!-- Multi Columns Form -->
      <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputState" class="form-label">Email</label>
               <asp:TextBox ID="txtemail" runat="server" class="form-control"  ></asp:TextBox>
          </div>
           <div class="col-md-6">
            <label for="inputState" class="form-label">Confirm Email</label>
               <asp:TextBox ID="txtconfirmemail" runat="server" class="form-control"></asp:TextBox>
          </div>

           <div class="col-md-6">
            <label for="inputState" class="form-label">Pegasus Charge Type </label>
               <<asp:DropDownList ID="cboChargeType" runat="server" CssClass="form-select" OnDataBound="cboChargeType_DataBound" >
                                                </asp:DropDownList>
          </div>


           <div class="col-md-6">
            <label for="inputState" class="form-label">Pegasus Charge</label>
               <asp:TextBox ID="txtPegPayCharge" runat="server" class="form-control" ></asp:TextBox>
          </div>

        <div class="col-md-6">
          <div class="form-check"  style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Certificate Required</label>
            <asp:CheckBox runat="server" type="checkbox"  class="form-check-input" ID="chkcert"/>
         
          </div>
        </div>

            <div class="col-md-6">
               <label for="inputUser" class="form-label">User</label>
               <asp:TextBox ID="txtUser" ReadOnly="True" runat="server" class="form-control"  ></asp:TextBox>
          </div>

          
 <asp:MultiView ID="MultiView2" runat="server">
                 <asp:View ID="View2" runat="server">
                     <h5>Password</h5>
                     <div class="row g-3">
                          <div class="col-md-6">
          <div class="form-check">
            <label for="inputCity" class="form-label">Reset</label>
            <asp:CheckBox ID="chkResetPassword" runat="server"  class="form-check-input" type="checkbox" />
         
          </div>
        </div>
                     </div>
                     <h5>Email</h5>
                     <div class="row g-3">
                          <div class="col-md-6">
          <div class="form-check" style="margin-top:20px" >
            <label for="inputCity" class="form-label">Resend</label>
            <asp:CheckBox ID="chkResend" runat="server"  class="form-check-input" type="checkbox" />
         
          </div>
        </div>
                     </div>
              </asp:View>
                </asp:MultiView>
    <div class="col-md-6"> 
           <label for="inputUser" class="form-label">Browse PegPay Certificates</label>
               <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>

    <div class="col-md-2">
                <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary w-100" Text="Save Agent"  OnClick="btnOK_Click" style="font: menu" />
            </div>
        </div>
  
      </div>
    </div>
  </div>
  </div>
</div>
 </section>
</asp:View>

 </asp:MultiView>

  <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
  
</div>

</asp:Content>

