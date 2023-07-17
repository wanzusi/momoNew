<%@ page language="C#" masterpagefile="~/ReportMaster.master" autoeventwireup="true" inherits="ViewPayBatches, App_Web_dl5sbboj" title="VIEW PAYMENT BATCHES" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    &nbsp;<table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            Payment Batches</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
                <asp:Label ID="lblBatchCode" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 96%; height: 2px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 96%; height: 5px">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 90%; border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                            <tr>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    Batch Type</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                    text-align: center">
                                    Batch Status</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 18%; height: 18px;
                                    text-align: center">
                                    bATCH No</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 20%; height: 18px;
                                    text-align: center">
                                    FROM DAte</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                    text-align: center">
                                    To date</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                    text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center;
                                    height: 1px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:DropDownList ID="cboType" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                        OnDataBound="cboType_DataBound" Width="90%">
                                    </asp:DropDownList>&nbsp;</td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:DropDownList ID="cboBatchStatus" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                        OnDataBound="cboBatchStatus_DataBound" Width="90%">
                                    </asp:DropDownList></td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 18%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:TextBox ID="txtBatchCode" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                </td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                        Text="Search" Width="85px" Style="font: menu" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 96%; height: 1px;">
                        <hr />
                        <asp:Label ID="tranStatusLbl" runat="server" Visible="False"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 96%; height: 1px">
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View3" runat="server">
                                <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                                    border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                                    <tr>
                                        <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                            width: 100px; border-bottom: #617da6 1px solid">
                                            <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                Text="PDF" /></td>
                                        <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                            width: 100px; border-bottom: #617da6 1px solid">
                                            <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                Text="EXCEL" /></td>
                                        <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                            width: 100px; border-bottom: #617da6 1px solid">
                                            <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnConvert_Click"
                                                Style="font: menu" Text="Convert" Width="85px" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView></td>
                </tr>
                <tr>
                    <td style="width: 96%; height: 2px">
                        &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify"
                            PageSize="100">
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
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnView" DataTextField="BatchCode" HeaderText="SUCCESSFUL"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="btnView1" DataTextField="BatchCode" HeaderText="FAILED"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="btnView2" DataTextField="BatchCode" HeaderText="PENDING"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                 <asp:ButtonColumn CommandName="btnView3" DataTextField="BatchCode" HeaderText="EXCLUDED"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="STATUS"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total" HeaderText="AMOUNT">
                                    <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Cleared" HeaderText="CLEARED">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RecordedBy" HeaderText="UPLOADED BY">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Date" HeaderText="DATE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 96%; height: 2px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 96%; height: 2px">
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 2px">
                        Batch Details
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 5px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:MultiView ID="MultiView5" runat="server">
                            <asp:View ID="View7" runat="server">
                                <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                                    border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                                    <tr>
                                        <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                            width: 100px; border-bottom: #617da6 1px solid">
                                            <asp:RadioButton ID="rdPdf2" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                Text="PDF" /></td>
                                        <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                            width: 100px; border-bottom: #617da6 1px solid">
                                            <asp:RadioButton ID="rdExcel2" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                Text="EXCEL" /></td>
                                        <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                            width: 100px; border-bottom: #617da6 1px solid">
                                            <asp:Button ID="btnConvertDetails" runat="server" Font-Size="9pt" Height="23px" Style="font: menu"
                                                Text="Convert" Width="85px" OnClick="btnConvertDetails_Click" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:Button ID="btnReturn" runat="server" Font-Bold="True" OnClick="btnReturn_Click"
                            Text="RETURN" Width="100px" /></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        &nbsp;<asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid2_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify"
                            PageSize="50">
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
                                <asp:BoundColumn DataField="BatchNo" HeaderText="BatchNo" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="CONTACT">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficiaryName" HeaderText="NAME">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PaymentNo" HeaderText="Payment Number">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranCharge" HeaderText="Pegasus Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PayOutFee" HeaderText="MNO Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CashOutFee" HeaderText="CashOut Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalCharge" HeaderText="Total Charge">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                
                                <asp:BoundColumn DataField="Status" HeaderText="Status">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp; &nbsp;
                        <asp:Label ID="lblPegasusTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblMnoFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblCashoutFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblAllTotal" runat="server" Font-Bold="True" Text="."></asp:Label></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>&nbsp;
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View4" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 1px;">
                        Rejected Batches</td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:DataGrid ID="DataGrid3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid3_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
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
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BatchID" HeaderText="BatchID" Visible="False">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnView" DataTextField="BatchCode" HeaderText="VIEW DETAILS"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="STATUS" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total" HeaderText="AMOUNT">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RecordedBy" HeaderText="UPLOADED">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Date" HeaderText="DATE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 1px">
                        Rejected Batch Details</td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:DataGrid ID="DataGrid4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
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
                                <asp:BoundColumn DataField="BatchNo" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="CONTACT" Visible="true">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficiaryName" HeaderText="NAME">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranCharge" HeaderText="Charge">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:Label ID="lblShowTotal" runat="server" Font-Bold="True" Text="."></asp:Label></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView4" runat="server">
        <asp:View ID="View6" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 98%; height: 1px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 1px">
                        Batch Audit trail</td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
            </table>
            <asp:DataGrid ID="DataGrid5" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
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
                    <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="No." HeaderText="NO.">
                        <HeaderStyle Width="5%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Comment" HeaderText="COMMENT" Visible="true">
                        <HeaderStyle Width="20%" />
                        <ItemStyle Width="20%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RecordedBy" HeaderText="RECORDED BY">
                        <HeaderStyle Width="25%" />
                        <ItemStyle Width="25%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Date" HeaderText="DATE/TIME">
                        <HeaderStyle Width="20%" />
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid></asp:View>
    </asp:MultiView>&nbsp;
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    &nbsp;
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <asp:Label ID="Label1" runat="server" Text="0" Visible="False"></asp:Label><br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False"></CR:CrystalReportViewer>
</asp:Content>
