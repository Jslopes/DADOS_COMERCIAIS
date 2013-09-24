Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid

Module m6DocAberto
    Public Sub AtualizarDocsAberto(dsDocsAberto As DataSet, Cliente As String, _
                              gv61 As GridView, gv62 As GridView, StrConectFA As String, StrConectKL As String, StrConectJU As String)
        Try

            CarregaEmpresas(dsDocsAberto, StrConectFA.Replace("PRIFASTIL", "PRIEMPRE"))

            dsDocsAberto.Tables("Pendentes").Clear()
            CarregaPendentes(dsDocsAberto, StrConectFA, Cliente, CStr("FASTIL"))
            CarregaPendentes(dsDocsAberto, StrConectKL, Cliente, CStr("KLICK"))
            CarregaPendentes(dsDocsAberto, StrConectJU, Cliente, CStr("JUALTEX"))

            'Configurar carateristicas das colunas
            Dim i As Integer = 0
            For i = 0 To gv61.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv61.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv61.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                gv61.Columns(i).OptionsColumn.AllowEdit = False
            Next

            For i = 0 To gv62.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                gv62.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gv62.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center
                gv62.Columns(i).OptionsColumn.AllowEdit = False

            Next

            gv61.OptionsView.ColumnAutoWidth = False
            gv61.Columns(0).Width = 100
            gv61.Columns(1).Width = 300

            gv62.OptionsView.ColumnAutoWidth = False


            If gv62.Columns(9).Summary.ActiveCount > 0 Then
                gv62.Columns(9).Summary.Remove(gv62.Columns(9).Summary.Item(0))
            End If
            'gv62.Columns(9).Summary.Remove(gv62.Columns(9).Summary.Item(0))
            gv62.Columns(9).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
            gv62.BestFitColumns()

            For i = 0 To gv61.RowCount - 1
                If CStr(gv61.GetRowCellValue(i, gv61.Columns(0))) = EmpresaGeral Then
                    gv61.ExpandMasterRow(i)
                Else
                    gv61.CollapseMasterRow(i)
                End If
            Next

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao atualizar os dados.", True)
        End Try
    End Sub

    Private Sub CarregaPendentes(dsPendentes As DataSet, StrConect As String, Cliente As String, Empresa As String)
        Try
            Dim sSql As String = ""
            sSql = sSql & " SELECT '" & Empresa & "' AS Empresa, TipoConta, TipoDoc, Serie, NumDoc, Estado, DataVenc, DataDoc, DATEDIFF(dd, '" & Now.Year & "-" & Now.Month & "-" & Now.Day & "', DataVenc) * -1 AS Dias, ValorPendente, * "
            sSql = sSql & " FROM Pendentes "
            sSql = sSql & " WHERE TipoConta = 'CCC' AND TipoEntidade = 'C' AND Estado = 'PEN' AND Entidade = '" & Cliente & "'"
            sSql = sSql & " ORDER BY Pendentes.DataDoc, Pendentes.TipoDoc, Pendentes.Serie, Pendentes.NumDoc "

            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read

                    dsPendentes.Tables("Pendentes").Rows.Add(New Object() _
                                                          {CStr(reader.Item(0).ToString), CStr(reader.Item(1).ToString), CStr(reader.Item(2).ToString), _
                                                           CStr(reader.Item(3).ToString), CStr(reader.Item(4).ToString), CStr(reader.Item(5).ToString), _
                                                           CDate(reader.Item(6).ToString), CDate(reader.Item(7).ToString), CInt(reader.Item(8).ToString), CDbl(reader.Item(9).ToString)})
                Loop
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
