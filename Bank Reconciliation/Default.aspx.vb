Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Odbc

Public Class _Default
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' SQL Connection
    ''' </summary>
    ''' <remarks></remarks>
    Dim SQLcon As SqlClient.SqlConnection

    ''' <summary>
    ''' Identifies current environment, PRODUCTION or TEST
    ''' </summary>
    ''' <remarks></remarks>
    Dim Sec As Integer

    Enum SummaryCols As Integer
        Client = 0
        Lockbox = 1
        EFT = 2
        CreditCards = 3
        PostedVarianceERA = 4
        PostedVarianceNonERA = 5
        Other = 6                   ' Calculate type = 'other' values in posted data table
        BankDataLoadErrors = 7
        NoVariance = 8
        Holding = 9

    End Enum


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not Me.IsPostBack Then
        If HttpContext.Current.Session("SQLcon") Is Nothing Then
            SetApplication_Connection()
        End If

        'Get user security and set up security on this page.
        Security()

        PopulateSummaryGrid()
        'End If
    End Sub

    Private Sub PopulateSummaryGrid()
        Dim dsSum As DataSet
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        dsSum = Summary(SQLcon)

        SQLcon.Close()

        grdSummary.DataSource = dsSum
        grdSummary.DataBind()

    End Sub

    ''' <summary>
    ''' Get the Username and the Security for the user.  Sets up security on this screen.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Security()
        Dim GetUser As String()
        Dim User As String = ""
        'Dim con As OdbcConnection

        If HttpContext.Current.Session("User") Is Nothing Then
            GetUser = Split(HttpContext.Current.User.Identity.Name, "\")
            User = GetUser(1).ToString
            lblcuruser.Text = User
            HttpContext.Current.Session("User") = User
        Else
            lblcuruser.Text = HttpContext.Current.Session("User")
        End If

        If HttpContext.Current.Session("Security") Is Nothing Then
            GetSecurity(User)
            HttpContext.Current.Session("Security") = Sec
        Else
            Sec = CInt(HttpContext.Current.Session("Security"))
        End If

        If Sec = 999 Then Server.Transfer("NoAccess.aspx")

        If Sec > 0 Then
            lnkITAdmin.Enabled = False
            lnkITAdmin.Visible = False
        Else
            lnkITAdmin.Enabled = True
            lnkITAdmin.Visible = True
        End If

        If Sec > 1 Then
            lnkUserAdmin.Enabled = False
            lnkUserAdmin.Visible = False
        Else
            lnkUserAdmin.Enabled = True
            lnkUserAdmin.Visible = True
        End If


    End Sub

    Protected Sub GetSecurity(ByVal User As String)
        If SQLcon Is Nothing Then SQLcon = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        Sec = SecuritySpecificUser(SQLcon, User)
        SQLcon.Close()

    End Sub


    Protected Sub lnkUserAdmin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkUserAdmin.Click
        'Response.Redirect("UserAdmin.aspx")
        Server.Transfer("UserAdmin.aspx")
    End Sub

    Private Sub grdSummary_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSummary.RowCommand
        Dim currentindex As Integer
        Dim row As GridViewRow
        Dim clientID As Integer
        Dim colCounts As New Collection
        Dim lnkButton As LinkButton

        currentindex = Convert.ToInt32(e.CommandArgument)
        row = grdSummary.Rows(currentindex)

        clientID = CInt(grdSummary.DataKeys(currentindex)("Master_Client_Number").ToString)

        'Client = 0
        'Lockbox = 1
        'EFT = 2
        'CreditCards = 3
        'PostedVarianceERA = 4
        'PostedVarianceNonERA = 5
        'Other = 6                   ' Calculate type = 'other' values in posted data table
        'BankDataLoadErrors = 7
        'NoVariance = 8
        'Holding = 9

        'set a collection of counts so we can determine which links should be available on the Variance Screen.
        lnkButton = row.Cells(SummaryCols.Lockbox).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.EFT).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.CreditCards).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.PostedVarianceERA).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.PostedVarianceNonERA).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.Other).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.BankDataLoadErrors).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.NoVariance).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)
        lnkButton = row.Cells(SummaryCols.Holding).Controls(0)
        colCounts.Add(lnkButton.Text, lnkButton.CommandName)


        Session("CurrentClientName") = row.Cells(SummaryCols.Client).Text
        Session("CurrentClientID") = clientID
        Session("CurrentWorkQueue") = e.CommandName
        Session("CountCollection") = colCounts
        'Response.Redirect("Variance.aspx")
        If e.CommandName = "No Variance(T-1)" Then
            Server.Transfer("BankMatched.aspx")
        Else
            Server.Transfer("Variance.aspx")
        End If
    End Sub

    Private Sub grdSummary_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSummary.RowDataBound
        Dim sec As Integer = HttpContext.Current.Session("Security")

        If e.Row.RowType = DataControlRowType.DataRow Then

            'Get all of the link buttons in case we need to do anything with them below
            Dim BDLErrors As LinkButton
            BDLErrors = e.Row.Cells(SummaryCols.BankDataLoadErrors).Controls(0)

            Dim LockboxButton As LinkButton
            LockboxButton = e.Row.Cells(SummaryCols.Lockbox).Controls(0)

            Dim EFTButton As LinkButton
            EFTButton = e.Row.Cells(SummaryCols.EFT).Controls(0)

            Dim CCButton As LinkButton
            CCButton = e.Row.Cells(SummaryCols.CreditCards).Controls(0)

            Dim PVERA As LinkButton
            PVERA = e.Row.Cells(SummaryCols.PostedVarianceERA).Controls(0)

            Dim PVnonERA As LinkButton
            PVnonERA = e.Row.Cells(SummaryCols.PostedVarianceNonERA).Controls(0)

            

            Dim Other As LinkButton
            Other = e.Row.Cells(SummaryCols.Other).Controls(0)

            'If any of the values are zero, then disable the link button
            If BDLErrors.Text = "0" Then BDLErrors.Enabled = False
            If LockboxButton.Text = "0" Then LockboxButton.Enabled = False
            If EFTButton.Text = "0" Then EFTButton.Enabled = False
            If CCButton.Text = "0" Then CCButton.Enabled = False
            If PVERA.Text = "0" Then PVERA.Enabled = False
            If PVnonERA.Text = "0" Then PVnonERA.Enabled = False
            If Other.Text = "0" Then Other.Enabled = False

            'once we have the query for this column then move it down into the select.
            'BDLErrors.Enabled = False
            'If security of 3, then the user can't click on any of these workqueues.  So disable the link buttons
            If sec = 3 Then
                LockboxButton.Enabled = False
                EFTButton.Enabled = False
                CCButton.Enabled = False
                PVERA.Enabled = False
                PVnonERA.Enabled = False
                Other.Enabled = False
            End If
        End If

    End Sub


    Protected Sub SearchData_Click(sender As Object, e As EventArgs) Handles searchData.Click
        Try
            Server.Transfer("SearchData.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
End Class