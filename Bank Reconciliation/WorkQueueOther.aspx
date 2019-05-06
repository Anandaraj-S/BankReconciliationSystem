<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WorkQueueOther.aspx.vb" Inherits="Bank_Reconciliation.WorkQueueOther" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                &nbsp;</td>
    <td style="width:20">
    </td>
    <td>
                <br />
                <br />
    </td>
    </tr>
    <tr>
    <td>
        <asp:LinkButton ID="lnkNewBank" runat="server">Create New Bank Record</asp:LinkButton>
    </td>
    <td style="width:20">
    </td>
    <td>
        &nbsp;</td>
    </tr>
    </table>
        <asp:Label ID="lblPartiallymatched" runat="server" Text="Bank Data" Font-Bold="True" 
            Font-Size="Medium"></asp:Label>
        <br />
          <div id="Mat_Sea_Div" runat ="server" style="height:20px;width:1200px; position:relative; top:5px; margin:0;padding:0;" >
          <center>
                   <table id="table2">
                   
                       <tr style="font-weight:bold">
                            <td></td>
                          
                           <td style="width:120px" id="MatNum" runat="Server" >                           
                               Number</td>
                          
                           <td style="width:100px">
                               Check #</td>
                          <td style="width:100px">
                               Deposit Date</td>
                          
                          <td style="width:100px">
                               Type</td>
                          
                          <td style="width:100px">
                               Amount</td>
                       </tr>
                        <tr style="font-weight:bold">
                       
                           <td>
                               <asp:CheckBox  ID="mckh" runat="server" Visible="false"></asp:CheckBox></td>                      
                          
                                   <td style="width:100px">                                        
                                <asp:TextBox ID="txtBankDataSearchCheck" runat="server" Width="85px" 
                                         AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px">
                               <asp:TextBox ID="txtBankDataSearchDepositDate" runat="server" Width="117px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          
                          <td style="width:110px">
                                <asp:TextBox ID="txtBankDataSearchType" runat="server" Width="140px" 
                                    AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtBankDataSearchPayamt" runat="server" Width="137px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                                                  
                       </tr>
                   </table>
                   </center>
               </div>
               <br /><br />
        <div runat="server" id="Mat_Div" 
            style="overflow: auto; height: 256px; width: 1200px;  ">                        
        <asp:GridView ID="grdBankData" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="number" AllowSorting="True" ShowHeader ="False" 
                AllowPaging ="True" PageSize = "10"
                Height="16px" Width="669px"    OnPageIndexChanging="BankDataOnPaging" 
                        onrowcancelingedit="BankDataCancelEdit" onrowediting="BankDataEdit" 
                        onrowupdating="BankDataUpdate" >
            <Columns>        
               <asp:CommandField ShowEditButton="false"  />
                <asp:TemplateField >
                    <ItemTemplate >
                        <asp:CheckBox ID="chkBankData" runat ="Server" Checked="True" ></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "BankID" Visible ="False">
                    <ItemTemplate>
                        <asp:Label ID="lblBankDataNumber" runat="server" Text='<%# Eval("Number")%>'  ></asp:Label>
                    </ItemTemplate>                               
                </asp:TemplateField>                    
                <asp:TemplateField   HeaderText = "Check #">
                    <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Check")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtBankDataCheck" runat="server" Text='<%# Eval("Check")%>'></asp:TextBox>
                    </EditItemTemplate>                  
                <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>     
                <asp:TemplateField  HeaderText = "Bank Dept Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblBankDataBankDeptDt" runat="server" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:Label>
                    </ItemTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtBankDataBankDeptDt" runat="server" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:TextBox>
                </EditItemTemplate>
                </asp:TemplateField>                  
                <asp:TemplateField  HeaderText = "Type">
                    <ItemTemplate>
                        <asp:Label ID="lblBankDataType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate >
                    <asp:TextBox ID="txtBankDataType" runat="server" Text='<%# Eval("Type")%>'></asp:TextBox>                                                                     
                    </EditItemTemplate>                                 
                </asp:TemplateField> 
                 <asp:TemplateField   HeaderText = "Pay Amt">
                    <ItemTemplate>
                        <asp:Label ID="lblBankDataPayAmt" runat="server" Text='<%# Eval("Pay_Amt")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtBankDataPayAmt" runat ="Server" Text='<%# Eval("Pay_Amt")%>'></asp:TextBox>
                    </EditItemTemplate>                  
                <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField> 
                <asp:CommandField  ShowEditButton="false" />
            </Columns>
        </asp:GridView>
       
        <br />
        </div> 
        <asp:Label ID="lblBankDataCount" runat="Server"  ForeColor ="Blue" ></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="lblPostedData" runat="server" Text="Posted Data" Font-Bold="True" 
            Font-Size="Medium"></asp:Label>
        &nbsp;<asp:LinkButton ID="lnkRefresh" runat="server" Visible="False">Refresh Table</asp:LinkButton>
        <br />
     
              <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <div id="Unmat_Sea_Div" runat ="server" style="height:26px; width:1200px; position:relative; top:0px; margin:0;padding:0; left: 0px;">
        <center>
                   <table id="Table1">
                       <tr style="font-weight:bold">
                       <td></td>
                       <td></td>
                           <td style="width:100px" id="PostedDataNum" runat ="Server" visible ="False" >
                               Number</td>      
                            <td style="width:100px">
                                    BatchID</td>
                           <td style="width:100px">
                                    Invoice</td>
                           <td style="width:100px">                                   
                                     Check #</td>
                          <td style="width:100px">
                               Deposit Date</td>
                          <td style="width:110px">
                               Closing Dt</td>
                          <td style="width:110px">
                               Create Dt</td>
                          <td style="width:110px">
                               Designation</td>
                          <td style="width:110px">
                               Type</td>
                          <td style="width:110px">
                               Pay Amt</td>
                          <td style="width:110px">
                               Pay Code</td>
                          <td style="width:110px">
                               Posted Dt</td>                         
                       </tr>
                      
                       <tr style="font-weight:bold">
                           <td>
                               <asp:CheckBox  ID="ckh" runat="server" Visible="False"></asp:CheckBox></td>
                           <td style="width:100px">
                              <asp:TextBox ID="TxtPostedDataNum" runat="server" Width="58px" Visible="False"
                                   AutoPostBack="True"></asp:TextBox></td>
                            <td style="width:100px">
                               <asp:TextBox ID="TxtPostedDataBatchID" runat="server" Width="58px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:100px">
                                    <asp:TextBox ID="TxtPostedDataInv" runat="server" Width="92px" 
                                        AutoPostBack="True"></asp:TextBox></td>
                           <td style="width:100px">                                   
                                     <asp:TextBox ID="TxtPostedDataChk" runat="server" Width="95px" 
                                         AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:100px">
                               <asp:TextBox ID="TxtPostedDataBDD" runat="server" Width="102px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtPostedDataCD" runat="server" Width="97px" AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtPostedDataCDt" runat="server" Width="97px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtPostedDataDes" runat="server" Width="102px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                                <asp:TextBox ID="TxtPostedDataType" runat="server" Width="102px" 
                                    AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtPostedDaTaPayAmt" runat="server" Width="86px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtPostedDataPayCode" runat="server" Width="105px" 
                                   AutoPostBack="True"></asp:TextBox></td>
                          <td style="width:110px">
                               <asp:TextBox ID="TxtPostedDataPD" runat="server" Width="80px" AutoPostBack="True"></asp:TextBox></td>                         
                       </tr>
                   </table>
                    </center>
               </div>
               <br />
               <br />
        <div runat="server" id="Unmat_Div" 
            style="overflow: auto; height: 420px; width: 1239px;">                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdPostedData" runat="server" AllowSorting="True" AllowPaging ="True"
                        AutoGenerateColumns="False" Height="122px" PageSize = "15"
                        OnPageIndexChanging="PostedDataOnPaging" 
                        onrowcancelingedit="PostedDataCancelEdit" onrowediting="PostedDataEdit" 
                        onrowupdating="PostedDataUpdate" ShowFooter="True" ShowHeader="False" 
                        Width="250px">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkPostedData" runat="Server" Checked="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Number" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataNumber" runat="server" Text='<%# Eval("Number")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BatchID">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataBatchID" runat="server" Text='<%# Eval("BatchID")%>'></asp:Label>
                                </ItemTemplate>                                
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataBatchID" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataInvoice" runat="server" Text='<%# Eval("Invoice")%>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataInvoice" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check #">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataCheck" runat="server" Text='<%# Eval("Check")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPostedDataCheck" runat="server" Text='<%# Eval("Check")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataCheck" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Dept Dt">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataBankDeptDt" runat="server" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPostedDataBankDeptDt" runat="server" Text='<%# Eval("Bank_Dept_Dt")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataBankDeptDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Dt">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataClosingDt" runat="server" Text='<%# Eval("Closing_Dt")%>'></asp:Label>
                                </ItemTemplate>                              
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataClosingDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Create Dt">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataCreateDt" runat="server" Text='<%# Eval("Create_Dt")%>'></asp:Label>
                                </ItemTemplate>                             
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataCreateDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtPostedDataDesignation" runat="server" Width="100px" Text='<%# Eval("Designation")%>'></asp:TextBox>                                  
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="dpPostedDataDesignation" runat="server" Width="100px">
                                        <asp:ListItem Text="ERA" Value="ERA" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="non-ERA" Value="non-ERA"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtPostedDataDesignation" runat="server" Width="100px"></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                </ItemTemplate>                                
                                <EditItemTemplate >
                                    <asp:TextBox ID="txtPostedDataType" runat="server" Text='<%# Eval("Type")%>'></asp:TextBox>                                                                     
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <%--<asp:TextBox ID="txtPostedDataType" runat="server" Width="100px"></asp:TextBox>--%>
                                     <asp:DropDownList ID="dpPostedDataType" runat="server" Width="100px" >
                                        <asp:ListItem Text="CHECK" Value="CHECK" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="CREDIT CARD" Value="CREDIT CARD" ></asp:ListItem>
                                        <asp:ListItem Text="EFT" Value="EFT"></asp:ListItem>                                        
                                        <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>                                                                               
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay Amt">
                            <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataPayAmt" runat="server" Text='<%# Eval("Pay_Amt")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate >
                                    <asp:TextBox ID="TxtPostedDataPayAmt" runat ="Server" Text='<%# Eval("Pay_Amt")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataPayAmt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataPayCode" runat="server" Text='<%# Eval("Pay_Code")%>'></asp:Label>
                                </ItemTemplate>                             
                                <FooterTemplate>
                                    <asp:DropDownList ID="dpPostedDataPayCode" runat="server" Width="100px">
                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Client Deposit" Value="Z01" ></asp:ListItem>
                                        <asp:ListItem Text="TOS Payment" Value="Z02"></asp:ListItem>    
                                        <asp:ListItem Text="Client Transfer" Value="Z03"></asp:ListItem>    
                                        <asp:ListItem Text="Posting Error To Be Corrected" Value="Z04"></asp:ListItem>    
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtPostedDataPayCode" runat="server" Width="100px"></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Posted Dt">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedDataPostedDt" runat="server" Text='<%# Eval("Posted_Dt")%>'></asp:Label>
                                </ItemTemplate>                             
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPostedDataPostedDt" runat="server" Width="100px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" runat="server" OnClick="AddPostedData" Text="Add" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID = "grdPostedData" />
                  </Triggers>
            </asp:UpdatePanel>
        </div> 
        <asp:Label ID="lblPostedDatacount" runat="Server"  ForeColor ="Blue" ></asp:Label>        
        <br />
        <br />
        <asp:LinkButton ID="lnkUpdate" runat="server">Match</asp:LinkButton>
    </center>
</asp:Content>

