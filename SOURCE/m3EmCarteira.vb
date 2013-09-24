Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Module m3EmCarteira
    Public Sub AtualizarCarteira(dsEncCarteira As DataSet, Cliente As String, _
                                  gv31 As GridView, gv32 As GridView, gv33 As GridView, StrConectFA As String, StrConectKL As String, StrConectJU As String)
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

            dsEncCarteira.Tables("LinhasDoc").Clear()
            CarregaDadosLinhas(dsEncCarteira, StrConectFA, Cliente)
            CarregaDadosLinhas(dsEncCarteira, StrConectKL, Cliente)
            CarregaDadosLinhas(dsEncCarteira, StrConectJU, Cliente)

            'Configurar carateristicas das colunas
            Dim i As Integer = 0
            For i = 0 To gv31.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv31.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv31.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                gv31.Columns(i).OptionsColumn.AllowEdit = False
            Next

            For i = 0 To gv32.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv32.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv32.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center
                gv32.Columns(i).OptionsColumn.AllowEdit = False

            Next

            For i = 0 To gv33.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv33.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv33.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                gv33.Columns(i).OptionsColumn.AllowEdit = False

            Next

            gv31.OptionsView.ColumnAutoWidth = False
            gv31.Columns(0).Width = 100
            gv31.Columns(1).Width = 300

            gv32.OptionsView.ColumnAutoWidth = False
            gv32.BestFitColumns()

            gv33.OptionsView.ColumnAutoWidth = False

            For i = 0 To gv31.RowCount - 1
                If CStr(gv31.GetRowCellValue(i, gv31.Columns(0))) = EmpresaGeral Then
                    gv31.ExpandMasterRow(i)
                Else
                    gv31.CollapseMasterRow(i)
                End If
            Next

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao atualizar os dados.", True)
        End Try
    End Sub

    Private Sub CarregaDadosCabec(dsEncCarteira As DataSet, StrConect As String, Cliente As String, Empresa As String)
        Try
            Dim sSql As String = ""
            sSql = sSql & "SELECT Id, Serie, NumDoc, Entidade, Nome, Data, '" & Empresa & "', CabecDoc.TotalMerc"
            sSql = sSql & " FROM CabecDoc "
            sSql = sSql & " INNER JOIN CabecDocStatus On CabecDocStatus.IdCabecDoc = CabecDoc.Id"
            sSql = sSql & " WHERE CabecDoc.TipoDoc = 'ECL' AND CabecDocStatus.Estado = 'P' AND CabecDocStatus.Anulado = 0 AND CabecDocStatus.Fechado = 0"
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
            sSql = sSql & "SELECT        dbo.LinhasDoc.IdCabecDoc, dbo.LinhasDoc.Data, dbo.CabecDoc.TipoDoc, dbo.CabecDoc.Serie, dbo.CabecDoc.NumDoc, dbo.LinhasDoc.NumLinha, "
            sSql = sSql & "             dbo.LinhasDoc.Artigo, dbo.LinhasDoc.Descricao, dbo.LinhasDoc.Quantidade AS QtdEnc, dbo.LinhasDoc.PrecoLiquido AS ValEnc, "
            sSql = sSql & "            dbo.MCR_CalcQtdProduzida(dbo.LinhasDoc.Id) AS Qtd_TotProd, dbo.LinhasDocStatus.QuantTrans AS Qtd_Faturadas, " '12
            sSql = sSql & "             dbo.LinhasDoc.Quantidade - dbo.MCR_CalcQtdProduzida(dbo.LinhasDoc.Id) AS Qtd_PProd, " '13
            sSql = sSql & "             (dbo.LinhasDoc.Quantidade - dbo.MCR_CalcQtdProduzida(dbo.LinhasDoc.Id)) * (dbo.LinhasDoc.PrecoLiquido / dbo.LinhasDoc.Quantidade) AS Val_PProd, "
            sSql = sSql & "             dbo.MCR_CalcQtdProduzida(dbo.LinhasDoc.Id) - dbo.LinhasDocStatus.QuantTrans AS Qtd_JProd, (dbo.MCR_CalcQtdProduzida(dbo.LinhasDoc.Id) "
            sSql = sSql & "              - dbo.LinhasDocStatus.QuantTrans) * (dbo.LinhasDoc.PrecoLiquido / dbo.LinhasDoc.Quantidade) AS Val_JProd,"
            sSql = sSql & " dbo.LinhasDocStatus.EstadoTrans, dbo.LinhasDocStatus.Fechado, dbo.LinhasDoc.DataEntrega "
            sSql = sSql & " FROM            dbo.LinhasDoc INNER JOIN"
            sSql = sSql & "                         dbo.CabecDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc INNER JOIN"
            sSql = sSql & "                          dbo.LinhasDocStatus ON dbo.LinhasDocStatus.IdLinhasDoc = dbo.LinhasDoc.Id INNER JOIN"
            sSql = sSql & "                          dbo.CabecDocStatus ON dbo.CabecDocStatus.IdCabecDoc = dbo.CabecDoc.Id INNER JOIN"
            sSql = sSql & "                          dbo.Artigo ON dbo.Artigo.Artigo = dbo.LinhasDoc.Artigo"
            sSql = sSql & " WHERE        CabecDoc.TipoDoc = 'ECL' AND CabecDocStatus.Estado = 'P' AND CabecDocStatus.Anulado = 0 AND CabecDocStatus.Fechado = 0"
            sSql = sSql & " AND (dbo.LinhasDoc.Quantidade > 0)"
            sSql = sSql & " AND (linhasdoc.TipoLinha = '10' OR linhasdoc.TipoLinha = '11') AND LinhasDocStatus.Fechado = 0 "
            sSql = sSql & " AND CabecDoc.Entidade = '" & Cliente & "'"
            sSql = sSql & " ORDER BY linhasdoc.NumLinha"

            Using sqlCon As New OleDb.OleDbConnection(StrConect)

                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                Do While reader.Read

                    dsEncCarteira.Tables("LinhasDoc").Rows.Add(New Object() _
                                                          {CStr(reader.Item(0).ToString), CStr(reader.Item(5).ToString), _
                                                           CStr(reader.Item(6).ToString), CStr(reader.Item(7).ToString), _
                                                           CDbl(reader.Item(8).ToString), CStr(reader.Item(18).ToString), _
                                                           CDbl(reader.Item(11).ToString), CDbl(reader.Item(10).ToString)})
                Loop

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Module
