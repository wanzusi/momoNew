<%@ Page Language="C#" MasterPageFile="~/NewBeneficiaries.master" AutoEventWireup="true" CodeFile="ApproveCustomerBeneficiaries.aspx.cs" Inherits="ApproveCustomerBeneficiaries" Title="APPROVE CUSTOMER BENEFICIARIES" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
   <section class="section">
       
       <div class="text-center">
           <h5 class="card-title">CUSTOMER BENEFICIARIES TO APPROVE</h5>
       </div>

   </section>
    <div class="row mb-2">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">

    
          
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
            
              <asp:TextBox ID="txtSearch" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Phone Number</label>
           <asp:TextBox ID="txtSearchPhone" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
            <div class="col-md-2">
            <label for="UserCategory" class="form-label">Location</label>
             <asp:TextBox ID="txtdistrict" runat="server"  class="form-control"></asp:TextBox>
          </div>
            <div class="col-md-2">
                 <label for="UserCategory" class="form-label">Beneficary Type</label>
                  <asp:DropDownList ID="cboBeneficiaryType" runat="server" CssClass="form-select"
                                OnDataBound="cboBeneficiaryType_DataBound" >
                            </asp:DropDownList>
            </div>
          
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-100" style="margin-top:18px;" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>
    <section class="section">

 
               <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                       
                            <div class="row m-2 justify-content-center">
                                    <div class="col-md-2">
                                <asp:Button ID="btnApprove" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                    OnClick="btnApprove_Click" Text="APPROVE" Width="150px" />
                                        </div>
                                <div class="col-md-2">
                                <asp:Button ID="btnReject" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                     Text="REJECT " Width="150px" OnClick="btnReject_Click" />
                                    </div> 
                                <div class="col-md-2">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Cambria"
                                    Font-Size="12pt" OnCheckedChanged="chkTransactions_CheckedChanged" Text="Select All Beneficiaries" />
                      </div>
                    </div>
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
                        <asp:BoundColumn DataField="RecordID" HeaderText="Beneficary ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode" Visible="False">
                            <HeaderStyle Width="5%" />
                            
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="FullName" HeaderText="Edit"
                            Text="FullName" Visible="false">
                            <HeaderStyle Width="13%" />
                              <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue"  Width="13%" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Title" HeaderText="Title">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Telephone">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TypeName" HeaderText="Type">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Location" HeaderText="Location">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordedBy" HeaderText="Created By" Visible="true">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Created" DataField="Date">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# DataBinder.Eval(Container, "DataItem.Approved") %>'
                                            Width="5%" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%" />
                                </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
                    
                    </asp:View>
                    
                </asp:MultiView>
      </section>

   
</asp:Content>

