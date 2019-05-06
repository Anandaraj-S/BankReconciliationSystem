Imports System.Data.SqlClient

Public Class BankDataInsert
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lnkWQSelectedQueue.Text = Session("CurrentWorkQueue")
        lblWQClient.Text = Session("CurrentClientName")
        lblWQSelectedQueue.Text = Session("CurrentWorkQueue")

        If Not IsPostBack Then
            LoadDropDownBaiCode()
            LoadDropdownPayer()
            LoadDropDownAccountName()
        End If
    End Sub
    Protected Sub SaveBankData(ByVal sender As Object, ByVal e As EventArgs)
        If ValidationControl() = False Then Exit Sub
        SaveRecords()
        Server.Transfer("Work Queue.aspx")
    End Sub
    Private Function ValidationControl() As Boolean
        Dim blnError As Boolean = True
        If (ddlAccountName.SelectedValue = "0" Or ddlAccountName.SelectedValue = "") Then
            lblErrorMessgae.Text = "Account Name  code cann't be empty, Please select Account Name."
            lblErrorMessgae.Visible = True
            blnError = False
        ElseIf (ddlBaicode.SelectedValue = "0" Or ddlBaicode.SelectedValue = "") Then
            lblErrorMessgae.Text = "BAI code cann't be empty, Please select BAI code."
            lblErrorMessgae.Visible = True
            blnError = False
        ElseIf (txtDepositdate.Text = "" Or txtDepositdate.Text.Length < 8) Then
            lblErrorMessgae.Text = "Deposit date cann't be empty,, Please give the correct deposit date (MM/DD/YYYY)."
            lblErrorMessgae.Visible = True
            blnError = False
        Else
            blnError = True
            lblErrorMessgae.Text = ""
            lblErrorMessgae.Visible = False
        End If
        Return blnError
    End Function
    Private Sub SaveRecords()
        Try
            Dim AccountName As String
            Dim AccountNumber As String
            Dim Amount As Double
            Dim Bai_Code As String
            Dim Bank_Id As Double
            Dim Check_Number As String
            Dim Payer As String
            Dim DepDt As String
            Dim Deposit_Date As DateTime
            Dim Text As String
            Dim Type As String
            Dim CurrentClient As Integer

            AccountName = ddlAccountName.SelectedItem.Text
            AccountNumber = txtAccountNumber.Text
            Amount = TxtAmount.Text
            Bai_Code = ddlBaicode.SelectedValue
            Bank_Id = txtBankId.Text
            Check_Number = txtCheckNumber.Text
            Payer = ddlPayer.SelectedValue
            DepDt = txtDepositdate.Text
            Deposit_Date = Convert.ToDateTime(DepDt)
            Text = txtText.Text
            Type = TxtType.Text
            CurrentClient = LoadBankSettingsdata(ddlAccountName.SelectedValue, "ClientID")
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            InsertBankData(SQLcon, AccountName, AccountNumber, Amount, Bai_Code, Bank_Id, Check_Number, Payer, Deposit_Date, Text, Type, CurrentClient, Session("user"))
            SQLcon.Close()
            'MsgBox("Inserted Successfully")
            RefreshPostedDataRecords()
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub
    Private Sub LoadDropDownBaiCode()
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
        Using cmd As New SqlCommand("SELECT PAYCODEID,PAYCODE_VALUE FROM BANK.BAI_PAYCODE")
            cmd.CommandType = CommandType.Text
            cmd.Connection = SQLcon
            ddlBaicode.DataSource = cmd.ExecuteReader()
            ddlBaicode.DataTextField = "PAYCODE_VALUE"
            ddlBaicode.DataValueField = "PAYCODEID"
            ddlBaicode.DataBind()
            SQLcon.Close()
        End Using

        ddlBaicode.Items.Insert(0, New ListItem("--Select PayCode--", "0"))

    End Sub
    Private Sub LoadDropdownPayer()
        Try
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            Using cmd As New SqlCommand("SELECT Payer_ID , Payer  FROM [Bank].[Payer] WHERE [Logical_Active] = 1 AND Client_Number= " & CurrentClient)
                cmd.CommandType = CommandType.Text
                cmd.Connection = SQLcon
                ddlPayer.DataSource = cmd.ExecuteReader()
                ddlPayer.DataTextField = "Payer"
                ddlPayer.DataValueField = "Payer_ID"
                ddlPayer.DataBind()
                SQLcon.Close()
            End Using

            ddlPayer.Items.Insert(0, New ListItem("--Select Payer--", "0"))
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub LoadDropDownAccountName()
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
        Using cmd As New SqlCommand("SELECT Bank_File_SettingsID,Setting_Name FROM BANK.Bank_File_Settings")
            cmd.CommandType = CommandType.Text
            cmd.Connection = SQLcon
            ddlAccountName.DataSource = cmd.ExecuteReader()
            ddlAccountName.DataTextField = "Setting_Name"
            ddlAccountName.DataValueField = "Bank_File_SettingsID"
            ddlAccountName.DataBind()
            SQLcon.Close()
        End Using

        ddlAccountName.Items.Insert(0, New ListItem("--Select AC Name--", "0"))

    End Sub
    Protected Sub lnkWQQueueTotals_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkWQQueueTotals.Click
        'Response.Redirect("Default.aspx")
        Server.Transfer("Default.aspx")
    End Sub

    Protected Sub lnkWQSelectedQueue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkWQSelectedQueue.Click
        'Response.Redirect("Variance.aspx")
        Server.Transfer("Variance.aspx")
    End Sub

    Private Function lnkInsertBankData() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub lnkWorkQueue_Click(sender As Object, e As EventArgs) Handles lnkWorkQueue.Click
        'Response.Redirect("Work Queue.aspx")
        Server.Transfer("Work Queue.aspx")
    End Sub

    Private Sub ddlAccountName_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAccountName.SelectedIndexChanged
        LoadBankSettingsdata(ddlAccountName.SelectedValue, "Bank_Account_Number")
    End Sub
    Private Function LoadBankSettingsdata(ByVal FileSettingsID As Integer, ByVal strGetColumn As String) As Integer
        Dim intClientID As Integer = 0
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        Dim reader As SqlDataReader
        Try
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            Using cmd As New SqlCommand("SELECT Bank_Account_Number,Bank_ID,Setting_Client from bank.Bank_File_Settings WHERE  Bank_File_SettingsID = " & FileSettingsID)
                cmd.CommandType = CommandType.Text
                cmd.Connection = SQLcon
                reader = cmd.ExecuteReader()
                If reader.Read() Then
                    If strGetColumn = "Bank_Account_Number" Then
                        txtAccountNumber.Text = reader("Bank_Account_Number").ToString()
                        txtBankId.Text = reader("Bank_ID").ToString()
                    Else
                        intClientID = CInt(reader("Setting_Client").ToString())
                    End If
                    reader.Close()
                    SQLcon.Close()
                End If
            End Using
            Return intClientID
        Catch ex As Exception
            Return 0
            ex.ToString()
        End Try
    End Function

    Private Sub ddlBaicode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBaicode.SelectedIndexChanged
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        Dim reader As SqlDataReader
        Try
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            Using cmd As New SqlCommand("SELECT [TYPE] FROM BANK.BAI_Paycode WHERE PaycodeID ='" & ddlBaicode.SelectedValue & "'")
                cmd.CommandType = CommandType.Text
                cmd.Connection = SQLcon
                reader = cmd.ExecuteReader()
                If reader.Read() Then
                    TxtType.Text = reader("TYPE").ToString()
                    reader.Close()
                    SQLcon.Close()
                End If
            End Using
        Catch ex As Exception
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
End Class