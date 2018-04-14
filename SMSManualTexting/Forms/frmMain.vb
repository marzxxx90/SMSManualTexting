Public Class frmMain
    Const DBPATH As String = "\W3W1LH4CKU.FDB"

    Private Sub LoadPath()
        Dim readValue = My.Computer.Registry.GetValue(
    "HKEY_LOCAL_MACHINE\Software\cdt-S0ft\Pawnshop", "InstallPath", Nothing)

        Dim firebird As String = readValue & DBPATH
        database.dbName = firebird
        ' txtPath.Text = firebird
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvSegList.Items.Clear()
        lvSms.Items.Clear()

        LoadPath()
        LoadNumber()

    End Sub

    Private Sub LoadNumber(Optional ByVal str As String = "")
        Dim mysql As String
        Dim secured_str As String = str
        secured_str = DreadKnight(secured_str)
        Dim strWords As String() = secured_str.Split(New Char() {" "c})
        Dim name As String

        If str = "" Then
            mysql = "SELECT C.ID, C.FIRSTNAME || ' ' || C.LASTNAME  AS CLIENT, C.CONTACTNUM "
            mysql &= "FROM KYC_CUSTOMERS C ROWS 20"
        Else
            mysql = "SELECT C.ID, C.FIRSTNAME || ' ' || C.LASTNAME  AS CLIENT, C.CONTACTNUM "
            mysql &= "FROM KYC_CUSTOMERS C "
            mysql &= "WHERE UPPER(C.CONTACTNUM) LIKE UPPER('%" & str & "%') OR "

            For Each Name In strWords

                mysql &= vbCr & " UPPER(C.FIRSTNAME || ' ' || C.LASTNAME) LIKE UPPER('%" & name & "%') AND "
                If Name Is strWords.Last Then
                    mysql &= vbCr & " UPPER(C.LASTNAME || ' ' || C.FIRSTNAME) LIKE UPPER('%" & name & "%') "
                    Exit For
                End If
            Next
        End If
        Dim ds As DataSet = LoadSQL(mysql)

        lvSms.Items.Clear()
        For Each dr In ds.Tables(0).Rows
            Additems(dr)
        Next

    End Sub

    Private Sub Additems(ByVal dr As DataRow)
        With dr
            Dim lv As ListViewItem = lvSms.Items.Add(.Item("ID"))
            lv.SubItems.Add(.Item("CLIENT"))
            lv.SubItems.Add(.Item("CONTACTNUM").ToString)
        End With
       
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LoadPath()
        LoadNumber(txtSearch.Text)
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If isEnter(e) Then btnSearch.PerformClick()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        For Each lv As ListViewItem In lvSegList.Items
            lv.Checked = chkAll.Checked
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        database.dbName = "IS3NDY0U.GDB"
        If lvSegList.CheckedItems.Count < 1 Then Exit Sub
        Try

            For Each lv As ListViewItem In lvSegList.CheckedItems
                Dim mysql As String = "Select * From tblSMS Where ClientID = '" & lv.Text & "' And DocDate = '" & CurrentDate.ToString("MM/dd/yyyy") & "'"
                Dim ds As DataSet = LoadSQL(mysql, "tblSMS")
                Dim dsNewRow As DataRow
                dsNewRow = ds.Tables(0).NewRow
                If ds.Tables(0).Rows.Count = 0 Then
                    With dsNewRow
                        .Item("CLIENTID") = lv.Text
                        .Item("CNAME") = lv.SubItems(1).Text
                        .Item("CONTACTNUM") = lv.SubItems(2).Text
                        .Item("DOCDATE") = CurrentDate
                    End With
                    ds.Tables(0).Rows.Add(dsNewRow)
                    database.SaveEntry(ds)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

        MsgBox("Successfully Saved!", MsgBoxStyle.Information, "System Info")

        For Each itm As ListViewItem In lvSegList.SelectedItems
            lvSegList.Items.Remove(itm)
        Next
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        If lvSms.SelectedItems.Count = 0 Then Exit Sub

        For Each itm As ListViewItem In lvSms.SelectedItems
            If mod_system.validate_cp(itm.SubItems(2).Text).Contains("INV") Then
                MsgBox("Please Check The Number", MsgBoxStyle.Information, "Information") : Exit Sub
                'If Not MsgBox("Do you want to POST?", MsgBoxStyle.YesNo + MsgBoxStyle.Information + vbDefaultButton2, "POSTING...") = vbYes Then
                '    Exit Sub
                'End If
            End If
            For Each lv As ListViewItem In lvSegList.Items
                If itm.Text = lv.Text Then Exit Sub
            Next
            lvSegList.Items.Add(itm.Clone)
        Next
    End Sub

    Private Sub DailyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyToolStripMenuItem.Click
        frmSMS.FormType = frmSMS.ReportTypes.Daily
        frmSMS.Show()
    End Sub

    Private Sub MonthlyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonthlyToolStripMenuItem.Click
        frmSMS.FormType = frmSMS.ReportTypes.Monthly
        frmSMS.Show()
    End Sub

    Private Sub lvSegList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSegList.KeyDown
        If lvSegList.SelectedItems.Count = 0 Then Exit Sub

        If e.KeyCode = Keys.Delete Then
            For Each itm As ListViewItem In lvSegList.SelectedItems
                lvSegList.Items.Remove(itm)
            Next

        End If
    End Sub

    Private Sub lvSms_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lvSms.KeyPress
        If isEnter(e) Then btnMove.PerformClick()
    End Sub
End Class
