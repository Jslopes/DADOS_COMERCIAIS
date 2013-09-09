<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListaDeDados
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListaDeDados))
        Me.GridControl = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BackstageViewButtonItem1 = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BackstageViewButtonItem2 = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.BackstageViewButtonItem3 = New DevExpress.XtraBars.Ribbon.BackstageViewButtonItem()
        Me.bar5 = New DevExpress.XtraBars.Bar()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar4 = New DevExpress.XtraBars.Bar()
        Me.xbtAtualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarLinkContainerItem1 = New DevExpress.XtraBars.BarLinkContainerItem()
        Me.BarSubItem3 = New DevExpress.XtraBars.BarSubItem()
        Me.xbtToExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtToRtf = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtToPdf = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtToHtml = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtToText = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem7 = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtVistas = New DevExpress.XtraBars.BarSubItem()
        Me.xbtPrevisualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtPrint = New DevExpress.XtraBars.BarSubItem()
        Me.xbtImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar7 = New DevExpress.XtraBars.Bar()
        Me.nReg = New DevExpress.XtraBars.BarStaticItem()
        Me.Bar6 = New DevExpress.XtraBars.Bar()
        Me.xbtCondicoes = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtFind = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtAgrupar = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtExpandir = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtFechar = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtInicio = New DevExpress.XtraBars.BarButtonItem()
        Me.xbtFim = New DevExpress.XtraBars.BarButtonItem()
        Me.BarAndDockingController1 = New DevExpress.XtraBars.BarAndDockingController(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarLargeButtonItem1 = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl
        '
        Me.GridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl.Location = New System.Drawing.Point(0, 62)
        Me.GridControl.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl.MainView = Me.GridView1
        Me.GridControl.Name = "GridControl"
        Me.GridControl.Size = New System.Drawing.Size(933, 410)
        Me.GridControl.TabIndex = 0
        Me.GridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.GroupPanel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.GridView1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.GridView1.FooterPanelHeight = 10
        Me.GridView1.GridControl = Me.GridControl
        Me.GridView1.Name = "GridView1"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "GridColumn3"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'BackstageViewButtonItem1
        '
        Me.BackstageViewButtonItem1.Caption = "BackstageViewButtonItem1"
        Me.BackstageViewButtonItem1.Name = "BackstageViewButtonItem1"
        '
        'BackstageViewButtonItem2
        '
        Me.BackstageViewButtonItem2.Caption = "BackstageViewButtonItem2"
        Me.BackstageViewButtonItem2.Name = "BackstageViewButtonItem2"
        '
        'BackstageViewButtonItem3
        '
        Me.BackstageViewButtonItem3.Caption = "BackstageViewButtonItem3"
        Me.BackstageViewButtonItem3.Name = "BackstageViewButtonItem3"
        '
        'bar5
        '
        Me.bar5.BarName = "StatusBar"
        Me.bar5.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.bar5.DockCol = 0
        Me.bar5.DockRow = 0
        Me.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.bar5.FloatLocation = New System.Drawing.Point(86, 499)
        Me.bar5.OptionsBar.AllowQuickCustomization = False
        Me.bar5.OptionsBar.DrawDragBorder = False
        Me.bar5.OptionsBar.DrawSizeGrip = True
        Me.bar5.OptionsBar.UseWholeRow = True
        Me.bar5.Text = "StatusBar"
        '
        'Bar1
        '
        Me.Bar1.BarName = "StatusBar"
        Me.Bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar1.FloatLocation = New System.Drawing.Point(86, 499)
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.DrawSizeGrip = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "StatusBar"
        '
        'Bar2
        '
        Me.Bar2.BarName = "StatusBar"
        Me.Bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar2.FloatLocation = New System.Drawing.Point(86, 499)
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.DrawSizeGrip = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "StatusBar"
        '
        'Bar3
        '
        Me.Bar3.BarName = "StatusBar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.FloatLocation = New System.Drawing.Point(86, 499)
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.DrawSizeGrip = True
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "StatusBar"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar4, Me.Bar7, Me.Bar6})
        Me.BarManager1.Controller = Me.BarAndDockingController1
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.xbtAtualizar, Me.BarButtonItem2, Me.BarButtonItem1, Me.xbtVistas, Me.xbtPrevisualizar, Me.xbtPrint, Me.xbtImprimir, Me.BarButtonItem5, Me.BarLinkContainerItem1, Me.BarButtonItem6, Me.BarButtonItem7, Me.xbtCondicoes, Me.xbtFind, Me.xbtAgrupar, Me.xbtExpandir, Me.xbtFechar, Me.xbtInicio, Me.xbtFim, Me.BarSubItem3, Me.xbtToExcel, Me.xbtToRtf, Me.xbtToPdf, Me.xbtToHtml, Me.xbtToText, Me.BarLargeButtonItem1, Me.nReg})
        Me.BarManager1.MaxItemId = 39
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1, Me.RepositoryItemTextEdit1})
        Me.BarManager1.StatusBar = Me.Bar7
        '
        'Bar4
        '
        Me.Bar4.BarName = "Tools"
        Me.Bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top
        Me.Bar4.DockCol = 0
        Me.Bar4.DockRow = 0
        Me.Bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar4.FloatLocation = New System.Drawing.Point(274, 162)
        Me.Bar4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.xbtAtualizar, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarLinkContainerItem1, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.xbtVistas, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.xbtPrint, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem5, True)})
        Me.Bar4.OptionsBar.AllowQuickCustomization = False
        Me.Bar4.OptionsBar.DisableClose = True
        Me.Bar4.OptionsBar.DisableCustomization = True
        Me.Bar4.Text = "Tools"
        '
        'xbtAtualizar
        '
        Me.xbtAtualizar.Caption = "Atualizar"
        Me.xbtAtualizar.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.Refresh16x16
        Me.xbtAtualizar.Id = 0
        Me.xbtAtualizar.ItemAppearance.Normal.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.xbtAtualizar.ItemAppearance.Normal.Options.UseFont = True
        Me.xbtAtualizar.Name = "xbtAtualizar"
        '
        'BarLinkContainerItem1
        '
        Me.BarLinkContainerItem1.Caption = "Outras"
        Me.BarLinkContainerItem1.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.gear
        Me.BarLinkContainerItem1.Id = 17
        Me.BarLinkContainerItem1.ItemAppearance.Normal.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.BarLinkContainerItem1.ItemAppearance.Normal.Options.UseFont = True
        Me.BarLinkContainerItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem3, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.BarButtonItem6, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.BarButtonItem7, True)})
        Me.BarLinkContainerItem1.Name = "BarLinkContainerItem1"
        '
        'BarSubItem3
        '
        Me.BarSubItem3.Caption = "Exportar"
        Me.BarSubItem3.Id = 27
        Me.BarSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.xbtToExcel), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtToRtf), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtToPdf), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtToHtml), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtToText)})
        Me.BarSubItem3.Name = "BarSubItem3"
        '
        'xbtToExcel
        '
        Me.xbtToExcel.Caption = "Excel"
        Me.xbtToExcel.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.docexcel
        Me.xbtToExcel.Id = 28
        Me.xbtToExcel.Name = "xbtToExcel"
        '
        'xbtToRtf
        '
        Me.xbtToRtf.Caption = "RTF"
        Me.xbtToRtf.Id = 29
        Me.xbtToRtf.Name = "xbtToRtf"
        '
        'xbtToPdf
        '
        Me.xbtToPdf.Caption = "PDF"
        Me.xbtToPdf.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.docpdf
        Me.xbtToPdf.Id = 30
        Me.xbtToPdf.Name = "xbtToPdf"
        '
        'xbtToHtml
        '
        Me.xbtToHtml.Caption = "HTML"
        Me.xbtToHtml.Id = 31
        Me.xbtToHtml.Name = "xbtToHtml"
        '
        'xbtToText
        '
        Me.xbtToText.Caption = "Texto(TXT)"
        Me.xbtToText.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.doctext
        Me.xbtToText.Id = 33
        Me.xbtToText.Name = "xbtToText"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Caption = "Mail Merge"
        Me.BarButtonItem6.Enabled = False
        Me.BarButtonItem6.Id = 18
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'BarButtonItem7
        '
        Me.BarButtonItem7.Caption = "Enviar Mensagem"
        Me.BarButtonItem7.Enabled = False
        Me.BarButtonItem7.Id = 19
        Me.BarButtonItem7.Name = "BarButtonItem7"
        '
        'xbtVistas
        '
        Me.xbtVistas.Caption = "Vistas"
        Me.xbtVistas.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.view
        Me.xbtVistas.Id = 12
        Me.xbtVistas.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.xbtPrevisualizar)})
        Me.xbtVistas.Name = "xbtVistas"
        '
        'xbtPrevisualizar
        '
        Me.xbtPrevisualizar.Caption = "Vista"
        Me.xbtPrevisualizar.Id = 13
        Me.xbtPrevisualizar.Name = "xbtPrevisualizar"
        '
        'xbtPrint
        '
        Me.xbtPrint.Caption = "Imprimir"
        Me.xbtPrint.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.print16
        Me.xbtPrint.Id = 14
        Me.xbtPrint.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.xbtImprimir)})
        Me.xbtPrint.Name = "xbtPrint"
        '
        'xbtImprimir
        '
        Me.xbtImprimir.Caption = "Imprimir"
        Me.xbtImprimir.Id = 15
        Me.xbtImprimir.Name = "xbtImprimir"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "Help"
        Me.BarButtonItem5.Enabled = False
        Me.BarButtonItem5.Id = 16
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'Bar7
        '
        Me.Bar7.BarName = "Status bar"
        Me.Bar7.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar7.DockCol = 0
        Me.Bar7.DockRow = 0
        Me.Bar7.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar7.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.nReg)})
        Me.Bar7.OptionsBar.AllowQuickCustomization = False
        Me.Bar7.OptionsBar.DrawDragBorder = False
        Me.Bar7.OptionsBar.DrawSizeGrip = True
        Me.Bar7.OptionsBar.UseWholeRow = True
        Me.Bar7.Text = "Status bar"
        '
        'nReg
        '
        Me.nReg.Caption = "Registos"
        Me.nReg.Id = 38
        Me.nReg.Name = "nReg"
        Me.nReg.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'Bar6
        '
        Me.Bar6.BarName = "Custom 4"
        Me.Bar6.DockCol = 0
        Me.Bar6.DockRow = 1
        Me.Bar6.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar6.FloatLocation = New System.Drawing.Point(244, 208)
        Me.Bar6.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.xbtCondicoes, True), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtFind, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.xbtAgrupar, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtExpandir, True), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtFechar), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtInicio), New DevExpress.XtraBars.LinkPersistInfo(Me.xbtFim)})
        Me.Bar6.OptionsBar.AllowQuickCustomization = False
        Me.Bar6.OptionsBar.DisableClose = True
        Me.Bar6.Text = "Custom 4"
        '
        'xbtCondicoes
        '
        Me.xbtCondicoes.Caption = "Condições"
        Me.xbtCondicoes.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.filterrow
        Me.xbtCondicoes.Id = 20
        Me.xbtCondicoes.Name = "xbtCondicoes"
        '
        'xbtFind
        '
        Me.xbtFind.Caption = "Procurar"
        Me.xbtFind.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.pesquisar16
        Me.xbtFind.Id = 21
        Me.xbtFind.Name = "xbtFind"
        '
        'xbtAgrupar
        '
        Me.xbtAgrupar.Caption = "Agrupar"
        Me.xbtAgrupar.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.group
        Me.xbtAgrupar.Id = 22
        Me.xbtAgrupar.ItemAppearance.Normal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xbtAgrupar.ItemAppearance.Normal.Options.UseFont = True
        Me.xbtAgrupar.Name = "xbtAgrupar"
        '
        'xbtExpandir
        '
        Me.xbtExpandir.Caption = "Expandir"
        Me.xbtExpandir.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.expand
        Me.xbtExpandir.Id = 23
        Me.xbtExpandir.Name = "xbtExpandir"
        '
        'xbtFechar
        '
        Me.xbtFechar.Caption = "Fechar"
        Me.xbtFechar.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.collapse
        Me.xbtFechar.Id = 24
        Me.xbtFechar.Name = "xbtFechar"
        '
        'xbtInicio
        '
        Me.xbtInicio.Caption = "Inicio"
        Me.xbtInicio.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.up
        Me.xbtInicio.Id = 25
        Me.xbtInicio.Name = "xbtInicio"
        '
        'xbtFim
        '
        Me.xbtFim.Caption = "Fim"
        Me.xbtFim.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.down
        Me.xbtFim.Id = 26
        Me.xbtFim.Name = "xbtFim"
        '
        'BarAndDockingController1
        '
        Me.BarAndDockingController1.LookAndFeel.SkinName = "Office 2013"
        Me.BarAndDockingController1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BarAndDockingController1.PropertiesBar.AllowLinkLighting = False
        '
        'barDockControlTop
        '
        Me.barDockControlTop.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.barDockControlTop.Appearance.Options.UseFont = True
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(933, 62)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 472)
        Me.barDockControlBottom.Size = New System.Drawing.Size(933, 24)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 62)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 410)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(933, 62)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 410)
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 8
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Outras"
        Me.BarButtonItem1.Glyph = Global.mcr_DadosComerciais.My.Resources.Resources.gear
        Me.BarButtonItem1.Id = 10
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarLargeButtonItem1
        '
        Me.BarLargeButtonItem1.Caption = "BarLargeButtonItem1"
        Me.BarLargeButtonItem1.Id = 34
        Me.BarLargeButtonItem1.Name = "BarLargeButtonItem1"
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'frmListaDeDados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 496)
        Me.Controls.Add(Me.GridControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmListaDeDados"
        Me.Text = "Lista de Dados"
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BackstageViewButtonItem1 As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BackstageViewButtonItem2 As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Friend WithEvents BackstageViewButtonItem3 As DevExpress.XtraBars.Ribbon.BackstageViewButtonItem
    Private WithEvents bar5 As DevExpress.XtraBars.Bar
    Private WithEvents Bar1 As DevExpress.XtraBars.Bar
    Private WithEvents Bar2 As DevExpress.XtraBars.Bar
    Private WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar4 As DevExpress.XtraBars.Bar
    Friend WithEvents Bar7 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents xbtAtualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtVistas As DevExpress.XtraBars.BarSubItem
    Friend WithEvents xbtPrevisualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtPrint As DevExpress.XtraBars.BarSubItem
    Friend WithEvents xbtImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarLinkContainerItem1 As DevExpress.XtraBars.BarLinkContainerItem
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar6 As DevExpress.XtraBars.Bar
    Friend WithEvents xbtCondicoes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtFind As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtAgrupar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtExpandir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtFechar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtInicio As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtFim As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubItem3 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents xbtToExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtToRtf As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtToPdf As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarAndDockingController1 As DevExpress.XtraBars.BarAndDockingController
    Friend WithEvents xbtToHtml As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xbtToText As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarLargeButtonItem1 As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents nReg As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
