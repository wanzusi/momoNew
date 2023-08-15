<%@ page language="C#" masterpagefile="~/NewReports.master" autoeventwireup="true" inherits="ViewCustomerBatches, App_Web_d1f0fhhh" title="VIEW CUSTOMER BATCHES" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <section class="section">
              <div class="text-center">
            <h5 class="card-title ">Payment Batches</h5>

        </div>
          </section>
     <asp:Label ID="lblBatchCode" runat="server" Text="0" Visible="False"></asp:Label>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Batch Type</label>
<asp:DropDownList ID="cboType" runat="server" CssClass="form-select"
                                        OnDataBound="cboType_DataBound">
                                    </asp:DropDownList>
          </div>
    
     <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Batch Status</label>
     <asp:DropDownList ID="cboBatchStatus" runat="server" CssClass="form-select"
                                        OnDataBound="cboBatchStatus_DataBound" >
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Batch No</label>
               
                 <asp:TextBox ID="txtBatchCode" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
               
                   <asp:TextBox ID="txtfromDate" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
               
                  <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
               
<asp:Button ID="btnOK"  runat="server" Style="margin-top:20px;" CssClass="btn btn-success btn-lg" Text="Search" OnClick="btnOK_Click" /> 
          </div>
    </div>
                                       </div>

                        <asp:Label ID="tranStatusLbl" runat="server" Visible="False"></asp:Label>
               
               
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View3" runat="server">
                                           <section class="section">

           <div class="row mb-4" style="justify-content:center">
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
                
               
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify"
                            PageSize="100">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnView" DataTextField="BatchCode" HeaderText="SUCCESSFUL"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="btnView1" DataTextField="BatchCode" HeaderText="FAILED"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:ButtonColumn CommandName="btnView2" DataTextField="BatchCode" HeaderText="PENDING"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                 <asp:ButtonColumn CommandName="btnView3" DataTextField="BatchCode" HeaderText="EXCLUDED"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="STATUS"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total" HeaderText="AMOUNT">
                                    <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Cleared" HeaderText="CLEARED">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RecordedBy" HeaderText="UPLOADED BY">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Date" HeaderText="DATE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
               
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%">
         <div class="text-center">
                <h6 class="card-title"> Batch Details</h6>
            </div>
                       
                
                
                        <asp:MultiView ID="MultiView5" runat="server">
                            <asp:View ID="View7" runat="server">
                                

                                            <section class="section">

           <div class="row mb-4" style="justify-content:center">
    <div class="col-lg-6" style="display:flex;">
        <div class="col-lg-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf2" runat="server"   Font-Bold="True" GroupName="FileFormat" Text="PDF" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel2" runat="server"  Font-Bold="True" GroupName="FileFormat"  Text="EXCEL" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvertDetails" runat="server"    CssClass="btn btn-primary w-100" OnClick="btnConvert_Click" Font-Bold="True"   Text="COVERT" />
        </div>
       
    </div>
</div>
               </section>

                            </asp:View>
                        </asp:MultiView>
                
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:Button ID="btnReturn" runat="server" Font-Bold="True" OnClick="btnReturn_Click"
                            Text="RETURN" Width="100px" /></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        &nbsp;
                        <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid2_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify"
                            PageSize="50">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchNo" HeaderText="BatchNo" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="CONTACT">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficiaryName" HeaderText="NAME">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PaymentNo" HeaderText="Payment Number">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranCharge" HeaderText="Pegasus Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PayOutFee" HeaderText="MNO Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CashOutFee" HeaderText="CashOut Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalCharge" HeaderText="Total Charge">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                
                                <asp:BoundColumn DataField="Status" HeaderText="Status">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                    </td>
                </tr>
                <tr>
                   <div class="container">
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp; &nbsp;
                        <asp:Label ID="lblPegasusTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblMnoFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblCashoutFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblAllTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                       </div>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View4" runat="server">
            <div class="text-center">
                <h6 class="card-title">Rejected Batches</h6>
            </div>
           
                
                        <asp:DataGrid ID="DataGrid3" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid3_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BatchID" HeaderText="BatchID" Visible="False">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnView" DataTextField="BatchCode" HeaderText="VIEW DETAILS"
                                    Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="STATUS" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total" HeaderText="AMOUNT">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RecordedBy" HeaderText="UPLOADED">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Date" HeaderText="DATE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
              
        </asp:View>
        <asp:View ID="View5" runat="server">
            <div class="text-center">
                <h6 class="card-title">Rejected Batch Details</h6>
            </div>
                        
              
                
                        <asp:DataGrid ID="DataGrid4" runat="server" AllowPaging="True" AutoGenerateColumns="False" class="table"
                            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchNo" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="CONTACT" Visible="true">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficiaryName" HeaderText="NAME">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranCharge" HeaderText="Charge">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
                
              
                        <asp:Label ID="lblShowTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
              
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView4" runat="server">
        <asp:View ID="View6" runat="server">
            <div class="text-center">
                <h6 class="card-title">Batch Audit trail</h6>
            </div>
                      
             
            <asp:DataGrid ID="DataGrid5" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table"
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
                font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="No." HeaderText="NO.">
                        <HeaderStyle Width="5%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Comment" HeaderText="COMMENT" Visible="true">
                        <HeaderStyle Width="20%" />
                        <ItemStyle Width="20%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RecordedBy" HeaderText="RECORDED BY">
                        <HeaderStyle Width="25%" />
                        <ItemStyle Width="25%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Date" HeaderText="DATE/TIME">
                        <HeaderStyle Width="20%" />
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid></asp:View>
    </asp:MultiView>&nbsp;
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    &nbsp;
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <asp:Label ID="Label1" runat="server" Text="0" Visible="False"></asp:Label><br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False"></CR:CrystalReportViewer>
</asp:Content>
