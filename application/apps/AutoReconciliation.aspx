<%@ Page Language="C#" MasterPageFile="~/NewReconciliation.master" AutoEventWireup="true" CodeFile="AutoReconciliation.aspx.cs" Inherits="AutoReconciliation" Title="AUTO RECONCILIATION" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
      <section class="section">
              <div class="text-center">
            <h5 class="card-title ">FILE RECONCILIATION</h5>

        </div>
          </section>
    
 
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
                <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Telecom</label>
  <asp:DropDownList ID="cboTelecom" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="cboTelecom_SelectedIndexChanged">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">OVA</label>
       <asp:DropDownList ID="ddOva" runat="server" class="form-select">
                                       
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Date</label>
                 <asp:TextBox ID="txtFromDate" runat="server" class="form-control" placeholder="Enter from date" />
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">From Hour</label>
               <asp:DropDownList ID="ddFromHour" runat="server" class="form-select">                                      
                                    </asp:DropDownList>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Date</label>
                <asp:TextBox ID="txtToDate" runat="server" class="form-control"></asp:TextBox>
          </div>
    </div>

     
    

         <div class="col-lg-12 mt-3" style="display: flex; justify-content:space-evenly">
                          <div class="col-md-2">
            <label for="UserCategory" class="form-label">To Hour</label>
                <asp:DropDownList ID="ddToHour" runat="server" CssClass="form-select">                                      
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Reconcile From Archieves</label>
          <asp:CheckBox Text="" ID="chkboxArchieves" CssClass="form-check-input" runat="server" />
          </div>
    
     <div class="col-md-2">
            <label for="inputEmail5" class="form-label">File</label>
      <asp:FileUpload ID="FileUpload1" runat="server" />
          </div>
        <div class="col-md-2">
        <asp:Button ID="btnSubmit" runat="server"   OnClick="btnOK_Click" CssClass="btn btn-primary w-75" style="margin-top:20px;"
                                Text="Send For Reconciliation" />
    </div>
    </div>
 

    
              </div>


          
        </asp:View>
       
        <asp:View ID="View2" runat="server">
          
                
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
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-primary w-75" OnClick="Button3_Click" Font-Bold="True"   Text="COVERT" />
        </div>

        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="Button2" runat="server"    CssClass="btn btn-danger w-75" OnClick="Button1_Click" Font-Bold="True"   Text="RETURN" />
        </div>
       
    </div>
</div>

    </section>
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" class="table"
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="Horizontal" HorizontalAlign="Justify" PageSize="50" Style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                            border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="No." HeaderText="No.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VendorRef" HeaderText="Agent Ref">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PayDate" HeaderText="Pay Date">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TransactionAmount" HeaderText="Amount">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Justify" Width="35%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Justify" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                        </asp:DataGrid>
                    <div class=" text-center">
                        <asp:Button ID="Button4" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="CONVERT TO PDF" Width="150px" Font-Bold="True" OnClick="Button3_Click" style="font: menu" />
                        <asp:Button ID="Button1" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="RETURN" Width="150px" Font-Bold="True" OnClick="Button1_Click" style="font: menu" />
                    </div>
                        
            
        </asp:View>
    </asp:MultiView>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />

</asp:Content>



