<%@ Page Language="C#" MasterPageFile="~/NewReports.master" AutoEventWireup="true" CodeFile="AccountStatementsNew1.aspx.cs" Inherits="AccountStatementsNew1" Title="ACCOUNT STATEMENTS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <section class="section">
              <div class="text-center">
            <h5 class="card-title ">ACCOUNT STATEMENTS</h5>

        </div>
          </section>
          <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Agent</label>
<asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                                 OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">From Date</label>
       <asp:TextBox ID="txtfromDate" runat="server" class="form-control" />
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
               
                  <asp:TextBox ID="txttoDate" runat="server" class="form-control" />
          </div>
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">Beneficiary Type</label>
               <asp:DropDownList ID="cboBeneficiaryType" runat="server" CssClass="form-select"
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
                       <div class="text-center row">
                           
                                            <asp:Button ID="btnDownload" runat="server" class="btn btn-secondary w-50" OnClick="btnDownload_Click"
                                                Style="font: menu" Text="Download Statement(PDF)" />

                                            <asp:Button ID="btnDownloadExcel" runat="server" class="btn btn-secondary w-50"  OnClick="btnDownloadExcel_Click"
                                                Style="font: menu" Text="Download Statement(EXCEL) "/>
                       </div>
                               
                                                        ACCOUNT STATMENT FOR
                                
                                            <table cellpadding="0" cellspacing="0" style="width: 90%">
                                                <tr>
                                                    <td style="vertical-align: middle; width: 50%; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 90%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 60px; height: 20px">
                                                                    Name:</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                    &nbsp;</td>
                                                                <td class="InterFaceTableRightRowUp" style="height: 20px">
                                                                    <asp:Label ID="lblName" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRow" style="width: 60px">
                                                                    Contact:</td>
                                                                <td class="InterFaceTableMiddleRow" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:Label ID="LblContact" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 50%; text-align: right">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 90%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp">
                                                                    Account No</td>
                                                                <td class="InterFaceTableMiddleRowUp">
                                                                    &nbsp;</td>
                                                                <td class="InterFaceTableRightRowUp">
                                                                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRow">
                                                                    Opening Balance</td>
                                                                <td class="InterFaceTableMiddleRow">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:Label ID="lblOpenBal" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRow">
                                                                    Closing Balance</td>
                                                                <td class="InterFaceTableMiddleRow">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:Label ID="lblCloseBal" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                    
                                 
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
                                                <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                                <Columns>
                                                <asp:BoundColumn DataField="Type" HeaderText="Type" Visible="false">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BatchNo" HeaderText="BatchNo" Visible="false">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="No." HeaderText="No.">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ValueDate" HeaderText="Transaction Date">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" />
                                                    </asp:BoundColumn>
                                                    <asp:ButtonColumn CommandName="btnView" DataTextField="BatchNo" HeaderText="Batch No.">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                                    </asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="Description" HeaderText="Description">
                                                        <HeaderStyle Width="25%" />
                                                        <ItemStyle Width="25%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Balance" HeaderText="Balance">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                            </asp:DataGrid>
                               
                                            <CR:CrystalReportViewer ID="Crystalreportviewer2" runat="server" AutoDataBind="true"
                                                BackColor="dimgray" Visible="False" />
           
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
                       
              
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <div class="text-center">
                            <h6 class="card-title"> BATCH DETAILS</h6>
                        </div>
                                           
                         
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
                         
                                <asp:Button ID="btnReturn" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReturn_Click"
                                                Style="font: menu" Text="RETURN" Width="85px" /></td>
                       
                        
                                <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" class="table"
                                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                    OnPageIndexChanged="DataGrid2_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
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
                                        <asp:BoundColumn DataField="RecordId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="No." HeaderText="No.">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="20%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id" Visible="False">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="False">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CustomerRef" HeaderText="Phone">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CustName" HeaderText="Name">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TransactionType" HeaderText="Tran Type">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Category" HeaderText="TransCategory">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PayDate" HeaderText="Tran Date">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ToAccount" HeaderText="To Account" Visible="False">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TranAmount" HeaderText="Amount">
                                            <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                            <ItemStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                </asp:DataGrid>
                       
                            <div class="text-center">
                                <asp:Label ID="lblBatchCode" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblRunningBal" runat="server" Text="0" Visible="False"></asp:Label>
                            </div>
                                
                        
                         
                                <CR:CrystalReportViewer ID="CrystalReportViewer3" runat="server" AutoDataBind="true"
                                                BackColor="dimgray" Visible="False" />
                        
                    </asp:View>
                </asp:MultiView>
        
      
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="BottomRight" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

