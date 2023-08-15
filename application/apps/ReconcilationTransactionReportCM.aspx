<%@ Page Language="C#" MasterPageFile="~/NewReconciliation.master" AutoEventWireup="true" CodeFile="ReconcilationTransactionReportCM.aspx.cs" Inherits="ReconcilationTransactionReportCM" Title="RECONCILIATION REPORT" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[


// ]]>
</script>

        <section class="section">
        <div class="text-center">
            <h5 class="card-title"> RECONCILATION REPORT</h5>
        </div>
    </section>

            
    <div class="row mb-2 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Vendor</label>
<asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select"
                                OnDataBound="cboVendor_DataBound"  >
                            </asp:DropDownList>
          </div>
    
       <div class="col-md-2">
            <label for="UserCategory" class="form-label">Vendor Trans Id</label>
                <asp:TextBox ID="txtvendortransid" runat="server" class="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Status</label>
  <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-select"
                                >
                            </asp:DropDownList>
          </div>
       

     <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                <asp:TextBox ID="txtfromDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">TO Date</label>
                <asp:TextBox ID="txttoDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    </div>
        <div class="col-lg-12 mt-4" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Phone Number</label>
<asp:TextBox ID="txtphonenumber" runat="server" class="form-control"></asp:TextBox>
          </div>
    
       <div class="col-md-2">
            <label for="UserCategory" class="form-label">Telecom Id</label>
                <asp:TextBox ID="txttelecomid" runat="server" class="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">OVAs</label>
  <asp:DropDownList ID="ddlOvas" runat="server" CssClass="form-select"
                                OnDataBound="ddlOvas_DataBound" >
                            </asp:DropDownList>
          </div>
       

     <div class="col-md-2">
            <label for="UserCategory" class="form-label">Cust Name</label>
                <asp:TextBox ID="txtCustName" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
             <asp:Button ID="btnOK" runat="server"  OnClick="btnOK_Click" CssClass="btn btn-primary w-75" Style="margin-top:20px"
                                Text="Search"  />
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
      

                <asp:GridView ID="Gvuploadedreading"                           
                           runat="server" CellPadding="3" visible="false"
                           ForeColor="#333333"                            
                           CssClass="table table-hover table-responsive text-center" 
                           HorizontalAlign="Center"                            
                           AllowPaging="True" 
                           onpageindexchanging="Gvuploadedreading_PageIndexChanging" 
                           OnRowDataBound="Gvuploadedreading_RowDataBound"                       
                  >
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#752828" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                               <HeaderStyle BackColor="#b70303" HorizontalAlign="Center" Font-Bold="True" ForeColor="White"  />                             
                                 <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#752828" />
                            
                          </asp:GridView>
       <div class="text-center">
             <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label>
       </div>
              

    <br />
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
