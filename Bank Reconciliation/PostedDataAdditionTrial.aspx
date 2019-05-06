<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PostedDataAdditionTrial.aspx.vb" MasterPageFile="~/Site.Master"  Inherits="Bank_Reconciliation.PostedDataAdditionTrial" Title="Posted Data Addition Trial" %>


<%--<head id="Head1" runat="server">
<title>Bank Reconciliation</title>

<script type="script" src="https://code.jquery.com/jquery-3.4.0.min.js" integrity="sha256-BJeo0qm959uMBGb65z40ejJYGSgR7REI4+CW1fNKwOg=" crossorigin="anonymous"></script>
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker.min.css" rel="stylesheet"/>
<script type="script" src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>

</head>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="css">
.left-border {
    border-left: 1px solid #ccc;
}
<%--.upload-margin
{
    margin: 10px 10px 10px 34px;
}--%>
</style>

<script type="text/javascript">
    $(function () {
        $('#bank_deposit_date').datepicker();
        $('#closing_date').datepicker();
    });

    function rowDelete(link) {      
        var row = link.parentNode.parentNode;
        var table = row.parentNode;
        table.removeChild(row);
        counter--;
    }

    function serializeForm(form) {
        var kvpairs = [];
        debugger
        for (var i = 0; i < form.elements.length; i++) {
           
            var e = form.elements[i];
            if (!e.name || !e.value) continue; // Shortcut, may not be suitable for values = 0 (considered as false)
            switch (e.type) {
                case 'text':
                case 'textarea':
                case 'password':
                case 'hidden':
                    kvpairs.push(encodeURIComponent(e.name) + "=" + encodeURIComponent(e.value));
                    break;
                case 'radio':
                case 'checkbox':
                    if (e.checked) kvpairs.push(encodeURIComponent(e.name) + "=" + encodeURIComponent(e.value));
                    break;
                /*  To be implemented if needed:
                case 'select-one':
                ... document.forms[x].elements[y].options[0].selected ...
                break;
                case 'select-multiple':
                for (z = 0; z < document.forms[x].elements[y].options.length; z++) {
                ... document.forms[x].elements[y].options[z].selected ...
                } */ 
                    break;
            }
        }
        debugger;
        return kvpairs.join("&");
        debugger;
    }

    var counter = 1;
    var limit = 9;
    function addInput(divName) {    
        if (counter == limit) {
            alert("You have reached the limit of adding " + counter + " inputs");
        }
        else {      
            var newdiv = document.createElement('div');
            newdiv.innerHTML = '<div class="form-group col-md-12 row">'+
           '<div class="col-md-1">'+
             ' <label for="client_number">Client Number</label>'+
             '<input type="text" class="form-control" id="client_number"/>'+
          '</div>'+
          '<div class="col-md-1">'+
            '<label for="batch_number">Batch Number</label>'+
             '<input type="text" class="form-control" id="batch_number"/>'+
         '</div>'+
          '<div class="col-md-1">'+
             '<label for="bank_deposit_date">Deposit Date</label>'+
             '<input type="text" class="form-control bank-deposit-date" />' +
                    '<span class="input-group-addon" >'+
                        '<span class="glyphicon glyphicon-calendar"></span>'+
                    '</span>'+
              '</div>'+
          
          '<div class="col-md-1">'+
             '<label for="closing_date">Closing Date</label>'+
             '<input type="text" class="form-control closing-date"/>' +
                    '<span class="input-group-addon">'+
                        '<span class="glyphicon glyphicon-calendar"></span>'+
                    '</span>'+
         '</div>'+
        '<div class="col-md-1">'+
           '<label for="invoice_number">Invoice Number</label>'+
             '<input type="text" class="form-control" id="invoice_number"/>'+           
         '</div>'+
           '<div class="col-md-1">'+
            '<label for="payment_amount">Payment Amount</label>'+
             '<input type="text" class="form-control" id="payment_amount"/>'+
         '</div>'+
           '<div class="col-md-1">'+
            '<label for="pay_code">Pay Code</label>'+
             '<input type="text" class="form-control" id="pay_code"/>'+
          '</div>'+
           '<div class="col-md-1">'+
           '<label for="designation">Designation</label>'+
             '<input type="text" class="form-control" id="designation"/>'+
        '</div>' +         
           '<div class="col-md-1">'+
             '<label for="data_type">Data Type</label>'+
            '<select class="form-control" id="data_type">'+
                  '<option selected>--Select--</option>'+
                        '<option>Check</option>'+
                        '<option>EFT</option>'+
                        '<option>Credit Card</option>'+
                        '<option>Others</option>'+
            '</select>'+
           '</div>' +
            '<div class="col-md-1" style="margin-top: 40px">' +
           '<a href="#" onclick="rowDelete(this); return false;">Delete</a>' +
           '</div>' +                   
           '</div>'
            document.getElementById(divName).appendChild(newdiv);
            counter++;
            $('.bank-deposit-date').datepicker();
            $('.closing-date').datepicker();
        }
    }
   
 </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <form action="Post" class="fluid-container">
     <div id="posted_data_addition">     
           <div class="form-group col-md-12 row"> 
           <div class="col-md-1">
             <asp:Label ID="Label1" runat="server" Text ="Client Number"></asp:Label>      
             <asp:TextBox runat="server" class="form-control" id="client_number" AutoPostBack="True"> </asp:TextBox>
          </div>
          <div class="col-md-1">        
             <asp:Label ID="Label2" runat="server" Text ="Batch Number"></asp:Label>
             <asp:TextBox runat="server" class="form-control" id="batch_number" AutoPostBack="True"> </asp:TextBox>
         </div>
          <div class="col-md-1">
             <asp:Label ID="Label3" runat="server" Text ="Deposit Date"></asp:Label>
             <input type="text" class="form-control" id="bank_deposit_date" autocomplete="off"/>
                    <span class="input-group-addon" >
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
              </div>           
          <div class="col-md-1">
             <asp:Label ID="Label4" runat="server" Text ="Closing Date"></asp:Label>
             <input type="text" class="form-control" id="closing_date" autocomplete="off"/>
                    <span class="input-group-addon" >
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
         </div>
        <div class="col-md-1">
            <asp:Label ID="Label5" runat="server" Text ="Invoice Number"></asp:Label>
             <asp:TextBox  runat="server" class="form-control" id="invoice_number" AutoPostBack="True"> </asp:TextBox>            
         </div>
           <div class="col-md-1">
            <asp:Label ID="Label6" runat="server" Text ="Payment Amount"></asp:Label>
             <asp:TextBox runat="server" class="form-control" id="payment_amount" AutoPostBack="True"> </asp:TextBox>
         </div>
           <div class="col-md-1">
            <asp:Label ID="Label7" runat="server" Text ="Pay Code"></asp:Label>
             <asp:TextBox runat="server" class="form-control" id="pay_code" AutoPostBack="True"> </asp:TextBox>
          </div>
           <div class="col-md-1">
            <asp:Label ID="Label8" runat="server" Text ="Designation"></asp:Label>
             <asp:TextBox runat="server" class="form-control" id="designation" AutoPostBack="True"> </asp:TextBox>
        </div>           
           <div class="col-md-1">
            <asp:Label ID="Label9" runat="server" Text ="Data Type"></asp:Label>
            <select class="form-control" id="data_type">
                  <option selected>--Select--</option>
                        <option>Check</option>
                        <option>EFT</option>
                        <option>Credit Card</option>
                        <option>Others</option>
            </select>                      
          </div> 
          <div class="col-md-3">         
               <asp:Button ID="Bulk_Upload" runat="server" class="btn btn-outline-primary" Text="Download Excel Format" OnClick="OnButtonClicked" />
               <asp:Button ID="btnImport" runat="server" Text="Save Bulk Data" CssClass="btn btn-primary" /> 
             <div class="row" style="margin: 10px 15px 0px 0px">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
             </div>   
          </div>  
          
              
             <%-- <asp:Button ID="btnImport" runat="server" Text="Save Data" CssClass="btn btn-primary" />  --%> 
             
          </div>                                 
        </div> 
        <div class="row">
             <div class="col-md-12 text-center">
              <button type="button" id="add_another_data" runat="server" onclick="addInput('posted_data_addition');" class="btn btn-outline-primary">Add Another Data</button> 
              <button type="button" id="Submit" runat="server" onclick="serializeForm(form);" class="btn btn-primary">Submit</button>
            <%-- <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Submit"/>   --%>         
           </div>
           </div>                                                             
    </form> 
</asp:Content>


           




