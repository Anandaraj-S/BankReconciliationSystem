

Public Class PostedDataAdditionTrial
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Function BindDatatable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ClientNumber", GetType(Int32))
        dt.Columns.Add("BatchNumber", GetType(String))
        dt.Columns.Add("DepositDate", GetType(Date))
        dt.Columns.Add("ClosingDate", GetType(String))
        dt.Columns.Add("InvoiceNumber", GetType(Int32))
        dt.Columns.Add("PaymentAmount", GetType(Int32))
        dt.Columns.Add("PayCode", GetType(Int32))
        dt.Columns.Add("Designation", GetType(String))
        dt.Columns.Add("DataType", GetType(String))
        Return dt
    End Function

    Protected Sub OnButtonClicked(sender As Object, e As EventArgs) Handles Bulk_Upload.Click
        Response.ClearContent()
        Response.Buffer = True
        Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "Customers.xls"))
        Response.ContentType = "application/ms-excel"
        Dim dt As DataTable = BindDatatable()
        Dim str As String = String.Empty
        For Each dtcol As DataColumn In dt.Columns
            Response.Write(str + dtcol.ColumnName)
            str = vbTab
        Next
        Response.Write(vbLf)
        For Each dr As DataRow In dt.Rows
            str = ""
            For j As Integer = 0 To dt.Columns.Count - 1
                Response.Write(str & Convert.ToString(dr(j)))
                str = vbTab
            Next
            Response.Write(vbLf)
        Next
        Response.[End]()

    End Sub
End Class