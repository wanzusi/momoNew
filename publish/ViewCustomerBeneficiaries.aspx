<%@ page language="C#" masterpagefile="~/NewBeneficiaries.master" autoeventwireup="true" inherits="ViewCustomerBeneficiaries, App_Web_rnqohcbv" title="VIEW CUSTOMER BENEFICIARIES" enableviewstatemac="false" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
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
                <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
              <div class="text-center">
            <h5 class="card-title ">CUSTOMER BENEFICIARY</h5>

        </div>
          </section>

      <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search String(Names)</label>
;<asp:TextBox ID="txtSearch" runat="server" class="form-control"></asp:TextBox>
          </div>
    
     <div class="col-md-3">
            <label for="inputEmail5" class="form-label">Phone Number</label>
       <asp:TextBox ID="txtSearchPhone" runat="server" class="form-control" />
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Location</label>
               
                  <asp:TextBox ID="txtdistrict" runat="server" class="form-control" />
          </div>
     <div class="col-md-2">
            <label for="UserCategory" class="form-label">Beneficiary Type</label>
               <asp:DropDownList ID="cboBeneficiaryType" runat="server" CssClass="form-select"
                                >
                            </asp:DropDownList>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
               
<asp:Button ID="btnOK"  runat="server" Style="margin-top:20px;" CssClass="btn btn-success btn-lg" Text="Search" OnClick="btnOK_Click" /> 
          </div>
    </div>
                                       </div>

                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View3" runat="server">
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
        <
    
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="30" CellPadding="4" CellSpacing="2"  OnRowCommand="dataGridResults_RowCommand">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#0375b7" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="editing" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="deletion" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="cssPager" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                                        </asp:GridView>
                        </div>
                    </asp:View>
                    <asp:View ID="AccountDetailsView" runat="server">
                                <div class="container">
                                    <div class="row">
                                        <div class="well col-lg-10 col-xs-10 col-sm-10 col-md-6 col-xs-offset-1 col-sm-offset-1 col-md-offset-3">
                                            <div class="row">
                                                <div class="col-xs-6 col-sm-6 col-md-6">
                                                        Name: <strong>
                                                            <asp:Label ID="lblName" runat="server">
                                                            </asp:Label></strong>
                                                        <br />
                                                        Phone:
                                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                        <br />
                                                        Location:
                                                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                        <br />
                                                        Type:
                                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <abbr title="Filer">
                                                            Filer:</abbr>
                                                        <asp:Label ID="lblFiler" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-xs-6 col-sm-6 col-md-6 text-right">
                                                    <p>Created On
                                                        <asp:Label ID="lblDate" runat="server">
                        <em>Date: </em></asp:Label>
                                                    </p>
                                                    <p>
                                                        <em>Beneficiary ID #:
                                                            <asp:Label ID="lblbeneficiaryId" runat="server"></asp:Label></em>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="text-center">
                                                    <label>
                                                            Reason</label>
                                                        <asp:TextBox ID="txtReason" runat="server" style="width:940px" CssClass="form-control" />
                                                        <p class="help-block">
                                                            Why are you deleting this beneficiary?</p>
                                                </div>
                                                <div class="row" id="buttons" runat="server" visible="true">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-primary" Text="Confirm"
                                                            OnClick="btnConfirm_Click" />
                                                       <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-primary" Text="Back"
                                                            OnClick="btnReturn_Click" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     
                                </div>
                            </asp:View>
                </asp:MultiView>
     
    <br />
</asp:Content>

