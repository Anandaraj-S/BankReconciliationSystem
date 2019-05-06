<%@ Page Title="Bank Reconciliation" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Variance.aspx.vb" Inherits="Bank_Reconciliation.Variance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LinkButton ID="lnkQueueTotals" runat="server">Queue Totals</asp:LinkButton>
    <p>
        <asp:Label ID="lblClientName" runat="server" Text="Label" 
            CssClass="bold"></asp:Label>
&nbsp;<asp:Label ID="Label1" runat="server" Text="-" CssClass="fieldLabels"></asp:Label>
&nbsp;<asp:Label ID="lblSelectedVariance" runat="server" Text="Label" 
            CssClass="fieldLabels"></asp:Label>
    </p>      
    
    <table>        
    <tr>   
    <td >
        <asp:Menu ID="mnuVariances" runat="server">
            <Items>
                <asp:MenuItem Text="Lockbox" Value="Lockbox"></asp:MenuItem>
                <asp:MenuItem Text="EFT" Value="EFT"></asp:MenuItem>
                <asp:MenuItem Text="Credit Cards" Value="CreditCards"></asp:MenuItem>
                <asp:MenuItem Text="Posted Variance ERA" Value="ERAVariance"></asp:MenuItem>
                <asp:MenuItem Text="Posted Variance Non-ERA" Value="NonERAVariance"></asp:MenuItem>
                <asp:MenuItem Text="Bank Data Load Errors" Value="Errors"></asp:MenuItem>
                <asp:MenuItem Text="Bank Data Other" Value="Other"></asp:MenuItem>
            </Items>
        </asp:Menu>
    </td>    
  
    <td style="width:990px">
    
        <div id ="div1" 
            
            
            style ="height:45px; width:990px; position:relative; top:0px; margin:0;padding:0; left: 150px;">
                        <table id="VarianceHeading" runat="server" cellspacing="0" cellpadding = "0" rules="all" border="1" 
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:990px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:70px;text-align:center">Bank Deposit Date</td>
                    <td style="width:70px;text-align:center">Bank Deposit Amt</td>
                    <td style="width:200px;text-align:center">Bank Check #</td>
                    <td id="Vari" runat ="Server"  style="width:200px;text-align:center">Variance</td>   
                    <td id="BaiCode" runat="Server" style="width:70px;text-align:center">BAI Code</td>   
                    <td id="Noofdays" runat="Server" style="width:70px;text-align:center">No of days</td>   
                    <td id="UpdateUser" runat="Server" style="width:70px;text-align:center">Update User</td>   
                     <td id="PayerName" runat="Server" style="width:200px;text-align:center">Payer</td>   
                   <td id="BankRecordID" runat ="server"  style="width:70px;text-align:center">BankRecordID</td>   
                    <td id="PostHeaderID" runat ="server"  style="width:70px;text-align:center">PostHeaderID</td>
             </tr>
             </table>
        </div>
      <%-- <center>--%>
       <div id="Div1"  runat="server" class="WordWrap"  style="overflow-x:hidden;overflow-y: Scroll;  height: 380px; width: 990px; position:relative; top:4px; left:150px; margin:0;padding:0">
            <asp:GridView ID="grdVariance" runat="server" AutoGenerateColumns="False" 
                ShowHeader="False" Width="990px">
                <Columns>
                    <asp:ButtonField CommandName="DateSelected" DataTextField="BANK_DEPOSIT_DATE" 
                        HeaderText="Bank Deposit Date" Text="Button" ItemStyle-Width="70px" />
                    <asp:BoundField DataField="BANK_DEPOSIT_AMOUNT" HeaderText="Bank Deposit Amt" ItemStyle-Width="70px" />
                    <asp:BoundField HeaderText="Bank Check #" DataField="BANK_CHECK_NUMBER" ItemStyle-Width="200px" />                   
                    <asp:BoundField HeaderText="Variance" ItemStyle-Width="200px" DataField ="Variance"  />
                    <asp:BoundField HeaderText="BAI Code" ItemStyle-Width="70px" DataField ="BAI_Code"  />
                    <asp:BoundField HeaderText="Noofdays" ItemStyle-Width="70px" DataField ="Noofdays"  />
                    <asp:BoundField HeaderText="UpdateUser" ItemStyle-Width="70px" DataField ="UpdateUser"  />                    
                    <asp:BoundField HeaderText="BankID" ItemStyle-Width="70px" DataField ="BankRecordID"  />
                    <asp:BoundField HeaderText="PostHeaderID" ItemStyle-Width="70px" DataField ="PostHeaderID"    />
                     <asp:BoundField HeaderText="Payer Name" ItemStyle-Width="200px" DataField ="PayerName"    />
              
                </Columns>
            </asp:GridView>
           
            
        </div>
    <%--</center>--%>
    </td>   
    </tr>
   </table> 
</asp:Content>
