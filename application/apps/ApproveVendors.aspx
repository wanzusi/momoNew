<%@ Page Language="C#" MasterPageFile="~/NewAccounts.master" AutoEventWireup="true"
    CodeFile="ApproveVendors.aspx.cs" Inherits="ApproveVendors" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
    <section class="section">
        <div class="text-center">
            <h5 class="card-title">APPROVE AGENTS</h5>
        </div>

    </section>

 <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Agent Code</label>
            
              <asp:TextBox ID="txtCustName" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
           <asp:TextBox ID="txtfromDate" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
            <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
             <asp:TextBox ID="txttoDate" runat="server"  class="form-control"></asp:TextBox>
          </div>
           
          
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-75" style="margin-top:18px;" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>

  <section class="section">

                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                                  <div class="row mb-4">
                                      <div class="col-md-2">
                                    <asp:Button ID="btnApprove" runat="server"  CssClass="btn btn-primary w-75"
                                        OnClick="btnApprove_Click" Text="APPROVE"  />
                                      </div>
                                      <div class="col-md-2">
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Cambria"
                                        Font-Size="12pt" OnCheckedChanged="chkTransactions_CheckedChanged" Text="Select All Agents"
                                        Visible="False" />
                                          </div>
                            </div>

                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                                        CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnPageIndexChanged="DataGrid1_PageIndexChanged"
                                        Width="100%" Style="text-align: justify; font: menu; border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;"
                                        Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" />
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="Vendorid" HeaderText="AGENT CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="VendorCode" HeaderText="AGENT CODE">
                                            </asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="Vendor" HeaderText="NAME">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ContactPerson" HeaderText="CONTACT PERSON">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VendorEmail" HeaderText="EMAIL">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Date" HeaderText="CREATED DATE">
                                            </asp:BoundColumn>
                                            
                                            <asp:TemplateColumn HeaderText="SELECT">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.Active") %>'
                                                        />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:DataGrid>
                       
                    </asp:View>
                   
                </asp:MultiView>
      
  </section>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
