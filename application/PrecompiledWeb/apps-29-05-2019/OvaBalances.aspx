<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="OvaBalances, App_Web_njxbuc3y" title="Untitled Page" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                           OVA Accounts</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                            &nbsp;
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                <table style="width: 100%">
                            <tr>
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            CHOOSE NETWORK
                            </td> 
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            CHOOSE OVA
                            </td> 
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            FROM DATE
                            </td> 
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            TO DATE
                            </td>                           
                            
                            </tr>
                            <tr> 
                            <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="ddNetwork" runat="server" CssClass="InterfaceDropdownList"
                                Width="45%" style="font: menu" OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="ddNetwork_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>        
                            <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboOvaAccount" runat="server" CssClass="InterfaceDropdownList"
                                Width="45%" style="font: menu" OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="cboOvaAccount_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                            <td><asp:TextBox ID="txtFromDate" runat="server" Style="font: menu" Width="45%" placeholder="Enter text" /></td>
                            <td><asp:TextBox ID="txtToDate" runat="server" Style="font: menu" Width="45%" placeholder="Enter text" /></td>
                            <td><asp:Button style="FONT: menu" id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Search" Width="85px" Height="23px" Font-Size="9pt"></asp:Button></td>
                            
                            </tr> 
               </table> 
                    </asp:View>                   
                </asp:MultiView>
                </td>
        </tr>
        <tr>
            <td style="width: 10%; height: 1px">
                <hr />
            </td>            
            <td style="width: 10%; height: 1px">
                <hr />
            </td>                         
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">                    
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
                       
                        &nbsp;<table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 1px">
                                 <asp:GridView ID="Gvuploadedreading"                           
                           runat="server" CellPadding="3" visible="false"
                           ForeColor="#333333"                            
                           CssClass="table table-hover table-responsive text-center" 
                           HorizontalAlign="Center"                            
                           AllowPaging="True" 
                           onpageindexchanging="Gvuploadedreading_PageIndexChanging"                        
                  >
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#752828" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                               <HeaderStyle BackColor="#b70303" HorizontalAlign="Center" Font-Bold="True" ForeColor="White"  />                             
                                 <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#752828" />
                            
                          </asp:GridView>
                <%--<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" Style="border-right: #617da6 1px solid;
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
                        <asp:BoundColumn DataField="SenderId" HeaderText="Account Name">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Balance" HeaderText="Account Balance">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Msisdn" HeaderText="Account Msisdn">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SpId" HeaderText="Sp Id">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>--%>
                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
    </table>
       <%-- Scripts--%>
                        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                            EnableScriptLocalization="true">
                        </ajaxToolkit:ToolkitScriptManager>
                        <br />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False"></cr:crystalreportviewer>
</asp:Content>

