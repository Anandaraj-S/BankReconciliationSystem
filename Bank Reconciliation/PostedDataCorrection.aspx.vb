Public Class PostedDataCorrection
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Try
                mvposteddatacorrection.ActiveViewIndex = 0
                LoadClients()
                PopulatePostedCorrectionRecordsGrid()
                Session("PDCChecks") = Nothing
                lblPDCCurrentType.Text = "Lockbox"
            Catch ex As Exception
                ex.Message.ToString()
            End Try
        End If
    End Sub
    Private Sub LoadClients()
        Try
            Dim dsPDCClients As DataSet
            Dim li As ListItem
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            ddlPDCClient.Items.Clear()
            dsPDCClients = GetClients(SQLcon)
            For Each dr As DataRow In dsPDCClients.Tables(0).Rows
                li = New ListItem
                li.Value = dr("Master_Client_Number").ToString
                li.Text = dr("Client_Name").ToString
                ddlPDCClient.Items.Add(li)
            Next
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub PopulatePostedCorrectionRecordsGrid()
        Try
            Dim intCurrentClient As Integer = CInt(ddlPDCClient.SelectedValue)
            Dim strCurrentType As String = GetCurrentType()
            Dim BatchID As String = txtPDCsearchBatchId.Text
            Dim Invoice As String = txtPDCsearchInvoice.Text
            Dim Closing_Dt As String = txtPDCsearchClosingDt.Text
            Dim Bank_Dept_Dt As String = txtPDCsearchDepositDate.Text
            Dim Create_Dt As String = txtPDCSearchCreateDate.Text
            Dim Designation As String = txtPDCsearchDesignation.Text
            Dim SearchType As String = txtPDCSearchType.Text
            Dim Pay_Amt As String = TxtPDCsearchPayamt.Text
            Dim Pay_Code As String = txtPDCSearchPayCode.Text
            Dim Posted_Dt As String = txtPDCSearchPosteddt.Text
            Dim Check As String = txtPDCsearchCheck.Text
            Dim Comment As String = txtPDCSearchcomment.Text

            Dim dsCorrectionPostRecords As New DataSet

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsCorrectionPostRecords = GetPostedDataCorrectionRecords(SQLcon, intCurrentClient, strCurrentType, BatchID, Invoice, Bank_Dept_Dt, Closing_Dt, Create_Dt, Designation, _
                                                                     SearchType, Pay_Amt, Pay_Code, Posted_Dt, Check, Comment)
            SQLcon.Close()
            grdPostedDataCorrectionLoad.DataSource = dsCorrectionPostRecords
            grdPostedDataCorrectionLoad.DataBind()

            If dsCorrectionPostRecords.Tables(0).Rows.Count = 0 Then
                lnkPDCPush.Visible = False
                lnkPDCdelete.Visible = False
            Else
                lnkPDCPush.Visible = True
                lnkPDCdelete.Visible = True
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Function GetCurrentType() As String
        Dim strType As String = String.Empty
        Try
            If rbPDCEFT.Checked = True Then
                strType = "EFT"
            ElseIf rbPDCCreditCard.Checked = True Then
                strType = "CREDIT CARD"
            ElseIf rbPDCOther.Checked = True Then
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
    Protected Sub rbPDCLockbox_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDCLockbox.CheckedChanged
        Try
            rbPDCEFT.Checked = False
            rbPDCCreditCard.Checked = False
            rbPDCOther.Checked = False
            Session("PDCChecks") = Nothing
            hfPDCDeleteCount.Value = "0"
            hfPDCPushCount.Value = "0"
            lblPDCCurrentType.Text = "Lockbox"
            UnChkCheckbox()
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkPDCReturn_Click(sender As Object, e As EventArgs) Handles lnkPDCReturn.Click
        Try
            Server.Transfer("UserAdmin.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PostedDataCorrectionCancelEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdPostedDataCorrectionLoad.RowCancelingEdit
        Try
            grdPostedDataCorrectionLoad.EditIndex = -1
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PostedDataCorrectionEdit(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdPostedDataCorrectionLoad.RowEditing
        Try
            grdPostedDataCorrectionLoad.EditIndex = e.NewEditIndex
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PostedDataCorrectionUpdate(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdPostedDataCorrectionLoad.RowUpdating
        Try
            Dim CurrentClient As Integer = CInt(ddlPDCClient.SelectedValue)
            Dim Staging_DataID As String = DirectCast(grdPostedDataCorrectionLoad.Rows(e.RowIndex).FindControl("lblPDCStaging_Number"), Label).Text
            Dim BankDeptDt As Date = DirectCast(grdPostedDataCorrectionLoad.Rows(e.RowIndex).FindControl("txtPDCBankDeptDt"), TextBox).Text
            Dim Designation As String = DirectCast(grdPostedDataCorrectionLoad.Rows(e.RowIndex).FindControl("txtPDCDesignation"), TextBox).Text
            Dim Type As String = DirectCast(grdPostedDataCorrectionLoad.Rows(e.RowIndex).FindControl("txtPDCType"), TextBox).Text
            Dim Check As String = DirectCast(grdPostedDataCorrectionLoad.Rows(e.RowIndex).FindControl("txtPDCCheck"), TextBox).Text
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            UpdatePostDataCorrectionRecords(SQLcon, Staging_DataID, BankDeptDt, Designation, Type, Check, Session("User"), CurrentClient)
            SQLcon.Close()
            grdPostedDataCorrectionLoad.EditIndex = -1
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdPostedDataCorrectionLoad_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPostedDataCorrectionLoad.PageIndexChanging
        Try
            SaveCheckedValues()
            grdPostedDataCorrectionLoad.PageIndex = e.NewPageIndex
            PopulatePostedCorrectionRecordsGrid()
            RestoreCheckedValues()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub PostedCorrectionDataSearch()
        SaveCheckedValues()
        PopulatePostedCorrectionRecordsGrid()
        RestoreCheckedValues()
    End Sub

    Protected Sub rbPDCEFT_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDCEFT.CheckedChanged
        Try
            rbPDCLockbox.Checked = False
            rbPDCCreditCard.Checked = False
            rbPDCOther.Checked = False
            Session("PDCChecks") = Nothing
            hfPDCDeleteCount.Value = "0"
            hfPDCPushCount.Value = "0"
            lblPDCCurrentType.Text = "EFT"
            UnChkCheckbox()
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub rbPDCCreditCard_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDCCreditCard.CheckedChanged
        Try
            rbPDCEFT.Checked = False
            rbPDCLockbox.Checked = False
            rbPDCOther.Checked = False
            Session("PDCChecks") = Nothing
            hfPDCDeleteCount.Value = "0"
            hfPDCPushCount.Value = "0"
            lblPDCCurrentType.Text = "CreditCard"
            UnChkCheckbox()
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub rbPDCOther_CheckedChanged(sender As Object, e As EventArgs) Handles rbPDCOther.CheckedChanged
        Try
            rbPDCEFT.Checked = False
            rbPDCCreditCard.Checked = False
            rbPDCLockbox.Checked = False
            Session("PDCChecks") = Nothing
            hfPDCDeleteCount.Value = "0"
            hfPDCPushCount.Value = "0"
            lblPDCCurrentType.Text = "Other"
            UnChkCheckbox()
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub ddlPDCClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPDCClient.SelectedIndexChanged
        Try
            Session("PDCChecks") = Nothing
            hfPDCDeleteCount.Value = "0"
            hfPDCPushCount.Value = "0"
            PopulatePostedCorrectionRecordsGrid()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtPDCsearchBatchId_TextChanged(sender As Object, e As EventArgs) Handles txtPDCsearchBatchId.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub txtPDCsearchInvoice_TextChanged(sender As Object, e As EventArgs) Handles txtPDCsearchInvoice.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub txtPDCsearchCheck_TextChanged(sender As Object, e As EventArgs) Handles txtPDCsearchCheck.TextChanged
        PostedCorrectionDataSearch()
    End Sub
    Protected Sub txtPDCSearchCreateDate_TextChanged(sender As Object, e As EventArgs) Handles txtPDCSearchCreateDate.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub txtPDCsearchDesignation_TextChanged(sender As Object, e As EventArgs) Handles txtPDCsearchDesignation.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub txtPDCSearchType_TextChanged(sender As Object, e As EventArgs) Handles txtPDCSearchType.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub TxtPDCsearchPayamt_TextChanged(sender As Object, e As EventArgs) Handles TxtPDCsearchPayamt.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub txtPDCSearchPayCode_TextChanged(sender As Object, e As EventArgs) Handles txtPDCSearchPayCode.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub txtPDCSearchPosteddt_TextChanged(sender As Object, e As EventArgs) Handles txtPDCSearchPosteddt.TextChanged
        PostedCorrectionDataSearch()
    End Sub
    Protected Sub txtPDCsearchDepositDate_TextChanged(sender As Object, e As EventArgs) Handles txtPDCsearchDepositDate.TextChanged
        PostedCorrectionDataSearch()
    End Sub

    Protected Sub lnkPDCPush_Click(sender As Object, e As EventArgs) Handles lnkPDCPush.Click
        Dim strStaging_DataID As String = String.Empty
        ''Dim intCount As Integer = 0
        Try
            Dim confirmValue As String = Request.Form("Confirm_Push_value")
            If confirmValue = "Yes" Then
                SaveCheckedValues()

                Dim arr_PostedDatacort As ArrayList = DirectCast(Session("PDCChecks"), ArrayList)

                If Not arr_PostedDatacort Is Nothing Then
                    For Each intstagingID As Integer In arr_PostedDatacort
                        If strStaging_DataID = String.Empty Then
                            strStaging_DataID = intstagingID.ToString()
                        Else
                            strStaging_DataID = strStaging_DataID + "," + intstagingID.ToString()
                        End If
                    Next

                    If strStaging_DataID <> String.Empty Then
                        ''intCount = arr_PostedDatacort.Count
                        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
                        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
                        CorrectionPostedRecordsPush(SQLcon, strStaging_DataID, Session("user"))
                        SQLcon.Close()
                        PopulatePostedCorrectionRecordsGrid()
                        ShowMessage("Records pushed sucessfully!")
                    End If
                End If
                Session("PDCChecks") = Nothing
                hfPDCPushCount.Value = "0"
                UnChkCheckbox()
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub SaveCheckedValues()
        Dim chkList As New ArrayList()
        Try

            Dim index As Integer = -1
            For Each gvrow As GridViewRow In grdPostedDataCorrectionLoad.Rows
                index = CInt(grdPostedDataCorrectionLoad.DataKeys(gvrow.RowIndex).Value)
                Dim result As Boolean = DirectCast(gvrow.FindControl("chkPostDataRecords"), CheckBox).Checked

                If Session("PDCChecks") IsNot Nothing Then
                    chkList = DirectCast(Session("PDCChecks"), ArrayList)
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
                Session("PDCChecks") = chkList
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub RestoreCheckedValues()
        Dim intCount As Integer = 0
        Try
            Dim chkList As ArrayList = DirectCast(Session("PDCChecks"), ArrayList)
            If chkList IsNot Nothing AndAlso chkList.Count > 0 Then
                For Each gvrow As GridViewRow In grdPostedDataCorrectionLoad.Rows
                    Dim index As Integer = CInt(grdPostedDataCorrectionLoad.DataKeys(gvrow.RowIndex).Value)
                    If chkList.Contains(index) Then
                        Dim myCheckBox As CheckBox = DirectCast(gvrow.FindControl("chkPostDataRecords"), CheckBox)
                        myCheckBox.Checked = True
                        intCount += 1
                    End If
                Next
            End If
            hfPDCDeleteCount.Value = (chkList.Count - intCount).ToString()
            hfPDCPushCount.Value = (chkList.Count - intCount).ToString()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub chkPDCSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkSelectAll As CheckBox = DirectCast(grdPostedDataCorrectionLoad.HeaderRow.FindControl("chkPDCSelectAll"), CheckBox)
            For Each rowDataManagment As GridViewRow In grdPostedDataCorrectionLoad.Rows
                Dim ChkPostDataCorrectionRecords As CheckBox = CType(rowDataManagment.FindControl("chkPostDataRecords"), CheckBox)
                If chkSelectAll.Checked = True Then
                    ChkPostDataCorrectionRecords.Checked = True
                Else
                    ChkPostDataCorrectionRecords.Checked = False
                End If
            Next

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtPDCsearchClosingDt_TextChanged(sender As Object, e As EventArgs) Handles txtPDCsearchClosingDt.TextChanged
        PostedCorrectionDataSearch()
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
    Private Sub UnChkCheckbox()
        Try
            If chkPDCGridSelectAll.Checked = True Then
                chkPDCGridSelectAll.Checked = False
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub chkPDCGridSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkPDCGridSelectAll.CheckedChanged
        Try
            Dim intPagecount As Integer = grdPostedDataCorrectionLoad.PageIndex
            'Loop through All Pages
            For intcount As Integer = 0 To grdPostedDataCorrectionLoad.PageCount - 1
                'Set Page Index
                grdPostedDataCorrectionLoad.SetPageIndex(intcount)
                For Each rowDataManagment As GridViewRow In grdPostedDataCorrectionLoad.Rows
                    Dim ChkPostDataDeletionRecords As CheckBox = CType(rowDataManagment.FindControl("chkPostDataRecords"), CheckBox)
                    If chkPDCGridSelectAll.Checked = True Then
                        ChkPostDataDeletionRecords.Checked = True
                    Else
                        ChkPostDataDeletionRecords.Checked = False
                    End If
                Next

            Next
            'Getting Back to the First State
            grdPostedDataCorrectionLoad.SetPageIndex(intPagecount)

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkPDCdelete_Click(sender As Object, e As EventArgs) Handles lnkPDCdelete.Click
        Dim strPosted_DataID As String = String.Empty
        Dim strmessage As String = String.Empty

        Try
            Dim confirmValue As String = Request.Form("Confirm_Delete_value")

            If confirmValue = "Yes" Then
                SaveCheckedValues()

                Dim arr_PostedData As ArrayList = DirectCast(Session("PDCChecks"), ArrayList)

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
                        DeleteCorrectionPostedRecords(SQLcon, strPosted_DataID, Session("user"))
                        SQLcon.Close()
                        PopulatePostedCorrectionRecordsGrid()
                        ShowMessage("Records deleted sucessfully!")
                    End If
                End If
                Session("PDCChecks") = Nothing
                hfPDCDeleteCount.Value = "0"
                UnChkCheckbox()
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtPDCSearchcomment_TextChanged(sender As Object, e As EventArgs) Handles txtPDCSearchcomment.TextChanged
        Try
            PostedCorrectionDataSearch()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
End Class