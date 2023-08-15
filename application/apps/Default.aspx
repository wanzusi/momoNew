<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    EnableEventValidation="false" Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Threading" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <title>PEGPAY - PAYMENTS INTERFACE PORTAL </title>
    <link href="scripts/WQC_stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="scripts/globalscape.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="//static.addtoany.com/menu/page.js"></script>

    <script type="text/javascript" src="ddtabmenufiles/ddtabmenu.js">
    

    </script>
    <meta charset="utf-8">



  <meta content="PEGASUS , TECHNOLOGIES" name="description">
  <meta content="" name="keywords">

  <!-- Favicons -->
  <link href="images/peglogo.png" rel="icon">
  <link href="images/peglogo.png" rel="apple-touch-icon">

  <!-- Google Fonts -->
  <link href="https://fonts.gstatic.com" rel="preconnect">
  <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

  <!--  CSS Files -->
  <link href="assets/CSS/bootstrap.min.css" rel="stylesheet"/>
  <link href="assets/ICONS/bootstrap-icons.css" rel="stylesheet"/>
  <link href="assets/boxicons/css/boxicons.min.css" rel="stylesheet"/>
  <link href="assets/quill/quill.snow.css" rel="stylesheet"/>
  <link href="assets/quill/quill.bubble.css" rel="stylesheet"/>
  <link href="assets/remixicon/remixicon.css" rel="stylesheet"/>
  <link href="assets/simple/style.css" rel="stylesheet"/>


  <!-- Template Main CSS File -->
  <link href="assets/CSS/style.css" rel="stylesheet">

    <!-- CSS for Tab Menu #4 -->
    <link rel="stylesheet" type="text/css" href="ddtabmenufiles/ddcolortabs_First.css" />

    <script type="text/javascript">
//SYNTAX: ddtabmenu.definemenu("tab_menu_id", integer OR "auto")
ddtabmenu.definemenu("ddtabs1", 0) //initialize Tab Menu #1 with 1st tab selected
ddtabmenu.definemenu("ddtabs2", 1) //initialize Tab Menu #2 with 2nd tab selected
ddtabmenu.definemenu("ddtabs3", 1) //initialize Tab Menu #3 with 2nd tab selected
ddtabmenu.definemenu("ddtabs4", 2) //initialize Tab Menu #4 with 3rd tab selected
ddtabmenu.definemenu("ddtabs5", -1) //initialize Tab Menu #5 with NO tabs selected (-1)

    </script>

    <style type="text/css">
        
        .style12
        {
            width: 100%;
        }
         .style13
        {
            width: 99%;
        }
    
        </style>
</head>



<body>

  <main>

 <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
      <asp:View ID="View2" runat="server">
        <div class="container">

      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

              <div class="d-flex justify-content-center py-4">
                <a href="#" class="logo d-flex align-items-center w-auto">
                  <img src="images/peglogo.png" alt="">
                  <span class="d-none d-lg-block">Pegasus Technologies</span>
                </a>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div><!-- End Logo -->

              <div class="card mb-3">

                <div class="card-body">

                  <div class="pt-4 pb-2">
                    <h5 class="card-title text-center pb-0 fs-4">Login to Your Account</h5>
                    <p class="text-center small">Enter your username & password to login</p>
                  </div>

                  <form class="row g-3 needs-validation" novalidate runat="server" id="form1">
                      <asp:Label ID="lblmsg" runat="server" class="form-label" Style="font: menu" Text="."></asp:Label>
                    <div class="col-12">
                      <label for="yourUsername" class="form-label">Username</label>
                      <div class="input-group has-validation">
                        <span class="input-group-text" id="inputGroupPrepend">@</span>
                          <asp:TextBox ID="txtUsername" runat="server"  class="form-control" onblur="Change(this, event)"   onfocus="Change(this, event)"></asp:TextBox>

                     
                        <div class="invalid-feedback">Please enter your username.</div>
                      </div>
                    </div>

                    <div class="col-12">
                      <label for="yourPassword" class="form-label">Password</label>
                          <asp:TextBox ID="txtpassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                   
                      <div class="invalid-feedback">Please enter your password!</div>
                    </div>

 
                    <div class="col-12 mb-2">

                         <asp:Button ID="Btnlogin" runat="server" class="btn btn-primary w-100 " Text="Login"  Onclick="Btnlogin_Click"/>
                     
                    </div>
                   
                  </form>

                </div>
              </div>

            <footer id="footer" class="footer">
            <div class="copyright">
                  &copy; Copyright <strong><span style="margin-right:3px;">Pegasus Technologies</span></strong><%= DateTime.Now.Year %></div>
    
             </footer>

            </div>
          </div>
        </div>

      </section>

    </div>
          </asp:View>

      <asp:View ID="View1" runat="server">
        <div class="container">

      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

              <div class="d-flex justify-content-center py-4">
                <a href="#" class="logo d-flex align-items-center w-auto">
                  <img src="images/peglogo.png" alt="">
                  <span class="d-none d-lg-block">Pegasus Technologies</span>
                </a>
</div><!-- End Logo -->

              <div class="card mb-3">

                <div class="card-body">

                  <div class="pt-4 pb-2">
                    <h5 class="card-title text-center pb-0 fs-4">Change Your System Password</h5>
                    <p class="text-center small">Enter Prefered New password </p>
                  </div>

                  <form class="row g-3 needs-validation" novalidate runat="server" id="form2">

                    <div class="col-12">
                      <label for="yourUsername" class="form-label">New Password</label>
                      <div class="input-group has-validation">
                        <span class="input-group-text" id="inputGroupPrepend">@</span>
                          <asp:TextBox ID="txtNewPassword" runat="server"  class="form-control" TextMode="Password" onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>


                     
                        <div class="invalid-feedback">Please enter your username.</div>
                      </div>
                    </div>

                    <div class="col-12">
                      <label for="yourPassword" class="form-label">Confrim Password</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server"  class="form-control" TextMode="Password" onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                         
                          
                   
                      <div class="invalid-feedback">Please enter your password!</div>
                    </div>

 
                    <div class="col-12">
                      <div class="row">
                          <div class="col-6 "> 
                               <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary w-50"  OnClick="BtnSave_Click"  Text="Save" />
                          </div>
                          <div class="col-6">
                               <asp:Button ID="btnCancel" runat="server"   class="btn btn-primary w-50"  OnClick="btnCancel_Click"    Text="Cancel"  />
                          </div>
                      </div>
                        
                    </div>

                      <div class="col-12">
                           <asp:Label ID="Label1" runat="server" Text="0" Visible="False"></asp:Label>
                           <asp:Label ID="Label2" runat="server" Text="0" Visible="False"></asp:Label>
                      </div>
                   
                  </form>

                </div>
              </div>

               <footer id="footer" class="footer">
            <div class="copyright">
                  &copy; Copyright <strong><span>Pegasus Technologies</span></strong><%= DateTime.Now.Year %></div>
    
             </footer>


            </div>
          </div>
        </div>

      </section>

    </div>
          </asp:View>
     <asp:View ID="View3" runat="server">
        <div class="container">

      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">

              <div class="d-flex justify-content-center py-4">
              
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div><!-- End Logo -->

              <div class="card mb-3">

                <div class="card-body">

                  <form class="row g-3 needs-validation" novalidate runat="server" id="form3">
                    <div class="col-12">
                         <asp:Label ID="lblQn" runat="server" Font-Bold="True" ForeColor="Maroon" Text=".">

                              </asp:Label>
                      <div class="row">
                          <div class="col-6 "> 
                             <asp:Button ID="btnYes" runat="server" CssClass="btn btn-primary w-50" OnClick="btnYes_Click" Text="Yes" Font-Bold="True"
                                                                             />
                                                                           
                              
                          </div>
                          <div class="col-6">
                               <asp:Button ID="btnNo" CssClass="btn btn-primary w-50" runat="server" OnClick="btnNo_Click" Text="No" Font-Bold="True" />
                               
                          </div>
                      </div>
                        
                    </div>

                    
                   
                  </form>

                </div>
              </div>
             <footer id="footer" class="footer">
            <div class="copyright">
                  &copy; Copyright <strong><span>Pegasus Technologies</span></strong><%= DateTime.Now.Year %></div>
    
             </footer>


            </div>
          </div>
        </div>

      </section>

    </div>
          </asp:View>

     <asp:View ID="View4" runat="server">
        <div class="container">

      <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-6 col-md-6 d-flex flex-column align-items-center justify-content-center">
              <div class="card mb-3">

                <div class="card-body">

           
                  <form class="row g-3 needs-validation" novalidate runat="server" id="form4">

                    <div class="col-12">
                      <label for="otp" class="form-label"> Enter One Time PIN Sent To Your  Email OR Phone</label>
                        <br />
                         <label for="otp" class="form-label">(check in spam if mail not delivered in 3 minutes)</label>
                        <asp:TextBox ID="PinPrompt" runat="server"  TextMode="Password" class="form-control"/>
                           
                      <div class="invalid-feedback">iinvalid OTP</div>
                    </div>

 
                    <div class="col-12">
                      <div class="row">
                          <div class="col-4 "> 
                               <asp:Button ID="BtnPromptResend" runat="server" CssClass="btn btn-primary w-100"  OnClick="BtnPromptResend_Click"  Text="ResendOTP" />

                               
                                                                           
                                                                           
                          </div>
                          <div class="col-4">
                               <asp:Button ID="BtnPinPrompt" runat="server"  CssClass="btn btn-success w-100" OnClick="BtnPinPrompt_Click" Text="Submit OTP" />
                           


                          </div>
                           <div class="col-4">
                              
                               <asp:Button ID="BtnPinPromptCancel" runat="server" class="btn btn-danger w-100"  OnClick="BtnPinPromptCancel_Click"  Text="Cancel" />
                           


                          </div>
                      </div>
                        
                    </div>

                      <div class="col-12">
                             <asp:Label ID="Label3" runat="server" Text="0" Visible="False"></asp:Label>
                              <asp:Label ID="Label4" runat="server" Text="0" Visible="False"></asp:Label>
                      </div>
                   
                  </form>

                </div>
              </div>


            </div>
          </div>
        </div>

      </section>

    </div>
          </asp:View>
 </asp:MultiView>
 
  </main>

  <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

  <!--  JS Files -->
  <script src="assets//apexcharts/apexcharts.min.js"></script>
  <script src="assets//bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="assets//chart.js/chart.umd.js"></script>
  <script src="assets//echarts/echarts.min.js"></script>
  <script src="assets//quill/quill.min.js"></script>
  <script src="assets//simple-datatables/simple-datatables.js"></script>
  <script src="assets//tinymce/tinymce.min.js"></script>
  <script src="assets//php-email-form/validate.js"></script>

  <!-- Template Main JS File -->
  <script src="assets/js/main.js"></script>

</body>

<script language="javascript" type="text/javascript">
//Function to disable Cntrl key/right click
function DisableControlKey(e) {
// Message to display
var message = "Cntrl key/ Right Click Option disabled";
// Condition to check mouse right click / Ctrl key press
if (e.which == 17 || e.button == 2) {
alert(message);
return false;
}
}

    function changeButtonText(button) {

        button.value = "Please Wait...";

    }
function TABLE1_onclick() {

}

</script>

</html>
