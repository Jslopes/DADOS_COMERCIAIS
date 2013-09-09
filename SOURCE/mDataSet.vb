Imports System.Data

Module mDataSet
    Public Sub CarregaEmpresas(ds As DataSet)
        Try
            ds.Tables("Empresa").Clear()
            ds.Tables("Empresa").Rows.Add(New Object() {CStr(PlataformaFA.Contexto.Empresa.CodEmp), CStr(PlataformaFA.Contexto.Empresa.IDNome)})
            ds.Tables("Empresa").Rows.Add(New Object() {CStr(PlataformaKL.Contexto.Empresa.CodEmp), CStr(PlataformaKL.Contexto.Empresa.IDNome)})
            ds.Tables("Empresa").Rows.Add(New Object() {CStr(PlataformaJU.Contexto.Empresa.CodEmp), CStr(PlataformaJU.Contexto.Empresa.IDNome)})

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao carregar as empresas.", True)
        End Try
    End Sub
End Module
