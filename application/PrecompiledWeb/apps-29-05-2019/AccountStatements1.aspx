<%@ page language="C#" masterpagefile="~/ReportMaster.master" autoeventwireup="true" inherits="AccountStatements, App_Web_cdgctkdq" title="AccountStatements" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>        <td style="width: 38%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            ACCOUNT STATEMENTS
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 38%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 38%; height: 1px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AGENT</td>
                        
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            FROM DATE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 30%; height: 18px;
                            text-align: center">
                            TO DATE</td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="3" style="vertical-align: middle; text-align: center; height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" style="font: menu" OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList></td>
                        
                        
                            
                        <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 30%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            </td>
                    </tr>
                    
                </table>
            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" /></td>
        </tr>
        <tr>
            <td style="width: 38%; height: 5px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server"><table style="width: 100%">
                        <tr>
                            <td style="width: 40%; height: 36px;">
            <hr />
                                <table style="width: 80%">
                                    <tr>
                                        <td align="center" style="width: 98%; height: 5px">
                                            <asp:Button ID="btnDownload" runat="server" Font-Size="9pt" Height="23px" OnClick="btnDownload_Click"
                                                Style="font: menu" Text="Download Statement" Width="150px" /></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 98%; height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 98%; height: 5px">
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                                style="width: 90%">
                                                <tr style="color: #000000">
                                                    <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                                                        text-align: center">
                                                        ACCOUNT STATMENT FOR
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%; height: 5px">
                                            <table cellpadding="0" cellspacing="0" style="width: 90%">
                                                <tr>
                                                    <td style="vertical-align: middle; width: 50%; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 90%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 60px; height: 20px">
                                                                    Name:</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                    &nbsp;</td>
                                                                <td class="InterFaceTableRightRowUp" style="height: 20px">
                                                                    <asp:Label ID="lblName" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRow" style="width: 60px">
                                                                    Contact:</td>
                                                                <td class="InterFaceTableMiddleRow" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:Label ID="LblContact" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 50%; text-align: right">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 90%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp">
                                                                    Account No</td>
                                                                <td class="InterFaceTableMiddleRowUp">
                                                                    &nbsp;</td>
                                                                <td class="InterFaceTableRightRowUp">
                                                                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRow">
                                                                    Opening Balance</td>
                                                                <td class="InterFaceTableMiddleRow">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:Label ID="lblOpenBal" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRow">
                                                                    Closing Balance</td>
                                                                <td class="InterFaceTableMiddleRow">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:Label ID="lblCloseBal" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%; height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%; height: 5px">
                                            <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                                OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                                                border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                                                border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
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
                                                <asp:BoundColumn DataField="Type" HeaderText="Type" Visible="false">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchNo" HeaderText="BatchNo" Visible="false">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="No." HeaderText="No.">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ValueDate" HeaderText="Transaction Date">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" />
                                                    </asp:BoundColumn>
                                                    <asp:ButtonColumn CommandName="btnView" DataTextField="BatchNo" HeaderText="Batch No.">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                                    </asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="Description" HeaderText="Description">
                                                        <HeaderStyle Width="25%" />
                                                        <ItemStyle Width="25%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Balance" HeaderText="Balance">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                            </asp:DataGrid></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%; height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%">
                                            <CR:CrystalReportViewer ID="Crystalreportviewer2" runat="server" AutoDataBind="true"
                                                BackColor="dimgray" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
            <td style="width: 40%; height: 2px">
                </td>
                        </tr>
                        <tr>
            <td style="width: 40%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
                        </tr>
                    </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server"><table style="width: 100%">
                        <tr>
                            <td align="center" style="width: 98%; height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 98%; height: 5px">
                                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                                style="width: 90%">
                                    <tr style="color: #000000">
                                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                                                        text-align: center">
                                            BATCH DETAILS</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 5px">
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
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 5px"><asp:Button ID="btnReturn" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReturn_Click"
                                                Style="font: menu" Text="RETURN" Width="85px" /></td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 5px">
                                <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                    OnPageIndexChanged="DataGrid2_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
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
                                        <asp:BoundColumn DataField="RecordId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="No." HeaderText="No.">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="20%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id" Visible="false">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="false">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CustomerRef" HeaderText="Phone">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CustName" HeaderText="Name">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TransactionType" HeaderText="Tran Type">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Vendor" HeaderText="Agent">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PayDate" HeaderText="Tran Date">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ToAccount" HeaderText="To Account" Visible="false">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TranAmount" HeaderText="Amount">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                </asp:DataGrid></td>
                        </tr>
                        <tr>
                            <td style="width: 98%; height: 5px">
                                <asp:Label ID="lblBatchCode" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblRunningBal" runat="server" Text="0" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 98%">
                                <CR:CrystalReportViewer ID="CrystalReportViewer3" runat="server" AutoDataBind="true"
                                                BackColor="dimgray" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 38%; height: 36px;">
                </td>
                
        </tr>
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

