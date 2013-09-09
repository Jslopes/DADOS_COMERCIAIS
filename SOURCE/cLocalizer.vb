Imports DevExpress.XtraGrid.Localization
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils.Localization.Internal

Public Class PortugueseGridLocalizer
    Inherits GridLocalizer
    Public Overrides ReadOnly Property Language() As String
        Get
            Return "Portuguese"
        End Get
    End Property

    Public Overrides Function GetLocalizedString(ByVal id As GridStringId) As String
        Dim ret As String = ""

        Select Case id
            ' ... 
            Case GridStringId.MenuColumnAutoFilterRowHide : Return "Ocultar linha de filtro"
            Case GridStringId.MenuColumnAutoFilterRowShow : Return "Mostrar linha de filtro"

            Case GridStringId.MenuColumnSortAscending : Return "Ordem ascendente"
            Case GridStringId.MenuColumnSortDescending : Return "Ordem descendente"
            Case GridStringId.MenuColumnGroup : Return "Agrupar por esta coluna"
            Case GridStringId.MenuColumnUnGroup : Return "Desagrupar"
            Case GridStringId.MenuColumnColumnCustomization : Return "Personalizar Colunas"
            Case GridStringId.MenuColumnBestFit : Return "Melhor Ajuste"
            Case GridStringId.MenuColumnFilter : Return "Pode agrupar"
            Case GridStringId.MenuColumnClearFilter : Return "limpar Filtro"
            Case GridStringId.MenuColumnBestFitAllColumns : Return "Melhor Ajuste (Todas as Colunas)"
            Case GridStringId.MenuColumnClearSorting : Return "Limpar Ordenação"
            Case GridStringId.MenuColumnRemoveColumn : Return "Remover esta coluna"
            Case GridStringId.MenuColumnShowColumn : Return "Mostrar Coluna"
            Case GridStringId.MenuColumnFilterEditor : Return "Editor de Filtros"

            Case GridStringId.MenuGroupPanelShow : Return "Mostrar painel de filtro"
            Case GridStringId.MenuGroupPanelHide : Return "Ocultar painel de filtro"
            Case GridStringId.MenuGroupPanelFullExpand : Return "Mostrar Coluna"
            Case GridStringId.MenuGroupPanelFullCollapse : Return "Mostrar Coluna"
            Case GridStringId.MenuGroupPanelClearGrouping : Return "Limpar agrupamentos"

            Case GridStringId.GridNewRowText : Return "Click para inserir novo registo"

            Case Else
                ret = DevExpress.XtraGrid.Localization.GridLocalizer.CreateDefaultLocalizer.GetLocalizedString(id)
        End Select
        Return ret

    End Function

End Class

Public Class PortugueseEditorsLocalizer
    Inherits Localizer
    Public Overrides ReadOnly Property Language() As String
        Get
            Return "Portuguese"
        End Get
    End Property

    Public Overrides Function GetLocalizedString(ByVal id As StringId) As String
        Select Case id
            ' ... 
            'Case StringId.NavigatorTextStringFormat : Return "Zeile {0} von {1}"
            'Case StringId.PictureEditMenuCut : Return "Ausschneiden"
            'Case StringId.PictureEditMenuCopy : Return "Kopieren"
            'Case StringId.PictureEditMenuPaste : Return "Einfugen"
            'Case StringId.PictureEditMenuDelete : Return "Loschen"
            'Case StringId.PictureEditMenuLoad : Return "Laden"
            'Case StringId.PictureEditMenuSave : Return "Speichern"
            'Case StringId.XtraMessageBoxYesButtonText : Return "Sim"
            'Case StringId.XtraMessageBoxNoButtonText : Return "Não"

            ' ... 
        End Select
        Return Localizer.CreateDefaultLocalizer.GetLocalizedString(id)
    End Function
End Class