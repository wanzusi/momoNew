<%@ Page Language="C#" MasterPageFile="~/NewInternetwork.master" AutoEventWireup="true"
    CodeFile="AccountTransactionsNew.aspx.cs" Inherits="TransactionsNew" Title="TRANSACTIONS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div id="page-wrapper">

                <div class="container-fluid">
                   <div class="container">
                       
                                              <% 
                                                    string IsError = Session["IsError"] as string;
                                                    if (IsError == null)
                                                    {
                                                        Response.Write("<div>");

                                                    }
                                                    else if (IsError == "True")
                                                    {
                                                        Response.Write("<div class=\"alert alert-danger\">");

                                                    }
                                                    else
                                                    {
                                                        Response.Write("<div class=\"alert alert-success\">");
                                                    } 
                                                %>
                                                <strong>
                                                <asp:Label ID="lblmsg" runat="server"></asp:Label></strong>
                                                <%Response.Write("</div>"); %>
                                                <asp:Label ID="lblCount" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
                   </div>
                            
                   
  

                     <section class="section">
                          <div class="text-center">
                              
                                               <h5 class="card-title">ALL TRANSACTIONS</h5> 
                          </div>
                      </section>


                   
  <section class="section">
<div class="row">
<div class="col-lg-12">
        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-1">
            <label for="inputEmail5" class="form-label">Vendor</label>
           <asp:DropDownList ID="ddVendor" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="inputPassword5" class="form-label">Telecom</label>
             <asp:DropDownList ID="ddTelecom" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label">Transaction Type</label>
            <asp:DropDownList ID="ddTranType" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="inputAddress2" class="form-label">Transaction Category</label>
         <asp:DropDownList ID="ddTranCategory" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="inputCity" class="form-label">Status</label>
            <asp:DropDownList ID="ddStatus" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
            <div class="col-md-2">
                <label class="form-label">Phone</label>
                 <asp:TextBox ID="txtPhone" runat="server"  placeholder="Enter phone number" />
            </div>


             <div class="col-md-2">
                <label class="form-label">VendorId</label>
                 <asp:TextBox ID="txtVendorTranId" runat="server"  placeholder="Enter vendor tranid" />
            </div>

             <div class="col-md-2">
                <label class="form-label">PegPayId</label>
                 <asp:TextBox ID="txtPegPayId" runat="server"  placeholder="Enter PegPayId" />
            </div>

             <div class="col-md-2">
                <label class="form-label">TelecomId</label>
                 <asp:TextBox ID="txtTelecomId" runat="server"  placeholder="Enter telecom id" />
            </div>

             <div class="col-md-2">
                <label class="form-label">From Date</label>
                 <asp:TextBox ID="txtFromDate" runat="server"  placeholder="Enter From Date" />
            </div>
             <div class="col-md-2">
                <label class="form-label">To Date</label>
                 <asp:TextBox ID="txtToDate" runat="server"  placeholder="Enter to Date" />
            </div>

              <div class="col-md-2">
                <label class="form-label">Search</label>
                 <asp:Button ID="btnSubmit" CssClass="btn btn-primary w-100" runat="server" Text="Search" OnClick="btnSubmit_Click" /> 
            </div>
           
    </div>
  </div>

  </div>



 </section>
                    <div class="container">
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
  <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
</div>
                    <asp:MultiView runat="server" ID="Multiview2">
                            <asp:View runat="server" ID="resultView">                                
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="100" CellPadding="4" CellSpacing="2">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                            
                                            <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server" ID="EmptyView"></asp:View>
                        </asp:MultiView>
                 
                        <%-- /row --%>
                       
                        <%-- Scripts--%>
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
                        <%--/Scripts
                        <%--</form>--%>
                        <%--</div>--%>
                        <!-- /.row -->

                    </div>
                    <!-- /.container-fluid -->
            </div>
            <!-- /#page-wrapper -->
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
   
</asp:Content>
