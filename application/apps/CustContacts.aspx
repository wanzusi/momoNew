<%@ Page Language="C#" MasterPageFile="~/ReportMaster.master" AutoEventWireup="true" CodeFile="CustContacts.aspx.cs" Inherits="CustContacts" Title="CUSTOMER CONTACTS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <section class="section">
        <div class="text-center">
            <h5 class="card-title">  CUSTOMER CONTACTS FILE</h5>
        </div>
    </section>

          <div class="row mb-2 justify-content-center">
<div class="col-lg-10" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Phone No</label>
       <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
       <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Account No</label>
       <asp:TextBox ID="txtaccountno" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">From Date</label>
       <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
                <asp:TextBox ID="txttodate" runat="server" class="form-control"></asp:TextBox>
          </div>

    <div class="col-md-2">
        <asp:Button ID="btnOK" runat="server"   OnClick="btnOK_Click" CssClass="btn btn-primary w-75" style="margin-top:20px;"
                                Text="Search" />
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


                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid; width: 100%;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify" AllowPaging="True">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerRef" HeaderText="Account">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Name" HeaderText="Name">
                            <HeaderStyle Width="55%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>

    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    &nbsp;<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtaccountno" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtPhone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
    <br />
</asp:Content>

