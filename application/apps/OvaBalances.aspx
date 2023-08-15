<%@ Page Language="C#" MasterPageFile="~/NewAccounts.master" AutoEventWireup="true" CodeFile="OvaBalances.aspx.cs" Inherits="OvaBalances" Title="OVA BALANCES" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="section">

          <section class="section">
              <div class="text-center">
            <h5 class="card-title ">OVA Accounts</h5>

        </div>
          </section>
        
         <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        
            <div class="row justify-content-center mb-4">
              <div class="col-10" style="display:flex; justify-content:space-evenly">
           
                <div class="col-md-2">
                   
                    <label class="form-label">Choose Network</label>
                 <asp:DropDownList ID="ddNetwork" runat="server" class="form-select"
                               style="font: menu" OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="ddNetwork_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>

                </div>
                 <div class="col-md-2">
                    
                     <label class=" form-label">Choose OVA</label>
                   <asp:DropDownList ID="cboOvaAccount" runat="server" class="form-select"
                              OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="cboOvaAccount_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>

                </div>
                <div class="col-md-2">
                 
                    <label class="form-label">From Date</label>
                  <asp:TextBox ID="txtFromDate" runat="server" class="form-control" ></asp:TextBox>

                </div>
                <div class="col-md-2">
                     <label class="form-label">To Date</label>
                     <asp:TextBox ID="txtToDate" runat="server" class="form-control" ></asp:TextBox>

                </div>
               

               <div class="col-md-2">
                   <label class="form-label"></label>
                <asp:Button ID="btnSearch" runat="server"   Text="Search" class="btn btn-primary w-100" style="margin-top:18px" onClick="btnSearch_Click"  />
                 </div>

            </div>

        </div>
                    </asp:View>
             </asp:MultiView>
        <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server"> 
                           <div class="row  mb-4" style="justify-content:center;">
    <div class="col-lg-8" style="display:flex;">
        <div class="col-lg-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf" runat="server"   Font-Bold="True" GroupName="FileFormat" Text="PDF" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel" runat="server"  Font-Bold="True" GroupName="FileFormat"  Text="EXCEL" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-success w-100" OnClick="btnConvert_Click" Font-Bold="True"   Text="COVERT" />
        </div>
      
    </div>
</div>
                         <asp:GridView ID="Gvuploadedreading"                           
                           runat="server" CellPadding="3" visible="false"
                           ForeColor="#333333"                            
                           CssClass="table table-hover table-responsive text-center" 
                           HorizontalAlign="Center"                            
                           AllowPaging="True" 
                           onpageindexchanging="Gvuploadedreading_PageIndexChanging"                        
                  >
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#752828" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                               <HeaderStyle BackColor="#b70303" HorizontalAlign="Center" Font-Bold="True" ForeColor="White"  />                             
                                 <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#752828" />
                            
                          </asp:GridView>
                        </asp:View>
            </asp:MultiView>
        <div class="text-center">
              <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
        </div>
    </section>
                        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                            EnableScriptLocalization="true">
                        </ajaxToolkit:ToolkitScriptManager>
                        <br />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False"></cr:crystalreportviewer>
</asp:Content>

