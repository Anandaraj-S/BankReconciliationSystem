Public Class SearchData
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub SearchData_Click(sender As Object, e As EventArgs) Handles SearchData.Click


        Dim CtrlName As String = hidSourceID.Value
        If (Not CtrlName Is Nothing) Then
            Dim ClientNumber As Integer
            ClientNumber = Convert.ToInt64(bank_data_client_number.Text)
            Dim DepositDate As Date
            DepositDate = String.Format("{0:yyyy/MM/dd}", bankdata_depositdate.Text)
            Dim CheckNumber As Integer
            CheckNumber = Convert.ToInt64(bank_check_no.Text)

            If (CtrlName = "SearchBankData") Then
                If String.IsNullOrEmpty(bank_data_client_number.Text) And String.IsNullOrEmpty(bankdata_depositdate.Text) And String.IsNullOrEmpty(BankDataDataType.SelectedItem.Text) And String.IsNullOrEmpty(bank_check_no.Text) Then
                    searchDataError.Text = "Please provide atleast one value for searching the records"
                    searchDataError.Visible = True
                    Exit Sub
                Else
                    PopulateBankData(DepositDate, BankDataDataType.SelectedItem.Text, CheckNumber, ClientNumber)
                    search_data_grid.ActiveViewIndex = 0
                End If
            Else
                If String.IsNullOrEmpty(batch_number.Text) And String.IsNullOrEmpty(invoice_number.Text) And String.IsNullOrEmpty(PostedDataDataType.SelectedValue) And String.IsNullOrEmpty(BankDepositDate.Text) Then
                    searchDataError.Text = "Please provide atleast one value for searching the records"
                    searchDataError.Visible = True
                    Exit Sub
                    search_data_grid.ActiveViewIndex = 0
                Else
                    'PopulatePostedData()
                End If
            End If
        End If

        'If batch_number.Text=="" &&  Then
    End Sub

    'Protected Sub SearchBankData_Click(sender As Object, e As EventArgs) Handles SearchBankData.Click
    '    search_data_grid.ActiveViewIndex = 1
    'End Sub

    'Protected Sub SearchPostedData_Click(sender As Object, e As EventArgs) Handles SearchPostedData.Click
    '    search_data_grid.ActiveViewIndex = 2
    'End Sub

    Private Sub PopulateBankData(ByVal DepositDate As Date, ByVal DataType As String, ByVal CheckNo As String, ByVal ClientNumber As Integer)
        Dim dsBankData As DataSet
        Try
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsBankData = LoadBankData(SQLcon, DepositDate, CheckNo, ClientNumber, DataType)
            SQLcon.Close()
            searchDataResults.DataSource = dsBankData
            searchDataResults.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class