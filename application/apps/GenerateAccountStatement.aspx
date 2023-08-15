<%@ Page Language="C#" MasterPageFile="~/NewReports.master" AutoEventWireup="true"
    CodeFile="~/GenerateAccountStatement.aspx.cs" Inherits="GenerateAccountStatement"
    Title="Generate Account Statement" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                <section class="section">
              <div class="text-center">
            <h5 class="card-title ">GENERATE ACCOUNT STATEMENT</h5>

        </div>
          </section>
           <div class="row mb-4 justify-content-center">
<div class="col-lg-10 mb-3" style="display: flex; justify-content:space-evenly">
    <div class="col-md-2">
         <asp:Label ID="tran_status" runat="server" Font-Bold="True"  Text="TRANSACTION STATUS"></asp:Label>
        <asp:DropDownList ID="cboTranStatus" runat="server" 
                                CssClass="form-select" OnDataBound="cboTranState_DataBound" 
                                 OnSelectedIndexChanged="cboTranStatus_SelectedIndexChanged">
                            </asp:DropDownList>
    </div>
   
          <div class="col-md-1.5">
            <label for="inputEmail5" class="form-label">Agent</label>
<asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                                 OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">From Date</label>
       <asp:TextBox ID="txtfromDate" runat="server" class="form-control w-75" />
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
               
                  <asp:TextBox ID="txttoDate" runat="server" class="form-control w-75" />
          </div>
      <div class="col-md-2">
            <label for="UserCategory" class="form-label">Beneficiary Type</label>
               <asp:DropDownList ID="cboBeneficiaryType" runat="server" CssClass="form-select w-75"
                                >
                            </asp:DropDownList>
          </div>
    </div>
    <div class="col-lg-6" style="display: flex; justify-content:space-evenly">
   
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">Status</label>
               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select"
                                >
                            </asp:DropDownList>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
               
<asp:Button ID="btnOK"  runat="server" Style="margin-top:20px;" CssClass="btn btn-success w-75" Text="Search" OnClick="btnOK_Click" /> 
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
    <div class=" text-center mb-3">
          <asp:Label ID="lblOpeningBal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
    </div>
        
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" class="table"
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
                        <asp:BoundColumn DataField="TranId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PegPayId" HeaderText="PegPayId">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordDate" HeaderText="TransactionDate"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TranType" HeaderText="TranType"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Amount" HeaderText="TranAmount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RunningBal" HeaderText="RunningBal">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
      
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text=".">
                </asp:Label>
      
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>
