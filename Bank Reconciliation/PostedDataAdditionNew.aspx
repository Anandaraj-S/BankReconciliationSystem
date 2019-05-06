<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PostedDataAdditionNew.aspx.vb" MasterPageFile="~/Site.Master" Inherits="Bank_Reconciliation.PostedDataAdditionNew" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Bank Reconciliation</title>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</head>
<body>
   
      <form action="Post">
     
           <div class="form-group col-md-12 row">
             <asp:Label ID="Label1" runat="server" CssClass="col-md-6" Text ="Client Number"></asp:Label>
             <asp:TextBox runat="server" class="form-control col-md-6" id="client_number" placeholder="Enter Client Number" AutoPostBack="True"> </asp:TextBox>
           </div>
      
          <div class="form-group col-md-12 row">
             <asp:Label ID="Label2" runat="server" CssClass="col-md-6" Text ="Batch Number"></asp:Label>
             <asp:TextBox runat="server" class="form-control col-md-6" id="batch_number" placeholder="Enter Batch Number" AutoPostBack="True"> </asp:TextBox>
          </div>
          <div class="form-group col-md-12 row" >
             <asp:Label ID="Label3" runat="server" CssClass="col-md-6" Text ="Bank Deposit Date"></asp:Label>
             <input type="text" class="form-control col-md-6" id="bank_deposit_date" placeholder="Select Deposit Date"/>
                    <span class="input-group-addon" >
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
           </div>          
          
           <div class="form-group col-md-12 row">
             <asp:Label ID="Label4" runat="server" CssClass="col-md-6" Text ="Closing Date"></asp:Label>
             <input type="text" class="form-control col-md-6" id="closing_date" placeholder="Select Closing Date"/>
                    <span class="input-group-addon" >
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
          </div>
          <div class="form-group col-md-12 row">
            <asp:Label ID="Label5" runat="server" CssClass="col-md-6" Text ="Invoice Number"></asp:Label>
             <asp:TextBox  runat="server" class="form-control col-md-6" id="invoice_number" AutoPostBack="True"> </asp:TextBox>            
          </div>
          <div class="form-group col-md-12 row">
            <asp:Label ID="Label6" runat="server" CssClass="col-md-6" Text ="Payment Amount"></asp:Label>
             <asp:TextBox runat="server" class="form-control col-md-6" id="payment_amount" AutoPostBack="True"> </asp:TextBox>
          </div>
          <div class="form-group col-md-12 row">
            <asp:Label ID="Label7" runat="server" CssClass="col-md-6" Text ="Pay Code"></asp:Label>
             <asp:TextBox runat="server" class="form-control col-md-6" id="pay_code" AutoPostBack="True"> </asp:TextBox>
          </div>
          <div class="form-group col-md-12 row">
            <asp:Label ID="Label8" runat="server" CssClass="col-md-6" Text ="Designation"></asp:Label>
             <asp:TextBox runat="server" class="form-control col-md-6" id="designation" AutoPostBack="True"> </asp:TextBox>
          </div>
          <div class="form-group col-md-12 row">
            <asp:Label ID="Label9" runat="server" CssClass="col-md-6" Text ="Data Type"></asp:Label>
            <select class="form-control col-md-6" id="data_type">
                  <option selected>--Select Data Type--</option>
                        <option>Check</option>
                        <option>EFT</option>
                        <option>Credit Card</option>
                        <option>Others</option>
            </select>                      
          </div>
           <div class="row">
             <div class="col text-center">
             <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Submit"/>  
          
           </div>
           </div>           
    </form>     
  </body>
  <script type="text/javascript">
      $(function () {
          $('#bank_deposit_date').datepicker();
          $('#closing_date').datepicker();
      });
    </script>
</html>
