<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="CustomerRunningBalances.aspx.cs" Inherits="Transactions" Title="TRANSACTIONS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <section class="section">
        <div class="text-center">
            <h5 class="card-title"> ALL TRANSACTIONS</h5>
        </div>
    </section>


     <div class="row mb-2 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Agent</label>
        <asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                               OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div>


    <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
    <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>

    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
           <asp:Button ID="btnOK" runat="server"  OnClick="btnOK_Click" style="margin-top:18px" CssClass="btn btn-primary" Text="Search" />
          </div>
      
    </div>

      </div>

                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                                    <section class="section">

           <div class="row" style="justify-content:center">
    <div class="col-lg-6" style="display:flex;">
        <div class="col-lg-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf" runat="server"   Font-Bold="True" GroupName="FileFormat" Text="PDF" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel" runat="server"  Font-Bold="True" GroupName="FileFormat"  Text="EXCEL" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-primary w-100" OnClick="btnConvert_Click" Font-Bold="True"   Text="COVERT" />
        </div>
       
    </div>
</div>

    </section>
                    </asp:View>
                </asp:MultiView>
      
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
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
                       <%-- <asp:BoundColumn DataField="RecordId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="20%" />
                        </asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>--%>
                    <%--    <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                      <asp:BoundColumn DataField="TranType" HeaderText="TranType">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <%--  
                        <asp:BoundColumn DataField="TransactionType" HeaderText="Tran Type">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="Agent">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PaymentDate" HeaderText="Tran Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        
                         <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                     <asp:BoundColumn DataField="TranAmount" HeaderText="Tran Amountt" Visible="True">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BalanceAfter" HeaderText="Running Balance">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                      <%--  <asp:BoundColumn DataField="Str1" HeaderText="PegPay ID">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>--%>
                    </Columns>
                   <%-- <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />--%>
                </asp:DataGrid>
    
     
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
      
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

