<%@ Page Language="C#" MasterPageFile="~/NewAccounts.master" AutoEventWireup="true" CodeFile="ResendCreditReceipts.aspx.cs" Inherits="ResendCreditReceipts" Title="RESEND CREDIT RECEIPTS" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <section class="section">
        <div class="text-center">
            <h5 class="card-title">RESEND CUSTOMER CREDIT RECEIPTS</h5>
        </div>
    </section>
    
    <div class="row mb-4 justify-content-center">
<div class="col-lg-10" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Customer Name</label>
 <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Account Number</label>
       <asp:TextBox ID="txtCustAccount" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Phone</label>
                <asp:TextBox ID="txtPhone" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
                <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    </div>

    <div class="col-md-2">
        <asp:Button ID="btnOK" runat="server"   OnClick="btnOK_Click" CssClass="btn btn-primary w-75" style="margin-top:20px;"
                                Text="Search" />
    </div>

    
              </div>


        
                <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
           

              <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
            
                        <div class="row mb-3 text-center">
                            <div class="col-md-2">
                                  <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success  w-75"
                                    OnClick="btnApprove_Click" Text="Resend" Width="150px" />
                            </div>
                            <div class="col-md-2">
                                
                                <asp:Button ID="btnReject" runat="server"  CssClass="btn btn-danger  w-75"
                                     Text="REJECT"  />
                            </div>
                           
                     
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Cambria"
                                    Font-Size="12pt" OnCheckedChanged="chkTransactions_CheckedChanged" Text="Select All Credits"  Visible="False"/>

                        </div>
                               
               
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="RecordID" HeaderText="Beneficary ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Company" HeaderText="Customer Name" Visible="False">
                            <HeaderStyle Width="5%" />
                            
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Company" HeaderText="Customer Name">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerAccount" HeaderText="Customer Account">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Credit Amount">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Approved" HeaderText="Approved">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Network" HeaderText="Network">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreatedBy" HeaderText="Created By" Visible="true">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Reason" DataField="Reason">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="CreationDate" DataField="Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="TelecomId" DataField="TelecomId">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# DataBinder.Eval(Container, "DataItem.Approve") %>'
                                            Width="5%" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%" />
                                </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
                      
                    </asp:View>
                   
                </asp:MultiView>
    
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>

