<%@ page language="C#" masterpagefile="~/ReportMaster.master" autoeventwireup="true" inherits="ViewFundTransfers, App_Web_rnqohcbv" title="Untitled Page" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <section class="section">
         <div>
            <h5 class="card-title">Account Transfers</h5>
        </div>

                   <div class="col-8" style="margin-right:auto; margin-left:auto; justify-content:space-evenly">
            <div class="row">
                <div class="col-2">
                    <label for="inputEmail5" class="form-label">From Account</label>
                <asp:DropDownList ID="cboCustomerAccount" runat="server" class="form-select">
                            </asp:DropDownList>
                    </div>
           
                <div class="col-2">
                   
                     <label for="inputEmail5" class="form-label">From Date</label>
                  <asp:TextBox ID="txtfromDate" runat="server" class="form-control" Style="font: menu"></asp:TextBox>

                </div>
                <div class="col-2">
                  
                     <label for="inputEmail5" class="form-label">To Date</label>
                     <asp:TextBox ID="txttoDate" runat="server" class="form-control" Style="font: menu"></asp:TextBox>

                </div>
               

               <div class="col-2">
                   <label for="inputEmail5" class="form-label" >.</label>
                <asp:Button ID="btnOK" runat="server"   Text="Search" class="btn btn-primary w-100" onClick="btnOK_Click" />
                 </div>

            </div>

        </div>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
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
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SerialNo" HeaderText="FROM ACCOUNT.">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustName" HeaderText="AMOUNT">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="TRANSACTION TYPE">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Cleared" HeaderText="RECORDED BY">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>

         <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
    </section>
</asp:Content>

