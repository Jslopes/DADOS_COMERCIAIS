Imports System.Data.OleDb
Imports System.Data
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Utils
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports System.Windows.Forms

Public Class frmListaDeDados

    Private StrConect As String
    Private Tabela As Structure_TDU
    Private sCampos As String = ""

    Friend Sub SetDados(ByVal Conect As String, StructureTabela As Structure_TDU)
        'Dim BaseDados As String = PlataformaFA.BaseDados.DaNomeBDdaEmpresa(CodEmpresa)
        'Dim ConectPri As String = PlataformaFA.BaseDados.DaConnectionString(BaseDados, "Default")
        'Debug.Print(ConectPri)
        StrConect = Conect
        Tabela = StructureTabela
        Me.Text = Tabela.Caption
    End Sub

    Private Sub frmListaPrecos_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        AtualizarDados()
    End Sub

    Private Sub frmListaPrecos_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Debug.Print(Me.Text)
        SaveMyForm(Me)
    End Sub

    Private Sub frmListaPrecos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.CenterToScreen()

        Try
            LoadMyForm(Me, True)
            ClearTextBox(Me)

            LookBaseXtraEditors(GridControl.LookAndFeel)

            LookBaseXtraEditors(BarAndDockingController1.LookAndFeel)

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Erro ao iniciar o formulário.", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Load Form.", True)
        End Try

        Try
            ' OPÇÕES GERAIS DA GRELHA
            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsView.ShowFooter = True
            GridView1.OptionsView.ShowAutoFilterRow = True
            GridView1.OptionsView.ShowGroupPanel = True

            GridView1.OptionsBehavior.AllowIncrementalSearch = True

            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridView1.OptionsSelection.MultiSelect = True

            nReg.Caption = GridView1.RowCount & " Registos"
        Catch ex As Exception
            'mcr_DadosComerciais.MostraErro(Plataforma, "Definir grelha ", "Erro ao desenhar a grelha.", ex.Message)
        End Try

    End Sub

    Private Sub gridView1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        DoRowDoubleClick(view, pt)
    End Sub

    Private Sub DoRowDoubleClick(ByVal view As GridView, ByVal pt As Point)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        If info.InRow OrElse info.InRowCell Then
            Dim colCaption As String
            If info.Column Is Nothing Then
                colCaption = "N/A"
            Else
                colCaption = info.Column.GetCaption()
            End If
            'MessageBox.Show(String.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption))
            'DevExpress.XtraEditors.XtraMessageBox.Show(view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(0)), colCaption, MessageBoxButtons.OK)
            'EditaRegisto(view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(0)))

            Dim iTbl As Integer
            If Tabela.SqlTabela = "TDU_PrecoFechos" Then
                'iTbl = TabelasGerais.PrecoFechos
            ElseIf Tabela.SqlTabela = "TDU_PrecoDivisiveis" Then
                'iTbl = TabelasGerais.PrecoDivisiveis
            ElseIf Tabela.SqlTabela = "TDU_PrecoCursores" Then
                'iTbl = TabelasGerais.PrecoCursores
            Else
                iTbl = 0
            End If

            'If iTbl <> 0 Then AbreTabelaPrecos(iTbl, Me.MdiParent, view.GetRowCellValue(view.GetSelectedRows(0), view.Columns(0)))

        End If
    End Sub

    Private Sub xbtEditar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) 'Handles xbtEditar.ItemClick
        ' esta função é para o botão
        If GridView1.GetSelectedRows.Count = 1 Then
            'DevExpress.XtraEditors.XtraMessageBox.Show(GridView1.GetRowCellValue(GridView1.GetSelectedRows(0), GridView1.Columns(0)), "Familia", MessageBoxButtons.OK)

            'EditaRegisto(GridView1.GetRowCellValue(GridView1.GetSelectedRows(0), GridView1.Columns(0)))
        End If
    End Sub


    Private Sub Abre()
        'Dim f As New frmTDU_Precos
        'Dim t As New Structure_TDU

        't.Caption = "Preços de Fechos"
        't.Titulo = "Preços de Fechos"
        't.SqlCampos = {"CDU_codigo", "CDU_Designacao", "CDU_DscAbrv", "CDU_Preco", "CDU_Familia"}
        't.tblCampos = {"Código", "Designação", "Descrição Abrv.", "Preço", "Familia"}
        't.SqlTabela = "TDU_PrecoFechos"
        'f.SetDados(t)

        'f.MdiParent = Me
        'f.Show()

        'f.BringToFront()

    End Sub

    Private Sub xbtAtualizar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtAtualizar.ItemClick
        'SplashScreenManager.ShowForm(Me, GetType(WaitForm1), True, True, False)
        'SplashScreenManager.Default.SetWaitFormDescription("A atualizar dados...")

        'For i As Integer = 1 To 100
        '    System.Threading.Thread.Sleep(15)

        '    '' Change progress to be displayed by SplashImagePainter
        '    'FastilSplashScreenManager.SplashImagePainter.Painter.ViewInfo.Counter = i
        '    ''Force SplashImagePainter to repaint information
        '    'SplashScreenManager.Default.Invalidate()
        'Next i


        AtualizarDados()

        'SplashScreenManager.CloseForm(False)
    End Sub

    Private Sub AtualizarDados()

        Try
            'GridView1.RefreshData()
        Catch ex As Exception

        End Try


        Try
            ' Create a connection object. 
            Dim Connection As New OleDbConnection(StrConect)

            ' Create a data adapter. 
            'Dim Adapter As New OleDbDataAdapter("SELECT CDU_Codigo as [Código], CDU_Designacao as [Designação], CDU_DscAbrv as [Designação Abrv.], CDU_Preco as [Preço], CDU_Familia as [Familia] FROM TDU_PrecoFechos", Connection)
            Dim Adapter As New OleDbDataAdapter("SELECT " & sCampos & " FROM " & Tabela.SqlTabela, Connection)

            ' Create and fill a dataset. 
            Dim SourceDataSet As New DataSet()
            Adapter.Fill(SourceDataSet)

            ' Specify the data source for the grid control. 
            GridControl.DataSource = SourceDataSet.Tables(0)

            GridView1.PopulateColumns()

            ' Alterar a aparencia do cabeçalho da grelha
            Try
                Dim i As Integer = 0
                For i = 0 To 4
                    'aparencia no cabeçalho deve ser editada no formulário
                    GridView1.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    GridView1.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center
                Next
            Catch ex As Exception

            End Try

            ' Esta opção estando ativa coloca as colunas como auto size....
            GridView1.OptionsView.ColumnAutoWidth = False

            'GridView1.Columns(0).Width = 75
            'GridView1.Columns(1).Width = 150
            'GridView1.Columns(2).Width = 150
            'GridView1.Columns(3).Width = 100
            'GridView1.Columns(4).Width = 75

            ' Atribui o tamanho automático para o melhor tamanho
            GridView1.BestFitColumns()

            'A coluna com o preços vai ficar maior
            GridView1.Columns(3).Width = GridView1.Columns(3).Width * 1.5

            'GridView1.Columns(3).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'GridView1.Columns(0).DisplayFormat.FormatString = New BaseFormatter
            'GridView1.Columns(3).DisplayFormat.FormatString = "c"
            'GridView1.Columns(3).DisplayFormat.FormatString = "###,###,##0.000"
            'GridView1.Columns(3).DisplayFormat.FormatType = FormatType.Numeric
            'GridView1.Columns(3).DisplayFormat.FormatString = "c3"
            GridView1.Columns(3).DisplayFormat.FormatType = FormatType.Numeric
            GridView1.Columns(3).DisplayFormat.FormatString = "N3"


            'nReg.Caption = GridView1.RowFilter & "/" & GridView1.RowCount & " Registos"
            nReg.Caption = GridView1.RowCount & " Registos"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick

    End Sub

    Private Sub xbtCondicoes_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtCondicoes.ItemClick
        GridView1.OptionsView.ShowAutoFilterRow = Not GridView1.OptionsView.ShowAutoFilterRow
    End Sub

    Private Sub xbtAgrupar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtAgrupar.ItemClick
        GridView1.OptionsView.ShowGroupPanel = Not GridView1.OptionsView.ShowGroupPanel
    End Sub

    Private Sub xbtFind_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtFind.ItemClick
        GridView1.ShowFindPanel()
        'GridView1.ShowFilterPopup(GridView1.Columns(0))
    End Sub

    Private Sub xbtPrevisualizar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtPrevisualizar.ItemClick
        GridControl.ShowPrintPreview()
    End Sub

    Private Sub xbtImprimir_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtImprimir.ItemClick
        GridControl.PrintDialog()
    End Sub

    Private Sub xbtToExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles _
        xbtToExcel.ItemClick, xbtToRtf.ItemClick, xbtToHtml.ItemClick, xbtToPdf.ItemClick, xbtToText.ItemClick
        Dim i As Integer
        Select Case e.Item.Caption
            Case "Excel"
                i = 2
            Case "RTF"
                i = 3
            Case "PDF"
                i = 4
            Case "HTML"
                i = 0
            Case "Text"
                i = 5
            Case Else
                i = 2
        End Select
        xTraGridExport(Me, i, GridView1)
    End Sub

    Private Sub xbtExpandir_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtExpandir.ItemClick
        GridView1.ExpandAllGroups()
    End Sub

    Private Sub xbtFechar_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtFechar.ItemClick
        GridView1.CollapseAllGroups()
    End Sub

    Private Sub xbtInicio_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtInicio.ItemClick
        'GridView1.SelectRow(0)
        'GridView1.ClearSelection()
        GridView1.MoveFirst()
    End Sub

    Private Sub xbtFim_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xbtFim.ItemClick
        'GridView1.SelectRow(GridView1.RowCount)
        GridView1.MoveLast()
    End Sub

    Private Sub gridView1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        If e.MenuType = GridMenuType.Column Then
            For i = 0 To e.Menu.Items.Count - 1
                Debug.Print(e.Menu.Items.Item(i).Caption)
                e.Menu.Items.Item(i).Caption = Traduzir(e.Menu.Items.Item(i).Caption)
                e.Menu.Items.Item(i).Visible = Visivel(e.Menu.Items.Item(i).Caption)
            Next
        ElseIf e.MenuType = GridMenuType.Group Then
            For i = 0 To e.Menu.Items.Count - 1
                e.Menu.Items.Item(i).Caption = Traduzir(e.Menu.Items.Item(i).Caption)
                e.Menu.Items.Item(i).Visible = Visivel(e.Menu.Items.Item(i).Caption)
            Next
        End If
    End Sub

    Private Function Visivel(Texto As String) As Boolean
        Select Case Texto
            Case "Column Chooser"
                Return False
            Case Else
                Return True
        End Select
    End Function

    Private Function Traduzir(Texto As String) As String

        Select Case Texto
            Case "Sort Ascending"
                Return "Ordenar Ascendente"
            Case "Sort Descending"
                Return "Ordenar Descendente"
            Case "Clear Sorting"
                Return "Limpar Ordenação"
            Case "Group By This Column"
                Return "Agrupar por esta coluna"
            Case "UnGroup"
                Return "Desagrupar"
            Case "Hide Group By Box"
                Return "Ocultar painel de agrupamentos"
            Case "Show Group By Box"
                Return "Mostar painel de agrupamentos"
            Case "Clear Grouping"
                Return "Limpar agrupamentos"

            Case "Remove This Column"
                Return "Remover esta coluna"
            Case "Best Fit"
                Return "Melhor Ajuste"
            Case "Best Fit (all columns)"
                Return "Melhor Ajuste (Todas as Colunas)"

            Case "Filter Editor..."
                Return "Editor de Filtros"
            Case "Show Find Panel"
                Return "Mostrar painel de filtro"
            Case "Hide Find Panel"
                Return "Ocultar painel de filtro"

            Case "Show Auto Filter Row"
                Return "Mostrar linha de filtro"
            Case "Hide Auto Filter Row"
                Return "Ocultar linha de filtro"

            Case "Full Expand"
                Return "Expandir tudo"
            Case "Full Collapse"
                Return "Fechar tudo"
            Case Else
                Return Texto
        End Select

    End Function

    Private Sub GridControl_Click(sender As System.Object, e As System.EventArgs) Handles GridControl.Click

    End Sub
End Class