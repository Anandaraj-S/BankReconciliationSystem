<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteLong.Master" Title ="Maintenance" CodeBehind="Maintenance.aspx.vb" Inherits="Bank_Reconciliation.Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="margin-left: auto; margin-right: auto; text-align: center;">
  <asp:ScriptManager ID="SMTimer" runat="server">
        </asp:ScriptManager>
  <asp:Timer runat="server" ID="Timerrefresh" Interval="60000" ></asp:Timer>
   <br />
   <br />
   <br />
   <br />

      <asp:Label ID="lblmessage" runat="server" Text="" style="color:Black;" Font-Names="Verdana" 
          Font-Size="Large"></asp:Label>
   <br />
   <br />
   
      </div>
</asp:Content>
