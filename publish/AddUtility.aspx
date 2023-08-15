<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="AddUtility, App_Web_rgb2gjqg" title="NEW UTILITY" culture="auto" uiculture="auto" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <section class="section"> 
                <div class="text-center">
                        <h5 class="card-title">CREATE UTILITY</h5>
            </div></section>
               
      <section class="section">
   <div class="row justify-content-center">
<div class="col-lg-8" >
    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Utility Details</h5>
 
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Utility Code</label>
            <asp:TextBox ID="txtCode" runat="server" class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">Utility Name</label>
            <asp:TextBox ID="txtName" runat="server"  class="form-control" />
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Contact Name</label>
            <asp:TextBox ID="txtcontact" runat="server"  class="form-control" />
          </div>
        

   <h5 class="card-title">Utility Contact Details</h5>
            <!-- Multi Columns Form -->
      <div class="row g-3">
   
          <div class="col-md-6">
            <label for="email" class="form-label">Email</label>
            <asp:TextBox ID="txtemail" runat="server"  class="form-control" />
          </div>

          <div class="col-md-6">
            <label for="email" class="form-label">Confirm Email</label>
            <asp:TextBox ID="txtconfirmemail" runat="server"  class="form-control" />
          </div>

           <div class="col-md-6">
          <div class="form-check" style="margin-top:20px;">
            <label for="inputCity" class="form-label">Is Active</label>
            <asp:CheckBox runat="server" type="checkbox" ID="chkIsActive"/>
         
          </div>

          <div class="col-md-6">
            <label for="email" class="form-label">User</label>
            <asp:TextBox ID="txtUser" runat="server"  ReadOnly="True"  class="form-control" />
          </div>

  
        </div>
     

            <div class="col-md-6">
                <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary" Text="SAVE AGENT"  Font-Bold="True" OnClick="btnOK_Click" style="font: menu" />
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
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />

</asp:Content>

