Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Module m5Extrato
    Public Sub AtualizarExtrato(dsExtrato As DataSet, Cliente As String, _
                          gv51 As GridView, gv52 As GridView, _
                          DataInicial As String, DataFinal As String)
        Try
            'Create a connection object. 
            Dim StrConectFA As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim StrConectKL As String = PlataformaKL.BaseDados.DaConnectionString(PlataformaKL.BaseDados.DaNomeBDdaEmpresa(PlataformaKL.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim StrConectJU As String = PlataformaJU.BaseDados.DaConnectionString(PlataformaJU.BaseDados.DaNomeBDdaEmpresa(PlataformaJU.Contexto.Empresa.CodEmp).ToString, "Default").ToString

            CarregaEmpresas(dsExtrato)

            dsExtrato.Tables("Extrato").Clear()
            CarregaExtrato(dsExtrato, StrConectFA, Cliente, CStr(PlataformaFA.Contexto.Empresa.CodEmp), DataInicial, DataFinal)
            CarregaExtrato(dsExtrato, StrConectKL, Cliente, CStr(PlataformaKL.Contexto.Empresa.CodEmp), DataInicial, DataFinal)
            CarregaExtrato(dsExtrato, StrConectJU, Cliente, CStr(PlataformaJU.Contexto.Empresa.CodEmp), DataInicial, DataFinal)

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
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao atualizar os dados.", True)
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
                                                           CStr(""), 0, 0, 0, CDbl(Saldo)})


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
                                           CStr(""), 0, 0, 0, CDbl(Saldo)})

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module
