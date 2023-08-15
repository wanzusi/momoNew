<%@ page language="C#" masterpagefile="~/NewAccounts.master" autoeventwireup="true" inherits="CheckOvaBalance, App_Web_rnqohcbv" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="section">
        <div class="text-center">
            <h5 class="card-title">OVA ACCOUNTS</h5>
         

        </div>
    </section>


 <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
          <div class="row mb-4 justify-content-center">
<div class="col-lg-8" style="display: flex; justify-content:space-evenly">
          <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Choose NetWork</label>
    <asp:DropDownList ID="ddNetwork" runat="server" CssClass="form-select"
                                 OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="ddNetwork_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
          </div>
       <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Choose Ova</label>
  <asp:DropDownList ID="cboOvaAccount" runat="server" CssClass="form-select"
                                OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="cboOvaAccount_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
          </div>
  


    </div>
              </div>
                    </asp:View>
                </asp:MultiView>
     

                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                        
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
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
                        <asp:BoundColumn DataField="SenderId" HeaderText="Account Name">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Balance" HeaderText="Account Balance">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Msisdn" HeaderText="Account Msisdn">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SpId" HeaderText="Sp Id">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
                    </asp:View>
                </asp:MultiView>
       
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
     
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False"></cr:crystalreportviewer>
</asp:Content>