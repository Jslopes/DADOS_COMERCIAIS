Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data
Imports System.Drawing
Imports DevExpress.Utils

Public Class frmDadosComerciais

    Dim FormInicializado As Boolean = False

    Private Sub frmDadosComerciais_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If Not FormInicializado Then
            txtCliente.Text = ClienteGeral

            ValidarCliente(ClienteGeral)

        End If
        FormInicializado = True
    End Sub

    Private Sub frmDadosComerciais_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveMyForm(Me)
    End Sub

    Private Sub frmDadosComerciais_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.CenterToScreen()

        Try
            LoadMyForm(Me, True)
            ClearTextBox(Me)

            LookBaseF4(txtCliente, My.Resources.F4, 48)
            LookBaseTxt(txtNome)

            Dim txtVnd() As DevExpress.XtraEditors.TextEdit = _
                {txtVnd101, txtVnd102, txtVnd103, txtVnd104, txtVnd105, txtVnd106, txtVnd107, txtVnd108, txtVnd109, txtVnd110, txtVnd111, txtVnd112, _
                 txtVnd201, txtVnd202, txtVnd203, txtVnd204, txtVnd205, txtVnd206, txtVnd207, txtVnd208, txtVnd209, txtVnd210, txtVnd211, txtVnd212, _
                 txtVnd301, txtVnd302, txtVnd303, txtVnd304, txtVnd305, txtVnd306, txtVnd307, txtVnd308, txtVnd309, txtVnd310, txtVnd311, txtVnd312}

            For i As Integer = 0 To txtVnd.Length - 1
                LookBaseF4(txtVnd(i), My.Resources.view)
                txtVnd(i).RightToLeft = Windows.Forms.RightToLeft.Yes
                txtVnd(i).Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                txtVnd(i).Properties.Mask.EditMask = "n2"
                txtVnd(i).Properties.DisplayFormat.FormatType = FormatType.Numeric
                txtVnd(i).Properties.EditFormat.FormatType = FormatType.Numeric
                txtVnd(i).Properties.ReadOnly = True
                txtVnd(i).Text = "0,00"
            Next

            LookBaseTxt(TextEdit1)
            LookBaseTxt(TextEdit2)
            LookBaseTxt(TextEdit3)
            LookBaseTxt(TextEdit4)
            LookBaseTxt(TextEdit5)
            LookBaseTxt(TextEdit6)
            LookBaseTxt(TextEdit7)
            LookBaseTxt(TextEdit8)
            LookBaseTxt(TextEdit9)
            LookBaseTxt(TextEdit10)
            LookBaseTxt(TextEdit11)
            LookBaseTxt(txtNome2)

            LookBaseXtraEditors(SimpleButton1.LookAndFeel)
            LookBaseXtraEditors(XtraTabControl1.LookAndFeel)

            LookBaseXtraEditors(BarAndDockingController1.LookAndFeel)
            LookBaseXtraEditors(PanelControl1.LookAndFeel)
            LookBaseXtraEditors(PanelControl2.LookAndFeel)

            LookBaseXtraEditors(GridControl.LookAndFeel)
            ConfigurarGridDadosComerciaisClientes(GridView1)

            LookBaseXtraEditors(GridControl3.LookAndFeel)
            ConfiguraViewEmpresa(GridView31)
            ConfiguraViewDetalhe(GridView32)
            ConfiguraViewDetalhe2(GridView33)

            LookBaseXtraEditors(GridControl4.LookAndFeel)
            ConfiguraViewEmpresa(GridView41)
            ConfiguraViewDetalhe(GridView42)
            ConfiguraViewDetalhe2(GridView43)

            LookBaseXtraEditors(GridControl5.LookAndFeel)
            ConfiguraViewEmpresa(GridView51)
            ConfiguraViewDetalhe(GridView52)

            LookBaseXtraEditors(GridControl6.LookAndFeel)
            ConfiguraViewEmpresa(GridView61)
            ConfiguraViewDetalhe(GridView62)

            LookBaseXtraEditors(btAtualizarVendas.LookAndFeel)
            LookBaseXtraEditors(cbEmpresa.LookAndFeel)
            'cbEmpresa.Properties.ReadOnly = True

            dtp1.Value = CDate(Now.Year & "-" & "01" & "-" & "01")
            dtp2.Value = Now

            BarStaticItem1.Caption = EmpresaGeral & " - " & PlataformaFA.Contexto.Empresa.IDNome

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao iniciar o formulário.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Load Form.", True)
        End Try
    End Sub

    Private Sub txtMpCod_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtCliente.ButtonClick
        ChamaFamilia()
    End Sub

    Private Sub ChamaFamilia()
        Me.Cursor = Cursors.WaitCursor
        Dim r As String = PlataformaFA.Listas.GetF4SQL("Clientes", "SELECT Cliente, Nome FROM Clientes ", "Cliente")
        Dim n As Integer = 0
        If r <> "" Then
            ValidarCliente(r)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtCliente_Validated(sender As Object, e As System.EventArgs) Handles txtCliente.Validated
        Me.Cursor = Cursors.WaitCursor
        txtCliente.Text = txtCliente.Text.ToUpper
        ValidarCliente(txtCliente.Text)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ValidarCliente(ByVal Valor As String)
        Dim sSql As String = ""
        Try

            Dim ClienteExiste As Boolean
            Select Case EmpresaGeral
                Case "FASTIL"
                    ClienteExiste = MotorFA.Comercial.Clientes.Existe(Valor)
                Case "KLICK"
                    ClienteExiste = MotorKL.Comercial.Clientes.Existe(Valor)
                Case "JUALTEX"
                    ClienteExiste = MotorJU.Comercial.Clientes.Existe(Valor)
            End Select

            If ClienteExiste Then
                txtCliente.Text = Valor
                txtNome.Text = MotorFA.Comercial.Clientes.DaNome(Valor)

                CarregaDadosGerais(Valor)
                AtualizarDadosComerciais(Valor)

                CarregaVendasAnuais(Now.Year)

                AtualizarCarteira(DataSetEncCarteira1, Valor, GridView31, GridView32, GridView33)
                AtualizarFaturado(DataSetEncFaturadas1, Valor, GridView41, GridView42, GridView43)


                Dim DataInicial As String = dtp1.Value.Year & "-" & dtp1.Value.Month & "-" & dtp1.Value.Day
                Dim DataFinal As String = dtp2.Value.Year & "-" & dtp2.Value.Month & "-" & dtp2.Value.Day

                AtualizarExtrato(DataSetExtrato1, Valor, GridView51, GridView52, DataInicial, DataFinal)
                AtualizarDocsAberto(DataSetDocAberto1, Valor, GridView61, GridView62)

            Else
                txtCliente.Text = ""
                txtNome.Text = ""
            End If
        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar o cliente.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub CarregaDadosGerais(Cliente As String)
        Try
            Dim motor As New Interop.ErpBS800.ErpBS
            Select Case EmpresaGeral
                Case "FASTIL"
                    motor = MotorFA
                Case "KLICK"
                    motor = MotorKL
                Case "JUALTEX"
                    motor = MotorJU
            End Select

            If motor.Comercial.Clientes.Existe(Cliente) Then
                Dim t() As Object = {"Morada", "Morada2", "Localidade", "CodigoPostal", "LocalidadeCodigoPostal", _
                                     "NumContribuinte", "Telefone", "Telefone2", "Fax", "Vendedor"}
                Dim c As Interop.StdBE800.StdBECampos = motor.Comercial.Clientes.DaValorAtributos(Cliente, t)

                TextEdit1.Text = c.Item(1).Valor
                TextEdit2.Text = c.Item(2).Valor
                TextEdit3.Text = c.Item(3).Valor
                TextEdit4.Text = c.Item(4).Valor
                TextEdit5.Text = c.Item(5).Valor
                TextEdit6.Text = c.Item(6).Valor
                TextEdit7.Text = c.Item(7).Valor
                TextEdit8.Text = c.Item(8).Valor
                TextEdit9.Text = c.Item(9).Valor
                TextEdit10.Text = c.Item(10).Valor
                TextEdit11.Text = MotorFA.Comercial.Vendedores.DaValorAtributo(c.Item(10).Valor, "Nome")

                c = Nothing

            End If

            motor = Nothing

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais de cliente.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub CarregaVendasAnuais(iAno As Integer)
        Try
            Select Case EmpresaGeral
                Case "FASTIL"
                    cbEmpresa.SelectedIndex = 0
                Case "KLICK"
                    cbEmpresa.SelectedIndex = 1
                Case "JUALTEX"
                    cbEmpresa.SelectedIndex = 2
            End Select
            ValidaNome(cbEmpresa.SelectedIndex)


            'ANO A INICIAR
            iAno = iAno - 2

            Dim txtVnd() As DevExpress.XtraEditors.TextEdit = _
    {txtVnd101, txtVnd102, txtVnd103, txtVnd104, txtVnd105, txtVnd106, txtVnd107, txtVnd108, txtVnd109, txtVnd110, txtVnd111, txtVnd112, _
     txtVnd201, txtVnd202, txtVnd203, txtVnd204, txtVnd205, txtVnd206, txtVnd207, txtVnd208, txtVnd209, txtVnd210, txtVnd211, txtVnd212, _
     txtVnd301, txtVnd302, txtVnd303, txtVnd304, txtVnd305, txtVnd306, txtVnd307, txtVnd308, txtVnd309, txtVnd310, txtVnd311, txtVnd312}

            For i As Integer = 0 To txtVnd.Length - 1
                txtVnd(i).Text = CalcularValorMes(iAno, i + 1)
            Next

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais nas vendas anuais.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub ValidaNome(Index As Integer)
        Try
            Select Case Index
                Case 0
                    txtNome2.Text = PlataformaFA.Contexto.Empresa.IDNome
                Case 1
                    txtNome2.Text = PlataformaKL.Contexto.Empresa.IDNome
                Case 2
                    txtNome2.Text = PlataformaJU.Contexto.Empresa.IDNome
            End Select
        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais nas vendas anuais.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub AtualizarDadosComerciais(Cliente As String)

        Try
            'Create a connection object. 
            Dim StrConect As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
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
            GridControl.DataSource = CarregaTblDadosComerciais(Cliente, ArrayColunas, ArrayCaption, ArrayGetType, StrConect)
            GridView1.PopulateColumns()

            ' Alterar a aparencia do cabeçalho da grelha
            Dim i As Integer = 0
            For i = 0 To GridView1.Columns.Count - 1
                'aparencia no cabeçalho deve ser editada no formulário
                GridView1.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView1.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

                If i = 0 Then
                    GridView1.Columns(i).Width = 150

                    GridView1.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

                Else
                    GridView1.Columns(i).Width = 150

                    GridView1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns(i).DisplayFormat.FormatString = "N3"
                    GridView1.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                End If

                GridView1.Columns(i).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

            Next

            GridView1.Columns(0).OptionsColumn.FixedWidth = True

            GridView1.OptionsView.ColumnAutoWidth = False

            GridView1.OptionsView.ColumnAutoWidth = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub gridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        'If e.RowHandle <> gridView1.FocusedRowHandle AndAlso ((e.RowHandle Mod 2 = 0 AndAlso e.Column.VisibleIndex Mod 2 = 1) OrElse (e.Column.VisibleIndex Mod 2 = 0 AndAlso e.RowHandle Mod 2 = 1)) Then
        '    e.Appearance.ForeColor = SystemColors.Window
        '    e.Appearance.BackColor = SystemColors.WindowText
        'End If

        e.Appearance.Font = New Font("Verdana", 9, FontStyle.Regular)

        If GridView1.GetRowCellValue(e.RowHandle, GridView1.Columns(0)) = "Saldo à Data" Then
            e.Appearance.BackColor = Color.Beige
            e.Appearance.Font = New Font("Verdana", 9, FontStyle.Bold)
        ElseIf GridView1.GetRowCellValue(e.RowHandle, GridView1.Columns(0)) = "" Then
            e.Appearance.BackColor = Color.White
        End If

        Select Case e.Column.AbsoluteIndex
            Case 0
                e.Column.AppearanceCell.BackColor = Color.Lavender
                e.Column.AppearanceCell.Font = New Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Bold)
            Case 1

            Case 2
            Case 3
            Case 4
                'e.Appearance.Font = New Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Bold)
                e.Appearance.Font = New Font("Verdana", 9, FontStyle.Bold)
        End Select



    End Sub

    Private Sub gridView5_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView5.RowCellStyle

        'e.Appearance.Font = New Font("Verdana", 9, FontStyle.Regular)

        If GridView1.GetRowCellValue(e.RowHandle, GridView1.Columns(2)) = "S. Final" Then
            e.Appearance.BackColor = Color.Beige
            e.Appearance.Font = New Font("Verdana", 9, FontStyle.Bold)
        End If
    End Sub

    Private Function CarregaTblDadosComerciais(Cliente As String, ArrayColunas() As String, ArrayCaption() As String, ArrayGetType() As System.Type, _
                                                    StrConect As String) As DataTable
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


            Tbl.Rows.Add(RowLimiteCredito(Tbl, Cliente))
            Tbl.Rows.Add(RowUltimaCompra(Tbl, Cliente))

            'incluir linha de separação
            Row = Tbl.NewRow()
            Row.Item(0) = ""
            Tbl.Rows.Add(Row)
            '===========================

            Saldos(Tbl)

            'incluir linha de separação
            Row = Tbl.NewRow()
            Row.Item(0) = ""
            Tbl.Rows.Add(Row)
            '===========================

            Carteira(Tbl, Cliente)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Tbl

    End Function

    Public Function RowLimiteCredito(Tbl As DataTable, Cliente As String) As DataRow
        Dim Row As DataRow
        Try
            'TOTAIS
            Row = Tbl.NewRow()
            Row.Item(0) = "Limite Crédito"
            Row.Item(1) = FormatNumber(CDbl(MotorFA.Comercial.Clientes.DaValorAtributo(Cliente, "LimiteCredito")), 2)
            Row.Item(2) = FormatNumber(CDbl(MotorKL.Comercial.Clientes.DaValorAtributo(Cliente, "LimiteCredito")), 2)
            Row.Item(3) = FormatNumber(CDbl(MotorJU.Comercial.Clientes.DaValorAtributo(Cliente, "LimiteCredito")), 2)
            Row.Item(4) = ""

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function RowUltimaCompra(Tbl As DataTable, cliente As String) As DataRow
        Dim Row As DataRow
        Try
            'TOTAIS
            Row = Tbl.NewRow()
            Row.Item(0) = "Data Ultima Compra"
            Row.Item(1) = FormatDateTime(MotorFA.DSO.Consulta("SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'").Valor(0), DateFormat.ShortDate)
            Row.Item(2) = FormatDateTime(MotorKL.DSO.Consulta("SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'").Valor(0), DateFormat.ShortDate)
            Row.Item(3) = FormatDateTime(MotorJU.DSO.Consulta("SELECT ISNULL(MAX(DATA),'2001-01-01') FROM CabecDoc WHERE ENTIDADE = '" & cliente & "' AND TipoDoc = 'FA'").Valor(0), DateFormat.ShortDate)

            Return Row
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Sub Saldos(Tbl As DataTable)
        Dim i As Integer = 0, j As Integer
        Try


            Dim sSql As String = ""

            Dim Data As String = Now.Year & "-" & Now.Month & "-" & Now.Day

            sSql = sSql & " SELECT DATEDIFF(dd, '" & Data & "', DataVenc) * -1 AS DiasVenc, "
            sSql = sSql & " ValorPendente AS PENDENTE"
            sSql = sSql & " FROM pendentes "
            sSql = sSql & " WHERE Modulo = 'V' AND TipoEntidade = 'C' AND TipoConta = 'CCC'"
            sSql = sSql & " AND entidade = '00091'"
            sSql = sSql & " ORDER BY DiasVenc"


            Dim RowSal As DataRow = Tbl.NewRow()
            Dim RowVen As DataRow = Tbl.NewRow()
            Dim Row30d As DataRow = Tbl.NewRow()
            Dim Row60d As DataRow = Tbl.NewRow()
            Dim Row90d As DataRow = Tbl.NewRow()
            Dim RowM90 As DataRow = Tbl.NewRow()

            'FASTIL
            'MotorFA.Comercial.Clientes.DaValorAtributo("", "")

            RowSal(0) = "Saldo à Data"
            RowVen(0) = "A Vencer"
            Row30d(0) = "A 30 dias"
            Row60d(0) = "A 60 dias"
            Row90d(0) = "A 90 Dias"
            RowM90(0) = "+ 90 Dias"
            For j = 1 To 3
                Dim ListaCC As New Interop.StdBE800.StdBELista
                Select Case j
                    Case 1
                        ListaCC = MotorFA.Consulta(sSql)
                    Case 2
                        ListaCC = MotorKL.Consulta(sSql)
                    Case 3
                        ListaCC = MotorJU.Consulta(sSql)
                End Select
                RowSal(j) = 0
                RowVen(j) = 0
                Row30d(j) = 0
                Row60d(j) = 0
                Row90d(j) = 0
                RowM90(j) = 0
                For i = 1 To ListaCC.NumLinhas
                    Select Case CInt(ListaCC.Valor(0).ToString)
                        Case Is <= 0
                            RowVen(j) = FormatNumber(RowVen(j) + CDbl(ListaCC.Valor(1).ToString), 2)
                        Case 1 To 30
                            Row30d(j) = FormatNumber(Row30d(j) + CDbl(ListaCC.Valor(1).ToString), 2)
                        Case 31 To 60
                            Row60d(j) = FormatNumber(Row60d(j) + CDbl(ListaCC.Valor(1).ToString), 2)
                        Case 61 To 91
                            Row90d(j) = FormatNumber(Row90d(j) + CDbl(ListaCC.Valor(1).ToString), 2)
                        Case Is > 90
                            RowM90(j) = FormatNumber(RowM90(j) + CDbl(ListaCC.Valor(1).ToString), 2)
                    End Select
                    ListaCC.Seguinte()
                Next
                RowSal(j) = CDbl(RowVen(j)) + CDbl(Row30d(j)) + CDbl(Row60d(j)) + CDbl(Row90d(j)) + CDbl(RowM90(j))
            Next

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

    Public Sub Carteira(Tbl As DataTable, cliente As String)
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

            'FASTIL
            'MotorFA.Comercial.Clientes.DaValorAtributo("", "")

            RowCrt(0) = "Carteira"
            RowCrt(4) = 0
            For j = 1 To 3
                Dim ListaCC As New Interop.StdBE800.StdBELista
                Select Case j
                    Case 1
                        ListaCC = MotorFA.Consulta(sSql)
                    Case 2
                        ListaCC = MotorKL.Consulta(sSql)
                    Case 3
                        ListaCC = MotorJU.Consulta(sSql)
                End Select
                RowCrt(j) = 0
                For i = 1 To ListaCC.NumLinhas

                    RowCrt(j) = FormatNumber(CDbl(RowCrt(j)) + CDbl(ListaCC.Valor(7).ToString), 2)

                    ListaCC.Seguinte()
                Next
                RowCrt(4) = FormatNumber(CDbl(RowCrt(4)) + CDbl(RowCrt(j)), 2)
            Next

            Tbl.Rows.Add(RowCrt)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btAtualizar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btAtualizar.ItemClick

        Me.Cursor = Cursors.WaitCursor

        Dim ClienteExiste As Boolean
        Select Case EmpresaGeral
            Case "FASTIL"
                ClienteExiste = MotorFA.Comercial.Clientes.Existe(txtCliente.Text)
            Case "KLICK"
                ClienteExiste = MotorKL.Comercial.Clientes.Existe(txtCliente.Text)
            Case "JUALTEX"
                ClienteExiste = MotorJU.Comercial.Clientes.Existe(txtCliente.Text)
        End Select

        If ClienteExiste Then

            Select Case XtraTabControl1.SelectedTabPageIndex
                Case 0
                    CarregaDadosGerais(txtCliente.Text)

                    GridView1.ShowLoadingPanel()
                    AtualizarDadosComerciais(txtCliente.Text)
                    GridView1.HideLoadingPanel()

                Case 1
                Case 2
                    GridView31.ShowLoadingPanel()
                    AtualizarCarteira(DataSetEncCarteira1, txtCliente.Text, GridView31, GridView32, GridView33)
                    GridView31.HideLoadingPanel()
                Case 3
                    GridView41.ShowLoadingPanel()
                    AtualizarFaturado(DataSetEncFaturadas1, txtCliente.Text, GridView41, GridView42, GridView43)
                    GridView41.HideLoadingPanel()
                Case 4
                    Dim DataInicial As String = dtp1.Value.Year & "-" & dtp1.Value.Month & "-" & dtp1.Value.Day
                    Dim DataFinal As String = dtp2.Value.Year & "-" & dtp2.Value.Month & "-" & dtp2.Value.Day
                    GridView51.ShowLoadingPanel()
                    AtualizarExtrato(DataSetExtrato1, txtCliente.Text, GridView51, GridView52, DataInicial, DataFinal)
                    GridView51.HideLoadingPanel()
                Case 5
                    GridView61.ShowLoadingPanel()
                    AtualizarDocsAberto(DataSetDocAberto1, txtCliente.Text, GridView61, GridView62)
                    GridView61.HideLoadingPanel()
            End Select

        End If
        Me.Cursor = Cursors.Default

        'MsgBox(XtraTabControl1.SelectedTabPageIndex)

    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Me.Close()
    End Sub

    Private Sub PanelControl2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles PanelControl2.Paint

    End Sub

    Private Sub cbEmpresa_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbEmpresa.SelectedIndexChanged
        Try
            ValidaNome(sender.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btAtualizarVendas_Click(sender As System.Object, e As System.EventArgs) Handles btAtualizarVendas.Click
        Try
            Dim iAno As Integer = Now.Year
            CarregaVendasAnuais(iAno)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtVnd_ButtonClick(sender As System.Object, e As System.EventArgs) Handles _
        txtVnd101.ButtonClick, txtVnd102.ButtonClick, txtVnd103.ButtonClick, txtVnd104.ButtonClick, txtVnd105.ButtonClick, txtVnd106.ButtonClick, txtVnd107.ButtonClick, txtVnd108.ButtonClick, txtVnd109.ButtonClick, txtVnd110.ButtonClick, txtVnd111.ButtonClick, txtVnd112.ButtonClick, _
        txtVnd201.ButtonClick, txtVnd202.ButtonClick, txtVnd203.ButtonClick, txtVnd204.ButtonClick, txtVnd205.ButtonClick, txtVnd206.ButtonClick, txtVnd207.ButtonClick, txtVnd208.ButtonClick, txtVnd209.ButtonClick, txtVnd210.ButtonClick, txtVnd211.ButtonClick, txtVnd212.ButtonClick, _
        txtVnd301.ButtonClick, txtVnd302.ButtonClick, txtVnd303.ButtonClick, txtVnd304.ButtonClick, txtVnd305.ButtonClick, txtVnd306.ButtonClick, txtVnd307.ButtonClick, txtVnd308.ButtonClick, txtVnd309.ButtonClick, txtVnd310.ButtonClick, txtVnd311.ButtonClick, txtVnd312.ButtonClick
        Try
            If Not FormInicializado Then Exit Sub

            Dim iAno As Integer = Now.Year - 2
            Dim iFatorAno As Integer = CInt(Mid(sender.name, 7, 1) - 1)
            Dim iFatorMes As Integer = CInt(Mid(sender.name, 8, 2))
            Dim sSql As String
            iAno = iAno + iFatorAno

            sSql = ""

            sSql = sSql & " SELECT LinhasDoc.Artigo, LinhasDoc.Descricao,LinhasDoc.Quantidade, LinhasDoc.PrecUnit, LinhasDoc.PrecoLiquido FROM LinhasDoc "
            sSql = sSql & " INNER JOIN CabecDoc ON CabecDoc.id = LinhasDoc.IdCabecDoc"
            sSql = sSql & " WHERE CabecDoc.TipoDoc  IN ('FA', 'NC')"
            sSql = sSql & " AND YEAR(LinhasDoc.Data) = '" & iAno & "' AND MONTH(LinhasDoc.Data) = '" & iFatorMes & "'"

            Me.Cursor = Cursors.WaitCursor
            Dim r As String = PlataformaFA.Listas.GetF4SQL("Linhas de Documentos", sSql, "")
            Dim n As Integer = 0
            If r <> "" Then
                ValidarCliente(r)
            End If
            Me.Cursor = Cursors.Default


        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCliente_ButtonClick(sender As System.Object, e As System.EventArgs) Handles txtCliente.ButtonClick

    End Sub
End Class