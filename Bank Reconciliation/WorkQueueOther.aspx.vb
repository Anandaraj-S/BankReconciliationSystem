Imports System.Data.SqlClient
Public Class WorkQueueOther
    Inherits System.Web.UI.Page

    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"

    Private dsBankData As New DataSet
    Private dsPostedData As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        If Not IsPostBack Then
            lnkWQSelectedQueue.Text = Session("CurrentWorkQueue")
            lblWQClient.Text = Session("CurrentClientName")
            lblWQSelectedQueue.Text = Session("CurrentWorkQueue")
            lblSelectedDate.Text = Session("CurrentBankDate")

            Dim tblBankData As New DataTable
            Dim mrow As DataRow
            Dim cm As New DataColumn
            cm.DataType = System.Type.GetType("System.Boolean")
            cm.ColumnName = "BankData"
            tblBankData.Columns.Add(cm)
            tblBankData.Columns.Add("number")
            tblBankData.Columns.Add("BatchID")
            tblBankData.Columns.Add("invoice")
            tblBankData.Columns.Add("Bank_Dept_Dt")
            tblBankData.Columns.Add("Closing_Dt")
            tblBankData.Columns.Add("Create_Dt")
            tblBankData.Columns.Add("Designation")
            tblBankData.Columns.Add("Type")
            tblBankData.Columns.Add("Pay_Amt")
            tblBankData.Columns.Add("Pay_Code")
            tblBankData.Columns.Add("Posted_Dt")
            tblBankData.Columns.Add("Check")


            For x = 1 To 5
                mrow = tblBankData.NewRow
                mrow("BankData") = 1
                tblBankData.Rows.Add(mrow)
            Next

            grdBankData.DataSource = tblBankData
            grdBankData.DataBind()

            tblBankData.Rows.Clear()
            For x = 1 To 5
                mrow = tblBankData.NewRow
                mrow("BankData") = 0
                tblBankData.Rows.Add(mrow)
            Next

            grdPostedData.DataSource = tblBankData
            grdPostedData.DataBind()

         
                PopulateBankDataGrid()
                PopulatePostedDataGrid()
                MatNum.Visible = False

        End If

    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkUpdate.Click
        'UpdatedBankDataGridRecord()
        'UpdatedPostedDataRecord()
        RefreshPostedDataRecords()
        RefereshGrid()
    End Sub
    Private Sub RefreshPostedDataRecords()
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            RefreshAllRecords(SQLcon, CurrentClient)
            SQLcon.Close()
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub
    Private Sub UpdatedBankDataGridRecord()
        Dim Strvalue As String = ""
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            For Each row As GridViewRow In grdBankData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkBankData"), CheckBox)
                    Dim Number As String = TryCast(row.Cells(1).FindControl("lblMatNumber"), Label).Text
                    If chkRow.Checked Then
                        If Strvalue = "" Then
                            Strvalue = "'" & Number & "'"
                        Else
                            Strvalue = Strvalue & "," & "'" & Number & "'"
                        End If
                    Else
                        UpdatePostedDataMatched(SQLcon, Session("BankDataID"), Session("PostedDataID"), Number, CurrentClient)
                    End If
                End If
            Next

            If Strvalue <> "" Then
                UpdatePostedDataMatched(SQLcon, Session("BankDataID"), Session("PostedDataID"), Strvalue, CurrentClient)
            End If
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub UpdatedPostedDataRecord()
        Dim Strvalue As String = ""
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            For Each row As GridViewRow In grdPostedData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkPostedData"), CheckBox)
                    Dim Number As String = TryCast(row.Cells(1).FindControl("lblUnmatchNumber"), Label).Text
                    If chkRow.Checked Then
                        If Strvalue = "" Then
                            Strvalue = "'" & Number & "'"
                        Else
                            Strvalue = Strvalue & "," & "'" & Number & "'"
                        End If

                    Else
                        UpdatePostedDataMatched(SQLcon, Session("BankDataID"), Session("PostedDataID"), Number, CurrentClient)
                    End If
                End If
            Next

            If Strvalue <> "" Then
                UpdatePostedDataMatched(SQLcon, Session("BankDataID"), Session("PostedDataID"), Strvalue, CurrentClient)
            End If
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub GridView4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPostedData.RowDataBound

        'Special processing for the Header rows
        If e.Row.RowType = DataControlRowType.Header Then
            'build the main header row.
            'BuildHeader(e)
        End If

    End Sub

    Sub BuildHeader(ByRef e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'get a new row
        Dim row As New GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal)
        row = BuildHeaderRow(e)

        'Insert the new header row at the top of the Gridview.
        e.Row.Visible = True
        e.Row.Parent.Controls.AddAt(0, row)

    End Sub

    Public Function BuildHeaderRow(ByRef e As System.Web.UI.WebControls.GridViewRowEventArgs) As GridViewRow

        'get a new row
        Dim row As New GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal)

        Dim cell1 As New TableCell
        cell1.RowSpan = 1
        cell1.ColumnSpan = 1
        row.Cells.Add(cell1)

        Dim cell2 As New TableCell
        Dim txtNumber As New TextBox
        txtNumber.ID = "txtNumber"
        txtNumber.Width = 40
        cell2.RowSpan = 1
        cell2.ColumnSpan = 1
        cell2.Controls.Add(txtNumber)
        row.Cells.Add(cell2)

        Dim cell3 As New TableCell
        Dim txtBankDepDate As New TextBox
        txtBankDepDate.ID = "txtBankDepDate"
        txtBankDepDate.Width = 50
        cell3.RowSpan = 1
        cell3.ColumnSpan = 1
        cell3.Controls.Add(txtBankDepDate)
        row.Cells.Add(cell3)

        Dim cell4 As New TableCell
        Dim txtCD As New TextBox
        txtCD.ID = "txtClosingDt"
        txtCD.Width = 50
        cell4.RowSpan = 1
        cell4.ColumnSpan = 1
        cell4.Controls.Add(txtCD)
        row.Cells.Add(cell4)

        Dim cell5 As New TableCell
        Dim txtCrDt As New TextBox
        txtCrDt.ID = "txtCreateDt"
        txtCrDt.Width = 50
        cell5.RowSpan = 1
        cell5.ColumnSpan = 1
        cell5.Controls.Add(txtCrDt)
        row.Cells.Add(cell5)

        Dim cell6 As New TableCell
        cell6.RowSpan = 1
        cell6.ColumnSpan = 1
        row.Cells.Add(cell6)

        Dim cell7 As New TableCell
        Dim txtINI As New TextBox
        txtINI.ID = "txtINI"
        txtINI.Width = 40
        cell7.RowSpan = 1
        cell7.ColumnSpan = 1
        cell7.Controls.Add(txtINI)
        row.Cells.Add(cell7)

        Dim cell8 As New TableCell
        cell8.RowSpan = 1
        cell8.ColumnSpan = 1
        row.Cells.Add(cell8)

        Dim cell9 As New TableCell
        Dim txtInvoice As New TextBox
        txtInvoice.ID = "txtInvoice"
        txtInvoice.Width = 50
        cell9.RowSpan = 1
        cell9.ColumnSpan = 1
        cell9.Controls.Add(txtInvoice)
        row.Cells.Add(cell9)

        Dim cell10 As New TableCell
        Dim txtPayAmt As New TextBox
        txtPayAmt.ID = "txtPayAmt"
        txtPayAmt.Width = 50
        cell10.RowSpan = 1
        cell10.ColumnSpan = 1
        cell10.Controls.Add(txtPayAmt)
        row.Cells.Add(cell10)

        Dim cell11 As New TableCell
        Dim txtPayCd As New TextBox
        txtPayCd.ID = "txtPayCd"
        txtPayCd.Width = 50
        cell11.RowSpan = 1
        cell11.ColumnSpan = 1
        cell11.Controls.Add(txtPayCd)
        row.Cells.Add(cell11)

        Dim cell12 As New TableCell
        Dim txtPostDt As New TextBox
        txtPostDt.ID = "txtPostDt"
        txtPostDt.Width = 50
        cell12.RowSpan = 1
        cell12.ColumnSpan = 1
        cell12.Controls.Add(txtPostDt)
        row.Cells.Add(cell12)

        Return row
    End Function

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkWQQueueTotals.Click
        'Response.Redirect("Default.aspx")
        Server.Transfer("Default.aspx")
    End Sub

    Protected Sub lnkWQSelectedQueue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkWQSelectedQueue.Click
        'Response.Redirect("Variance.aspx")
        Server.Transfer("Variance.aspx")
    End Sub
    Private Sub PopulateBankDataGrid()

        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
        dsBankData = LoadBankDataOther(SQLcon, "Bank")
        SQLcon.Close()
        grdBankData.DataSource = dsBankData
        grdBankData.DataBind()
        If grdBankData.Rows.Count > 0 Then
            lblBankDataCount.Text = grdBankData.Rows.Count & " Records"
        Else
            lblBankDataCount.Text = "No Bank Data records for this grouping."
        End If
    End Sub
    Private Sub PopulatePostedDataGrid()

        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
        dsPostedData = LoadBankDataOther(SQLcon, "Posted")
        SQLcon.Close()
        grdPostedData.DataSource = dsPostedData
        grdPostedData.DataBind()
        If grdPostedData.Rows.Count > 0 Then
            lblPostedDatacount.Text = grdPostedData.Rows.Count & " Records"
        Else
            lblPostedDatacount.Text = "No PostedData records found."
        End If
    End Sub
    Protected Sub PostedDataEdit(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        grdPostedData.EditIndex = e.NewEditIndex
        SearchPostedDataGrid()
    End Sub
    Protected Sub PostedDataCancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        grdPostedData.EditIndex = -1
        SearchPostedDataGrid()
    End Sub
    Protected Sub PostedDataUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim Number As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("lblUnmatchNumber"), Label).Text
            'Dim BatchID As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchBatchID"), TextBox).Text
            'Dim Invoice As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchInvoice"), TextBox).Text
            Dim BankDeptDt As Date = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchBankDeptDt"), TextBox).Text
            'Dim ClosingDt As Date = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchClosingDt"), TextBox).Text
            'Dim CreateDt As Date = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchCreateDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchDesignation"), TextBox).Text
            Dim Type As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchType"), TextBox).Text
            'Dim PayAmt As Double = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchPayAmt"), TextBox).Text
            'Dim Paycode As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchPayCode"), TextBox).Text
            'Dim PostedDt As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchPostedDt"), TextBox).Text
            Dim Check As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchCheck"), TextBox).Text
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            UpdatePostData(SQLcon, Number, BankDeptDt, Designation, Type, Check, Session("User"), Now.Date, CurrentClient)
            'UpdatePostData(SQLcon, Number, BatchID, Invoice, BankDeptDt, ClosingDt, CreateDt, Designation, Type, PayAmt, Paycode, PostedDt, Check, Session("User"), Now.Date)
            SQLcon.Close()
            'MsgBox("Updated Successfully")
            grdPostedData.EditIndex = -1
            PopulatePostedDataGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub
    Protected Sub PostedDataOnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        PopulatePostedDataGrid()
        grdPostedData.PageIndex = e.NewPageIndex
        grdPostedData.DataBind()
    End Sub
    Protected Sub AddPostedData(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim Invoice As Integer = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchInvoice"), TextBox).Text
            Dim BatchID As Integer = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchBatchID"), TextBox).Text
            Dim BankDeptDt As Date = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchBankDeptDt"), TextBox).Text
            Dim ClosingDt As Date = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchClosingDt"), TextBox).Text
            Dim CreateDt As Date = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchCreateDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdPostedData.FooterRow.FindControl("dpUnMatchDesignation"), DropDownList).Text
            Dim Type As String = DirectCast(grdPostedData.FooterRow.FindControl("dpUnMatchType"), DropDownList).Text
            Dim PayAmt As Double = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchPayAmt"), TextBox).Text
            Dim Paycode As String = DirectCast(grdPostedData.FooterRow.FindControl("dpUnMatchPayCode"), DropDownList).Text
            Dim PostedDt As Integer = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchPostedDt"), TextBox).Text
            Dim Check As String = DirectCast(grdPostedData.FooterRow.FindControl("txtUnMatchCheck"), TextBox).Text

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            InsertPostData(SQLcon, BatchID, Invoice, BankDeptDt, ClosingDt, CreateDt, Designation, Type, PayAmt, Paycode, PostedDt, Check, Session("User"), Now.Date, CurrentClient)
            SQLcon.Close()
            'MsgBox("Record Inserted Successfully")
            grdPostedData.EditIndex = -1
            PopulatePostedDataGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub
    Protected Sub lnkRefresh_Click(sender As Object, e As EventArgs) Handles lnkRefresh.Click
        RefereshGrid()
    End Sub
    Private Sub RefereshGrid()
        PopulateBankDataGrid()
        PopulatePostedDataGrid()
    End Sub
    Private Sub lnkNewBank_Click(sender As Object, e As System.EventArgs) Handles lnkNewBank.Click
        'Response.Redirect("BankDataInsert.aspx")
        Server.Transfer("BankDataInsert.aspx")
    End Sub
    Private Sub SearchPostedDataGrid()

        Dim dsSearch As DataSet
        Dim Number As String = TxtPostedDataNum.Text
        Dim BatchID As String = TxtPostedDataBatchID.Text
        Dim Invoice As String = TxtPostedDataInv.Text
        Dim Bank_Dept_Dt As String = TxtPostedDataBDD.Text
        Dim Closing_Dt As String = TxtPostedDataCD.Text
        Dim Create_Dt As String = TxtPostedDataCDt.Text
        Dim Designation As String = TxtPostedDataDes.Text
        Dim Type As String = TxtPostedDataType.Text
        Dim Pay_Amt As String = TxtPostedDaTaPayAmt.Text
        Dim Pay_Code As String = TxtPostedDataPayCode.Text
        Dim Posted_Dt As String = TxtPostedDataPD.Text
        Dim Check As String = TxtPostedDataChk.Text

        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            If (Number <> "" Or BatchID <> "" Or Invoice <> "" Or Bank_Dept_Dt <> "" Or Closing_Dt <> "" Or Create_Dt <> "" Or Designation <> "" Or Type <> "" Or Pay_Amt <> "" Or Pay_Code <> "" Or
                                                                                                    Posted_Dt <> "" Or Check <> "") Then
                dsSearch = LoadPostedDataSearch(SQLcon, Number, BatchID, Invoice, Bank_Dept_Dt, Closing_Dt, Create_Dt, Designation, Type, Pay_Amt, Pay_Code, Posted_Dt, Check, 2, String.Empty, String.Empty, CurrentClient)
            Else
                dsSearch = LoadPostedData(SQLcon, Session("CurrentBankDate"), Session("CurrentCheckNum"), Session("CurrentWorkQueue"), 2, CurrentClient)
            End If

            SQLcon.Close()
            grdPostedData.DataSource = dsSearch
            grdPostedData.DataBind()
            If grdPostedData.Rows.Count > 0 Then
                lblPostedDatacount.Text = grdPostedData.Rows.Count & " Records"
            Else
                lblPostedDatacount.Text = "No PostedData records."
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub TxtPostedDataBDD_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataBDD.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataCD_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataCD.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataCDt_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataCDt.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataChk_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataChk.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataDes_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataDes.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataInv_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataInv.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataNum_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataNum.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDaTaPayAmt_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDaTaPayAmt.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataPayCode_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataPayCode.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataPD_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataPD.TextChanged
        SearchPostedDataGrid()
    End Sub

    Private Sub TxtPostedDataType_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataType.TextChanged
        SearchPostedDataGrid()
    End Sub
    Private Sub TxtPostedDataBatchID_TextChanged(sender As Object, e As System.EventArgs) Handles TxtPostedDataBatchID.TextChanged
        SearchPostedDataGrid()
    End Sub
    Private Sub txtBankDataSearchDepositDate_TextChanged(sender As Object, e As System.EventArgs) Handles txtBankDataSearchDepositDate.TextChanged
        SearchBankDataGrid()
    End Sub

    Private Sub txtBankDataSearchCheck_TextChanged(sender As Object, e As System.EventArgs) Handles txtBankDataSearchCheck.TextChanged
        SearchBankDataGrid()
    End Sub


    Private Sub TxtBankDataSearchPayamt_TextChanged(sender As Object, e As System.EventArgs) Handles TxtBankDataSearchPayamt.TextChanged
        SearchBankDataGrid()
    End Sub

    Private Sub txtBankDataSearchType_TextChanged(sender As Object, e As System.EventArgs) Handles txtBankDataSearchType.TextChanged
        SearchBankDataGrid()
    End Sub
    Private Sub SearchBankDataGrid()

        Dim dsSearch As DataSet
        Dim Number As String = ""


        Dim Bank_Dept_Dt As String

        Dim Type As String = txtBankDataSearchType.Text
        Dim Pay_Amt As String = TxtBankDataSearchPayamt.Text

        Dim Check As String = txtBankDataSearchCheck.Text
        Try
            If txtBankDataSearchDepositDate.Text <> "" Then
                Bank_Dept_Dt = txtBankDataSearchDepositDate.Text
            Else
                Bank_Dept_Dt = Session("CurrentBankDate")
            End If

            If txtBankDataSearchType.Text <> "" Then
                Type = txtBankDataSearchType.Text
            Else
                Type = Session("CurrentWorkQueue")
            End If
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            'If (Number <> "" Or Bank_Dept_Dt <> "" Or Type <> "" Or Pay_Amt <> "" Or Check <> "") Then
            '    'dsSearch = LoadPostedDataSearch(SQLcon, Number, BatchID, Invoice, Bank_Dept_Dt, Closing_Dt, Create_Dt, Designation, Type, Pay_Amt, Pay_Code, Posted_Dt, Check, 3)
            'Else
            dsSearch = LoadPostedData(SQLcon, Session("CurrentBankDate"), Session("CurrentCheckNum"), Session("CurrentWorkQueue"), 3, CurrentClient)
            'End If

            SQLcon.Close()
            grdBankData.DataSource = dsSearch
            grdBankData.DataBind()
            If grdBankData.Rows.Count > 0 Then
                lblBankDataCount.Text = grdBankData.Rows.Count & " Records"
            Else
                lblBankDataCount.Text = "No Bank Data records."
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BankDataEdit(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        grdBankData.EditIndex = e.NewEditIndex
        PopulateBankDataGrid()
    End Sub
    Protected Sub BankDataCancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        grdBankData.EditIndex = -1
        PopulateBankDataGrid()
    End Sub
    Protected Sub BankDataUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim Number As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("lblUnmatchNumber"), Label).Text
            'Dim BatchID As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchBatchID"), TextBox).Text
            'Dim Invoice As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchInvoice"), TextBox).Text
            Dim BankDeptDt As Date = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchBankDeptDt"), TextBox).Text
            'Dim ClosingDt As Date = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchClosingDt"), TextBox).Text
            'Dim CreateDt As Date = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchCreateDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchDesignation"), TextBox).Text
            Dim Type As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchType"), TextBox).Text
            'Dim PayAmt As Double = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchPayAmt"), TextBox).Text
            'Dim Paycode As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchPayCode"), TextBox).Text
            'Dim PostedDt As Integer = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchPostedDt"), TextBox).Text
            Dim Check As String = DirectCast(grdPostedData.Rows(e.RowIndex).FindControl("txtUnMatchCheck"), TextBox).Text
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            UpdatePostData(SQLcon, Number, BankDeptDt, Designation, Type, Check, Session("User"), Now.Date, CurrentClient)
            'UpdatePostData(SQLcon, Number, BatchID, Invoice, BankDeptDt, ClosingDt, CreateDt, Designation, Type, PayAmt, Paycode, PostedDt, Check, Session("User"), Now.Date)
            SQLcon.Close()
            'MsgBox("Updated Successfully")
            grdPostedData.EditIndex = -1
            PopulatePostedDataGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub
    Protected Sub BankDataOnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        PopulateBankDataGrid()
        grdBankData.PageIndex = e.NewPageIndex
        grdBankData.DataBind()
    End Sub

End Class