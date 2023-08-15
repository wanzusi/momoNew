<%@ page language="C#" masterpagefile="~/NewReports.master" autoeventwireup="true" inherits="TransactionsOld, App_Web_rgb2gjqg" title="TRANSACTIONS" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <label for="UserCategory" class="form-label">Agent-Ref</label>
             <asp:TextBox ID="txtpartnerRef" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">TelecomId</label>
           <asp:TextBox ID="telecomId" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>

    <div class="col-md-2">
            <label for="UserCategory" class="form-label">Payment Ref</label>
          <asp:DropDownList ID="cboPaymentType" runat="server" CssClass="form-select"
                                OnDataBound="cboPaymentType_DataBound">
                            </asp:DropDownList>
          </div>

    <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
           <asp:TextBox ID="txtfromDate" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>
    </div>
       <div class="col-lg-12 mt-2" style="display: flex; justify-content:space-evenly">
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
           <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>
      <div class="col-md-2">
            <label for="UserCategory" class="form-label">Network</label>
         <asp:DropDownList ID="cboNetwork" runat="server" CssClass="form-select"
                               OnDataBound="cboNetwork_DataBound">
                            </asp:DropDownList>
          </div>
      <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Network</label>
           <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Network</label>
           <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">Customer Name</label>
           <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>
    
           </div>
            <div class="col-lg-12 mt-2" style="display: flex; justify-content:space-evenly">
                 <div class="col-md-2">
            <label for="UserCategory" class="form-label">Transaction Type</label>
           <asp:DropDownList ID="cboTranType" runat="server" CssClass="form-select"
                                OnDataBound="cboTranType_DataBound" >
                            </asp:DropDownList>
          </div>
         <div class="col-md-2">
            <label for="UserCategory" class="form-label">Transaction Status</label>
           <asp:DropDownList ID="cboTranStatus" runat="server" CssClass="form-control"
                                OnDataBound="cboTranState_DataBound" >
                            </asp:DropDownList>
          </div>

    <div class="col-md-2">
                 <label ID="Label1" runat="server" class="form-label">Search by phone number</label>
                            <asp:TextBox ID="phoneNum" runat="server" class="form-control"></asp:TextBox>
    </div>
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-75" style="margin-top:18px;" OnClick="btnOK_Click"
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
                        <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id">
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
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
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
                        <asp:BoundColumn DataField="Str1" HeaderText="PegPay ID">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="TranId" HeaderText="Telecom ID">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>--%>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
        
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
 
    <br />
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>
