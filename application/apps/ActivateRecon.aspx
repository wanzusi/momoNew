<%@ Page Title="" Language="C#" MasterPageFile="~/NewReconciliation.master" AutoEventWireup="true" CodeFile="ActivateRecon.aspx.cs" Inherits="ActivateRecon" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Import
    Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <section class="section">
        <div class="text-center">
            <h5 class="card-title">ACTIVATE RECONCILIATION</h5>
        </div>
    </section>


    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View2" runat="server">
            <div class="text-center">
        <div class="row">
            <div class="col-12">
                 <asp:CheckBox ID="chkactivate" runat="server" Text="Tick To Activate" Font-Bold="True" AutoPostBack="True" OnCheckedChanged="chkResetPassword_CheckedChanged" />
            </div>
             <div class="col-md-2">
                 <asp:Button ID="btnOK" runat="server" Text="SAVE" class="btn btn-success w-100"  OnClick="btnOK_Click" />
             </div>
        </div>
    </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

