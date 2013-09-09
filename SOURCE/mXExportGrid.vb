Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms

Module mXExportGrid
    Public Sub xTraGridExport(frm As Form, Index As Integer, GridView As DevExpress.XtraGrid.Views.Base.BaseView)
        If Index < 0 Then
            Return
        End If
        Dim fileName As String = ShowSaveFileDialog(exportData.GetValue(Index, 0).ToString(), exportData.GetValue(Index, 1).ToString())
        If fileName = String.Empty Then
            Return
        End If
        ExportToEx(fileName, exportData.GetValue(Index, 2).ToString(), GridView)
        OpenFile(frm, fileName)
    End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String) As String
        Dim dlg As New SaveFileDialog()
        Dim name As String = Application.ProductName
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

    Private exportData(,) As String = { _
    {"HTML Document", "HTML Documents|*.html", "htm"}, _
    {"Microsoft Excel 2007 Document", "Microsoft Excel|*.xlsx", "xlsx"}, _
    {"Microsoft Excel Document", "Microsoft Excel|*.xls", "xls"}, _
    {"RTF Document", "RTF Files|*.rtf", "rtf"}, _
    {"PDF Document", "PDF Files|*.pdf", "pdf"}, _
    {"MHT Document", "MHT Files|*.mht", "mht"}, _
    {"Text Document", "Text Files|*.txt", "txt"}}

    Private Sub ExportToEx(ByVal filename As String, ByVal ext As String, ByVal exportView As BaseView)
        Dim currentCursor As Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor
        If ext = "rtf" Then
            exportView.ExportToRtf(filename)
        End If
        If ext = "pdf" Then
            exportView.ExportToPdf(filename)
        End If
        If ext = "mht" Then
            exportView.ExportToMht(filename)
        End If
        If ext = "htm" Then
            exportView.ExportToHtml(filename)
        End If
        If ext = "txt" Then
            exportView.ExportToText(filename)
        End If
        If ext = "xls" Then
            exportView.ExportToXls(filename)
        End If
        If ext = "xlsx" Then
            exportView.ExportToXlsx(filename)
        End If
        Cursor.Current = currentCursor
    End Sub

    Private Sub OpenFile(ByVal frm As Form, ByVal fileName As String)
        If XtraMessageBox.Show("Deseja abrir este ficheiro?", "Exportar para...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim process As New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(frm, "Não foi possível encontrar no sistema uma aplicação adequada para abrir o ficheiro com os dados exportados.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        'progressBarControl1.Position = 0
    End Sub
End Module
