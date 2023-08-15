<%@ Page Language="C#" MasterPageFile="~/NewReconciliation.master" AutoEventWireup="true" CodeFile="Reconciliation.aspx.cs" Inherits="Reconciliation" Title="MANUAL RECONCILIATION" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


         <section class="section">
        <div class="text-center">
            <h5 class="card-title">TRANSACTIONS TO RECONCILE</h5>
        </div>
    </section>

        
    <div class="row mb-2 justify-content-center">
<div class="col-lg-10" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Agent</label>
<asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                       OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Telecom</label>
  <asp:DropDownList ID="cboTelecoms" runat="server" CssClass="form-select"
                            OnDataBound="cboTelecoms_DataBound">
                            </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Agent Ref</label>
                <asp:TextBox ID="txtpartnerRef" runat="server" class="form-control"></asp:TextBox>
          </div>

     <div class="col-md-2">
            <label for="UserCategory" class="form-label">Telecom Ref</label>
                <asp:TextBox ID="txtSearch" runat="server" class="form-control"></asp:TextBox>
          </div>
    </div>

   <div class="col-lg-10" style="display: flex; justify-content:space-evenly">
    <div class="col-md-2">
         <div class="col-md-2">
            <label for="UserCategory" class="form-label">Transaction Type</label>
                <asp:DropDownList ID="cboTranType" runat="server" CssClass="form-select"
                                OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
                <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>

           <div class="col-md-2">
        <asp:Button ID="btnOK" runat="server"   OnClick="btnOK_Click" CssClass="btn btn-primary w-75" style="margin-top:20px;"
                                Text="Search" />
    </div>
    </div>

 

    
              </div>





  

                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">

                            <div class="row">
                            <div class="col-md-2">
                                  
                            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Bold="True"
                                OnCheckedChanged="CheckBox2_CheckedChanged" Text="Select All" />
                            </div>
                            <div class="col-md-2">
                                
                                <asp:Button ID="btnReconcile" runat="server" OnClick="btnReconcile_Click" CssClass="btn btn-primary w-75"
                                Text="RECONCILE TRANSACTIONS" />
                            </div>
                           
                     <div class="col-md-2">
                          <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True"
                                OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" />
                     </div>
                               

                        </div>
                        <div class="text-center">
                                     <asp:Label ID="lblTotal" runat="server" Text="." Font-Bold="True" ForeColor="#0000C0"></asp:Label>
                        </div>
               

                    </asp:View>
                </asp:MultiView></
       
                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="PegPayTranId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="false">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustName" HeaderText="Name">
                            <HeaderStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TransactionType" HeaderText="Tran Type">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Vendor" HeaderText="Agent">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TranAmount" HeaderText="Amount">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PayDate" HeaderText="Tran Date">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Str1" HeaderText="PegPay Id">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                         <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.Approved") %>'
                                            Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                    </Columns>
                    
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
       
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>

