Imports System.Data

Module mDataSet
    Public Sub CarregaEmpresas(ds As DataSet, strCon As String)
        Try
            ds.Tables("Empresa").Clear()

            Dim strConect As String = ""
            Dim sSql As String = "SELECT Codigo, IdNome as Nome FROM Empresas WHERE Codigo IN ('FASTIL','JUALTEX','KLICK')"

            Using sqlCon As New OleDb.OleDbConnection(strCon)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read
                    ds.Tables("Empresa").Rows.Add(New Object() {CStr(reader.Item(0).ToString), CStr(reader.Item(1).ToString)})
                Loop
            End Using


            'ds.Tables("Empresa").Rows.Add(New Object() {CStr("FASTIL"), CStr(Plataforma.Contexto.Empresa.IDNome)})
            'ds.Tables("Empresa").Rows.Add(New Object() {CStr("KLICK"), CStr(PlataformaKL.Contexto.Empresa.IDNome)})
            'ds.Tables("Empresa").Rows.Add(New Object() {CStr("JUALTEX"), CStr(PlataformaJU.Contexto.Empresa.IDNome)})

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao carregar as empresas.", True)
        End Try
    End Sub
End Module
