Public Class Variance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PopulateVarianceGrid()
        End If
    End Sub

    Private Sub PopulateVarianceGrid()
        Try
            Dim CurrentWorkQueue As String = Session("CurrentWorkQueue")
            Dim CurrentClient As Integer = CInt(Session("CurrentClientID").ToString)
            Dim CurrentClientName As String = Session("CurrentClientName")
            Dim colCounts As Collection = Session("CountCollection")
            Dim Count As String
            Dim dsVariance As DataSet
            BaiCode.Visible = False
            Vari.Visible = True
            Noofdays.Visible = False
            UpdateUser.Visible = False
            Vari.InnerText = "Variance"
            BaiCode.InnerText = "BAI Code"
            grdVariance.Columns(0).Visible = True    ' - Bank Deposit Date
            grdVariance.Columns(1).Visible = True    ' - Bank Deposit Amt
            grdVariance.Columns(2).Visible = True    ' - Bank Check #
            grdVariance.Columns(3).Visible = True    ' - Variance
            grdVariance.Columns(4).Visible = False   ' - BAI_Code
            grdVariance.Columns(5).Visible = False   ' - Noofdays
            grdVariance.Columns(6).Visible = False   ' - UpdateUser
            grdVariance.Columns(7).Visible = True    ' - BankRecordID
            grdVariance.Columns(8).Visible = True    ' - PostHeaderID
            grdVariance.Columns(9).Visible = True    ' -PayerName  
            PayerName.Visible = True
            BankRecordID.Visible = False
            PostHeaderID.Visible = False

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsVariance = Nothing
            'populate the grid for the selected queue
            Select Case CurrentWorkQueue
                Case Is = "Lockbox"

                    dsVariance = LockboxVariance(SQLcon, CurrentClient)
                Case Is = "EFT"
                    dsVariance = EFTVariance(SQLcon, CurrentClient)
                Case Is = "CreditCards"
                    dsVariance = CreditCardVariance(SQLcon, CurrentClient)
                Case Is = "ERAVariance"
                    dsVariance = ERAVariance(SQLcon, CurrentClient)
                Case Is = "NonERAVariance"
                    dsVariance = NonERAVariance(SQLcon, CurrentClient)
                Case Is = "Errors"
                    'don't have this yet.
                    dsVariance = BankDataLoadError(SQLcon, CurrentClient)
                    BaiCode.Visible = True
                    grdVariance.Columns(4).Visible = True   ' - BAI_Code
                    Vari.InnerText = "Text"

                    PayerName.Visible = False                            ''Payer Name 
                    grdVariance.Columns(9).Visible = False
                Case Is = "Other"
                    dsVariance = OtherVariance(SQLcon, CurrentClient)
                    Vari.Visible = False
                    BaiCode.Visible = True
                    grdVariance.Columns(3).Visible = False ' Variance data
                    grdVariance.Columns(4).Visible = True ' Bai Code
                Case Is = "Holding"
                    dsVariance = LoadBankDataHolding(SQLcon, CurrentClient)
                    BaiCode.Visible = False
                    Vari.InnerText = "Comments"
                    Vari.Visible = True
                    Noofdays.Visible = True
                    UpdateUser.Visible = True
                    grdVariance.Columns(5).Visible = True   ' - Noofdays
                    grdVariance.Columns(6).Visible = True   ' - UpdateUser

                    PayerName.Visible = False                            ''Payer Name 
                    grdVariance.Columns(9).Visible = False
            End Select

            SQLcon.Close()

            'determine which menu options should be available.  If Zero count then disable.
            Count = colCounts("Lockbox")
            If Count = "0" Then mnuVariances.Items(0).Enabled = False
            Count = colCounts("EFT")
            If Count = "0" Then mnuVariances.Items(1).Enabled = False
            Count = colCounts("CreditCards")
            If Count = "0" Then mnuVariances.Items(2).Enabled = False
            Count = colCounts("ERAVariance")
            If Count = "0" Then mnuVariances.Items(3).Enabled = False
            Count = colCounts("NonERAVariance")
            If Count = "0" Then mnuVariances.Items(4).Enabled = False
            Count = colCounts("Errors")
            If Count = "0" Then mnuVariances.Items(5).Enabled = False
            Count = colCounts("Other")
            If Count = "0" Then mnuVariances.Items(6).Enabled = False


            lblClientName.Text = CurrentClientName
            lblSelectedVariance.Text = CurrentWorkQueue

            grdVariance.DataSource = dsVariance
            grdVariance.DataBind()
            grdVariance.Columns(8).Visible = False     ' - PostHeaderID
            grdVariance.Columns(7).Visible = False     ' - BankRecordID
           
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub

    Protected Sub lnkQueueTotals_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkQueueTotals.Click
        'Response.Redirect("Default.aspx")
        Server.Transfer("Default.aspx")
    End Sub

    Protected Sub mnuVariances_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuVariances.MenuItemClick

        Session("CurrentWorkQueue") = e.Item.Value
        PopulateVarianceGrid()

    End Sub
   
    Private Sub grdVariance_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdVariance.RowCommand
        Dim lnkDateButton As LinkButton
        Dim currentindex As Integer
        Dim row As GridViewRow

        currentindex = Convert.ToInt32(e.CommandArgument)
        row = grdVariance.Rows(currentindex)

        lnkDateButton = row.Cells(0).Controls(0)
        Session("CurrentBankDate") = lnkDateButton.Text
        If row.Cells(2).Text <> "&nbsp;" And row.Cells(2).Text <> "" Then
            Session("CurrentCheckNum") = row.Cells(2).Text
        Else
            Session("CurrentCheckNum") = ""
        End If
        If row.Cells(7).Text <> "&nbsp;" And row.Cells(7).Text <> "" Then
            Session("BankRecordID") = row.Cells(7).Text
        Else
            Session("BankRecordID") = ""
        End If
        If row.Cells(8).Text <> "&nbsp;" And row.Cells(8).Text <> "" Then
            Session("PostHeaderID") = row.Cells(8).Text
        Else
            Session("PostHeaderID") = ""
        End If
        'Response.Redirect("Work Queue.aspx")
        Server.Transfer("Work Queue.aspx")
    End Sub

    Private Sub grdVariance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdVariance.RowDataBound
        Dim lnkDateButton As LinkButton
        Dim dtText As Date

        If e.Row.RowType = DataControlRowType.DataRow Then
            lnkDateButton = e.Row.Cells(0).Controls(0)
            dtText = lnkDateButton.Text
            lnkDateButton.Text = dtText.ToString("MM/dd/yyyy")
        End If

    End Sub

    Protected Sub grdVariance_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdVariance.SelectedIndexChanged

    End Sub
End Class