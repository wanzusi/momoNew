<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="PrepaymentTrans.aspx.cs" Inherits="PrepaymentTrans" Title="PREPAYMENT TRANSACTIONS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <section class="section">
        <div class="text-center">
            <h5 class="card-title">PREPAYMENT TRANSACTIONS</h5>
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
            <label for="UserCategory" class="form-label">Receipt No</label>
               <asp:TextBox ID="txtReceiptno" runat="server" class="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Meter No</label>
<asp:TextBox ID="txtMeterNo" runat="server" class="form-control"></asp:TextBox>
          </div>
       

     <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">TO Date</label>
                <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    </div>
         
             <div class="col-md-2 text-center">
            <label for="UserCategory" class="form-label"></label>
              <asp:Button ID="btnOK" runat="server"  OnClick="btnOK_Click" CssClass="btn btn-primary w-75"
                                Text="Search"  />
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
                        <asp:BoundColumn DataField="Id" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Str1" HeaderText="Meter No">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReceiptNumber" HeaderText="Receipt No">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Token" HeaderText="Token">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Vendor" HeaderText="Agent">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Justify" Width="20%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Justify" Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="GenerationDate" HeaderText="Pay Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Units" HeaderText="Units">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TokenValue" HeaderText="Value">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Total" HeaderText="Amount">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="15%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="15%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
       <div class="text-center">
           <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
       </div>
           
    
    
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

