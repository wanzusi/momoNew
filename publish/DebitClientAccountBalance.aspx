<%@ page language="C#" masterpagefile="~/NewAccounts.master" autoeventwireup="true" inherits="DebitClientAccountBalance, App_Web_d1f0fhhh" title="Debit-Client-AccountBalance" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="section">
        <div class="text-center">
            <h5 class="card-title"> DEBIT CLIENT</h5>
        </div>
    </section>

    <section clas="section">
        <div class="row justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">

      
        <div class="col-md-2">
            <label for="inputState" class="form-label">Agent</label>
            <asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                            OnDataBound="cboVendor_DataBound" OnSelectedIndexChanged="cboVendor_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
          </div>

         <div class="col-md-2">
            <label for="inputState" class="form-label">Agent-Account Number</label>
               <asp:TextBox ID="txtAgentAccountNumber"  Enabled="False" runat="server" class="form-control"></asp:TextBox>
          </div>

          <div class="col-md-2">
            <label for="inputState" class="form-label">Agent-Account Balance</label>
               <asp:TextBox ID="txtAgentAccountBalance" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
          </div>

         <div class="col-md-2">
            <label for="inputState" class="form-label">Amount To Debit</label>
             <asp:TextBox ID="txtAgentDebitAmount"  Font-Bold="True" runat="server" onkeyup = "javascript:this.value=Comma(this.value);" CssClass="form-control" ForeColor="#C00000"></asp:TextBox>
          </div>
    </div>
    <div class="col-lg-12 justify-content-evenly d-flex mt-4">

   
    
         <div class="col-md-2">
            <label for="inputState" class="form-label">Pegasus Account Name</label>
             <asp:DropDownList ID="cboPegasusAccountName" runat="server" CssClass="form-select"
                               OnDataBound="cboPegasusAccountName_DataBound" OnSelectedIndexChanged="cboPegasusAccountName_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
          </div>
         <div class="col-md-2">
            <label for="inputState" class="form-label">Pegasus Account Number</label>
              <asp:TextBox ID="txtPegasusAccountNo"  Enabled="False" runat="server" class="form-control"></asp:TextBox>
                            
          </div>
        
            <div class="col-md-2">
            <label for="inputState" class="form-label">Pegasus Account Balance</label>
                    <asp:TextBox ID="txtPegasusAccountBalance" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="inputState" class="form-label">Network</label>
               <asp:TextBox ID="cboNetwork" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
   
      </div>


              <div class="col-lg-2 text-center mt-4" >
              
              <div class="col-md-2">
                  <asp:Button ID="btnDebit" runat="server" OnClick="btnDebit_Click"
                                Text="DEBIT" class="btn btn-primary" Width="125" 
                                OnClientClick="return confirm('Do you really want to debit the client?');" />

              </div>
                 
          </div>
   
    </div>
    </section>

    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                            
                            ValidChars = ",0123456789"
                             
                            TargetControlID="txtAgentDebitAmount">
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

