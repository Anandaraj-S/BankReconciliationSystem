Imports System.Data.SqlClient
Public Class BankMatched
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tblBank As New DataTable
        Dim brow As DataRow
        If Not IsPostBack Then
            lnkWQSelectedQueue.Text = Session("CurrentWorkQueue")
            lblWQClient.Text = Session("CurrentClientName")
            lblWQSelectedQueue.Text = Session("CurrentWorkQueue")
            lblSelectedDate.Text = Session("CurrentBankDate")

            tblBank.Columns.Add("BankDepositDate")
            tblBank.Columns.Add("CheckNumber")
            tblBank.Columns.Add("AMOUNT")
            tblBank.Columns.Add("Type")
            brow = tblBank.NewRow
            brow("Amount") = ""

            tblBank.Rows.Add(brow)

            grdBankMatched.DataSource = tblBank
            grdBankMatched.DataBind()
            LoadBankMatchedRecord()
        End If
    End Sub
    Private Sub LoadBankMatchedRecord()
        Dim dsBDMatched As DataSet
        Dim Type As String
        Dim DepostDate As String
        Dim Amount As String
        Dim CheckNumner As String
        Dim CurrentClient As Integer

        Try
            CurrentClient = CInt(Session("CurrentClientID").ToString)
            If rbAll.Checked = True Then
                Type = "ALL"
            ElseIf rbEFT.Checked = True Then
                Type = "EFT"
            ElseIf rbCreditCard.Checked = True Then
                Type = "CREDIT CARD"
            ElseIf rbLockbox.Checked = True Then
                Type = "CHECK"
            ElseIf rbOther.Checked = True Then
                Type = "OTHER"
            Else
                Type = "ALL"
            End If

            DepostDate = txtDepositDate.Text
            Amount = txtAmount.Text
            CheckNumner = txtCheck.Text
            If txtCheck.Text <> "" Then
                Type = txtType.Text
            End If


            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsBDMatched = LoadBankDataMatched(SQLcon, DepostDate, CheckNumner, Amount, Type, CurrentClient)
            SQLcon.Close()
            grdBankMatched.DataSource = dsBDMatched
            grdBankMatched.DataBind()
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub

    Private Sub rbAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbAll.CheckedChanged
        rbEFT.Checked = False
        rbCreditCard.Checked = False
        rbLockbox.Checked = False
        rbOther.Checked = False
        LoadBankMatchedRecord()
    End Sub

    Private Sub rbCreditCard_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbCreditCard.CheckedChanged
        rbAll.Checked = False
        rbEFT.Checked = False
        rbLockbox.Checked = False
        rbOther.Checked = False
        LoadBankMatchedRecord()
    End Sub

    Private Sub rbEFT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbEFT.CheckedChanged
        rbAll.Checked = False
        rbCreditCard.Checked = False
        rbLockbox.Checked = False
        rbOther.Checked = False
        LoadBankMatchedRecord()
    End Sub

    Private Sub rbLockbox_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbLockbox.CheckedChanged
        rbAll.Checked = False
        rbCreditCard.Checked = False
        rbEFT.Checked = False
        rbOther.Checked = False
        LoadBankMatchedRecord()
    End Sub
    Private Sub rbOther_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbOther.CheckedChanged
        rbAll.Checked = False
        rbCreditCard.Checked = False
        rbEFT.Checked = False
        rbLockbox.Checked = False
        LoadBankMatchedRecord()
    End Sub
    Private Sub txtAmount_TextChanged(sender As Object, e As System.EventArgs) Handles txtAmount.TextChanged
        LoadBankMatchedRecord()
    End Sub

    Private Sub txtCheck_TextChanged(sender As Object, e As System.EventArgs) Handles txtCheck.TextChanged
        LoadBankMatchedRecord()
    End Sub

    Private Sub txtDepositDate_TextChanged(sender As Object, e As System.EventArgs) Handles txtDepositDate.TextChanged
        LoadBankMatchedRecord()
    End Sub

    Private Sub txtType_TextChanged(sender As Object, e As System.EventArgs) Handles txtType.TextChanged
        LoadBankMatchedRecord()
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkWQQueueTotals.Click
        'Response.Redirect("Default.aspx")
        Server.Transfer("Default.aspx")
    End Sub

    Protected Sub lnkWQSelectedQueue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkWQSelectedQueue.Click
        'Response.Redirect("Variance.aspx")
        Server.Transfer("Variance.aspx")
    End Sub

   
    
End Class
