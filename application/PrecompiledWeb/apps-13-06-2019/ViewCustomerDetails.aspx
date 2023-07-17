<%@ page language="C#" masterpagefile="~/Customers.master" autoeventwireup="true" inherits="RegisterCustomer, App_Web_owlh0qvh" title="Register Customer" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; text-align: center; width: 100%;">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                        style="width: 90%">
                                        <tr style="color: #000000">
                                            <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center; height: 20px;">
                                                APPROVE CUSTOMER DETAILS</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; height: 2px; text-align: center; width: 664px;">
                                    <asp:MultiView ID="MultiView2" runat="server">
                                        <asp:View ID="View3" runat="server">
                                            <table id="TABLE1" align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 95%; border-bottom: #617da6 1px solid">
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                        text-align: center">
                                                        Search (Names)</td>
                                                    <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                        text-align: center">
                                                        Customer Type</td>
                                                    <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                        text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ddcolortabsline2" colspan="3" style="vertical-align: middle; height: 1px;
                                                        text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                        width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                        border-right-color: #617da6">
                                                        &nbsp;<asp:TextBox ID="txtSearch" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                        border-right-color: #617da6">
                                                        <asp:DropDownList ID="cboCustomerType" runat="server" OnDataBound="cboCustomerType_DataBound"
                                                            Style="font: menu" Width="90%">
                                                        </asp:DropDownList></td>
                                                    <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                        width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                        border-right-color: #617da6">
                                                        <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                                            Style="font: menu" Text="Search" Width="85px" />&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        &nbsp;
                                    </asp:MultiView>
                                    <asp:MultiView ID="MultiView3" runat="server">
                                        <asp:View ID="View4" runat="server">
                                            <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                                OnPageIndexChanged="DataGrid1_PageIndexChanged" Style="border-right: #617da6 1px solid;
                                                border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                                text-align: justify" Width="100%">
                                                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                <EditItemStyle BackColor="#999999" />
                                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                                    Mode="NumericPages" />
                                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                                <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID" HeaderText="CustomerID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID" HeaderText="CustomerId" Visible="False">
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" />
                                                    </asp:BoundColumn>
                                                    <asp:ButtonColumn CommandName="btnapprove" DataTextField="Approved" HeaderText="Approve"
                                                        Text="Approve">
                                                        <HeaderStyle Width="13%" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" ForeColor="Blue" Width="13%" />
                                                    </asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                                                        <HeaderStyle Width="30%" />
                                                        <ItemStyle Width="30%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CustomerType" HeaderText="CustomerType">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Active" HeaderText="Active">
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Approved" HeaderText="Approved" Visible="True">
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RecordedBy" HeaderText="CreatedBy">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Date" HeaderText="Created">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                            </asp:DataGrid></asp:View>
                                    </asp:MultiView></td>
                            </tr>
           <tr>
               <td style="padding-bottom: 10px; vertical-align: top; width: 664px; height: 2px;
                   text-align: center">
               </td>
           </tr>
           <tr>
               <td style="padding-bottom: 10px; vertical-align: top; width: 664px; height: 2px;
                   text-align: center">
               </td>
           </tr>
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; text-align: center; width: 100%;">
                                    <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="View1" runat="server">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                                        <tr style="color: #000000">
                                            <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; text-align: center; height: 19px;">
                                                RETAIL ACCOUNT</td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td colspan="4" style="vertical-align: top; background-color: white; text-align: center">
                                                </td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                            text-align: left; height: 27px; width: 83px;">
                                                First
                                                Name</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 27px; width: 143px;">
                                                <asp:TextBox ID="txtFname" runat="server" Enabled="False" ></asp:TextBox></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                            text-align: left; height: 27px; width: 64px;">
                                                PassPort</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%; height: 27px;">
                                                <asp:TextBox ID="txtPassport" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                width: 83px; height: 27px; text-align: left">
                                                Last Name</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 143px;
                                                height: 27px; text-align: left">
                                                <asp:TextBox ID="txtLname" runat="server" Enabled="False"></asp:TextBox></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                width: 64px; height: 27px; text-align: left">
                                            </td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                                height: 27px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                            text-align: left; width: 83px;">
                                                Email</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 143px;">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                            text-align: left; width: 64px;">
                                                Driving Permit</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtDrivingPermit" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                            text-align: left; width: 83px; height: 24px;">
                                                Confirm Email</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 143px; height: 24px;">
                                                <asp:TextBox ID="txtEmailReconf" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                            text-align: left; width: 64px; height: 24px;">
                                                OtherID</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%; height: 24px;">
                                                <asp:TextBox ID="txtOtherID" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" EnableViewState="False"></asp:TextBox></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; width: 83px;">
                                                Gender</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 143px;">
                                                <asp:RadioButtonList ID="rbnGender" runat="server" RepeatDirection="Horizontal"
                                                    Width="83px" Enabled="False">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; width: 64px;">
                                                Place of Work</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtplaceofwork" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                    Height="30px" TextMode="MultiLine" EnableViewState="False"></asp:TextBox></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                width: 83px; text-align: left; height: 38px;">
                                                Phone</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 143px;
                                                text-align: left; height: 38px;">
                                                <asp:TextBox ID="txtphone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                width: 64px; text-align: left; height: 38px;">
                                                Access Level</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                                text-align: left; height: 38px;">
                                                <asp:DropDownList ID="cboAccessLevel" runat="server" Enabled="False" OnDataBound="cboAccessLevel_DataBound"
                                                     Style="font: menu"
                                                    Width="60%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 29px; width: 83px;">
                                                Address</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 29px; width: 143px;">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                    Height="30px" TextMode="MultiLine" Enabled="False"></asp:TextBox></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 29px; width: 64px;">
                                                Active</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%; height: 29px;">
                                                <asp:CheckBox ID="chkActive" runat="server" Enabled="False" /></td></tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 38px; width: 83px;">
                                                User Type</td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 38px; width: 143px;"><asp:DropDownList ID="cboUserType" runat="server" Enabled="False" OnDataBound="cboUserType_DataBound"
                                              Style="font: menu"
                                                    Width="60%">
                                            </asp:DropDownList></td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; width: 64px; height: 38px;">
                                                </td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%; height: 38px;"></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 9px; width: 83px;">
                                                </td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 9px; width: 143px;">
                                                </td>
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 9px; width: 64px;">
                                                </td>
                                            <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                                text-align: left; height: 9px;">
                                                </td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts21" colspan="4" style="vertical-align: top; padding-top: 5px;
                                                height: 23px; text-align: center"><asp:DropDownList ID="cboCompany" runat="server" Enabled="False" OnDataBound="cboCompany_DataBound"
                                              Style="font: menu"
                                                    Width="60%" Visible="False">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center; height: 99px;">
                                                <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                Height="30px" Text="APPROVE CUSTOMER" Width="160px" OnClick="btnSave_Click" />&nbsp;<br />
                                                <asp:Label ID="lblun" runat="server" Font-Bold="True" ForeColor="#FF0000" Visible="False"></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                        </asp:View>
                                        &nbsp;
                                        <asp:View ID="View2" runat="server">
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                                                <tr style="color: #000000">
                                                    <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; text-align: center">
                                                        CORPORATE &nbsp;ACCOUNT</td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts21" colspan="4" style="vertical-align: top; height: 2px;
                                                        background-color: white; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                            text-align: left; height: 27px; width: 143px;">
                                                        Name</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 27px; width: 172px;">
                                                        <asp:TextBox ID="txtFname2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                            text-align: left; height: 27px;">
                                                        Contact</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%; height: 27px;">
                                                        <asp:TextBox ID="txtPhone2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                        text-align: left; width: 143px;">
                                                        Contact Person</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 172px;">
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                        text-align: left">
                                                        Address</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                                        text-align: left">
                                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                            Height="30px" TextMode="MultiLine" Enabled="False"></asp:TextBox></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                            text-align: left; width: 143px; height: 24px;">
                                                        Email</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 172px; height: 24px;">
                                                        <asp:TextBox ID="txtEmail2" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                            text-align: left; height: 24px;">
                                                        Active</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 30%; height: 24px;">
                                                        <asp:CheckBox ID="chkActive2" runat="server" Enabled="False" /></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; width: 143px;">
                                                        Reconfirm Email</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; width: 172px;">
                                                        <asp:TextBox ID="txtEmailReconf2" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left">
                                                        Approve</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                                text-align: left"><asp:CheckBox ID="chkApprove2" runat="server" /></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 33px; width: 143px;">
                                                        Customer Type</td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 33px; width: 172px;">
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                            Enabled="False"></asp:TextBox></td>
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                                text-align: left; height: 33px;">
                                                        </td>
                                                    <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                                text-align: left; height: 33px;">
                                                        </td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center">
                                                        <asp:Button ID="Button2" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                Height="30px" Text="APPROVE CUSTOMER" Width="160px" OnClick="Button2_Click" />&nbsp;<br />
                                                        <asp:Label ID="lblun2" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; padding-top: 30px; text-align: center; width: 664px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; text-align: center; height: 71px; width: 664px;">
                                    <asp:Label ID="lblSave" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                                    <asp:Label ID="lblusername" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblpassword" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblIBAccountActive" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblEmailSent" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblMBAccountActive" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblNewPassword" runat="server" Text="Label" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblStatus" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <ajaxToolkit:ScriptManager ID="ScriptManager1" runat="server">
                                    </ajaxToolkit:ScriptManager>
                                    <asp:Label ID="Label4" runat="server" Text="0" Visible="False"></asp:Label></td>
                            </tr>
                        </table>

       <script type ="text/javascript">
 
 function Comma(Num)
 {
       Num += '';
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       x = Num.split('.');
       x1 = x[0];
       x2 = x.length > 1 ? '.' + x[1] : '';
       var rgx = /(\d+)(\d{3})/;
       while (rgx.test(x1))
       x1 = x1.replace(rgx, '$1' + ',' + '$2');
       return x1 + x2;
 } 
    
   </script> 

    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtphone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    <br />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPhone2" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
</asp:Content>

