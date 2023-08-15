<%@ page language="C#" masterpagefile="~/NewReconcilIation.master" autoeventwireup="true" inherits="ReconciledExceptions, App_Web_zulwb1bx" title="RECONCILIATION EXCEPTIONS" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <section class="section">
        <div class="text-center">
            <h5 class="card-title"> RECONCILIATION EXCEPTIONS</h5>
        </div>
    </section>

            
    <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Bank</label>
<asp:DropDownList ID="cboBank" runat="server" CssClass="form-select"
                                 AutoPostBack="true" OnSelectedIndexChanged="cboBank_SelectedIndexChanged">
                            </asp:DropDownList>
          </div>
    
       <div class="col-md-2">
            <label for="UserCategory" class="form-label">OVA Account</label>
               <asp:DropDownList ID="ddOva" runat="server" CssClass="form-select"
                                OnDataBound="ddOva_DataBound">
                            </asp:DropDownList>
          </div>
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Recon Type</label>
 <asp:DropDownList ID="ddReconType" runat="server" CssClass="form-select"
                               >
                                <asp:ListItem Selected="True">ALL</asp:ListItem>
                                <asp:ListItem Value="0">UNRECONCILED</asp:ListItem>
                                <asp:ListItem Value="1">RECONCILED</asp:ListItem>
                            </asp:DropDownList>
          </div>
       

     <div class="col-md-2">
            <label for="UserCategory" class="form-label">Telecom/Bank</label>
                <asp:TextBox ID="txtBankRef" runat="server" class="form-control"></asp:TextBox>
          </div>

      <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div> 
   
    </div>

<div class="col-lg-6 mt-3" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">TO Date</label>
                <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>

         <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
           <asp:Button ID="btnOK" runat="server"  OnClick="btnOK_Click" CssClass="btn btn-primary w-75" Style="margin-top:18px;"
                                Text="Search"  />
          </div>
    </div>
</div>





                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                                            <section class="section">

           <div class="row" style="justify-content:center">
    <div class="col-lg-6" style="display:flex;">
        <div class="col-lg-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf" runat="server"   Font-Bold="True" GroupName="FileFormat" Text="PDF" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel" runat="server"  Font-Bold="True" GroupName="FileFormat"  Text="EXCEL" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-primary w-100" OnClick="btnConvert_Click" Font-Bold="True"   Text="COVERT" />
        </div>
       
    </div>
</div>

    </section>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                               <section class="section">
        <div class="text-center">
            <h5 class="card-title">ADD A COMMENT</h5>
        </div>
    </section>
                            <div class="row mb-2 justify-content-center">
<div class="col-lg-10" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Record Id</label>
 <asp:TextBox ID="txtId" runat="server" class="form-control"></asp:TextBox>
          </div>
    
       <div class="col-md-2">
            <label for="UserCategory" class="form-label">Transaction Id</label>
         <asp:TextBox ID="txtTranId" runat="server" class="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Comment</label>
 <asp:TextBox ID="txtComment" runat="server" class="form-control"></asp:TextBox>
          </div>
       

     <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
              <asp:Button ID="Button2" runat="server" class="btn btn-primary w-75"
              Text="Update" OnClientClick="return confirm('Do you really want to add a comment?');" OnClick="btnUpdate_Click" 
              Width="85px" style="font: menu" />
          </div>
    </div>
                                </div>







                    </asp:View>
                </asp:MultiView>
    

                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="No." HeaderText="No." Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="StatementId" HeaderText="StatementId"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Bank" HeaderText="Bank"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Account" HeaderText="Account"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PegPayCategory" HeaderText="Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BankId" HeaderText="TelecomId"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TranAmount" HeaderText="Amount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BankStatus" HeaderText="TelecomStatus"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TranDate" HeaderText="TranDate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="RecordDate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Reconciled" HeaderText="ReconStatus"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconDate" HeaderText="ReconDate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconciledBy" HeaderText="ReconBy"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
        <div class="text-center">
              <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
    
        </div>
              
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

