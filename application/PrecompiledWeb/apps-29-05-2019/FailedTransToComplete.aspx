<%@ page language="C#" masterpagefile="~/ReportMaster2.master" autoeventwireup="true" inherits="Reconciliation, App_Web_uqxe7nlr" title="FAILED TRANSACTIONS TO COMPLETE" enableviewstatemac="false" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <script type="text/javascript">
            function jsFunction()
            {
                if (confirm("WAIT.....You are about to complete this transaction, Are you really sure"))
                    return true
                else
                    return false
            }
        </script>


    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            FAILED TRANSACTIONS TO COMPLETE</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 3px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                &nbsp;<asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View4" runat="server">
                        &nbsp;<table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 5px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                text-align: center">
                                                CUSTOMER CODE</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                text-align: center">
                                                BATCH NO</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                text-align: center">
                                                recorded by</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                text-align: center">
                                                from DATE</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                text-align: center">
                                                to DATE</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                                text-align: center">
                                                STATUS
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; height: 1px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;
                                                <asp:TextBox ID="customerCode" runat="server" Width="225px"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;
                                                <asp:TextBox ID="batchNo" runat="server" Width="225px"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:TextBox ID="recordedBy" runat="server" Width="225px"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:TextBox ID="fromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                                            </td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;<asp:TextBox ID="toDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" /></asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 19%; text-align: left; height: 25px;">
                            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Bold="True"
                                OnCheckedChanged="CheckBox2_CheckedChanged" Text="Select All" Visible="False" /></td>
                        <td style="width: 66%; text-align: center; height: 25px;">
                            <asp:Button ID="btnReverse" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReverse_Click"
                                Text="REVERSE TRANSACTIONS" Width="150px" style="font: menu" Visible="False" />
                            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnResend" runat="server" Font-Size="9pt" Height="23px" OnClick="btnResend_Click"
                                Text="RESEND TRANSACTIONS" Width="150px" style="font: menu" UseSubmitBehavior="False" Visible="False" /></td>
                        <td style="width: 30%; text-align: right; height: 25px;">
                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True"
                                OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" Visible="False" /></td>
                    </tr>
                </table><hr />
                        <asp:Label ID="lblTotal" runat="server" Text="." Font-Bold="True" ForeColor="#0000C0"></asp:Label></asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                       <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode" Visible="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BeneficiaryName" HeaderText="Beneficiary Name">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PaymentDate" HeaderText="Payment Date">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BatchNo" HeaderText="Batch NUMBER">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="Record Date">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordedBy" HeaderText="Recorded By">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                            <HeaderStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PaymentNo" HeaderText="payment number">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                       
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="PaymentNo" HeaderText="Payment number"
                                    >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                         <asp:TemplateColumn HeaderText="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                            Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                    </Columns>
                    
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></asp:View>
                    <asp:View ID="View3" runat="server">
                        <table align="center" style="width: 90%">
                            <tr>
                                <td style="width: 100%; height: 2px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                                    <tr>
                                                        <td class="InterfaceHeaderLabel" style="text-align:center;">
                                                            COMPLETE BATCH TRANSACTION</td>
                                                    </tr>
                                                </table>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                Transaction DETAILS</td>
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
                                                                </td>
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
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Batch Number</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="batchNum" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Enabled="False" Width="60%" OnTextChanged="txtVendorId_TextChanged"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Payment Number</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="paymentNum" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Current status</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="currentStatus" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="60%" Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="txtMark" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="Button1_Click" Style="font: menu" Text="Complete transaction" Width="150px" CausesValidation="True" OnClientClick="if (!jsFunction()) return false;" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 2px; text-align: center">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="fromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="toDate">
    </ajaxToolkit:CalendarExtender>
    
    
    
    

    
    
    
</asp:Content>

