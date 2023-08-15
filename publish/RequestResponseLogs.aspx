<%@ page language="C#" masterpagefile="~/NewReports.master" autoeventwireup="true" inherits="RequestResponseLogs, App_Web_rnqohcbv" title="View Request-Response Logs" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <section class="section">
        <div class="text-center">
            <h5 class="card-title">VIEW REQUEST-RESPONSE LOGS</h5>
        </div>
    </section>

    <div class="row mb-2 justify-content-center">
<div class="col-lg-10" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Vendor</label>
<asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                               OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Vendor TranId</label>
       <asp:TextBox ID="txtVendorId" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Phone</label>
                <asp:TextBox ID="txtPhone" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
                <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    </div>

    <div class="col-md-2">
        <asp:Button ID="btnOK" runat="server"   OnClick="btnOK_Click" CssClass="btn btn-primary w-75" style="margin-top:20px;"
                                Text="Search" />
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
 
              <div class="text-center">
                  <asp:Label ID="lblOpeningBal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
              </div> 
                

                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Right" OnItemCommand="DataGrid1_ItemCommand"
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
                    <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="RecordId" HeaderText="RecordId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode">
                            <HeaderStyle Width="6%" />
                            <ItemStyle Width="6%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorTranId" HeaderText="VendorTranId">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="11%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ToAccount" HeaderText="Receiver">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="11%" />
                        </asp:BoundColumn>
                       <asp:BoundColumn DataField="FromAccount" HeaderText="Sender">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="12%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="RecordDate">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TranType" HeaderText="TranType">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ToNetwork" HeaderText="Network"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
    
    <br />
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="yyyy-MM-dd" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="yyyy-MM-dd" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>
