Imports System.Data.SqlClient
Imports System.Collections
Public Class Work_Queue
    Inherits System.Web.UI.Page

    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"

    Private dsMatched As New DataSet
    Private dsUnMatched As New DataSet
    Private dsPostHeader As New DataSet
    Private dsBankData As New DataSet
    Private IsSearched As Boolean
    Private MatchedNumber As String
    Private UnMatchedNumber As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tblBank As New DataTable
        Dim brow As DataRow
        Try
            If Not IsPostBack Then
                lnkWQSelectedQueue.Text = Session("CurrentWorkQueue")
                lblWQClient.Text = Session("CurrentClientName")
                lblWQSelectedQueue.Text = Session("CurrentWorkQueue")
                lblSelectedDate.Text = Session("CurrentBankDate")


                tblBank.Columns.Add("Deposit_Date")
                tblBank.Columns.Add("Amount")
                tblBank.Columns.Add("Check_Number")
                tblBank.Columns.Add("Variance")
                tblBank.Columns.Add("PayerName")
                brow = tblBank.NewRow
                brow("Amount") = ""

                tblBank.Rows.Add(brow)

                grdBank.DataSource = tblBank
                grdBank.DataBind()

                Dim tblPosted As New DataTable
                Dim prow As DataRow

                tblPosted.Columns.Add("Bank_Deposit_Date")
                tblPosted.Columns.Add("Amount")
                tblPosted.Columns.Add("Check_Number")

                prow = tblPosted.NewRow
                prow("Amount") = ""

                tblPosted.Rows.Add(prow)

                grdPostedHeader.DataSource = tblPosted
                grdPostedHeader.DataBind()

                Dim tblmatched As New DataTable
                Dim mrow As DataRow
                Dim cm As New DataColumn
                cm.DataType = System.Type.GetType("System.Boolean")
                cm.ColumnName = "matched"
                tblmatched.Columns.Add(cm)
                tblmatched.Columns.Add("number")
                tblmatched.Columns.Add("BatchID")
                tblmatched.Columns.Add("invoice")
                tblmatched.Columns.Add("Bank_Dept_Dt")
                tblmatched.Columns.Add("Closing_Dt")
                tblmatched.Columns.Add("Create_Dt")
                tblmatched.Columns.Add("Designation")
                tblmatched.Columns.Add("Type")
                tblmatched.Columns.Add("Pay_Amt")
                tblmatched.Columns.Add("Pay_Code")
                tblmatched.Columns.Add("Posted_Dt")
                tblmatched.Columns.Add("Check")


                For x = 1 To 5
                    mrow = tblmatched.NewRow
                    mrow("matched") = 1
                    tblmatched.Rows.Add(mrow)
                Next

                grdMatched.DataSource = tblmatched
                grdMatched.DataBind()

                tblmatched.Rows.Clear()
                For x = 1 To 5
                    mrow = tblmatched.NewRow
                    mrow("matched") = 0
                    tblmatched.Rows.Add(mrow)
                Next

                grdUnmatched.DataSource = tblmatched
                grdUnmatched.DataBind()

                If Session("CurrentWorkQueue") = "Holding" Then
                    Mat_Sea_Div.Visible = False
                    Mat_Div.Visible = False
                    Unmat_Sea_Div.Visible = False
                    Unmat_Div.Visible = False
                    lblPartiallymatched.Visible = False
                    lblUnmatched.Visible = False
                    lnkUpdate.Visible = False
                    PopulateBankDataGrid_Holding()
                Else
                    Mat_Sea_Div.Visible = True
                    Mat_Div.Visible = True
                    Unmat_Sea_Div.Visible = True
                    Unmat_Div.Visible = True
                    lblPartiallymatched.Visible = True
                    lblUnmatched.Visible = True
                    lnkUpdate.Visible = True
                    PopulateBankDataGrid()
                    PopulatePostedDataGrid()
                    PopulateMatchedGrid()
                    PopulateUnmatchedGrid()
                    MatNum.Visible = False
                    ControlsVisible(False)
                End If
            End If
            EnableSelectAll()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
        
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkUpdate.Click
        Dim dsPostedID As DataSet
        Dim strvalue As String = ""
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        Try
            'If Session("UnMatchedNumber") = Nothing Then
            '    UpdatedUnmatchedRecord()
            'Else
            '    UnMatchedNumber = Session("UnMatchedNumber")
            'End If
            'If Session("MatchedNumber") = Nothing Then
            '    UpdatedMatchedGridRecord()
            'Else
            '    MatchedNumber = Session("MatchedNumber")
            'End If
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            UpdatedUnmatchedRecord(CurrentClient)
            UpdatedMatchedGridRecord(CurrentClient)

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            If MatchedNumber <> "" And UnMatchedNumber <> "" Then
                strvalue = MatchedNumber & "," & UnMatchedNumber
            ElseIf MatchedNumber <> "" And UnMatchedNumber = "" Then
                strvalue = MatchedNumber
            ElseIf MatchedNumber = "" And UnMatchedNumber <> "" Then
                strvalue = UnMatchedNumber
            End If
            dsPostedID = UpdatePostedDataMatched(SQLcon, Session("BankRecordID"), Session("PostHeaderID"), strvalue, CurrentClient)
            SQLcon.Close()
            If dsPostedID.Tables(0).Rows(0)("PostedDataHeaderID") <> "" Then
                Session("PostHeaderID") = dsPostedID.Tables(0).Rows(0)("PostedDataHeaderID")
            Else
                Session("PostHeaderID") = ""
            End If
            RefereshGrid()
        Catch ex As Exception
            SQLcon.Close()
            ex.ToString()
        End Try
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
    Private Sub UpdatedMatchedGridRecord(ByVal CurrentClient As Integer)
        Dim Number As String = ""
        Try
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            For Each row As GridViewRow In grdMatched.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkmatched"), CheckBox)
                    If chkRow.Checked Then
                        Number = TryCast(row.Cells(1).FindControl("lblMatNumber"), Label).Text
                        If MatchedNumber = "" Then
                            MatchedNumber = Number
                        Else
                            MatchedNumber = MatchedNumber & "," & Number
                        End If
                    Else
                        Number = TryCast(row.Cells(1).FindControl("lblMatNumber"), Label).Text
                        UpdatePostedDataMatandUnmat(SQLcon, Number, 2, CurrentClient)
                    End If
                End If
            Next
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub UpdatedUnmatchedRecord(ByVal CurrentClient As Integer)
        Dim Number As String = ""
        Try
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            For Each row As GridViewRow In grdUnmatched.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkUnmatched"), CheckBox)
                    If chkRow.Checked Then
                        Number = TryCast(row.Cells(1).FindControl("lblUnmatchNumber"), Label).Text
                        If UnMatchedNumber = "" Then
                            UnMatchedNumber = Number
                        Else
                            UnMatchedNumber = UnMatchedNumber & "," & Number
                        End If

                    Else
                        Number = TryCast(row.Cells(1).FindControl("lblUnmatchNumber"), Label).Text
                        UpdatePostedDataMatandUnmat(SQLcon, Number, 2, CurrentClient)
                    End If
                End If
            Next
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub grdUnmatched_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUnmatched.RowDataBound

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
    Private Sub PopulateMatchedGrid()
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsMatched = LoadPostedData(SQLcon, Session("CurrentBankDate"), Session("CurrentCheckNum"), Session("CurrentWorkQueue"), 3, CurrentClient)
            SQLcon.Close()
            grdMatched.DataSource = dsMatched
            grdMatched.DataBind()
            If grdMatched.Rows.Count > 0 Then
                lblMatchedCount.Text = dsMatched.Tables(0).Rows.Count & " Records"
            Else
                lblMatchedCount.Text = "No Matched records for this grouping."
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub PopulateUnmatchedGrid()
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsUnMatched = LoadPostedData(SQLcon, Session("CurrentBankDate"), Session("CurrentCheckNum"), Session("CurrentWorkQueue"), 2, CurrentClient)
            SQLcon.Close()
            grdUnmatched.DataSource = dsUnMatched
            grdUnmatched.DataBind()
            If grdUnmatched.Rows.Count > 0 Then
                'lblUnmatchedcount.Text = grdUnmatched.Rows.Count & " Records"
                lblUnmatchedcount.Text = dsUnMatched.Tables(0).Rows.Count & " Records"

            Else
                lblUnmatchedcount.Text = "No Unmatched records found."
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub UnMatchedEdit(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        grdUnmatched.EditIndex = e.NewEditIndex

        SearchUnMatchedGrid()
    End Sub
    Protected Sub UnMatchedCancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        grdUnmatched.EditIndex = -1
        SearchUnMatchedGrid()
    End Sub
    Protected Sub UnMatchedUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim Number As String = DirectCast(grdUnmatched.Rows(e.RowIndex).FindControl("lblUnmatchNumber"), Label).Text
            Dim BankDeptDt As Date = DirectCast(grdUnmatched.Rows(e.RowIndex).FindControl("txtUnMatchBankDeptDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdUnmatched.Rows(e.RowIndex).FindControl("txtUnMatchDesignation"), TextBox).Text
            Dim Type As String = DirectCast(grdUnmatched.Rows(e.RowIndex).FindControl("txtUnMatchType"), TextBox).Text
            'Dim PayAmt As Double = DirectCast(grdUnmatched.Rows(e.RowIndex).FindControl("txtUnMatchPayAmt"), TextBox).Text
            Dim Check As String = DirectCast(grdUnmatched.Rows(e.RowIndex).FindControl("txtUnMatchCheck"), TextBox).Text
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            'UpdatePostData(SQLcon, Number, BankDeptDt, Designation, Type, PayAmt, Check, Session("User"), Now.Date, CurrentClient)    'Raja comment:Amount not editable.
            UpdatePostData(SQLcon, Number, BankDeptDt, Designation, Type, Check, Session("User"), Now.Date, CurrentClient)
            SQLcon.Close()
            grdUnmatched.EditIndex = -1
            'PopulateUnmatchedGrid()                    'Raja Modified :Call Filter functionality after update. 
            SearchUnMatchedGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub
    Protected Sub UnMatchedOnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        SaveCheckedStates_Unmatched()
        grdUnmatched.PageIndex = e.NewPageIndex
        PopulateUnmatchedGrid()
        PopulateCheckedStates_Unmatched()
    End Sub
    Private Sub SaveCheckedStates_Unmatched()
        Dim objAL As New ArrayList()
        Dim rowIndex As Integer = -1
        For Each row As GridViewRow In grdUnmatched.Rows
            rowIndex = Convert.ToInt32(grdUnmatched.DataKeys(row.RowIndex).Value)
            Dim isSelected As Boolean = CType(row.FindControl("chkUnmatched"), CheckBox).Checked
            If ViewState("SELECTED_UNMATCHED_ROWS") IsNot Nothing Then
                objAL = CType(ViewState("SELECTED_UNMATCHED_ROWS"), ArrayList)
            End If
            If isSelected Then
                If Not objAL.Contains(rowIndex) Then
                    objAL.Add(rowIndex)
                End If
            Else
                objAL.Remove(rowIndex)
            End If
        Next row
        If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
            ViewState("SELECTED_UNMATCHED_ROWS") = objAL
        End If
    End Sub

    'Populate the saved checked checkbox status
    Private Sub PopulateCheckedStates_Unmatched()

        Dim objAL As ArrayList = CType(ViewState("SELECTED_UNMATCHED_ROWS"), ArrayList)
        If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
            For Each row As GridViewRow In grdUnmatched.Rows
                Dim rowIndex As Integer = Convert.ToInt32(grdUnmatched.DataKeys(row.RowIndex).Value)
                If objAL.Contains(rowIndex) Then
                    Dim chkSelect As CheckBox = CType(row.FindControl("chkUnmatched"), CheckBox)
                    chkSelect.Checked = True
                    row.Attributes.Add("class", "selected")
                    'If UnMatchedNumber = "" Then
                    '    UnMatchedNumber = rowIndex.ToString()
                    'Else
                    '    UnMatchedNumber = UnMatchedNumber + "," + rowIndex.ToString()
                    'End If

                End If
            Next row
        End If

    End Sub
    Protected Sub AddUnMatched(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim Invoice As Integer = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchInvoice"), TextBox).Text
            Dim BatchID As Integer = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchBatchID"), TextBox).Text
            Dim BankDeptDt As Date = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchBankDeptDt"), TextBox).Text
            Dim ClosingDt As Date = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchClosingDt"), TextBox).Text
            Dim CreateDt As Date = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchCreateDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdUnmatched.FooterRow.FindControl("dpUnMatchDesignation"), DropDownList).Text
            Dim Type As String = DirectCast(grdUnmatched.FooterRow.FindControl("dpUnMatchType"), DropDownList).Text
            Dim PayAmt As Double = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchPayAmt"), TextBox).Text
            Dim Paycode As String = DirectCast(grdUnmatched.FooterRow.FindControl("dpUnMatchPayCode"), DropDownList).Text
            Dim PostedDt As Integer = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchPostedDt"), TextBox).Text
            Dim Check As String = DirectCast(grdUnmatched.FooterRow.FindControl("txtUnMatchCheck"), TextBox).Text

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            InsertPostData(SQLcon, BatchID, Invoice, BankDeptDt, ClosingDt, CreateDt, Designation, Type, PayAmt, Paycode, PostedDt, Check, Session("User"), _
                           Now.Date, CurrentClient)
            SQLcon.Close()
            'MsgBox("Record Inserted Successfully")
            grdUnmatched.EditIndex = -1
            PopulateUnmatchedGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub
    Private Sub PopulatePostedDataGrid()
        Dim ChkNum As String
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            If Session("CurrentCheckNum") = "" Then
                ChkNum = Session("PostHeaderID")
            Else
                ChkNum = Session("CurrentCheckNum")
            End If
            dsPostHeader = LoadPostedHeaderData(SQLcon, Session("CurrentBankDate"), ChkNum, CurrentClient)
            SQLcon.Close()
            grdPostedHeader.DataSource = dsPostHeader
            grdPostedHeader.DataBind()
            If grdPostedHeader.Rows.Count > 0 Then
                'lblPostDateCount.Text = grdPostedHeader.Rows.Count & " Records"
                lblPostDateCount.Text = dsPostHeader.Tables(0).Rows.Count & " Records"
            Else
                lblPostDateCount.Text = "No Post Data Header records."
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub PopulateBankDataGrid()
        Dim ChkNum As String
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            If Session("CurrentCheckNum") = "" Then
                ChkNum = Session("BankRecordID")
            Else
                ChkNum = Session("CurrentCheckNum")
            End If
            dsBankData = LoadBankData(SQLcon, Session("CurrentBankDate"), ChkNum, CurrentClient, Session("CurrentWorkQueue"))                 'Newely added ClientID
            SQLcon.Close()
            grdBank.DataSource = dsBankData
            grdBank.DataBind()
            If grdBank.Rows.Count > 0 Then
                'lblBankRecordCount.Text = grdBank.Rows.Count & " Records"
                lblBankRecordCount.Text = dsBankData.Tables(0).Rows.Count & " Records"
                If Session("CurrentWorkQueue") = "CreditCards" Then
                    lnkNewBank.Visible = True
                Else
                    lnkNewBank.Visible = False
                End If
                TxtHoldComments.Text = If(IsDBNull(dsBankData.Tables(0).Rows(0)("Comments")), String.Empty, dsBankData.Tables(0).Rows(0)("Comments"))   'Raja added Comments.
            Else
                lblBankRecordCount.Text = "No Bank file records."
                lnkNewBank.Visible = True
            End If
            If Session("CurrentCheckNum") <> "" Then
                grdBank.Columns(0).Visible = False
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkRefresh_Click(sender As Object, e As EventArgs) Handles lnkRefresh.Click
        RefereshGrid()
    End Sub
    Private Sub RefereshGrid()
        PopulateBankDataGrid()
        PopulatePostedDataGrid()
        PopulateMatchedGrid()
        PopulateUnmatchedGrid()
    End Sub
    Private Sub lnkNewBank_Click(sender As Object, e As System.EventArgs) Handles lnkNewBank.Click
        'Response.Redirect("BankDataInsert.aspx")
        Server.Transfer("BankDataInsert.aspx")
    End Sub

    Private Sub Lnkhold_Click(sender As Object, e As System.EventArgs) Handles Lnkhold.Click
        Dim chkNum As String
        Dim interval As Integer = 0
        Try
            If txtHolddaysno.Text = "" Then
                txtHolddaysno.Text = 0
                interval = txtHolddaysno.Text
            Else
                interval = txtHolddaysno.Text
            End If

            If Session("CurrentCheckNum") = "" Then
                chkNum = Session("BankRecordID")
            Else
                chkNum = Session("CurrentCheckNum")
            End If
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            UpdateBankDataHolding(SQLcon, Session("BankRecordID"), interval, TxtHoldComments.Text.Trim, Session("User"), CurrentClient)
            SQLcon.Close()
            'Response.Redirect("Variance.aspx")
            Server.Transfer("Variance.aspx")

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub SearchUnMatchedGrid()

        Dim dsSearch As DataSet
        Dim Number As String = TxtSeaUnmatNum.Text
        Dim BatchID As String = TxtSeaBatchID.Text
        Dim Invoice As String = TxtSeaUnMatInv.Text
        Dim Bank_Dept_Dt As String = TxtSeaUnMatBDD.Text
        Dim Closing_Dt As String = TxtSeaUnMatCD.Text
        Dim Create_Dt As String = TxtSeaUnMatCDt.Text
        Dim Designation As String = TxtSeaUnMatDes.Text
        Dim Type As String = TxtSeaUnMatType.Text
        Dim Pay_Amt As String = TxtSeaUnMatPayAmt.Text
        Dim Pay_Code As String = TxtSeaUnMatPayCode.Text
        Dim Posted_Dt As String = TxtSeaUnMatPD.Text
        Dim Check As String = TxtSeaUnmatChk.Text

        IsSearched = True

        chkWQSelectAll.Checked = False
        lblSaveStatus.Text = String.Empty
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            If (Number <> "" Or BatchID <> "" Or Invoice <> "" Or Bank_Dept_Dt <> "" Or Closing_Dt <> "" Or Create_Dt <> "" Or Designation <> "" Or Type <> "" Or Pay_Amt <> "" Or Pay_Code <> "" Or
                                                                                                    Posted_Dt <> "" Or Check <> "") Then
                dsSearch = LoadPostedDataSearch(SQLcon, Number, BatchID, Invoice, Bank_Dept_Dt, Closing_Dt, Create_Dt, Designation, Type, Pay_Amt, Pay_Code, _
                                                Posted_Dt, Check, 2, String.Empty, String.Empty, CurrentClient)
            Else
                dsSearch = LoadPostedData(SQLcon, Session("CurrentBankDate"), Session("CurrentCheckNum"), Session("CurrentWorkQueue"), 2, CurrentClient)
            End If

            SQLcon.Close()
            grdUnmatched.DataSource = dsSearch
            grdUnmatched.DataBind()
            If grdUnmatched.Rows.Count > 0 Then
                lblUnmatchedcount.Text = grdUnmatched.Rows.Count & " Records"
            Else
                lblUnmatchedcount.Text = "No Unmatched records."
                chkWQSelectAll.Enabled = False
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub

    Private Sub TxtSeaUnMatBDD_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatBDD.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatCD_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatCD.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatCDt_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatCDt.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnmatChk_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnmatChk.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatDes_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatDes.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatInv_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatInv.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnmatNum_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnmatNum.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatPayAmt_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatPayAmt.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatPayCode_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatPayCode.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatPD_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatPD.TextChanged
        SearchUnMatchedGrid()
    End Sub

    Private Sub TxtSeaUnMatType_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaUnMatType.TextChanged
        SearchUnMatchedGrid()
    End Sub
    Private Sub TxtSeaBatchID_TextChanged(sender As Object, e As System.EventArgs) Handles TxtSeaBatchID.TextChanged
        SearchUnMatchedGrid()
    End Sub
    Protected Sub BankDataEdit(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        grdBank.EditIndex = e.NewEditIndex
        PopulateBankDataGrid()
        'PopulateUnmatchedGrid()
    End Sub
    Protected Sub BankdataCancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        grdBank.EditIndex = -1
        PopulateBankDataGrid()
    End Sub
    Protected Sub BankDataUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim Check_number As String = DirectCast(grdBank.Rows(e.RowIndex).FindControl("txtBankCheckNumber"), TextBox).Text
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            UpdateBankDataRecord(SQLcon, Session("BankRecordID"), Check_number, CurrentClient)
            SQLcon.Close()
            'MsgBox("Bank Data Updated Successfully")
            grdBank.EditIndex = -1
            PopulateBankDataGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub


    
    Private Sub txtMatsearchDepositDate_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatsearchDepositDate.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtmatsearchBatchId_TextChanged(sender As Object, e As System.EventArgs) Handles txtmatsearchBatchId.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatsearchCheck_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatsearchCheck.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatsearchClosingDt_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatsearchClosingDt.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatSearchCreateDt_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatSearchCreateDt.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatsearchDesignation_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatsearchDesignation.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtmatsearchInvoice_TextChanged(sender As Object, e As System.EventArgs) Handles txtmatsearchInvoice.TextChanged
        SearchMatchedGrid()
    End Sub
    Private Sub TxtMatsearchPayamt_TextChanged(sender As Object, e As System.EventArgs) Handles TxtMatsearchPayamt.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatSearchPayCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatSearchPayCode.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatSearchPosteddt_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatSearchPosteddt.TextChanged
        SearchMatchedGrid()
    End Sub

    Private Sub txtMatSearchType_TextChanged(sender As Object, e As System.EventArgs) Handles txtMatSearchType.TextChanged
        SearchMatchedGrid()
    End Sub
    Private Sub SearchMatchedGrid()

        Dim dsSearch As DataSet
        Dim Number As String = ""
        Dim BatchID As String = txtmatsearchBatchId.Text
        Dim Invoice As String = txtmatsearchInvoice.Text
        Dim Bank_Dept_Dt As String = txtMatsearchDepositDate.Text
        Dim Closing_Dt As String = txtMatsearchClosingDt.Text
        Dim Create_Dt As String = txtMatsearchClosingDt.Text
        Dim Designation As String = txtMatsearchDesignation.Text
        Dim Type As String = txtMatSearchType.Text
        Dim Pay_Amt As String = TxtMatsearchPayamt.Text
        Dim Pay_Code As String = txtMatSearchPayCode.Text
        Dim Posted_Dt As String = txtMatSearchPosteddt.Text
        Dim Check As String = txtMatsearchCheck.Text
        Dim strOriginalType As String = String.Empty
        Dim strOriginalDeptDate As String = String.Empty
        Try
            'If txtMatsearchDepositDate.Text <> "" Then
            '    Bank_Dept_Dt = txtMatsearchDepositDate.Text
            ''Else
            ''    Bank_Dept_Dt = Session("CurrentBankDate")
            'End If

            'If txtMatSearchType.Text <> "" Then
            '    Type = txtMatSearchType.Text
            '    'Else
            '    '    Type = Session("CurrentWorkQueue")
            'End If
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            If (Number <> "" Or BatchID <> "" Or Invoice <> "" Or Bank_Dept_Dt <> "" Or Closing_Dt <> "" Or Create_Dt <> "" Or Designation <> "" Or Type <> "" Or Pay_Amt <> "" Or Pay_Code <> "" Or
                                                                                                    Posted_Dt <> "" Or Check <> "") Then
                'If txtMatsearchDepositDate.Text = "" Then
                '    Bank_Dept_Dt = Session("CurrentBankDate")
                'End If
                'If txtMatSearchType.Text = "" Then
                'End If
                If If(IsDBNull(Session("CurrentWorkQueue")), String.Empty, Session("CurrentWorkQueue")) <> "EFT" Then
                    strOriginalType = If(IsDBNull(Session("CurrentWorkQueue")), String.Empty, Session("CurrentWorkQueue"))
                    strOriginalDeptDate = If(IsDBNull(Session("CurrentBankDate")), String.Empty, Session("CurrentBankDate"))
                End If
                dsSearch = LoadPostedDataSearch(SQLcon, Number, BatchID, Invoice, Bank_Dept_Dt, Closing_Dt, Create_Dt, Designation, Type, Pay_Amt, Pay_Code, _
                                                Posted_Dt, Check, 3, strOriginalType, strOriginalDeptDate, CurrentClient)
            Else
                dsSearch = LoadPostedData(SQLcon, Session("CurrentBankDate"), Session("CurrentCheckNum"), Session("CurrentWorkQueue"), 3, CurrentClient)
            End If

            SQLcon.Close()
            grdMatched.DataSource = dsSearch
            grdMatched.DataBind()
            If grdMatched.Rows.Count > 0 Then
                lblMatchedCount.Text = grdMatched.Rows.Count & " Records"
            Else
                lblMatchedCount.Text = "No Unmatched records."
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub PopulateBankDataGrid_Holding()
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            grdBank.Columns(5).Visible = False
            dsBankData = LoadBankDataHolding_BDD(SQLcon, Session("BankRecordID"), CurrentClient)
            SQLcon.Close()
            grdBank.DataSource = dsBankData
            grdBank.DataBind()

            If grdBank.Rows.Count > 0 Then
                lblBankRecordCount.Text = grdBank.Rows.Count & " Records"
                If Session("CurrentWorkQueue") = "CreditCards" Then
                    lnkNewBank.Visible = True
                Else
                    lnkNewBank.Visible = False
                End If
                txtHolddaysno.Text = If(IsDBNull(dsBankData.Tables(0).Rows(0)("NoofDays")), String.Empty, dsBankData.Tables(0).Rows(0)("NoofDays"))
                TxtHoldComments.Text = If(IsDBNull(dsBankData.Tables(0).Rows(0)("Comments")), String.Empty, dsBankData.Tables(0).Rows(0)("Comments"))
            Else
                lblBankRecordCount.Text = "No Bank file records."
                lnkNewBank.Visible = True
            End If

            If Session("CurrentCheckNum") <> "" Then
                grdBank.Columns(0).Visible = False
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub MatchedEdit(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        grdMatched.EditIndex = e.NewEditIndex
        SearchMatchedGrid()
    End Sub
    Protected Sub MatchedCancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        grdMatched.EditIndex = -1
        SearchMatchedGrid()
    End Sub
    Protected Sub MatchedUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim Number As String = DirectCast(grdMatched.Rows(e.RowIndex).FindControl("lblMatNumber"), Label).Text
            Dim BankDeptDt As Date = DirectCast(grdMatched.Rows(e.RowIndex).FindControl("txtMatBankDeptDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdMatched.Rows(e.RowIndex).FindControl("txtMatDesignation"), TextBox).Text
            Dim Type As String = DirectCast(grdMatched.Rows(e.RowIndex).FindControl("txtMatType"), TextBox).Text
            'Dim PayAmt As Double = DirectCast(grdMatched.Rows(e.RowIndex).FindControl("txtMatPayAmt"), TextBox).Text    'Raja Comment :Amount not Editable. 
            Dim Check As String = DirectCast(grdMatched.Rows(e.RowIndex).FindControl("txtMatCheck"), TextBox).Text
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            'UpdatePostData(SQLcon, Number, BankDeptDt, Designation, Type, PayAmt, Check, Session("User"), Now.Date, CurrentClient)
            UpdatePostData(SQLcon, Number, BankDeptDt, Designation, Type, Check, Session("User"), Now.Date, CurrentClient)

            SQLcon.Close()

            grdMatched.EditIndex = -1
            'PopulateMatchedGrid()                          'Raja Modified :Call filter functionality after update
            SearchMatchedGrid()
        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub
    Protected Sub MatchedOnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        SaveCheckedStates_matched()
        grdMatched.PageIndex = e.NewPageIndex
        PopulateMatchedGrid()
        PopulateCheckedStates_matched()
    End Sub
    Private Sub SaveCheckedStates_matched()
        Dim objAL As New ArrayList()
        Dim rowIndex As Integer = -1
        For Each row As GridViewRow In grdMatched.Rows
            rowIndex = Convert.ToInt32(grdMatched.DataKeys(row.RowIndex).Value)
            Dim isSelected As Boolean = CType(row.FindControl("chkmatched"), CheckBox).Checked
            If ViewState("SELECTED_MATCHED_ROWS") IsNot Nothing Then
                objAL = CType(ViewState("SELECTED_MATCHED_ROWS"), ArrayList)
            End If
            If isSelected Then
                If Not objAL.Contains(rowIndex) Then
                    objAL.Add(rowIndex)
                End If
            Else
                objAL.Remove(rowIndex)
            End If
        Next row
        If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
            ViewState("SELECTED_MATCHED_ROWS") = objAL
        End If
    End Sub

    'Populate the saved checked checkbox status
    Private Sub PopulateCheckedStates_matched()
        'MatchedNumber = ""
        Dim objAL As ArrayList = CType(ViewState("SELECTED_MATCHED_ROWS"), ArrayList)
        If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
            For Each row As GridViewRow In grdMatched.Rows
                Dim rowIndex As Integer = Convert.ToInt32(grdMatched.DataKeys(row.RowIndex).Value)
                If objAL.Contains(rowIndex) Then
                    Dim chkSelect As CheckBox = CType(row.FindControl("chkmatched"), CheckBox)
                    chkSelect.Checked = True
                    row.Attributes.Add("class", "selected")
                    'If MatchedNumber = "" Then
                    '    MatchedNumber = rowIndex.ToString()
                    'Else
                    '    MatchedNumber = MatchedNumber + "," + rowIndex.ToString()
                    'End If

                End If
            Next row
        End If
        'Session("MatchedNumber") = MatchedNumber
    End Sub

    Protected Sub btnMatchClear_Click(sender As Object, e As EventArgs) Handles btnMatchClear.Click
        txtmatsearchBatchId.Text = ""
        txtmatsearchInvoice.Text = ""
        txtMatsearchDepositDate.Text = ""
        txtMatsearchClosingDt.Text = ""
        txtMatsearchClosingDt.Text = ""
        txtMatsearchDesignation.Text = ""
        txtMatSearchType.Text = ""
        TxtMatsearchPayamt.Text = ""
        txtMatSearchPayCode.Text = ""
        txtMatSearchPosteddt.Text = ""
        txtMatsearchCheck.Text = ""
        PopulateMatchedGrid()
    End Sub

    Private Sub btnUnmatchClear_Click(sender As Object, e As System.EventArgs) Handles btnUnmatchClear.Click
        TxtSeaUnmatNum.Text = ""
        TxtSeaBatchID.Text = ""
        TxtSeaUnMatInv.Text = ""
        TxtSeaUnMatBDD.Text = ""
        TxtSeaUnMatCD.Text = ""
        TxtSeaUnMatCDt.Text = ""
        TxtSeaUnMatDes.Text = ""
        TxtSeaUnMatType.Text = ""
        TxtSeaUnMatPayAmt.Text = ""
        TxtSeaUnMatPayCode.Text = ""
        TxtSeaUnMatPD.Text = ""
        TxtSeaUnmatChk.Text = ""
        PopulateUnmatchedGrid()
    End Sub

    Protected Sub btnUpdateInfo_Click(sender As Object, e As EventArgs) Handles btnUpdateInfo.Click
        Dim StrUnmatchedNumber As String = String.Empty
        If grdUnmatched.Rows.Count <> 0 Then
            If btnUpdateInfo.Text = "Update Info" Then
                btnUpdateInfo.Text = "Save"
                ControlsVisible(True)
                TxtCheck.Text = String.Empty
                TxtDepositDate.Text = String.Empty
                TxtType.Text = String.Empty
                lblSaveStatus.Text = String.Empty
            Else
                StrUnmatchedNumber = GetUnMatchedRecords()
                If StrUnmatchedNumber = String.Empty Then
                    Exit Sub
                Else
                    UpdateInformations(StrUnmatchedNumber)
                    btnUpdateInfo.Text = "Update Info"
                    ControlsVisible(False)
                    'PopulateUnmatchedGrid()
                    SearchUnMatchedGrid()
                    lblSaveStatus.Text = "Record Updated Sucessfully"
                End If
            End If
        Else
            btnUpdateInfo.Visible = False
        End If
    End Sub
    Private Sub ControlsVisible(ByVal blnVisible As Boolean)
        lblCheck.Visible = blnVisible
        lblDepositDate.Visible = blnVisible
        lblType.Visible = blnVisible
        TxtCheck.Visible = blnVisible
        TxtDepositDate.Visible = blnVisible
        TxtType.Visible = blnVisible
        'lblSaveStatus.Visible = blnVisible
    End Sub

    Private Sub UpdateInformations(ByVal strUnmatchedNumber As String)
        Dim strCheck, strDepositeDate, strType As String


        If TxtCheck.Text <> String.Empty Then
            strCheck = TxtCheck.Text.Trim()
        Else
            strCheck = String.Empty
        End If

        If TxtDepositDate.Text <> String.Empty Then
            strDepositeDate = TxtDepositDate.Text.Trim()
        Else
            strDepositeDate = String.Empty
        End If

        If TxtType.Text <> String.Empty Then
            strType = TxtType.Text.Trim()
        Else
            strType = String.Empty
        End If
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            UpdateRecordInformations(SQLcon, strUnmatchedNumber, strCheck, strDepositeDate, strType, Session("User"), CurrentClient)
            SQLcon.Close()
        Catch ex As Exception
            lblSaveStatus.Text = ex.Message.Trim()
        End Try
    End Sub
    Private Function GetUnMatchedRecords() As String
        Dim strNumber As String = String.Empty
        Dim StrUnmatchedNumber As String = String.Empty
        Try
            For Each row As GridViewRow In grdUnmatched.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkUnmatched"), CheckBox)
                    If chkRow.Checked Then
                        strNumber = TryCast(row.Cells(1).FindControl("lblUnmatchNumber"), Label).Text
                        If StrUnmatchedNumber = "" Then
                            StrUnmatchedNumber = strNumber
                        Else
                            StrUnmatchedNumber = StrUnmatchedNumber & "," & strNumber
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            ex.Message.ToString()
        End Try
       
        Return StrUnmatchedNumber
    End Function

    Protected Sub chkWQSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkWQSelectAll.CheckedChanged
        Try
            For Each rowWQUnmatched As GridViewRow In grdUnmatched.Rows
                Dim ChkWQUnmatched As CheckBox = CType(rowWQUnmatched.FindControl("chkUnmatched"), CheckBox)
                If chkWQSelectAll.Checked = True Then
                    ChkWQUnmatched.Checked = True
                Else
                    ChkWQUnmatched.Checked = False
                End If
            Next
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub EnableSelectAll()
        Try
            If TxtSeaBatchID.Text <> String.Empty Or TxtSeaUnMatInv.Text <> String.Empty Or TxtSeaUnmatChk.Text <> String.Empty Or TxtSeaUnMatBDD.Text <> String.Empty Or TxtSeaUnMatCD.Text <> String.Empty _
                Or TxtSeaUnMatCDt.Text <> String.Empty Or TxtSeaUnMatDes.Text <> String.Empty Or TxtSeaUnMatType.Text <> String.Empty Or TxtSeaUnMatPayAmt.Text <> String.Empty Or TxtSeaUnMatPayCode.Text <> String.Empty _
                Or TxtSeaUnMatPD.Text <> String.Empty Then

                chkWQSelectAll.Enabled = True
            Else
                chkWQSelectAll.Enabled = False
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
End Class
