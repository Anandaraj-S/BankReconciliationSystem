<%@ Page Title="Administration" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" 
    CodeBehind="UserAdmin.aspx.vb" Inherits="Bank_Reconciliation.UserAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        .style1
        {
            width: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<asp:MultiView runat="server" ID="mvUserAdmin">
    <asp:View runat="server" ID="select">
        <center>
            <br />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                Text="Administration Page"></asp:Label>
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lnkUser" runat="server">User Administration</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkPayor" runat="server">Payer Table Management</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkPayerXmap" runat="server">Payer Crossmap Management</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkAppMgnt" runat="server">Application Management</asp:LinkButton>
            <br />
            <br />
               <asp:LinkButton ID="lnkDataMgnt" runat="server">Data Management</asp:LinkButton>
            <br />
            <br />
             <asp:LinkButton ID="LinkPostedDataDeletion" runat="server">Posted Data Deletion</asp:LinkButton>
           <br />
             <br />
              <asp:LinkButton ID="LinkPostedDataCorrection" runat="server">Posted Data Correction</asp:LinkButton>
             <br />
              <br />
              <asp:LinkButton ID="LinkPostedDataAddition" runat="server">Posted Data Addition</asp:LinkButton>
             <br />
            <br />
            <asp:LinkButton ID="LinkPostedTrial" runat="server">Posted Data Addition Trial</asp:LinkButton>
             <br />
            <br />
             <asp:LinkButton ID="PostedDataAdditionNew" runat="server">Posted Data Addition New</asp:LinkButton>
             <br />
            <br />

            <asp:LinkButton ID="lnkExit" runat="server">Back</asp:LinkButton>
        </center>
    </asp:View>
    <div class="container">
    <asp:View runat="server" ID="users">
        <div>
        <center>
            <asp:Label ID="Label10" runat="server" CssClass="viewHeading" 
                Text="User Administration"></asp:Label>
            <br />
            <div style ="height:20px;width:100%;top:0px; margin:0;padding:0">
            <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="Table1"
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:100%;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:44px;text-align:center"></td>
                    <td style="width:194px;text-align:center">Windows ID</td>
                    <td style="width:112px;text-align:center">First Name</td>
                    <td style="width:106px;text-align:center">Last Name</td>
                    <td style="width:10px;text-align:center">Security Role</td>
                    <td style="width:451px;text-align:center">Email Address</td>
                    <td style="width:40px;text-align:center">Created By</td>
                    <td style="width:148px;text-align:center">Created Date</td>
                    <td style="width:60px;text-align:center">Updated By</td>
                    <td style="width:148px;text-align:center">Updated Date</td>
                    <td style="width:94px;text-align:center"></td>
                </tr>
             </table>

            </div>
            <div runat="server" id="Div1" style="overflow: auto; height: 425px; width: 100%; top:0px; margin:0;padding:0">
            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" ShowHeader = "False" DataKeyNames="UserRowID">
                <Columns>
                    <asp:ButtonField CommandName="EditUser" Text="Edit" ItemStyle-Width="55px"></asp:ButtonField>
                    <asp:BoundField DataField="User_Windows_Authentication" HeaderText="Windows ID" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="User_First_Name" HeaderText="First Name" ItemStyle-Width="40px"/>
                    <asp:BoundField DataField="User_Last_Name" HeaderText="Last Name" ItemStyle-Width="40px"/>
                    <asp:BoundField DataField="Security_RoleID" HeaderText="Security Role" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" ItemStyle-Width="250px"/>
                    <asp:BoundField DataField="CreatedUser" HeaderText="Created By" ItemStyle-Width="40px" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-Width="148px"/>
                    <asp:BoundField DataField="LastUpdatedUser" HeaderText="Updated By" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="LastUpdatedDate" HeaderText="Updated Date" ItemStyle-Width="148px"/>
                    <asp:ButtonField CommandName="DeleteUser" Text="Delete User" ItemStyle-Width="74px"/>
                </Columns>
            </asp:GridView>
            </div>
            <br />
            <asp:LinkButton ID="lnkAddUser" runat="server">Add New User</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="lnkUserReturn" runat="server">Return</asp:LinkButton>
        </center>
        </div>
    </asp:View>
    </div>
    <asp:View runat="server" ID="EditUser">
    <div>
    <center>
        <asp:Label ID="lblUser" runat="server"
            Text="label" CssClass="viewHeading"></asp:Label>
        <br />
        <asp:TextBox ID="txtHiddenRowID" runat="server" Visible="False"></asp:TextBox>
        <br />
        <table>
            <tr>
            <td align="right">
            <asp:Label ID="Label2" runat="server" Text="Windows ID:  " Font-Bold="True" 
                    CssClass="fieldLabels"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtUserID" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right">
            <asp:Label ID="Label3" runat="server" Text="Security Level:  " Font-Bold="True" 
                    CssClass="fieldLabels"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlSecurity" runat="server">
                </asp:DropDownList>
            </td>
            </tr>

            <tr>
            <td align="right">
            <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="First:  " 
                    CssClass="fieldLabels"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtFirst" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right">
            <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Last:  " 
                    CssClass="fieldLabels"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtLast" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right">
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Email Address:  " 
                    CssClass="fieldLabels"></asp:Label>
            </td>
            <td align="left">
            <asp:TextBox ID="txtEmail" runat="server" Width="256px"></asp:TextBox>
            </td>
            </tr>
        </table>
        <asp:Label ID="lblDataValidation" runat="server" CssClass="failureNotification" 
            Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:LinkButton ID="lnkCancelUser" runat="server">Cancel</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkSaveUser" runat="server">Save</asp:LinkButton>
        <br />
        <br />
        <asp:Label ID="lblSecLevels" runat="server" CssClass="fieldLabels" 
            Text="Security Levels"></asp:Label>
        <br />
        <asp:GridView ID="grdRoles" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Security_RoleID" HeaderText="Role" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
            </Columns>
        </asp:GridView>
        <br />

     </center>

    </div>
    </asp:View>
    <asp:View runat="server" ID="Payer">
    <div>
    <center>
        <asp:Label ID="Label9" runat="server" CssClass="viewHeading" 
            Text="Payer Table Management"></asp:Label>
        <br />
        <table>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblPayerClient" runat="server" Text="Client"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPayerClient" runat="server" AutoPostBack="True" 
                        Width="238px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div style="height:20px;width:525px; position:relative; top:15px; margin:0;padding:0">
            <table Id="tblHeader" border="1" cellpadding="0" cellspacing="0" rules="all" style="font-family:Arial;font-size:10pt;font-weight:bold;width:525px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:55px;text-align:center">
                    </td>
                    <td style="width:150px;text-align:center">
                        Payer</td>
                    <td style="width:225px;text-align:center">
                        Description</td>
                    <td style="width:95px;text-align:center">
                    </td>
                </tr>
            </table>
        </div>
        <div runat="server" id="editfields" style="overflow: auto; height: 425px; width: 525px; position:relative; top:15px; left:0px; margin:0;padding:0">
        <asp:GridView ID="grdPayers" runat="server" ShowHeader = "False" DataKeyNames="Payer_ID"
            AutoGenerateColumns="False">
            <Columns>
                 <asp:ButtonField CommandName="EditPayer" Text="Edit" ItemStyle-Width="50px" >
                 <ItemStyle Width="50px" />
                 </asp:ButtonField>
                 <asp:BoundField DataField="Payer" HeaderText="Payer" ItemStyle-Width="75px"  >
                 <ItemStyle Width="150px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Description" HeaderText="Description" 
                     ItemStyle-Width="150px"  >
                 <ItemStyle Width="225px" />
                 </asp:BoundField>
                 <asp:ButtonField CommandName="DeletePayer" Text="Delete" 
                     ItemStyle-Width="75px"  >
                 <ItemStyle Width="75px" />
                 </asp:ButtonField>
             </Columns>
        </asp:GridView>
        </div>
        <br />
        <br />
        <asp:LinkButton ID="lnkAddPayer" runat="server">Add New Payer</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="lnkPayerReturn" runat="server">Return</asp:LinkButton>
    </center>
    </div>
    </asp:View>
    <asp:View runat="server" ID="AddPayer">
    <div>
    <center>
        <asp:Label ID="lblPayer" runat="server" Text="Label" CssClass="viewHeading"></asp:Label>
        <br />
        <asp:TextBox ID="txtHiddenKey" runat="server" Visible="False"></asp:TextBox>
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="Label5" runat="server" Text="Payer" CssClass="fieldLabels"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtPayer" runat="server" Height="22px" Width="267px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label12" runat="server" Text="Description" 
                        CssClass="fieldLabels"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtDescription" runat="server" Height="22px" Width="354px"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Label ID="lblPayerDataValidation" runat="server" 
            CssClass="failureNotification" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:LinkButton ID="lnkCancelPayer" runat="server">Cancel</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkSavePayer" runat="server">Save</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkUpdate" runat="server" Visible="False">Update </asp:LinkButton>
        &nbsp;
    </center>
    </div>
    </asp:View>
    <asp:View runat="server" ID="SelXmap">
    <div>
    <center>
    <asp:Label ID="Label14" runat="server" CssClass="viewHeading" 
            Text="Select Payer Crossmap Criteria"></asp:Label>
        <br />
        <br />
    <table>
    <tr>
    <td align="right"><asp:Label ID="Label11" runat="server"  Text="Client"></asp:Label></td>
    <td align="left"><asp:DropDownList ID="ddlClient" runat="server" Width="238px">
        </asp:DropDownList></td>
    </tr>
    <tr>
    <td align="right"><asp:Label ID="Label13" runat="server" Text="Type"></asp:Label></td>
    <td align="left"><asp:DropDownList
            ID="ddlType" runat="server" Width="125px">
            <asp:ListItem Selected="True"></asp:ListItem>
            <asp:ListItem>Bank</asp:ListItem>
            <asp:ListItem>Posted</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    </table>
        <asp:Label ID="lblCriteriaError" runat="server" CssClass="failureNotification" 
            Text="Label" Visible="False"></asp:Label>
        <br />
        &nbsp;<asp:LinkButton ID="lnkCancelCrossmap" runat="server">Cancel</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server">Continue</asp:LinkButton>
    </center>
    </div>
    </asp:View>
    <asp:View runat="server" ID="Payerxmap">
    <div>
        <center>
        <asp:Label ID="Label4" runat="server" Text="Payer Crossmap Management" 
                CssClass="viewHeading"></asp:Label>
        <br />
        <asp:Label ID="lblClient" runat="server" CssClass="fieldLabels" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblType" runat="server" CssClass="fieldLabels" Text="Label"></asp:Label>
        <div style ="height:20px;width:425px; position:relative; top:15px; margin:0;padding:0">
            <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="Table2"
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:425px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:50px;text-align:center"></td>
                    <td style="width:150px;text-align:center">Source Value</td>
                    <td style="width:150px;text-align:center">System Value</td>
                    <td style="width:90px;text-align:center"></td>
                </tr>
             </table>
        </div>
        <div runat="server" id="Div2" style="overflow: auto; height: 375px; width: 425px; position:relative; top:15px; left:0px; margin:0;padding:0">
            <asp:GridView ID="grdXmap" runat="server" ShowHeader = "False" DataKeyNames="Payer_CrossmapID,System_Value"
            AutoGenerateColumns="False">
            <Columns>
                 <asp:ButtonField CommandName="EditPayer" Text="Edit" ItemStyle-Width="50px" >
                 </asp:ButtonField>
                 <asp:BoundField DataField="Source_Value" HeaderText="Source Value" 
                     ItemStyle-Width="150px"  >
                 </asp:BoundField>
                 <asp:BoundField DataField="Payer" HeaderText="System Value" 
                     ItemStyle-Width="150px"  >
                 </asp:BoundField>
                 <asp:ButtonField CommandName="DeletePayer" Text="Delete" 
                     ItemStyle-Width="75px"  >
                 </asp:ButtonField>
             </Columns>
        </asp:GridView>
        </div>
            <br />
            <asp:LinkButton ID="lnkAddEntries" runat="server">Add New Entries</asp:LinkButton>
            <br />
            <br />
            <asp:LinkButton ID="LinkButton2" runat="server">Return</asp:LinkButton>
        </center>
    </div>
    </asp:View>
    <asp:View runat="server" ID="EditDeleteMapping">
    <div>
    <center>
        <asp:Label ID="lblEditDelMap" runat="server" Text="Label" CssClass="viewHeading"></asp:Label>
            <br />
        <asp:TextBox ID="txtHiddenXmapID" runat="server" Visible="False"></asp:TextBox>
            <table>
            <tr>
                <td align="right">
                    <asp:Label ID="Label15" runat="server" Text="Source Value:" CssClass="fieldLabels"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtSource" runat="server" Width="267px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label16" runat="server" Text="System Value" 
                        CssClass="fieldLabels"></asp:Label></td>
                <td align="left">
                    <asp:DropDownList ID="ddlSystem" runat="server" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblXmapVerification" runat="server" 
            CssClass="failureNotification" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:LinkButton ID="lnkCancelXmap" runat="server">Cancel</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkSaveXmap" runat="server">Save Mapping</asp:LinkButton>
    </center>
    </div>
    </asp:View>
    <asp:View runat="server" ID="AddXmap">
    <div>
    <center>
        <asp:Label ID="Label17" runat="server" CssClass="viewHeading" 
            Text="Add Crossmap Entries"></asp:Label>
        <br />
        <asp:Label ID="lblClientAdd" runat="server" CssClass="fieldLabels" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblClientTypeAdd" runat="server" CssClass="fieldLabels" 
            Text="Label"></asp:Label>
        <table>
        <tr>
        <td>
        <div>
            <asp:Label ID="Label18" runat="server" Text="Select Excel File to Import"></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload1"  runat="server" />
            <br />
            <asp:Label ID="lblFileType" runat="server" CssClass="failureNotification" 
                Text="Please select an Excel File" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="cmdImport" runat="server" Text="Import" />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
        </td>
        <td>
        <div style ="height:20px;width:530px; position:relative; top:15px; margin:0;padding:0">
            <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="Table3"
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:530px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:260px;text-align:center">Source Value</td>
                    <td style="width:260px;text-align:center">System Value</td>
                </tr>
             </table>
        </div>
        <div runat="server" id="div10" style="width: 800px">
        <div runat="server" id="Div3" style="overflow: auto; height: 375px; width: 530px; position:relative; top:15px; left:0px; margin:0;padding:0">
        <asp:GridView ID="grdAddXmap" runat="server" AutoGenerateColumns="False" 
                ShowHeader="False">
            <Columns>
                <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSourceAdd" runat="server" Width="250px"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="260px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="System Value" ItemStyle-Width="260px">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlSystemAdd" runat="server" Width="250px">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="260px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        </div>
        </td>
        </tr>
        </table>
        <br />
        <asp:Label ID="lblAddError" runat="server" CssClass="failureNotification" 
            Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:LinkButton ID="lnkCancelAdd" runat="server">Cancel</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkSaveAdd" runat="server">Save</asp:LinkButton>
    </center>
    </div>
    </asp:View>
    <asp:View runat="server" ID="addbulk">
        <center>
        <asp:Label ID="Label19" runat="server" CssClass="viewHeading" 
            Text="Add Crossmap Entries"></asp:Label>
        <br />
        <asp:Label ID="lblClientAddBulk" runat="server" CssClass="fieldLabels" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblClientTypeAddBulk" runat="server" CssClass="fieldLabels" 
            Text="Label"></asp:Label>

        <div style ="height:20px;width:530px; position:relative; top:15px; margin:0;padding:0">
            <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="Table4"
                     style="font-family:Arial;font-size:10pt;font-weight:bold;width:530px;color:black;
                     border-collapse:collapse;height:100%;">
                <tr style="font-weight:bold">
                    <td style="width:260px;text-align:center">Source Value</td>
                    <td style="width:260px;text-align:center">System Value</td>
                </tr>
             </table>
        </div>

        <div runat="server" id="Div4" style="overflow: auto; height: 375px; width: 530px; position:relative; top:15px; left:0px; margin:0;padding:0">
        <asp:GridView ID="grdAddBulk" runat="server" AutoGenerateColumns="False" DataKeyNames="Value"
                ShowHeader="False">
            <Columns>
                <asp:TemplateField HeaderText="Source Value" ItemStyle-Width="260px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSourceAdd" runat="server" Width="250px" 
                            Text='<%# Bind("Source") %>' ReadOnly="True"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="260px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="System Value" ItemStyle-Width="260px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSystemAdd" runat="server" Width="250px" 
                            Text='<%# Bind("System") %>' ReadOnly="True"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="260px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
            <br />
            <asp:Label ID="lblBulkError" runat="server" CssClass="failureNotification" 
                Text="Label" Visible="False"></asp:Label>
            <br />
            <asp:LinkButton ID="lnkCancelBulk" runat="server">Cancel</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkSaveBulk" runat="server">Save</asp:LinkButton>
        </center>
    </asp:View>
    <asp:View runat="server" ID="PayerRemap">
    <div>
    <center>
        <asp:Label ID="Label20" runat="server" Text="Re-map Payer" 
            CssClass="viewHeading"></asp:Label>
        <br />
        <asp:TextBox ID="txtRemapHiddenKey" runat="server" Visible="False"></asp:TextBox>
        <br />
        <asp:Label ID="lblRemapMessage" runat="server" CssClass="bold" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label21" runat="server" CssClass="bold" 
            Text="Please select a new Payer for these records."></asp:Label>
        <br />
        <asp:Label ID="Label22" runat="server" CssClass="bold" 
            Text="Before the Payer is deleted."></asp:Label>
        <br />
        <asp:DropDownList ID="ddlRemapPayers" runat="server" Width="267px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblXmap" runat="server" CssClass="bold" Text="Label"></asp:Label>
        <br />
        <asp:GridView ID="grdXmapToReplace" runat="server" 
            DataKeyNames="Payer_CrossmapID,Repository_clientID" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Client_Name" HeaderText="Client Name" />
                <asp:BoundField DataField="Type" HeaderText="Crossmap Type" />
                <asp:BoundField DataField="Source_Value" HeaderText="Source Value" />
                <asp:TemplateField HeaderText="System Value" ItemStyle-Width="260px">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlSystemAdd" runat="server" Width="250px">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="260px" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lblRemapFailure" runat="server" CssClass="failureNotification" 
            Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:LinkButton ID="lnkCancelRemap" runat="server">Cancel</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton3" runat="server">Re-map and Delete</asp:LinkButton>
    </center>
    </div>
    </asp:View>
</asp:MultiView>
</div>
</asp:Content>
