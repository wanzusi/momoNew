<%@ page language="C#" masterpagefile="~/Internetwork.master" autoeventwireup="true" inherits="TransactionsNew, App_Web_6l6kiw0m" title="TRANSACTIONS" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div id="page-wrapper">

                <div class="container-fluid">
                    <table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 2px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                         <tr>
                                           <td style="vertical-align: middle; width: 98%; height: 2px; text-align: center;">
                                              <% 
                                                    string IsError = Session["IsError"] as string;
                                                    if (IsError == null)
                                                    {
                                                        Response.Write("<div>");

                                                    }
                                                    else if (IsError == "True")
                                                    {
                                                        Response.Write("<div class=\"alert alert-danger\">");

                                                    }
                                                    else
                                                    {
                                                        Response.Write("<div class=\"alert alert-success\">");
                                                    } 
                                                %>
                                                <strong>
                                                <asp:Label ID="lblmsg" runat="server"></asp:Label></strong>
                                                <%Response.Write("</div>"); %>
                                                <asp:Label ID="lblCount" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                      </table>
                   
                       <table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 2px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                        <tr>
                                            <td class="InterfaceHeaderLabel">
                                                ALL TRANSACTIONS</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                        <!-- /.row -->
                      <table style="width: 100%">
                          <tr>
                              <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Vendor</label>
                                    <asp:DropDownList ID="ddVendor" runat="server" Style="font: menu" Width="90%">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Telecom</label>
                                    <asp:DropDownList ID="ddTelecom" runat="server" Style="font: menu" Width="90%">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Transaction Type</label>
                                    <asp:DropDownList ID="ddTranType" runat="server" Style="font: menu" Width="90%">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Transaction Category</label>
                                    <asp:DropDownList ID="ddTranCategory" runat="server" Style="font: menu" Width="90%">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Status</label>
                                    <asp:DropDownList ID="ddStatus" runat="server" Style="font: menu" Width="90%">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Phone</label>
                                    <asp:TextBox ID="txtPhone" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                               </td>
                          </tr>                            
                           <tr>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>VendorId</label>
                                    <asp:TextBox ID="txtVendorTranId" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>PegPayId</label>
                                    <asp:TextBox ID="txtPegPayId" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>TelecomId</label>
                                    <asp:TextBox ID="txtTelecomId" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>To Date</label>
                                    <asp:TextBox ID="txtToDate" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                                </td>
                                 <td style="vertical-align: middle; width: 10%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Search</label>
                                    <div class="button-wrapper">
                                        <asp:Button ID="btnSubmit" Width="130px" Height="20px" runat="server" Text="Search" OnClick="btnSubmit_Click" />                                  
                                </div>
                                </td>
                           </tr>  
                       </table>
                       <table>
                           <tr>
                                <td style="width: 20%; height: 2px">
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
                                </td>
                           </tr>
                       </table>
                        <hr />
                         <table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 2px">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                        <tr>
                                            <td style="vertical-align: middle; width: 98%; height: 2px; text-align: center;">
                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:MultiView runat="server" ID="Multiview2">
                            <asp:View runat="server" ID="resultView">                                
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="30" CellPadding="4" CellSpacing="2">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                            
                                            <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server" ID="EmptyView"></asp:View>
                        </asp:MultiView>
                        <%-- /row --%>
                       
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
                        <%--/Scripts
                        <%--</form>--%>
                        <%--</div>--%>
                        <!-- /.row -->

                    </div>
                    <!-- /.container-fluid -->
            </div>
            <!-- /#page-wrapper -->
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
   
</asp:Content>
