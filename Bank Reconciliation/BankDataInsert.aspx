<%@ Page Language="vb" Title="Bank Reconciliation"  AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="BankDataInsert.aspx.vb" Inherits="Bank_Reconciliation.BankDataInsert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LinkButton ID="lnkWQQueueTotals" runat="server">Queue Totals</asp:LinkButton>
&nbsp;&nbsp;
    <asp:LinkButton ID="lnkWQSelectedQueue" runat="server">[name of selected queue]</asp:LinkButton>
   &nbsp;
    <asp:LinkButton ID="lnkWorkQueue" runat="server">Work Queue</asp:LinkButton>
   <br />
    <p>
        <asp:Label ID="lblWQClient" runat="server" Text="Label" CssClass="fieldLabels"></asp:Label>
        <b>&nbsp;<asp:Label ID="lblUserName" runat="server" CssClass="fieldLabels" Text="-"></asp:Label>
&nbsp;<asp:Label ID="lblWQSelectedQueue" runat="server" CssClass="fieldLabels" 
            Text="Label"></asp:Label>
&nbsp;<asp:Label ID="lbluserID" runat="server" CssClass="fieldLabels" Text="-"></asp:Label></b> 
     </p>
    <center>
        <asp:Label ID="lblMandatory" runat="server" Text=" * Mandatory Field " ForeColor ="Red" Font-Bold ="True"></asp:Label>
    <table>
    <tr>
    <td>Account Name   *</td>
    <td >        
        <asp:DropDownList ID="ddlAccountName" runat="server" AutoPostBack ="True" Height="25px" Width="154px">
        </asp:DropDownList> 
    </td>
    </tr>
    <tr>
    <td>Account Number   </td>
    <td >
        <asp:TextBox ID="txtAccountNumber" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>Amount  </td>
    <td> 
        <asp:TextBox ID="TxtAmount" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
    <td>BAI Code *</td>
    <td>
        <asp:DropDownList ID="ddlBaicode" runat="server" Height="25px" Width="154px" 
            AutoPostBack="True">
        </asp:DropDownList> </td>
    </tr>
    <tr>
    <td>Bank ID   </td>
    <td>
        <asp:TextBox ID="txtBankId" runat="server"></asp:TextBox> </td>
    </tr>
    <tr>
    <td>Check Number </td>
    <td>
        <asp:TextBox ID="txtCheckNumber" runat="server"></asp:TextBox> </td>
    </tr>
    <tr>
    <td>Payer </td>
    <td> 
        <asp:DropDownList ID="ddlPayer" runat="server" Height="27px" Width="154px">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>Deposit Date *</td>
    <td>
    &nbsp;<asp:TextBox ID="txtDepositdate" runat="server" ></asp:TextBox>
    &nbsp;</td>
    <td>[MM/DD/YYYY]</td>
    </tr>
    <tr>
    <td>Text </td>
    <td> 
        <asp:TextBox ID="txtText" runat="server"></asp:TextBox></td>
    </tr>
    <tr> 
    <td>Type </td>
    <td> 
        <asp:TextBox ID="TxtType" runat="server"></asp:TextBox></td>
        <%-- <asp:DropDownList ID="ddltype" runat="server" Height="27px" Width="154px">
        <asp:ListItem Text="CHECK" Value="CHECK" Selected="True"></asp:ListItem>
        <asp:ListItem Text="CREDIT CARD" Value="CREDIT CARD" ></asp:ListItem>
        <asp:ListItem Text="EFT" Value="EFT"></asp:ListItem>                                        
        <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>   
                                       
        </asp:DropDownList>--%>
        
    </tr>
    
    </table>
        <asp:Label ID="lblErrorMessgae" runat="server" Text="  " ForeColor ="Red" Font-Bold ="True"  ></asp:Label>
        <br />
        <asp:Button ID="btnSave" runat="server" OnClick ="SaveBankData" Text="Save" />
    </center>
    </asp:Content>
