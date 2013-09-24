Imports System.Data.OleDb
Imports System.Data
Imports System.Math

Module m2VendasAnuais
    Public Function CalcularValorMes(cliente As String, iAno As Integer, iMes As Integer, StrConect As String) As Double
        Dim sSql As String = ""
        Dim dValor As Double
        Try
            Dim tmp As Double = iMes / 12
            If tmp > 0 And tmp <= 1 Then
                iAno = iAno + 0
                iMes = iMes - 0
            ElseIf tmp > 1 And tmp <= 2 Then
                iAno = iAno + 1
                iMes = iMes - 12
            ElseIf tmp > 2 And tmp <= 3 Then
                iAno = iAno + 2
                iMes = iMes - 24
            End If
            tmp = Abs(tmp)

            sSql = ""
            sSql = sSql & " SELECT ISNULL(SUM(LinhasDoc.PrecoLiquido),0) AS Total FROM LinhasDoc "
            sSql = sSql & " INNER JOIN CabecDoc ON CabecDoc.id = LinhasDoc.IdCabecDoc"
            sSql = sSql & " WHERE CabecDoc.TipoDoc  IN ('FA', 'NC') AND "
            sSql = sSql & "     YEAR(LinhasDoc.Data) = '" & iAno & "' AND MONTH(LinhasDoc.Data) = '" & iMes & "' AND "
            sSql = sSql & "     CabecDoc.Entidade = '" & cliente & "'"

            'Dim StrConect As String = Plataforma.BaseDados.DaConnectionString(Plataforma.BaseDados.DaNomeBDdaEmpresa(Plataforma.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim Connection As New OleDbConnection(StrConect)
            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                Do While reader.Read
                    dValor = CDbl(reader.Item(0).ToString)
                Loop

            End Using

            Return dValor

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return 0
        End Try
    End Function
End Module
