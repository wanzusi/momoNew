<%@ page language="C#" masterpagefile="~/NewSystemTools.master" autoeventwireup="true" inherits="VendorUtilityCredentials, App_Web_1vhklsuy" title="Vendor Utility Credentials" enableviewstatemac="false" %>

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
                    <h5 class="card-title"> SWITCH UTILITY VALIDATION</h5>
                </div>
            </section>
                       
                <div class="row mb-3 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">

   
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Vendor</label>
         <asp:DropDownList ID="ddVendor" runat="server" class="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Utility</label>
              <asp:DropDownList ID="ddUtility" runat="server" CssClass="form-select">
                                        <asp:ListItem>True</asp:ListItem>
                                        <asp:ListItem>False</asp:ListItem>
                                    </asp:DropDownList>
          </div>
            <div class="col-md-2">
            <label for="UserCategory" class="form-label">Utility</label>
            <asp:DropDownList ID="ddState" runat="server" class="form-select">
                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                        <asp:ListItem Text="OFFLINE" Value="true"></asp:ListItem>
                                        <asp:ListItem Text="ONLINE" Value="false"></asp:ListItem>
                                    </asp:DropDownList>
          </div>
            <div class="col-md-2">
                 <label for="UserCategory" class="form-label">Search Parameter</label>
                   <asp:TextBox ID="txtReference" runat="server" class="form-control" placeholder="Enter text" />
            </div>
  
       
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="Button3" runat="server" class="btn btn-primary w-75" style="margin-top:20px;" OnClick="btnSubmit_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>

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
                        <asp:Button ID="btnSwitch" class="btn btn-primary w-25" runat="server" Text="Switch" OnClick="btnSwitch_Click" visible="false"/>                                  

                        <asp:MultiView runat="server" ID="Multiview2">
                            <asp:View runat="server" ID="resultView">                                
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults" OnRowCommand="dataGridResults_RowCommand">
                                            <Columns>
                                            <asp:TemplateField HeaderText="Edit" Visible="true">
                                              <ItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRow" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server" ID="EmptyView"></asp:View>
                        </asp:MultiView>
                        <%-- /row --%>
                       
                      

 
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
   
</asp:Content>

