Public Class ApplicationManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            mvUserAdmin.ActiveViewIndex = 0
            GetPayerTableMgmtClients()
        End If
    End Sub

    Protected Sub lnkExit_Click(sender As Object, e As EventArgs) Handles lnkExit.Click
        Try
            Server.Transfer("UserAdmin.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkOffCycleRefresh_Click(sender As Object, e As EventArgs) Handles lnkOffCycleRefresh.Click
        Dim strClient As String = String.Empty
        Try
            strClient = GetSelectedClients()
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            RefreshDailyEPRAData(SQLcon, strClient)
            RefreshDailyPostedData(SQLcon, strClient)
            SQLcon.Close()
            Server.Transfer("Default.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub
    Private Sub GetPayerTableMgmtClients()
        Try
            Dim dsClients As DataSet
            Dim li As ListItem
            ddlClient.Items.Clear()
            ddlClient.Items.Insert(0, New ListItem("All", "NA"))
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
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
    Private Function GetSelectedClients() As String
        Dim strClients As String = String.Empty
        Dim intClient As Integer
        Try
            If ddlClient.SelectedItem.ToString = "All" Then
                For intClient = 0 To ddlClient.Items.Count - 1
                    If ddlClient.Items(intClient).Text <> "All" Then
                        If strClients = String.Empty Then
                            strClients = ddlClient.Items(intClient).Value
                        Else
                            strClients = strClients + "," + ddlClient.Items(intClient).Value
                        End If
                    End If
                Next
            Else
                strClients = ddlClient.SelectedValue.ToString()
            End If

            Return strClients
        Catch ex As Exception
            Return String.Empty
            ex.Message.ToString()
        End Try
    End Function
End Class