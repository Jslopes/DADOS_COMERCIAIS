Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base

Module mDadosComerciaisView

    Public Sub ConfigurarGridDadosComerciaisClientes(GridView1 As GridView)
        Try
            ' OPÇÕES GERAIS DA GRELHA
            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsBehavior.AllowIncrementalSearch = False

            GridView1.OptionsHint.ShowColumnHeaderHints = False

            GridView1.OptionsView.ShowFooter = True
            GridView1.OptionsView.ShowAutoFilterRow = False
            GridView1.OptionsView.ShowGroupPanel = False
            GridView1.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            GridView1.OptionsView.ShowFooter = False
            GridView1.OptionsView.ShowGroupExpandCollapseButtons = False
            GridView1.OptionsView.ShowGroupedColumns = False

            GridView1.OptionsFilter.AllowFilterEditor = False
            GridView1.OptionsFilter.AllowFilterIncrementalSearch = False

            GridView1.OptionsFind.AllowFindPanel = False
            GridView1.OptionsFind.ShowFindButton = False

            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.OptionsSelection.MultiSelect = False

            GridView1.OptionsMenu.EnableGroupPanelMenu = False

            GridView1.OptionsLayout.Columns.StoreAllOptions = False

            GridView1.OptionsCustomization.AllowFilter = False
            GridView1.OptionsCustomization.AllowColumnMoving = False
            GridView1.OptionsCustomization.AllowColumnResizing = False

            GridView1.OptionsCustomization.AllowRowSizing = False

            GridView1.FocusRectStyle = DrawFocusRectStyle.None

            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = False


        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Definir grelha", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao desenhar a grelha", True)
        End Try
    End Sub

    Public Sub ConfiguraViewEmpresa(gv As GridView)

        Try
            gv.OptionsBehavior.Editable = False
            gv.OptionsBehavior.AllowIncrementalSearch = False

            gv.OptionsHint.ShowColumnHeaderHints = False

            gv.OptionsView.ShowAutoFilterRow = False
            gv.OptionsView.ShowGroupPanel = False
            gv.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            gv.OptionsView.ShowFooter = False
            gv.OptionsView.ShowGroupExpandCollapseButtons = False
            gv.OptionsView.ShowGroupedColumns = False

            gv.OptionsFilter.AllowFilterEditor = False
            gv.OptionsFilter.AllowFilterIncrementalSearch = False

            gv.OptionsFind.AllowFindPanel = False
            gv.OptionsFind.ShowFindButton = True

            gv.OptionsSelection.EnableAppearanceFocusedCell = False
            gv.OptionsSelection.MultiSelect = False

            gv.OptionsMenu.EnableGroupPanelMenu = False

            gv.OptionsLayout.Columns.StoreAllOptions = False

            gv.OptionsCustomization.AllowFilter = True
            gv.OptionsCustomization.AllowColumnMoving = False
            gv.OptionsCustomization.AllowColumnResizing = True

            gv.OptionsCustomization.AllowRowSizing = False

            gv.FocusRectStyle = DrawFocusRectStyle.RowFocus

            'NÃO MOSTRAS O SEPARADOR DA TABELA
            gv.OptionsDetail.ShowDetailTabs = False

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Definir grelha", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao desenhar a grelha", True)
        End Try
    End Sub


    Public Sub ConfiguraViewDetalhe(gv As GridView)

        Try
            gv.OptionsBehavior.Editable = False
            gv.OptionsBehavior.AllowIncrementalSearch = True

            gv.OptionsHint.ShowColumnHeaderHints = False

            gv.OptionsView.ShowAutoFilterRow = True
            gv.OptionsView.ShowGroupPanel = False
            gv.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            gv.OptionsView.ShowFooter = True
            gv.OptionsView.ShowGroupExpandCollapseButtons = False
            gv.OptionsView.ShowGroupedColumns = False

            gv.OptionsFilter.AllowFilterEditor = False
            gv.OptionsFilter.AllowFilterIncrementalSearch = False

            gv.OptionsFind.AllowFindPanel = False
            gv.OptionsFind.ShowFindButton = True

            gv.OptionsSelection.EnableAppearanceFocusedCell = False
            gv.OptionsSelection.MultiSelect = False

            gv.OptionsMenu.EnableGroupPanelMenu = False

            gv.OptionsLayout.Columns.StoreAllOptions = False

            gv.OptionsCustomization.AllowFilter = True
            gv.OptionsCustomization.AllowColumnMoving = False
            gv.OptionsCustomization.AllowColumnResizing = True

            gv.OptionsCustomization.AllowRowSizing = False

            gv.FocusRectStyle = DrawFocusRectStyle.RowFocus

            'NÃO MOSTRAS O SEPARADOR DA TABELA
            gv.OptionsDetail.ShowDetailTabs = False

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Definir grelha", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao desenhar a grelha", True)
        End Try
    End Sub
    Public Sub ConfiguraViewDetalhe2(gv As GridView)

        Try
            gv.OptionsBehavior.Editable = False
            gv.OptionsBehavior.AllowIncrementalSearch = True

            gv.OptionsHint.ShowColumnHeaderHints = False

            gv.OptionsView.ShowAutoFilterRow = False
            gv.OptionsView.ShowGroupPanel = False
            gv.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            gv.OptionsView.ShowFooter = False
            gv.OptionsView.ShowGroupExpandCollapseButtons = False
            gv.OptionsView.ShowGroupedColumns = False

            gv.OptionsFilter.AllowFilterEditor = False
            gv.OptionsFilter.AllowFilterIncrementalSearch = False

            gv.OptionsFind.AllowFindPanel = False
            gv.OptionsFind.ShowFindButton = True

            gv.OptionsSelection.EnableAppearanceFocusedCell = False
            gv.OptionsSelection.MultiSelect = False

            gv.OptionsMenu.EnableGroupPanelMenu = False

            gv.OptionsLayout.Columns.StoreAllOptions = False

            gv.OptionsCustomization.AllowFilter = True
            gv.OptionsCustomization.AllowColumnMoving = False
            gv.OptionsCustomization.AllowColumnResizing = True

            gv.OptionsCustomization.AllowRowSizing = False

            gv.FocusRectStyle = DrawFocusRectStyle.RowFocus

            'NÃO MOSTRAS O SEPARADOR DA TABELA
            gv.OptionsDetail.ShowDetailTabs = False

        Catch ex As Exception
            PlataformaFA.Dialogos.MostraMensagemEx(Interop.StdPlatBS800.TipoMsg.PRI_SimplesOk, "Definir grelha", Interop.StdPlatBS800.IconId.PRI_Critico, ex.Message, "Erro ao desenhar a grelha", True)
        End Try

    End Sub

End Module
