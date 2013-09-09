Imports System.Data.OleDb
Imports System.Data

Module mPedidosEmCarteira

    Public Function RowAtraso(Tbl As DataTable, ArrayQtd() As Double, ArrayVal() As Double) As DataRow
        Dim Row As DataRow
        Try
            'LINHA DE ATRASO
            Row = Tbl.NewRow()
            Row.Item(0) = "Atr"
            For i = 1 To 14
                Row.Item(i) = ArrayQtd(i)
            Next
            Row.Item(15) = ArrayVal(15)

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function RowTotais(Tbl As DataTable, ArrayQtd() As Double, ArrayVal() As Double) As DataRow
        Dim Row As DataRow
        Try
            'TOTAIS
            Row = Tbl.NewRow()
            Row.Item(0) = "TOT"
            For i = 1 To 14
                Row.Item(i) = ArrayQtd(i)
            Next
            Row.Item(15) = ArrayVal(15)

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function RowCampacidade(Tbl As DataTable, ArrayCol() As String) As DataRow
        Dim Row As DataRow
        Try
            'CAPACIDADE DE PRODUÇÃO
            Row = Tbl.NewRow()
            Row.Item(0) = "CAP"
            For i As Integer = 1 To 13
                Row.Item(i) = CapcidadePrd(ArrayCol(i))
            Next
            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function RowProducaoMedia(Tbl As DataTable, ArrayCol() As String, DataInicial As Date, DataFinal As Date, NDias As Integer) As DataRow
        Dim Row As DataRow
        Dim Val() As Double
        Try
            'MEDIA DE PRODUÇÃO
            Row = Tbl.NewRow()
            Row.Item(0) = "MED"
            For i As Integer = 1 To 13
                Val = CalculaValorTotal(ArrayCol(i), DataInicial, DataFinal, TipoValor.Producao)
                Row.Item(i) = Val(0) / NDias
            Next
            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function RowDataFab(Tbl As DataTable) As DataRow
        Dim Row As DataRow
        Try
            'CAPACIDADE DE PRODUÇÃO
            Row = Tbl.NewRow()

            Row.Item(0) = "FAB"
            'Row.Item(1) = 0
            'Row.Item(2) = 0
            'Row.Item(3) = 0
            'Row.Item(4) = 0
            'Row.Item(5) = 0
            'Row.Item(6) = 0
            'Row.Item(7) = 0
            'Row.Item(8) = 0
            'Row.Item(9) = 0
            'Row.Item(10) = 0
            'Row.Item(11) = 0
            'Row.Item(12) = 0

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function RowDataCli(Tbl As DataTable) As DataRow
        Dim Row As DataRow
        Try
            'CAPACIDADE DE PRODUÇÃO
            Row = Tbl.NewRow()

            Row.Item(0) = "CLI"
            'Row.Item(1) = 0
            'Row.Item(2) = 0
            'Row.Item(3) = 0
            'Row.Item(4) = 0
            'Row.Item(5) = 0
            'Row.Item(6) = 0
            'Row.Item(7) = 0
            'Row.Item(8) = 0
            'Row.Item(9) = 0
            'Row.Item(10) = 0
            'Row.Item(11) = 0
            'Row.Item(12) = 0

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Enum TipoValor
        Producao = 0
        Encomendas = 1
        Faturacao = 2
    End Enum

    Public Enum TipoOutput
        Quantidade = 0
        Valores = 1
        Metros = 2
    End Enum

    Public Function RowValorResumo(Tbl As DataTable, ArrayCol() As String, Data As Date, Valor As TipoValor, _
                                   Optional MesCompleto As Boolean = False, Optional RowOutput As TipoOutput = TipoOutput.Quantidade) As DataRow
        Dim Row As DataRow
        Dim Val() As Double
        Dim TotQtd As Double
        Dim TotVal As Double
        Dim TotMet As Double
        Dim DataInicio As Date
        Dim DataFim As Date
        Try
            'LINHAS DE TOTAIS
            Row = Tbl.NewRow()
            Select Case Valor
                Case TipoValor.Encomendas
                    Row.Item(0) = "ENC"
                Case TipoValor.Faturacao
                    Row.Item(0) = "FAT"
                Case TipoValor.Producao
                    If MesCompleto Then
                        Select Case RowOutput
                            Case TipoOutput.Quantidade
                                Row.Item(0) = "P.U"
                            Case TipoOutput.Metros
                                Row.Item(0) = "P.M"
                            Case Else
                                Row.Item(0) = "..."
                        End Select
                    Else
                        Row.Item(0) = "PRD"
                    End If

                Case Else
                    Row.Item(0) = "..."
            End Select

            If MesCompleto Then
                DataInicio = FirstDayOfMonth(Data)
                DataFim = LastDayOfMonth(Data)
            Else
                DataInicio = Data
                DataFim = Data
            End If

            For i = 1 To 13

                Val = CalculaValorTotal(ArrayCol(i), DataInicio, DataFim, Valor)

                Select Case RowOutput
                    Case TipoOutput.Quantidade
                        Row.Item(i) = Val(0)
                    Case TipoOutput.Metros
                        Row.Item(i) = Val(2)
                    Case Else
                        Row.Item(i) = Val(1)
                End Select

                TotQtd += Val(0)
                TotVal += Val(1)
                TotMet += Val(2)

            Next

            Select Case RowOutput
                Case TipoOutput.Quantidade
                    Row.Item(14) = TotQtd
                Case TipoOutput.Metros
                    Row.Item(14) = TotMet
                Case Else
                    Row.Item(14) = TotQtd
            End Select

            Row.Item(15) = TotVal

            Return Row

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Get the first day of the month
    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Return New DateTime(sourceDate.Year, sourceDate.Month, 1)
    End Function

    'Get the last day of the month
    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function

    Public Function DataUtil(DataRef As Date, Ndias As Integer) As Date
        Try
            Dim data As Date = DataRef
            While Ndias <> 0
                data = data.AddDays(-1)
                If data.DayOfWeek <> DayOfWeek.Saturday And data.DayOfWeek <> DayOfWeek.Sunday Then
                    Ndias = Ndias - 1
                End If
            End While
            Return data
        Catch ex As Exception
            Return DataRef
        End Try
    End Function

    Private Function CapcidadePrd(TipoFamilia As String) As Double
        Dim sSql As String = ""
        Dim Valor As Double = 0
        Try
            sSql = "SELECT CDU_CapacidadeProd FROM TDU_TiposFamilias WHERE CDU_Codigo = '" & TipoFamilia & "'"

            Dim StrConect As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim Connection As New OleDbConnection(StrConect)
            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                Do While reader.Read
                    Valor = CDbl(reader.Item(0).ToString)
                Loop

            End Using

            Return Valor

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return 0
        End Try
    End Function

    Private Function CalculaValorTotal(TipoFamilia As String, DataIni As Date, DataFim As Date, TipoCalculo As TipoValor) As Object
        Dim sSql As String = ""
        Dim Data1 As String = DataIni.Year & "-" & DataIni.Month.ToString.PadLeft(2, "0") & "-" & DataIni.Day.ToString.PadLeft(2, "0") & " 00:00:00"
        Dim Data2 As String = DataFim.Year & "-" & DataFim.Month.ToString.PadLeft(2, "0") & "-" & DataFim.Day.ToString.PadLeft(2, "0") & " 23:59:59"
        Dim ValorArray(2) As Double
        Try
            Select Case TipoCalculo
                Case TipoValor.Faturacao
                    sSql = ""
                    sSql = sSql & "  SELECT ISNULL(SUM(LinhasDoc.Quantidade),0) as Qtd, ISNULL(SUM(LinhasDoc.PrecoLiquido),0) as Val, ISNULL(SUM(LinhasDoc.Quantidade * (ISNULL(Artigo.CDU_Medida,0)/100)),0) AS QTD_MT   "
                    sSql = sSql & " FROM CabecDoc"
                    sSql = sSql & "      INNER JOIN LinhasDoc ON LinhasDoc.IdCabecDoc = CabecDoc.ID"
                    sSql = sSql & "     LEFT JOIN LinhasDocStatus ON LinhasDocStatus.IdLinhasDoc = LinhasDoc.Id"
                    sSql = sSql & "    LEFT JOIN Artigo On Artigo.Artigo = LinhasDoc.Artigo"
                    sSql = sSql & "    LEFT JOIN SubFamilias On SubFamilias.Familia = Artigo.Familia AND SubFamilias.SubFamilia = Artigo.SubFamilia"
                    sSql = sSql & " WHERE CabecDoc.TipoDoc = 'FA'"
                    sSql = sSql & "     AND (SELECT Count(*) FROM TDU_TiposFamilias WHERE CDU_Codigo = SubFamilias.CDU_TipoFamilia) > 0"
                    sSql = sSql & "    AND SubFamilias.CDU_TipoFamilia = '" & TipoFamilia & "' AND (LinhasDoc.Data >= '" & Data1 & "' AND LinhasDoc.Data <= ' " & Data2 & "')"
                    sSql = sSql & " GROUP BY SubFamilias.CDU_TipoFamilia"
                Case TipoValor.Encomendas
                    sSql = ""
                    sSql = sSql & "  SELECT ISNULL(SUM(LinhasDoc.Quantidade),0) as Qtd, ISNULL(SUM(LinhasDoc.PrecoLiquido),0) as Val, ISNULL(SUM(LinhasDoc.Quantidade * (ISNULL(Artigo.CDU_Medida,0)/100)),0) AS QTD_MT  "
                    sSql = sSql & " FROM CabecDoc"
                    sSql = sSql & "      INNER JOIN LinhasDoc ON LinhasDoc.IdCabecDoc = CabecDoc.ID"
                    sSql = sSql & "     LEFT JOIN LinhasDocStatus ON LinhasDocStatus.IdLinhasDoc = LinhasDoc.Id"
                    sSql = sSql & "    LEFT JOIN Artigo On Artigo.Artigo = LinhasDoc.Artigo"
                    sSql = sSql & "    LEFT JOIN SubFamilias On SubFamilias.Familia = Artigo.Familia AND SubFamilias.SubFamilia = Artigo.SubFamilia"
                    sSql = sSql & " WHERE CabecDoc.TipoDoc = 'ECL'"
                    sSql = sSql & "     AND (SELECT Count(*) FROM TDU_TiposFamilias WHERE CDU_Codigo = SubFamilias.CDU_TipoFamilia) > 0"
                    sSql = sSql & "    AND SubFamilias.CDU_TipoFamilia = '" & TipoFamilia & "' AND (LinhasDoc.Data >= '" & Data1 & "' AND LinhasDoc.Data <= ' " & Data2 & "')"
                    sSql = sSql & " GROUP BY SubFamilias.CDU_TipoFamilia"
                Case TipoValor.Producao
                    sSql = ""
                    sSql = sSql & " SELECT ISNULL(SUM(LinhasSTK.Quantidade),0) AS QTD, SUM(ISNULL(LinhasDoc.PrecoLiquido / LinhasDoc.Quantidade,0) * LinhasSTK.Quantidade)AS VAL, ISNULL(SUM(LinhasSTK.Quantidade * (ISNULL(Artigo.CDU_Medida,0)/100)),0) AS QTD_MT "
                    sSql = sSql & " FROM LinhasSTK "
                    sSql = sSql & " LEFT JOIN Artigo On Artigo.Artigo = LinhasSTK.Artigo     "
                    sSql = sSql & " LEFT JOIN SubFamilias On SubFamilias.Familia = Artigo.Familia AND SubFamilias.SubFamilia = Artigo.SubFamilia "
                    sSql = sSql & " LEFT JOIN LinhasInternos ON LinhasInternos.Id = LinhasSTK.IdLinhaOrigemCopia AND LinhasSTK.ModuloOrigemCopia = 'N' "
                    sSql = sSql & " LEFT JOIN LinhasDoc ON LinhasDoc.id = LinhasInternos.IdLinhaOrigemCopia AND LinhasInternos.ModuloOrigemCopia = 'V' "

                    sSql = sSql & " WHERE LinhasSTK.TipoDoc = 'EOP' AND LinhasSTK.Modulo = 'S'"
                    sSql = sSql & "     AND (SELECT Count(*) FROM TDU_TiposFamilias WHERE CDU_Codigo = SubFamilias.CDU_TipoFamilia) > 0"
                    sSql = sSql & "    AND SubFamilias.CDU_TipoFamilia = '" & TipoFamilia & "' AND (LinhasSTK.Data >= '" & Data1 & "' AND LinhasSTK.Data <= ' " & Data2 & "')"
                    sSql = sSql & " GROUP BY SubFamilias.CDU_TipoFamilia"

                Case Else
                    sSql = ""
            End Select


            Dim StrConect As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim Connection As New OleDbConnection(StrConect)
            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                Do While reader.Read
                    ValorArray(0) = CDbl(reader.Item(0).ToString)
                    ValorArray(1) = CDbl(reader.Item(1).ToString)
                    ValorArray(2) = CDbl(reader.Item(2).ToString)
                Loop

            End Using

            Return ValorArray

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return 0
        End Try

    End Function

End Module
