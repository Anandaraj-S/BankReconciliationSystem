Imports System.Web.Services
Imports System.Web.Script.Services

<ScriptService()>
Public Class PostedDataAddition
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    <WebMethod()>
    Private Shared Function AddPostedData(ByVal sender As Object, ByVal e As System.EventArgs)

    End Function

End Class