
<%@ Page Title="Bank Reconciliation" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BankMatched.aspx.vb" Inherits="Bank_Reconciliation.BankMatched" %>
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
                <asp:RadioButton ID="rbAll" runat="server" AutoPostBack="True" Checked="True" 
                    Text="ALL" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td> 
                <asp:RadioButton ID="rbEFT" runat="server" Text="EFT" AutoPostBack="True" />
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbLockbox" runat="server" Text="Lock Box" 
                    AutoPostBack="True" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:RadioButton ID="rbCreditCard" runat="server" Text="Credit Card" 
                    AutoPostBack="True" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
             <td>
                <asp:RadioButton ID="rbOther" runat="server" Text="OTHER" 
                    AutoPostBack="True" />
            </td>
        </tr>
        </table>
        <table>
        <tr>
        <td>
            
            <table id="Heading" runat="server" cellspacing="0" cellpadding = "0" rules="all" border="1" 
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:494px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:100px;text-align:center">Deposit Date</td>
                    <td style="width:100px;text-align:center">Check #</td>
                    <td style="width:100px;text-align:center">Amount</td>
                    <td style="width:100px;text-align:center">Type</td>
                    
             </tr>
             </table>
      
        </td>
        </tr>
        </table>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <table>
                <td>
                    <asp:TextBox ID="txtDepositDate" runat="server" AutoPostBack="True" 
                        Width="102px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtCheck" runat="server" AutoPostBack="True"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="True"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtType" runat="server" AutoPostBack="True" Width="101px"></asp:TextBox>
                </td>
        </table>
        </tr>
        <tr> 
            <td> 
                 <div runat="server" id="Div1" 
               
               
            style="overflow: auto; height: 380px; width: 518px; position:relative; top:-3px; left:3px; margin:0;padding:0">
            <asp:GridView ID="grdBankMatched" runat="server" AutoGenerateColumns="False" 
                ShowHeader="False" Width="507px">
                <Columns>                    
                    <asp:BoundField DataField="BankDepositDate" HeaderText="Deposit Date" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="CheckNumber" HeaderText="Check Number" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="100px" />
                </Columns>
            </asp:GridView>
           
            
        </div>
            </td>
        </tr>
    </table>
    </center> 
      </asp:Content>

