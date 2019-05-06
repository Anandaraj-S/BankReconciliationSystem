<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PostedDataDeletion.aspx.vb" Inherits="Bank_Reconciliation.PostedDataDeletion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        .style1
        {
            width: 32px;
        }
       
    </style>
    <script type = "text/javascript">
<!--
        function Check_Click(objRef) {
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
        function checkAll(objRef) {
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
         function ConfirmDelete() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             var drpClient = document.getElementById("<%=ddlPDDClient.ClientID %>");
             var Type = document.getElementById("<%=lblPDDCurrentType.ClientID %>").innerHTML;
             var Client = drpClient.options[drpClient.selectedIndex].innerHTML;
              var count = document.getElementById("<%=hfCount.ClientID %>").value;
             var gv = document.getElementById("<%=grdPostedDataDeletionLoad.ClientID%>");
             var chk = gv.getElementsByTagName("input");
             for (var i = 0; i < chk.length; i++) {
                 if (chk[i].checked && chk[i].id.indexOf("chkPDDSelectAll") == -1) {
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<asp:MultiView runat="server" ID="mvposteddatadeletion">
    
    <asp:View runat="server" ID="PostedDataDeletion">
    <div>
    <center>
        <asp:Label ID="Label9" runat="server" CssClass="viewHeading" 
            Text="Posted Data Deletion"></asp:Label>
        <br />
          <br />
        <table>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblPDDClient" runat="server" Text="Client" style="font-family:Times New Roman;"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPDDClient" runat="server" AutoPostBack="True" 
                        Width="238px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
         <div style="height:25px;width:1300px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblPostDataDeletionlist" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Times New Roman;font-size:10pt;font-weight:bold;width:1300px;color:black;
                     border-collapse:collapse;height:100%;">
                      <tr style="font-weight:bold">
                <td style="width:100px;height :25px; text-align:center" ></td> 
                <td style="width:300px; height :25px; text-align:center"><asp:RadioButton ID="rbPDDLockbox" runat="server" Text="LockBox" AutoPostBack="True" Checked="True"  /></td>
                    <td style="width:300px; height :25px; text-align:center"><asp:RadioButton ID="rbPDDEFT" runat="server" Text="EFT" AutoPostBack="True" /></td>
                    <td style="width:300px; height :25px; text-align:center"><asp:RadioButton ID="rbPDDCreditCard" runat="server" Text="Credit Card" AutoPostBack="True" /></td>
                    <td style="width:300px;  height :25px; text-align:center"><asp:RadioButton ID="rbPDDOther" runat="server" Text="Other" AutoPostBack="True" />
                    </td>   
                </tr>
            </table>
        </div><br /><br />
             <div id="postedData_Div" runat ="server" style="height:25px;width:1300px; position:relative; top:5px; margin:0;padding:0;" >
          <center>
            <table id="tblPDCHeader" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Times New Roman;font-size:11pt;font-weight:bold;width:1300px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                <td style="width:200px;">

                 <asp:CheckBox ID="chkPDDGridSelectAll" Text ="Select All Pages Records" runat="server" AutoPostBack="true" />
                </td>
                          <td style="width:100px" >                           
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
                          <td style="width:100px">
                               Posted Dt</td>
                </tr>
            </table>
            </center> 
        </div>
       
        
        
          <div style="height:30px;width:1300px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblPDDsearch" border ="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Times New Roman;font-size:10pt;font-weight:bold;width:1300px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                <td style="width:200px; height :30px";>
                 <asp:Label ID="lblType" runat="server" ForeColor ="DarkRed"  
                        Text ="Type : " Font-Bold="True" Font-Size="Medium" 
                        Font-Names="Times New Roman" ></asp:Label>
                <asp:Label ID="lblPDDCurrentType" runat="server" ForeColor ="DarkRed"  
                        Text =" " Font-Bold="True" Font-Size="Medium" 
                        Font-Names="Times New Roman" ></asp:Label>
               </td> 
                          <td style="width:100px; height :30px;"align="right" >
                               <asp:TextBox ID="txtPDDsearchBatchId" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:100px;height :30px;"align="right">
                                    <asp:TextBox ID="txtPDDsearchInvoice" runat="server" width="100%"
                                        AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:100px; height :30px;"align="right">                                   
                                     <asp:TextBox ID="txtPDDsearchCheck" runat="server" width="100%"
                                         AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDDsearchClosingDt" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;"align="right">
                               <asp:TextBox ID="txtPDDsearchDepositDate" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDDSearchCreateDate" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDDsearchDesignation" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                                <asp:TextBox ID="txtPDDSearchType" runat="server" width="100%"
                                    AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="TxtPDDsearchPayamt" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDDSearchPayCode" runat="server" width="100%"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px; height :30px;" align="right">
                               <asp:TextBox ID="txtPDDSearchPosteddt" runat="server" width="94%"
                                   AutoPostBack="True"></asp:TextBox></td> 
                </tr>
            </table>
        </div>
      
        <div class="WordWrap" runat="server" id="div_PDDLoad" style="overflow: auto; height:425px; width: 1300px; position:relative; top:15px; left:0px; margin:0;padding:0">
        <asp:GridView ID="grdPostedDataDeletionLoad" runat="server" ShowHeader = "true" 
                AllowPaging ="true"  PageSize="200"
                   AutoGenerateColumns="False"  Width="1300px" 
                onrowcancelingedit="PostedDataDeletionCancelEdit" onrowediting="PostedDataDeletionEdit" 
                        onrowupdating="PostedDataDeletionUpdate"     DataKeyNames="Number" 
                Font-Bold="False">
                        <rowstyle Height="20px" />
                         <HeaderStyle Height="50px" Font-Size="13px" BorderColor="#DCDCDC"
                    BorderStyle="Solid" BorderWidth="1px" />
            <Columns>
             <asp:CommandField ShowEditButton="True"  ItemStyle-Width ="100px" >
                 <ItemStyle Width="100px" />
                </asp:CommandField>
                <asp:TemplateField ItemStyle-Width="100px"  >
                        <HeaderTemplate >
                            <asp:CheckBox ID="chkPDDSelectAll" Text ="Select All Current Page" runat="server" AutoPostBack="true" style="font-family:Times New Roman;" onclick = "checkAll(this);"  
                                oncheckedchanged="chkPDDSelectAll_CheckedChanged"/>
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkPostDataDeletion" runat="server" onclick = "Check_Click(this)"  ></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
             
                    <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" Visible ="false"  >
                    <ItemTemplate>
                        <asp:Label ID="lblPDDPosted_DataID" runat="server" Text='<%# Eval("Number")%>'  ></asp:Label>
                    </ItemTemplate>              
                        <ItemStyle Width="100px" />
                </asp:TemplateField>  
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000"  >
                    <ItemTemplate>
                        <asp:Label ID="lblPDDBatchID" runat="server" Text='<%# Eval("Batch_Number")%>'></asp:Label>
                    </ItemTemplate>                         
                     <HeaderStyle ForeColor="#000000" />
                     <ItemStyle Width="100px" />
                </asp:TemplateField>    

                 <asp:TemplateField   HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                     <div style="word-wrap: break-word; width: 100px;">
                        <asp:Label ID="lblPDDInvoice" runat="server" Text='<%# Eval("Invoice_Number")%>'></asp:Label>
                        </div> 
                    </ItemTemplate>
                     <HeaderStyle ForeColor="#000000" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText = "" HeaderStyle-ForeColor="000000" >
                        <ItemTemplate>
                    <div style="word-wrap: break-word; width: 100px;">
                        <asp:Label ID="lblPDDCheck" runat="server" Text='<%# Eval("Check_Number")%>'></asp:Label>
                        </div> 
                    </ItemTemplate>
                    <EditItemTemplate>
                     <asp:TextBox ID="txtPDDCheck" runat="server" TextMode="MultiLine" Rows="4" width="100%" Text='<%# Eval("Check_Number")%>'></asp:TextBox>
                      </EditItemTemplate>
                        <HeaderStyle ForeColor="#000000" />
                      <ItemStyle Width="100px" />
                       </asp:TemplateField>     
                       <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDClosingDt" runat="server" width="100%" Text='<%# Eval("Closing_Date")%>'></asp:Label>
                    </ItemTemplate>
                   
                           <HeaderStyle ForeColor="#000000" />
                   
                           <ItemStyle Width="100px" />
                   
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDBankDeptDt" runat="server" Text='<%# Eval("Bank_Deposit_Date")%>'></asp:Label>
                    </ItemTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtPDDBankDeptDt" runat="server" width="100%" Text='<%# Eval("Bank_Deposit_Date")%>'></asp:TextBox>
                </EditItemTemplate>
                    <HeaderStyle ForeColor="#000000" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>  
                   
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDCreateDt" runat="server" width="100%" Text='<%# Eval("Creation_Date")%>'></asp:Label>
                    </ItemTemplate>
                    
                     <HeaderStyle ForeColor="#000000" />
                    
                     <ItemStyle Width="100px" />
                    
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                    </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID="txtPDDDesignation" runat="server" width="100%" Text='<%# Eval("Designation")%>'></asp:TextBox>                                  
                 </EditItemTemplate>
                    <HeaderStyle ForeColor="#000000" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField> 
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDType" runat="server" Text='<%# Eval("Data_Type")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate >
                    <asp:TextBox ID="txtPDDType" runat="server" width="100%" Text='<%# Eval("Data_Type")%>'></asp:TextBox>                                                                     
                    </EditItemTemplate>
                                   
                    <HeaderStyle ForeColor="#000000" />
                                   
                    <ItemStyle Width="100px" />
                                   
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDPayAmt" runat="server" Text='<%# Eval("Payment_Amount")%>'></asp:Label>
                    </ItemTemplate>
                 
                     <HeaderStyle ForeColor="#000000" />
                 
                <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDPayCode" runat="server" Text='<%# Eval("Pay_Code")%>'></asp:Label>
                    </ItemTemplate>
                 
                     <HeaderStyle ForeColor="#000000" />
                 
                     <ItemStyle Width="100px" />
                 
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "" HeaderStyle-ForeColor="000000">
                    <ItemTemplate>
                        <asp:Label ID="lblPDDPostedDt" runat="server" Text='<%# Eval("Posted_Date")%>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle ForeColor="#000000" />
                     <ItemStyle Width="100px" />
                </asp:TemplateField> 
             </Columns>
        </asp:GridView>
        </div>
        <br />
         <asp:LinkButton ID="lnkPDDdelete"  runat="server" OnClientClick = "ConfirmDelete();"  
              Font-Size="Medium"  >Delete</asp:LinkButton>
        <br />
         <br />
         <asp:LinkButton ID="lnkPDDReturn" runat="server">Return</asp:LinkButton>
         <asp:HiddenField ID="hfCount" runat="server" Value = "0" />
       
    </center>
    </div>
    </asp:View>
  
</asp:MultiView>
</div>
</asp:Content>
