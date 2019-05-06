<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SearchData.aspx.vb" Inherits="Bank_Reconciliation.SearchData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">

        .display-none
        {
            display: none;
        }
        
        .margin {
           margin-bottom:15px;
           margin-right:15px;
           margin-top:5px;
         }
       
</style>
<script type="text/javascript">


    $(function () {
    debugger
        $("#bankdata_depositdate").datepicker();
        $("#BankDepositDate").datepicker();
        var hidSourceID = document.getElementById("<%=hidSourceID.ClientID%>");
        hidSourceID.value = "SearchBankData";
    });
    
    function SearchBankData() {
        $("#posted_data").addClass("display-none");
        $("#bank_data").removeClass("display-none");
        var hidSourceID = document.getElementById("<%=hidSourceID.ClientID%>");
        hidSourceID.value = "SearchBankData";
    }

    function SearchPostedData() {
        $("#bank_data").addClass("display-none");
        $("#posted_data").removeClass("display-none");
        var hidSourceID = document.getElementById("<%=hidSourceID.ClientID%>");
        hidSourceID.value = "SearchPostedData";
    }            
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:HiddenField ID="hidSourceID" runat="server" />
<div class="col-md-12 text-center">
    <div class="btn-group" data-toggle="buttons">
      <asp:Button ID="SearchBankData" runat="server" OnClientClick="SearchBankData(this.id)" class="btn btn-outline-primary active" Text="Search Bank Data"/>
     <asp:Button ID="SearchPostedData" runat="server" OnClientClick="SearchPostedData(this.id)" class="btn btn-outline-primary" Text="Search Posted Data" />    
    </div>
</div>

 
<div class="col-md-12 row margin display-none"  id="posted_data">
 <div class="col-md-3">
     <asp:Label ID="Label2" runat="server" Text ="Batch Number"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="batch_number" placeholder="Enter Batch Number" AutoPostBack="True"> </asp:TextBox>
 </div>
  <div class="col-md-3">
     <asp:Label ID="Label3" runat="server" Text ="Invoice Number"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="invoice_number" placeholder="Enter Invoice Number" AutoPostBack="True"> </asp:TextBox>
</div>
<div class="col-md-3">
    <asp:Label ID="Label9" runat="server" Text ="Data Type"></asp:Label>
            <asp:DropDownList runat="server" class="form-control margin" ID="PostedDataDataType">
                        <asp:ListItem Value="">--Select--</asp:ListItem>
                        <asp:ListItem Value="0">Check</asp:ListItem>
                        <asp:ListItem Value="1">EFT</asp:ListItem>
                        <asp:ListItem Value="2">Credit Card</asp:ListItem>
                        <asp:ListItem Value="3">Others</asp:ListItem>
           </asp:DropDownList>   
</div>
          <div class="col-md-3">
            <asp:Label ID="Label1" runat="server" Text ="Bank Deposit Date"></asp:Label>
            <%--<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="DateChange"> </asp:Calendar>
            <asp:TextBox ID="BankDepositDate" runat="server" class="form-control margin" placeholder="Enter Deposit Date" autocomplete="off"></asp:TextBox>--%>

            <asp:TextBox ID="BankDepositDate" runat="server" class="form-control margin" placeholder="Enter Deposit Date" autocomplete="off" TextMode="Date">
                   
             </asp:TextBox>
</div>
</div>



<div class="col-md-12 row margin" id="bank_data">
<div class="col-md-3">
     <asp:Label ID="Label15" runat="server" Text ="Client No"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="bank_data_client_number" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="bank_data_client_number" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
</asp:RegularExpressionValidator>
</div>
<div class="col-md-3">
     <asp:Label ID="Label10" runat="server" Text ="Deposit Date"></asp:Label>

     <asp:TextBox ID="bankdata_depositdate" runat="server" class="form-control margin" placeholder="Select Deposit Date" autocomplete="off" TextMode="Date"> 
       
       </asp:TextBox>
</div>
<div class="col-md-3">
     <asp:Label ID="Label11" runat="server" Text ="Data Type"></asp:Label>
        <asp:DropDownList runat="server" class="form-control margin" ID="BankDataDataType">
                        <asp:ListItem Value="">--Select--</asp:ListItem>
                        <asp:ListItem Value="0">Check</asp:ListItem>
                        <asp:ListItem Value="1">EFT</asp:ListItem>
                        <asp:ListItem Value="2">Credit Card</asp:ListItem>
                        <asp:ListItem Value="3">Others</asp:ListItem>
           </asp:DropDownList>             
</div>

<div class="col-md-3">
     <asp:Label ID="Label12" runat="server" Text ="Check No"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="bank_check_no" placeholder="Enter Check Number" AutoPostBack="True"> </asp:TextBox>
</div>
</div>

<%--<div class="row">
     <asp:Label ID="Label13" runat="server" Text ="Payment amount"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="TextBox10" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
</div>
<div class="row">
     <asp:Label ID="Label14" runat="server" Text ="Client Number"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="TextBox11" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
</div>
</div>
<div class="col-md-4">
<div class="row">
     <asp:Label ID="Label16" runat="server" Text ="Client Number"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="TextBox13" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
</div>
<div class="row">
     <asp:Label ID="Label17" runat="server" Text ="Client Number"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="TextBox14" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
</div>
<div class="row">
     <asp:Label ID="Label18" runat="server" Text ="Client Number"></asp:Label>
     <asp:TextBox runat="server" class="form-control margin" id="TextBox15" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
</div>--%>
 <asp:Label ID="searchDataError" runat="server" CssClass="failureNotification" Text="Label" Visible="False"></asp:Label>

        <div class="col-md-12 text-center margin"> 
            <asp:Button ID="SearchData" runat="server" class="btn btn-outline-primary" Text="Search"/>
        </div>

<asp:MultiView ID="search_data_grid" runat="server">
  <asp:View runat="server" ID="search_data">
        <div>
        <center>
            <asp:Label ID="Label4" runat="server" CssClass="viewHeading" 
                Text="Data Results"></asp:Label>
            <br />
            <div style ="height:20px;width:100%;top:0px; margin:0;padding:0">
            <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="Table1"
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:100%;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:44px;text-align:center"></td>
                    <td style="width:194px;text-align:center">Account Name</td>
                    <td style="width:112px;text-align:center">Account Number</td>
                    <td style="width:106px;text-align:center">Amount</td>
                    <td style="width:10px;text-align:center">Deposit Date</td>
                    <td style="width:451px;text-align:center">IsMatched</td>
                    <td style="width:40px;text-align:center">Type</td>
                    <td style="width:148px;text-align:center">Created Date</td>
                    <td style="width:60px;text-align:center">Comments</td>
                    <td style="width:148px;text-align:center">Client No.</td>
                    <td style="width:94px;text-align:center"></td>
                </tr>
             </table>

            </div>
            <div runat="server" id="Div1" style="overflow: auto; height: 425px; width: 100%; top:0px; margin:0;padding:0">
            <asp:GridView ID="searchDataResults" runat="server" AutoGenerateColumns="False" ShowHeader = "False" DataKeyNames="UserRowID">
                <Columns>
                 <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                <ItemTemplate>
                        <asp:Label ID="lblBankDataCheck" runat="server" Text='<%# Eval("Bank_DataID")%>'></asp:Label>
                 </ItemTemplate>
                    <%--<asp:ButtonField CommandName="EditUser" Text="Edit" ItemStyle-Width="55px"/>
                    <asp:BoundField DataField="Bank_DataID" HeaderText="Bank Data ID" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="Account_Name" HeaderText="Account Name" ItemStyle-Width="40px"/>
                    <asp:BoundField DataField="Account_Number" HeaderText="Account Number" ItemStyle-Width="40px"/>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="BAI_Code" HeaderText="BAI Code" ItemStyle-Width="250px"/>
                    <asp:BoundField DataField="Bank_ID" HeaderText="Bank ID" ItemStyle-Width="40px" />
                    <asp:BoundField DataField="BD.Check_Number" HeaderText="BD Check Number" ItemStyle-Width="148px"/>
                    <asp:BoundField DataField="BD.Payer" HeaderText="BD Payer" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="Deposit_Date" HeaderText="Deposit Date" ItemStyle-Width="148px"/>
                   <asp:ButtonField CommandName="DeleteUser" Text="Delete User" ItemStyle-Width="74px"/>--%>
                </Columns>
            </asp:GridView>
            </div>
            <br />
            <asp:LinkButton ID="lnkAddUser" runat="server">Add New User</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkUserReturn" runat="server">Return</asp:LinkButton>
        </center>
        </div>
    </asp:View>
 </asp:MultiView>
    </asp:Content>

