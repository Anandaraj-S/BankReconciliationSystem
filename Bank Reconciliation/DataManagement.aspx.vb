Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class DataManagement
    Inherits System.Web.UI.Page
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            mvUserAdmin.ActiveViewIndex = 0
            LoadClients()
            LoadBankRecords()
        End If
        lblVarianceOrBaicode.Text = "Variance"
        lblDataMgmt.Visible = False
        ''ChkSelectAll.Checked = False
        ''lnkDMdelete.Visible = False
    End Sub
    Private Sub LoadClients()
        Try
            Dim dsClients As DataSet
            Dim li As ListItem
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            ddlClient.Items.Clear()
            dsClients = GetClients(SQLcon)
            For Each dr As DataRow In dsClients.Tables(0).Rows
                li = New ListItem
                li.Value = dr("Master_Client_Number").ToString
                li.Text = dr("Client_Name").ToString
                ddlClient.Items.Add(li)
            Next
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub rbLockbox_CheckedChanged(sender As Object, e As EventArgs) Handles rbLockbox.CheckedChanged
        rbBankdataother.Checked = False
        rbCreditCard.Checked = False
        rbEFT.Checked = False
        LoadBankRecords()
    End Sub

    Protected Sub rbEFT_CheckedChanged(sender As Object, e As EventArgs) Handles rbEFT.CheckedChanged
        rbBankdataother.Checked = False
        rbCreditCard.Checked = False
        rbLockbox.Checked = False
        LoadBankRecords()
    End Sub

    Protected Sub rbCreditCard_CheckedChanged(sender As Object, e As EventArgs) Handles rbCreditCard.CheckedChanged
        rbBankdataother.Checked = False
        rbEFT.Checked = False
        rbLockbox.Checked = False
        LoadBankRecords()
    End Sub

    Protected Sub rbBankdataother_CheckedChanged(sender As Object, e As EventArgs) Handles rbBankdataother.CheckedChanged
        rbEFT.Checked = False
        rbCreditCard.Checked = False
        rbLockbox.Checked = False
        lblVarianceOrBaicode.Text = "BAI Code"
        LoadBankRecords()
    End Sub
    Private Sub LoadBankRecords()
        Dim CurrentWorkQueue As String
        Dim CurrentClient As Integer
        Dim dsVariance As DataSet
        Dim DepositeDate As String
        Dim CheckNo As String
        Dim DepositAmount As String
        Dim Variance As String
        Dim BAIcode As String

        Try
            ChkSelectAll.Checked = False
            CurrentClient = CInt(ddlClient.SelectedValue)
            CurrentWorkQueue = GetCurrentQueue()
            Dim objVarianceOrBaicode As System.Web.UI.WebControls.BoundField = grdDataMgmt.Columns(4)
            dsVariance = Nothing

            If txtBankDepositeDate.Text <> String.Empty Then
                DepositeDate = txtBankDepositeDate.Text
            Else
                DepositeDate = String.Empty
            End If

            DepositAmount = txtBankDepositAmt.Text
            CheckNo = txtCheckNo.Text

            If rbBankdataother.Checked = True Then
                objVarianceOrBaicode.DataField = "BAI_Code"
                Variance = String.Empty
                BAIcode = txtVarianceOrBaiCode.Text
            Else
                objVarianceOrBaicode.DataField = "VARIANCE"
                BAIcode = String.Empty
                Variance = txtVarianceOrBaiCode.Text
            End If

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsVariance = LoadDataManagement(SQLcon, CurrentClient, DepositeDate, CheckNo, DepositAmount, Variance, BAIcode, CurrentWorkQueue)
            SQLcon.Close()
            grdDataMgmt.DataSource = dsVariance
            grdDataMgmt.DataBind()
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub
    Private Function GetCurrentQueue() As String
        Dim strQueue As String

        If rbEFT.Checked = True Then
            strQueue = "EFT"
        ElseIf rbCreditCard.Checked = True Then
            strQueue = "CREDIT CARD"
        ElseIf rbBankdataother.Checked = True Then
            strQueue = "OTHER"
        Else
            strQueue = "CHECK"
        End If
        Return strQueue
    End Function

    Protected Sub lnkDataMgmtReturn_Click(sender As Object, e As EventArgs) Handles lnkDataMgmtReturn.Click
        Server.Transfer("UserAdmin.aspx")
    End Sub

    Protected Sub ddlClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClient.SelectedIndexChanged
        If rbBankdataother.Checked = True Then
            lblVarianceOrBaicode.Text = "BAI Code"
        Else
            lblVarianceOrBaicode.Text = "Variance"
        End If
        LoadBankRecords()
    End Sub

    Protected Sub txtBankDepositeDate_TextChanged(sender As Object, e As EventArgs) Handles txtBankDepositeDate.TextChanged
        LoadBankRecords()
    End Sub

    Protected Sub txtBankDepositAmt_TextChanged(sender As Object, e As EventArgs) Handles txtBankDepositAmt.TextChanged
        LoadBankRecords()
    End Sub

    Protected Sub txtCheckNo_TextChanged(sender As Object, e As EventArgs) Handles txtCheckNo.TextChanged
        LoadBankRecords()
    End Sub

    Protected Sub txtVarianceOrBaiCode_TextChanged(sender As Object, e As EventArgs) Handles txtVarianceOrBaiCode.TextChanged
        LoadBankRecords()
    End Sub
    Protected Sub ChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSelectAll.CheckedChanged
        Try

            For Each rowDataManagment As GridViewRow In grdDataMgmt.Rows
                Dim ChkDMRecords As CheckBox = CType(rowDataManagment.FindControl("chkRecords"), CheckBox)
                If ChkSelectAll.Checked = True Then
                    ChkDMRecords.Checked = True
                Else
                    ChkDMRecords.Checked = False
                End If
            Next

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub lnkDMdelete_Click(sender As Object, e As EventArgs) Handles lnkDMdelete.Click
        Dim DepositeDate As String = String.Empty
        Dim CheckNo As String = String.Empty
        Dim DepositAmount As String = String.Empty
        Dim Variance As String = String.Empty
        Dim BAIcode As String = String.Empty
        Dim CurrentClient As Integer
        Dim CurrentWorkQueue As String = String.Empty
        Dim AccountNumber As String = String.Empty
        Dim VarianceOrBAICode As String = String.Empty
        Dim Bank_DataID As String = String.Empty
        lblDataMgmt.Visible = True
        Try

            CurrentClient = CInt(ddlClient.SelectedValue)
            CurrentWorkQueue = GetCurrentQueue()
            
            For index As Integer = 0 To grdDataMgmt.Rows.Count - 1

                Dim ckRecord As CheckBox = CType(grdDataMgmt.Rows(index).FindControl("chkRecords"), CheckBox)

                If ckRecord.Checked Then
                    ''Bank_RecordID
                    If grdDataMgmt.Rows(index).Cells(6).Text.Length <> 0 Then
                        If Bank_DataID = String.Empty Then
                            Bank_DataID = +grdDataMgmt.Rows(index).Cells(6).Text
                        Else
                            Bank_DataID = Bank_DataID + "," + grdDataMgmt.Rows(index).Cells(6).Text
                        End If
                    End If
                End If
            Next

            If Bank_DataID <> String.Empty Then

                Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
                If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

                DeleteBankData(SQLcon, Bank_DataID, CurrentClient, CurrentWorkQueue, Session("user"))
                SQLcon.Close()
                LoadBankRecords()
                lblDataMgmt.Text = "Deletion attempt sucessful!"
            Else
                lblDataMgmt.Text = "Deletion Failed. Please select the records to delete!"
            End If

        Catch ex As Exception
            lblDataMgmt.Text = "Deletion Failed!"
            ex.Message.ToString()
        End Try
    End Sub
End Class