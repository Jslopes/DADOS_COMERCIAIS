﻿Module mPrint
    'Imprimir_OP Filial, "OP", Serie, NumDoc, Me.DocumentoVenda.PreVisualizar, ""
    'Imprimir_OP Filial, "OP", Serie, NumDoc, Me.DocumentoVenda.PreVisualizar, "FASOPLB1"



    Public Sub Imprimir_OP(Filial As String, TipoDoc As String, SerieDoc As String, NumDoc As Long, PreVisualizar As Boolean, _
                           Optional MapaLinhas As String = "", Optional CondicaoLinhas As String = "")
        'P - Imprime diretamente para a impressora
        'W - Pré Visualiza

        Dim PreViz As String
        Dim DocumentoLiqAGerar As String
        Dim NomeDoMapa As String
        Dim strFormula As String
        Dim strSelFormula As String
        Dim i As Integer
        Dim StrXplicado(6) As String
        Dim iVias As Integer
        Dim sMapa As String
        Dim Titulo As String

        '==================== Carregar Parametros ===================
        On Error GoTo ERRO
        '-----CARREGAR PARAMETROS
        If Motor.Comercial.TabInternos.Existe(TipoDoc) Then
            iVias = Motor.Comercial.Series.DaValorAtributo("N", TipoDoc, SerieDoc, "NumVias")
            sMapa = Motor.Comercial.Series.DaConfig("N", TipoDoc, SerieDoc)
        Else
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir mapa.", _
                                                            Interop.StdPlatBS800.IconId.PRI_Critico, "Tipo de Documento não existe." & vbCrLf & TipoDoc)
            GoTo FIM
        End If

        If PreVisualizar Then
            PreViz = "W"
        Else
            PreViz = "P"
        End If

        If Len(Trim(MapaLinhas)) > 0 Then sMapa = MapaLinhas

        If Len(Trim(sMapa)) > 0 Then
            NomeDoMapa = sMapa
        Else
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir a Ordem de Produção.", Interop.StdPlatBS800.IconId.PRI_Critico, "Mapa de O.Produção.")
            GoTo FIM
        End If

        StrXplicado(1) = Plataforma.Localizacao.DaResString("GCP", 6535)
        StrXplicado(2) = Plataforma.Localizacao.DaResString("GCP", 6536)
        StrXplicado(3) = Plataforma.Localizacao.DaResString("GCP", 6537)
        StrXplicado(4) = Plataforma.Localizacao.DaResString("GCP", 7270)
        StrXplicado(5) = Plataforma.Localizacao.DaResString("GCP", 6539)
        StrXplicado(6) = Plataforma.Localizacao.DaResString("GCP", 6540)
        '--^--

        '================= OUTROS ASSUNTOS - Exportar ===============
        'exportar a factura para pdf.
        'PlataformaPRIMAVERA.Mapas.Destino = edFicheiro
        'PlataformaPRIMAVERA.Mapas.SetFileProp efWord, "TESTE.pdf"
        '============================================================

        Plataforma.Mapas.Inicializar("GCP")
        Plataforma.Contexto.Erp.Inicializado = True

        strFormula = "NumberVar TipoDesc;NumberVar RegimeIva;NumberVar DecQde;NumberVar DecPrecUnit;StringVar MotivoIsencao; TipoDesc:=" & 1 & ";RegimeIva:=3;DecQde:=1;DecPrecUnit:=" & 2 & ";MotivoIsencao:=' ';"
        Plataforma.Mapas.AddFormula("InicializaParametros", strFormula)

        strFormula = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;"
        'strFormula = strFormula & "Nome:='" & Motor.Empresa.IDNome & "'"
        'strFormula = strFormula & ";Localidade:='" & Motor.Empresa.IDLocalidade & "'"
        'strFormula = strFormula & ";CodPostal:='" & Aplicacao.Empresa.IDLocalidadeCod & "'"
        'strFormula = strFormula & ";Telefone:='" & Aplicacao.Empresa.IDTelefone & "'"
        'strFormula = strFormula & ";Fax:='" & Aplicacao.Empresa.IDFax & "'"
        'strFormula = strFormula & ";Contribuinte:='" & Aplicacao.Empresa.IFNIF & "'"
        'strFormula = strFormula & ";CapitalSocial:='" & Aplicacao.Empresa.ICCapitalSocial & "'"
        'strFormula = strFormula & ";Conservatoria:='" & Aplicacao.Empresa.ICConservatoria & "'"
        'strFormula = strFormula & ";Matricula:='" & Aplicacao.Empresa.ICMatricula & "'"
        'strFormula = strFormula & ";MoedaCapitalSocial:='" & Aplicacao.Empresa.ICMoedaCapSocial & "'"
        'strFormula = strFormula & ";"


        strFormula = strFormula & "Nome:='" & Plataforma.Contexto.Empresa.IDNome & "'"
        strFormula = strFormula & ";Morada:='" & Plataforma.Contexto.Empresa.IDMorada & "'"
        strFormula = strFormula & ";Localidade:='" & Plataforma.Contexto.Empresa.IDLocalidade & "'"
        strFormula = strFormula & ";CodPostal:='" & Plataforma.Contexto.Empresa.IDCodPostal & "´'"
        strFormula = strFormula & ";Telefone:='" & Plataforma.Contexto.Empresa.IDTelefone & "'"
        strFormula = strFormula & ";Fax:='" & Plataforma.Contexto.Empresa.IDFax & "'"
        strFormula = strFormula & ";Contribuinte:='" & Plataforma.Contexto.Empresa.IFNIF & "'"
        strFormula = strFormula & ";CapitalSocial:='" & Plataforma.Contexto.Empresa.ICCapitalSocial & "'"
        strFormula = strFormula & ";Conservatoria:='" & Plataforma.Contexto.Empresa.ICConservatoria & "'"
        strFormula = strFormula & ";Matricula:='" & Plataforma.Contexto.Empresa.ICMatricula & "'"
        strFormula = strFormula & ";MoedaCapitalSocial:='" & Plataforma.Contexto.Empresa.ICMoedaCapSocial & "'"


        Plataforma.Mapas.AddFormula("DadosEmpresa", strFormula)

        strSelFormula = "{CabecInternos.Filial}='" & Filial & "' And {CabecInternos.Serie}='" & SerieDoc & "' And {CabecInternos.TipoDoc}='" & TipoDoc & "' and {CabecInternos.NumDoc}= " & NumDoc & ""
        If CondicaoLinhas.Length > 0 Then
            strSelFormula = strSelFormula & " And {LinhasInternos.NumLinha} IN [" & CondicaoLinhas & "]"
            Dim TotalLinhas As Integer = GetNumeroLinhas(TipoDoc, SerieDoc, NumDoc)
            Plataforma.Mapas.AddFormula("TotalLinhas", "NumberVar TotalLinhas; TotalLinhas:= " & TotalLinhas)
        End If

        Plataforma.Mapas.SelectionFormula = strSelFormula

        Titulo = Motor.Comercial.TabInternos.DaValorAtributo(TipoDoc, "Descricao") & " Nº " & Trim$(Str$(NumDoc))

        For i = 1 To iVias
            Plataforma.Mapas.AddFormula("NumVia", "'" & StrXplicado(i) & "'")
            Plataforma.Mapas.ImprimeListagem(NomeDoMapa, Titulo, PreViz, 1, "S", strSelFormula, , , False, , True)
        Next i

FIM:

        Exit Sub
ERRO:
        Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir mapa.", Interop.StdPlatBS800.IconId.PRI_Critico, Err.Number & vbCrLf & Err.Description & vbCrLf & vbCrLf & Err.Source, , True)
    End Sub


    Private Function GetNumeroLinhas(TipoDoc As String, Serie As String, Numdoc As Long) As Integer
        Try
            Dim sSql As String = ""
            sSql = sSql & "Select Count(LinhasDoc.id)"
            sSql = sSql & " FROM LinhasDoc "
            sSql = sSql & " INNER JOIN CabecDoc on CabecDoc.id = LinhasDoc.IdCabecDoc  "
            sSql = sSql & " WHERE CabecDoc.TipoDoc = 'ECL' AND CabecDoc.Serie = '" & Serie & "' "
            sSql = sSql & " AND CabecDoc.NumDoc = " & Numdoc & " AND (LinhasDoc.TipoLinha = 10 OR LinhasDoc.TipoLinha = 11)"
            Return Motor.Consulta(sSql).Valor(0)
        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir mapa.", Interop.StdPlatBS800.IconId.PRI_Critico, Err.Number & vbCrLf & Err.Description & vbCrLf & vbCrLf & Err.Source, , True)
            Return 0
        End Try
    End Function

    Public Sub Imprimir_ECL(Filial As String, TipoDoc As String, SerieDoc As String, NumDoc As Long, PreVisualizar As Boolean)
        'P - Imprime diretamente para a impressora
        'W - Pré Visualiza

        Dim PreViz As String
        Dim DocumentoLiqAGerar As String
        Dim NomeDoMapa As String
        Dim strFormula As String
        Dim strSelFormula As String
        Dim i As Integer
        Dim StrXplicado(6) As String
        Dim iVias As Integer
        Dim sMapa As String
        Dim Titulo As String

        '==================== Carregar Parametros ===================
        On Error GoTo ERRO
        '-----CARREGAR PARAMETROS
        If Motor.Comercial.TabVendas.Existe(TipoDoc) Then
            iVias = Motor.Comercial.Series.DaValorAtributo("V", TipoDoc, SerieDoc, "NumVias")
            sMapa = Motor.Comercial.Series.DaConfig("V", TipoDoc, SerieDoc)
        Else
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir mapa.", _
                                                            Interop.StdPlatBS800.IconId.PRI_Critico, "Tipo de Documento não existe." & vbCrLf & TipoDoc)
            GoTo FIM
        End If

        If PreVisualizar Then
            PreViz = "W"
        Else
            PreViz = "P"
        End If

        If Len(Trim(sMapa)) > 0 Then
            NomeDoMapa = sMapa
        Else
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir a Ordem de Produção.", Interop.StdPlatBS800.IconId.PRI_Critico, "Mapa de O.Produção.")
            GoTo FIM
        End If

        StrXplicado(1) = Plataforma.Localizacao.DaResString("GCP", 6535)
        StrXplicado(2) = Plataforma.Localizacao.DaResString("GCP", 6536)
        StrXplicado(3) = Plataforma.Localizacao.DaResString("GCP", 6537)
        StrXplicado(4) = Plataforma.Localizacao.DaResString("GCP", 7270)
        StrXplicado(5) = Plataforma.Localizacao.DaResString("GCP", 6539)
        StrXplicado(6) = Plataforma.Localizacao.DaResString("GCP", 6540)
        '--^--

        '================= OUTROS ASSUNTOS - Exportar ===============
        'exportar a factura para pdf.
        'PlataformaPRIMAVERA.Mapas.Destino = edFicheiro
        'PlataformaPRIMAVERA.Mapas.SetFileProp efWord, "TESTE.pdf"
        '============================================================

        Plataforma.Mapas.Inicializar("GCP")
        Plataforma.Contexto.Erp.Inicializado = True

        strFormula = "NumberVar TipoDesc;NumberVar RegimeIva;NumberVar DecQde;NumberVar DecPrecUnit;StringVar MotivoIsencao; TipoDesc:=" & 1 & ";RegimeIva:=3;DecQde:=1;DecPrecUnit:=" & 2 & ";MotivoIsencao:=' ';"
        Plataforma.Mapas.AddFormula("InicializaParametros", strFormula)

        strFormula = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;"
        'strFormula = strFormula & "Nome:='" & Motor.Empresa.IDNome & "'"
        'strFormula = strFormula & ";Localidade:='" & Motor.Empresa.IDLocalidade & "'"
        'strFormula = strFormula & ";CodPostal:='" & Aplicacao.Empresa.IDLocalidadeCod & "'"
        'strFormula = strFormula & ";Telefone:='" & Aplicacao.Empresa.IDTelefone & "'"
        'strFormula = strFormula & ";Fax:='" & Aplicacao.Empresa.IDFax & "'"
        'strFormula = strFormula & ";Contribuinte:='" & Aplicacao.Empresa.IFNIF & "'"
        'strFormula = strFormula & ";CapitalSocial:='" & Aplicacao.Empresa.ICCapitalSocial & "'"
        'strFormula = strFormula & ";Conservatoria:='" & Aplicacao.Empresa.ICConservatoria & "'"
        'strFormula = strFormula & ";Matricula:='" & Aplicacao.Empresa.ICMatricula & "'"
        'strFormula = strFormula & ";MoedaCapitalSocial:='" & Aplicacao.Empresa.ICMoedaCapSocial & "'"
        'strFormula = strFormula & ";"


        strFormula = strFormula & "Nome:='" & Plataforma.Contexto.Empresa.IDNome & "'"
        strFormula = strFormula & ";Morada:='" & Plataforma.Contexto.Empresa.IDMorada & "'"
        strFormula = strFormula & ";Localidade:='" & Plataforma.Contexto.Empresa.IDLocalidade & "'"
        strFormula = strFormula & ";CodPostal:='" & Plataforma.Contexto.Empresa.IDCodPostal & "´'"
        strFormula = strFormula & ";Telefone:='" & Plataforma.Contexto.Empresa.IDTelefone & "'"
        strFormula = strFormula & ";Fax:='" & Plataforma.Contexto.Empresa.IDFax & "'"
        strFormula = strFormula & ";Contribuinte:='" & Plataforma.Contexto.Empresa.IFNIF & "'"
        strFormula = strFormula & ";CapitalSocial:='" & Plataforma.Contexto.Empresa.ICCapitalSocial & "'"
        strFormula = strFormula & ";Conservatoria:='" & Plataforma.Contexto.Empresa.ICConservatoria & "'"
        strFormula = strFormula & ";Matricula:='" & Plataforma.Contexto.Empresa.ICMatricula & "'"
        strFormula = strFormula & ";MoedaCapitalSocial:='" & Plataforma.Contexto.Empresa.ICMoedaCapSocial & "'"


        Plataforma.Mapas.AddFormula("DadosEmpresa", strFormula)

        strSelFormula = "{CabecDoc.Filial}='" & Filial & "' And {CabecDoc.Serie}='" & SerieDoc & "' And {CabecDoc.TipoDoc}='" & TipoDoc & "' and {CabecDoc.NumDoc}= " & NumDoc & ""
        Plataforma.Mapas.SelectionFormula = strSelFormula

        Titulo = Motor.Comercial.TabInternos.DaValorAtributo(TipoDoc, "Descricao") & " Nº " & Trim$(Str$(NumDoc))

        For i = 1 To iVias
            Plataforma.Mapas.AddFormula("NumVia", "'" & StrXplicado(i) & "'")
            Plataforma.Mapas.ImprimeListagem(NomeDoMapa, Titulo, PreViz, 1, "S", strSelFormula, , , False, , True)
        Next i

        'alterar a flag docimp no cabecdocstatus
        ' Motor.Comercial.Vendas.ActualizaValorAtributo("000", TipoDoc, SerieDoc, NumDoc, "DocImp", 1)
        Dim s As String = ""
        s = ""
        s = s & "UPDATE CabecDocStatus SET CabecDocStatus.DocImp = 1"
        s = s & " FROM CabecDoc"
        s = s & " INNER JOIN CabecDocStatus on CabecDocStatus.IdCabecDoc = CabecDoc.Id"
        s = s & " WHERE CabecDoc.TipoDoc = '" & TipoDoc & "' AND CabecDoc.Serie = '" & SerieDoc & "' AND CabecDoc.NumDoc = " & NumDoc & ""

        Motor.DSO.BDAPL.Execute(s)

FIM:

        Exit Sub
ERRO:
        Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao imprimir mapa.", Interop.StdPlatBS800.IconId.PRI_Critico, Err.Number & vbCrLf & Err.Description & vbCrLf & vbCrLf & Err.Source, , True)
    End Sub

End Module
