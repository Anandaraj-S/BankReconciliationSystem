<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PostedDataCorrection.aspx.vb" Inherits="Bank_Reconciliation.PostedDataCorrection" Title ="Posted Data Correction" MasterPageFile="~/Site.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        .style1
        {
            width: 32px;
        }
       
    </style>
     <script type = "text/javascript">
<!--
         function PDC_Check_Click(objRef) {
             //Get the Row based on checkbox
             var row = objRef.parentNode.parentNode;

             //Get the reference of GridView
             var GridView = row.parentNode;

             //Get all input elements in Gridview
             var inputList = GridView.getElementsByTagName("input");

             for (var i = 0; i < inputList.length; i++) {
                 //The First element is the Header Checkbox
                 var headerCheckBox = inputList[0];

                 //Based on all or none checkboxes
                 //are checked check/uncheck Header Checkbox
                 var checked = true;
                 if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                     if (!inputList[i].checked) {
                         checked = false;
                         break;
                     }
                 }
             }
             headerCheckBox.checked = checked;

         }
         function PDC_checkAll(objRef) {
             var GridView = objRef.parentNode.parentNode.parentNode;
             var inputList = GridView.getElementsByTagName("input");
             for (var i = 0; i < inputList.length; i++) {
                 var row = inputList[i].parentNode.parentNode;
                 if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                     if (objRef.checked) {
                         inputList[i].checked = true;
                     }
                     else {
                         inputList[i].checked = false;
                     }
                 }
             }
         }
//-->
</script>
     <script type = "text/javascript">
         function PDC_ConfirmDelete() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "Confirm_Delete_value";
             var drpClient = document.getElementById("<%=ddlPDCClient.ClientID %>");
             var Type = document.getElementById("<%=lblPDCCurrentType.ClientID %>").innerHTML;
             var Client = drpClient.options[drpClient.selectedIndex].innerHTML;
             var count = document.getElementById("<%=hfPDCDeleteCount.ClientID %>").value;
             var gv = document.getElementById("<%=grdPostedDataCorrectionLoad.ClientID%>");
             var chk = gv.getElementsByTagName("input");
             for (var i = 0; i < chk.length; i++) {
                 if (chk[i].checked && chk[i].id.indexOf("chkPDCSelectAll") == -1) {
                     count++;
                 }
             }
             if (count == 0) {
                 alert("No records to delete.");
                 confirm_value.value = "No";
             }
             else {
                 if (confirm("Client : " + Client + '\n\n' + " Type : " + Type + '\n\n' + " Are you sure you want to delete " + count + " records ?")) {
                     confirm_value.value = "Yes";
                 } else {
                     confirm_value.value = "No";
                 }
             }
             document.forms[0].appendChild(confirm_value);
         }

         function PDC_ConfirmPush() {
             var confirm_Push_value = document.createElement("INPUT");
             confirm_Push_value.type = "hidden";
             confirm_Push_value.name = "Confirm_Push_value";
             var drpClient = document.getElementById("<%=ddlPDCClient.ClientID %>");
             var Type = document.getElementById("<%=lblPDCCurrentType.ClientID %>").innerHTML;
             var Client = drpClient.options[drpClient.selectedIndex].innerHTML;
             var count = document.getElementById("<%=hfPDCPushCount.ClientID %>").value;
             var gv = document.getElementById("<%=grdPostedDataCorrectionLoad.ClientID%>");
             var chk = gv.getElementsByTagName("input");
             for (var i = 0; i < chk.length; i++) {
                 if (chk[i].checked && chk[i].id.indexOf("chkPDCSelectAll") == -1) {
                     count++;
                 }
             }
             if (count == 0) {
                 alert("No records to push.");
                 confirm_Push_value.value = "No";
             }
             else {
                 if (confirm("Client : " + Client + '\n\n' + " Type : " + Type + '\n\n' + " Are you sure you want to push " + count + " records ?")) {
                     confirm_Push_value.value = "Yes";
                 } else {
                     confirm_Push_value.value = "No";
                 }
             }
             document.forms[0].appendChild(confirm_Push_value);
         }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<asp:MultiView runat="server" ID="mvposteddatacorrection">
    
    <asp:View runat="server" ID="PostedDataLoad">
    <div>
    <center>
        <asp:Label ID="Label9" runat="server" CssClass="viewHeading" 
            Text="Posted Data Correction"></asp:Label>
        <br />
          <br />
        <table>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblPDCClient" runat="server" Text="Client"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPDCClient" runat="server" AutoPostBack="True" 
                        Width="238px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
         <div style="height:25px;width:1450px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblPostDataCorrectionlist" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:1450px;color:black;
                     border-collapse:collapse;height:100%;">
                      <tr style="font-weight:bold">
                <td style="width:100px;height :25px; text-align:center" ></td> 
                <td style="width:300px; height :25px; text-align:center"><asp:RadioButton ID="rbPDCLockbox" runat="server" Text="LockBox" AutoPostBack="True" Checked="True"  /></td>
                    <td style="width:300px; height :25px; text-align:center"><asp:RadioButton ID="rbPDCEFT" runat="server" Text="EFT" AutoPostBack="True" /></td>
                    <td style="width:300px; height :25px; text-align:center"><asp:RadioButton ID="rbPDCCreditCard" runat="server" Text="Credit Card" AutoPostBack="True" /></td>
                    <td style="width:300px;  height :25px; text-align:center"><asp:RadioButton ID="rbPDCOther" runat="server" Text="Other" AutoPostBack="True" />
                    </td>   
                </tr>
            </table>
        </div><br /><br />
             <div id="postedData_Div" runat ="server" style="height:20px;width:1450px; position:relative; top:5px; margin:0;padding:0;" >
          <center>
            <table id="tblPDCHeader" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:1450px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                <td style="width:200px;">
                  <asp:CheckBox ID="chkPDCGridSelectAll" Text ="Select All Pages Records" runat="server" AutoPostBack="true" />
                </td>
               
                           <td style="width:100px">                           
                               BatchId</td>
                           <td style="width:100px">
                               Invoice</td>
                           <td style="width:100px">
                               Check #</td>
                           <td style="width:100px">
                               Closing Dt</td>
                          <td style="width:100px">
                               Deposit Dt</td>
                          <td style="width:100px">
                               Create Dt</td>
                          <td style="width:100px">
                               Designation</td>
                          <td style="width:100px">
                               Type</td>
                          <td style="width:100px">
                               Pay Amt</td>
                          <td style="width:100px">
                               Pay Code</td>
                           <td style="width:150px">
                               Comments</td>
                         <td style="width:100px">
                               Posted Dt</td>
                </tr>
            </table>
            </center> 
        </div>
       
        
        
          <div style="height:30px;width:1450px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblPDDsearch" border ="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:1450px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                <td style="width:200px; height :30px";>
                 <asp:Label ID="lblPDCType" runat="server" ForeColor ="DarkRed"  
                        Text ="Type : " Font-Bold="True" Font-Size="Medium" 
                        Font-Names="Times New Roman" ></asp:Label>
                <asp:Label ID="lblPDCCurrentType" runat="server" ForeColor ="DarkRed"  
                        Text =" " Font-Bold="True" Font-Size="Medium" 
                        Font-Names="Times New Roman" ></asp:Label>
               </td> 
             
                          <td style="width:100px; height :30px;"align="right" >
                               <asp:TextBox ID="txtPDCsearchBatchId" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:100px;height :30px;"align="right">
                                    <asp:TextBox ID="txtPDCsearchInvoice" runat="server" width="100%"
                                        AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:100px; height :30px;"align="right">                                   
                                     <asp:TextBox ID="txtPDCsearchCheck" runat="server" width="100%"
                                         AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDCsearchClosingDt" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;"align="right">
                               <asp:TextBox ID="txtPDCsearchDepositDate" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDCSearchCreateDate" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDCsearchDesignation" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                                <asp:TextBox ID="txtPDCSearchType" runat="server" width="100%"
                                    AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="TxtPDCsearchPayamt" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDCSearchPayCode" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:150px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDCSearchcomment" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td> 
                                      <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDCSearchPosteddt" runat="server" width="94%"
                                   AutoPostBack="True"></asp:TextBox></td> 
                </tr>
            </table>
        </div>
      
        <div class="WordWrap" runat="server" id="div_PDCLoad" style="overflow: auto; height:425px; width: 1450px; position:relative; top:15px; left:0px; margin:0;padding:0">
        <asp:GridView ID="grdPostedDataCorrectionLoad" runat="server" ShowHeader = "true" AllowPaging ="true"  PageSize="200"
                   AutoGenerateColumns="False"  Width="1450px" onrowcancelingedit="PostedDataCorrectionCancelEdit" onrowediting="PostedDataCorrectionEdit" 
                        onrowupdating="PostedDataCorrectionUpdate"     DataKeyNames="Number">
                        <rowstyle Height="20px" />
                         <HeaderStyle Height="40px" Font-Size="15px" BorderColor="#DCDCDC"
                    BorderStyle="Solid" BorderWidth="1px" />
            <Columns>
             <asp:CommandField ShowEditButton="True"  ItemStyle-Width ="100px" >
                 <ItemStyle Width="100px" />
                </asp:CommandField>
                <asp:TemplateField ItemStyle-Width="100px"  >
                        <HeaderTemplate >
                            <asp:CheckBox ID="chkPDCSelectAll" Text ="Select All" runat="server" AutoPostBack="true"  style="font-family:Times New Roman;" onclick = "PDC_checkAll(this);"   
                                oncheckedchanged="chkPDCSelectAll_CheckedChanged"/>
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkPostDataRecords" runat="server" onclick = "PDC_Check_Click(this)" ></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
             
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" Visible ="false"  >
                    <ItemTemplate>
                        <asp:Label ID="lblPDCStaging_Number" runat="server" Text='<%# Eval("Number")%>'  ></asp:Label>
                    </ItemTemplate>              
                        <ItemStyle Width="100px" />
                </asp:TemplateField>  
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000"  >
                    <ItemTemplate>
                        <asp:Label ID="lblPDCBatchID" runat="server" Text='<%# Eval("Batch_Number")%>'></asp:Label>
                    </ItemTemplate>                         
                       <ItemStyle Width="100px" />
                </asp:TemplateField>    

                 <asp:TemplateField   HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                     <div style="word-wrap: break-word; width: 100px;">
                        <asp:Label ID="lblPDCInvoice" runat="server" Text='<%# Eval("Invoice_Number")%>'></asp:Label>
                        </div> 
                    </ItemTemplate>
                    </asp:TemplateField>  
                <asp:TemplateField HeaderText = "" HeaderStyle-ForeColor="000000" >
                        <ItemTemplate>
                    <div style="word-wrap: break-word; width: 100px;">
                        <asp:Label ID="lblPDCCheck" runat="server" Text='<%# Eval("Check_Number")%>'></asp:Label>
                        </div> 
                    </ItemTemplate>
                    <EditItemTemplate>
                     <asp:TextBox ID="txtPDCCheck" runat="server" TextMode="MultiLine" Rows="4" width="100%" Text='<%# Eval("Check_Number")%>'></asp:TextBox>
                      </EditItemTemplate>
                        <ItemStyle Width="100px" />
                       </asp:TemplateField>     
                       <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCClosingDt" runat="server" width="100%" Text='<%# Eval("Closing_Date")%>'></asp:Label>
                    </ItemTemplate>
                      <ItemStyle Width="100px" />
                   
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCBankDeptDt" runat="server" Text='<%# Eval("Bank_Deposit_Date")%>'></asp:Label>
                    </ItemTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtPDCBankDeptDt" runat="server" width="100%" Text='<%# Eval("Bank_Deposit_Date")%>'></asp:TextBox>
                </EditItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>  
                   
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCCreateDt" runat="server" width="100%" Text='<%# Eval("Creation_Date")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                    
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                    </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID="txtPDCDesignation" runat="server" width="100%" Text='<%# Eval("Designation")%>'></asp:TextBox>                                  
                 </EditItemTemplate>
                     <ItemStyle Width="100px" />
                </asp:TemplateField> 
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCType" runat="server" Text='<%# Eval("Data_Type")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate >
                    <asp:TextBox ID="txtPDCType" runat="server" width="100%" Text='<%# Eval("Data_Type")%>'></asp:TextBox>                                                                     
                    </EditItemTemplate>
                     <ItemStyle Width="100px" />
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCPayAmt" runat="server" Text='<%# Eval("Payment_Amount")%>'></asp:Label>
                    </ItemTemplate>
                 <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCPayCode" runat="server" Text='<%# Eval("Pay_Code")%>'></asp:Label>
                    </ItemTemplate>
                   <ItemStyle Width="100px" />
                 
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "150px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCComments" runat="server" Text='<%# Eval("Comment")%>'></asp:Label>
                    </ItemTemplate>
                     <ItemStyle Width="150px" />
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDCPostedDt" runat="server" Text='<%# Eval("Posted_Date")%>'></asp:Label>
                    </ItemTemplate>
                     <ItemStyle Width="100px" />
                </asp:TemplateField> 
             </Columns>
        </asp:GridView>
        </div>
        <br />
        <br />
        <br />
        <asp:LinkButton ID="lnkPDCPush"  runat="server"  
            OnClientClick = "PDC_ConfirmPush();"  Font-Size="Medium">Push</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:LinkButton ID="lnkPDCdelete"  runat="server" 
            OnClientClick = "PDC_ConfirmDelete();" Font-Size="Medium" >Delete</asp:LinkButton>
        <br />
        <br />
             <asp:LinkButton ID="lnkPDCReturn" runat="server">Return</asp:LinkButton>
               <asp:HiddenField ID="hfPDCDeleteCount" runat="server" Value = "0" />
            <asp:HiddenField ID="hfPDCPushCount" runat="server" Value = "0" />
    </center>
    </div>
    </asp:View>
  
</asp:MultiView>
</div>
</asp:Content>
