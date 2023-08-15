<%@ Page Language="C#" MasterPageFile="~/NewCustomer.master" AutoEventWireup="true" CodeFile="ViewCoperateUsers.aspx.cs" Inherits="ViewCoperateUsers" Title="VIEW CORPORATE USERS" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>

     <div class="row mb-2 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
            
              <asp:TextBox ID="txtSearch" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">User category</label>
            <asp:DropDownList ID="cboAreas" runat="server" AutoPostBack="True" CssClass="form-select"
                                OnDataBound="cboAreas_DataBound" OnSelectedIndexChanged="cboAreas_SelectedIndexChanged"
                                >
                            </asp:DropDownList>
          </div>
      <div class="col-md-2">
            <label for="UserCategory" class="form-label">Company</label>
          <asp:DropDownList ID="cboBranches" runat="server" CssClass="form-select"
                                OnDataBound="cboCostCenter_DataBound" >
                            </asp:DropDownList>
          </div>
      <div class="col-md-2">
            <label for="UserCategory" class="form-label">Role</label>
            <asp:DropDownList ID="cboAccessLevel" runat="server" OnDataBound="cboAccessLevel_DataBound" CssClass="form-select">
                            </asp:DropDownList>
          </div>
         
          <div class="col-md-3">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-75" style="margin-top:22px;" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>


               <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
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
                        <asp:BoundColumn DataField="UserID" HeaderText="UserID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Userid" HeaderText="UserId" Visible="False">
                            <HeaderStyle Width="5%" />
                            
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="UserName" HeaderText="Edit"
                            Text="UserName">
                            <HeaderStyle Width="13%" />
                              <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue"  Width="13%" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="btnSms" HeaderText="SMS" Text="Credit" Visible="False">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="UserName" HeaderText="User Name" Visible="False">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UserType" HeaderText="UserCategory">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BranchName" HeaderText="Company">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RoleName" HeaderText=" Role">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="LoggedOn" HeaderText="LoggedOn" Visible="False">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Created" DataField="Date">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></asp:View>
                    <asp:View ID="View2" runat="server">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                            <tr>
                                <td colspan="2" style="vertical-align: top; width: 100%; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="InterfaceHeaderLabel">
                                                <asp:Label ID="lblHeader" runat="server" Text="."></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: top; width: 100%; height: 5px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: top; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 70%">
                                        <tr>
                                            <td colspan="3" style="height: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Credit To Add</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtCredit" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterfaceItemSeparator" colspan="3" style="height: 1px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: top; height: 23px; text-align: center">
                                    <asp:Button ID="BtnSave" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="BtnSave_Click" Style="font: menu" Text="ADD CREDIT" Width="106px" />&nbsp;
                                    <asp:Button ID="btnReturn" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="btnReturn_Click" Style="font: menu" Text="RETURN" Width="106px" /></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: top; text-align: center">
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblUserName" runat="server" Text="0" Visible="False"></asp:Label></asp:View>
                </asp:MultiView>
       
    <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtCredit" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    <br />
    <br />
</asp:Content>

