Public Class PostedDataDeletion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                mvposteddatadeletion.ActiveViewIndex = 0
                PDDLoadClients()
                PopulatePDDPostedRecordsGrid()
                Session("PDDRemindChecks") = Nothing
                lblPDDCurrentType.Text = "Lockbox"
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlPDDClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPDDClient.SelectedIndexChanged
        Try
            Session("PDDRemindChecks") = Nothing
            hfCount.Value = "0"
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub PDDLoadClients()
        Try
            Dim dsPDDClients As DataSet
            Dim li As ListItem
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            ddlPDDClient.Items.Clear()
            dsPDDClients = GetClients(SQLcon)
            For Each dr As DataRow In dsPDDClients.Tables(0).Rows
                li = New ListItem
                li.Value = dr("Master_Client_Number").ToString
                li.Text = dr("Client_Name").ToString
                ddlPDDClient.Items.Add(li)
            Next
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub PopulatePDDPostedRecordsGrid()
        Try
            Dim intCurrentClient As Integer = CInt(ddlPDDClient.SelectedValue)
            Dim strCurrentType As String = PDDGetCurrentType()
            Dim BatchID As String = txtPDDsearchBatchId.Text
            Dim Invoice As String = txtPDDsearchInvoice.Text
            Dim Closing_Dt As String = txtPDDsearchClosingDt.Text
            Dim Bank_Dept_Dt As String = txtPDDsearchDepositDate.Text
            Dim Create_Dt As String = txtPDDSearchCreateDate.Text
            Dim Designation As String = txtPDDsearchDesignation.Text
            Dim SearchType As String = txtPDDSearchType.Text
            Dim Pay_Amt As String = TxtPDDsearchPayamt.Text
            Dim Pay_Code As String = txtPDDSearchPayCode.Text
            Dim Posted_Dt As String = txtPDDSearchPosteddt.Text
            Dim Check As String = txtPDDsearchCheck.Text


            Dim dsUnmatchedPostRecords As New DataSet

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsUnmatchedPostRecords = GetUnmatched_PostedDataRecords_Deletion(SQLcon, intCurrentClient, strCurrentType, BatchID, Invoice, Bank_Dept_Dt, Closing_Dt, Create_Dt, Designation, _
                                                                     SearchType, Pay_Amt, Pay_Code, Posted_Dt, Check)
            SQLcon.Close()
            grdPostedDataDeletionLoad.DataSource = dsUnmatchedPostRecords
            grdPostedDataDeletionLoad.DataBind()

            If dsUnmatchedPostRecords.Tables(0).Rows.Count = 0 Then
                lnkPDDdelete.Visible = False
            Else
                lnkPDDdelete.Visible = True
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Function PDDGetCurrentType() As String
        Dim strType As String = String.Empty
        Try
            If rbPDDEFT.Checked = True Then
                strType = "EFT"
            ElseIf rbPDDCreditCard.Checked = True Then
                strType = "CREDIT CARD"
            ElseIf rbPDDOther.Checked = True Then
                strType = "OTHER"
            Else
                strType = "CHECK"
            End If
            Return strType
        Catch ex As Exception
            Return strType
            ex.Message.ToString()
        End Try
    End Function

    Protected Sub rbPDDLockbox_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDDLockbox.CheckedChanged
        Try
            rbPDDEFT.Checked = False
            rbPDDCreditCard.Checked = False
            rbPDDOther.Checked = False
            Session("PDDRemindChecks") = Nothing
            hfCount.Value = "0"
            lblPDDCurrentType.Text = "Lockbox"
            UnChkCheckbox()
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkPDDReturn_Click(sender As Object, e As EventArgs) Handles lnkPDDReturn.Click
        Try
            Server.Transfer("UserAdmin.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub


    Protected Sub PostedDataDeletionCancelEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdPostedDataDeletionLoad.RowCancelingEdit
        Try
            grdPostedDataDeletionLoad.EditIndex = -1
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PostedDataDeletionEdit(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdPostedDataDeletionLoad.RowEditing
        Try
            grdPostedDataDeletionLoad.EditIndex = e.NewEditIndex
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PostedDataDeletionUpdate(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdPostedDataDeletionLoad.RowUpdating
        Try
            Dim CurrentClient As Integer = CInt(ddlPDDClient.SelectedValue)
            Dim Posted_DataID As String = DirectCast(grdPostedDataDeletionLoad.Rows(e.RowIndex).FindControl("lblPDDPosted_DataID"), Label).Text
            Dim BankDeptDt As Date = DirectCast(grdPostedDataDeletionLoad.Rows(e.RowIndex).FindControl("txtPDDBankDeptDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdPostedDataDeletionLoad.Rows(e.RowIndex).FindControl("txtPDDDesignation"), TextBox).Text
            Dim Type As String = DirectCast(grdPostedDataDeletionLoad.Rows(e.RowIndex).FindControl("txtPDDType"), TextBox).Text
            Dim Check As String = DirectCast(grdPostedDataDeletionLoad.Rows(e.RowIndex).FindControl("txtPDDCheck"), TextBox).Text
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            Update_UnmatchedPostedRecords(SQLcon, Posted_DataID, BankDeptDt, Designation, Type, Check, Session("User"), CurrentClient)
            SQLcon.Close()
            grdPostedDataDeletionLoad.EditIndex = -1
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdPostedDataDeletionLoad_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPostedDataDeletionLoad.PageIndexChanging
        Try
            PDDSaveCheckedValues()
            grdPostedDataDeletionLoad.PageIndex = e.NewPageIndex
            PopulatePDDPostedRecordsGrid()
            PDDRestoreCheckedValues()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub SearchUnmatchedpostedRecords()
        PDDSaveCheckedValues()
        PopulatePDDPostedRecordsGrid()
        PDDRestoreCheckedValues()
    End Sub
    Private Sub PDDSaveCheckedValues()
        Dim chkList As New ArrayList()
        Try

            Dim index As Integer = -1
            For Each gvrow As GridViewRow In grdPostedDataDeletionLoad.Rows()
                index = CInt(grdPostedDataDeletionLoad.DataKeys(gvrow.RowIndex).Value)
                Dim result As Boolean = DirectCast(gvrow.FindControl("chkPostDataDeletion"), CheckBox).Checked

                If Session("PDDRemindChecks") IsNot Nothing Then
                    chkList = DirectCast(Session("PDDRemindChecks"), ArrayList)
                End If
                If result Then
                    If Not chkList.Contains(index) Then
                        chkList.Add(index)
                    End If
                Else
                    chkList.Remove(index)
                End If
            Next
            If chkList IsNot Nothing AndAlso chkList.Count > 0 Then
                Session("PDDRemindChecks") = chkList
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub PDDRestoreCheckedValues()
        Dim currentCount As Integer = 0
        Try
            Dim chkList As ArrayList = DirectCast(Session("PDDRemindChecks"), ArrayList)
            If chkList IsNot Nothing AndAlso chkList.Count > 0 Then
                For Each gvrow As GridViewRow In grdPostedDataDeletionLoad.Rows
                    Dim index As Integer = CInt(grdPostedDataDeletionLoad.DataKeys(gvrow.RowIndex).Value)
                    If chkList.Contains(index) Then
                        Dim myCheckBox As CheckBox = DirectCast(gvrow.FindControl("chkPostDataDeletion"), CheckBox)
                        myCheckBox.Checked = True
                        currentCount += 1
                    End If
                Next
            End If
            hfCount.Value = (chkList.Count - currentCount).ToString()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub rbPDDEFT_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDDEFT.CheckedChanged
        Try
            rbPDDLockbox.Checked = False
            rbPDDCreditCard.Checked = False
            rbPDDOther.Checked = False
            Session("PDDRemindChecks") = Nothing
            hfCount.Value = "0"
            lblPDDCurrentType.Text = "EFT"
            UnChkCheckbox()
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub rbPDDCreditCard_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDDCreditCard.CheckedChanged
        Try
            rbPDDEFT.Checked = False
            rbPDDLockbox.Checked = False
            rbPDDOther.Checked = False
            Session("PDDRemindChecks") = Nothing
            hfCount.Value = "0"
            lblPDDCurrentType.Text = "CreditCard"
            UnChkCheckbox()
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub rbPDDOther_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDDOther.CheckedChanged
        Try
            rbPDDEFT.Checked = False
            rbPDDCreditCard.Checked = False
            rbPDDLockbox.Checked = False
            Session("PDDRemindChecks") = Nothing
            hfCount.Value = "0"
            lblPDDCurrentType.Text = "Other"
            UnChkCheckbox()
            PopulatePDDPostedRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtPDDsearchBatchId_TextChanged(sender As Object, e As EventArgs) Handles txtPDDsearchBatchId.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDsearchInvoice_TextChanged(sender As Object, e As EventArgs) Handles txtPDDsearchInvoice.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDsearchCheck_TextChanged(sender As Object, e As EventArgs) Handles txtPDDsearchCheck.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDsearchClosingDt_TextChanged(sender As Object, e As EventArgs) Handles txtPDDsearchClosingDt.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDsearchDepositDate_TextChanged(sender As Object, e As EventArgs) Handles txtPDDsearchDepositDate.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub
    Protected Sub txtPDDsearchDesignation_TextChanged(sender As Object, e As EventArgs) Handles txtPDDsearchDesignation.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDSearchType_TextChanged(sender As Object, e As EventArgs) Handles txtPDDSearchType.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub TxtPDDsearchPayamt_TextChanged(sender As Object, e As EventArgs) Handles TxtPDDsearchPayamt.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDSearchPayCode_TextChanged(sender As Object, e As EventArgs) Handles txtPDDSearchPayCode.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub txtPDDSearchPosteddt_TextChanged(sender As Object, e As EventArgs) Handles txtPDDSearchPosteddt.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub

    Protected Sub lnkPDDdelete_Click(sender As Object, e As EventArgs) Handles lnkPDDdelete.Click
        Dim strPosted_DataID As String = String.Empty
        Dim strmessage As String = String.Empty

        Try
            Dim confirmValue As String = Request.Form("confirm_value")

            If confirmValue = "Yes" Then
                PDDSaveCheckedValues()

                Dim arr_PostedData As ArrayList = DirectCast(Session("PDDRemindChecks"), ArrayList)

                If Not arr_PostedData Is Nothing Then
                    For Each intPosteddata As Integer In arr_PostedData
                        If strPosted_DataID = String.Empty Then
                            strPosted_DataID = intPosteddata.ToString()
                        Else
                            strPosted_DataID = strPosted_DataID + "," + intPosteddata.ToString()
                        End If
                    Next

                    If strPosted_DataID <> String.Empty Then

                        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
                        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
                        DeleteUnmatchedPostedRecords(SQLcon, strPosted_DataID, Session("user"))
                        SQLcon.Close()
                        PopulatePDDPostedRecordsGrid()
                        ShowMessage("Records deleted sucessfully!")
                    End If
                End If
                Session("PDDRemindChecks") = Nothing
                hfCount.Value = "0"
                UnChkCheckbox()
            End If
           
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub chkPDDSelectAll_CheckedChanged(sender As Object, e As EventArgs)

        Try
            Dim chkSelectAll As CheckBox = DirectCast(grdPostedDataDeletionLoad.HeaderRow.FindControl("chkPDDSelectAll"), CheckBox)
            For Each rowDataManagment As GridViewRow In grdPostedDataDeletionLoad.Rows
                Dim ChkPostDataDeletionRecords As CheckBox = CType(rowDataManagment.FindControl("chkPostDataDeletion"), CheckBox)
                If chkSelectAll.Checked = True Then
                    ChkPostDataDeletionRecords.Checked = True
                Else
                    ChkPostDataDeletionRecords.Checked = False
                End If

            Next

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtPDDSearchCreateDate_TextChanged(sender As Object, e As EventArgs) Handles txtPDDSearchCreateDate.TextChanged
        SearchUnmatchedpostedRecords()
    End Sub
    Private Sub ShowMessage(ByVal strmessage As String)
        Dim sb As New System.Text.StringBuilder()
        Try
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.onload=function(){")
            sb.Append("alert('")
            sb.Append(strmessage + "')};")
            sb.Append("</script>")
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "Alert", sb.ToString())
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub chkPDDGridSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkPDDGridSelectAll.CheckedChanged
        Try
            Dim intPagecount As Integer = grdPostedDataDeletionLoad.PageIndex
            'Loop through All Pages
            For intcount As Integer = 0 To grdPostedDataDeletionLoad.PageCount - 1
                'Set Page Index
                grdPostedDataDeletionLoad.SetPageIndex(intcount)
                For Each rowDataManagment As GridViewRow In grdPostedDataDeletionLoad.Rows
                    Dim ChkPostDataDeletionRecords As CheckBox = CType(rowDataManagment.FindControl("chkPostDataDeletion"), CheckBox)
                    If chkPDDGridSelectAll.Checked = True Then
                        ChkPostDataDeletionRecords.Checked = True
                    Else
                        ChkPostDataDeletionRecords.Checked = False
                    End If
                Next

            Next
            'Getting Back to the First State
            grdPostedDataDeletionLoad.SetPageIndex(intPagecount)

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub UnChkCheckbox()
        Try
            If chkPDDGridSelectAll.Checked = True Then
                chkPDDGridSelectAll.Checked = False
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
End Class