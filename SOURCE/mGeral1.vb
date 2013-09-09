Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports Interop.StdPlatBS800.TipoMsg
Imports Interop.StdPlatBS800.IconId
Imports Interop.StdPlatBS800.CRPESentidoOrdenacao
Module mGeral1
    'Public Motor As Object
    'Public ObjConfApl As Object
    'Public Plataforma As Object

    'Public Motor As Interop.ErpBS800.ErpBS
    'Public ObjConfApl As Interop.StdPlatBS800.StdBSConfApl
    'Public Plataforma As Interop.StdPlatBS800.StdPlatBS

    Public ConnStr As String

    Public Enum MsgAluno
        sim = 0
        SimParaTodos = 1
        Nao = 2
        NaoParaTodos = 3
    End Enum

    Public Enum StateProcessamento
        OK = 0
        Err = 1
        Null = 2
    End Enum

    Public Function ExisteDocSerie(TipoDoc As String, Serie As String, Aluno As String, ByRef Id As String) As Boolean
        Dim s As String
        Try
            s = "SELECT id From CabecInternos WHERE TipoDoc = '" & TipoDoc & "' AND Serie = '" & Serie & "' AND TipoEntidade = 'C' AND Entidade = '" & Aluno & "'"
            Dim Lista As Interop.StdBE800.StdBELista = MotorFA.Consulta(s)
            If Lista.NumLinhas = 1 Then
                Id = Lista.Valor(0)
                Return True
            Else
                If Lista.NumLinhas > 1 Then
                    MsgBox("Existe mais de 1 documento para este alunos na série [" & Serie & "].", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Documentos")
                End If
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

    Public Function FechaSerie(Serie As String) As Boolean
        Try
            'ANTES DE SAIR, VERIFICAR OS DOCUMENTOS DESTA SÉRIE JÁ ESTÃO TODOS FECHADOS
            Dim sSql As String = "SELECT COUNT(NumDoc) FROM CabecInternos WHERE TipoDoc='MVM' AND Serie = '" & Serie & "' AND ESTADO <> 'F'"
            Dim Lista As Interop.StdBE800.StdBELista = MotorFA.Consulta(sSql)
            If Lista.NumLinhas > 0 Then
                If Lista.Valor(0) = 0 Then
                    Dim objChave As New Interop.StdBE800.StdBECamposChave
                    objChave.AddCampoChave("CDU_SerieMes", Serie)
                    MotorFA.TabelasUtilizador.ActualizaValorAtributo("TDU_MesesAnoLetivo", objChave, "CDU_Activo", 0)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

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

        PlataformaFA.Mapas.Inicializar("GCP")

        If PreVisualizar Then
            Destino = "W" 'PréVisualizar
        Else
            Destino = "P" 'Impressão directa
        End If

        Titulo = "Mapa de romaneio"

        If (Len(strNomeReport) <> 0) Then
            Return PlataformaFA.Mapas.ImprimeListagem(strNomeReport, Titulo, Destino, , "S", SelFormula, soNenhuma, False, , , True, eCultura:=1) ', eCultura:=1  'eCultura:=  DaIdiomaMapa(strNomeReport))
        Else
            PlataformaFA.Dialogos.MostraMensagem(PRI_Detalhe, PlataformaFA.Localizacao.DaResString(cABRV_APL, 6531), PRI_Informativo, PlataformaFA.Localizacao.DaResString(cABRV_APL, 6532))
            Return 0
        End If

    End Function

End Module
