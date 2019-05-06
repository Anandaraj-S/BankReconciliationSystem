<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PostedDataAddition.aspx.vb" Inherits="Bank_Reconciliation.PostedDataAddition" Title ="PostedDataAddition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Bank Reconciliation</title>

<script src="https://code.jquery.com/jquery-3.4.0.min.js" integrity="sha256-BJeo0qm959uMBGb65z40ejJYGSgR7REI4+CW1fNKwOg=" crossorigin="anonymous"></script>
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker.min.css" rel="stylesheet"/>
<script src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>

</head>
<body>
   
      <form class="container">
     
           <div class="form-group col-md-12 row">
              <label for="client_number" class="col-md-6">Client Number</label>
             <input type="text" class="form-control col-md-6" id="client_number" placeholder="Enter Client Number"/>
           </div>
      
          <div class="form-group col-md-12 row">
             <label for="batch_number" class="col-md-6">Batch Number</label>
             <input type="text" class="form-control col-md-6" id="batch_number" placeholder="Enter Batch Number"/>
          </div>
          <div class="form-group col-md-12 row" >
             <label for="bank_deposit_date" class="col-md-6">Bank Deposit Date</label>
             <input type="text" class="form-control col-md-6" id="bank_deposit_date" placeholder="Select Deposit Date"/>
                    <span class="input-group-addon" >
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
           </div>          
          
           <div class="form-group col-md-12 row">
             <label for="closing_date" class="col-md-6">Closing Date</label>
             <input type="text" class="form-control col-md-6" id="closing_date" placeholder="Select Closing Date"/>
                    <span class="input-group-addon" >
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
          </div>
          <div class="form-group col-md-12 row">
            <label for="invoice_number" class="col-md-6">Invoice Number</label>
             <input type="text" class="form-control col-md-6" id="invoice_number"/>             
          </div>
          <div class="form-group col-md-12 row">
            <label for="payment_amount" class="col-md-6">Payment Amount</label>
             <input type="text" class="form-control col-md-6" id="payment_amount"/>
          </div>
          <div class="form-group col-md-12 row">
            <label for="pay_code" class="col-md-6">Pay Code</label>
             <input type="text" class="form-control col-md-6" id="pay_code"/>
          </div>
          <div class="form-group col-md-12 row">
            <label for="designation" class="col-md-6">Designation</label>
             <input type="text" class="form-control col-md-6" id="designation"/>
          </div>
          <div class="form-group col-md-12 row">
            <label for="data_type" class="col-md-6">Data Type</label>
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
             <button type="submit" class="btn btn-primary" onclick="SavePostedData()">Submit</button>
           </div>
           </div>           
    </form>     
  </body>
  <%--<script type="text/javascript">
      $(function () {
          $('#bank_deposit_date').datepicker();
          $('#closing_date').datepicker();
      });

      function SavePostedData({
              debugger
             $.ajax({
              type: "POST",
              url: "PostedDataAddition.aspx.cs/AddPostedData",
              data: { 
                      Client_Number: $('#client_number').val(),
                      Batch_Number:  $('#batch_number').val(),
                      Deposit_Date:  $('#bank_deposit_date').val(),
                      Closing_Date:  $('#closing_date').val(),
                      InvoiceNumber: $('#invoice_number').val(),
                      PaymentAmount: $('#payment_amount').val()                          
                       },
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: OnSuccess,
              failure: function (response) {
                  alert(response.d);
              }
          });
     });
    </script>--%>
</html>
