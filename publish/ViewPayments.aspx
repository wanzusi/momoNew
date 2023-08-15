<%@ page language="C#" masterpagefile="~/NewReports.master" autoeventwireup="true" inherits="ViewPayments, App_Web_rgb2gjqg" title="VIEW PAYMENT" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="section">
        <div class="text-center">
            <h5 class="card-title">Payments</h5>
        </div>
    </section>
  <section class="section">
<div class="row justify-content-center">
<div class="col-lg-12" style="display: flex;">

      
        <div class="col-md-2">
            <label for="inputState" class="form-label">Beneficiary Type</label>
            <asp:DropDownList ID="cboBeneficiaryType" runat="server"  class="form-select"
                                OnDataBound="cboBeneficiaryType_DataBound" Style="font: menu" >
                            </asp:DropDownList>
          </div>

         <div class="col-md-2">
            <label for="inputState" class="form-label">Payment Type</label>
            <asp:DropDownList ID="cboPaymentType" runat="server"  class="form-select" style="font: menu" OnDataBound="cboPaymentType_DataBound">
                            </asp:DropDownList>
          </div>

          <div class="col-md-2">
            <label for="inputState" class="form-label">Beneficiary Number</label>
              <asp:TextBox ID="txtbenConatct" runat="server" Style="font: menu" CssClass="form-control"></asp:TextBox>
          </div>

         <div class="col-md-2">
            <label for="inputState" class="form-label">Beneficiary Name</label>
              <asp:TextBox ID="txtBenName" runat="server" Style="font: menu" CssClass="form-control"></asp:TextBox>
          </div>

         <div class="col-md-2">
            <label for="inputState" class="form-label">From Date</label>
              <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" CssClass="form-control"></asp:TextBox>
          </div>
         <div class="col-md-2">
            <label for="inputState" class="form-label">TO Date</label>
              <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" CssClass="form-control"></asp:TextBox>
          </div>
        
         <div class="col-md-2 text-center" >
              
              <asp:Button ID="btnOK" runat="server"  OnClick="btnOK_Click" CssClass="btn btn-primary"
                                Text="Search" Width="85px" style="font: menu" />
          </div>
      </div>

   
    </div>
<asp:MultiView ID="MultiView1" runat="server">
 <asp:View ID="View1" runat="server">
           <div class="row" style="justify-content:center">
    <div class="col-lg-8" style="display:flex;">
        <div class="col-lg-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf" runat="server"   Font-Bold="True" GroupName="FileFormat" Text="PDF" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel" runat="server"  Font-Bold="True" GroupName="FileFormat"  Text="EXCEL" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-primary w-100" OnClick="btnConvert_Click" Font-Bold="True"   Text="COVERT" />
        </div>
        <div class="col-lg-2">
          <asp:CheckBox ID="withReason" runat="server" Text="Format with reason"  OnCheckedChanged="RadioButton1_CheckedChanged" />
        </div>
    </div>
</div>




   </asp:View>
    </asp:MultiView>

      <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify" AllowPaging="True" Visible="true">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="RecordId" HeaderText="PaymentID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BatchNo" HeaderText="Payment No" Visible="False">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BatchNo" HeaderText="VendorRef" Visible="False"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnPrint" DataTextField="PaymentNo" HeaderText="PaymentID"
                            Text="PaymentNo" Visible="false">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="Beneficiary Account">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BeneficiaryName" HeaderText="Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                          <asp:BoundColumn DataField="TranCharge" HeaderText="Pegasus Fee">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PayOutFee" HeaderText="MNO Fee">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CashOutFee" HeaderText="CashOut Fee">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="TotalCharge" HeaderText="Total Charge">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordedBy" HeaderText="Uploaded By" Visible="False">
                            <HeaderStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="Date">
                            <HeaderStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Reason" HeaderText="Reason" Visible="true">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                    </Columns>
                    
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>

      <div>
           <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="Black" Text="."></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblPegasusTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblMnoFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblCashoutFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblAllTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
      </div>



      </section>
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

