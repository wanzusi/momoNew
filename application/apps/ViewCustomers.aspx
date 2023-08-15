<%@ Page Language="C#" MasterPageFile="~/NewCustomer.master" AutoEventWireup="true" CodeFile="ViewCustomers.aspx.cs" Inherits="ViewCustomers" Title="VIEW CUSTOMERS" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="section">
         <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
        <div class="row justify-content-center">
<div class="col-lg-12 mb-4" style="display: flex; justify-content:space-evenly">



          
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
            
              <asp:TextBox ID="txtSearch" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Pegpay Account</label>
                   <asp:TextBox ID="txtpegpayAccount" runat="server"  class="form-control"></asp:TextBox>
          </div>
            <div class="col-md-2">
            <label for="UserCategory" class="form-label">MOMO Account</label>
              
                 <asp:TextBox ID="txtMomoAccount" runat="server"  class="form-control"></asp:TextBox>
            
          </div>
            <div class="col-md-2">
                 <label for="UserCategory" class="form-label">Customer Type</label>
                   <asp:DropDownList ID="cboCustomerType" runat="server" OnDataBound="cboCustomerType_DataBound"
                                Style="font: menu" CssClass="form-select">
                            </asp:DropDownList>
            </div>
         
         <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-75" OnClick="btnOK_Click" style="margin-top:22px;"
                                Text="Search" />

          </div>



            </div>
         

    </div>

        <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White"  CssClass="table"/>
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Id" HeaderText="ClientId" Visible="False">
                            <HeaderStyle Width="5%" />
                            
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="ID" HeaderText="Edit"
                            Text="Edit">
                            <HeaderStyle Width="13%" />
                              <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue"  Width="13%" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="25%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerType" HeaderText="Customer Type">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PegpayAccountNumber" HeaderText="PegpayAccountNumber">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MoMoAccountNumber" HeaderText=" MoMoAccountNumber">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Approved" HeaderText="Approved" Visible="True">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Created By " DataField="RecordedBy">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Created" DataField="Date">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>

                    </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="container">
                    <asp:Label ID="lblHeader" CssClass="form-label" runat="server" Text="."></asp:Label>
                    <div class="col-md-4">
                        <asp:Label runat="server" CssClass="form-label">Credit To Add</asp:Label>
                        <asp:TextBox ID="txtCredit" runat="server" CssClass="InterfaceTextboxLongReadOnly" class="form-control"></asp:TextBox>
                    </div>
                    <div class="row">
                    <div class="col-md-4">
                        <div class="col-md-2">
                                <asp:Button ID="BtnSave" runat="server" Font-Bold="True" class="btn btn-primary w-100"
                                        OnClick="BtnSave_Click" Style="font: menu" Text="ADD CREDIT"  />
                        </div>
                        <div class="col-md-2">
                             <asp:Button ID="btnReturn" runat="server" Font-Bold="True" class="btn btn-success w-100"
                                        OnClick="btnReturn_Click" Style="font: menu" Text="RETURN" />
                        </div>
                                   
                    </div>
                        </div>

                        <asp:Label ID="lblUserName" CssClass="form-label" runat="server" Text="0" Visible="False"></asp:Label>
                </div>
              
            </asp:View>

            </asp:MultiView>
    </section>
    <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtCredit" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    <br />
    <br />
</asp:Content>

