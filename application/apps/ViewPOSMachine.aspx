<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ViewPOSMachine.aspx.cs" Inherits="ViewVendors" Title="VENDORS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="section">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
<div class="row">
<div class="col-lg-12" style="display: flex;">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">POS ACCOUNT</h5>
  
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


            </div>
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
                         <asp:BoundColumn DataField="OwnerId" HeaderText="Vendor Code">
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
                              <div class="row">
                                  <h5 class="card-title">ADD/EDIT POS ACCOUNT</h5>
<div class="col-lg-12" style="display: flex;">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Device Details</h5>
  

        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Code</label>
           <asp:DropDownList ID="cboVendorCode" runat="server" AutoPostBack="False" Style="font: menu" class="form-select" >
                                       <asp:ListItem Value="0">--Select Device Type--</asp:ListItem>
                                       <asp:ListItem Value="1">Phone</asp:ListItem>
                                       <asp:ListItem Value="2">POS</asp:ListItem>
                                                        </asp:DropDownList>
          </div>

          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">Agent Name</label>
           <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Agent Address</label>
          <asp:TextBox ID="txtAgentAddress" runat="server" CssClass="form-control"></asp:TextBox>

          </div>
            <div class="col-md-6" >
                <label for="inputAddress5" class="form-label">Agent Telphone</label>
                  <asp:TextBox ID="txtAgentContact" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

             
          <div class="col-md-6">
            <label for="inputEmail5" class="form-label">Code</label>
           <asp:DropDownList ID="cboDeviceType" runat="server" AutoPostBack="False" Style="font: menu" class="form-select" >
                                       <asp:ListItem Value="0">--Select Device Type--</asp:ListItem>
                                       <asp:ListItem Value="1">Phone</asp:ListItem>
                                       <asp:ListItem Value="2">POS</asp:ListItem>
                                                        </asp:DropDownList>
          </div>

          <div class="col-md-6">
            <label for="inputPassword5" class="form-label">Device Id</label>
           <asp:TextBox ID="txtDeviceId" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-6">
            <label for="inputAddress5" class="form-label">Device Serial</label>
          <asp:TextBox ID="txtDeviceSerial" runat="server" CssClass="form-control"></asp:TextBox>

          </div>
            <div class="col-md-6" >
                <label for="inputAddress5" class="form-label">Device DataSim</label>
                  <asp:TextBox ID="txtDataSim" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

                <div class="col-md-6" >
                <label for="inputAddress5" class="form-label">Is Active</label>
                <asp:CheckBox ID="chkIsActive" class="form-check-input" runat="server" Font-Bold="True" Text="Tick To Activate" />
            </div>

                <div class="col-md-6" >
                <label for="inputAddress5" class="form-label">User</label>
                  <asp:TextBox ID="txtUser" runat="server" BackColor="#E0E0E0" CssClass="form-control" ReadOnly="True" ></asp:TextBox>
            </div>

             <asp:Label ID="lblDeviceId" runat="server" Text="0" Visible="False" CssClass="form-label"></asp:Label>

            <div class="text-center">
                 <asp:Button ID="btnSave" runat="server" Font-Bold="True" CssClass="btn btn-primary"  OnClick="btnSave_Click" Style="font: menu" Text="Save Details"  />
            </div>


            </div>
          </div>
</div>
    </div>
    </div>
                        </asp:View>
                    </asp:MultiView>

                              
                <asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View3" runat="server">

               <div class="row">

<div class="col-lg-12" style="display: flex;">                 
  <div class="card" style="margin: 10px;">
    <div class="card-body">
      <h5 class="card-title">Agent Device List</h5>
        <p class="card-title">Customer Devices</p>
        <div class="text-center">

                             <asp:Button ID="Button1" runat="server" class="btn btn-primary " OnClick="Button1_Click"
                                Text="Add Device"  style="font: menu " />
                                  <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click"
                                Text="Return"  style="font: menu" class="btn btn-success"/>


        </div>
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

