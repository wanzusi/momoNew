<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="BankDetails.aspx.cs" 
Inherits="BankDetails" 
Title="BANK DETAILS"

Culture="auto" 
UICulture="auto" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
      
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
             <section class="section">
        <div class="text-center">
            <h5 class="card-title">CHEQUE BANKS</h5>
        </div>
    </section>


      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-6 col-md-6 d-flex flex-column align-items-center justify-content-center">
              <div class="card mb-3">

                <div class="card-body">
                    <div class="crad-title">BANK DETAILS ENTRY</div>
           
                  <div class="row g-3 needs-validation" novalidate runat="server" id="form4">

                    <div class="col-12">
                      <label for="otp" class="form-label">Name</label>
                       
                      <asp:TextBox ID="txtname" runat="server" CssClass="form-control"
                                             ></asp:TextBox>
                
                    </div>

                          <div class="col-12">
                      <label for="otp" class="form-label">Phone</label>
                       
                      <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" 
                                             ></asp:TextBox>
              
                    </div>
                          <div class="col-12">
                      <label for="otp" class="form-label">Email</label>
                       
                      <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"
                                             ></asp:TextBox>
               
                    </div>

                         <div class="col-12">
                      <label for="otp" class="form-label">Is Active</label>
                       
                  <asp:CheckBox ID="chkIsActive" runat="server" Font-Bold="True" class="form-check" />
               
                    </div>
 
                    <div class="col-12">
                    
                              <asp:Button ID="btnOK" runat="server"  CssClass="btn btn-primary w-75" Text="SAVE BANK"  Font-Bold="True" OnClick="btnOK_Click" />
                                         
                          </div>
                  
                   
                  </div>

                </div>
              </div>


            </div>
          </div>
        </div>

      </section>
<section class="section">
      <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                        OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="4" Style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                        text-align: justify" Width="100%">
                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                            Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundColumn DataField="No." HeaderText="No.">
                                                <HeaderStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="RecordID" HeaderText="RecordID" Visible="False">
                                                <ItemStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn DataTextField="BankName" HeaderText="Edit" Text="BankName" CommandName="btnEdit">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle Width="25%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" ForeColor="Blue" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Active" HeaderText="Active">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundColumn>
                                             <asp:BoundColumn DataField="Date" HeaderText="PostDate">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    </asp:DataGrid>
</section>


       
        </asp:View>
   
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />

</asp:Content>

