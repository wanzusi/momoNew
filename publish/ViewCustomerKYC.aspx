<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="ViewVendors, App_Web_zulwb1bx" title="VENDORS" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="section">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
             <div class="row">
<div class="col-lg-6" style="display: flex;">
    <h5 class="card-title">AGENT KYC</h5>
        <div class="row g-3">
          
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
              <asp:TextBox ID="txtSearch" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Active</label>
                   <asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="Tick" />
          </div>
       
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-100" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
          </div>

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
                        <asp:BoundColumn DataField="RecordId" HeaderText="Customerid" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode" Visible="False">
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="VendorCode" HeaderText="Edit"
                            Text="VendorCode">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="btnAddDevice" DataTextField="VendorCode" HeaderText="Device List"
                            Text="VendorCode">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="FirstName" HeaderText="Agent Name">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Address" HeaderText="Address">
                            <HeaderStyle Width="30%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active" Visible="false">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreatedBY" HeaderText="CreatedBy">
                            <HeaderStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreationDate" HeaderText="Date">
                            <HeaderStyle Width="25%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
                </asp:View>
             </asp:MultiView>

        
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                        <div class="text-center">
                            <h4 class="card-title">ADD/EDIT AGENT DEVICE</h4>
                        </div>
                        <div class="row">
    <div class="col-lg-12" style="display: flex;">
    <h5 class="card-title">Device Details</h5>
        <div class="row g-3">
          
          <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Code</label>
             <asp:DropDownList ID="cboVendorCode" runat="server" AutoPostBack="False" Style="font: menu" CssClass="form-select"
                                                                >
                                                            <asp:ListItem Value="0">--Select Device Type--</asp:ListItem>
                                                            <asp:ListItem Value="1">Phone</asp:ListItem>
                                                            <asp:ListItem Value="2">POS</asp:ListItem>
                                                        </asp:DropDownList>
          </div>
          <div class="col-md-3">
            <label for="UserCategory" class="form-label">Agent Name</label>
                  <asp:TextBox ID="txtAgentName" runat="server" class="form-control"></asp:TextBox>
          </div>
       
          <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Agent Address</label>
          <asp:TextBox ID="txtAgentAddress" runat="server" CssClass="form-control"></asp:TextBox>

          </div>
             <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Agent Telephone</label>
            <asp:TextBox ID="txtAgentContact" runat="server" class="form-control"></asp:TextBox>

          </div>
              <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Device Type</label>
           <asp:DropDownList ID="cboDeviceType" runat="server" AutoPostBack="False" Style="font: menu" CssClass="form-select"
                                                                >
                                                                <asp:ListItem Value="0">--Select Device Type--</asp:ListItem>
                                                                <asp:ListItem Value="1">Phone</asp:ListItem>
                                                                <asp:ListItem Value="2">POS</asp:ListItem>
                                                            </asp:DropDownList>

          </div>
              <div class="col-md-3">
            <label for="inputAddress5" class="form-label">DeviceId</label>
            <asp:TextBox ID="txtDeviceId" runat="server" class="form-control"></asp:TextBox>

          </div>

                <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Device Serial</label>
            <asp:TextBox ID="txtDeviceSerial" runat="server" class="form-control"></asp:TextBox>

          </div>
                <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Device DataSim</label>
           <asp:TextBox ID="txtDataSim" runat="server" CssClass="form-control"></asp:TextBox>

          </div>
                <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Is Active</label>
         <asp:CheckBox ID="chkIsActive" CssClass="form-check" runat="server" Font-Bold="True" Text="Tick To Activate" />

          </div>

            <div class="col-md-3">
            <label for="inputAddress5" class="form-label">User</label>
          <asp:TextBox ID="txtUser" runat="server" BackColor="#E0E0E0" class="form-control"
                                                                ReadOnly="True" ></asp:TextBox>

          </div>
            <div class="label">
                   <asp:Label ID="lblDeviceId" runat="server" Text="0" Visible="False"></asp:Label>
            </div>
            
             <div class="col-md-3">
            <label for="inputAddress5" class="form-label"></label>
         <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-100" Font-Bold="True" OnClick="btnSave_Click" Style="font: menu" Text="SAVE DETAILS" />

          </div>


            </div>
          </div>
                        </div>

                    </asp:View>

                    </asp:MultiView>
        <asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View3" runat="server">
                        <div class="text-center">
                            <h4 class="card-title">AGENT DEVICE LIST</h4>
                            <h5 class="card-title">Customer Devices</h5>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="col-md-2">
                                          <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary w-100" OnClick="Button1_Click"
                                Text="Add Device" Width="85px" style="font: menu" />&nbsp;
                          
                                    </div>
                                    <div class=" col-md-2">
                                 <asp:Button ID="btnReturn" runat="server" class="btn btn-success w-100" OnClick="btnReturn_Click"
                                Text="Return" Width="85px" style="font: menu" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    
                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="False"  AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid2_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="RecordId" HeaderText="Id" Visible="False"></asp:BoundColumn>
                                             <asp:BoundColumn DataField="AgentId" HeaderText="DeviceId" Visible="False">
                                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OwnerId" HeaderText="OwnerId">
                                                <HeaderStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="AgentId" HeaderText="AgentId" Visible="False">
                                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="btnEdit" DataTextField="AgentId" HeaderText="Device Id"
                            Text="VendorCode" Visible="true">
                                                <HeaderStyle Width="15%" />
                                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="AgentName" HeaderText="Agent Name">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="AgentAddress" HeaderText="Address">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Active" HeaderText="Active" Visible="false">
                                                <HeaderStyle Width="5%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DeviceSerial" HeaderText="Device Serial">
                                                <HeaderStyle Width="15%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DeviceDataSim" HeaderText="Device DataSim">
                                                <HeaderStyle Width="20%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DeviceType" HeaderText="DeviceType">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CreatedBy" HeaderText="CreatedBy">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CreationDate" HeaderText="Date">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:DataGrid>
                    </asp:View>
            </asp:MultiView>

    </section>
</asp:Content>

