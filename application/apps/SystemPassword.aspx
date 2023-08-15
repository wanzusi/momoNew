<%@ Page Language="C#" MasterPageFile="~/NewProfileTool.master" AutoEventWireup="true" CodeFile="SystemPassword.aspx.cs" Inherits="SystemPassword" Title="CHANGE YOUR SYSTEM PASSWORD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="container">

      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

              <div class="d-flex justify-content-center py-4">
                <a href="#" class="logo d-flex align-items-center w-auto">
                  <img src="images/peglogo.png" alt=""/>
                  <span class="d-none d-lg-block">Pegasus Technologies</span>
                </a>
</div>

              <div class="card mb-3">

                <div class="card-body">

                  <div class="pt-4 pb-2">
                    <h5 class="card-title text-center pb-0 fs-4">Change Your System Password</h5>
                    <p class="text-center small">Enter Prefered New password </p>
                  </div>

                  <div class="row g-3 needs-validation" novalidate runat="server" id="form2">

                       <div class="col-12">
                      <label for="yourUsername" class="form-label">Old Password</label>
                      <div class="input-group has-validation">
                      
                          <asp:TextBox ID="txtoldpw" runat="server"  class="form-control" TextMode="Password" onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>

                     
                        <div class="invalid-feedback">Please enter your old Password</div>
                      </div>
                    </div>


                    <div class="col-12">
                      <label for="yourUsername" class="form-label">New Password</label>
                      <div class="input-group has-validation">
                      
                          <asp:TextBox ID="txtnewpw" runat="server"  class="form-control" TextMode="Password" onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>


                     
                        <div class="invalid-feedback">Please enter your username.</div>
                      </div>
                    </div>

                    <div class="col-12">
                      <label for="yourPassword" class="form-label">Confrim Password</label>
                            <asp:TextBox ID="txtconfirmpw" runat="server"  class="form-control" TextMode="Password" onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                         
                          
                   
                      <div class="invalid-feedback">Please enter your password!</div>
                    </div>

 
                    <div class="col-12 text-center">
                 
                        
                               <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary w-50"  OnClick="BtnSave_Click"  Text="Change Password" />
                    
                    </div>

                      <div class="col-12">
                   
                     
                            <asp:Label ID="lblUserCode" runat="server" Text="0" Visible="False"></asp:Label>
    
                     <script type ="text/javascript">
                   function changeButtonText(button) {

                       button.value = "Please Wait...";

                   }
                   </script>
                      </div>
                   
                  </div>

                </div>
              </div>

         


            </div>
          </div>
        </div>

      </section>

    </div>


</asp:Content>



