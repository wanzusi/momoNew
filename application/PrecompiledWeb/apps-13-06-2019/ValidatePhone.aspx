<%@ page language="C#" masterpagefile="~/Internetwork.master" autoeventwireup="true" inherits="ValidatePhone, App_Web_dlu_-nfv" title="TRANSACTIONS" enableviewstatemac="false" %>

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
                                                VALIDATE PHONENUMBER</td>
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
                                    <label>Input Type</label>
                                    <asp:DropDownList ID="ddInputType" runat="server" Style="font: menu" Width="90%">
                                        <asp:ListItem Text="One Or Many" Value="TEXTBOX"></asp:ListItem>
                                        <asp:ListItem Text="Upload File" Value="FILE"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                               <td style="vertical-align: middle; width: 40%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Provide one or a comma separated list of numbers</label>
                                    <asp:TextBox ID="txtValue" runat="server" Style="font: menu" Width="90%" placeholder="Enter text" />
                               </td>
                                <td style="vertical-align: middle; width: 50%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                                    <label>Upload File</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" Width="70%" />
                               </td>                              
                          </tr>                            
                       </table>
                       <hr />
                       <asp:Button ID="btnSearch" Width="130px" Height="30px" runat="server" CssClass="btn btn-success btn-lg" Text="Search" OnClick="btnSubmit_Click" />                       
                       <hr />
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
