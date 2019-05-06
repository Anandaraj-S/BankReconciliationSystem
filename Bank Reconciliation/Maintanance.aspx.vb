Public Class Maintanance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ti As Integer = 1
        Response.AppendHeader("Refresh", "5")
        'Response.AppendHeader("Refresh", ti & ";url=Default.aspx")
        'Label1.Text = Hour(Now) & ":" & Minute(Now) & ":" & Second(Now)
    End Sub
End Class