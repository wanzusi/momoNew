<%@ page language="C#" masterpagefile="~/NewAccounts.master" autoeventwireup="true" inherits="RegisterAccount, App_Web_raqppk4d" title="REGISTER ACCOUNT" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="section">
        <div class="text-center">
            <h5 class="card-title">EDIT/ADD ACCOUNT</h5>
        </div>
    </section>

    <section class="section">
        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="text-center">Search For Company</div>
                                
    <div class="row mb-2 justify-content-center">
<div class="col-lg-8" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Company Name</label>
            
              <asp:TextBox ID="txtsearchName" runat="server"  class="form-control"></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Company Code</label>
           <asp:TextBox ID="txtSearchCode" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnSearch" runat="server" class="btn btn-primary w-100" style="margin-top:18px;" OnClick="btnSearch_Click"
                                Text="Search" />

          </div>



            </div>
         
        <div class="text-center">
            <label class="form-label">Select Company</label>
            <asp:DropDownList ID="cboCompanyCode" runat="server" CssClass="form-select"
                                OnDataBound="cboCompanyCode_DataBound" Width="55%" AutoPostBack="True" OnSelectedIndexChanged="cboCompanyCode_SelectedIndexChanged">
                            </asp:DropDownList>
        </div>

    </div>

                            </asp:View>
            </asp:MultiView>

        
                                    <asp:MultiView ID="MultiView2" runat="server">
                                        <asp:View ID="View2" runat="server">
                                               <div class="row justify-content-center">
                                                <div class="col-lg-5 col-md-6">
    <div class="card" style="margin: 10px;">
      <div class="card-body">
        <h5 class="card-title">Account Details</h5>
 
        <div class="row g-3">
          
          <div class="col-12">
            <label for="inputEmail5" class="form-label">Company Name</label>
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
          </div>

            <div class="col-12">
            <label for="inputEmail5" class="form-label">Company Code</label>
          <asp:TextBox ID="txtCompanyCode" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
          </div>
            
            <div class="col-12">
                <label class="form-label">Account Name</label>
                <asp:TextBox ID="txtAccountName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-12">
            <label for="inputEmail5" class="form-label">Account Number</label>
          <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control" ></asp:TextBox>
          </div>

            <div class="col-12">
            <label for="inputEmail5" class="form-label">Account Type</label>
       <asp:DropDownList ID="cboAccountType" runat="server" CssClass="form-select "
                                                                                    OnDataBound="cboAccountType_DataBound"  AutoPostBack="True" OnSelectedIndexChanged="cboAccountType_SelectedIndexChanged">
                                                                                <asp:ListItem>--Select Account Type--</asp:ListItem>
                                                                                <asp:ListItem Value="ESCROW">ESCROW Account</asp:ListItem>
                                                                                <asp:ListItem Value="COMMISSION">Commission Account</asp:ListItem>
                                                                                <asp:ListItem Value="AIRTIMESUSP">AirTime Suspense</asp:ListItem>
                                                                            </asp:DropDownList>
          </div>

        

             <div class="col-12">
            <label for="inputEmail5" class="form-label">Network</label>
   <asp:DropDownList ID="cboNetwork" runat="server" CssClass="form-select" OnDataBound="cboNetwork_DataBound" >
                                                                                </asp:DropDownList>
          </div>


              <div class="col-md-6">
          <div class="form-check" style="margin-top:20px">
            <label for="inputCity" class="form-label">Is Active</label>
               <asp:CheckBox ID="chkActive" runat="server" CssClass="form-check-input" />
          
         
          </div>
        </div>
       

            <div class="col-12  text-center">
                <div class="col-md-6 text-center" >
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-75"   OnClick="btnSave_Click" Text="CREDIT ACCOUNT" />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnCancel" runat="server" Text="RETURN" class="btn btn-primary w-75" OnClick="btnCancel_Click" />
                </div>
            </div>
            
                                                        

            </div>
          </div>
        </div>
          </div>
                                                </div>
                                        </asp:View>
                                        </asp:MultiView>
            <asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
          
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>

    </section>



     
            

</asp:Content>





