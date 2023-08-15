<%@ page language="C#" masterpagefile="~/NewSystemTools.master" autoeventwireup="true" inherits="ViewUsers, App_Web_d1f0fhhh" title="SYSTEM USERS" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section class="section">
  <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
<div class="row mb-3">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">

    
          
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
            
              <asp:TextBox ID="txtSearch" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">User Category</label>
             <asp:DropDownList ID="cboAreas" runat="server" AutoPostBack="True" class="form-select"
                                OnDataBound="cboAreas_DataBound" OnSelectedIndexChanged="cboAreas_SelectedIndexChanged"
                               style="font: menu">
                            </asp:DropDownList>
          </div>
            <div class="col-md-2">
            <label for="UserCategory" class="form-label">Company</label>
             <asp:DropDownList ID="cboBranches" runat="server" AutoPostBack="True" class="form-select"
                                OnDataBound="cboCostCenter_DataBound" 
                               style="font: menu">
                            </asp:DropDownList>
          </div>
            <div class="col-md-2">
                 <label for="UserCategory" class="form-label">Role</label>
                  <asp:DropDownList ID="cboAccessLevel" runat="server" OnDataBound="cboAccessLevel_DataBound" CssClass="form-select"
                                Style="font: menu">
                            </asp:DropDownList>
            </div>
          
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-100" style="margin-top:18px;" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>

<div class="container">
        <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table mt-4"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify" >
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
                  <div class="container">
                        <asp:Label ID="lblHeader" runat="server" CssClass="form-label" Text="."></asp:Label>
                      <div class="col-md-6">
                          <label for="inputAddress5" class="form-label">Credit To Add</label>
                          <asp:TextBox ID="txtCredit" runat="server" class="form-control"></asp:TextBox>

                      </div>
                      <div class="col-md-6">
                          <div class="row">
                              <div class="col-md-3">
                                  <asp:Button ID="BtnSave" runat="server" Font-Bold="True" CssClass="btn"
                                        OnClick="BtnSave_Click" Style="font: menu" Text="ADD CREDIT" />
                              </div>

                              <div class="col-md-3">
                                  <asp:Button ID="btnReturn" runat="server" Font-Bold="True" CssClass="btn"
                                        OnClick="btnReturn_Click" Style="font: menu" Text="RETURN" />
                              </div>
                          </div>
                      </div>

                  </div>
                                       
                     
                 <asp:Label ID="lblUserName" runat="server" Text="0" Visible="False" CssClass="form-label"></asp:Label>

                    </asp:View>
                </asp:MultiView>
    </div>

       <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtCredit" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
                 </section>


</asp:Content>

