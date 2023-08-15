<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="ViewVendors, App_Web_rgb2gjqg" title="VENDORS" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
  <section class="section">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">

<div class="row">
<div class="col-lg-12" style="display: flex;">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">User Details</h5>
  
        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
            
              <asp:TextBox ID="txtSearch" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="inputPassword5" class="form-label">Active</label>
            <asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="Tick" />
          </div>
          <div class="col-md-4">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary" OnClick="btnOK_Click"
                                Text="Search" />

          </div>

            <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="Vendorid" HeaderText="Vendorid" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode" Visible="False">
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit"  DataTextField="VendorCode" HeaderText="Edit"
                            Text="VendorCode" Visible="false">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="btnAddKYC" DataTextField="VendorCode" HeaderText="Add KYC"
                            Text="VendorCode">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="Vendor" HeaderText="Agent Name">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ContactPerson" HeaderText="Contact Name">
                            <HeaderStyle Width="30%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreatedBy" HeaderText="CreatedBy">
                            <HeaderStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreationDate" HeaderText="Date">
                            <HeaderStyle Width="25%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>

            </div>
          </div>
</div>
    </div>
    </div>
                

                        </asp:View>
                </asp:MultiView>
       </section>
</asp:Content>

