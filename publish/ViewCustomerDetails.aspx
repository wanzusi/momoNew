<%@ page language="C#" masterpagefile="~/Customers.master" autoeventwireup="true" inherits="RegisterCustomer, App_Web_jzvpb42g" title="Register Customer" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="section">
        <div class="text-center">
            <h5 class="card-title">APPROVE CUSTOMER DETAILS </h5>
        </div>
    </section>
 
     <asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View3" runat="server">
           <div class="row mb-2">
<div class="col-lg-6" style="display: flex; justify-content:center">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Search (Names)</label>
           <asp:TextBox ID="txtSearch" runat="server" class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Customer Type</label>
            <asp:DropDownList ID="cboCustomerType" runat="server" OnDataBound="cboCustomerType_DataBound"  class="form-select">
                                                        </asp:DropDownList>
          </div>
      
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-100" style="margin-top:18px;" OnClick="btnOK_Click"
                                Text="Search" />

          </div>



            </div>
         

    </div>
    </asp:View>
</asp:MultiView>


    <section class="section">
        <asp:MultiView ID="MultiView3" runat="server">
                                        <asp:View ID="View4" runat="server">
                                            <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                                                OnPageIndexChanged="DataGrid1_PageIndexChanged" Style="border-right: #617da6 1px solid;
                                                border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                                text-align: justify" Width="100%">
                                                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                <EditItemStyle BackColor="#999999" />
                                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                                    Mode="NumericPages" />
                                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                                <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID" HeaderText="CustomerID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID" HeaderText="CustomerId" Visible="False">
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" />
                                                    </asp:BoundColumn>
                                                    <asp:ButtonColumn CommandName="btnapprove" DataTextField="Approved" HeaderText="Approve"
                                                        Text="Approve">
                                                        <HeaderStyle Width="13%" />
                                                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" ForeColor="Blue" Width="13%" />
                                                    </asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                                                        <HeaderStyle Width="30%" />
                                                        <ItemStyle Width="30%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CustomerType" HeaderText="CustomerType">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Active" HeaderText="Active">
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Approved" HeaderText="Approved" Visible="True">
                                                        <HeaderStyle Width="5%" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RecordedBy" HeaderText="CreatedBy">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Date" HeaderText="Created">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                            </asp:DataGrid></asp:View>
                                    </asp:MultiView> </section>

    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
    <section class="section">
 <div class="row justify-content-center">
<div class="col-lg-6">

    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">RETAIL ACCOUNT</h5>
  
        <!-- Multi Columns Form -->
        <div class="row g-3">
          
          <div class="col-md-3">
            <label for="inputEmail5" class="form-label">First Name</label>
            <asp:TextBox ID="txtFname" runat="server" class="form-control" ></asp:TextBox>
          </div>

              <div class="col-md-3">
            <label for="inputAddress5" class="form-label">Last Name</label>
            <asp:TextBox ID="txtLname" runat="server"  class="form-control" ></asp:TextBox>
          </div>

       
          <div class="col-md-3">
            <label for="inputAddress2" class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
          </div>

            <div class="col-md-3">
            <label for="inputAddress2" class="form-label">Confirm Email</label>
            <asp:TextBox ID="txtEmailReconf" runat="server" class="form-control"></asp:TextBox>
          </div>

          <div class="col-md-3">
            <label for="inputCity" class="form-label">Phone</label>
            <asp:TextBox ID="txtphone" runat="server" class="form-control"></asp:TextBox>
          </div>

          <div class="col-md-3">
            <label for="inputState" class="form-label">Gender</label>
               <asp:RadioButtonList ID="rbnGender" runat="server" RepeatDirection="Horizontal" CssClass="form-select"
                                                    Enabled="False">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>
                                                </asp:RadioButtonList>
          </div>
            <div class="col-md-3">
                <label class="form-label">Passport</label>
                 <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
             <div class="col-md-3">
                <label class="form-label">Driving Permit</label>
                 <asp:TextBox ID="txtDrivingPermit" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
             <div class="col-md-3">
                <label class="form-label">Other ID</label>
                 <asp:TextBox ID="txtOtherID" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
             <div class="col-md-3">
                <label class="form-label">Place Of Work</label>
                 <asp:TextBox ID="txtplaceofwork" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </div>
              <div class="col-md-3">
                <label class="form-label">Access Level</label>
              <asp:DropDownList ID="cboAccessLevel" runat="server" Enabled="False" OnDataBound="cboAccessLevel_DataBound"
                                                     Style="font: menu"
                                                    Width="60%">
                                                </asp:DropDownList>
            </div>
              <div class="col-md-3">
                <label class="form-label">Address</label>
                 <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </div>
            

             <div class="col-md-3">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Active</label>
            <asp:CheckBox ID="chkActive" runat="server" Enabled="False" />
         
          </div>
              <div class="col-md-3">
                <label class="form-label">User Type</label>
                <asp:DropDownList ID="cboUserType" runat="server" Enabled="False" OnDataBound="cboUserType_DataBound"  >
                                            </asp:DropDownList>
            </div>

            <div class="col-md-3">
                 <asp:DropDownList ID="cboCompany" runat="server" Enabled="False" OnDataBound="cboCompany_DataBound"  Visible="False">
                                                </asp:DropDownList>
            </div>

  <div class="col-md-3 txt-center">
 <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-100" Text="Approve Customer" OnClick="btnSave_Click" />
      <br />
      <asp:Label ID="lblun" runat="server" Font-Bold="True" ForeColor="#FF0000" Visible="False"></asp:Label>
  </div>

  
      </div>
    </div>
  </div>
  </div>
</div>
     </div>
 </section>

   </asp:View>
        <asp:View ID="View2" runat="server">
            <section>
                <div class="row">
                 <div class="col-lg-6">
                <div class="card">
            <div class="card-body">
              <h5 class="card-title">Vertical Form</h5>

              <!-- Vertical Form -->
              <div class="row g-3">
                <div class="col-12">
                  <label for="inputNanme4" class="form-label"> Name</label>
                 <asp:TextBox ID="txtFname2" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-12">
                  <label for="inputEmail4" class="form-label">Contact </label>
              <asp:TextBox ID="txtPhone2" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-12">
                  <label for="inputPassword4" class="form-label">Contact Person</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-12">
                  <label for="inputAddress" class="form-label">Email</label>
                  <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                   <div class="col-12">
                  <label for="inputAddress" class="form-label">Confirm Email</label>
                 <asp:TextBox ID="txtEmailReconf2" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                   <div class="col-12">
                  <label for="inputAddress" class="form-label">Address</label>
                 <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                    <div class="col-12">
                  <label for="inputAddress" class="form-label">Customer Type</label>
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
                  
           <div class="col-12">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Active</label>
            <asp:CheckBox ID="chkActive2" runat="server" Enabled="False" />
         
          </div>
           <div class="col-12">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Approved</label>
            <asp:CheckBox ID="chkApprove2" runat="server" Enabled="False" />
         
          </div>
                <div class="text-center">
             <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary w-100" Text="Approve Customer" OnClick="Button2_Click" />
      <br />
      <asp:Label ID="lblun2" runat="server" Font-Bold="True" ForeColor="#FF0000" Visible="False"></asp:Label>
                  
                </div>
              </div><!-- Vertical Form -->

            </div>
          </div>
                    </div>
                </div>
                     </div>
                    </div>
            </section>
        </asp:View>

        </asp:MultiView>

     <div class="container">
                                    <asp:Label ID="lblSave" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                                    <asp:Label ID="lblusername" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblpassword" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblIBAccountActive" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblEmailSent" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblMBAccountActive" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblNewPassword" runat="server" Text="Label" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblStatus" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <ajaxToolkit:ScriptManager ID="ScriptManager1" runat="server">
                                    </ajaxToolkit:ScriptManager>
                                    <asp:Label ID="Label4" runat="server" Text="0" Visible="False"></asp:Label>
                  </div>

       <script type ="text/javascript">
 
 function Comma(Num)
 {
       Num += '';
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       x = Num.split('.');
       x1 = x[0];
       x2 = x.length > 1 ? '.' + x[1] : '';
       var rgx = /(\d+)(\d{3})/;
       while (rgx.test(x1))
       x1 = x1.replace(rgx, '$1' + ',' + '$2');
       return x1 + x2;
 } 
    
   </script> 



    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtphone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    <br />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPhone2" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
</asp:Content>

