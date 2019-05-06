Module DataBase

    Public Function SecuritySpecificUser(ByVal sqlcon As SqlClient.SqlConnection, ByVal user As String) As Integer
        Dim dsUser As DataSet
        Dim Security As Integer = 999

        Dim cmdUserSecurity As New SqlClient.SqlCommand("Process.usp_UsersGet", sqlcon)
        cmdUserSecurity.Parameters.Add(New SqlClient.SqlParameter("@Windows_Authentication", SqlDbType.VarChar)).Value = user
        cmdUserSecurity.CommandType = CommandType.StoredProcedure
        cmdUserSecurity.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdUserSecurity)
        dsUser = New DataSet
        da.Fill(dsUser)

        If dsUser.Tables(0).Rows.Count > 0 Then
            Security = dsUser.Tables(0).Rows(0)("Security_RoleID")
        End If

        Return Security
    End Function


    Public Function AllUsers(ByVal sqlcon As SqlClient.SqlConnection) As DataSet
        Dim dsUsers As DataSet

        Dim cmdUsers As New SqlClient.SqlCommand("Process.usp_UsersGetAll", sqlcon)
        cmdUsers.CommandType = CommandType.StoredProcedure
        cmdUsers.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdUsers)
        dsUsers = New DataSet
        da.Fill(dsUsers)
        Return dsUsers
    End Function

    Public Sub UpdateUser(ByVal sqlcon As SqlClient.SqlConnection, ByVal RowID As String, ByVal WindowsID As String, ByVal First As String, ByVal Last As String, _
                               ByVal RoleID As Integer, ByVal LastUpdateUser As String, ByVal Email As String)

        Dim cmdUpdateUser As New SqlClient.SqlCommand("Process.usp_UserUpdate", sqlcon)
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@UserRowID", SqlDbType.Int)).Value = CInt(RowID)
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@WindowsAuthentication", SqlDbType.VarChar)).Value = WindowsID
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@FirstName", SqlDbType.VarChar)).Value = First
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@LastName", SqlDbType.VarChar)).Value = Last
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@RoleID", SqlDbType.Int)).Value = CInt(RoleID)
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@LastUpdatedUser", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@EmailAddress", SqlDbType.VarChar)).Value = Email
        cmdUpdateUser.CommandType = CommandType.StoredProcedure
        cmdUpdateUser.CommandTimeout = 0
        cmdUpdateUser.ExecuteNonQuery()

    End Sub

    Public Sub InsertUser(ByVal sqlcon As SqlClient.SqlConnection, ByVal WindowsID As String, ByVal First As String, ByVal Last As String, _
                          ByVal roleID As Integer, ByVal CreatedUser As String, ByVal Email As String)

        Dim cmdUpdateUser As New SqlClient.SqlCommand("Process.usp_UsersInsert", sqlcon)
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@WindowsAuthentication", SqlDbType.VarChar)).Value = WindowsID
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@First_Name", SqlDbType.VarChar)).Value = First
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@Last_Name", SqlDbType.VarChar)).Value = Last
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@RoleID", SqlDbType.Int)).Value = CInt(roleID)
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@CreatedUser", SqlDbType.VarChar)).Value = CreatedUser
        cmdUpdateUser.Parameters.Add(New SqlClient.SqlParameter("@EmailAddress", SqlDbType.VarChar)).Value = Email
        cmdUpdateUser.CommandType = CommandType.StoredProcedure
        cmdUpdateUser.CommandTimeout = 0
        cmdUpdateUser.ExecuteNonQuery()

    End Sub

    Public Sub DeleteUser(ByVal sqlcon As SqlClient.SqlConnection, ByVal WindowsID As String, ByVal LastUpdateUser As String)
        Dim cmdDeleteUser As New SqlClient.SqlCommand("Process.usp_UsersDelete", sqlcon)
        cmdDeleteUser.Parameters.Add(New SqlClient.SqlParameter("@User_Windows_Authentication", SqlDbType.VarChar)).Value = WindowsID
        cmdDeleteUser.Parameters.Add(New SqlClient.SqlParameter("@LastUpdatedUser", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdDeleteUser.CommandType = CommandType.StoredProcedure
        cmdDeleteUser.CommandTimeout = 0
        cmdDeleteUser.ExecuteNonQuery()

    End Sub

    Public Function PossibleSecurityRoles(ByVal SQLcon As SqlClient.SqlConnection, ByVal iCurrentSecurityLevel As Integer) As DataTable
        Dim dsSecRoles As DataSet
        Dim dtSecRoles As DataTable

        Dim cmdSec As New SqlClient.SqlCommand("Process.usp_Security_RolesGetAll", SQLcon)
        cmdSec.CommandType = CommandType.StoredProcedure
        cmdSec.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdSec)
        dsSecRoles = New DataSet
        da.Fill(dsSecRoles)

        dtSecRoles = dsSecRoles.Tables(0)
        If iCurrentSecurityLevel = 0 Then
            Return dtSecRoles
            Exit Function
        End If

        For Each dr As DataRow In dtSecRoles.Rows
            If dr("Security_RoleID") < iCurrentSecurityLevel Then dr.Delete()
        Next
        Return dtSecRoles
    End Function

    Public Function GetAllPayers(ByVal sqlcon As SqlClient.SqlConnection, ByVal CurrentClient As Integer) As DataSet
        Dim dsPayers As DataSet

        Dim cmdPayers As New SqlClient.SqlCommand("Process.usp_PayersGetAll", sqlcon)
        cmdPayers.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdPayers.CommandType = CommandType.StoredProcedure
        cmdPayers.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdPayers)
        dsPayers = New DataSet
        da.Fill(dsPayers)

        Return dsPayers

    End Function

    Public Function Summary(ByVal sqlcon As SqlClient.SqlConnection) As DataSet
        Dim dsSummary As DataSet

        Dim cmdSummary As New SqlClient.SqlCommand("Process.usp_Bank_SummaryGet", sqlcon)
        cmdSummary.CommandType = CommandType.StoredProcedure
        cmdSummary.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdSummary)
        dsSummary = New DataSet
        da.Fill(dsSummary)

        Return dsSummary

    End Function

    Public Function LockboxVariance(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsLockbox As DataSet

        Dim cmdLockbox As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_CheckGet", sqlcon)
        cmdLockbox.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdLockbox.CommandType = CommandType.StoredProcedure
        cmdLockbox.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdLockbox)
        dsLockbox = New DataSet
        da.Fill(dsLockbox)

        Return dsLockbox

    End Function
    Public Function EFTVariance(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsEFT As DataSet

        Dim cmdEFT As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_EFTGet", sqlcon)
        cmdEFT.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdEFT.CommandType = CommandType.StoredProcedure
        cmdEFT.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdEFT)
        dsEFT = New DataSet
        da.Fill(dsEFT)

        Return dsEFT


    End Function
    Public Function CreditCardVariance(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsCC As DataSet

        Dim cmdCC As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_CCGet", sqlcon)
        cmdCC.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdCC.CommandType = CommandType.StoredProcedure
        cmdCC.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdCC)
        dsCC = New DataSet
        da.Fill(dsCC)

        Return dsCC


    End Function
    Public Function ERAVariance(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsERA As DataSet

        Dim cmdERA As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_ERAGet", sqlcon)
        cmdERA.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdERA.CommandType = CommandType.StoredProcedure
        cmdERA.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdERA)
        dsERA = New DataSet
        da.Fill(dsERA)

        Return dsERA


    End Function
    Public Function NonERAVariance(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsNonERA As DataSet

        Dim cmdNonERA As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_NONERAGet", sqlcon)
        cmdNonERA.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdNonERA.CommandType = CommandType.StoredProcedure
        cmdNonERA.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdNonERA)
        dsNonERA = New DataSet
        da.Fill(dsNonERA)

        Return dsNonERA


    End Function

    Public Function Other(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsOther As DataSet

        Dim cmdOther As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_Other", sqlcon)
        cmdOther.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdOther.CommandType = CommandType.StoredProcedure
        cmdOther.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdOther)
        dsOther = New DataSet
        da.Fill(dsOther)

        Return dsOther

    End Function


    Public Function BankDataLoadError(ByVal sqlcon As SqlClient.SqlConnection, ByVal CurrentClient As Integer) As DataSet
        Dim dsdataError As DataSet

        Dim cmddataError As New SqlClient.SqlCommand("Process.usp_Bank_Data_LoadErrorGet", sqlcon)
        cmddataError.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient     'Newely added ClientID.
        cmddataError.CommandType = CommandType.StoredProcedure
        cmddataError.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmddataError)
        dsdataError = New DataSet
        da.Fill(dsdataError)

        Return dsdataError


    End Function

    Public Sub UpdatePayer(ByVal sqlcon As SqlClient.SqlConnection, ByVal PayerID As String, ByVal Payer As String, _
                           ByVal Description As String, ByVal LastUpdateUser As String, ByVal CurrentClient As Integer)

        Dim cmdUpdatepayer As New SqlClient.SqlCommand("Process.usp_PayersUpdate", sqlcon)
        cmdUpdatepayer.Parameters.Add(New SqlClient.SqlParameter("@Payer_ID", SqlDbType.Int)).Value = CInt(PayerID)
        cmdUpdatepayer.Parameters.Add(New SqlClient.SqlParameter("@Payer", SqlDbType.VarChar)).Value = Payer
        cmdUpdatepayer.Parameters.Add(New SqlClient.SqlParameter("@Last_Update_User", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdUpdatepayer.Parameters.Add(New SqlClient.SqlParameter("@Description", SqlDbType.VarChar)).Value = Description
        cmdUpdatepayer.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdatepayer.CommandType = CommandType.StoredProcedure
        cmdUpdatepayer.CommandTimeout = 0
        cmdUpdatepayer.ExecuteNonQuery()

    End Sub

    Public Sub InsertPayer(ByVal sqlcon As SqlClient.SqlConnection, ByVal Payer As String, _
                           ByVal Description As String, ByVal CreatedUser As String, ByVal CurrentClient As Integer)

        Dim cmdUpdatePayer As New SqlClient.SqlCommand("Process.usp_PayersInsert", sqlcon)
        cmdUpdatePayer.Parameters.Add(New SqlClient.SqlParameter("@Payer", SqlDbType.VarChar)).Value = Payer
        cmdUpdatePayer.Parameters.Add(New SqlClient.SqlParameter("@CreatedUser", SqlDbType.VarChar)).Value = CreatedUser
        cmdUpdatePayer.Parameters.Add(New SqlClient.SqlParameter("@Description", SqlDbType.VarChar)).Value = Description
        cmdUpdatePayer.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdatePayer.CommandType = CommandType.StoredProcedure
        cmdUpdatePayer.CommandTimeout = 0
        cmdUpdatePayer.ExecuteNonQuery()

    End Sub

    Public Sub DeletePayer(ByVal sqlcon As SqlClient.SqlConnection, ByVal PayerID As String, ByVal LastUpdateUser As String)

        Dim cmdDeletePayer As New SqlClient.SqlCommand("Process.usp_PayersDelete", sqlcon)
        cmdDeletePayer.Parameters.Add(New SqlClient.SqlParameter("@Payer_ID", SqlDbType.Int)).Value = CInt(PayerID)
        cmdDeletePayer.Parameters.Add(New SqlClient.SqlParameter("@LastUpdatedUser", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdDeletePayer.CommandType = CommandType.StoredProcedure
        cmdDeletePayer.CommandTimeout = 0
        cmdDeletePayer.ExecuteNonQuery()

    End Sub

    Public Function GetClients(ByVal sqlcon As SqlClient.SqlConnection) As DataSet
        Dim dsClients As DataSet

        Dim cmdGetClients As New SqlClient.SqlCommand("Process.usp_ClientsGetList", sqlcon)
        cmdGetClients.CommandType = CommandType.StoredProcedure
        cmdGetClients.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdGetClients)
        dsClients = New DataSet
        da.Fill(dsClients)

        Return dsClients

    End Function

    Public Function GetXmap(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientmasterID As Integer, ByVal type As String) As DataSet
        Dim dsXmap As DataSet

        Dim cmdGetXmap As New SqlClient.SqlCommand("Process.usp_PayersXMapGetByClientID", sqlcon)
        cmdGetXmap.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientmasterID
        cmdGetXmap.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = type
        cmdGetXmap.CommandType = CommandType.StoredProcedure
        cmdGetXmap.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdGetXmap)
        dsXmap = New DataSet
        da.Fill(dsXmap)

        'remove inactive rows.
        For Each dr As DataRow In dsXmap.Tables(0).Rows
            If Not dr("LogicalActive") Is DBNull.Value Then
                If dr("LogicalActive") = 0 Then dr.Delete()
            End If
        Next

        Return dsXmap

    End Function

    Public Sub UpdateXmap(ByVal sqlcon As SqlClient.SqlConnection, ByVal mapID As String, ByVal Source As String, _
                       ByVal System As String, ByVal Type As String, ByVal Client As String, ByVal LastUpdateUser As String)

        Dim cmdUpdatepayerxmap As New SqlClient.SqlCommand("Process.usp_PayerXmapUpdate", sqlcon)
        cmdUpdatepayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Crossmap_ID", SqlDbType.Int)).Value = CInt(mapID)
        cmdUpdatepayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Client", SqlDbType.Int)).Value = CInt(Client)
        cmdUpdatepayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Source", SqlDbType.VarChar)).Value = Source
        cmdUpdatepayerxmap.Parameters.Add(New SqlClient.SqlParameter("@System", SqlDbType.Int)).Value = CInt(System)
        cmdUpdatepayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdUpdatepayerxmap.Parameters.Add(New SqlClient.SqlParameter("@User", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdUpdatepayerxmap.CommandType = CommandType.StoredProcedure
        cmdUpdatepayerxmap.CommandTimeout = 0
        cmdUpdatepayerxmap.ExecuteNonQuery()

    End Sub

    Public Sub DeleteXmap(ByVal sqlcon As SqlClient.SqlConnection, ByVal mapID As String, ByVal LastUpdateUser As String)

        Dim cmdDeletePayerxmap As New SqlClient.SqlCommand("Process.usp_PayerXmapDelete", sqlcon)
        cmdDeletePayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Xmap_ID", SqlDbType.Int)).Value = CInt(mapID)
        cmdDeletePayerxmap.Parameters.Add(New SqlClient.SqlParameter("@LastUpdatedUser", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdDeletePayerxmap.CommandType = CommandType.StoredProcedure
        cmdDeletePayerxmap.CommandTimeout = 0
        cmdDeletePayerxmap.ExecuteNonQuery()

    End Sub

    Public Sub InsertXmap(ByVal sqlcon As SqlClient.SqlConnection, ByVal Source As String, ByVal System As String, _
                          ByVal Type As String, ByVal Client As String, ByVal LastUpdateUser As String)

        Dim cmdInsertpayerxmap As New SqlClient.SqlCommand("Process.usp_PayerXmapInsert", sqlcon)
        cmdInsertpayerxmap.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CInt(Client)
        cmdInsertpayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Source", SqlDbType.VarChar)).Value = Source
        cmdInsertpayerxmap.Parameters.Add(New SqlClient.SqlParameter("@System", SqlDbType.Int)).Value = CInt(System)
        cmdInsertpayerxmap.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdInsertpayerxmap.Parameters.Add(New SqlClient.SqlParameter("@CreatedUser", SqlDbType.VarChar)).Value = LastUpdateUser
        cmdInsertpayerxmap.CommandType = CommandType.StoredProcedure
        cmdInsertpayerxmap.CommandTimeout = 0
        cmdInsertpayerxmap.ExecuteNonQuery()

    End Sub
    'Bharat 
#Region "Posted Data Load/Insert/Update"
    Public Function LoadPostedData(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDeptDt As Date, ByVal Check_Number As String, ByVal Type As String, _
                                   ByVal IsMatched As Integer, ByVal CurrentClient As Integer) As DataSet
        Dim dsPostData As DataSet

        Dim cmdPostLoad As New SqlClient.SqlCommand("Process.usp_Posted_DataGet", sqlcon)
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@BankDeptDate", SqlDbType.Date)).Value = BankDeptDt
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check_Number
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@IsMatched ", SqlDbType.TinyInt)).Value = IsMatched
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@ClientID ", SqlDbType.Int)).Value = CurrentClient
        cmdPostLoad.CommandType = CommandType.StoredProcedure
        cmdPostLoad.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdPostLoad)
        dsPostData = New DataSet
        da.Fill(dsPostData)

        Return dsPostData

    End Function
    Public Sub InsertPostData(ByVal sqlcon As SqlClient.SqlConnection, ByVal BatchID As Integer, ByVal Invoice As Integer, ByVal Bankdeptdt As Date, _
                                        ByVal Closingdt As Date, ByVal CreatedDt As Date, ByVal Designation As String, ByVal Type As String, ByVal Payamt As Double, _
                                        ByVal Paycode As String, ByVal Postddt As Integer, ByVal Check As String, ByVal UpdateUser As String, ByVal UpdateDate As Date, _
                                        ByVal CurrentClient As Integer)

        Dim cmdInsert As New SqlClient.SqlCommand("Process.usp_Posted_DataInsert", sqlcon)
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Client_Number", SqlDbType.BigInt)).Value = vbNull
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Batch_Number", SqlDbType.BigInt)).Value = BatchID
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Bank_Deposit_Date", SqlDbType.Date)).Value = Bankdeptdt
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Closing_Date", SqlDbType.Date)).Value = Closingdt
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Creation_Date", SqlDbType.Date)).Value = CreatedDt
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Invoice_Number", SqlDbType.BigInt)).Value = Invoice
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Payment_Amount", SqlDbType.Float)).Value = Payamt
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Pay_Code", SqlDbType.VarChar)).Value = Paycode
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@EDM_Number", SqlDbType.BigInt)).Value = vbNull
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Data_Type", SqlDbType.VarChar)).Value = Type
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Payer", SqlDbType.VarChar)).Value = vbNull
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Update_User", SqlDbType.VarChar)).Value = UpdateUser
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Update_Date", SqlDbType.Date)).Value = UpdateDate
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Posted_Date", SqlDbType.Int)).Value = Postddt
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Record_Status", SqlDbType.VarChar)).Value = 2
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.CommandTimeout = 0
        cmdInsert.ExecuteNonQuery()

    End Sub
    'Public Sub UpdatePostData(ByVal sqlcon As SqlClient.SqlConnection, ByVal Number As Integer, ByVal BatchID As Integer, ByVal Invoice As Integer, ByVal Bankdeptdt As Date, _
    '                                    ByVal Closingdt As Date, ByVal CreatedDt As Date, ByVal Designation As String, ByVal Type As String, ByVal Payamt As Double, _
    '                                    ByVal Paycode As Integer, ByVal Postddt As Integer, ByVal Check As String, ByVal UpdateUser As String, ByVal UpdateDate As Date)
    Public Sub UpdatePostData(ByVal sqlcon As SqlClient.SqlConnection, ByVal Number As Integer, ByVal Bankdeptdt As Date, _
                                        ByVal Designation As String, ByVal Type As String, _
                                        ByVal Check As String, ByVal UpdateUser As String, ByVal UpdateDate As Date, _
                                        ByVal CurrentClient As Integer)
        Dim cmdUpdate As New SqlClient.SqlCommand("Process.usp_Posted_DataUpdate", sqlcon)
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Posted_DataID", SqlDbType.BigInt)).Value = Number
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Bank_Deposit_Date", SqlDbType.Date)).Value = Bankdeptdt
        'cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Payment_Amount", SqlDbType.Float)).Value = Payamt
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Data_Type", SqlDbType.VarChar)).Value = Type
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Update_User", SqlDbType.VarChar)).Value = UpdateUser
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdate.CommandTimeout = 0
        cmdUpdate.ExecuteNonQuery()

    End Sub
#End Region
    Public Function LoadBankData(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDeptDt As Date, ByVal CheckNumber As String, ByVal CurrentClient As Integer, ByVal Type As String) As DataSet
        Dim dsBankData As DataSet

        Dim cmdBankLoad As New SqlClient.SqlCommand("[Process].[usp_Bank_DataGet]", sqlcon)
        cmdBankLoad.Parameters.Add(New SqlClient.SqlParameter("@Bank_Dep_Date", SqlDbType.Date)).Value = BankDeptDt
        cmdBankLoad.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = CheckNumber
        cmdBankLoad.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdBankLoad.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdBankLoad.CommandType = CommandType.StoredProcedure
        cmdBankLoad.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdBankLoad)
        dsBankData = New DataSet
        da.Fill(dsBankData)

        Return dsBankData

    End Function
    Public Function LoadPostedHeaderData(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDeptDt As Date, ByVal CheckNumber As String, ByVal CurrentClient As Integer) As DataSet
        Dim dsPostHeaderData As DataSet

        Dim cmdPostHeaderLoad As New SqlClient.SqlCommand("[Process].[usp_Posted_Data_HeaderGet]", sqlcon)
        cmdPostHeaderLoad.Parameters.Add(New SqlClient.SqlParameter("@Bank_Deposit_Date", SqlDbType.Date)).Value = BankDeptDt
        cmdPostHeaderLoad.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = CheckNumber
        cmdPostHeaderLoad.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdPostHeaderLoad.CommandType = CommandType.StoredProcedure
        cmdPostHeaderLoad.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdPostHeaderLoad)
        dsPostHeaderData = New DataSet
        da.Fill(dsPostHeaderData)

        Return dsPostHeaderData

    End Function
    Public Sub InsertBankData(ByVal sqlcon As SqlClient.SqlConnection, ByVal AccountName As String, ByVal AccountNumber As String, ByVal Amount As Double, ByVal BaiCode As String,
                                ByVal BankID As Integer, ByVal CheckNumber As String, ByVal Payer As Integer, ByVal depositDate As String, ByVal Text As String, _
                                ByVal Type As String, ByVal CurrentClient As Integer, ByVal strCreatedUser As String)

        Dim cmdInsert As New SqlClient.SqlCommand("Process.usp_Bank_Data_RepositoryInsert ", sqlcon)
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Account_Name", SqlDbType.VarChar)).Value = AccountName
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Account_Number", SqlDbType.VarChar)).Value = AccountNumber
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Amount", SqlDbType.Float)).Value = Amount
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@BAI_Code", SqlDbType.VarChar)).Value = BaiCode
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Bank_ID", SqlDbType.BigInt)).Value = BankID
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = CheckNumber
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Payer", SqlDbType.VarChar)).Value = Payer
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Deposit_Date", SqlDbType.Date)).Value = depositDate
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Text", SqlDbType.VarChar)).Value = Text
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@CreatedBy", SqlDbType.VarChar)).Value = strCreatedUser
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.CommandTimeout = 0
        cmdInsert.ExecuteNonQuery()

    End Sub
    'Public Sub InsertBankDataHolding(ByVal sqlcon As SqlClient.SqlConnection, ByVal DepositDate As Date, ByVal CheckNumber As String, _
    '                                 ByVal interval As Integer, ByVal Comments As String, ByVal UpdateUser As String)

    '    Dim cmdInsert As New SqlClient.SqlCommand("Process.usp_Bank_Data_HoldInsert ", sqlcon)
    '    cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Deposit_Date", SqlDbType.Date)).Value = DepositDate
    '    cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = CheckNumber
    '    cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Interval", SqlDbType.Int)).Value = interval
    '    cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@Comments", SqlDbType.VarChar)).Value = Comments
    '    cmdInsert.Parameters.Add(New SqlClient.SqlParameter("@UpdatedBy", SqlDbType.VarChar)).Value = UpdateUser
    '    cmdInsert.CommandType = CommandType.StoredProcedure
    '    cmdInsert.ExecuteNonQuery()

    'End Sub
    Public Sub UpdateBankDataHolding(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDataID As Integer, ByVal interval As Integer, _
                                     ByVal Comments As String, ByVal UpdateUser As String, ByVal CurrentClient As Integer)

        Dim cmdUpdate As New SqlClient.SqlCommand("Process.usp_Bank_Data_HoldUpdate ", sqlcon)
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Bank_DataID", SqlDbType.BigInt)).Value = BankDataID
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Interval", SqlDbType.Int)).Value = interval
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Comments", SqlDbType.VarChar)).Value = Comments
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@UpdatedBy", SqlDbType.VarChar)).Value = UpdateUser
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdate.CommandTimeout = 0
        cmdUpdate.ExecuteNonQuery()

    End Sub
    Public Function UpdatePostedDataMatched(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDataID As String, _
                                            ByVal PostedDataHeaderID As String, ByVal PostedDataId As String, ByVal CurrentClient As Integer) As DataSet
        Dim dsPostHeaderData As DataSet
        Dim cmdUpdateMatched As New SqlClient.SqlCommand("Process.usp_Posted_DataToMatched", sqlcon)
        cmdUpdateMatched.Parameters.Add(New SqlClient.SqlParameter("@BankDataId", SqlDbType.VarChar)).Value = BankDataID
        cmdUpdateMatched.Parameters.Add(New SqlClient.SqlParameter("@PostedDataHeaderId", SqlDbType.VarChar)).Value = PostedDataHeaderID
        cmdUpdateMatched.Parameters.Add(New SqlClient.SqlParameter("@PostedDataId", SqlDbType.VarChar)).Value = PostedDataId
        cmdUpdateMatched.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdateMatched.CommandType = CommandType.StoredProcedure
        cmdUpdateMatched.CommandTimeout = 0

        Dim da As New SqlClient.SqlDataAdapter(cmdUpdateMatched)
        dsPostHeaderData = New DataSet
        da.Fill(dsPostHeaderData)

        Return dsPostHeaderData



    End Function
    Public Sub UpdatePostedDataMatandUnmat(ByVal sqlcon As SqlClient.SqlConnection, ByVal PostedDataID As Integer, ByVal RecordStatus As Integer, _
                                           ByVal CurrentClient As Integer)

        Dim cmdUpdate As New SqlClient.SqlCommand("Process.usp_Updated_Posted_Data_Unmatched", sqlcon)
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Posted_DataID", SqlDbType.BigInt)).Value = PostedDataID
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Record_Status", SqlDbType.Int)).Value = RecordStatus
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdate.CommandTimeout = 0
        cmdUpdate.ExecuteNonQuery()

    End Sub
    Public Function OtherVariance(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsOther As DataSet

        Dim cmdOther As New SqlClient.SqlCommand("Process.usp_Bank_Unmatched_Other", sqlcon)
        cmdOther.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdOther.CommandType = CommandType.StoredProcedure
        cmdOther.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdOther)
        dsOther = New DataSet
        da.Fill(dsOther)

        Return dsOther


    End Function
    Public Function LoadPostedDataSearch(ByVal sqlcon As SqlClient.SqlConnection, ByVal Number As String, ByVal BatchID As String, ByVal Invoice As String, ByVal Bank_Dept_Dt As String,
                                        ByVal Closing_Dt As String, ByVal Create_Dt As String, ByVal Designation As String, ByVal Type As String, ByVal Pay_Amt As String,
                                        ByVal Pay_Code As String, ByVal Posted_Dt As String, ByVal Check As String, ByVal RecordStatus As Integer,
                                        ByVal strOriginalType As String, ByVal strOriginalDeptDate As String, ByVal CurrentClient As Integer) As DataSet

        Dim dsOther As DataSet

        Dim cmdPdSearch As New SqlClient.SqlCommand("Process.usp_Posted_DataSearch", sqlcon)
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Number", SqlDbType.VarChar)).Value = Number
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@BatchID", SqlDbType.VarChar)).Value = BatchID
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Invoice", SqlDbType.VarChar)).Value = Invoice
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Bank_Dept_Dt", SqlDbType.VarChar)).Value = Bank_Dept_Dt
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Closing_Dt", SqlDbType.VarChar)).Value = Closing_Dt
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Create_Dt", SqlDbType.VarChar)).Value = Create_Dt
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Pay_Amt", SqlDbType.VarChar)).Value = Pay_Amt
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Pay_Code", SqlDbType.VarChar)).Value = Pay_Code
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Posted_Dt", SqlDbType.VarChar)).Value = Posted_Dt
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Check", SqlDbType.VarChar)).Value = Check
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@RecordStatus", SqlDbType.VarChar)).Value = RecordStatus
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Bank_Dept_Dt_Original", SqlDbType.VarChar)).Value = strOriginalDeptDate
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@Type_Original", SqlDbType.VarChar)).Value = strOriginalType
        cmdPdSearch.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdPdSearch.CommandType = CommandType.StoredProcedure
        cmdPdSearch.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdPdSearch)
        dsOther = New DataSet
        da.Fill(dsOther)

        Return dsOther


    End Function
    Public Function LoadBankDataHolding(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer) As DataSet
        Dim dsOther As DataSet

        Dim cmdOther As New SqlClient.SqlCommand("Process.usp_Bank_Data_HoldingGet", sqlcon)
        cmdOther.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdOther.CommandType = CommandType.StoredProcedure
        cmdOther.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdOther)
        dsOther = New DataSet
        da.Fill(dsOther)

        Return dsOther


    End Function
    Public Sub UpdateBankDataRecord(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDataId As Integer, ByVal Check_Number As String, ByVal CurrentClient As Integer)

        Dim cmdUpdate As New SqlClient.SqlCommand("Process.usp_Bank_DataUpdate", sqlcon)
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@BankDataID", SqlDbType.BigInt)).Value = BankDataId
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check_Number
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdate.CommandTimeout = 0
        cmdUpdate.ExecuteNonQuery()

    End Sub
    Public Sub RefreshAllRecords(ByVal sqlcon As SqlClient.SqlConnection, ByVal CurrentClient As Integer)
        Dim dsrefPostedData As DataSet
        Try
            Dim cmdRefPostedData As New SqlClient.SqlCommand("Process.usp_Refresh_Posted_Data", sqlcon)
            cmdRefPostedData.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
            cmdRefPostedData.CommandType = CommandType.StoredProcedure
            cmdRefPostedData.CommandTimeout = 0
            Dim da As New SqlClient.SqlDataAdapter(cmdRefPostedData)
            dsrefPostedData = New DataSet
            da.Fill(dsrefPostedData)
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub


    Public Function GetPayersXMapToReplace(ByVal sqlcon As SqlClient.SqlConnection, ByVal payerID As String, ByVal CurrentClient As Integer) As DataSet
        Dim dsXmap As DataSet

        Dim cmdXmap As New SqlClient.SqlCommand("Process.usp_PayersXMapToReplace", sqlcon)
        cmdXmap.Parameters.Add(New SqlClient.SqlParameter("@PayerID", SqlDbType.Int)).Value = CInt(payerID)
        cmdXmap.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdXmap.CommandType = CommandType.StoredProcedure
        cmdXmap.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdXmap)
        dsXmap = New DataSet
        da.Fill(dsXmap)
        Return dsXmap

    End Function
    Public Function GetRecordCountWithPayer(ByVal sqlcon As SqlClient.SqlConnection, ByVal PayerID As String) As Integer
        Dim dsPostedCount As DataSet
        Dim dsBankCount As DataSet
        Dim postedCount As Integer = 0
        Dim bankCount As Integer = 0

        Dim cmdPostedCount As New SqlClient.SqlCommand("Process.usp_GetPostedCountWithPayer", sqlcon)
        cmdPostedCount.Parameters.Add(New SqlClient.SqlParameter("@Payer_ID", SqlDbType.Int)).Value = CInt(PayerID)
        cmdPostedCount.CommandType = CommandType.StoredProcedure
        cmdPostedCount.CommandTimeout = 0
        Dim dap As New SqlClient.SqlDataAdapter(cmdPostedCount)
        dsPostedCount = New DataSet
        dap.Fill(dsPostedCount)

        Dim cmdBankCount As New SqlClient.SqlCommand("Process.usp_GetBankCountWithPayer", sqlcon)
        cmdBankCount.Parameters.Add(New SqlClient.SqlParameter("@Payer_ID", SqlDbType.Int)).Value = CInt(PayerID)
        cmdBankCount.CommandType = CommandType.StoredProcedure
        cmdBankCount.CommandTimeout = 0
        Dim dab As New SqlClient.SqlDataAdapter(cmdBankCount)
        dsBankCount = New DataSet
        dab.Fill(dsBankCount)

        postedCount = CInt(dsPostedCount.Tables(0).Rows(0)("Count").ToString)
        bankCount = CInt(dsBankCount.Tables(0).Rows(0)("Count").ToString)

        Return postedCount + bankCount
    End Function

    Public Sub ReplaceAndDeletePayer(ByVal sqlcon As SqlClient.SqlConnection, ByVal NewPayerID As Integer, ByVal OldPayerID As Integer)
        Dim cmdReplaceDeletePayer As New SqlClient.SqlCommand("Process.usp_Payers_UpdatewithNewPayer", sqlcon)
        cmdReplaceDeletePayer.Parameters.Add(New SqlClient.SqlParameter("@OldPayer", SqlDbType.Int)).Value = OldPayerID
        cmdReplaceDeletePayer.Parameters.Add(New SqlClient.SqlParameter("@NewPayer", SqlDbType.Int)).Value = NewPayerID
        cmdReplaceDeletePayer.CommandType = CommandType.StoredProcedure
        cmdReplaceDeletePayer.CommandTimeout = 0
        cmdReplaceDeletePayer.ExecuteNonQuery()

    End Sub
    Public Function LoadBankDataHolding_BDD(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDataID As Integer, ByVal CurrentClient As Integer) As DataSet
        Dim dsBankData As DataSet

        Dim cmdBankLoad As New SqlClient.SqlCommand("[Process].[usp_Bank_Data_HoldingGet_BDD]", sqlcon)
        cmdBankLoad.Parameters.Add(New SqlClient.SqlParameter("@BankDataID", SqlDbType.BigInt)).Value = BankDataID
        cmdBankLoad.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdBankLoad.CommandType = CommandType.StoredProcedure
        cmdBankLoad.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdBankLoad)
        dsBankData = New DataSet
        da.Fill(dsBankData)

        Return dsBankData

    End Function
    Public Function LoadBankDataOther(ByVal sqlcon As SqlClient.SqlConnection, ByVal Operation As String) As DataSet
        Dim dsBankPostData As DataSet

        Dim cmdBankPostLoad As New SqlClient.SqlCommand("Process.usp_Bank_Post_DataOther", sqlcon)
        cmdBankPostLoad.Parameters.Add(New SqlClient.SqlParameter("@OperationFlag", SqlDbType.VarChar)).Value = Operation
        cmdBankPostLoad.CommandType = CommandType.StoredProcedure
        cmdBankPostLoad.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdBankPostLoad)
        dsBankPostData = New DataSet
        da.Fill(dsBankPostData)

        Return dsBankPostData

    End Function
    Public Function LoadPostedDataOther(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDeptDt As Date, ByVal Check_Number As String, ByVal IsMatched As Integer) As DataSet
        Dim dsPostData As DataSet

        Dim cmdPostLoad As New SqlClient.SqlCommand("Process.usp_Posted_DataGet", sqlcon)
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@BankDeptDate", SqlDbType.Date)).Value = BankDeptDt
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check_Number
        cmdPostLoad.Parameters.Add(New SqlClient.SqlParameter("@IsMatched ", SqlDbType.TinyInt)).Value = IsMatched
        cmdPostLoad.CommandType = CommandType.StoredProcedure
        cmdPostLoad.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdPostLoad)
        dsPostData = New DataSet
        da.Fill(dsPostData)

        Return dsPostData

    End Function
    Public Function LoadBankDataMatched(ByVal sqlcon As SqlClient.SqlConnection, ByVal BankDeptDt As String, ByVal Check_Number As String,
                                                    ByVal Amount As String, ByVal Type As String, ByVal CurrentClient As Integer) As DataSet
        Dim dsBDMatched As DataSet

        Dim cmdBDMatched As New SqlClient.SqlCommand("Process.usp_Load_Bank_Data_Matched", sqlcon)
        cmdBDMatched.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdBDMatched.Parameters.Add(New SqlClient.SqlParameter("@DepositDate", SqlDbType.VarChar)).Value = BankDeptDt
        cmdBDMatched.Parameters.Add(New SqlClient.SqlParameter("@CheckNumber", SqlDbType.VarChar)).Value = Check_Number
        cmdBDMatched.Parameters.Add(New SqlClient.SqlParameter("@Amount", SqlDbType.VarChar)).Value = Amount
        cmdBDMatched.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient     'Newly added Current Client.
        cmdBDMatched.CommandType = CommandType.StoredProcedure
        cmdBDMatched.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdBDMatched)
        dsBDMatched = New DataSet
        da.Fill(dsBDMatched)

        Return dsBDMatched

    End Function
    Public Sub UpdateRecordInformations(ByVal sqlcon As SqlClient.SqlConnection, ByVal strUnMatchedNumber As String, _
                                       ByVal strCheck As String, ByVal strDepositeDate As String, ByVal strType As String, ByVal strUser As String, _
                                       ByVal CurrentClient As Integer)
        Dim cmdUpdate As New SqlClient.SqlCommand("Process.usp_Update_PostedRecords", sqlcon)
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@strUnMatchedNumber", SqlDbType.VarChar)).Value = strUnMatchedNumber
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@strCheck", SqlDbType.VarChar)).Value = strCheck
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@strDepositeDate", SqlDbType.VarChar)).Value = strDepositeDate
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@strType", SqlDbType.VarChar)).Value = strType
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@strUpdateUser", SqlDbType.VarChar)).Value = strUser
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdate.CommandTimeout = 0
        cmdUpdate.ExecuteNonQuery()

    End Sub
    Public Function GetAllPayersByClient(ByVal sqlcon As SqlClient.SqlConnection, ByVal CurrentClient As Integer) As DataSet
        Dim dsPayers As DataSet

        Dim cmdPayers As New SqlClient.SqlCommand("Process.usp_PayersGetAllByClient", sqlcon)
        cmdPayers.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdPayers.CommandType = CommandType.StoredProcedure
        cmdPayers.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdPayers)
        dsPayers = New DataSet
        da.Fill(dsPayers)

        Return dsPayers

    End Function
    Public Function GetRepositoryClient(ByVal sqlcon As SqlClient.SqlConnection, ByVal intMasterClientNo As Integer) As DataSet
        Dim dsRepositoryClientID As DataSet

        Dim cmdRepositoryClient As New SqlClient.SqlCommand("Process.usp_Bank_GetRepositoryClientID", sqlcon)
        cmdRepositoryClient.Parameters.Add(New SqlClient.SqlParameter("@MasterClientNO", SqlDbType.Int)).Value = intMasterClientNo
        cmdRepositoryClient.CommandType = CommandType.StoredProcedure
        cmdRepositoryClient.CommandTimeout = 0
        Dim da As New SqlClient.SqlDataAdapter(cmdRepositoryClient)
        dsRepositoryClientID = New DataSet
        da.Fill(dsRepositoryClientID)
        Return dsRepositoryClientID
    End Function
    Public Function GetUserAccessByRoleID(ByVal sqlcon As SqlClient.SqlConnection, ByVal CurrentUserRoleID As Integer, ByVal CurrentUser As String) As Boolean
        Dim blnaccees As Boolean = False
        Dim dsCheckUserAccess As DataSet
        Dim cmdUserRoleID As New SqlClient.SqlCommand("Process.usp_Security_UserAccessGet", sqlcon)
        cmdUserRoleID.Parameters.Add(New SqlClient.SqlParameter("@CurrentUser_RoleID", SqlDbType.BigInt)).Value = CurrentUserRoleID
        cmdUserRoleID.Parameters.Add(New SqlClient.SqlParameter("@Windows_Authentication", SqlDbType.VarChar)).Value = CurrentUser
        cmdUserRoleID.CommandType = CommandType.StoredProcedure
        cmdUserRoleID.CommandTimeout = 0
        Dim daCheckUserAccess As New SqlClient.SqlDataAdapter(cmdUserRoleID)
        dsCheckUserAccess = New DataSet
        daCheckUserAccess.Fill(dsCheckUserAccess)
        If CInt(dsCheckUserAccess.Tables(0).Rows(0)("RoleID Access").ToString) = 1 Then
            blnaccees = True
        End If
        Return blnaccees
    End Function
    Public Sub RefreshDailyPostedData(ByVal sqlcon As SqlClient.SqlConnection, ByVal strClient As String)
        Dim cmdUpdate As New SqlClient.SqlCommand("Process.usp_Daily_Refresh_Posted_Data", sqlcon)
        cmdUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.VarChar)).Value = strClient
        cmdUpdate.CommandTimeout = 0
        cmdUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdate.ExecuteNonQuery()
    End Sub
    Public Sub RefreshDailyEPRAData(ByVal sqlcon As SqlClient.SqlConnection, ByVal strClient As String)
        Dim cmdUpdateEpra As New SqlClient.SqlCommand("Process.usp_Daily_Refresh_EPRA_Details", sqlcon)
        cmdUpdateEpra.CommandTimeout = 0
        cmdUpdateEpra.CommandType = CommandType.StoredProcedure
        cmdUpdateEpra.ExecuteNonQuery()
    End Sub
    Public Function LoadDataManagement(ByVal sqlcon As SqlClient.SqlConnection, ByVal clientID As Integer, ByVal DepositeDate As String, ByVal CheckNumber As String _
                                       , ByVal DepositeAmount As String, ByVal Variance As String, ByVal BAICode As String, ByVal QueueType As String) As DataSet
        Dim dsDataMgmt As DataSet

        Dim cmdDataMgmt As New SqlClient.SqlCommand("Process.usp_Load_DataManagement", sqlcon)
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = clientID
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@Deposit_Date", SqlDbType.VarChar)).Value = DepositeDate
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = CheckNumber
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@Deposit_Amount", SqlDbType.VarChar)).Value = DepositeAmount
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@Variance", SqlDbType.VarChar)).Value = Variance
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@BAI_Code", SqlDbType.VarChar)).Value = BAICode
        cmdDataMgmt.Parameters.Add(New SqlClient.SqlParameter("@QueueType", SqlDbType.VarChar)).Value = QueueType
        cmdDataMgmt.CommandType = CommandType.StoredProcedure
        cmdDataMgmt.CommandTimeout = 0
        Dim daDataMgmt As New SqlClient.SqlDataAdapter(cmdDataMgmt)
        dsDataMgmt = New DataSet
        daDataMgmt.Fill(dsDataMgmt)

        Return dsDataMgmt

    End Function
    Public Sub DeleteBankData(ByVal sqlcon As SqlClient.SqlConnection, ByVal Bank_DataID As String, ByVal clientID As Integer, ByVal QueueType As String, ByVal DeletedUser As String)
        Dim cmdDelete As New SqlClient.SqlCommand("Process.usp_Delete_BankData", sqlcon)
        cmdDelete.Parameters.Add(New SqlClient.SqlParameter("@Bank_DataID", SqlDbType.VarChar)).Value = Bank_DataID
        cmdDelete.Parameters.Add(New SqlClient.SqlParameter("@Client_Number", SqlDbType.Int)).Value = clientID
        cmdDelete.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = QueueType
        cmdDelete.Parameters.Add(New SqlClient.SqlParameter("@DeletedUser", SqlDbType.VarChar)).Value = DeletedUser
        cmdDelete.CommandType = CommandType.StoredProcedure
        cmdDelete.CommandTimeout = 0
        cmdDelete.ExecuteNonQuery()
    End Sub
#Region "Posted_Data_Correction"
    Public Function GetPostedDataCorrectionRecords(ByVal sqlcon As SqlClient.SqlConnection, ByVal ClientID As Integer, ByVal Type As String, ByVal BatchID As String, ByVal Invoice As String, _
                                                   ByVal Bank_Dept_Dt As String, ByVal Closing_Dt As String, ByVal Create_Dt As String, ByVal Designation As String, ByVal SearchType As String, _
                                                   ByVal Pay_Amt As String, ByVal Pay_Code As String, ByVal Posted_Dt As String, ByVal Check As String, ByVal Comment As String)
        Dim dsPostDataCorrectionRecords As DataSet
        Dim cmdPostDataCorrectionRecords As New SqlClient.SqlCommand("Process.usp_Get_CorrectionPostedData", sqlcon)
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Client_Number", SqlDbType.Int)).Value = ClientID
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@BatchID", SqlDbType.VarChar)).Value = BatchID
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Invoice", SqlDbType.VarChar)).Value = Invoice
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Bank_Dept_Dt", SqlDbType.VarChar)).Value = Bank_Dept_Dt
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Closing_Dt", SqlDbType.VarChar)).Value = Closing_Dt
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Create_Dt", SqlDbType.VarChar)).Value = Create_Dt
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@SearchType", SqlDbType.VarChar)).Value = SearchType
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Pay_Amt", SqlDbType.VarChar)).Value = Pay_Amt
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Pay_Code", SqlDbType.VarChar)).Value = Pay_Code
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Posted_Dt", SqlDbType.VarChar)).Value = Posted_Dt
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Check", SqlDbType.VarChar)).Value = Check
        cmdPostDataCorrectionRecords.Parameters.Add(New SqlClient.SqlParameter("@Comment", SqlDbType.VarChar)).Value = Comment
        cmdPostDataCorrectionRecords.CommandType = CommandType.StoredProcedure
        cmdPostDataCorrectionRecords.CommandTimeout = 0
        Dim daPostDataCorrectionRecords As New SqlClient.SqlDataAdapter(cmdPostDataCorrectionRecords)
        dsPostDataCorrectionRecords = New DataSet
        daPostDataCorrectionRecords.Fill(dsPostDataCorrectionRecords)

        Return dsPostDataCorrectionRecords
    End Function
    Public Sub UpdatePostDataCorrectionRecords(ByVal sqlcon As SqlClient.SqlConnection, ByVal Staging_DataID As Integer, ByVal Bankdeptdt As Date, _
                                       ByVal Designation As String, ByVal Type As String, ByVal Check As String, ByVal UpdateUser As String, _
                                       ByVal CurrentClient As Integer)
        Dim cmdPostedDataUpdate As New SqlClient.SqlCommand("Process.usp_Update_CorrectionPostedData", sqlcon)
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Staging_DataID", SqlDbType.BigInt)).Value = Staging_DataID
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Bank_Deposit_Date", SqlDbType.Date)).Value = Bankdeptdt
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Data_Type", SqlDbType.VarChar)).Value = Type
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Update_User", SqlDbType.VarChar)).Value = UpdateUser
        cmdPostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdPostedDataUpdate.CommandType = CommandType.StoredProcedure
        cmdPostedDataUpdate.CommandTimeout = 0
        cmdPostedDataUpdate.ExecuteNonQuery()

    End Sub
    Public Sub CorrectionPostedRecordsPush(ByVal sqlcon As SqlClient.SqlConnection, ByVal strStaging_DataID As String, ByVal User As String)
        Dim cmdPushPostedRecords As New SqlClient.SqlCommand("Process.usp_Push_CorrectionPostedData", sqlcon)
        cmdPushPostedRecords.Parameters.Add(New SqlClient.SqlParameter("@Staging_DataID", SqlDbType.VarChar)).Value = strStaging_DataID
        cmdPushPostedRecords.Parameters.Add(New SqlClient.SqlParameter("@User", SqlDbType.VarChar)).Value = User
        cmdPushPostedRecords.CommandType = CommandType.StoredProcedure
        cmdPushPostedRecords.CommandTimeout = 0
        cmdPushPostedRecords.ExecuteNonQuery()
    End Sub
    Public Sub DeleteCorrectionPostedRecords(ByVal sqlcon As SqlClient.SqlConnection, ByVal strPosted_DataID As String, ByVal User As String)
        Dim cmdDeletePostedRecords As New SqlClient.SqlCommand("Process.usp_Delete_CorrectioPostedData", sqlcon)
        cmdDeletePostedRecords.Parameters.Add(New SqlClient.SqlParameter("@Staging_DataID", SqlDbType.VarChar)).Value = strPosted_DataID
        cmdDeletePostedRecords.Parameters.Add(New SqlClient.SqlParameter("@User", SqlDbType.VarChar)).Value = User
        cmdDeletePostedRecords.CommandType = CommandType.StoredProcedure
        cmdDeletePostedRecords.CommandTimeout = 0
        cmdDeletePostedRecords.ExecuteNonQuery()
    End Sub
#End Region
#Region "Posted_Data_Deletion"
    Public Function GetUnmatched_PostedDataRecords_Deletion(ByVal sqlcon As SqlClient.SqlConnection, ByVal ClientID As Integer, ByVal Type As String, ByVal BatchID As String, ByVal Invoice As String, _
                                                  ByVal Bank_Dept_Dt As String, ByVal Closing_Dt As String, ByVal Create_Dt As String, ByVal Designation As String, ByVal SearchType As String, _
                                                  ByVal Pay_Amt As String, ByVal Pay_Code As String, ByVal Posted_Dt As String, ByVal Check As String)
        Dim dsUnmatchedPostDataRecords As DataSet

        Dim cmdUnmatchedPostDataRecords As New SqlClient.SqlCommand("Process.usp_Get_UnmatchedPostedData", sqlcon)
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Client_Number", SqlDbType.Int)).Value = ClientID
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = Type
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@BatchID", SqlDbType.VarChar)).Value = BatchID
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Invoice", SqlDbType.VarChar)).Value = Invoice
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Bank_Dept_Dt", SqlDbType.VarChar)).Value = Bank_Dept_Dt
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Closing_Dt", SqlDbType.VarChar)).Value = Closing_Dt
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Create_Dt", SqlDbType.VarChar)).Value = Create_Dt
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@SearchType", SqlDbType.VarChar)).Value = SearchType
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Pay_Amt", SqlDbType.VarChar)).Value = Pay_Amt
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Pay_Code", SqlDbType.VarChar)).Value = Pay_Code
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Posted_Dt", SqlDbType.VarChar)).Value = Posted_Dt
        cmdUnmatchedPostDataRecords.Parameters.Add(New SqlClient.SqlParameter("@Check", SqlDbType.VarChar)).Value = Check
        cmdUnmatchedPostDataRecords.CommandType = CommandType.StoredProcedure
        cmdUnmatchedPostDataRecords.CommandTimeout = 0
        Dim daUnmatchedPostDataRecords As New SqlClient.SqlDataAdapter(cmdUnmatchedPostDataRecords)
        dsUnmatchedPostDataRecords = New DataSet
        daUnmatchedPostDataRecords.Fill(dsUnmatchedPostDataRecords)

        Return dsUnmatchedPostDataRecords
    End Function
    Public Sub Update_UnmatchedPostedRecords(ByVal sqlcon As SqlClient.SqlConnection, ByVal Posted_DataID As Integer, ByVal Bankdeptdt As Date, _
                                       ByVal Designation As String, ByVal Type As String, ByVal Check As String, ByVal UpdateUser As String, _
                                       ByVal CurrentClient As Integer)
        Dim cmdUpdatePostedDataUpdate As New SqlClient.SqlCommand("Process.usp_Update_UnmatchedPostedData", sqlcon)
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Posted_DataID", SqlDbType.BigInt)).Value = Posted_DataID
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Bank_Deposit_Date", SqlDbType.Date)).Value = Bankdeptdt
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Designation", SqlDbType.VarChar)).Value = Designation
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Data_Type", SqlDbType.VarChar)).Value = Type
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Check_Number", SqlDbType.VarChar)).Value = Check
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@Update_User", SqlDbType.VarChar)).Value = UpdateUser
        cmdUpdatePostedDataUpdate.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int)).Value = CurrentClient
        cmdUpdatePostedDataUpdate.CommandType = CommandType.StoredProcedure
        cmdUpdatePostedDataUpdate.CommandTimeout = 0
        cmdUpdatePostedDataUpdate.ExecuteNonQuery()

    End Sub
    Public Sub DeleteUnmatchedPostedRecords(ByVal sqlcon As SqlClient.SqlConnection, ByVal strPosted_DataID As String, ByVal User As String)
        Dim cmdDeletePostedRecords As New SqlClient.SqlCommand("Process.usp_Delete_UnmatchedPostedData", sqlcon)
        cmdDeletePostedRecords.Parameters.Add(New SqlClient.SqlParameter("@Posted_DataID", SqlDbType.VarChar)).Value = strPosted_DataID
        cmdDeletePostedRecords.Parameters.Add(New SqlClient.SqlParameter("@User", SqlDbType.VarChar)).Value = User
        cmdDeletePostedRecords.CommandType = CommandType.StoredProcedure
        cmdDeletePostedRecords.CommandTimeout = 0
        cmdDeletePostedRecords.ExecuteNonQuery()
    End Sub

#End Region
#Region "Maintanance"
    Public Function Get_LoadProcessDetails(ByVal sqlcon As SqlClient.SqlConnection, ByVal dtCurr_Time As DateTime) As DataSet
        Dim dsMaintanance As DataSet

        Dim cmdMaintanance As New SqlClient.SqlCommand("Process.usp_Get_LoadDetails_Maintanance", sqlcon)
        cmdMaintanance.Parameters.Add(New SqlClient.SqlParameter("@Curr_Useraccess_Time", SqlDbType.DateTime)).Value = dtCurr_Time
        cmdMaintanance.CommandType = CommandType.StoredProcedure
        cmdMaintanance.CommandTimeout = 0
        Dim daMaintanance As New SqlClient.SqlDataAdapter(cmdMaintanance)
        dsMaintanance = New DataSet
        daMaintanance.Fill(dsMaintanance)
        Return dsMaintanance
    End Function
    Public Function Get_LoadStatus(ByVal sqlcon As SqlClient.SqlConnection, ByVal dtCurr_Time As DateTime) As DataSet
        Dim dsLoadStatus As DataSet

        Dim cmdLoadStatus As New SqlClient.SqlCommand("Process.usp_Get_LoadStatus", sqlcon)
        cmdLoadStatus.Parameters.Add(New SqlClient.SqlParameter("@Curr_Useraccess_Time", SqlDbType.DateTime)).Value = dtCurr_Time
        cmdLoadStatus.CommandType = CommandType.StoredProcedure
        cmdLoadStatus.CommandTimeout = 0
        Dim daLoadStatus As New SqlClient.SqlDataAdapter(cmdLoadStatus)
        dsLoadStatus = New DataSet
        daLoadStatus.Fill(dsLoadStatus)
        Return dsLoadStatus
    End Function
    Public Sub SetApplication_Connection()
        Dim SQLcon As SqlClient.SqlConnection
        Dim strEnvironment As String
        Dim strCon As String
        Try
            
            Dim env As New SSC.Tools.MachineEnvironment
            strEnvironment = env.ReturnEnvironment
            If strEnvironment = "TEST" Then
                strCon = SSC.ConnectionStrings.GetConnectionString("BANKRECONTEST", "SQL")
            Else
                strCon = SSC.ConnectionStrings.GetConnectionString("BANKRECONPROD", "SQL")
            End If

            ''strCon = "Data Source=TP-SQL\SSCSQL;Initial Catalog=BankReconciliation;Trusted_Connection=False;User ID=BankReconciliationUser;Password=c0n1F3r:B4nK:2015"

            HttpContext.Current.Session("strCon") = strCon

            'set up the SQL connection
            SQLcon = New SqlClient.SqlConnection(strCon)
            HttpContext.Current.Session("SQLcon") = SQLcon
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

#End Region
End Module
