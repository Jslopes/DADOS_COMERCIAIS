Imports System.Windows.Forms
Imports System.Text

Module mForms
    Public Declare Auto Function GetPrivateProfileString Lib "Kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Auto Function WritePrivateProfileString Lib "Kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    Public Sub ClearTextBox(ByVal root As Control)
        'For Each ctrl As Control In root.Controls
        '    ClearTextBox(ctrl)
        '    If TypeOf ctrl Is TextBox Then
        '        CType(ctrl, TextBox).Text = String.Empty
        '    ElseIf TypeOf ctrl Is DevExpress.XtraEditors.TextEdit Then
        '        CType(ctrl, DevExpress.XtraEditors.TextEdit).Text = String.Empty
        '    End If
        'Next ctrl
        For Each ctrl As Control In root.Controls
            ClearTextBox(ctrl)
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            End If
        Next ctrl
    End Sub

    Public Function GetIni(ByVal file_name As String, ByVal section_name As String, ByVal key_name As String, ByVal default_value As String) As String

        Const MAX_LENGTH As Integer = 500
        Dim string_builder As New StringBuilder(MAX_LENGTH)

        GetPrivateProfileString(section_name, key_name, default_value, string_builder, MAX_LENGTH, file_name)

        Return string_builder.ToString()

    End Function

    Public Sub SetIni(ByVal file_name As String, ByVal section_name As String, ByVal key_name As String, ByVal default_value As String)

        WritePrivateProfileString(section_name, key_name, default_value, file_name)

    End Sub

    Public Sub SaveMyForm(frm As Form, Optional ComSpliter As Boolean = False)
        Try
            If frm.WindowState = FormWindowState.Normal Then
                SetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Top", frm.Top)
                SetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Left", frm.Left)
                SetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Height", frm.Height)
                SetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Width", frm.Width)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try

    End Sub

    Public Sub LoadMyForm(frm As Form, Optional resize As Boolean = True)
        Try
            frm.Top = GetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Top", frm.Top)
            frm.Left = GetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Left", frm.Left)
            If resize Then
                frm.Height = GetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Height", frm.Height)
                frm.Width = GetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "Width", frm.Width)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try

    End Sub

    Public Sub LoadMySpliter(frm As Form, Spliter As SplitContainer)
        Try
            Spliter.SplitterDistance = GetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "SplitterDistance", Spliter.SplitterDistance)
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try

    End Sub

    Public Sub SaveMySpliter(frm As Form, Spliter As SplitContainer)
        Try
            SetIni(Application.StartupPath.ToString & "\MyForms.ini", frm.Text, "SplitterDistance", Spliter.SplitterDistance)
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try
    End Sub

    Public Sub SaveMyDgv(frm As Form, dgv As DataGridView, Optional sAux As String = "")
        Try
            For i As Integer = 0 To dgv.Columns.Count - 2 ' TODAS AS MINHAS DVG TEM A ULTIMA COLUNA EM BRANCO
                'GUARDAR A LARGURA DA COLUNA
                SetIni(Application.StartupPath.ToString & "\MyDgvProperties.ini", frm.Text & "_" & dgv.Name & "_" & sAux, "Column(" & i & ")", dgv.Columns(i).Width)
            Next i
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try
    End Sub

    Public Sub LoadMyDgv(frm As Form, dgv As DataGridView, Optional sAux As String = "")
        Try
            For i As Integer = 0 To dgv.Columns.Count - 2 ' TODAS AS MINHAS DVG TEM A ULTIMA COLUNA EM BRANCO
                dgv.Columns(i).Width = GetIni(Application.StartupPath.ToString & "\MyDgvProperties.ini", frm.Text & "_" & dgv.Name & "_" & sAux, "Column(" & i & ")", dgv.Columns(i).Width)
            Next i
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation)
        End Try
    End Sub

    Public Sub TratamentoDeErros()


    End Sub


End Module
