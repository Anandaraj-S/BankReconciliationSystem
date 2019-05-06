<%@ Page Title="Bank Reconciliation" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Work Queue.aspx.vb" Inherits="Bank_Reconciliation.Work_Queue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">



        .style1
        {
            width: 723px;
            height: 91px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LinkButton ID="lnkWQQueueTotals" runat="server">Queue Totals</asp:LinkButton>
&nbsp;&nbsp;
    <asp:LinkButton ID="lnkWQSelectedQueue" runat="server">[name of selected queue]</asp:LinkButton>
   <br />
    <p>
        <asp:Label ID="lblWQClient" runat="server" Text="Label" CssClass="fieldLabels"></asp:Label>
        <b>&nbsp;<asp:Label ID="Label5" runat="server" CssClass="fieldLabels" Text="-"></asp:Label>
&nbsp;<asp:Label ID="lblWQSelectedQueue" runat="server" CssClass="fieldLabels" 
            Text="Label"></asp:Label>
&nbsp;<asp:Label ID="Label6" runat="server" CssClass="fieldLabels" Text="-"></asp:Label>
&nbsp;</b><asp:Label ID="lblSelectedDate" runat="server" CssClass="fieldLabels" 
            Text="Label"></asp:Label>
    </p>
    <center>
    <table>
    <tr>
    <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="Bank File   " Font-Bold="True" 
                    Font-Size="Medium"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    <div runat="server" id="Div4"   class="WordWrap" 
                    style="overflow: auto; height: 130px; width:640px; color:Black   ">                        
                <asp:GridView ID="grdBank" runat="server" AutoGenerateColumns="False" Width="640px"    onrowcancelingedit="BankdataCancelEdit" onrowediting="BankDataEdit" 
                            onrowupdating="BankDataUpdate" >
                    <Columns>            
                    <asp:CommandField ShowEditButton="True" ItemStyle-Width ="100px" />
                    <asp:TemplateField HeaderText="Bank Deposit Date" ItemStyle-Width ="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositdate" runat="server" Text='<%# Eval("Deposit_Date")%>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>                   
                   
                             <asp:TemplateField HeaderText="Bank Deposit Amt" ItemStyle-Width ="120px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>' ></asp:Label>
                                </ItemTemplate>                                                          
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Bank Check #" ItemStyle-Width ="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblBankCheckNumber" runat="server" Text='<%# Eval("Check_Number")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBankCheckNumber"  runat="server" TextMode="MultiLine" Rows="2"  Width="100px" Text='<%# Eval("Check_Number")%>'></asp:TextBox>
                                </EditItemTemplate>                               
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Variance" ItemStyle-Width ="120px">
                                <ItemTemplate>
                                    <asp:Label ID="lblVariance" runat="server" Text='<%# Eval("Variance")%>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Payer" ItemStyle-Width ="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayer" runat="server" Text='<%# Eval("PayerName")%>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                                       
                    </Columns>
                </asp:GridView>
                </div> 
                <asp:Label ID="lblBankRecordCount" runat="Server" ForeColor ="Blue" ></asp:Label>
                <br />
                 No of days
                      <asp:TextBox ID="txtHolddaysno" runat="server" 
                    ToolTip ="Enter Number only." Width="69px" TextMode="Number"></asp:TextBox>
                &nbsp;Comments&nbsp;
                      <asp:TextBox ID="TxtHoldComments" runat="server" 
                    ToolTip ="Enter the comments." Width="149px"></asp:TextBox>
                &nbsp;&nbsp;
                 <asp:LinkButton ID="Lnkhold" runat="server">Defer</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <br />
    </td>
    <td style="width:100">
    </td>
    <td>
        <asp:Label ID="Label2" runat="server" Text="Posted Data Header" Font-Bold="True" 
            Font-Size="Medium"></asp:Label>
            <div runat="server" id="Div3" style="overflow: auto; height: 130px; width: 400px; color:Black   ">                        
        <asp:GridView ID="grdPostedHeader" runat="server" AutoGenerateColumns="False" 
                    Width="400px">
            <Columns>                         
                <asp:BoundField HeaderText="Deposit Date" DataField="Bank_Deposit_Date" ItemStyle-Width="100px"  />
                <asp:BoundField HeaderText="Deposit Amount" DataField="Amount" ItemStyle-Width="150px" >
                </asp:BoundField>
                <asp:BoundField HeaderText="Check #" DataField="Check_Number" ItemStyle-Width="150px" />
            </Columns>
        </asp:GridView>
        </div> 
        <asp:Label ID="lblPostDateCount" runat="Server" ForeColor ="Blue" ></asp:Label>
                <br />
                <br />
    </td>
    </tr>
    <tr>
    <td>
        <asp:LinkButton ID="lnkNewBank" runat="server">Create New Bank Record</asp:LinkButton>
    </td>
    <td style="width:100">
    </td>
    <td>
        <asp:LinkButton ID="lnkNewPosted" runat="server" Visible="False">Create New Posted Record</asp:LinkButton>
    </td>
    </tr>
    </table>
     <br />
      <br />
       <table class="style1"  style="width:1400px;">
        <tr>
         <td style="width:475px;"> </td> 
         <td style="width:150px;"><asp:Label ID="lblPartiallymatched" runat="server" Text="Partially Matched" Font-Bold="True" 
            Font-Size="Medium"></asp:Label></td> 
         <td style="width:150px;"> <asp:Button ID="btnMatchClear" runat="server" Width ="100px"  Text="Clear Search" /></td> 
         <td style="width:150px;"></td>
         <td style="width:475px;"></td> 
         </tr>
         </table> 
       
        <br />
        <br />
          <div id="Mat_Sea_Div" runat ="server" style="height:26px;width:1400px; position:relative; top:5px; margin:0;padding:0;" >
         
                   <table id="tblWQMatHeader"  border="1" cellpadding="0" cellspacing="0" rules="all" style="font-size:11pt;font-weight:bold;width:1400px; color :Black;
                     border-collapse:collapse;height:100%;">
                   
                       <tr style="font-weight:bold">
                            <td style="width:200px"></td>
                        
                           <td style="width:100px" id="MatNum" runat="Server" >                           
                               Number</td>
                           <td style="width:100px">                           
                               BatchId</td>
                           <td style="width:100px">
                               Invoice</td>
                           <td style="width:100px">
                               Check #</td>
                          <td style="width:100px">
                               Deposit Dt</td>
                          <td style="width:100px">
                               Closing Dt</td>
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
           
        </div>
          <div style="overflow: auto; height:30px;width:1400px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblmatchsearch" border ="1" cellpadding="0" cellspacing="0" rules="all" style="font-size:10pt;font-weight:bold;width:1400px;
                     border-collapse:collapse;height:100%;">
                      
                       <tr style="font-weight:bold">
                       
                       
                           <td style="width:200px">
                             <%--  <asp:CheckBox  ID="mckh" runat="server" Visible="false"></asp:CheckBox>--%>
                               </td>
                               <td style="width:110px">
                              <asp:TextBox ID="txtmatsearchNumber" runat="server" Width="96px" Visible="False"
                                   AutoPostBack="True"></asp:TextBox></td>
                            <td style="width:110px">
                               <asp:TextBox ID="txtmatsearchBatchId" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:110px">
                                    <asp:TextBox ID="txtmatsearchInvoice" runat="server" Width="96px" 
                                        AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:110px">                                   
                                     <asp:TextBox ID="txtMatsearchCheck" runat="server" Width="96px" 
                                         AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="txtMatsearchDepositDate" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="txtMatsearchClosingDt" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="txtMatSearchCreateDt" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="txtMatsearchDesignation" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                                <asp:TextBox ID="txtMatSearchType" runat="server" Width="96px" 
                                    AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtMatsearchPayamt" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="txtMatSearchPayCode" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="txtMatSearchPosteddt" runat="server" Width="96px" 
                                   AutoPostBack="True"></asp:TextBox></td>                         
                    </tr>
            </table>
        </div>
               <br /><br />
        <div  class="WordWrap" runat="server" id="Mat_Div" 
            style="overflow: auto; height: 300px; width: 1400px;  ">                        
        <asp:GridView ID="grdMatched" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="number" AllowSorting="True" ShowHeader ="False" AllowPaging ="false" 
                Height="116px" Width="1400px"   OnPageIndexChanging="MatchedOnPaging" 
                        onrowcancelingedit="MatchedCancelEdit" onrowediting="MatchedEdit" 
                        onrowupdating="MatchedUpdate">
            <Columns>        
               <asp:CommandField ShowEditButton="True"  ItemStyle-Width ="100px" >
                 </asp:CommandField>
                <asp:TemplateField ItemStyle-Width="100px" >
                    <ItemTemplate >
                        <asp:CheckBox ID="chkmatched" runat ="Server" Checked="True" ></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Number" Visible ="False">
                    <ItemTemplate>
                        <asp:Label ID="lblMatNumber" runat="server" Text='<%# Eval("Number")%>'  ></asp:Label>
                    </ItemTemplate>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatNumber" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>                
                </asp:TemplateField>    
                  <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "BatchID">
                    <ItemTemplate>
                        <asp:Label ID="lblMatBatchID" runat="server" Text='<%# Eval("BatchID")%>'></asp:Label>
                    </ItemTemplate>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatBatchID" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate> --%>               
                </asp:TemplateField>    

                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Invoice">
                    <ItemTemplate>
                        <asp:Label ID="lblMatInvoice" runat="server" Text='<%# Eval("Invoice")%>'></asp:Label>
                    </ItemTemplate>
                  <%--  <FooterTemplate>
                        <asp:TextBox ID="txtMatInvoice" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Check #">
                    <ItemTemplate>
                        <asp:Label ID="lblMatCheck" runat="server" Text='<%# Eval("Check")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtMatCheck" runat="server" TextMode="MultiLine" Rows="3"  Width="96px" Text='<%# Eval("Check")%>'></asp:TextBox>
                    </EditItemTemplate>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatCheck" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
               <%-- <ItemStyle Width="100px"></ItemStyle>--%>
                </asp:TemplateField>     
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Bank Dept Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblMatBankDeptDt" runat="server" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:Label>
                    </ItemTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtMatBankDeptDt" runat="server"  Width="96px" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:TextBox>
                </EditItemTemplate>
                </asp:TemplateField>  
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Closing Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblMatClosingDt" runat="server" Text='<%# Eval("Closing_Dt")%>'></asp:Label>
                    </ItemTemplate>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatClosingDt" Width = "100px" runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
                </asp:TemplateField>  
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Create Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblMatCreateDt" runat="server" Text='<%# Eval("Create_Dt")%>'></asp:Label>
                    </ItemTemplate>
                    <%--<FooterTemplate>
                        <asp:TextBox ID="txtMatCreateDt" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Designation">
                    <ItemTemplate>
                        <asp:Label ID="lblMatDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                    </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID="txtMatDesignation" runat="server"  Width="96px" Text='<%# Eval("Designation")%>'></asp:TextBox>                                  
                 </EditItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Type">
                    <ItemTemplate>
                        <asp:Label ID="lblMatType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate >
                    <asp:TextBox ID="txtMatType" runat="server"  Width="96px" Text='<%# Eval("Type")%>'></asp:TextBox>                                                                     
                    </EditItemTemplate>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatType" Width = "100px" runat="server"></asp:TextBox>
                    </FooterTemplate>--%>                
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Pay Amt">
                    <ItemTemplate>
                        <asp:Label ID="lblMatPayAmt" runat="server" Text='<%# Eval("Pay_Amt")%>'></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                    <asp:TextBox ID="txtMatPayAmt" runat ="Server" Text='<%# Eval("Pay_Amt")%>'></asp:TextBox>
                    </EditItemTemplate>--%>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatPayAmt" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
                <%--<ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>--%>
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Pay Code">
                    <ItemTemplate>
                        <asp:Label ID="lblMatPayCode" runat="server" Text='<%# Eval("Pay_Code")%>'></asp:Label>
                    </ItemTemplate>
                  <%--  <FooterTemplate>
                        <asp:TextBox ID="txtMatPayCode" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
                </asp:TemplateField> 
                 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Posted Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblMatPostedDt" runat="server" Text='<%# Eval("Posted_Dt")%>'></asp:Label>
                    </ItemTemplate>
                   <%-- <FooterTemplate>
                        <asp:TextBox ID="txtMatPostedDt" Width = "100px"  runat="server"></asp:TextBox>
                    </FooterTemplate>--%>
               <%-- <ItemStyle Width="100px"></ItemStyle>--%>
                </asp:TemplateField> 
                          
              <%--  <asp:CommandField  ShowEditButton="false" />--%>
            </Columns>
        </asp:GridView>
       
        <br />
        </div> 
        <asp:Label ID="lblMatchedCount" runat="Server"  ForeColor ="Blue" ></asp:Label>
        <br />
        <br />
        <br />
       
       
        <br />
     
              <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <table class="style1"  style="width:1400px;">
        <tr>
         <td style="width:475px;"></td> 
                <td style="width:150px;"><asp:Label ID="lblUnmatched" runat="server" Text="Unmatched" Font-Bold="True" 
            Font-Size="Medium"></asp:Label></td> 
                <td style="width:150px;"> 
                 <asp:Button ID="btnUnmatchClear" runat="server" Width ="100px"  Text="Clear Search" />
        </td> 
                <td style="width:150px;"> 
                <asp:LinkButton ID="lnkRefresh" runat="server" Visible="False">Refresh Table</asp:LinkButton></td> 
                  <td style="width:475px;"></td> 
        </tr>
            <tr>
            <td style="width:475px;"></td> 
                <td style="width:150px;">
                    <br />
                    <asp:Label ID="lblCheck" runat="Server">Check             </asp:Label>
                    &nbsp;
                    <asp:TextBox ID="TxtCheck" runat="server" Width="130px"></asp:TextBox>
                </td>
                <td style="width:150px;">
                    <br />
                    <asp:Label ID="lblDepositDate" runat="server" Text="Deposite Date  "></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="TxtDepositDate" runat="server" ValidationGroup="Deposite" 
                        Width="130px"></asp:TextBox>
                    <br />
                </td>
                <td style="width:150px;">
                    <br />
                    <asp:Label ID="lblType" runat="Server" ForeColor="Black">Type </asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TxtType" runat="server" Width="130px"></asp:TextBox>
                </td>
                <td style="width:475px;"></td> 
            </tr>
            <tr>
                <td style="width:475px;">
                </td>
                <td style="width:150px;">
                   
                </td>
                <td style="width:150px;" align="center" >
                       <asp:RegularExpressionValidator ID="RegDepositeDate" runat="server" 
                        ControlToValidate="TxtDepositDate" ErrorMessage="Invalid Deposite Date" 
                        ForeColor="Red" SetFocusOnError="True" 
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" 
                        ValidationGroup="Deposite" Width="100px"></asp:RegularExpressionValidator>
                    <br />
                    <asp:Label ID="lblSaveStatus" runat="server"></asp:Label>
                    <br />
                    <br />
                   <asp:Button ID="btnUpdateInfo" runat="server" Width ="100px" Text="Update Info"/>
                    </td>
                <td style="width:150px;" ></td>
                <td style="width:475px;" ></td>
            </tr>
        </table>
        <br />
        <br />

        <%--unmatched--%>

         <div id="Unmat_Sea_Div" runat ="server" style="height:26px; width:1450px; position:relative; top:0px; margin:0;padding:0; left: 0px;">
       
                   <table id="tblWQHeader" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-size:11pt;font-weight:bold;width:1450px; color :Black; 
                     border-collapse:collapse;height:100%;">
                       <tr style="font-weight:bold">
                       <td style="width:100px"></td>
                       <td style="width:100px"><asp:CheckBox ID="chkWQSelectAll" Text ="Select All" runat="server" AutoPostBack="true" /></td>
                           <td style="width:100px" id="UnMatNum" runat ="Server" visible ="False" >
                               Number</td>      
                            <td style="width:100px">
                                    BatchID</td>
                           <td style="width:100px">
                                    Invoice</td>
                           <td style="width:100px">                                   
                                     Check #</td>
                          <td style="width:100px">
                               Deposit Dt</td>
                          <td style="width:100px">
                               Closing Dt</td>
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
                               <td style="width:50px"></td>                      
                       </tr>
            </table>
           
        </div>

         <div style="overflow: auto; height:30px;width:1450px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblWQUnmatched" border ="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Times New Roman;font-size:10pt;font-weight:bold;width:1450px;color:black;
                     border-collapse:collapse;height:100%;">
                      
                       <tr style="font-weight:bold">
                           <td style="width:200px">
                               <%--<asp:CheckBox  ID="ckh" runat="server" Visible="False"></asp:CheckBox>--%>
                             
                               </td>
                           <td style="width:110px">
                              <asp:TextBox ID="TxtSeaUnmatNum" runat="server" width="96px" Visible="False"
                                   AutoPostBack="True"></asp:TextBox></td>
                            <td style="width:110px">
                               <asp:TextBox ID="TxtSeaBatchID" runat="server" width="96px"
                                   AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:110px">
                                    <asp:TextBox ID="TxtSeaUnMatInv" runat="server" width="96px"
                                        AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:110px">                                   
                                     <asp:TextBox ID="TxtSeaUnmatChk" runat="server" width="96px"
                                         AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatBDD" runat="server" width="96px"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatCD" runat="server" width="96px" AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatCDt" runat="server" width="96px"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatDes" runat="server" width="96px"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                                <asp:TextBox ID="TxtSeaUnMatType" runat="server" width="96px" 
                                    AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatPayAmt" runat="server" width="96px"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatPayCode" runat="server" width="96px"
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtSeaUnMatPD" runat="server" width="96px" AutoPostBack="True"></asp:TextBox></td>         
                                 <td style="width:50px">
                               </td>                
                     </tr>
            </table>
        </div>
               <br />
               <br />
        <div class="WordWrap" runat="server" id="Unmat_Div" 
            style="overflow: auto; height: 641px; width: 1450px;">                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdUnmatched" runat="server" AllowSorting="True" AllowPaging ="false"
                        AutoGenerateColumns="False" Height="170px" PageSize = "60"
                        OnPageIndexChanging="UnMatchedOnPaging" 
                        onrowcancelingedit="UnMatchedCancelEdit" onrowediting="UnMatchedEdit"  
                        onrowupdating="UnMatchedUpdate" ShowFooter="True" ShowHeader="False" DataKeyNames ="Number"
                        Width="1450px">
                        <Columns>
                            <asp:CommandField ShowEditButton="True"  ItemStyle-Width ="100px" >
                 <ItemStyle Width="100px" />
                </asp:CommandField>
                            <asp:TemplateField ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkUnmatched" runat="Server" Checked="False" />
                                </ItemTemplate> 
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Number" ItemStyle-Width="100px"   Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchNumber" runat="server" Text='<%# Eval("Number")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BatchID" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchBatchID" runat="server" Text='<%# Eval("BatchID")%>'></asp:Label>
                                </ItemTemplate>                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchBatchID" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchInvoice" runat="server" Text='<%# Eval("Invoice")%>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchInvoice" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check #" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchCheck" runat="server" Text='<%# Eval("Check")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUnMatchCheck" runat="server" TextMode="MultiLine" Rows="3"  Width="96px" Text='<%# Eval("Check")%>' ></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchCheck" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Dept Dt" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchBankDeptDt" runat="server" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUnMatchBankDeptDt" runat="server" Width="96px" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchBankDeptDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Dt" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchClosingDt" runat="server" Text='<%# Eval("Closing_Dt")%>'></asp:Label>
                                </ItemTemplate>                              
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchClosingDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Create Dt" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchCreateDt" runat="server" Text='<%# Eval("Create_Dt")%>'></asp:Label>
                                </ItemTemplate>                             
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchCreateDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" ItemStyle-Width="100px" >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtUnMatchDesignation" runat="server" Width="96px" Text='<%# Eval("Designation")%>'></asp:TextBox>                                  
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="dpUnMatchDesignation" runat="server" Width="100px">
                                        <asp:ListItem Text="ERA" Value="ERA" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="non-ERA" Value="non-ERA"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtUnMatchDesignation" runat="server" Width="100px"></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                </ItemTemplate>                                
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtUnMatchType" runat="server" Width="96px" Text='<%# Eval("Type")%>'></asp:TextBox>                                                                     
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <%--<asp:TextBox ID="txtUnMatchType" runat="server" Width="100px"></asp:TextBox>--%>
                                     <asp:DropDownList ID="dpUnMatchType" runat="server" Width="100px" >
                                        <asp:ListItem Text="CHECK" Value="CHECK" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="CREDIT CARD" Value="CREDIT CARD" ></asp:ListItem>
                                        <asp:ListItem Text="EFT" Value="EFT"></asp:ListItem>                                        
                                        <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>                                                                               
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay Amt" ItemStyle-Width="100px"  >
                                 <ItemTemplate>
                                    <asp:Label ID="lblUnmatchPayAmt" runat="server" Text='<%# Eval("Pay_Amt")%>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate >
                                    <asp:TextBox ID="TxtUnMatchPayAmt" runat ="Server" Text='<%# Eval("Pay_Amt")%>'></asp:TextBox>
                                </EditItemTemplate>--%>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchPayAmt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay Code" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchPayCode" runat="server" Text='<%# Eval("Pay_Code")%>'></asp:Label>
                                </ItemTemplate>                             
                                <FooterTemplate>
                                    <asp:DropDownList ID="dpUnMatchPayCode" runat="server" Width="100px">
                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Client Deposit" Value="Z01" ></asp:ListItem>
                                        <asp:ListItem Text="TOS Payment" Value="Z02"></asp:ListItem>    
                                        <asp:ListItem Text="Client Transfer" Value="Z03"></asp:ListItem>    
                                        <asp:ListItem Text="Posting Error To Be Corrected" Value="Z04"></asp:ListItem>    
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtUnMatchPayCode" runat="server" Width="100px"></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Posted Dt" ItemStyle-Width="100px"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblUnmatchPostedDt" runat="server" Text='<%# Eval("Posted_Dt")%>'></asp:Label>
                                </ItemTemplate>                             
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnMatchPostedDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="50px"  > 
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" runat="server" OnClick="AddUnMatched" Text="Add" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID = "grdUnmatched" />
                  </Triggers>
            </asp:UpdatePanel>
        </div> 
        <asp:Label ID="lblUnmatchedcount" runat="Server"  ForeColor ="Blue" ></asp:Label>        
        <br />
        <br />
        <asp:LinkButton ID="lnkUpdate" runat="server">Match</asp:LinkButton>
    </center>
</asp:Content>

