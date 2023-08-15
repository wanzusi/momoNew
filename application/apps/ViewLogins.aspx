<%@ Page Language="C#" MasterPageFile="~/NewProfileTool.master" AutoEventWireup="true" CodeFile="ViewLogins.aspx.cs" Inherits="ViewLogins" Title="SYSTEM LOGINS" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <section class="section">
                <div class="col-12 mb-4" style="margin-right:auto; margin-left:auto; justify-content:space-evenly">
            <div class="row">
                <div class="col-2">
            
                     <label for="inputEmail5" class="form-label">Search String(Names)</label>
                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" style="font: menu"></asp:TextBox>

                </div>
                 <div class="col-2">
                
                      <label for="inputEmail5" class="form-label">Role</label>
                   <asp:DropDownList ID="cboAccessLevel" runat="server" CssClass="form-select" OnDataBound="cboAccessLevel_DataBound"
                                Style="font: menu" >
                            </asp:DropDownList>

                </div>
                <div class="col-2">
                  
                     <label for="inputEmail5" class="form-label">From Date</label>
                  <asp:TextBox ID="txtfromDate" runat="server" class="form-control" Style="font: menu"></asp:TextBox>

                </div>
                <div class="col-2">
                    <p style="text-align:center" >T</p>
                      <label for="inputEmail5" class="form-label">To Date</label>
                     <asp:TextBox ID="txttoDate" runat="server" class="form-control" Style="font: menu"></asp:TextBox>

                </div>
               

               <div class="col-2">
                   <label></label>
                <asp:Button ID="btnOK" runat="server"   Text="Search" class="btn btn-primary w-75" onClick="btnOK_Click"  style="margin-top:20px;" />
                 </div>

            </div>

        </div>
         <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand" class="table mt-4"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UserName" HeaderText="User Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="LogAction" HeaderText="Action">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CreationDate" HeaderText="Date/Time">
                            <HeaderStyle Width="25%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
    </section>
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender><ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <br />
</asp:Content>

