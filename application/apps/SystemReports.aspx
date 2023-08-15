<%@ Page Language="C#" MasterPageFile="~/NewReports.master" AutoEventWireup="true"
    CodeFile="SystemReports.aspx.cs" Inherits="SystemReports" Title="SYSTEM REPORTS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
   

                <div class="text-center">
                  
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
            <h5 class="card-title">SYSTEM REPORTS</h5>
        </div>
    </section>
  <div class="row mb-4 justify-content-center">
<div class="col-lg-12 mb-3" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Company</label>
         <asp:DropDownList ID="ddCompany" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Telecom</label>
              <asp:DropDownList ID="ddTelecom" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">TelecomId</label>
           <asp:TextBox ID="telecomId" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>

    <div class="col-md-2">
            <label for="UserCategory" class="form-label">Reports</label>
    
                                    <asp:DropDownList ID="ddReport" runat="server" class="form-select" OnSelectedIndexChanged="ddReport_OnSelectedIndexChanged">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>

    <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
           <asp:TextBox ID="txtFromDate" runat="server" class="form-control" placeholder="Enter text" />
          </div>
      
    </div>
      <div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">To Date</label>
            
                             <asp:TextBox ID="txtToDate" runat="server" class="form-control" placeholder="Enter text" />
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Search Value</label>
            <asp:TextBox ID="txtValue" runat="server" class="form-control" placeholder="Enter text" />
          </div>
    <div class="col-md-2" id ="simpleFilterTable" runat="server">
            <label for="UserCategory" class="form-label">Column Filter</label>
                     <asp:TextBox ID="simpleFilter" runat="server"  CssClass="form-control"  />
          </div>

    <div class="col-md-2" id="advancedFilterTable" runat="server">
            <label for="UserCategory" class="form-label">Advanced Search</label>
        <asp:TextBox ID="advancedFilter" runat="server" Style="font: menu" class="form-control" />
          </div>

    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
               <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-primary w-50" Style="margin-top:20px;" OnClick="btnSubmit_Click" /> 
          </div>
      
    </div>
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
<asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
    </section>
         
                  
                        <asp:MultiView runat="server" ID="Multiview2">
                            <asp:View runat="server" ID="resultView">                                
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="30" CellPadding="4" CellSpacing="2" OnRowCommand="dataGridResults_RowCommand">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                            
                                            <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="Action" Text="Edit" Visible="false" />
                                        </Columns>
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

        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
   
</asp:Content>
