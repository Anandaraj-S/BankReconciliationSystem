Public Class Maintenance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If HttpContext.Current.Session("SQLcon") Is Nothing Then
                SetApplication_Connection()
            End If
            If Not IsPostBack Then
                CheckMaintenanceStatus()
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub
    Private Sub CheckMaintenanceStatus()
        Dim dsStatus As New DataSet
        Try

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsStatus = Get_LoadStatus(SQLcon, DateTime.Now)
            SQLcon.Close()
            If dsStatus.Tables(0).Rows.Count > 0 Then
                lblmessage.Text = dsStatus.Tables(0).Rows(0).Item("Load_Description")
            Else
                Server.Transfer("Default.aspx")
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub


    Private Sub Timerrefresh_Tick(sender As Object, e As System.EventArgs) Handles Timerrefresh.Tick
        Try
            CheckMaintenanceStatus()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
End Class