<%@ page language="C#" masterpagefile="~/NewSystemTools.master" autoeventwireup="true" inherits="AddUser, App_Web_jzvpb42g" title="NEW SYSTEM USER" culture="auto" uiculture="auto" enableviewstatemac="false" %>
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

        <h5 class="card-title">CREATE SYSTEM USER</h5>

    </div>
</section>
 
 <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
      <section class="section">
   <div class="row justify-content-center">
<div class="col-lg-6">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">User Details</h5>
  
        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">First Name</label>
            <asp:TextBox ID="TxtFname" runat="server" class="form-control" ></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">Middle Name</label>
            <asp:TextBox ID="txtMiddleName" runat="server"  class="form-control" ></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Last Name</label>
            <asp:TextBox ID="txtLname" runat="server"  class="form-control" ></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputAddress2" class="form-label">Email</label>
            <asp:TextBox ID="txtemail" runat="server" class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputCity" class="form-label">Phone</label>
            <asp:TextBox ID="txtphone" runat="server" class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputState" class="form-label">Job Role</label>
               <asp:TextBox ID="txtDesignation" runat="server" class="form-control" TextMode="MultiLine" ></asp:TextBox>
          </div>

   <h5 class="card-title">System Accessibility Details</h5>
            <!-- Multi Columns Form -->
      <div class="row g-3">
          <ajaxToolkit:UpdatePanel ID="UpdatePanel" runat="server">
                                        <ContentTemplate>
        <div class="row">
        <div class="col-md-6">
          <label for="inputEmail5" class="form-label">User category</label>
           <asp:DropDownList ID="cboUserType" class="form-select" runat="server" AutoPostBack="True" OnDataBound="cboUserType_DataBound"  OnSelectedIndexChanged="cboUserType_SelectedIndexChanged"> </asp:DropDownList>
        </div>
        <div class="col-md-6">
          <label for="inputPassword5" class="form-label">Company</label>
           <asp:DropDownList ID="cboCompany" runat="server" OnDataBound="cboCompany_DataBound" class="form-select"> </asp:DropDownList>
        </div>
        <div class="col-md-6">
          <label for="inputAddress5" class="form-label">UserType</label>
         <asp:DropDownList ID="cboAccessLevel" runat="server" OnDataBound="cboAccessLevel_DataBound" class="form-select"  OnSelectedIndexChanged="cboAccessLevel_SelectedIndexChanged" > </asp:DropDownList>
        </div>
        <div class="col-md-6">
          <label for="inputAddress2" class="form-label">OTP Notifiaction Type</label>
           <asp:DropDownList ID="ddNotificationType" runat="server" OnDataBound="ddlNotificaction_DataBound" CssClass="form-select"  OnSelectedIndexChanged="ddlNotification_SelectedIndexChanged">
                                                        </asp:DropDownList>
        </div>

        <div class="col-md-6">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Active</label>
            <asp:CheckBox runat="server" type="checkbox" ID="chkIsActive"/>
         
          </div>
        </div>
        <div class="col-md-6">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Logged on</label>
            <asp:CheckBox ID="chkIsLoggedon" runat="server"  class="form-check-input" type="checkbox" />
              
         
          </div>
        </div>

        <div class="col-md-6">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Reset Password</label>
            <asp:CheckBox runat="server"  class="form-check-input" type="checkbox" ID="chkResetPassword"/>
         
          </div>
        </div>
            </div>
   <asp:MultiView ID="MultiView2" runat="server">
       <asp:View ID="View3" runat="server">
       <div class="col-md-6">
                <label for="username" class="form-label">Username</label>
           <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
            
               
         
       </div>
           </asp:View>
   </asp:MultiView>

            <div class="col-md-6 mb-2 mt-2 text-center">
                <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary w-50" Text="SAVE USER"  Font-Bold="True" OnClick="btnOK_Click" style="font: menu" />
            </div>


   </ContentTemplate>
     </ajaxToolkit:UpdatePanel>
        </div>
  
      </div>
    </div>
  </div>
  </div>
</div>
 </section>
</asp:View>
     <asp:View ID="View2" runat="server">
         <div class="col-md-6">
              <asp:Label ID="lblQn" class="form-label" runat="server" Text="."></asp:Label>
                        <asp:Button CssClass="btn btn-success"
                            ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes" />
                        <asp:Button ID="btnNo"  CssClass="btn btn-danger" runat="server" OnClick="btnNo_Click" Text="No" />
         </div>

     </asp:View>
 </asp:MultiView>
 
                        <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
    <asp:Label ID="lblusername" runat="server" Text="." Visible="False"></asp:Label>



</asp:Content>

