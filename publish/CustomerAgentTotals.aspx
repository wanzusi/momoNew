<%@ page language="C#" masterpagefile="~/ReportMaster.master" autoeventwireup="true" inherits="AgentTotals, App_Web_dz3he2w4" title="AGENT TOTALS" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="section">
        <div class="text-center">
            <h5 class="card-title">AGENT TRANSACTION TOTALS</h5>
        </div>
    </section>
         <div class="row mb-2 justify-content-center">
<div class="col-lg-8" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Agent</label>
            
            <asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"  OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
      <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Status</label>
           <asp:DropDownList ID="cboStatus" runat="server" class="form-select">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="PENDING">PENDING</asp:ListItem>
                                <asp:ListItem Value="SUCCESS">SUCCESS</asp:ListItem>
                                <asp:ListItem Value="FAILED">FAILED</asp:ListItem>
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
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-100" style="margin-top:18px;" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>

    <section class="section">
         <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
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
                    </asp:View>
                </asp:MultiView>
    </section>

    <section class="section">
         <asp:DataGrid style="text-align: justify; border-right: #617da6 1px solid; border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%; border-bottom: #617da6 1px solid;" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Justify" ID="DataGrid1" OnItemCommand="DataGrid1_ItemCommand" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server" Width="100%">
                                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                <EditItemStyle BackColor="#999999" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <ItemStyle Font-Bold="False" ForeColor="#333333" BackColor="InactiveCaption" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <Columns >
                                    <asp:BoundColumn DataField="No." HeaderText="No.">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VendorCode" HeaderText="Agent Code" Visible="False">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Vendor" HeaderText="Agent Name">
                                        <HeaderStyle Width="50%" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Total" HeaderText="Total Amount">
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                            </asp:DataGrid>

         <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
    </section>


    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    &nbsp; &nbsp;<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
        CssClass="MyCalendar" Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

