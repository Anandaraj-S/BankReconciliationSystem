<%@ Page Title="Administration" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" 
   CodeBehind="ApplicationManagement.aspx.vb"  Inherits="Bank_Reconciliation.ApplicationManagement"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        .style1
        {
            width: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<asp:MultiView runat="server" ID="mvUserAdmin">
    <asp:View runat="server" ID="select">
        <center>
            <br />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                Text="Application Management"></asp:Label>
            <br />
            <br />
            <table>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblPayerClient" runat="server" Text="Client"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlClient" runat="server" 
                        Width="238px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
            <br />
            <asp:LinkButton ID="lnkOffCycleRefresh" runat="server">Off Cycle Refresh</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkExit" runat="server">Back</asp:LinkButton>
        </center>
    </asp:View>
    
</asp:MultiView>
</div>
</asp:Content>
