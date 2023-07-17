<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="ViewVendors, App_Web_i-fhi2me" title="VENDORS" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                                <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            POS ACCOUNT</td>
                    </tr>
                </table>
                <br />
                <table align="center" cellpadding="0" cellspacing="0" style="width: 70%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            Search String(Names)</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            Active</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="3" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtSearch" runat="server" style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="Tick" /></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" />&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
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
                </asp:DataGrid></td>
        </tr>
                        </table>
                        </asp:View>
                </asp:MultiView>
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                        <table align="center" style="width: 90%">
                            <tr>
                                <td style="width: 100%; height: 2px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                        <tr>
                                            <td class="InterfaceHeaderLabel">
                                                ADD/EDIT POS ACCOUNT</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 2px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                Device DETAILS</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                Device DETAILS</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                            Code</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px"><asp:DropDownList ID="cboVendorCode" runat="server" AutoPostBack="False" Style="font: menu"
                                                                Width="60%">
                                                            <asp:ListItem Value="0">--Select Device Type--</asp:ListItem>
                                                            <asp:ListItem Value="1">Phone</asp:ListItem>
                                                            <asp:ListItem Value="2">POS</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                            AgentName</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtAgentName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                            AgentAddress</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtAgentAddress" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                            AgentTelephone</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtAgentContact" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                            DeviceType</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:DropDownList ID="cboDeviceType" runat="server" AutoPostBack="False" Style="font: menu"
                                                                Width="60%">
                                                                <asp:ListItem Value="0">--Select Device Type--</asp:ListItem>
                                                                <asp:ListItem Value="1">Phone</asp:ListItem>
                                                                <asp:ListItem Value="2">POS</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                            DeviceId</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtDeviceId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                            Device Serial</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtDeviceSerial" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                            Device DataSim</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtDataSim" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 103px">
                                                            Is Active</td>
                                                        <td class="InterFaceTableMiddleRowUp">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Font-Bold="True" Text="Tick To Activate" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 103px">
                                                            User</td>
                                                        <td class="InterFaceTableMiddleRowUp">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtUser" runat="server" BackColor="#E0E0E0" CssClass="InterfaceTextboxLongReadOnly"
                                                                ReadOnly="True" Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                                <asp:Label ID="lblDeviceId" runat="server" Text="0" Visible="False"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 100%; height: 30px; text-align: center">
                                    <asp:Button ID="btnSave" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="btnSave_Click" Style="font: menu" Text="SAVE DETAILS" Width="150px" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
                <asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View3" runat="server">
                        <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                            <tr>
                                <td style="width: 98%; height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 5px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                        <tr>
                                            <td class="InterfaceHeaderLabel">
                                                AGENT DEVICE LIST</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 5px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 70%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 18px;
                            text-align: center" colspan="3">
                                                Customer DEVICES</td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="3" style="vertical-align: middle; text-align: center; height: 1px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;" colspan="3">
                                                &nbsp;<asp:Button ID="Button1" runat="server" Font-Size="9pt" Height="23px" OnClick="Button1_Click"
                                Text="Add Device" Width="85px" style="font: menu" />&nbsp;<asp:Button ID="btnReturn" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReturn_Click"
                                Text="Return" Width="85px" style="font: menu" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 1px;">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 2px">
                                    &nbsp;<asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="False"  AutoGenerateColumns="False"
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
                                    </asp:DataGrid></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>

    </table>
    <br />
    <br />
</asp:Content>

