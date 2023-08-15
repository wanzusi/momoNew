<%@ Page Language="C#" MasterPageFile="~/ReportMaster2.master" AutoEventWireup="true" CodeFile="Graphs.aspx.cs" Inherits="Graphs" Title="Running Balances Graph" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <section class="section">
              <div class="text-center">
            <h5 class="card-title ">RUNNING BALANCES GRAPH TRANSACTIONS</h5>

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
            <label for="inputEmail5" class="form-label">Network</label>
<asp:DropDownList ID="cboNetwork" runat="server" CssClass="form-select"
                                 OnDataBound="cboNetwork_DataBound">
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
               
<asp:Button ID="btnOK"  runat="server" Style="margin-top:20px;" CssClass="btn btn-primary w-75" Text="Search" OnClick="btnOK_Click" /> 
          </div>
    </div>
                                       </div>



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

