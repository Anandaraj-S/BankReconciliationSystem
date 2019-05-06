<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="Bank_Reconciliation._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="container">
    <asp:Label ID="Label1" runat="server" Text="Current User: "></asp:Label>
    <asp:Label ID="lblcuruser" runat="server" Font-Bold="True"></asp:Label>
    &nbsp;<asp:LinkButton ID="lnkUserAdmin" runat="server">Administration</asp:LinkButton>
    <br />
         <asp:LinkButton ID="lnkITAdmin" runat="server">IT Administration</asp:LinkButton>
    <br />
    
    <center>
        <h2>Queue Totals</h2>
    </center>
    <center>
            <asp:LinkButton ID="searchData" runat="server">Search Data</asp:LinkButton>
     </center>    
    <div class="col-md-12">
        <center>
        <div >
            <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="Table1"
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:100%;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:350px;text-align:center">Client</td>
                    <td style="width:100px;text-align:center">LockBox</td>
                    <td style="width:100px;text-align:center">EFT</td>
                    <td style="width:100px;text-align:center">Credit Cards</td>
                    <td style="width:100px;text-align:center">Posted Variance ERA</td>
                    <td style="width:100px;text-align:center">Posted Variance Non-ERA</td>
                    <td style="width:100px;text-align:center">Bank Data Other</td>
                    <td style="width:100px;text-align:center">Banking Data Load Errors</td>
                    <td style="width:100px;text-align:center">No Variance (T-1)</td>
                    <td style="width:100px;text-align:center">Holding</td>
             </tr>
             </table>

        </div>
       <div runat="server" id="Div1" style="overflow: auto; height: 425px; width: 100%; position:relative;" >
        <asp:GridView ID="grdSummary" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Master_Client_Number" ShowHeader="False">
            <Columns>
                <asp:BoundField HeaderText="Client" DataField="Client_Name" 
                    ItemStyle-Width="300px">
                </asp:BoundField>
                <asp:ButtonField CommandName="Lockbox" HeaderText="Lockbox" Text="Button" 
                    DataTextField="UNMATCHED_CHECK" ItemStyle-Width="100px">
                </asp:ButtonField>
                <asp:ButtonField CommandName="EFT" HeaderText="EFT" Text="Button" 
                    DataTextField="UNMATCHED_EFT" ItemStyle-Width="100px">
                </asp:ButtonField>
                <asp:ButtonField CommandName="CreditCards" HeaderText="Credit Cards" 
                    Text="Button" DataTextField="UNMATCHED_CC" ItemStyle-Width="100px">
                </asp:ButtonField>
                <asp:ButtonField CommandName="ERAVariance" HeaderText="Posted Variance ERA" 
                    Text="Button" DataTextField="UNMATCHED_ERA" ItemStyle-Width="100px">
                </asp:ButtonField>
                <asp:ButtonField CommandName="NonERAVariance" 
                    HeaderText="Posted Variance Non-ERA" Text="Button" 
                    DataTextField="UNMATCHED_NONERA" ItemStyle-Width="100px">
                </asp:ButtonField>

                <asp:ButtonField CommandName="Other" 
                    HeaderText="Other" Text="Button" 
                    DataTextField="UNMATCHED_OTHERS" ItemStyle-Width="100px">
                </asp:ButtonField>

                
                <asp:ButtonField CommandName="Errors" 
                    HeaderText="Banking Data Load Errors" Text="Button" 
                    DataTextField="BANK_DATA_LOAD_ERROR" ItemStyle-Width="100px">
                </asp:ButtonField>

                 <asp:ButtonField CommandName="No Variance(T-1)" 
                    HeaderText="No Variance(T-1)" Text="Button" 
                    DataTextField="MATCHED_DATA" ItemStyle-Width="100px">
                </asp:ButtonField>
                
                 <asp:ButtonField CommandName="Holding" 
                    HeaderText="Holding" Text="Button" 
                    DataTextField="HOLD_DATA" ItemStyle-Width="100px">
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
        </div>
        </center>
        </div>
        </div>
        </div>
    </asp:Content>
