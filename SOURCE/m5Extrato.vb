Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Module m5Extrato
    Public Sub AtualizarExtrato(dsExtrato As DataSet, Cliente As String, _
                          gv51 As GridView, gv52 As GridView, _
                          DataInicial As String, DataFinal As String, StrConectFA As String, StrConectKL As String, StrConectJU As String)
        Try
            'Create a connection object. 
            'Dim StrConectFA As String = Plataforma.BaseDados.DaConnectionString(Plataforma.BaseDados.DaNomeBDdaEmpresa(Plataforma.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            'Dim StrConectKL As String = PlataformaKL.BaseDados.DaConnectionString(PlataformaKL.BaseDados.DaNomeBDdaEmpresa(PlataformaKL.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            'Dim StrConectJU As String = PlataformaJU.BaseDados.DaConnectionString(PlataformaJU.BaseDados.DaNomeBDdaEmpresa(PlataformaJU.Contexto.Empresa.CodEmp).ToString, "Default").ToString

            CarregaEmpresas(dsExtrato, StrConectFA.Replace("PRIFASTIL", "PRIEMPRE"))

            dsExtrato.Tables("Extrato").Clear()
            CarregaExtrato(dsExtrato, StrConectFA, Cliente, CStr("FASTIL"), DataInicial, DataFinal)
            CarregaExtrato(dsExtrato, StrConectKL, Cliente, CStr("KLICK"), DataInicial, DataFinal)
            CarregaExtrato(dsExtrato, StrConectJU, Cliente, CStr("JUALTEX"), DataInicial, DataFinal)

            'Configurar carateristicas das colunas
            Dim i As Integer = 0
            For i = 0 To gv51.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv51.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv51.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                gv51.Columns(i).OptionsColumn.AllowEdit = False
            Next

            For i = 0 To gv52.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv52.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv52.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center
                gv52.Columns(i).OptionsColumn.AllowEdit = False
                Select Case i
                    Case 5, 6, 7
                        gv52.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        gv52.Columns(i).DisplayFormat.FormatString = "N2"
                    Case 4
                        gv52.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        gv52.Columns(i).AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    Case Else

                End Select

            Next

            For i = 0 To gv51.RowCount - 1
                Dim dRelationIndex As Integer = gv51.GetRelationIndex(i, "Empresa_Extrato")
                Dim dView As GridView = TryCast(gv51.GetDetailView(i, 0), GridView)
                Dim aCollapsed As Boolean = dView Is Nothing
                If dView Is Nothing Then
                    gv51.ExpandMasterRow(i)
                    dView = TryCast(gv51.GetDetailView(i, dRelationIndex), GridView)
                End If
                If dView IsNot Nothing Then
                    dView.SetRowCellValue(0, dView.Columns(4), "")
                    dView.SetRowCellValue(dView.RowCount - 1, dView.Columns(4), "")
                End If
                If aCollapsed Then gv51.CollapseMasterRow(i)
            Next

            gv51.OptionsView.ColumnAutoWidth = False
            gv51.Columns(0).Width = 100
            gv51.Columns(1).Width = 300

            gv52.OptionsView.ColumnAutoWidth = False
            'gv52.Columns(9).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
            gv52.BestFitColumns()

            For i = 0 To gv51.RowCount - 1
                If CStr(gv51.GetRowCellValue(i, gv51.Columns(0))) = EmpresaGeral Then
                    gv51.ExpandMasterRow(i)
                Else
                    gv51.CollapseMasterRow(i)
                End If
            Next

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao atualizar os dados.", True)
        End Try
    End Sub

    Private Sub CarregaExtrato(dsExtrato As DataSet, StrConect As String, Cliente As String, Empresa As String, _
                               DataInicial As String, DataFinal As String)
        Try

            Dim sSql As String = ""
            Dim ssql2 As String = ""

            sSql = sSql & " SELECT '" & Empresa & "' AS Empresa,  DataDoc, TipoDoc, Serie, NumDoc,  "
            sSql = sSql & " CASE WHEN (ValorTotal + Historico.ValorDesconto - Historico.DifArredondamento) >= 0 THEN ABS(ValorTotal + Historico.ValorDesconto - Historico.DifArredondamento) ELSE 0 END AS Debito,"
            sSql = sSql & " CASE WHEN (ValorTotal + Historico.ValorDesconto - Historico.DifArredondamento) < 0 THEN ABS(ValorTotal + Historico.ValorDesconto - Historico.DifArredondamento) ELSE 0 END AS Credito"
            sSql = sSql & " FROM Historico "
            sSql = sSql & " WHERE TipoConta = 'CCC' AND TipoEntidade = 'C' AND Entidade = '" & Cliente & "'"
            sSql = sSql & "  AND (Historico.Modulo IN ('M','V'))"
            sSql = sSql & " AND Historico.DataDoc BETWEEN '" & DataInicial & "' AND '" & DataFinal & "'"
            sSql = sSql & "  ORDER BY Historico.DataDoc, Historico.TipoDoc, Historico.Serie, Historico.NumDoc "


            ssql2 = ssql2 & "  SELECT ISNULL(SUM(ValorTotal + Historico.ValorDesconto - Historico.DifArredondamento),0) AS SALDO"
            ssql2 = ssql2 & "  FROM Historico "
            ssql2 = ssql2 & "  WHERE TipoConta = 'CCC' AND TipoEntidade = 'C' AND Entidade = '" & Cliente & "'"
            ssql2 = ssql2 & "  AND (Historico.Modulo IN ('M','V'))"
            ssql2 = ssql2 & "  AND Historico.DataDoc < '" & DataInicial & "'"

            Dim SaldoAnterior As Double = 0
            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(ssql2, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read
                    SaldoAnterior = CDbl(reader.Item(0).ToString)
                Loop
            End Using

            Dim Saldo As Double = 0 + SaldoAnterior
            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                dsExtrato.Tables("Extrato").Rows.Add(New Object() {CStr(Empresa), CDate(DataInicial), CStr("S. Inicial"), _
                                                           CStr(""), CStr(""), 0, 0, CDbl(Saldo)})


                Do While reader.Read

                    Saldo = Saldo + (CDbl(reader.Item(5).ToString) - CDbl(reader.Item(6).ToString))

                    dsExtrato.Tables("Extrato").Rows.Add(New Object() _
                                                          {CStr(reader.Item(0).ToString), CDate(reader.Item(1).ToString), CStr(reader.Item(2).ToString), _
                                                           CStr(reader.Item(3).ToString), CLng(reader.Item(4).ToString), _
                                                           CDbl(reader.Item(5).ToString), CDbl(reader.Item(6).ToString), _
                                                           Saldo})
                Loop
            End Using

            dsExtrato.Tables("Extrato").Rows.Add(New Object() {CStr(Empresa), CDate(DataFinal), CStr("S. Final"), _
                                           CStr(""), CStr(""), 0, 0, CDbl(Saldo)})

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module
