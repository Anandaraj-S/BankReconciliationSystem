Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class UserAdmin
    Inherits System.Web.UI.Page
    Enum ucolumns As Integer
        WindowsID = 1
        FirstName = 2
        LastName = 3
        SecurityRole = 4
        EmailAddress = 5
    End Enum
    Enum pcolumns As Integer
        Payer = 1
        Description = 2
    End Enum
    Enum xcolumns As Integer
        Source = 1
        System = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            mvUserAdmin.ActiveViewIndex = 0
            CheckUserAccess()
        End If
    End Sub

    Protected Sub lnkPayor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPayor.Click
        GetPayerTableMgmtClients()
        PopulatePayers()
        mvUserAdmin.ActiveViewIndex = 3
    End Sub

    Protected Sub lnkExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkExit.Click
        'Response.Redirect("Default.aspx")
        Server.Transfer("Default.aspx")
    End Sub

    Protected Sub lnkUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkUser.Click
        PopulateUsers()
        mvUserAdmin.ActiveViewIndex = 1
    End Sub

    Protected Sub lnkUserReturn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkUserReturn.Click
        mvUserAdmin.ActiveViewIndex = 0
    End Sub

    Private Sub PopulatePayers()
        Dim currentClient As Integer
        Dim dsPayers As DataSet
        Try
            currentClient = CInt(ddlPayerClient.SelectedValue)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            dsPayers = GetAllPayers(SQLcon, currentClient)

            SQLcon.Close()

            For Each dr As DataRow In dsPayers.Tables(0).Rows
                If Not dr("Logical_Active") Is DBNull.Value Then
                    If dr("Logical_Active") = 0 Then dr.Delete()
                End If
            Next
            grdPayers.DataSource = dsPayers
            grdPayers.DataBind()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub PopulateUsers()
        Dim dsUsers As DataSet
        Dim dtSecRoles As DataTable
        Try
            Dim iCurrentSecurityLevel As Integer = HttpContext.Current.Session("Security")
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            'populate User gridview
            dsUsers = AllUsers(SQLcon)
            Session("dsUsers") = dsUsers
            grdUsers.DataSource = dsUsers
            grdUsers.DataBind()

            'populate security role description table
            dtSecRoles = PossibleSecurityRoles(SQLcon, iCurrentSecurityLevel)
            grdRoles.DataSource = dtSecRoles
            grdRoles.DataBind()

            SQLcon.Close()

            'populate security role dropdown box
            ddlSecurity.Items.Clear()
            If iCurrentSecurityLevel = 0 Then
                ddlSecurity.Items.Add("0 - IT full access")
                ddlSecurity.Items(ddlSecurity.Items.Count - 1).Value = 0
            End If

            ddlSecurity.Items.Add("1 - Users, Payor Crossmap Setup")
            ddlSecurity.Items(ddlSecurity.Items.Count - 1).Value = 1
            ddlSecurity.Items.Add("2 - Work Queue Access")
            ddlSecurity.Items(ddlSecurity.Items.Count - 1).Value = 2
            ddlSecurity.Items.Add("3 - View Only Main Queue Totals")
            ddlSecurity.Items(ddlSecurity.Items.Count - 1).Value = 3
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub grdUsers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdUsers.RowCommand
        Dim currentindex As Integer
        Dim row As GridViewRow

        currentindex = Convert.ToInt32(e.CommandArgument)
        row = grdUsers.Rows(currentindex)

        ResetUserScreen()

        txtHiddenRowID.Text = grdUsers.DataKeys(currentindex)("UserRowID")
        txtUserID.Text = HttpUtility.HtmlDecode(row.Cells(ucolumns.WindowsID).Text)
        txtFirst.Text = HttpUtility.HtmlDecode(row.Cells(ucolumns.FirstName).Text)
        txtLast.Text = HttpUtility.HtmlDecode(row.Cells(ucolumns.LastName).Text)
        ddlSecurity.Text = HttpUtility.HtmlDecode(row.Cells(ucolumns.SecurityRole).Text)
        txtEmail.Text = HttpUtility.HtmlDecode(row.Cells(ucolumns.EmailAddress).Text)

        Select Case e.CommandName
            Case Is = "EditUser"
                lblUser.Text = "Edit User"
                Session("UserScreenMode") = "Edit"
                mvUserAdmin.ActiveViewIndex = 2
            Case Is = "DeleteUser"
                lblUser.Text = "DELETE FOLLOWING USER?"
                lblUser.ForeColor = Drawing.Color.Red
                txtUserID.Enabled = False
                txtFirst.Enabled = False
                txtLast.Enabled = False
                ddlSecurity.Enabled = False
                txtEmail.Enabled = False
                lnkSaveUser.Text = "Delete"
                lblSecLevels.Visible = False
                grdRoles.Visible = False
                Session("UserScreenMode") = "Delete"
                mvUserAdmin.ActiveViewIndex = 2
        End Select

    End Sub

    Private Sub ResetUserScreen()

        lblUser.ForeColor = Drawing.Color.Black
        txtUserID.Enabled = True
        txtFirst.Enabled = True
        txtLast.Enabled = True
        ddlSecurity.Enabled = True
        txtEmail.Enabled = True
        lnkSaveUser.Text = "Save"
        lblSecLevels.Visible = True
        grdRoles.Visible = True

    End Sub

    Private Sub ResetPayerScreen()

        lblPayer.ForeColor = Drawing.Color.Black
        txtPayer.Enabled = True
        txtDescription.Enabled = True
        lnkSavePayer.Text = "Save"

    End Sub

    Private Sub grdUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowDataBound
        Dim iCurrentSecurityLevel As Integer = HttpContext.Current.Session("Security")
        Dim sCurrentUserName As String = HttpContext.Current.Session("User")
        Dim iUserSecurityLevel As Integer
        Dim sUserName As String
        Dim lnkEbtn As LinkButton
        Dim lnkDbtn As LinkButton

        If e.Row.RowType = DataControlRowType.DataRow Then
            iUserSecurityLevel = CInt(e.Row.Cells(ucolumns.SecurityRole).Text)  'Get security level for the user in the gridview
            sUserName = e.Row.Cells(ucolumns.WindowsID).Text  'get security level for the user in the gridview
            lnkEbtn = e.Row.Cells(0).Controls(0)  'get Edit link button
            lnkDbtn = e.Row.Cells(10).Controls(0)  'get the Delete link button
            'if current user in this row has higher security then the current person editing users, then disable the edit and delete buttons.
            If iUserSecurityLevel < iCurrentSecurityLevel Then
                lnkEbtn.Enabled = False
                lnkDbtn.Enabled = False
            End If
            'If the current security is not 0, and if current user and the user in the is row are the same,
            'then disable the edit and delete buttons so they can edit or delete themselves.
            If iCurrentSecurityLevel > 0 And sUserName = sCurrentUserName Then
                lnkEbtn.Enabled = False
                lnkDbtn.Enabled = False
            End If

        End If

    End Sub
    Protected Sub lnkCancelUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelUser.Click
        lblDataValidation.Visible = False
        'clear out the textboxes
        txtHiddenRowID.Text = ""
        txtUserID.Text = ""
        txtFirst.Text = ""
        txtLast.Text = ""
        txtEmail.Text = ""

        ResetUserScreen()

        mvUserAdmin.ActiveViewIndex = 1
    End Sub

    Protected Sub lnkSaveUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSaveUser.Click
        Dim sCurrentUser As String = Session("User")
        Dim UserScreenMode As String = Session("UserScreenMode")
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        ResetUserScreen()
        lblDataValidation.Visible = False

        'Data Validation
        If txtUserID.Text = "" Then
            lblDataValidation.Text = "Windows ID is required"
            lblDataValidation.Visible = True
            Exit Sub
        End If
        If UserScreenMode = "New" And txtUserID.Text <> "" Then
            Dim dsUsers As DataSet = Session("dsUsers")
            Dim dtUsers As DataTable = dsUsers.Tables(0)
            Dim dr() As DataRow
            dr = dtUsers.Select("User_Windows_Authentication = '" & txtUserID.Text & "'")
            If dr.Length > 0 Then
                lblDataValidation.Text = txtUserID.Text & " already exists with a security of " & dr(0)("Security_RoleID")
                lblDataValidation.Visible = True
                Exit Sub
            End If
        End If

        If txtFirst.Text = "" Then
            lblDataValidation.Text = "First Name is required"
            lblDataValidation.Visible = True
            Exit Sub
        End If
        If txtLast.Text = "" Then
            lblDataValidation.Text = "Last Name is required"
            lblDataValidation.Visible = True
            Exit Sub
        End If
        If txtEmail.Text = "" Then
            lblDataValidation.Text = "Email Address is required"
            lblDataValidation.Visible = True
            Exit Sub
        End If

        'Depending on the current function, call the right stored proc.

        If UserScreenMode = "Edit" Then UpdateUser(SQLcon, txtHiddenRowID.Text, txtUserID.Text, txtFirst.Text, txtLast.Text, _
            ddlSecurity.SelectedValue, sCurrentUser, txtEmail.Text)

        If UserScreenMode = "New" Then InsertUser(SQLcon, txtUserID.Text, txtFirst.Text, txtLast.Text, ddlSecurity.SelectedValue, sCurrentUser, txtEmail.Text)

        If UserScreenMode = "Delete" Then DeleteUser(SQLcon, txtUserID.Text, sCurrentUser)

        SQLcon.Close()

        'clear out the textboxes
        txtHiddenRowID.Text = ""
        txtUserID.Text = ""
        txtFirst.Text = ""
        txtLast.Text = ""
        txtEmail.Text = ""

        PopulateUsers()
        mvUserAdmin.ActiveViewIndex = 1
    End Sub

    Protected Sub lnkAddUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAddUser.Click
        Session("UserScreenMode") = "New"
        lblUser.Text = "Add New User"
        ddlSecurity.SelectedValue = 2
        mvUserAdmin.ActiveViewIndex = 2
    End Sub

    Protected Sub lnkPayerReturn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPayerReturn.Click
        mvUserAdmin.ActiveViewIndex = 0
    End Sub

    Private Sub grdPayers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdPayers.RowCommand
        Dim currentindex As Integer
        Dim row As GridViewRow

        currentindex = Convert.ToInt32(e.CommandArgument)
        row = grdPayers.Rows(currentindex)

        ResetPayerScreen()

        txtHiddenKey.Text = grdPayers.DataKeys(currentindex)("Payer_ID")
        txtPayer.Text = HttpUtility.HtmlDecode(row.Cells(pcolumns.Payer).Text)
        txtDescription.Text = HttpUtility.HtmlDecode(row.Cells(pcolumns.Description).Text)

        Select Case e.CommandName
            Case Is = "EditPayer"
                lblPayer.Text = "Edit Payer"
                Session("PayerScreenMode") = "Edit"
                mvUserAdmin.ActiveViewIndex = 4
            Case Is = "DeletePayer"
                lblPayer.Text = "DELETE FOLLOWING PAYER?"
                lblPayer.ForeColor = Drawing.Color.Red
                txtPayer.Enabled = False
                txtDescription.Enabled = False
                lnkSavePayer.Text = "Delete"
                Session("PayerScreenMode") = "Delete"
                mvUserAdmin.ActiveViewIndex = 4
        End Select


    End Sub
    Protected Sub lnkCancelPayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelPayer.Click
        lblPayerDataValidation.Visible = False
        'clear out the textboxes
        txtPayer.Text = ""
        txtDescription.Text = ""

        mvUserAdmin.ActiveViewIndex = 3

    End Sub

    Protected Sub lnkSavePayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSavePayer.Click
        Dim CurrentClient As Integer
        Try
            CurrentClient = CInt(ddlPayerClient.SelectedValue)
            Dim sCurrentUser As String = Session("User")
            Dim PayerScreenMode As String = Session("PayerScreenMode")
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            ResetPayerScreen()
            lblPayerDataValidation.Visible = False

            'Data Validation
            If txtPayer.Text = "" Then
                lblPayerDataValidation.Text = "Payer is required"
                lblPayerDataValidation.Visible = True
                Exit Sub
            End If

            'Depending on the current function, call the right stored proc.

            If PayerScreenMode = "Edit" Then UpdatePayer(SQLcon, txtHiddenKey.Text, txtPayer.Text, txtDescription.Text, sCurrentUser, CurrentClient)

            If PayerScreenMode = "New" Then InsertPayer(SQLcon, txtPayer.Text, txtDescription.Text, sCurrentUser, CurrentClient)

            SQLcon.Close()

            If PayerScreenMode = "Delete" Then
                HandleDeletingPayers(txtHiddenKey.Text, txtPayer.Text)
                Exit Sub
            End If

            'clear out the textboxes
            txtHiddenKey.Text = ""
            txtPayer.Text = ""
            txtDescription.Text = ""

            PopulatePayers()
            mvUserAdmin.ActiveViewIndex = 3
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub HandleDeletingPayers(ByVal PayerID As String, ByVal PayerName As String)
        Dim CurrentClient As Integer
        Try
            CurrentClient = CInt(ddlPayerClient.SelectedValue)
            Dim sCurrentUser As String = Session("User")
            Dim dsXmapToReplace As DataSet
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            Dim RecordCountWithPayer As Integer

            'First see if there are any records in the database that contain the payer they want to delete.
            RecordCountWithPayer = GetRecordCountWithPayer(SQLcon, PayerID)
            'And see if there are any crossmap entries that need to be remapped.
            dsXmapToReplace = GetPayersXMapToReplace(SQLcon, PayerID, CurrentClient)

            'If the RecordCountWithPayer > 0 then prompt the user for the correct payer to change it to before deleting this payer.
            'Or if there are values mapped to the payer they want to delete, require them to recrossmap those values before deleting the payer.
            If RecordCountWithPayer > 0 Or dsXmapToReplace.Tables(0).Rows.Count > 0 Then
                'clear out the textboxes
                txtHiddenKey.Text = ""
                txtPayer.Text = ""
                txtDescription.Text = ""
                txtRemapHiddenKey.Text = PayerID
                lblRemapMessage.Text = "There are currently " & RecordCountWithPayer & " records containing payer " & PayerName & "."
                lblXmap.Text = "The following entries are all Cross Mapped to " & PayerName & ".  Please select a new Cross Map value."
                LoadPayerDropdown(ddlRemapPayers, CurrentClient, True)
                grdXmapToReplace.DataSource = dsXmapToReplace
                grdXmapToReplace.DataBind()
                mvUserAdmin.ActiveViewIndex = 10
            Else
                'clear out the textboxes
                txtHiddenKey.Text = ""
                txtPayer.Text = ""
                txtDescription.Text = ""
                DeletePayer(SQLcon, PayerID, sCurrentUser)
                PopulatePayers()
                mvUserAdmin.ActiveViewIndex = 3
            End If

            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub lnkAddPayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAddPayer.Click
        Session("PayerScreenMode") = "New"
        lblPayer.Text = "Add New Payer"
        mvUserAdmin.ActiveViewIndex = 4
    End Sub
    Protected Sub lnkPayerXmap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPayerXmap.Click
        Dim dsClients As DataSet
        Dim li As ListItem
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        ddlClient.Items.Clear()
        dsClients = GetClients(SQLcon)
        For Each dr As DataRow In dsClients.Tables(0).Rows
            li = New ListItem
            li.Value = dr("Master_Client_Number").ToString
            li.Text = dr("Client_Name").ToString
            ddlClient.Items.Add(li)
        Next
        ddlType.SelectedValue = ""
        SQLcon.Close()
        mvUserAdmin.ActiveViewIndex = 5
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton1.Click
        lblCriteriaError.Visible = False
        If ddlType.Text = "" Then
            lblCriteriaError.Text = "Please select a Type"
            lblCriteriaError.Visible = True
            Exit Sub
        End If
        lblClient.Text = "Client:  " & ddlClient.SelectedItem.Text
        lblType.Text = "Crossmap Type:  " & ddlType.SelectedItem.Text '
        Session("CurrentCrossmapType") = ddlType.SelectedItem.Text
        Session("CurrentCrossmapClient") = ddlClient.SelectedValue
        PopulateXmap()
        mvUserAdmin.ActiveViewIndex = 6

    End Sub

    Protected Sub PopulateXmap()
        Dim dsXmap As DataSet
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        dsXmap = GetXmap(SQLcon, ddlClient.SelectedValue, ddlType.SelectedValue)
        grdXmap.DataSource = dsXmap
        grdXmap.DataBind()
        SQLcon.Close()

    End Sub
    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton2.Click
        mvUserAdmin.ActiveViewIndex = 0
    End Sub

    Private Sub grdXmap_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdXmap.RowCommand
        Dim currentindex As Integer
        Dim row As GridViewRow
        Dim CurrentClient As Integer
        Try
            CurrentClient = CInt(ddlClient.SelectedValue)
            ResetXmapScreen()
            LoadPayerDropdown(ddlSystem, CurrentClient, True)

            currentindex = Convert.ToInt32(e.CommandArgument)
            row = grdXmap.Rows(currentindex)

            txtHiddenXmapID.Text = grdXmap.DataKeys(currentindex)("Payer_CrossmapID")
            txtSource.Text = HttpUtility.HtmlDecode(row.Cells(xcolumns.Source).Text)
            ddlSystem.SelectedValue = grdXmap.DataKeys(currentindex)("System_Value").ToString

            Select Case e.CommandName
                Case Is = "EditPayer"
                    lblEditDelMap.Text = "Edit Crossmap Values"
                    Session("EditDelMapMode") = "Edit"
                    mvUserAdmin.ActiveViewIndex = 7
                Case Is = "DeletePayer"
                    lblEditDelMap.Text = "DELETE FOLLOWING CROSSMAP ENTRY?"
                    lblEditDelMap.ForeColor = Drawing.Color.Red
                    txtSource.Enabled = False
                    ddlSystem.Enabled = False
                    lnkSaveXmap.Text = "Delete Mapping"
                    Session("EditDelMapMode") = "Delete"
                    mvUserAdmin.ActiveViewIndex = 7
            End Select
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub ResetXmapScreen()
        txtSource.Text = ""
        ddlSystem.Items.Clear()
        txtSource.Enabled = True
        ddlSystem.Enabled = True
        lnkSaveXmap.Text = "Save Mapping"
    End Sub

    Protected Sub LoadPayerDropdown(ByRef ddl As DropDownList, ByVal CurrentClient As Integer, Optional ByVal InlcudeBlankLine As Boolean = False, _
                                    Optional ByVal ExcludeValue As String = "")

        Dim dsPayers As DataSet
        Dim li As ListItem
        Dim listtext As String

        ddl.Items.Clear()
        li = New ListItem
        li.Text = ""
        li.Value = ""
        If InlcudeBlankLine Then ddl.Items.Add(li)

        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        dsPayers = GetAllPayers(SQLcon, CurrentClient)

        SQLcon.Close()

        For Each dr As DataRow In dsPayers.Tables(0).Rows
            If Not dr("Logical_Active") Is DBNull.Value Then
                If dr("Logical_Active") = 0 Then Continue For
            End If
            If ExcludeValue <> "" Then
                If ExcludeValue = dr("Payer_ID") Then Continue For
            End If
            li = New ListItem
            listtext = dr("Payer").ToString
            If Not dr("Description") Is DBNull.Value Then
                If Not dr("Description").ToString = "" Then listtext = listtext & ":  " & dr("Description").ToString
            End If
            li.Text = listtext
            li.Value = dr("Payer_ID").ToString
            ddl.Items.Add(li)
        Next

    End Sub
    Protected Sub lnkSaveXmap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSaveXmap.Click
        Try
            Dim sCurrentUser As String = Session("User")
            Dim EditDelMapMode As String = Session("EditDelMapMode")
            Dim strRepoitoryClientID As String = GetRepositoryClientID(CInt(Session("CurrentCrossmapClient").ToString))
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            lblXmapVerification.Visible = False

            'Data Validation
            If EditDelMapMode = "Edit" Then
                If txtSource.Text = "" Then
                    lblXmapVerification.Text = "Source Value is required"
                    lblXmapVerification.Visible = True
                    Exit Sub
                End If
                If ddlSystem.Text = "" Then
                    lblXmapVerification.Text = "System Value is required"
                    lblXmapVerification.Visible = True
                    Exit Sub
                End If
            End If

            'Depending on the current function, call the right stored proc.

            If EditDelMapMode = "Edit" Then UpdateXmap(SQLcon, txtHiddenXmapID.Text, txtSource.Text, ddlSystem.SelectedValue, Session("CurrentCrossmapType").ToString, _
                                                strRepoitoryClientID, sCurrentUser)

            If EditDelMapMode = "Delete" Then DeleteXmap(SQLcon, txtHiddenXmapID.Text, sCurrentUser)

            SQLcon.Close()

            ResetXmapScreen()

            'clear out the textboxes
            txtSource.Text = ""
            ddlSystem.Items.Clear()

            PopulateXmap()
            mvUserAdmin.ActiveViewIndex = 6
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkCancelXmap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelXmap.Click

        ResetXmapScreen()
        txtSource.Text = ""

        mvUserAdmin.ActiveViewIndex = 6
    End Sub

    Protected Sub lnkAddEntries_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAddEntries.Click
        Dim dt As New DataTable
        Dim nr As DataRow

        'build a dummy table to put some entries in the gridview for people to enter their crossmap values.
        dt.Columns.Add("Source")
        dt.Columns.Add("System")
        For x = 1 To 150
            nr = dt.NewRow
            dt.Rows.Add(nr)
        Next

        grdAddXmap.DataSource = dt
        grdAddXmap.DataBind()

        lblAddError.Visible = False
        lblClientAdd.Text = lblClient.Text
        lblClientAddBulk.Text = lblClient.Text
        lblClientTypeAdd.Text = lblType.Text
        lblClientTypeAddBulk.Text = lblType.Text
        mvUserAdmin.ActiveViewIndex = 8

    End Sub

    Private Sub grdAddXmap_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAddXmap.RowDataBound
        Dim ddl As DropDownList

        If e.Row.RowType = DataControlRowType.DataRow Then
            ddl = e.Row.Cells(1).FindControl("ddlSystemAdd")  'get drop down list
            'LoadPayerDropdown(ddl, True)
            GetPayerInformations(ddl)
        End If


    End Sub
    Protected Sub lnkCancelAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelAdd.Click

        lblAddError.Visible = False
        mvUserAdmin.ActiveViewIndex = 6
    End Sub

    Protected Sub lnkSaveAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSaveAdd.Click
        Dim tb As TextBox
        Dim ddl As DropDownList
        Dim sSourceVal As String
        Dim sSystemVal As String
        Try
            Dim sCurrentUser As String = Session("User")
            Dim sCurrentType As String = Session("CurrentCrossmapType").ToString

            Dim strRepoitoryClientID As String = GetRepositoryClientID(CInt(Session("CurrentCrossmapClient").ToString))
            'make sure if they typed in a source value, that they also included a System Value
            For Each gvr As GridViewRow In grdAddXmap.Rows
                tb = gvr.Cells(0).FindControl("txtSourceAdd")
                tb.ForeColor = Drawing.Color.Black
                If tb.Text = "" Then Continue For
                ddl = gvr.Cells(0).FindControl("ddlSystemAdd")
                ddl.ForeColor = Drawing.Color.Black
                If ddl.SelectedValue = "" Then
                    tb.ForeColor = Drawing.Color.Red
                    lblAddError.Visible = True
                    lblAddError.Text = "Please make sure you have selected a System Value for each Source Value entered."
                    Exit Sub
                End If
            Next

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            For Each gvr As GridViewRow In grdAddXmap.Rows
                tb = gvr.Cells(0).FindControl("txtSourceAdd")
                sSourceVal = tb.Text
                If sSourceVal = "" Then Continue For
                ddl = gvr.Cells(1).FindControl("ddlSystemAdd")
                sSystemVal = ddl.SelectedValue.ToString
                InsertXmap(SQLcon, sSourceVal, sSystemVal, sCurrentType, strRepoitoryClientID, sCurrentUser)
            Next
            SQLcon.Close()
            PopulateXmap()

            mvUserAdmin.ActiveViewIndex = 6
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub cmdImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdImport.Click
        Dim constr As String = ""
        Dim ext As String
        Dim filepath As String
        Dim query As String
        Dim conn As OleDbConnection
        Dim cmd As OleDbCommand
        Dim da As OleDbDataAdapter
        Dim ds As DataSet
        Dim bSource As Boolean
        Dim bSystem As Boolean
        Dim SysVal As String
        Dim SysNumeric As String
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        Dim iNotFound As Integer = 0
        Dim CurrentClient As Integer

        lblFileType.Visible = False
        Try
            'make sure they picked an excel file.
            CurrentClient = CInt(ddlClient.SelectedValue)
            If FileUpload1.HasFile Then
                If Not (FileUpload1.PostedFile.ContentType = "application/vnd.ms-excel" Or
                   FileUpload1.PostedFile.ContentType = "application/excel" Or
                   FileUpload1.PostedFile.ContentType = "application/x-msexcel" Or
                   FileUpload1.PostedFile.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") Then
                    lblFileType.Text = "Either this is not an Excel File, or the file is currently in use."
                    lblFileType.Visible = True
                    Exit Sub
                End If
            End If

            'get extension
            ext = Path.GetExtension(FileUpload1.FileName).ToLower
            'get path
            filepath = Server.MapPath("~/Uploads/" & FileUpload1.FileName)
            'upload file
            FileUpload1.SaveAs(filepath)
            'set the connection string
            If ext = "xls" Then
                constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";" + "Extended Properties=Excel 8.0;"

            Else
                constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";" + "Extended Properties=Excel 12.0 Xml;"
            End If
            'set the query
            query = "SELECT * FROM [Sheet1$]"
            'get a connection
            conn = New OleDbConnection(constr)
            'open connection
            If conn.State = ConnectionState.Closed Then conn.Open()
            'create command object
            cmd = New OleDbCommand(query, conn)
            'create data adapter
            da = New OleDbDataAdapter(cmd)
            'create dataset
            ds = New DataSet
            'fill dataset
            da.Fill(ds)

            'make sure the dataset contains the two required columns, Source and System
            For x = 0 To ds.Tables(0).Columns.Count - 1
                If ds.Tables(0).Columns(x).ColumnName = "Source" Then bSource = True
                If ds.Tables(0).Columns(x).ColumnName = "System" Then bSystem = True
            Next

            'if the two required columns aren't in there, display an error.
            If bSource = False Or bSystem = False Then
                lblFileType.Text = "The file must contain a column called Source, and a column called System"
                lblFileType.Visible = True
                Exit Sub
            End If

            'get the system payers so we can look up the internal number
            Dim dsPayers As DataSet
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsPayers = GetAllPayers(SQLcon, CurrentClient)
            SQLcon.Close()

            'add a column to the dataset for the internal value
            Dim dc As New DataColumn
            dc.ColumnName = "Value"
            ds.Tables(0).Columns.Add(dc)

            'look up each payer sent in, and populate the Value column in the table.k
            For Each dr As DataRow In ds.Tables(0).Rows
                SysVal = dr("System")
                SysNumeric = Lookup(dsPayers, SysVal)
                dr("Value") = SysNumeric
                If SysNumeric = "" Then
                    dr("System") = SysVal & "(Payer Not Found)"
                    iNotFound += 1
                End If
            Next
            If iNotFound > 0 Then
                lblBulkError.Text = "There were " & iNotFound & " entries mapped to payers that don't exist.  These will not be filed.  " & _
                    "Press cancel to correct the file and import it again."
                lblBulkError.Visible = True
            End If

            grdAddBulk.DataSource = ds
            grdAddBulk.DataBind()
            mvUserAdmin.ActiveViewIndex = 9
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Function Lookup(ByRef dspayers As DataSet, ByVal sysval As String) As String
        Dim dt As DataTable
        Dim dr() As DataRow

        dt = dspayers.Tables(0)
        dr = dt.Select("Payer = '" & sysval & "'")
        If dr.Length = 0 Then
            Return ""
            Exit Function
        End If
        Return dr(0)("Payer_id").ToString

    End Function
    Private Sub grdAddBulk_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAddBulk.RowDataBound
        Dim tb1 As TextBox
        Dim tb2 As TextBox
        Dim value As String
        'Dim ddl As DropDownList

        If e.Row.RowType = DataControlRowType.DataRow Then

            tb1 = e.Row.Cells(1).FindControl("txtSystemAdd")
            value = tb1.Text
            If InStr(value, "(Payer Not Found)") > 0 Then
                tb1.ForeColor = Drawing.Color.Red
                tb2 = e.Row.Cells(0).FindControl("txtSourceAdd")
                tb2.ForeColor = Drawing.Color.Red
            End If
            '    e.Row.Cells(1).Controls.Remove(tb)
            '    ddl = New DropDownList
            '    ddl.Width = 250
            '    ddl.ID = "ddlSystemAdd"
            '    LoadPayerDropdown(ddl, True)
            '    ddl.SelectedValue = value
            '    e.Row.Cells(1).Controls.Add(ddl)
        End If

    End Sub

    Protected Sub lnkCancelBulk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelBulk.Click

        lblBulkError.Visible = False
        mvUserAdmin.ActiveViewIndex = 6
    End Sub

    Protected Sub lnkSaveBulk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSaveBulk.Click
        Dim tb As TextBox
        Dim sSourceVal As String
        Dim sSystemVal As String
        Dim currentindex As Integer
        Try
            Dim sCurrentUser As String = Session("User")
            Dim sCurrentType As String = Session("CurrentCrossmapType").ToString
            Dim strRepoitoryClientID As String = GetRepositoryClientID(CInt(Session("CurrentCrossmapClient").ToString))



            'make sure if they typed in a source value, that they also included a System Value
            'they can't change the values on the bulk add screen any more.  I couldn't figure out how to make it work
            'with the drop downs, and the IDs and everything.  So we don't need the check this.
            'For Each gvr As GridViewRow In grdAddBulk.Rows
            '    tb = gvr.Cells(0).FindControl("txtSourceAdd")
            '    tb.ForeColor = Drawing.Color.Black
            '    If tb.Text = "" Then Continue For
            '    ddl = gvr.Cells(1).FindControl("ddlSystemAdd")
            '    ddl.ForeColor = Drawing.Color.Black
            '    If ddl.SelectedValue = "" Then
            '        tb.ForeColor = Drawing.Color.Red
            '        lblBulkError.Visible = True
            '        lblBulkError.Text = "Please make sure you have selected a System Value for each Source Value entered."
            '        Exit Sub
            '    End If
            'Next

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            For Each gvr As GridViewRow In grdAddBulk.Rows
                tb = gvr.Cells(0).FindControl("txtSourceAdd")
                sSourceVal = tb.Text
                If sSourceVal = "" Then Continue For
                currentindex = gvr.RowIndex
                sSystemVal = grdAddBulk.DataKeys(currentindex)("Value")
                If sSystemVal = "" Then Continue For
                InsertXmap(SQLcon, sSourceVal, sSystemVal, sCurrentType, strRepoitoryClientID, sCurrentUser)
            Next
            SQLcon.Close()

            PopulateXmap()

            mvUserAdmin.ActiveViewIndex = 6
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkCancelCrossmap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelCrossmap.Click
        mvUserAdmin.ActiveViewIndex = 0
    End Sub

    Protected Sub lnkCancelRemap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancelRemap.Click

        txtRemapHiddenKey.Text = ""
        lblRemapMessage.Text = ""
        lblRemapFailure.Visible = False

        PopulatePayers()
        mvUserAdmin.ActiveViewIndex = 3
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton3.Click
        Dim sCurrentUser As String = Session("User")
        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        If ddlRemapPayers.Text = "" Then
            lblRemapFailure.Text = "Please select a Payer to replace the one being deleted."
            lblRemapFailure.Visible = True
            Exit Sub
        End If

        'update the crossmap entries with the new selected crossmap values.
        UpdateCrossmap()

        'replace the deleted payer in the existing records, and delete the payer.
        ReplaceAndDeletePayer(SQLcon, CInt(ddlRemapPayers.SelectedValue), CInt(txtRemapHiddenKey.Text))

        SQLcon.Close()
        'clear out the fields
        txtRemapHiddenKey.Text = ""
        lblRemapMessage.Text = ""
        lblRemapFailure.Visible = False

        'repopulate payer list
        PopulatePayers()
        mvUserAdmin.ActiveViewIndex = 3

    End Sub

    Private Sub UpdateCrossmap()
        Dim ddl As DropDownList
        Dim sSourceVal As String
        Dim sSystemVal As String
        Dim sType As String
        Dim xmapID As String
        Dim sCurrentUser As String = Session("User")
        Dim sClient As String


        'make sure if they typed in a source value, that they also included a System Value
        For Each gvr As GridViewRow In grdXmapToReplace.Rows
            ddl = gvr.Cells(3).FindControl("ddlSystemAdd")
            ddl.ForeColor = Drawing.Color.Black
            If ddl.SelectedValue = "" Then
                gvr.ForeColor = Drawing.Color.Red
                lblRemapFailure.Visible = True
                lblRemapFailure.Text = "Please make sure you have selected a System Value for each entry."
                Exit Sub
            End If
        Next

        Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
        If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

        For Each gvr As GridViewRow In grdXmapToReplace.Rows
            sType = gvr.Cells(1).Text.ToString
            sSourceVal = gvr.Cells(2).Text.ToString
            ddl = gvr.Cells(3).FindControl("ddlSystemAdd")
            sSystemVal = ddl.SelectedValue.ToString
            xmapID = grdXmapToReplace.DataKeys(gvr.RowIndex)("Payer_CrossmapID").ToString()
            sClient = grdXmapToReplace.DataKeys(gvr.RowIndex)("Repository_clientID").ToString()
            UpdateXmap(SQLcon, xmapID, sSourceVal, sSystemVal, sType, sClient, sCurrentUser)
        Next
        SQLcon.Close()

    End Sub
    Private Sub grdXmapToReplace_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdXmapToReplace.RowDataBound
        Dim ddl As DropDownList
        Dim CurrentClient As Integer
        Try
            CurrentClient = CInt(ddlPayerClient.SelectedValue)
            If e.Row.RowType = DataControlRowType.DataRow Then
                ddl = e.Row.Cells(1).FindControl("ddlSystemAdd")  'get drop down list
                LoadPayerDropdown(ddl, CurrentClient, True, txtRemapHiddenKey.Text)
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub RefreshpayerInfo()
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
    Private Sub GetPayerInformations(ByRef ddl As DropDownList)
        Dim dsPayers As DataSet
        Dim li As ListItem
        Dim listtext As String
        Dim CurrentClient As Integer
        Try
            ddl.Items.Clear()
            li = New ListItem
            li.Text = ""
            li.Value = ""
            ddl.Items.Add(li)

            CurrentClient = CInt(ddlClient.SelectedValue)
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")

            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            dsPayers = GetAllPayersByClient(SQLcon, CurrentClient)

            SQLcon.Close()

            For Each dr As DataRow In dsPayers.Tables(0).Rows
                If Not dr("Logical_Active") Is DBNull.Value Then
                    If dr("Logical_Active") = 0 Then Continue For
                End If
                li = New ListItem
                listtext = dr("Payer").ToString
                If Not dr("Description") Is DBNull.Value Then
                    If Not dr("Description").ToString = "" Then listtext = listtext & ":  " & dr("Description").ToString
                End If
                li.Text = listtext
                li.Value = dr("Payer_ID").ToString
                ddl.Items.Add(li)
            Next
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Private Sub GetPayerTableMgmtClients()
        Try
            Dim dsClients As DataSet
            Dim li As ListItem
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()

            ddlPayerClient.Items.Clear()
            dsClients = GetClients(SQLcon)
            For Each dr As DataRow In dsClients.Tables(0).Rows
                li = New ListItem
                li.Value = dr("Master_Client_Number").ToString
                li.Text = dr("Client_Name").ToString
                ddlPayerClient.Items.Add(li)
            Next
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlPayerClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPayerClient.SelectedIndexChanged
        PopulatePayers()
    End Sub
    Private Function GetRepositoryClientID(ByVal intMasterClientNo As Integer)
        Dim strRepositoryClient As String = String.Empty
        Dim dsRepositoryClient As DataSet
        Try

            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            dsRepositoryClient = GetRepositoryClient(SQLcon, intMasterClientNo)
            If dsRepositoryClient.Tables(0).Rows.Count <> 0 Then
                strRepositoryClient = dsRepositoryClient.Tables(0).Rows(0)("Repository_ClientID")
            End If
            SQLcon.Close()
            Return strRepositoryClient
        Catch ex As Exception
            Return String.Empty
            ex.Message.ToString()
        End Try
    End Function
    Private Sub CheckUserAccess()
        Dim intCurrentUserRoleID As Integer
        Dim strCurrentUser As String
        Dim blnCheckUserAccess As Boolean
        Try
            strCurrentUser = Session("User")
            intCurrentUserRoleID = HttpContext.Current.Session("Security")
            Dim SQLcon As SqlClient.SqlConnection = HttpContext.Current.Session("SQLcon")
            If Not SQLcon.State = ConnectionState.Open Then SQLcon.Open()
            blnCheckUserAccess = GetUserAccessByRoleID(SQLcon, intCurrentUserRoleID, strCurrentUser)
            If blnCheckUserAccess = True Then
                lnkAppMgnt.Visible = True
            Else
                lnkAppMgnt.Visible = False
            End If
            SQLcon.Close()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

   
    Protected Sub lnkAppMgnt_Click(sender As Object, e As EventArgs) Handles lnkAppMgnt.Click
        Try
            Server.Transfer("ApplicationManagement.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkDataMgnt_Click(sender As Object, e As EventArgs) Handles lnkDataMgnt.Click
        Try
            Server.Transfer("DataManagement.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LinkPostedDataDeletion_Click(sender As Object, e As EventArgs) Handles LinkPostedDataDeletion.Click
        Try
            Server.Transfer("PostedDataDeletion.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LinkPostedDataCorrection_Click(sender As Object, e As EventArgs) Handles LinkPostedDataCorrection.Click
        Try
            Server.Transfer("PostedDataCorrection.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LinkPostedDataAddition_Click(sender As Object, e As EventArgs) Handles LinkPostedDataAddition.Click
        Try
            Server.Transfer("PostedDataAdditionNew.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LinkPostedDataAdditionTrial_Click(sender As Object, e As EventArgs) Handles LinkPostedTrial.Click
        Try
            Server.Transfer("PostedDataAdditionTrial.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LinkPostedDataAdditionNew_Click(sender As Object, e As EventArgs) Handles PostedDataAdditionNew.Click
        Try
            Server.Transfer("PostedDataAdditionNew.aspx")
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
End Class