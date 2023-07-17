<%@ page language="C#" masterpagefile="~/Beneficiary.master" autoeventwireup="true" inherits="General_ViewUsers, App_Web_2y4p7bji" title="CUSTOMER BENEFICIARIES" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                            text-align: center">
                            CUSTOMER BENEFICIARIES TO Approve</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 95%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 23%; height: 18px;
                            text-align: center">
                            Search String(Names)</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 19%; height: 18px;
                            text-align: center">
                            Phone Number</td>
                        <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 171px;
                            height: 18px; text-align: center">
                            LOCATION</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 18px;
                            text-align: center" colspan="2">
                            Beneficary Type</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 23%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtSearch" runat="server" style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 19%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:TextBox ID="txtSearchPhone" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 171px; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtdistrict" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            &nbsp;<asp:DropDownList ID="cboBeneficiaryType" runat="server" CssClass="InterfaceDropdownList"
                                OnDataBound="cboBeneficiaryType_DataBound" Width="85%" style="font: menu">
                            </asp:DropDownList></td>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server"><table style="width: 100%">
                        <tr>
                            <td style="width: 98%; height: 2px">
                                <asp:Button ID="btnApprove" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                    OnClick="btnApprove_Click" Text="APPROVE" Width="150px" />
                                <asp:Button ID="btnReject" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                     Text="REJECT " Width="150px" OnClick="btnReject_Click" /></td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 2px; text-align:right;">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Cambria"
                                    Font-Size="12pt" OnCheckedChanged="chkTransactions_CheckedChanged" Text="Select All Beneficiaries" /></td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 5px">
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
                        <asp:BoundColumn DataField="RecordID" HeaderText="Beneficary ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode" Visible="False">
                            <HeaderStyle Width="5%" />
                            
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="FullName" HeaderText="Edit"
                            Text="FullName" Visible="false">
                            <HeaderStyle Width="13%" />
                              <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue"  Width="13%" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Title" HeaderText="Title">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Telephone">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TypeName" HeaderText="Type">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Location" HeaderText="Location">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordedBy" HeaderText="Created By" Visible="true">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Created" DataField="Date">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# DataBinder.Eval(Container, "DataItem.Approved") %>'
                                            Width="5%" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%" />
                                </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></td>
                        </tr>
                    </table>
                    </asp:View>
                    &nbsp;
                </asp:MultiView></td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>

