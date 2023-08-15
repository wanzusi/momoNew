<%@ Page Language="C#" MasterPageFile="~/NewPayments.master" AutoEventWireup="true" CodeFile="PayGoodsAndServices.aspx.cs" Inherits="PayGoodsAndServices" Title="GOOD AND SERVICES" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">

               <section class="section">
        <div class="text-center">
            <h5 class="card-title">ONLINE PAYMENT</h5>
        </div></section>

                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View2" runat="server">
       <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
              <div class="card mb-3">

                <div class="card-body">
                  <div class="text-center"> 
                      <h5 class="card-title">PURCHASE GOODs AND SERVICES</h5>
                  </div>
                  <div class="row g-3 needs-validation" novalidate runat="server" id="form4">

                    <div class="col-12">
                      <label for="otp" class="form-label">Select Service</label>
                 
                       <asp:DropDownList ID="cboService" runat="server" CssClass="form-select" OnSelectedIndexChanged="cboService_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">---Select Service---</asp:ListItem>
                                                                        <asp:ListItem Value="001">WebHosting</asp:ListItem>
                                                                        <asp:ListItem Value="002">AirUganda Ticket</asp:ListItem>
                                                                    </asp:DropDownList>
               
                    <div class="col-12">
                   <label for="otp" class="form-label">Select Option</label>
                            <asp:DropDownList ID="cboSubservice" runat="server" CssClass="form-select" OnDataBound="cboSubservice_OnDataBound" OnSelectedIndexChanged="cboSubservice_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                      </div>
                        
                    </div>

             <div class="col-12">
                   <label for="otp" class="form-label">Select Account</label>
                       <asp:DropDownList ID="cboCustomerAccount" runat="server" Width="35%" OnDataBound="cboCustomerAccount_OnDataBound" OnSelectedIndexChanged="cboCustomerAccount_SelectedIndexChanged">
                                                        </asp:DropDownList>

             </div>
                        <div class="col-12">
                   <label for="otp" class="form-label">Amount To Transfer</label>
                      <asp:TextBox ID="txtAmount" runat="server"  CssClass="form-control" Font-Bold="True" ForeColor="Red" Enabled="False" ></asp:TextBox>
                        
                    </div>

                      <div class="col-12">
                             <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-100" Font-Bold="True" OnClick="btnSave_Click" Text="Process Tranfer" />
                      </div>
                   
                  </div>

                </div>
              </div>
            </div>
          </div>
            </div>
      </section>
                            </asp:View>
                         
                            
                            <asp:View ID="View3" runat="server">
                                <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
              <div class="card mb-3">

                <div class="card-body">
                  <div class="text-center"> 
                      <h5 class="card-title">Confirm Payemt And Proceed</h5>
                      <h6 class="card-title">Funds Transfer Account Details</h6>
                  </div>
                  <div class="row g-3 needs-validation" novalidate runat="server" id="Div1">

                    <div class="col-12">
                      <label for="otp" class="form-label">Service</label>
                 
                     <asp:TextBox ID="txtViewService" runat="server" CssClass="form-select" Enabled="False"></asp:TextBox>
               
                    <div class="col-12">
                   <label for="otp" class="form-label">Sub Service</label>
                        <asp:TextBox ID="txtViewSubservice" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                      </div>
                        
                    </div>

             <div class="col-12">
                   <label for="otp" class="form-label">Select Account</label>
                     <asp:DropDownList ID="cboviewAccount" runat="server" class="form-select" OnDataBound="cboviewAccount_OnDataBound" Enabled="False">
                                                    </asp:DropDownList>
                        
                    </div>
                        <div class="col-12">
                   <label for="otp" class="form-label">Amount To Transfer</label>
                  <asp:TextBox ID="txtViewAmount" runat="server" CssClass="form-control " Enabled="False"  Font-Bold="True" ForeColor="Red"></asp:TextBox>
                        
                    </div>

                      <div class="col-12">
                          <div class="row justify-content-evenly">
                          <div class="col-md-4"><asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger w-50" Font-Bold="True" OnClick="btnCancel_Click" Text="NO"  /></div>
                           <div class="col-md-4">
                               <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-success ww-50" Font-Bold="True" OnClick="btnConfirm_Click" Text="YES" />
                             
                           </div>
                                 </div>                   
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
                        <asp:Label ID="lblCustName" runat="server" Visible="False"></asp:Label></td>
             
        </asp:View>
    </asp:MultiView>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                            TargetControlID="txtAmount" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
</asp:Content>

