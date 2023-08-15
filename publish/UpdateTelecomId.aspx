<%@ page language="C#" masterpagefile="~/NewReconciliation.master" autoeventwireup="true" inherits="UpdateTelecomId, App_Web_rgb2gjqg" title="UPDATE TELECOM ID" enableviewstatemac="false" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                <section class="section">
              <div class="text-center">
            <h5 class="card-title ">UPDATE TRANSACTIONS</h5>

        </div>
          </section>

       
             <div class="text-center mb-2">
                  <asp:RadioButtonList ID="rbnMethod" runat="server" AutoPostBack="True"
                                Font-Bold="True" OnSelectedIndexChanged="rbnMethod_SelectedIndexChanged" RepeatDirection="Horizontal"  Width="80%"
                                 >
                                <asp:ListItem Value="0">One By One UPDATE</asp:ListItem>
                                <asp:ListItem Value="1">BULK UPLOAD</asp:ListItem>
                            </asp:RadioButtonList>
             </div>
                           
          
    
                      <asp:MultiView ID="MultiView1" runat="server">
   
                        <asp:View ID="View1" runat="server">
                            <table>
                                <tr>
                                    <td style="height: 22px">
                                        Sent To Telecom</td>
                                    <td style="width: 322px; height: 22px">
                                        <asp:CheckBox ID="CbxSentToMomo" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Sent To Pegpay</td>
                                    <td style="width: 322px">
                                        <asp:CheckBox ID="CbxSentToPegpay" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Sent To School</td>
                                    <td style="width: 322px">
                                        <asp:CheckBox ID="CbxSentToSchool" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Payment Id</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txtPaymentId" runat="server" Enabled="False" Width="231px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Payment Amount</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txtPaymentAmount" runat="server" Enabled="False" Width="231px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 322px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Momo Amount</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txtMomoAmount" runat="server" Width="231px" Enabled="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        MomoId</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txt_momoId" runat="server" Width="231px" Enabled="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Telecom Id</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txt_TelecomIds" runat="server" Width="231px" Enabled="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 322px">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancle" Width="157px" ForeColor="red"
                                            OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnFinish" runat="server" Text="Finish Operation" Width="157px" ForeColor="green"
                                            OnClick="btnFinish_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                                                            <div class="row mb-4 justify-content-center">
<div class="col-lg-12" style="display: flex; justify-content:space-evenly">
          <div class="col-md-2">
            <label for="inputEmail5" class="form-label">Network</label>
 <asp:DropDownList ID="cboNetwork" runat="server" CssClass="form-select"
                                 OnDataBound="cboNetwork_DataBound">
                            </asp:DropDownList>
          </div>
    
     <div class="col-md-3">
            <label for="inputEmail5" class="form-label">PegPay ID</label>
       <asp:TextBox ID="txtpegpayid" runat="server" class="form-control" />
          </div>
          <div class="col-md-2">
            <label for="UserCategory" class="form-label">Telecoom Id</label>
               
                       <asp:TextBox ID="TelecomId" runat="server" class="form-control"></asp:TextBox>
          </div>
    <div class="col-md-2">
            <label for="UserCategory" class="form-label"></label>
               
 <asp:Button ID="btnOK" runat="server"  Text="Update" CssClass="btn btn-primary w-75" Style ="margin-top:18px;"
                    OnClientClick="return confirm('Do you really want to update?');" OnClick="btnUpdate_Click"
                     />
          </div>
    </div>
                                       </div>


           
                            </asp:View>
                          <asp:View ID="View3" runat="server">

                              <div class="row mt-2">
                                  <div class="col-md-2">
                                      <label class="form-label">SELECT FILE TO UPLOAD</label>
                                       <asp:FileUpload ID="FileUpload1" runat="server" width="80%" />
                                      
                                  </div>
                                  <div class="col-md-2">
                                       <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary w-50" Font-Bold="True" OnClick="Upload_Click" Text="UPLOAD"  />
                                  </div>
                                 
                              </div>



               
               
                    </asp:View>
                    </asp:MultiView>

        

    
    <br />
<%--    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <br />
</asp:Content>
