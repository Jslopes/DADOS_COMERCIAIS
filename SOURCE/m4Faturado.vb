Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Module m4Faturado
    Public Sub AtualizarFaturado(dsEncCarteira As DataSet, Cliente As String, _
                              gv41 As GridView, gv42 As GridView, StrConectFA As String, StrConectKL As String, StrConectJU As String)
        Try
            'Create a connection object. 
            'Dim StrConectFA As String = Plataforma.BaseDados.DaConnectionString(Plataforma.BaseDados.DaNomeBDdaEmpresa(Plataforma.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            'Dim StrConectKL As String = PlataformaKL.BaseDados.DaConnectionString(PlataformaKL.BaseDados.DaNomeBDdaEmpresa(PlataformaKL.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            'Dim StrConectJU As String = PlataformaJU.BaseDados.DaConnectionString(PlataformaJU.BaseDados.DaNomeBDdaEmpresa(PlataformaJU.Contexto.Empresa.CodEmp).ToString, "Default").ToString

            CarregaEmpresas(dsEncCarteira, StrConectFA.Replace("PRIFASTIL", "PRIEMPRE"))

            dsEncCarteira.Tables("CabecDoc").Clear()
            CarregaDadosCabec(dsEncCarteira, StrConectFA, Cliente, CStr("FASTIL"))
            CarregaDadosCabec(dsEncCarteira, StrConectKL, Cliente, CStr("KLICK"))
            CarregaDadosCabec(dsEncCarteira, StrConectJU, Cliente, CStr("JUALTEX"))

            'dsEncCarteira.Tables("Faturas").Clear()
            'CarregaDadosLinhas(dsEncCarteira, StrConectFA, Cliente)
            'CarregaDadosLinhas(dsEncCarteira, StrConectKL, Cliente)
            'CarregaDadosLinhas(dsEncCarteira, StrConectJU, Cliente)

            'Configurar carateristicas das colunas
            Dim i As Integer = 0
            For i = 0 To gv41.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv41.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv41.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                gv41.Columns(i).OptionsColumn.AllowEdit = False
            Next

            For i = 0 To gv42.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv42.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv42.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center
                gv42.Columns(i).OptionsColumn.AllowEdit = False

            Next

            'For i = 0 To gv43.Columns.Count - 1
            '    'aparencia no cabeçalho deve ser editada no formulário
            '    gv43.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '    gv43.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

            '    gv43.Columns(i).OptionsColumn.AllowEdit = False

            'Next

            gv41.OptionsView.ColumnAutoWidth = False
            gv41.Columns(0).Width = 100
            gv41.Columns(1).Width = 300

            gv42.OptionsView.ColumnAutoWidth = False
            gv42.BestFitColumns()

            'gv43.OptionsView.ColumnAutoWidth = False

            For i = 0 To gv41.RowCount - 1
                If CStr(gv41.GetRowCellValue(i, gv41.Columns(0))) = EmpresaGeral Then
                    gv41.ExpandMasterRow(i)
                Else
                    gv41.CollapseMasterRow(i)
                End If
            Next

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao atualizar os dados.", True)
        End Try
    End Sub

    Private Sub CarregaDadosCabec(dsEncCarteira As DataSet, StrConect As String, Cliente As String, Empresa As String)
        Try
            Dim sSql As String = ""
            sSql = sSql & " SELECT '" & Empresa & "', CabecDoc.TipoDoc, CabecDoc.Serie, CabecDoc.NumDoc, CabecDoc.Data,  "
            sSql = sSql & " CabECL.TipoDoc, CabECL.Serie, CabECL.NumDoc, CabECL.Data AS [Data Pedido], "
            sSql = sSql & " (CabecDoc.TotalMerc + CabecDoc.TotalOutros - CabecDoc.TotalDesc) AS TotalDoc, SUM(LinhasDoc.PrecoLiquido) AS [Total ECL]"
            sSql = sSql & "         FROM CabecDoc "
            sSql = sSql & " INNER JOIN CabecDocStatus On CabecDocStatus.IdCabecDoc = CabecDoc.Id"
            sSql = sSql & " INNER JOIN LinhasDoc ON LinhasDoc.IdCabecDoc = CabecDoc.Id"
            sSql = sSql & " LEFT JOIN LinhasDocTrans ON LinhasDocTrans.IdLinhasDoc = LinhasDoc.Id"
            sSql = sSql & " LEFT JOIN LinhasDoc AS LinECL ON LinECL.id =  LinhasDocTrans.IdLinhasDocOrigem"
            sSql = sSql & " INNER JOIN CabecDoc AS CabECL ON CabECL.Id = LinECL.IdCabecDoc "
            sSql = sSql & " WHERE CabecDoc.TipoDoc = 'FA' AND CabecDocStatus.Anulado = 0 AND CabecDoc.Entidade = '" & Cliente & "'"
            sSql = sSql & " Group by CabecDoc.TipoDoc, CabecDoc.Serie, CabecDoc.NumDoc, CabecDoc.Data,CabECL.TipoDoc, CabECL.Serie, CabECL.NumDoc, CabECL.Data, (CabecDoc.TotalMerc + CabecDoc.TotalOutros - CabecDoc.TotalDesc)"
            sSql = sSql & " Order by CabecDoc.Data DESC, CabecDoc.Serie, CabecDoc.Numdoc"

            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read

                    dsEncCarteira.Tables("CabecDoc").Rows.Add(New Object() _
                                    {CStr(reader.Item(0).ToString), _
                                    CStr(reader.Item(1).ToString), CStr(reader.Item(2).ToString), CStr(reader.Item(3).ToString), CDate(reader.Item(4).ToString), CDbl(reader.Item(9).ToString), _
                                    CStr(reader.Item(5).ToString), CStr(reader.Item(6).ToString), CStr(reader.Item(7).ToString), CDate(reader.Item(8).ToString), CDbl(reader.Item(10).ToString)})
                Loop
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CarregaDadosLinhas(dsEncCarteira As DataSet, StrConect As String, Cliente As String)

        Dim i As Integer = 0, k As Integer = 0, j As Integer = 0

        Try

            Dim sSql As String = ""
            sSql = sSql & " SELECT DISTINCT c1.id AS Origem,"
            sSql = sSql & " c2.TipoDoc, c2.Serie, c2.NumDoc, c2.Data, c2.Entidade, c2.Nome, (c2.TotalMerc + C2.TotalOutros - C2.TotalDesc) AS TOTAL "
            sSql = sSql & " FROM CabecDoc as c1"
            sSql = sSql & " INNER JOIN linhasdoc AS l1 ON l1.IdCabecDoc  = c1.id"
            sSql = sSql & " INNER JOIN LinhasDocTrans ON LinhasDocTrans.IdLinhasDocOrigem = l1.Id"
            sSql = sSql & " INNER JOIN linhasdoc AS l2 ON l2.Id = LinhasDocTrans.IdLinhasDoc"
            sSql = sSql & "  INNER JOIN cabecdoc AS c2 ON c2.id = l2.IdCabecDoc "
            sSql = sSql & "  WHERE c1.TipoDoc ='ECL'"
            sSql = sSql & " AND c1.Entidade = '" & Cliente & "'"


            Using sqlCon As New OleDb.OleDbConnection(StrConect)

                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                Do While reader.Read

                    dsEncCarteira.Tables("Faturas").Rows.Add(New Object() _
                                                          {CStr(reader.Item(0).ToString), CStr(reader.Item(1).ToString), CStr(reader.Item(2).ToString), _
                                                           CLng(reader.Item(3).ToString), CDate(reader.Item(4).ToString), _
                                                           CStr(reader.Item(5).ToString), CStr(reader.Item(6).ToString), _
                                                           CDbl(reader.Item(7).ToString)})
                Loop

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Module
