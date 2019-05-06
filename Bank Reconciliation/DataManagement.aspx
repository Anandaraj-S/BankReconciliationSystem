
<%@ Page Title="Data Management" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" 
    CodeBehind="DataManagement.aspx.vb" Inherits="Bank_Reconciliation.DataManagement" %>
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
    
    <asp:View runat="server" ID="Payer">
    <div>
    <center>
        <asp:Label ID="Label9" runat="server" CssClass="viewHeading" 
            Text="Data Management"></asp:Label>
        <br />
          <br />
        <table>
            <tr>
                <td class="style1">
                    <asp:Label ID="lbllient" runat="server" Text="Client"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" 
                        Width="238px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div style="height:25px;width:800px; position:relative; top:15px; margin:0;padding:0">
            <table id="tbllist" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:800px;color:black;
                     border-collapse:collapse;height:100%;">
                      <tr style="font-weight:bold">
                <td style="width:100px;height :25px; text-align:center" ></td> 
                <td style="width:150px; height :25px; text-align:center"><asp:RadioButton ID="rbLockbox" runat="server" Text="LockBox" AutoPostBack="True" Checked="True"  /></td>
                    <td style="width:150px; height :25px; text-align:center"><asp:RadioButton ID="rbEFT" runat="server" Text="EFT" AutoPostBack="True" /></td>
                    <td style="width:200px; height :25px; text-align:center"><asp:RadioButton ID="rbCreditCard" runat="server" Text="Credit Card" AutoPostBack="True" /></td>
                    <td style="width:200px;  height :25px; text-align:center">
                    <asp:RadioButton ID="rbBankdataother" runat="server" Text="Bank Data other" AutoPostBack="True" />
                    </td>   
                </tr>
            </table>
        </div><br /><br />
        <div style="height:30px;width:800px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblHeader" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:800px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                <td style="width:100px; height :30px;text-align:center"> <asp:CheckBox ID="ChkSelectAll" runat="server" AutoPostBack="true" /></td> 
                <td style="width:150px; height :30px;text-align:center">Bank Deposit Date</td>
                    <td style="width:150px;height :30px; text-align:center">Bank Deposit Amt</td>
                    <td style="width:200px; height :30px;text-align:center">Bank Check #</td>
                    <td  style="width:200px; height :30px;text-align:center">
                        <asp:Label ID="lblVarianceOrBaicode" runat="server" Text="Variance"></asp:Label></td>   
                </tr>
            </table>
        </div>
       
          <div style="height:30px;width:800px; position:relative; top:15px; margin:0;padding:0">
            <table id="tblsearch" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:800px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                <td style="width:90px; height :30px;text-align:center">
               </td> 
                <td style="width:150px; height :30px;text-align:center">
                   <asp:TextBox ID="txtBankDepositeDate" runat="server" Width="135px" 
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td style="width:150px;height :30px; text-align:center">
                    <asp:TextBox ID="txtBankDepositAmt" runat="server" Width="135px" 
                        AutoPostBack="True"></asp:TextBox>
                </td>
                    <td style="width:200px; height :30px;text-align:center">
                    <asp:TextBox ID="txtCheckNo" runat="server" Width="190px" AutoPostBack="True"></asp:TextBox>
                </td>
                    <td  style="width:200px; height :30px;text-align:center">
                    <asp:TextBox ID="txtVarianceOrBaiCode" runat="server" Width="190px" 
                            AutoPostBack="True" ></asp:TextBox>
                </td>
                </tr>
            </table>
        </div>
        <div runat="server" id="divfield" style="overflow: auto; height: 425px; width: 800px; position:relative; top:15px; left:0px; margin:0;padding:0">
        <asp:GridView ID="grdDataMgmt" runat="server" ShowHeader = "False" 
            AutoGenerateColumns="False">
            <Columns>
                    <asp:TemplateField ItemStyle-Width="100px" >
                    <ItemTemplate>
                            <asp:CheckBox ID="chkRecords" runat="server" 
                                ></asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                        </asp:TemplateField>
                   <asp:BoundField DataField="BANK_DEPOSIT_DATE" HeaderText="Bank Deposit Date" 
                    ItemStyle-Width="150px" >
                <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="BANK_DEPOSIT_AMOUNT" HeaderText="Bank Deposit Amt" 
                    ItemStyle-Width="150px" >
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                    <asp:BoundField HeaderText="Bank Check #" DataField="BANK_CHECK_NUMBER" 
                    ItemStyle-Width="200px" >                   
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                    <asp:BoundField HeaderText="Variance" ItemStyle-Width="200px" 
                    DataField ="VARIANCE"  >
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                    <asp:BoundField HeaderText="Account Number" ItemStyle-Width="100px" 
                    DataField ="Account_Number"  ItemStyle-CssClass="hiddencol"  >
                    <ItemStyle CssClass="hiddencol" Width="100px" />
                </asp:BoundField>
                    <asp:BoundField HeaderText="Bank_DataID" ItemStyle-Width="100px" 
                    DataField ="BankRecordID"  ItemStyle-CssClass="hiddencol"  >
                <ItemStyle CssClass="hiddencol" Width="100px" />
                </asp:BoundField>
             </Columns>
        </asp:GridView>
        </div>
        <br />
        <asp:LinkButton ID="lnkDMdelete"  runat="server"  OnClientClick="return confirm('Are You Sure Want to Delete the Selected Records ?')" >Delete</asp:LinkButton>
        <br />
        <br />
        <asp:Label ID="lblDataMgmt" runat="server" CssClass="fieldLabels" 
            Visible="False" Font-Size="Small" ForeColor="Black"></asp:Label>
        <br />
        <asp:LinkButton ID="lnkDataMgmtReturn" runat="server">Return</asp:LinkButton>
    </center>
    </div>
    </asp:View>
  
</asp:MultiView>
</div>
</asp:Content>
