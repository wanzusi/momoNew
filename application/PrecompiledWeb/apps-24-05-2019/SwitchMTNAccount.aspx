<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="SwitchMTNAccount, App_Web_o7vnt00f" title="Switch MTN Account" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            SWITCH MTN ACCOUNTS
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
             
                <table style="width: 100%">
                            <tr>
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            SWITCH TO
                            </td> 
                            </tr>
                            <tr>         
                            <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboMtnAccountUsername" runat="server" CssClass="InterfaceDropdownList"
                                Width="45%" style="font: menu" OnDataBound="cboMtnAccountUsername_DataBound" OnSelectedIndexChanged="cboMtnAccountUsername_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                            
                            </tr> 
               </table> 
         </td>          
         </tr>
                  
       
        <tr>
            <td style="width: 98%; height: 2px">
             <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
            <script type="text/javascript" language="javascript">
     function Check_Click(objRef){
            //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;
              if(objRef.checked){
            //If checked change color to Aqua
                row.style.backgroundColor = "aqua";
                }
              else{   
        //If not checked change back to original color
                    if(row.rowIndex % 2 == 0){
                    //Alternating Row Color
                  row.style.backgroundColor = "#C2D69B"
                      }
                       else{
                  row.style.backgroundColor = "white";
                    }
                 } 
    //Get the reference of DataGrid
                var DataGrid = row.parentNode;
    //Get all input elements in DataGrid
                var inputList = DataGrid.getElementsByTagName("input");
                for (var i=0;i<inputList.length;i++){
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];
        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if(inputList[i].type == "checkbox" && inputList[i] != headerCheckBox){
            if(!inputList[i].checked)
                {
                checked = false;
                break;
           }
        }
    }
    headerCheckBox.checked = checked;
}
</script>
<script type = "text/javascript">

        function CheckAll(objRef){
        var DataGrid = objRef.parentNode.parentNode.parentNode;
        var inputList = DataGrid.getElementsByTagName("input");
        for (var i=0;i<inputList.length;i++){
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if(inputList[i].type == "checkbox"  && objRef != inputList[i]){
            if (objRef.checked){
                //If the header checkbox is checked
                //check all checkboxes
                //and highlight all rows
                row.style.backgroundColor = "aqua";
                inputList[i].checked=true;
            }
            else
            {
                //If the header checkbox is checked
                //uncheck all checkboxes
                //and change rowcolor back to original
                if(row.rowIndex % 2 == 0)
                {
                   //Alternating Row Color
                   row.style.backgroundColor = "#C2D69B";

                }
                else
                {
                   row.style.backgroundColor = "white";
                }
                
            inputList[i].checked=false;

            }
        }
    }
}
</script> 
<script type = "text/javascript">

    function ConfirmSwitch()
    {
       var count = document.getElementById("<%=hfCount.ClientID %>").value;

       var gv = document.getElementById("<%=DataGrid1.ClientID%>");

       var chk = gv.getElementsByTagName("input");

       for(var i=0;i<chk.length;i++)
       {
            if(chk[i].checked && chk[i].id.indexOf("chkAll") == -1)
            {
                count++;
            }
       }
       if(count == 0){
            alert("No records to Switch.");
            return false;
       }
       else{
            return confirm("Do you want to Switch " + count + " record(s).");
       }
    }
</script>  
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%" DataKeyField = "VendorCode" >
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <Columns>
                          <asp:TemplateColumn>
                          <HeaderTemplate>  
                          <input type="checkbox" id="checkAll"
                            onclick="CheckAll(this);" runat="server" name=""/>
                          </HeaderTemplate>
                          
                    <ItemTemplate>
                     <input type="checkbox" runat="server" id="SelectCheckBox"
                       onclick = "Check_Click(this)"  name="" />
                    </ItemTemplate>

            </asp:TemplateColumn>
                   <asp:BoundColumn DataField="VendorCode" HeaderText="VendorCode">  </asp:BoundColumn> 
                     <asp:BoundColumn DataField="UtilityUsername" HeaderText="UtilityUsername"></asp:BoundColumn>
                     <asp:BoundColumn DataField="UtilityPassword" HeaderText="UtilityPassword"></asp:BoundColumn>
                    
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid>
                </asp:View>
                                      
                </asp:MultiView>
                </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px" colspan = "5">
             <asp:HiddenField ID="hfCount" runat="server" Value = "0" />
                <asp:Button ID="btnSwitch" runat="server" Font-Size="9pt" Height="23px" Text="Switch"
                   OnClientClick = "return ConfirmSwitch();" Visible="false" 
                 Style="font: menu"  Width="125px" OnClick="btnSwitch_Click"/>
                  
                 </td>
        </tr>
    </table>
    <br /> 
    <br />   
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

