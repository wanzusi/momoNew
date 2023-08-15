<%@ page language="C#" masterpagefile="~/ReportMaster.master" autoeventwireup="true" inherits="AccountStatements, App_Web_zulwb1bx" title="AccountStatements" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <section class="section">
        <div class="text-center">
            <h5 class="card-title"> ACCOUNT STATEMENTS</h5>
        </div>
         </section>
        <div class="row mb-2">
<div class="col-lg-8" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Agent</label>
            
             <asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select" OnDataBound="cboVendor_DataBound">
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

     <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="text-center">
                            <asp:Button ID="btnDownload" runat="server"  OnClick="btnDownload_Click" CssClass="btn  btn-success w-25"  Text="Download Statement" />
                        </div>
                              <section class="section">
   <div class="row justify-content-center">
<div class="col-lg-6" >
    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Account Statement For</h5>
 
        <div class="row g-3">
          
          <div class="col-md-4">
            <label for="inputEmail5" class="form-label">Name</label>
            <asp:Label ID="lblName" runat="server" CssClass="form-label"></asp:Label>
          </div>
          <div class="col-md-4">
            <label for="inputPassword5" class="form-label">Contact</label>
            <asp:Label ID="LblContact" CssClass="form-label" runat="server"></asp:Label>
          </div>
          <div class="col-md-4">
            <label for="inputAddress5" class="form-label">Account No</label>
            <asp:Label ID="lblAccountNo" CssClass="form-label" runat="server"></asp:Label>
          </div>
        
   
          <div class="col-md-4">
            <label for="email" class="form-label">Opening Balance</label>
         <asp:Label ID="lblOpenBal" CssClass="form-label" runat="server"></asp:Label>
          </div>

          <div class="col-md-4">
            <label for="email" class="form-label">Closing Balance</label>
           <asp:Label ID="lblCloseBal" CssClass="form-label" runat="server"></asp:Label>
          </div>

        


        </div>
  
     
    </div>
  </div>
  </div>
</div>

 </section>
                     <section class="section">

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
                         <br />
                         
                     </section>
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
                    </asp:View>
          <asp:View ID="View2" runat="server">
              <section class="section">
                  <div class="text-center">
                      <h6 class="card-title">BATCH DETAILS</h6>
                  </div>
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
        </div>
               </div>
                  <div class="m-2 text-center"> 
                      <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="RETURN" CssClass="btn btn-primary" />
                  </div>
                  <div class="mt-2">
                       <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
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
                                        <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id" Visible="false">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="false">
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
                                        <asp:BoundColumn DataField="Vendor" HeaderText="Agent">
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
                                        <asp:BoundColumn DataField="ToAccount" HeaderText="To Account" Visible="false">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TranAmount" HeaderText="Amount">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                </asp:DataGrid>
                  </div>
                  <div class="container">
                         <asp:Label ID="lblBatchCode" runat="server" Text="0" Visible="False"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblRunningBal" runat="server" Text="0" Visible="False"></asp:Label>

                      <CR:CrystalReportViewer ID="CrystalReportViewer3" runat="server" AutoDataBind="true"
                                                BackColor="dimgray" Visible="False" />
                  </div>

              </section>
              </asp:View>


         </asp:MultiView>

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

