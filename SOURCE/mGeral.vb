Imports System.Windows.Forms

Module mGeral
    Public MotorFA As Interop.ErpBS800.ErpBS = Nothing
    Public MotorKL As Interop.ErpBS800.ErpBS = Nothing
    Public MotorJU As Interop.ErpBS800.ErpBS = Nothing

    Public PlataformaFA As Interop.StdPlatBS800.StdPlatBS = Nothing
    Public PlataformaKL As Interop.StdPlatBS800.StdPlatBS = Nothing
    Public PlataformaJU As Interop.StdPlatBS800.StdPlatBS = Nothing

    Public ObjConfApl As Interop.StdPlatBS800.StdBSConfApl

    Public sUtilizador As String = ""
    Public sPassword As String = ""
    Public CodEmpresaFA As String = ""
    Public CodEmpresaKL As String = ""
    Public CodEmpresaJU As String = ""
    Public EmpresaGeral As String = ""

    Public ClienteGeral As String

    Public objTipo_SimplesOk As Object = Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk
    Public objTipo_SimNao As Object = Interop.StdPlatBS800.TipoMsg.PRI_SimNao
    Public objIcon_Critico As Object = Interop.StdPlatBS800.IconId.PRI_Critico
    Public objIcon_Questiona As Object = Interop.StdPlatBS800.IconId.PRI_Questiona
    Public objRslt_Nao As Object = Interop.StdPlatBS800.ResultMsg.PRI_Nao

    Public Structure Structure_TDU
        Public Caption As String
        Public Titulo As String

        Public SqlTabela As String
        Public SqlCampos() As String
        Public tblCampos() As String
        Public SqlCondicao As String
        Public SqlOrdenacao As String
        Public SqlDimensao As String

    End Structure

    Public Function DaUltimoNumeroArtigo() As Long
        Dim lista As Interop.StdBE800.StdBELista
        Dim UltimoNumero As Long = 0
        Try
            'NECESSÁRIO VERIFICAR QUAL O NUMERO DE CARATERES PARA O TAMANHO DO CODIGO.
            lista = New Interop.StdBE800.StdBELista
            lista = MotorFA.Consulta("SELECT MAX(CAST(Artigo.Artigo AS Bigint)) as ProximoNumero FROM Artigo WHERE ISNUMERIC(Artigo.Artigo) = 1 AND Artigo.TipoArtigo = '4' AND Artigo.Artigo <> '.'")
            If lista.NumLinhas > 0 Then
                If IsNumeric(lista.Valor(0).ToString) Then
                    UltimoNumero = CLng(lista.Valor(0).ToString) + 1
                Else
                    UltimoNumero = 0
                End If
            End If

            'confirmar que o proximo numero não existe
            Do While MotorFA.Comercial.Artigos.Existe(UltimoNumero)
                UltimoNumero += 1
            Loop

            Return UltimoNumero

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(objTipo_SimplesOk, "Erro ao ler o próximo numero de artigo.", objIcon_Critico, ex.Message)
            Return 0
        End Try
    End Function

    Public Sub LookF4(bt As DevExpress.XtraEditors.ButtonEdit)

        With bt.Properties.LookAndFeel
            .SkinName = "Office 2013"
            .Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
            .UseDefaultLookAndFeel = False
            .UseWindowsXPTheme = False
        End With

        With bt.Properties.Buttons(0)
            .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            .Image = My.Resources.F4
            .Shortcut = New DevExpress.Utils.KeyShortcut(Keys.F4)
        End With

        bt.MaximumSize = New System.Drawing.Size(bt.Size.Width, 20)

        'bt.Properties.Buttons(0).Shortcut = New DevExpress.Utils.KeyShortcut(Keys.Control Or Keys.F5)

    End Sub

End Module
