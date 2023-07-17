<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="AddPosOwnerKYC, App_Web_qrlbwxxq" title="Untitled Page" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center; height: 50px;">
                &nbsp;<table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                    <tr>
                        <td style="vertical-align: middle; height: 41px; text-align: center">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        CREATE/EDIT AGENT KYC</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table align="center" style="width: 90%">
                                    <tr>
                                        <td style="width: 100%; height: 2px; text-align: center">
                                            <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                                <tr>
                                                    <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                        Customer DETAILS</td>
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
                                                                        Customer DETAILS</td>
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
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                                    Code</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtVendorCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%" Enabled="False"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                                    First Name</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtFname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 175px;">
                                                                    Last Name</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtLname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                                    OtherName</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtOtherName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                                    Date of Birth</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtDateofBirth" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                                    Contact One</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtcontact1" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                                    Contact Two</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtContact2" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                                    Gender</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:RadioButtonList ID="rbnGender" runat="server" BackColor="white" Font-Bold="True"
                                                                        RepeatDirection="Horizontal" Width="60%">
                                                                        <asp:ListItem>MALE</asp:ListItem>
                                                                        <asp:ListItem>FEMALE</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                                    Nationality</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtNattionality" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px; height: 20px">
                                                                    Address</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 175px">
                                                                    Email</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                    </td>
                                                    <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 103px;">
                                                                    CustomerType</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:DropDownList ID="cboCustomerType" runat="server" AutoPostBack="False" OnDataBound="cboCustomerType_DataBound"
                                                                        OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Style="font: menu"
                                                                        Width="60%">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 103px;">
                                                                    Business Type</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:DropDownList ID="cboBusinessType" runat="server" AutoPostBack="False" OnDataBound="cboBusinessType_DataBound"
                                                                        OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Style="font: menu"
                                                                        Width="60%">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    Trading Name</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtTradingName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    Company Reg No</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtCompanyReg" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    TIN</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtTin" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    Region</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtRegion" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    District</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    CustomerID Type</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:DropDownList ID="cboCustomerIdType" runat="server" AutoPostBack="False" OnDataBound="cboCustomerIdType_DataBound"
                                                                        OnSelectedIndexChanged="cboUserType_SelectedIndexChanged" Style="font: menu"
                                                                        Width="60%">
                                                                        <asp:ListItem Value="0">Select Id Type</asp:ListItem>
                                                                        <asp:ListItem Value="1">National Id</asp:ListItem>
                                                                        <asp:ListItem Value="2">Voter's Card</asp:ListItem>
                                                                        <asp:ListItem Value="3">Residentail Id</asp:ListItem>
                                                                        <asp:ListItem Value="4">Company Id</asp:ListItem>
                                                                        <asp:ListItem Value="5">Others</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px; height: 20px">
                                                                    CudtomerID No.</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtCustomerIdNo" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 103px">
                                                                    Is KYC Active</td>
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
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="width: 100%; height: 30px; text-align: center">
                                            <asp:Button ID="btnOK" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                OnClick="btnOK_Click" Style="font: menu" Text="SAVE DETAILS" Width="150px" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                            &nbsp;
                        </asp:MultiView>
                                    </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 30px; text-align: center; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; width: 870px; text-align: center">
                &nbsp;<asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblVendorCode" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
    </table>


</asp:Content>

