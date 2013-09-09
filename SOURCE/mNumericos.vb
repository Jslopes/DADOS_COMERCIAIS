Imports System.Windows.Forms

Module mNumericos
    Public Function ValidarValor(ByVal ValorAscii As Integer, ByVal ValorTxt As String) As Integer

        ValorTxt = IIf(Not IsNumeric(ValorTxt), 0, ValorTxt)

        If ValorAscii < 48 Or ValorAscii > 57 Then

            If ValorAscii = 46 Then ValorAscii = 44

            Select Case ValorAscii

                Case 127, 8
                    ValidarValor = ValorAscii

                Case 44
                    If InStr(1, ValorTxt, ",") <> 0 Then
                        ValidarValor = 0
                    Else
                        ValidarValor = ValorAscii
                    End If
                Case Else
                    ValidarValor = 0
            End Select

        Else
            ValidarValor = ValorAscii
        End If

    End Function

    Public Sub ValidarTxtNum(ByVal txt As TextBox, ByVal ErrProvider As ErrorProvider, _
                             ByVal Menssagem As String, Optional ByVal CasasDecimais As Integer = 2)
        ErrProvider.Clear()
        Try
            If txt.Text = "" Then txt.Text = "0"
            txt.Text = FormatNumber(txt.Text, CasasDecimais)
        Catch ex As Exception
            ErrProvider.SetError(txt, Menssagem)
        End Try
    End Sub
    Public Function FormataNum(ByVal Valor As Double) As String
        Dim Decimais As Double = 0
        Try
            Decimais = MotorFA.Comercial.Params.CasasDecimaisQnt
            Return FormatNumber(Valor, Decimais)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "")
            Return FormatNumber(0, Decimais)
        End Try
    End Function
End Module
