Public Class frmSMS

    Enum ReportTypes As Integer
        Daily = 0
        Monthly = 1

    End Enum
    Friend FormType As ReportTypes = ReportTypes.Daily

    Private Sub DailyReport()
        Dim mySql As String, dsName As String, rptPath As String
        dsName = "dsSMS"
        rptPath = "Reports\rpt_SMS.rdlc"
        mySql = "SELECT * FROM TBLSMS WHERE DOCDATE = '" & monCal.SelectionStart.ToShortDateString & "'"

        If DEV_MODE Then Console.WriteLine(mySql)
        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & monCal.SelectionStart.ToString("MMMM dd, yyyy"))

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub

    Private Sub MonthlyReport()
        Dim mySql As String, dsName As String, rptPath As String
        Dim st As Date = GetFirstDate(monCal.SelectionStart)
        Dim en As Date = GetLastDate(monCal.SelectionEnd)
        dsName = "dsSMS"
        rptPath = "Reports\rpt_SMS.rdlc"
        mySql = "SELECT * FROM TBLSMS WHERE DOCDATE BETWEEN '" & st.ToShortDateString & "' AND '" & en.ToShortDateString & "'"

        If DEV_MODE Then Console.WriteLine(mySql)
        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "FOR THE MONTH OF " + st.ToString("MMMM yyyy"))

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        database.dbName = "IS3NDY0U.GDB"
        Select Case FormType
            Case ReportTypes.Daily
                DailyReport()
            Case ReportTypes.Monthly
                MonthlyReport()
        End Select
    End Sub
End Class