<%@ page language="C#" masterpagefile="~/NewAccounts.master" autoeventwireup="true" inherits="AccountStatments, App_Web_raqppk4d" title="ACCOUNTS" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="section">
   <div class="text-center">
        <h4 class="card-title">ACCOUNT INFO</h4>
    </div>

    <div>
           <asp:MultiView ID="MultiView1" runat="server" >
                    <asp:View ID="View1" runat="server">

                        <div class="row mb-4 justify-content-center">
                        
                            <div class="col-md-2 text-center">
                               <label class="form-label text-center">Customer/Agent Code</label>
                             <asp:TextBox ID="txtCustomerCode" runat="server" Style="font: menu" class="form-control" ></asp:TextBox>
                         </div>
                              <div class="col-md-2  text-center">
                             <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary w-100"  Style="margin-top:20px" OnClick="btnOK_Click" Text="Search"  />

                                </div>
                        </div>
                    </asp:View>
                 </asp:MultiView>

               <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
       <div class="row" style="justify-content:center;margin-top:10px; margin-bottom:10px;">
    <div class="col-md-6" style="display:flex;">
        <div class="col-md-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf" runat="server"   Font-Bold="True" GroupName="FileFormat" Text="PDF" />
        </div>
        <div class="col-md-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel" runat="server"  Font-Bold="True" GroupName="FileFormat"  Text="EXCEL" />
        </div>
        <div class="col-md-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-success w-100" OnClick="btnConvert_Click"   Text="Convert" />
        </div>
      
    </div>
</div>

                   
                        <div>
                       <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"  CssClass="table"
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
                     <asp:BoundColumn DataField="RecordId" HeaderText="RecordId" Visible="false">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerCode" HeaderText="Customer Code" Visible="false">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AccountName" HeaderText="Account Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AccountNumber" HeaderText="Account Number">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="AccountBalance" HeaderText="Account Balance">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Network" HeaderText="Network">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Type" HeaderText="Account Type">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>

                   </div>

                    </asp:View> 
                   

               <asp:View ID="View3" runat="server">

                      <div class="container">
                     <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
                       <p class="text-center small">Account Details </p>
                      <asp:Label ID="Label6" runat="server" class="form-label" Style="font: menu" Text="."></asp:Label>
                    <div class="col-12">
                      <label for="yourUsername" class="form-label">Company Name</label>
                      <div class="input-group has-validation">
                        
                          <asp:TextBox ID="txtName" runat="server" Enabled="false"  class="form-control" onblur="Change(this, event)"   onfocus="Change(this, event)"></asp:TextBox>

                     
                        <div class="invalid-feedback">Please enter your company name.</div>
                      </div>
                    </div>

                    <div class="col-12">
                      <label for="trasingName"  class="form-label">Company Code</label>
                          <asp:TextBox ID="txtCompanyCode" class="form-control" runat="server"  Enabled="false"></asp:TextBox>
                   
                      <div class="invalid-feedback">Please enter your company code</div>
                    </div>

                      <div class="col-12">
                      <label for="address" class="form-label">Account Name</label>
                          <asp:TextBox ID="txtAccountName" class="form-control" runat="server" Enabled="false" ></asp:TextBox>
                   
                      <div class="invalid-feedback">Please enter your accounT Name!</div>
                    </div>
 

                      
                      <div class="col-12">
                      <label for="username" class="form-label">Account Number</label>
                          <asp:TextBox ID="txtAccountNumber" class="form-control" runat="server" Enabled="false" ></asp:TextBox>
                   
                      <div class="invalid-feedback">Please enter your account Number!</div>
                    </div>
                      
                      <div class="col-12">
                      <label for="password" class="form-label">Network</label>
                          <asp:TextBox ID="TextBox1" class="form-control" runat="server" Enabled="false" ></asp:TextBox>
                   
                      <div class="invalid-feedback">Please enter your newtwork!</div>
                    </div>
                      
                      <div class="col-12">
                      <label for="spid" class="form-label">Account Type</label>
                          <asp:TextBox ID="TextBox12" class="form-control" runat="server" Enabled="false" ></asp:TextBox>
                   
                      <div class="invalid-feedback">Please enter your account TYpe</div>
                    </div>
 
 
 
                    <div class="col-12" style="margin-top:10px">

                         <asp:Button ID="btnReturn" runat="server" class="btn btn-primary w-100" Text="Return"  Onclick="btnReturn_Click"/>
                     
                    </div>
                <div class="col-12" style="margin-top:15px">
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
                </div>
                   </div>
                 </div>
                    </div>
               </asp:View>
                   </asp:MultiView>
             
    </div>
        </section>
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False"></cr:crystalreportviewer>
</asp:Content>

