<%@ Page Language="C#" MasterPageFile="~/PaymentMaster.master" AutoEventWireup="true" CodeFile="ViewInvoices.aspx.cs" Inherits="ViewInvoices" Title="VIEW INVOICES" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <section class="section">
        <div>
            <h5 class="card-title">VIEW INVOICES</h5>
        </div>

                   <div class="col-10" style="margin-right:auto; margin-left:auto; justify-content:space-evenly">
            <div class="row">
                <div class="col-2">
                    <label for="inputEmail5" class="form-label">Payment Type</label>
               <asp:DropDownList ID="cboPaymentType" runat="server" CssClass="InterfaceDropdownList"
                                class="form-select" style="font: menu" OnDataBound="cboPaymentType_DataBound">
                            </asp:DropDownList>

                </div>
                 <div class="col-2">
                  
                     <label for="inputEmail5" class="form-label">Invoice No</label>
                   <asp:TextBox ID="txtReceiptno" runat="server" Style="font: menu" class="form-control"></asp:TextBox>

                </div>
                  <div class="col-2">
                    
                        <label for="inputEmail5" class="form-label">Customer Name</label>
                    <asp:TextBox ID="txtname" runat="server" Style="font: menu" CssClass="form-control" ></asp:TextBox>

                </div>
                <div class="col-2">
                   
                     <label for="inputEmail5" class="form-label">From Date</label>
                  <asp:TextBox ID="txtfromDate" runat="server" class="form-control" Style="font: menu"></asp:TextBox>

                </div>
                <div class="col-2">
                  
                     <label for="inputEmail5" class="form-label">To Date</label>
                     <asp:TextBox ID="txttoDate" runat="server" class="form-control" Style="font: menu"></asp:TextBox>

                </div>
               

               <div class="col-2">
                   <label for="inputEmail5" class="form-label" >.</label>
                <asp:Button ID="btnOK" runat="server"   Text="Search" class="btn btn-primary w-100" onClick="btnOK_Click" />
                 </div>

            </div>

        </div>
          <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify" AllowPaging="True">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SerialNo" HeaderText="Invoice No.">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnPrint" DataTextField="SerialNo" HeaderText="Print Invoice"
                            Text="SerialNo" Visible="False">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="CustName" HeaderText="Name">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PaymentType" HeaderText="Type">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                            <HeaderStyle Width="15%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            <ItemStyle Width="15%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AmountPaid" HeaderText="Paid">
                            <HeaderStyle Width="15%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                      <ItemStyle Width="15%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="balance" HeaderText="Balance">
                            <HeaderStyle Width="15%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                               <ItemStyle Width="15%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Cleared" HeaderText="Cleared">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                    </Columns>
                    
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
         <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>

    </section>

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
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

