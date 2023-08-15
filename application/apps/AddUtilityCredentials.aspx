<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddUtilityCredentials.aspx.cs" 
Inherits="AddUtilityCredentials" 
Title="NEW UTILITY CREDENTIALS"

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
       <div class="container">
   
 <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
      <section class="section">
    
   <div class="row">
<div class="col-lg-8">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Add Utility Credentials</h5>
  
        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Select Vendor</label>
            <asp:DropDownList ID="ddlVendor"  CssClass="form-select" runat="server" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" OnDataBound="ddlVendor_DataBound"> </asp:DropDownList>
          </div>
          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">Select Utility</label>
            <asp:DropDownList ID="ddlUtility" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlUtility_SelectedIndexChanged" OnDataBound="ddlUtility_DataBound">
                                                </asp:DropDownList>
          </div>
          <div class="col-md-6">
            <label for="txtUsername" class="form-label">Utility Username</label>
            <asp:TextBox ID="txtUsername" runat="server"  class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputAddress2" class="form-label">Utility Password</label>
            <asp:TextBox ID="txtPassword" runat="server" class="form-control"/>
          </div>
          <div class="col-md-6">
            <label for="inputCity" class="form-label">Bank Code</label>
            <asp:TextBox ID="txtBankCode" runat="server" class="form-control"/>
          </div>
          <div class="col-md-6">
          <div class="form-check">
            <label for="inputCity" class="form-label">Tick if Utility Has Certificate</label>
           
               <asp:CheckBox ID="chkPrepayment"  class="form-check-input" type="checkbox" runat="server"  AutoPostBack="True" OnCheckedChanged="chkPrepayment_CheckedChanged" />
         
          </div>
        </div>

        <asp:MultiView ID="MultiView3" runat="server">
               <asp:View ID="View3" runat="server">
                   <h5>Certificate Details</h5>
               <div class="col-md-6"> 
           <label for="upload" class="form-label">Browse PegPay Certificates</label>
               <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
               </asp:View>
            </asp:MultiView>
            
            <div class="col-md-6">
                <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary" Text="SAVE"  Font-Bold="True" OnClick="btnOK_Click" style="font: menu" />
            </div>

  
      </div>
    </div>
  </div>


  </div>
</div>
 </section>
</asp:View>

 </asp:MultiView>

  <asp:Label ID="lblCode" CssClass="form-label" runat="server" Text="0" Visible="False"></asp:Label><br/>


</div>

</asp:Content>

