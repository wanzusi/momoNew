<%@ Page Language="C#" MasterPageFile="~/NewAccounts.master" AutoEventWireup="true" CodeFile="CreditAccount.aspx.cs" Inherits="CreditAccount" Title="CREDIT ACCOUNTS" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <section class="section">
        <div class="text-center">
            <h5 class="card-title">CREDIT ACCOUNT </h5>
            <p class="card-text">Search For Company</p>
        </div>
    </section>
    <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
       <div class="row mb-2 justify-content-center">
<div class="col-lg-10" style="display: flex; justify-content:space-evenly">
          <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Company Name</label>
            
           <asp:TextBox ID="txtsearchName" runat="server"  CssClass="form-control" ></asp:TextBox>
          </div>
      <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Company Code</label>
       <asp:TextBox ID="txtSearchCode" runat="server"  CssClass="form-control" ></asp:TextBox>
 

         



            </div>
     <div class="col-md-3">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnSearch" runat="server" class="btn btn-primary w-50" style="margin-top:25px;" OnClick="btnSearch_Click"
                                Text="Search" />

          </div>
         

    </div>
           </div>

                                <div class="text-center">
                                    <h5 class="card-title">Select Company</h5>
                                    <div class="col-md-2">
                                         <asp:DropDownList ID="cboCompanyCode" runat="server" CssClass="form-select "
                                OnDataBound="cboCompanyCode_DataBound" AutoPostBack="True" OnSelectedIndexChanged="cboCompanyCode_SelectedIndexChanged">
                            </asp:DropDownList>
                                    </div>
                                </div>

                </asp:View>
    </asp:MultiView>

         <asp:MultiView ID="MultiView2" runat="server">
                                        <asp:View ID="View2" runat="server">
                                            <div class="row justify-content-center">
                                                <div class="col-lg-6">
    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Account Details</h5>
 
        <div class="row g-3">
          
          <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Company Name</label>
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
          </div>

            <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Company Code</label>
          <asp:TextBox ID="txtCompanyCode" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
          </div>

            <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Account Number</label>
          <asp:TextBox ID="txtAccNumber" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
          </div>

            <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Account Balance</label>
          <asp:TextBox ID="txtAccountBalance" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
          </div>

            <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Credit Amount</label>
          <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="form-control" onkeyup = "javascript:this.value=Comma(this.value);" Enabled="False"></asp:TextBox>
          </div>

             <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Network</label>
         <asp:DropDownList ID="cboNetwork" runat="server" CssClass="form-select"   OnDataBound="cboNetwork_DataBound">
                                                                                </asp:DropDownList>
          </div>
             <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Telecom ID</label>
          <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
             <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Reason</label>
          <asp:TextBox ID="txt_reason" runat="server" CssClass="form-control"></asp:TextBox>
          </div>

            <div class="col-md-6">
                <div class="col-md-3">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-100"  OnClick="btnSave_Click" Text="CREDIT ACCOUNT" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnCancel" runat="server" Text="RETURN" class="btn btn-danger w-100" OnClick="btnCancel_Click" />
                </div>
            </div>
            
                                                        

            </div>
          </div>
        </div>
          </div>
                                                </div>

                                              
                                            </asp:View>
                                            </asp:MultiView>
        <div class="container">
           <asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
                
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>

            </div>

             <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>

   
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                            
                            ValidChars = ",0123456789"
                             
                            TargetControlID="txtCreditAmount">
                        </ajaxToolkit:FilteredTextBoxExtender>
        <script type ="text/javascript">
 
 function Comma(Num)
 {
       Num += '';
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       x = Num.split('.');
       x1 = x[0];
       x2 = x.length > 1 ? '.' + x[1] : '';
       var rgx = /(\d+)(\d{3})/;
       while (rgx.test(x1))
       x1 = x1.replace(rgx, '$1' + ',' + '$2');
       return x1 + x2;
 } 
    
   </script> 
</asp:Content>





