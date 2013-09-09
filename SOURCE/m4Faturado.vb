Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Module m4Faturado
    Public Sub AtualizarFaturado(dsEncCarteira As DataSet, Cliente As String, _
                              gv41 As GridView, gv42 As GridView, gv43 As GridView)
        Try
            'Create a connection object. 
            Dim StrConectFA As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim StrConectKL As String = PlataformaKL.BaseDados.DaConnectionString(PlataformaKL.BaseDados.DaNomeBDdaEmpresa(PlataformaKL.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim StrConectJU As String = PlataformaJU.BaseDados.DaConnectionString(PlataformaJU.BaseDados.DaNomeBDdaEmpresa(PlataformaJU.Contexto.Empresa.CodEmp).ToString, "Default").ToString

            CarregaEmpresas(dsEncCarteira)

            dsEncCarteira.Tables("CabecDoc").Clear()
            CarregaDadosCabec(dsEncCarteira, StrConectFA, Cliente, CStr(PlataformaFA.Contexto.Empresa.CodEmp))
            CarregaDadosCabec(dsEncCarteira, StrConectKL, Cliente, CStr(PlataformaKL.Contexto.Empresa.CodEmp))
            CarregaDadosCabec(dsEncCarteira, StrConectJU, Cliente, CStr(PlataformaJU.Contexto.Empresa.CodEmp))

            dsEncCarteira.Tables("Faturas").Clear()
            CarregaDadosLinhas(dsEncCarteira, StrConectFA, Cliente)
            CarregaDadosLinhas(dsEncCarteira, StrConectKL, Cliente)
            CarregaDadosLinhas(dsEncCarteira, StrConectJU, Cliente)

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

            For i = 0 To gv43.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv43.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv43.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                gv43.Columns(i).OptionsColumn.AllowEdit = False

            Next

            gv41.OptionsView.ColumnAutoWidth = False
            gv41.Columns(0).Width = 100
            gv41.Columns(1).Width = 300

            gv42.OptionsView.ColumnAutoWidth = False
            gv42.BestFitColumns()

            gv43.OptionsView.ColumnAutoWidth = False

            For i = 0 To gv41.RowCount - 1
                If CStr(gv41.GetRowCellValue(i, gv41.Columns(0))) = EmpresaGeral Then
                    gv41.ExpandMasterRow(i)
                Else
                    gv41.CollapseMasterRow(i)
                End If
            Next

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao atualizar os dados.", True)
        End Try
    End Sub

    Private Sub CarregaDadosCabec(dsEncCarteira As DataSet, StrConect As String, Cliente As String, Empresa As String)
        Try
            Dim sSql As String = ""
            sSql = sSql & "SELECT Id, Serie, NumDoc, Entidade, Nome, Data, '" & Empresa & "', CabecDoc.TotalMerc"
            sSql = sSql & " FROM CabecDoc "
            sSql = sSql & " INNER JOIN CabecDocStatus On CabecDocStatus.IdCabecDoc = CabecDoc.Id"
            sSql = sSql & " WHERE CabecDoc.TipoDoc = 'ECL' AND CabecDocStatus.Estado = 'T' AND CabecDocStatus.Anulado = 0"
            sSql = sSql & " AND CabecDoc.Entidade = '" & Cliente & "'"
            sSql = sSql & " Order by CabecDoc.Serie, CabecDoc.Numdoc, CabecDoc.Data"

            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read

                    dsEncCarteira.Tables("CabecDoc").Rows.Add(New Object() _
                                                          {CStr(reader.Item(0).ToString), CStr(reader.Item(1).ToString), CLng(reader.Item(2).ToString), _
                                                           CStr(reader.Item(3).ToString), CStr(reader.Item(4).ToString), CDate(reader.Item(5).ToString), _
                                                           CStr(reader.Item(6).ToString), CDbl(reader.Item(7).ToString)})
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
