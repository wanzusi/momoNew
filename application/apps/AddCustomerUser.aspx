<%@ Page Language="C#" MasterPageFile="~/NewCustomer.master" AutoEventWireup="true" CodeFile="AddCustomerUser.aspx.cs" 
Inherits="AddCustomerUser" 
Title="NEW SYSTEM USER"

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
            <h5 class="card-title">CREATE/EDIT CORPORATE USER</h5>
        </div>
    </section>
    
    
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
       
        </table>
            <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%">
                                                                </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            &nbsp;<asp:MultiView ID="MultiView4" runat="server">
                <asp:View ID="View5" runat="server">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                style="width: 90%">
                <tr style="color: #000000">
                    <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; text-align: center">
                        Search Customer Name</td>
                </tr>
                <tr style="color: #000000">
                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                        text-align: center">
                        &nbsp;&nbsp;
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                            style="width: 90%">
                            <tr>
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="1" style="vertical-align: top;width: 40%;
                                    height: 20px; text-align: left">
                                    <asp:TextBox ID="txtCustCode" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="2" style="vertical-align: top;width: 40%;
                                    height: 20px; text-align: center">
                                    <asp:TextBox ID="txtCustSearch" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                <td class="InterFaceTableRightRow" colspan="1" style="vertical-align: top; width: 20%;
                                    height: 20px; text-align: left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                            Height="25px" OnClick="btnSearch_Click" Text="Search" Width="140px" /></td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: left">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                        height: 20px; text-align: left">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; height: 20px;
                        text-align: center">
                        Select Customer</td>
                </tr>
                <tr style="color: #000000">
                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                        text-align: center">
                        <asp:DropDownList ID="cboCustomers" runat="server" AutoPostBack="True" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                            OnDataBound="cboCorporation_DataBound" Width="50%" OnSelectedIndexChanged="cboCustomers_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr style="color: #000000">
                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                        background-color: white; text-align: left">
                    </td>
                </tr>
            </table>
                </asp:View>
            </asp:MultiView>
            <asp:MultiView ID="MultiView3" runat="server">
                <asp:View ID="View4" runat="server">
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
            <label for="inputAddress2" class="form-label">Email</label>
            <asp:TextBox ID="txtConfirmEmail" runat="server" class="form-control"></asp:TextBox>
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
  
          <ajaxToolkit:UpdatePanel ID="UpdatePanel" runat="server">
                                        <ContentTemplate>
            <div class="row">
            <div class="col-md-6">
                <label for="inputEmail5" class="form-label">Customer Code</label>
                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"
                                                    Enabled="False"></asp:TextBox>
            </div>
               <div class="col-md-6">
                <label for="inputEmail5" class="form-label">Customer Name</label>
                <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"
                                                    Enabled="False"></asp:TextBox>
            </div>

        <div class="col-6">
          <label for="inputEmail5" class="form-label">User category</label>
           <asp:DropDownList ID="cboUserType" class="form-select" runat="server" AutoPostBack="True" OnDataBound="cboUserType_DataBound"  OnSelectedIndexChanged="cboUserType_SelectedIndexChanged"> </asp:DropDownList>
        </div>
        
        <div class="col-md-6">
          <label for="inputAddress5" class="form-label">UserType</label>
         <asp:DropDownList ID="cboAccessLevel" runat="server" OnDataBound="cboAccessLevel_DataBound" class="form-select"  OnSelectedIndexChanged="cboAccessLevel_SelectedIndexChanged" > </asp:DropDownList>
        </div>
       
        <div class="col-md-6">
             <label for="inputAddress2" class="form-label">Access Level</label>
             <asp:DropDownList ID="cboFileLevel" runat="server" OnDataBound="cboFileLevel_DataBound"
                                                             class="form-select">
                                                        </asp:DropDownList>
        </div>

        <div class="col-md-6">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Active</label>
            <asp:CheckBox runat="server" type="checkbox" ID="chkIsActive" CssClass="form-check-input" />
         
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

            <div class="col-md-12 mb-2 mt-2 text-center">
                <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary w-50" Text="SAVE USER"  Font-Bold="True" OnClick="btnOK_Click" style="font: menu" />
            </div>


   </ContentTemplate>
     </ajaxToolkit:UpdatePanel>
        </div>
  
      
    </div>
  </div>
  </div>
</div>


               <%-- <table style="width: 90%" align="center">
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                    <table style="width: 98%" align="center" cellpadding="0" cellspacing="0" >
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    User Details</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left" >
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    System Accessiblity Details</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                CustomerName</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtCustName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%" Enabled="False"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Customer Code</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%" Enabled="False"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                First Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="TxtFname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Middle Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Last Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtLname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Email</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Confirm Email</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtConfirmEmail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Phone</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtphone" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Job Role</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 4%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="InterfaceTextboxMultiline"
                                                            Height="22px" TextMode="MultiLine" Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <ajaxToolkit:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px">
                                                        User Category</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:DropDownList ID="cboUserType" runat="server" AutoPostBack="True" OnDataBound="cboUserType_DataBound"
                                                            OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Width="60%" style="font: menu" Enabled="False">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px; height: 20px;">
                                                        User Type</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:DropDownList ID="cboAccessLevel" runat="server" OnDataBound="cboAccessLevel_DataBound"
                                                            OnSelectedIndexChanged="cboAccessLevel_SelectedIndexChanged" Width="60%" style="font: menu">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px">
                                                        Access Level</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:DropDownList ID="cboFileLevel" runat="server" OnDataBound="cboFileLevel_DataBound"
                                                             Width="60%" style="font: menu">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px; height: 20px;">
                                                Is Active</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Font-Bold="True" Text="Tick" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px">
                                                        Is Logged on</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:CheckBox ID="chkIsLoggedon" runat="server" Font-Bold="True" Text="Tick" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="width: 69px">
                                                        Reset Password</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                    </td>
                                                    <td class="InterFaceTableRightRow">
                                                        <asp:CheckBox ID="chkResetPassword" runat="server" Font-Bold="True" Text="Tick" /></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                            <asp:MultiView ID="MultiView2" runat="server">
                                <asp:View ID="View3" runat="server">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 40%">
                                        <tr>
                                            <td class="InterFaceTableRightRow" colspan="3" style="height: 5px; text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                UserName</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="SAVE USER" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>--%>
                </asp:View>
            </asp:MultiView></asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center; height: 20px;">
                        <asp:Label ID="lblQn" runat="server" Font-Bold="True" ForeColor="Maroon" Text="."></asp:Label><asp:Button
                            ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes" /><asp:Button ID="btnNo"
                                runat="server" OnClick="btnNo_Click" Text="No" /></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
    <asp:Label ID="lblusername" runat="server" Text="." Visible="False"></asp:Label>

</asp:Content>

