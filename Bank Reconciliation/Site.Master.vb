Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dsMaintanance As DataSet
        Dim Load_Starttime As TimeSpan
        Dim Load_Endtime As TimeSpan = Nothing
        Dim Curr_Time As TimeSpan
        Dim Curr_Second As Integer = 0
        Dim Cal_Loadtime As TimeSpan
        Dim Cal_LoadSeconds As Long
       
        Try

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsMaintanance = Get_LoadProcessDetails(SQLcon, DateTime.Now)
            SQLcon.Close()
            If dsMaintanance.Tables(0).Rows.Count > 0 Then
                Load_Starttime = dsMaintanance.Tables(0).Rows(0).Item("Load_Start_Time")

                If Not dsMaintanance.Tables(0).Rows(0).Item("Load_End_Time") Is DBNull.Value Then
                    Load_Endtime = dsMaintanance.Tables(0).Rows(0).Item("Load_End_Time")
                End If
                Curr_Time = DateTime.Now.TimeOfDay

                If (Curr_Time >= Load_Starttime And Load_Endtime = Nothing) Or (Curr_Time > Load_Starttime And Curr_Time < Load_Endtime) Then
                    Response.Redirect("Maintenance.aspx", False)
                Else
                    lblLoadName.Text = dsMaintanance.Tables(0).Rows(0).Item("Load_Process_Name").ToString()
                    Cal_Loadtime = Load_Starttime - Curr_Time
                    Cal_LoadSeconds = (CLng(Cal_Loadtime.TotalSeconds())) * 1000
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Alert", "UnderMaintenance(" & Cal_LoadSeconds.ToString & ");", True)
                End If
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

End Class