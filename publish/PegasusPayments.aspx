<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="PegasusPayments, App_Web_gsh5rsca" title="NEW PAYMENT" culture="auto" uiculture="auto" enableviewstatemac="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />



    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
             <section class="section" >
                <div class="text-center">
                    <h5 class="card-title">
                        CHECK OUT
                    </h5>
                </div>
            </section>
            <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
              <div class="card mb-3">

                <div class="card-body">
                  <div class="text-center"> 
                      <h5 class="card-title">Transaction Details</h5>
                  </div>
                  <div class="row g-3 needs-validation" novalidate runat="server" id="form4">

                    <div class="col-12">
                      <label for="otp" class="form-label">Item Description</label>
                 
                       <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"
                                                    ></asp:TextBox>
               
                    <div class="col-12">
                   <label for="otp" class="form-label">Total Price for Item</label>
                       <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ></asp:TextBox>
                      </div>
                        
                    </div>

             <div class="col-12">
                   <label for="otp" class="form-label">Customers Name</label>
                   <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control" ></asp:TextBox>

             </div>
                

                      <div class="col-12">
                            <asp:Button ID="btnOK" runat="server"  Text="PAY FOR ITEM" Font-Bold="True" OnClick="btnOK_Click" class="btn btn-primary" />
                      </div>
                   
                  </div>

                </div>
              </div>
            </div>
          </div>
            </div>
      </section>
            
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />
</asp:Content>
