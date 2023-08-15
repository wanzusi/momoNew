<%@ page language="C#" masterpagefile="~/NewSystemTools.master" autoeventwireup="true" inherits="ManageVendor, App_Web_1vhklsuy" title="Untitled Page" enableviewstatemac="false" %>

 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
               
         <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
              <div class="card mb-3">

                <div class="card-body">
                  <div class="text-center"> 
                      <h5 class="card-title">CREATE VENDOR</h5>
                  </div>
                  <div class="row g-3 needs-validation" novalidate runat="server" id="form4">

                    <div class="col-12">
                      <label for="otp" class="form-label">Vendor Name</label>
                 <asp:TextBox ID="ddVendor" runat="server" class="form-control"  placeholder="Enter text" />
               
                    <div class="col-12">
                   <label for="otp" class="form-label">Vendor Code</label>
                           <asp:TextBox ID="vendorCode" runat="server"  class="form-control" placeholder="Enter text" />
                      </div>
                        
                    </div>

             <div class="col-12">
                   <label for="otp" class="form-label">Bank</label>
                    <asp:DropDownList ID="ddTelecom" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>

             </div>
                    <div class="col-12">
                   <label for="otp" class="form-label">Transaction Type</label>
                      <asp:DropDownList ID="ddTranType" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                      </div>

             </div>
                      <div class="col-12">
                   <label for="otp" class="form-label">OVA</label>
                      <asp:DropDownList ID="ddOvaChoice" runat="server" class="form-select" OnSelectedIndexChanged="ddTranCtegory_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
                      </div>

             </div>
                    <div class="col-12">
                   <label for="otp" id="SpIdlabel" runat="server" class="form-label">OVAName/SpId</label>
                      <asp:TextBox ID="txtSpId" runat="server"  class="form-control" placeholder="Enter text" />
                      </div>

             </div>
                  <div class="col-12">
                   <label for="otp"  id="SenderIdlabel" runat="server" class="form-label">SenderId</label>
                      <asp:TextBox ID="txtSenderId" runat="server"  class="form-control" placeholder="Enter text"  Visible="false" />
                      </div>

             </div>

                        <div class="col-12">
                   <label for="otp"  runat="server"   id="passwordlabel" class="form-label">Password/PIN</label>
                      <asp:TextBox ID="txtPin" runat="server"  CssClass="form-control"  Visible="false" ></asp:TextBox>
                        
                    </div>
              
                        <div class="col-12">
                   <label for="otp" class="form-label" runat="server" id="pullenabledlable">Enabled For Pulls</label>
                      <asp:DropDownList ID="enabledPulls" runat="server" class="form-select" Visible="false">
                                        <asp:ListItem Value="TRUE">True</asp:ListItem>
                                        <asp:ListItem Value="FALSE">False</asp:ListItem>
                                    </asp:DropDownList>
                        
                    </div>
              
                        <div class="col-12">
                   <label for="otp" class="form-label" runat="server" id="pushenabledlable">Enabled For Pushes</label>
                      <asp:DropDownList ID="enabledPushes" runat="server" CssClass="form-select" Visible="false">
                                        <asp:ListItem Value="TRUE">True</asp:ListItem>
                                        <asp:ListItem Value = "FALSE">False</asp:ListItem>
                                    </asp:DropDownList>
                        
                    </div>
              
                        <div class="col-12">
                   <label for="otp" class="form-label">Password/PIN</label>
                      <asp:TextBox ID="TextBox3" runat="server"  CssClass="form-control"  Visible="false" ></asp:TextBox>
                        
                    </div>

                      <div class="col-12">
                             <<asp:Button ID="btnSubmit" class="btn btn-primary w-100" runat="server" Text="SAVE" OnClick="btnSubmit_Click" />  
                      </div>
                   
                  </div>

                </div>

             </section>
        <div>
                    <div class="row" style="justify-content:center">
    <div class="col-lg-8" style="display:flex;">
        <div class="col-lg-2  text-center" style=" margin-top:auto; margin-bottom:auto">
            <asp:RadioButton ID="rdPdf" runat="server"   Font-Bold="True" GroupName="FileFormat" Visible="false" Text="PDF" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw; margin-top:auto; margin-bottom:auto">
             <asp:RadioButton ID="rdExcel" runat="server"  Font-Bold="True" GroupName="FileFormat" Visible="false"  Text="EXCEL" />
        </div>
        <div class="col-lg-2 text-center" style="margin-left:2vw">
             <asp:Button ID="btnConvert" runat="server"    CssClass="btn btn-primary w-100" OnClick="btnConvert_Click" Font-Bold="True"  Visible="false"  Text="COVERT" />
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
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="30" CellPadding="4" CellSpacing="2">
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
                    <%--    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>--%>
                        <%--/Scripts
                        <%--</form>--%>
                        <%--</div>--%>
         
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
   
</asp:Content>


