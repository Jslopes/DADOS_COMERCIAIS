Imports System.Data.OleDb
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Data

Module m1DadosComerciais
    Public Sub AtualizarDadosComerciais(Empresa As String, Cliente As String, GridControl As GridControl, View As GridView, _
                                        StrConectFA As String, StrConectKL As String, StrConectJU As String)

        Try
            'Create a connection object. 
            Dim StrConect As String = ""
            Select Case Empresa
                Case "FASTIL"
                    StrConect = StrConectFA
                Case "KLICK"
                    StrConect = StrConectKL
                Case "JUALTEX"
                    StrConect = StrConectJU
            End Select
            'Dim StrConect As String = Plataforma.BaseDados.DaConnectionString(Plataforma.BaseDados.DaNomeBDdaEmpresa(Plataforma.Contexto.Empresa.CodEmp).ToString, "Default").ToString
            Dim Connection As New OleDbConnection(StrConect)

            '==================================== CRIAR BASE DA TABELA ===========================================
            Dim ArrayColunas() As String
            Dim ArrayCaption() As String
            Dim ArrayGetType() As System.Type
            'System.Type.GetType("System.Int32")
            ArrayColunas = {"Col", "FA", "KL", "JU", "TOT"}
            ArrayCaption = {"", "FASTIL", "KLICK", "JUALTEX", "Total"}
            ArrayGetType = {System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), _
                            System.Type.GetType("System.String"), System.Type.GetType("System.String")}
            '======================================================================================================

            '==================================== CARREGAR DADOS NA TABELA DE DETALHE =============================
            GridControl.DataSource = CarregaTblDadosComerciais(Cliente, ArrayColunas, ArrayCaption, ArrayGetType, StrConectFA, StrConectKL, StrConectJU)
            View.PopulateColumns()

            ' Alterar a aparencia do cabeçalho da grelha
            Dim i As Integer = 0
            For i = 0 To View.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                View.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                View.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                If i = 0 Then
                    View.Columns(i).Width = 150

                    View.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

                Else
                    View.Columns(i).Width = 150

                    View.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    View.Columns(i).DisplayFormat.FormatString = "N3"
                    View.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                End If

                View.Columns(i).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

            Next

            View.Columns(0).OptionsColumn.FixedWidth = True

            View.OptionsView.ColumnAutoWidth = False

            View.OptionsView.ColumnAutoWidth = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CarregaTblDadosComerciais(Cliente As String, ArrayColunas() As String, ArrayCaption() As String, ArrayGetType() As System.Type, _
                                                    StrConectFA As String, StrConectKL As String, StrConectJU As String) As DataTable
        Dim Tbl As DataTable
        Dim Row As DataRow

        Dim c As Integer = 0
        Dim sSql As String = ""

        Dim i As Integer = 0, k As Integer = 0, j As Integer = 0

        Tbl = New DataTable("DadosComerciais")
        Try
            '================================= CRIAR A TABELA ===============================================
            For c = 0 To ArrayColunas.Length - 1
                Dim Column As DataColumn = New DataColumn(ArrayColunas(c))
                With Column
                    .DataType = ArrayGetType(c)
                    .Caption = ArrayCaption(c)
                End With
                Tbl.Columns.Add(Column)
            Next
            '================================================================================================


            Tbl.Rows.Add(RowLimiteCredito(Tbl, Cliente, StrConectFA, StrConectKL, StrConectJU))
            Tbl.Rows.Add(RowUltimaCompra(Tbl, Cliente, StrConectFA, StrConectKL, StrConectJU))

            'incluir linha de separação
            Row = Tbl.NewRow()
            Row.Item(0) = ""
            Tbl.Rows.Add(Row)
            '===========================

            Saldos(Tbl, Cliente, StrConectFA, StrConectKL, StrConectJU)

            'incluir linha de separação
            Row = Tbl.NewRow()
            Row.Item(0) = ""
            Tbl.Rows.Add(Row)
            '===========================

            Carteira(Tbl, Cliente, StrConectFA, StrConectKL, StrConectJU)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Tbl

    End Function

    Public Function RowLimiteCredito(Tbl As DataTable, Cliente As String, StrConectFA As String, StrConectKL As String, StrConectJU As String) As DataRow
        Dim Row As DataRow
        Try
            'TOTAIS
            Row = Tbl.NewRow()
            Row.Item(0) = "Limite Crédito"

            Dim a() As String = {StrConectFA, StrConectKL, StrConectJU}
            Dim sSql As String = "SELECT LimiteCred FROM Clientes WHERE Clientes.cliente = '" & Cliente & "'"
            For i As Integer = 0 To a.Length - 1
                Using sqlCon As New OleDb.OleDbConnection(a(i).ToString)
                    Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                    sqlCon.Open()
                    Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                    Do While reader.Read
                        If IsNumeric(reader.Item(0).ToString) Then
                            Row.Item(i + 1) = CDbl(reader.Item(0).ToString)
                        Else
                            Row.Item(i + 1) = 0
                        End If
                        Exit Do
                    Loop
                End Using
            Next
            'Row.Item(1) = FormatNumber(CDbl(Motor.Comercial.Clientes.DaValorAtributo(Cliente, "LimiteCredito")), 2)
            'Row.Item(2) = FormatNumber(CDbl(MotorKL.Comercial.Clientes.DaValorAtributo(Cliente, "LimiteCredito")), 2)
            'Row.Item(3) = FormatNumber(CDbl(MotorJU.Comercial.Clientes.DaValorAtributo(Cliente, "LimiteCredito")), 2)
            Row.Item(4) = ""
            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function RowUltimaCompra(Tbl As DataTable, cliente As String, StrConectFA As String, StrConectKL As String, StrConectJU As String) As DataRow
        Dim Row As DataRow
        Try
            'TOTAIS
            Row = Tbl.NewRow()
            Row.Item(0) = "Data Ultima Compra"

            Dim a() As String = {StrConectFA, StrConectKL, StrConectJU}
            Dim sSql As String = "SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'"
            For i As Integer = 0 To a.Length - 1
                Using sqlCon As New OleDb.OleDbConnection(a(i).ToString)
                    Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                    sqlCon.Open()
                    Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                    Do While reader.Read
                        If IsDate(reader.Item(0).ToString) Then
                            Row.Item(i + 1) = FormatDateTime(CDate(reader.Item(0).ToString), DateFormat.ShortDate)
                        Else
                            Row.Item(i + 1) = FormatDateTime("2001-01-01", DateFormat.ShortDate)
                        End If
                        Exit Do
                    Loop
                End Using
            Next

            'Row.Item(1) = FormatDateTime(Motor.DSO.Consulta("SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'").Valor(0), DateFormat.ShortDate)
            'Row.Item(2) = FormatDateTime(MotorKL.DSO.Consulta("SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'").Valor(0), DateFormat.ShortDate)
            'Row.Item(3) = FormatDateTime(MotorJU.DSO.Consulta("SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'").Valor(0), DateFormat.ShortDate)

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Sub Saldos(Tbl As DataTable, cliente As String, StrConectFA As String, StrConectKL As String, StrConectJU As String)
        Dim i As Integer = 0, j As Integer
        Try
            Dim sSql As String = ""

            Dim Data As String = Now.Year & "-" & Now.Month & "-" & Now.Day

            sSql = sSql & " SELECT DATEDIFF(dd, '" & Data & "', DataVenc) * -1 AS DiasVenc, "
            sSql = sSql & " ValorPendente AS PENDENTE"
            sSql = sSql & " FROM pendentes "
            sSql = sSql & " WHERE Modulo = 'V' AND TipoEntidade = 'C' AND TipoConta = 'CCC'"
            sSql = sSql & " AND entidade = '" & cliente & "'"
            sSql = sSql & " ORDER BY DiasVenc"


            Dim RowSal As DataRow = Tbl.NewRow()
            Dim RowVen As DataRow = Tbl.NewRow()
            Dim Row30d As DataRow = Tbl.NewRow()
            Dim Row60d As DataRow = Tbl.NewRow()
            Dim Row90d As DataRow = Tbl.NewRow()
            Dim RowM90 As DataRow = Tbl.NewRow()

            'FASTIL
            'Motor.Comercial.Clientes.DaValorAtributo("", "")

            RowSal(0) = "Saldo à Data"
            RowVen(0) = "A Vencer"
            Row30d(0) = "A 30 dias"
            Row60d(0) = "A 60 dias"
            Row90d(0) = "A 90 Dias"
            RowM90(0) = "+ 90 Dias"


            Dim a() As String = {StrConectFA, StrConectKL, StrConectJU}
            For j = 1 To a.Length
                Using sqlCon As New OleDb.OleDbConnection(a(j - 1).ToString)
                    Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                    sqlCon.Open()
                    Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                    RowSal(j) = 0
                    RowVen(j) = 0
                    Row30d(j) = 0
                    Row60d(j) = 0
                    Row90d(j) = 0
                    RowM90(j) = 0

                    Do While reader.Read
                        Select Case CInt(reader.Item(0).ToString)
                            Case Is <= 0
                                RowVen(j) = FormatNumber(RowVen(j) + CDbl(reader.Item(1).ToString), 2)
                            Case 1 To 30
                                Row30d(j) = FormatNumber(Row30d(j) + CDbl(reader.Item(1).ToString), 2)
                            Case 31 To 60
                                Row60d(j) = FormatNumber(Row60d(j) + CDbl(reader.Item(1).ToString), 2)
                            Case 61 To 91
                                Row90d(j) = FormatNumber(Row90d(j) + CDbl(reader.Item(1).ToString), 2)
                            Case Is > 90
                                RowM90(j) = FormatNumber(RowM90(j) + CDbl(reader.Item(1).ToString), 2)
                        End Select
                    Loop

                End Using

                RowSal(j) = CDbl(RowVen(j)) + CDbl(Row30d(j)) + CDbl(Row60d(j)) + CDbl(Row90d(j)) + CDbl(RowM90(j))

            Next


            'For j = 1 To 3
            '    Dim ListaCC As New Interop.StdBE800.StdBELista
            '    Select Case j
            '        Case 1
            '            ListaCC = Motor.Consulta(sSql)
            '        Case 2
            '            ListaCC = MotorKL.Consulta(sSql)
            '        Case 3
            '            ListaCC = MotorJU.Consulta(sSql)
            '    End Select
            '    RowSal(j) = 0
            '    RowVen(j) = 0
            '    Row30d(j) = 0
            '    Row60d(j) = 0
            '    Row90d(j) = 0
            '    RowM90(j) = 0
            '    For i = 1 To ListaCC.NumLinhas
            '        Select Case CInt(ListaCC.Valor(0).ToString)
            '            Case Is <= 0
            '                RowVen(j) = FormatNumber(RowVen(j) + CDbl(ListaCC.Valor(1).ToString), 2)
            '            Case 1 To 30
            '                Row30d(j) = FormatNumber(Row30d(j) + CDbl(ListaCC.Valor(1).ToString), 2)
            '            Case 31 To 60
            '                Row60d(j) = FormatNumber(Row60d(j) + CDbl(ListaCC.Valor(1).ToString), 2)
            '            Case 61 To 91
            '                Row90d(j) = FormatNumber(Row90d(j) + CDbl(ListaCC.Valor(1).ToString), 2)
            '            Case Is > 90
            '                RowM90(j) = FormatNumber(RowM90(j) + CDbl(ListaCC.Valor(1).ToString), 2)
            '        End Select
            '        ListaCC.Seguinte()
            '    Next
            '    RowSal(j) = CDbl(RowVen(j)) + CDbl(Row30d(j)) + CDbl(Row60d(j)) + CDbl(Row90d(j)) + CDbl(RowM90(j))
            'Next

            RowSal(4) = FormatNumber(CDbl(RowSal(1)) + CDbl(RowSal(2)) + CDbl(RowSal(3)), 2)
            RowVen(4) = FormatNumber(CDbl(RowVen(1)) + CDbl(RowVen(2)) + CDbl(RowVen(3)), 2)
            Row30d(4) = FormatNumber(CDbl(Row30d(1)) + CDbl(Row30d(2)) + CDbl(Row30d(3)), 2)
            Row60d(4) = FormatNumber(CDbl(Row60d(1)) + CDbl(Row60d(2)) + CDbl(Row60d(3)), 2)
            Row90d(4) = FormatNumber(CDbl(Row90d(1)) + CDbl(Row90d(2)) + CDbl(Row90d(3)), 2)
            RowM90(4) = FormatNumber(CDbl(RowM90(1)) + CDbl(RowM90(2)) + CDbl(RowM90(3)), 2)

            Tbl.Rows.Add(RowSal)
            Tbl.Rows.Add(RowVen)
            Tbl.Rows.Add(Row30d)
            Tbl.Rows.Add(Row60d)
            Tbl.Rows.Add(Row90d)
            Tbl.Rows.Add(RowM90)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Carteira(Tbl As DataTable, cliente As String, StrConectFA As String, StrConectKL As String, StrConectJU As String)
        Dim i As Integer = 0, j As Integer
        Try

            Dim sSql As String = ""

            sSql = sSql & " SELECT        dbo.LinhasDoc.Data, dbo.CabecDoc.TipoDoc, dbo.CabecDoc.Serie, dbo.CabecDoc.NumDoc, dbo.LinhasDoc.NumLinha, "
            sSql = sSql & "                          dbo.LinhasDoc.Artigo, dbo.LinhasDoc.Quantidade AS QtdEnc, dbo.LinhasDoc.PrecoLiquido AS ValEnc"
            sSql = sSql & " FROM            dbo.LinhasDoc INNER JOIN"
            sSql = sSql & "                          dbo.CabecDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc INNER JOIN"
            sSql = sSql & "                          dbo.LinhasDocStatus ON dbo.LinhasDocStatus.IdLinhasDoc = dbo.LinhasDoc.Id INNER JOIN"
            sSql = sSql & "                          dbo.CabecDocStatus ON dbo.CabecDocStatus.IdCabecDoc = dbo.CabecDoc.Id INNER JOIN"
            sSql = sSql & "                          dbo.Artigo ON dbo.Artigo.Artigo = dbo.LinhasDoc.Artigo"
            sSql = sSql & " WHERE        (dbo.CabecDoc.TipoDoc = 'ECL') "
            sSql = sSql & " AND (dbo.LinhasDocStatus.EstadoTrans = 'P') AND (dbo.LinhasDocStatus.Fechado = 0) AND (dbo.CabecDocStatus.Anulado = 0) "
            sSql = sSql & " AND (dbo.LinhasDoc.Quantidade > 0)"
            sSql = sSql & " AND dbo.CabecDoc.Entidade = '" & cliente & "'"

            Dim RowCrt As DataRow = Tbl.NewRow()

            RowCrt(0) = "Carteira"
            RowCrt(4) = 0

            Dim a() As String = {StrConectFA, StrConectKL, StrConectJU}
            For j = 1 To a.Length
                Using sqlCon As New OleDb.OleDbConnection(a(j - 1).ToString)
                    Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                    sqlCon.Open()
                    Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)

                    RowCrt(j) = 0

                    Do While reader.Read

                        RowCrt(j) = FormatNumber(CDbl(RowCrt(j)) + CDbl(reader.Item(7).ToString), 2)

                    Loop

                End Using

                RowCrt(4) = FormatNumber(CDbl(RowCrt(4)) + CDbl(RowCrt(j)), 2)

            Next


            'For j = 1 To 3
            '    Dim ListaCC As New Interop.StdBE800.StdBELista
            '    Select Case j
            '        Case 1
            '            ListaCC = Motor.Consulta(sSql)
            '        Case 2
            '            ListaCC = MotorKL.Consulta(sSql)
            '        Case 3
            '            ListaCC = MotorJU.Consulta(sSql)
            '    End Select
            '    RowCrt(j) = 0
            '    For i = 1 To ListaCC.NumLinhas

            '        RowCrt(j) = FormatNumber(CDbl(RowCrt(j)) + CDbl(ListaCC.Valor(7).ToString), 2)

            '        ListaCC.Seguinte()
            '    Next
            '    RowCrt(4) = FormatNumber(CDbl(RowCrt(4)) + CDbl(RowCrt(j)), 2)
            'Next

            Tbl.Rows.Add(RowCrt)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
