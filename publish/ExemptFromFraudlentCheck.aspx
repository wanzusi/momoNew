<%@ page title="" language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="ExemptFromFraudlentCheck, App_Web_1vhklsuy" enableviewstatemac="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


          <section class="section">
                <div class="text-center">
                    <h5 class="card-title">EXEMPT FROM FRAUDLENT CHECK</h5>
                </div>
            </section>
     <div class="row mb-3">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">

   
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Vendor</label>
       <asp:DropDownList ID="cboVendor" runat="server" CssClass="form-select" OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList>
              </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Type</label>
             <asp:DropDownList ID="cboType" runat="server" CssClass="form-select"
                                Width="95%" Style="font: menu" OnSelectedIndexChanged="cboType_SelectedIndexChanged" AutoPostBack="True" OnDataBound="cboType_DataBound">
                                <asp:ListItem Text="One Number" Value="OTHER"></asp:ListItem>
                                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                            </asp:DropDownList>
          </div>

            <div class="col-md-2">
            <label for="UserCategory" class="form-label">Phone Number</label>
            <asp:TextBox ID="txtPhone" runat="server" class="form-control"></asp:TextBox>
          </div>
           
  
       
          <div class="col-md-2">
            <label for="inputAddress5" class="form-label"></label>
            <asp:Button ID="btnOK" runat="server" class="btn btn-primary w-100"  style="margin-top:18px;" OnClick="btnOK_Click"
                                Text="Exempt"  />

          </div>



            </div>
         

    </div>
    




</asp:Content>

