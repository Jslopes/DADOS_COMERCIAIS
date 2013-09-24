Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data
Imports System.Drawing
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmDadosComerciais

    Dim FormInicializado As Boolean = False

    Dim StrConectFA As String = ""
    Dim StrConectKL As String = ""
    Dim StrConectJU As String = ""


    Friend Sub GetDados(sConFA As String, sConKL As String, sConJU As String)
        'Create a connection object. 
        StrConectFA = sConFA
        StrConectKL = sConKL
        StrConectJU = sConJU

    End Sub

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
                'txtVnd(i).Properties.Mask.ma = "n2"
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

            LookBaseXtraEditors(btContatos.LookAndFeel)
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
            'ConfiguraViewDetalhe2(GridView43)

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

            BarStaticItem1.Caption = EmpresaGeral & " - " & Plataforma.Contexto.Empresa.IDNome

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao iniciar o formulário.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Load Form.", True)
        End Try
    End Sub

    Private Sub txtMpCod_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtCliente.ButtonClick
        ChamaFamilia()
    End Sub

    Private Sub ChamaFamilia()
        Me.Cursor = Cursors.WaitCursor
        Dim r As String = Plataforma.Listas.GetF4SQL("Clientes", "SELECT Cliente, Nome FROM Clientes ", "Cliente")
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

            'Dim bClienteExiste As Boolean
            'Select Case EmpresaGeral
            '    Case "FASTIL"
            '        bClienteExiste = Motor.Comercial.Clientes.Existe(Valor)
            '    Case "KLICK"
            '        bClienteExiste = MotorKL.Comercial.Clientes.Existe(Valor)
            '    Case "JUALTEX"
            '        bClienteExiste = MotorJU.Comercial.Clientes.Existe(Valor)
            'End Select

            If ClienteExiste(EmpresaGeral, Valor, StrConectFA, StrConectKL, StrConectJU) Then
                txtCliente.Text = Valor
                'txtNome.Text = Motor.Comercial.Clientes.DaNome(Valor)

                CarregaDadosGerais(Valor)
                AtualizarDadosComerciais(EmpresaGeral, Valor, GridControl, GridView1, StrConectFA, StrConectKL, StrConectJU)

                Dim strConect As String = ""
                Select Case EmpresaGeral
                    Case "FASTIL"
                        cbEmpresa.SelectedIndex = 0
                        strConect = StrConectFA
                    Case "KLICK"
                        cbEmpresa.SelectedIndex = 1
                        strConect = StrConectKL
                    Case "JUALTEX"
                        cbEmpresa.SelectedIndex = 2
                        strConect = StrConectJU
                End Select
                CarregaVendasAnuais(Valor, Now.Year, strConect)

                AtualizarCarteira(DataSetEncCarteira1, Valor, GridView31, GridView32, GridView33, StrConectFA, StrConectKL, StrConectJU)
                AtualizarFaturado(DataSetEncFaturadas1, Valor, GridView41, GridView42, StrConectFA, StrConectKL, StrConectJU)


                Dim DataInicial As String = dtp1.Value.Year & "-" & dtp1.Value.Month & "-" & dtp1.Value.Day
                Dim DataFinal As String = dtp2.Value.Year & "-" & dtp2.Value.Month & "-" & dtp2.Value.Day

                AtualizarExtrato(DataSetExtrato1, Valor, GridView51, GridView52, DataInicial, DataFinal, StrConectFA, StrConectKL, StrConectJU)
                AtualizarDocsAberto(DataSetDocAberto1, Valor, GridView61, GridView62, StrConectFA, StrConectKL, StrConectJU)
            Else
                txtCliente.Text = ""
                txtNome.Text = ""
            End If
        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar o cliente.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub CarregaDadosGerais(Cliente As String)
        Try
            'Dim motor As New Interop.ErpBS800.ErpBS
            'Select Case EmpresaGeral
            '    Case "FASTIL"
            '        motor = Motor
            '    Case "KLICK"
            '        motor = MotorKL
            '    Case "JUALTEX"
            '        motor = MotorJU
            'End Select

            'If motor.Comercial.Clientes.Existe(Cliente) Then
            '    Dim t() As Object = {"Morada", "Morada2", "Localidade", "CodigoPostal", "LocalidadeCodigoPostal", _
            '                         "NumContribuinte", "Telefone", "Telefone2", "Fax", "Vendedor"}
            '    Dim c As Interop.StdBE800.StdBECampos = motor.Comercial.Clientes.DaValorAtributos(Cliente, t)

            '    TextEdit1.Text = c.Item(1).Valor
            '    TextEdit2.Text = c.Item(2).Valor
            '    TextEdit3.Text = c.Item(3).Valor
            '    TextEdit4.Text = c.Item(4).Valor
            '    TextEdit5.Text = c.Item(5).Valor
            '    TextEdit6.Text = c.Item(6).Valor
            '    TextEdit7.Text = c.Item(7).Valor
            '    TextEdit8.Text = c.Item(8).Valor
            '    TextEdit9.Text = c.Item(9).Valor
            '    TextEdit10.Text = c.Item(10).Valor
            '    TextEdit11.Text = Motor.Comercial.Vendedores.DaValorAtributo(c.Item(10).Valor, "Nome")

            '    c = Nothing

            'End If

            'motor = Nothing


            Dim StrConect As String = ""
            Select Case EmpresaGeral
                Case "FASTIL"
                    StrConect = StrConectFA
                Case "KLICK"
                    StrConect = StrConectKL
                Case "JUALTEX"
                    StrConect = StrConectJU
            End Select


            Dim sSql As String = ""
            sSql = sSql & " SELECT Clientes.cliente, ISNULL(Fac_mor,'') AS Fac_mor, ISNULL(Fac_mor2,'') AS Fac_mor2, ISNULL(Fac_local,'') AS Fac_local, "
            sSql = sSql & " ISNULL(Fac_Cp,'') AS Fac_Cp, ISNULL(Fac_Cploc,'') AS Fac_Cploc, ISNULL(NumContrib,'') AS NumContrib, "
            sSql = sSql & " ISNULL(Fac_Tel,'') AS Fac_Tel, ISNULL(Telefone2,'') AS Telefone2, ISNULL(Fac_Fax,'') AS Fac_Fax, "
            sSql = sSql & " ISNULL(Clientes.Vendedor,'') AS Vendedor,  ISNULL(Vendedores.Nome,'') AS NomeVendedor, Clientes.Nome"
            sSql = sSql & " FROM Clientes LEFT JOIN Vendedores ON Vendedores.Vendedor = Clientes.Vendedor"
            sSql = sSql & " WHERE Clientes.cliente = '" & Cliente & "'"


            Using sqlCon As New OleDb.OleDbConnection(StrConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read

                    txtNome.Text = reader.Item(12).ToString

                    TextEdit1.Text = reader.Item(1).ToString
                    TextEdit2.Text = reader.Item(2).ToString
                    TextEdit3.Text = reader.Item(3).ToString
                    TextEdit4.Text = reader.Item(4).ToString
                    TextEdit5.Text = reader.Item(5).ToString
                    TextEdit6.Text = reader.Item(6).ToString
                    TextEdit7.Text = reader.Item(7).ToString
                    TextEdit8.Text = reader.Item(8).ToString
                    TextEdit9.Text = reader.Item(9).ToString
                    TextEdit10.Text = reader.Item(10).ToString
                    TextEdit11.Text = reader.Item(11).ToString

                    Exit Do

                Loop
            End Using

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais de cliente.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub CarregaVendasAnuais(cliente As String, iAno As Integer, strConect As String)
        Try

            ValidaNome(cbEmpresa.SelectedIndex)

            GroupBox4.Text = "Vendas de " & iAno - 0
            GroupBox3.Text = "Vendas de " & iAno - 1
            GroupBox2.Text = "Vendas de " & iAno - 2

            'ANO A INICIAR
            iAno = iAno - 2

            Dim txtVnd() As DevExpress.XtraEditors.TextEdit = _
    {txtVnd101, txtVnd102, txtVnd103, txtVnd104, txtVnd105, txtVnd106, txtVnd107, txtVnd108, txtVnd109, txtVnd110, txtVnd111, txtVnd112, _
     txtVnd201, txtVnd202, txtVnd203, txtVnd204, txtVnd205, txtVnd206, txtVnd207, txtVnd208, txtVnd209, txtVnd210, txtVnd211, txtVnd212, _
     txtVnd301, txtVnd302, txtVnd303, txtVnd304, txtVnd305, txtVnd306, txtVnd307, txtVnd308, txtVnd309, txtVnd310, txtVnd311, txtVnd312}

            For i As Integer = 0 To txtVnd.Length - 1
                txtVnd(i).Text = CalcularValorMes(cliente, iAno, i + 1, strConect)
            Next

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais nas vendas anuais.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub ValidaNome(Index As Integer)
        Try
            Dim strConect As String = ""
            Dim sSql As String = ""
            Select Case Index
                Case 0
                    'txtNome2.Text = Plataforma.Contexto.Empresa.IDNome
                    strConect = StrConectFA.Replace("PRIFASTIL", "PRIEMPRE")
                    sSql = " SELECT IdNome FROM Empresas WHERE Codigo = 'FASTIL'"
                Case 1
                    'txtNome2.Text = PlataformaKL.Contexto.Empresa.IDNome
                    strConect = StrConectKL.Replace("PRIKLICK", "PRIEMPRE")
                    sSql = " SELECT IdNome FROM Empresas WHERE Codigo = 'KLICK'"
                Case 2
                    'txtNome2.Text = PlataformaJU.Contexto.Empresa.IDNome
                    strConect = StrConectJU.Replace("PRIJUALTEX", "PRIEMPRE")
                    sSql = " SELECT IdNome FROM Empresas WHERE Codigo = 'JUALTEX'"
            End Select
            Using sqlCon As New OleDb.OleDbConnection(strConect)
                Dim sqlCmd As New OleDb.OleDbCommand(sSql, sqlCon)
                sqlCon.Open()
                Dim reader As OleDb.OleDbDataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
                Do While reader.Read
                    txtNome2.Text = reader.Item(0).ToString
                    Exit Do
                Loop
            End Using

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais nas vendas anuais.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
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

    Private Sub GridView52_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView52.RowStyle
        Dim View As GridView = sender
        If (e.RowHandle >= 0) Then
            Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns(2))
            If category = "S. Final" Or category = "S. Inicial" Then
                e.Appearance.BackColor = Color.Beige
                e.Appearance.BackColor2 = Color.Beige
                e.Appearance.Font = New Font("Verdana", 9, FontStyle.Bold)

            End If
        End If
    End Sub

    Private Sub btAtualizar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btAtualizar.ItemClick

        Try
            Me.Cursor = Cursors.WaitCursor

            'Dim ClienteExiste As Boolean
            'Select Case EmpresaGeral
            '    Case "FASTIL"
            '        ClienteExiste = Motor.Comercial.Clientes.Existe(txtCliente.Text)
            '    Case "KLICK"
            '        ClienteExiste = MotorKL.Comercial.Clientes.Existe(txtCliente.Text)
            '    Case "JUALTEX"
            '        ClienteExiste = MotorJU.Comercial.Clientes.Existe(txtCliente.Text)
            'End Select

            If ClienteExiste(EmpresaGeral, txtCliente.Text, StrConectFA, StrConectKL, StrConectJU) Then

                Select Case XtraTabControl1.SelectedTabPageIndex
                    Case 0
                        CarregaDadosGerais(txtCliente.Text)

                        GridView1.ShowLoadingPanel()
                        AtualizarDadosComerciais(EmpresaGeral, txtCliente.Text, GridControl, GridView1, StrConectFA, StrConectKL, StrConectJU)
                        GridView1.HideLoadingPanel()
                    Case 1
                        Dim strConect As String = ""
                        Select Case cbEmpresa.SelectedIndex
                            Case 0
                                strConect = StrConectFA
                            Case 1
                                strConect = StrConectKL
                            Case 2
                                strConect = StrConectJU
                        End Select
                        CarregaVendasAnuais(txtCliente.Text, Now.Year, strConect)

                    Case 2
                        GridView31.ShowLoadingPanel()
                        AtualizarCarteira(DataSetEncCarteira1, txtCliente.Text, GridView31, GridView32, GridView33, StrConectFA, StrConectKL, StrConectJU)
                        GridView31.HideLoadingPanel()
                    Case 3
                        GridView41.ShowLoadingPanel()
                        AtualizarFaturado(DataSetEncFaturadas1, txtCliente.Text, GridView41, GridView42, StrConectFA, StrConectKL, StrConectJU)
                        GridView41.HideLoadingPanel()
                    Case 4
                        Dim DataInicial As String = dtp1.Value.Year & "-" & dtp1.Value.Month & "-" & dtp1.Value.Day
                        Dim DataFinal As String = dtp2.Value.Year & "-" & dtp2.Value.Month & "-" & dtp2.Value.Day
                        GridView51.ShowLoadingPanel()
                        AtualizarExtrato(DataSetExtrato1, txtCliente.Text, GridView51, GridView52, DataInicial, DataFinal, StrConectFA, StrConectKL, StrConectJU)
                        GridView51.HideLoadingPanel()

                    Case 5
                        GridView61.ShowLoadingPanel()
                        AtualizarDocsAberto(DataSetDocAberto1, txtCliente.Text, GridView61, GridView62, StrConectFA, StrConectKL, StrConectJU)
                        GridView61.HideLoadingPanel()
                End Select

            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao atualizar os dados.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Me.Close()
    End Sub

    Private Sub cbEmpresa_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbEmpresa.SelectedIndexChanged
        Try
            ValidaNome(sender.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btAtualizarVendas_Click(sender As System.Object, e As System.EventArgs) Handles btAtualizarVendas.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim iAno As Integer = Now.Year
            Dim strConect As String = ""
            Select Case cbEmpresa.SelectedIndex
                Case 0
                    strConect = StrConectFA
                Case 1
                    strConect = StrConectKL
                Case 2
                    strConect = StrConectJU
            End Select
            CarregaVendasAnuais(txtCliente.Text, iAno, strConect)

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao validar os dados gerais nas vendas anuais.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
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

            sSql = sSql & " SELECT LinhasDoc.Artigo, LinhasDoc.Descricao as [Descrição], LinhasDoc.Quantidade, LinhasDoc.PrecUnit as [Preço Unit.], LinhasDoc.PrecoLiquido as [Preço Liq.] FROM LinhasDoc "
            sSql = sSql & " INNER JOIN CabecDoc ON CabecDoc.id = LinhasDoc.IdCabecDoc"
            sSql = sSql & " WHERE CabecDoc.TipoDoc  IN ('FA', 'NC') "
            sSql = sSql & " AND  linhasdoc.TipoLinha IN ('10', '11') "
            sSql = sSql & " AND YEAR(LinhasDoc.Data) = '" & iAno & "' AND MONTH(LinhasDoc.Data) = '" & iFatorMes & "'"

            Me.Cursor = Cursors.WaitCursor


            Dim f As New frmListaDeDados
            Dim s As String = ""
            f.SetDados("Lista de artigos", sSql, GetConectEmpresa, False, 5, "2,N2;3,N3;4,N3")
            f.ShowDialog()


            'Dim r As String = Plataforma.Listas.GetF4SQL("Linhas de Documentos", sSql, "")
            'Dim n As Integer = 0
            'If r <> "" Then
            '    ValidarCliente(r)
            'End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro na leitura dos dados das vendas.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try
    End Sub

    Private Sub btContatos_Click(sender As System.Object, e As System.EventArgs) Handles btContatos.Click

        Try
            Dim f As New frmListaDeDados
            Dim s As String = ""
            s = s & " SELECT Contactos.Contacto, LinhasContactoEntidades.TipoContacto, Contactos.PrimeiroNome + ' ' +  Contactos.UltimoNome AS Nome,  "
            s = s & " LinhasContactoEntidades.Telefone, LinhasContactoEntidades.Telemovel, LinhasContactoEntidades.Email, LinhasContactoEntidades.Cargo "
            s = s & " FROM LinhasContactoEntidades INNER JOIN Contactos on Contactos.Id = LinhasContactoEntidades.IDContacto "
            s = s & " WHERE TipoEntidade = 'C' AND Entidade = '" & txtCliente.Text & "' "
            f.SetDados("Lista de Contatos", s, GetConectEmpresa, False)
            f.ShowDialog()
        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao abrir os contatos de cliente.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try

    End Sub

    Public Function GetConectEmpresa(Optional Empresa As String = "") As String
        Try
            If Empresa.Trim.Length = 0 Then Empresa = EmpresaGeral
            Dim StrConect As String = ""
            Select Case Empresa
                Case "FASTIL"
                    Return StrConectFA
                Case "KLICK"
                    Return StrConectKL
                Case "JUALTEX"
                    Return StrConectJU
                Case Else
                    Return ""
            End Select
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub gridView42_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles GridView42.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        DoRowDoubleClick(view, pt)
    End Sub

    Private Sub DoRowDoubleClick(ByVal view As GridView, ByVal pt As Point)
        Try
            Dim info As GridHitInfo = view.CalcHitInfo(pt)
            If info.InRow OrElse info.InRowCell Then
                Dim colCaption As String
                If info.Column Is Nothing Then
                    colCaption = "N/A"
                Else
                    colCaption = info.Column.GetCaption()

                    'MsgBox(view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(0)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(1)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(2)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(3)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(4)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(5)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(6)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(7)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(8)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(9)) & vbCrLf & _
                    '       view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(10)))

                    Dim sSql As String = ""
                    sSql = sSql & " SELECT CabECL.TipoDoc, CabECL.Serie, CabECL.NumDoc, CabECL.Data AS [Data Pedido], "
                    sSql = sSql & " LinhasDoc.Artigo, LinhasDoc.Descricao AS [Descrição], LinhasDoc.Quantidade , LinhasDoc.PrecUnit AS [Preço Unit.], LinhasDoc.PrecoLiquido AS [Preço Liq.]"
                    sSql = sSql & "         FROM CabecDoc "
                    sSql = sSql & " INNER JOIN CabecDocStatus On CabecDocStatus.IdCabecDoc = CabecDoc.Id"
                    sSql = sSql & " INNER JOIN LinhasDoc ON LinhasDoc.IdCabecDoc = CabecDoc.Id"
                    sSql = sSql & " LEFT JOIN LinhasDocTrans ON LinhasDocTrans.IdLinhasDoc = LinhasDoc.Id"
                    sSql = sSql & " LEFT JOIN LinhasDoc AS LinECL ON LinECL.id =  LinhasDocTrans.IdLinhasDocOrigem"
                    sSql = sSql & " INNER JOIN CabecDoc AS CabECL ON CabECL.Id = LinECL.IdCabecDoc "
                    sSql = sSql & " WHERE CabecDoc.TipoDoc = '" & view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(1)) & "' AND CabecDocStatus.Anulado = 0 AND CabecDoc.Entidade = '" & txtCliente.Text & "'"
                    sSql = sSql & "     AND CabecDoc.Serie = '" & view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(2)) & "' AND CabecDoc.NumDoc = '" & view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(3)) & "' "
                    sSql = sSql & "     AND CabECL.TipoDoc = '" & view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(6)) & "' AND CabECL.Serie = '" & view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(7)) & "' AND CabECL.NumDoc = '" & view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(8)) & "' "
                    sSql = sSql & " Order by CabecDoc.Data DESC, CabecDoc.Serie, CabecDoc.Numdoc"


                    'Me.Cursor = Cursors.WaitCursor

                    Dim f As New frmListaDeDados
                    Dim s As String = ""
                    f.SetDados("Lista de artigos", sSql, _
                               GetConectEmpresa(view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(0))), _
                               False, 9, "6,N2;7,N3;8,N3")
                    f.ShowDialog()

                End If

            End If

        Catch ex As Exception
            Plataforma.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao abrir os contatos de cliente.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "", True)
        End Try

    End Sub

End Class