Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports Interop.StdPlatBS800.TipoMsg
Imports Interop.StdPlatBS800.IconId
Imports System.Data

Module mGeral
    Public Motor As Interop.ErpBS800.ErpBS = Nothing
    Public MotorKL As Interop.ErpBS800.ErpBS = Nothing
    Public MotorJU As Interop.ErpBS800.ErpBS = Nothing

    Public Plataforma As Interop.StdPlatBS800.StdPlatBS = Nothing
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

    Public Function ClienteExiste(Empresa As String, Cliente As String, StrConectFA As String, StrConectKL As String, StrConectJU As String) As Boolean
        Try
            Dim StrConect As String = ""
            Select Case Empresa
                Case "FASTIL"
                    StrConect = StrConectFA
                Case "KLICK"
                    StrConect = StrConectKL
                Case "JUALTEX"
                    StrConect = StrConectJU
            End Select

            Dim sSql As String = ""
            sSql = sSql & " SELECT COUNT(Clientes.cliente) AS Existe"
            sSql = sSql & " FROM Clientes"
            sSql = sSql & " WHERE Clientes.cliente = '" & Cliente & "'"

            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read

                    If CInt(reader.Item(0).ToString) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                    Exit Do
                Loop

                Return False

            End Using
        Catch ex As Exception
            MsgBox("Erro ao validar o cliente." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return ""
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

    Public Sub CarregaImpressoras(tsCbImpressoras As Object)
        ' variaveis(utilizadas)te
        Dim i, j As Integer
        Try
            'corre todas as impressoas instaladas adicionando-as ao combobox 
            With PrinterSettings.InstalledPrinters
                For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                    tsCbImpressoras.Items.Add(.Item(i))
                Next
            End With

            'seleciona o primeiro item
            tsCbImpressoras.SelectedIndex = (j)
        Catch ex As Exception
            'exibe mensagem de erro cajo aconteça ao inesperado
            MessageBox.Show("(Erro ao ler as impressoras.)" & vbCrLf & vbCrLf & ex.Message, "Impressoras", MessageBoxButtons.OK)
        Finally

        End Try
    End Sub

    Public Function ValidaImpressora(sImpressora As String) As String
        Dim i As Integer
        Dim bOK As Boolean = False

        Dim sImpTemp As String
        sImpTemp = ""
        Try
            'corre todas as impressoas instaladas
            With PrinterSettings.InstalledPrinters
                For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                    'Verifica se existe! e termina o ciclo
                    If .Item(i) = sImpressora Then
                        bOK = True
                        Exit For
                    End If
                Next
                If bOK = False Then
                    MsgBox("Impressora selecccionada não foi encontrada." & vbCrLf & vbCrLf & _
                           "Vai ser selecccionada a impressora predefinida do windows.", MsgBoxStyle.Exclamation, "")
                    sImpressora = DefaultPrinterName()
                End If

            End With

        Catch ex As Exception
            'exibe mensagem de erro cajo aconteça ao inesperado
            MessageBox.Show("(Erro ao validar a impressora.)" & vbCrLf & vbCrLf & ex.Message)
            MsgBox("Erro ao validar a impressora." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "")
        End Try
        Return sImpressora
    End Function

    Public Function DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings

        Try
            DefaultPrinterName = oPS.PrinterName
        Catch ex As System.Exception
            DefaultPrinterName = ""
        Finally
            oPS = Nothing
        End Try

    End Function

    Public Function ImprimeMapa(strNomeReport As String, PreVisualizar As Integer, SelFormula As String) As String
        Dim Titulo As String = ""
        Dim Destino As String = ""
        Dim cABRV_APL As String = "GCP"

        Plataforma.Mapas.Inicializar("GCP")

        If PreVisualizar Then
            Destino = "W" 'PréVisualizar
        Else
            Destino = "P" 'Impressão directa
        End If

        Titulo = "Nome do mapa"

        If (Len(strNomeReport) <> 0) Then
            Return Plataforma.Mapas.ImprimeListagem(strNomeReport, Titulo, Destino, , "S", SelFormula, Interop.StdPlatBS800.CRPESentidoOrdenacao.soNenhuma, False, , , True, eCultura:=1) ', eCultura:=1  'eCultura:=  DaIdiomaMapa(strNomeReport))
        Else
            Plataforma.Dialogos.MostraMensagem(PRI_Detalhe, Plataforma.Localizacao.DaResString(cABRV_APL, 6531), PRI_Informativo, Plataforma.Localizacao.DaResString(cABRV_APL, 6532))
            Return 0
        End If

    End Function

End Module
