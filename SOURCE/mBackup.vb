Module mBackup



    'Private Sub AtualizarDadosOld()

    '    Try
    '        'Create a connection object. 
    '        Dim StrConect As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
    '        Dim Connection As New OleDbConnection(StrConect)

    '        Dim sSql As String = ""
    '        sSql = sSql & "SELECT ~CabecDocStatus.DocImp as [Imprimir], Id, Serie, NumDoc, Entidade, Nome, CabecDocStatus.Estado, CabecDocStatus.Anulado, CabecDocStatus.Fechado, CabecDocStatus.DocImp    "
    '        sSql = sSql & " FROM CabecDoc "
    '        sSql = sSql & " INNER JOIN CabecDocStatus On CabecDocStatus.IdCabecDoc = CabecDoc.Id"
    '        sSql = sSql & " WHERE TipoDoc = 'ECL'"
    '        Select Case RadioGroup1.SelectedIndex
    '            Case 1
    '                sSql = sSql & " AND CabecDocStatus.DocImp = 0"
    '            Case 2
    '                sSql = sSql & " AND CabecDocStatus.DocImp = 1"
    '            Case Else
    '                sSql = sSql & " "
    '        End Select
    '        If CheckEdit4.Checked = True Then sSql = sSql & " AND CabecDocStatus.Estado = 'P'"


    '        Dim AdapterParent As New OleDbDataAdapter(sSql, Connection)

    '        sSql = ""
    '        sSql = sSql & "SELECT ~CabecDocStatus.DocImp as [Imprimir], linhasdoc.IdCabecdoc, NumLinha, artigo, descricao, quantidade "
    '        sSql = sSql & " FROM linhasdoc inner join CabecDoc on cabecdoc.id = linhasdoc.idcabecdoc"
    '        sSql = sSql & " INNER JOIN CabecDocStatus On CabecDocStatus.IdCabecDoc = CabecDoc.Id"
    '        sSql = sSql & " WHERE TipoDoc = 'ECL'"
    '        Select Case RadioGroup1.SelectedIndex
    '            Case 1
    '                sSql = sSql & " AND CabecDocStatus.DocImp = 0"
    '            Case 2
    '                sSql = sSql & " AND CabecDocStatus.DocImp = 1"
    '            Case Else
    '                sSql = sSql & " "
    '        End Select
    '        If CheckEdit4.Checked = True Then sSql = sSql & " AND CabecDocStatus.Estado = 'P'"
    '        Dim AdapterChildren As New OleDbDataAdapter(sSql, Connection)

    '        ' Create and fill a dataset. 
    '        Dim SourceDataSet As New DataSet()
    '        AdapterParent.Fill(SourceDataSet, "CabecDoc")
    '        AdapterChildren.Fill(SourceDataSet, "LinhasDoc")

    '        SourceDataSet.Relations.Add("Linhas de Produção", SourceDataSet.Tables(0).Columns("ID"), SourceDataSet.Tables(1).Columns("IdCabecdoc"))

    '        ' Specify the data source for the grid control. 
    '        GridControl.DataSource = SourceDataSet.Tables(0)

    '        GridView1.PopulateColumns()

    '        GridView1.Columns(1).Visible = False

    '        ' Alterar a aparencia do cabeçalho da grelha
    '        Dim i As Integer = 0
    '        For i = 0 To GridView1.Columns.Count - 1
    '            'aparencia no cabeçalho deve ser editada no formulário
    '            GridView1.Columns(i).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '            GridView1.Columns(i).AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.HorzAlignment.Center

    '            If i = 0 Then
    '                GridView1.Columns(i).OptionsColumn.AllowEdit = True
    '            Else
    '                GridView1.Columns(i).OptionsColumn.AllowEdit = False
    '            End If

    '            Select Case i
    '                Case 0, 1
    '                Case 2, 3
    '                    GridView1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                    GridView1.Columns(i).DisplayFormat.FormatString = "####0"
    '                    GridView1.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    '                Case 4, 5
    '                Case 6, 7, 8
    '            End Select

    '            'If i = 0 Then
    '            '    'GridView1.(i).Width = 30
    '            '    'ElseColumns()
    '            '    GridView1.Columns(i).Width = GridControl.Width / 16 '70 'GridView1.Columns(i).Width * 1.5
    '            'End If

    '            'GridView1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '            'If i = (GridView1.Columns.Count - 1) Then
    '            '    GridView1.Columns(i).DisplayFormat.FormatString = "N2"
    '            'Else
    '            '    GridView1.Columns(i).DisplayFormat.FormatString = "N0"
    '            'End If
    '            'GridView1.Columns(i).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far


    '            'GridView1.Columns(i).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

    '        Next

    '        'GridView1.Columns(0).OptionsColumn.FixedWidth = True

    '        GridView1.OptionsView.ColumnAutoWidth = False

    '        'ConditionsAdjustment()

    '        GridView1.BestFitColumns()
    '        '======================================================================================================

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

    '    Dim i As Integer
    '    'DataRow[] childRows =row.GetChildRows(row.Table.ChildRelations[0]);
    '    Dim view As GridView = GridView1
    '    For i = 0 To view.RowCount - 1
    '        MessageBox.Show(view.GetRowCellValue(i, view.Columns(0)))

    '        Dim Row As DataRow
    '        Row = GridView1.GetDataRow(i)

    '        Dim ChildRows() As DataRow = Row.GetChildRows(Row.Table.ChildRelations("CabecDoc_LinhasDoc"))



    '        Dim dRelationIndex As Integer = GridView1.GetRelationIndex(i, "CabecDoc_LinhasDoc") 'GetRelationIndex
    '        Dim dView As GridView = TryCast(GridView1.GetDetailView(i, 0), GridView)
    '        Dim aCollapsed As Boolean = dView Is Nothing
    '        If dView Is Nothing Then
    '            GridView1.ExpandMasterRow(i)
    '            dView = TryCast(GridView1.GetDetailView(i, dRelationIndex), GridView)
    '        End If
    '        If dView IsNot Nothing Then

    '            Dim sDados As String = ""

    '            For j As Integer = 0 To dView.DataRowCount - 1
    '                'dView.SetRowCellValue(i, dView.Columns("check"), state)
    '                'MessageBox.Show(dView.GetRowCellValue(j, dView.Columns(0)) & "  " & dView.GetRowCellValue(j, dView.Columns(4)))

    '                sDados = sDados & vbCrLf & dView.GetRowCellValue(j, dView.Columns(0)) & " - " & dView.GetRowCellValue(j, dView.Columns(4))

    '            Next j

    '            MessageBox.Show(sDados)

    '        Else
    '            MessageBox.Show("Ups")
    '        End If
    '        GridView1.CollapseMasterRow(i)



    '        'Dim view2 As GridView
    '        ''.MasterRowExpanded()
    '        'GridView1.ExpandMasterRow(i)
    '        'view2 = view.GetDetailView(i, 0)


    '        'For j As Integer = 0 To view2.RowCount - 1
    '        '    MessageBox.Show(view2.GetRowCellValue(i, view2.Columns(0)) & "  " & view2.GetRowCellValue(i, view2.Columns(4)))
    '        'Next

    '        'For j = 0 To ChildRows.Length
    '        '    ChildRows.Clone()
    '        'Next


    '        'MsgBox(ChildRows.Count)

    '        'If view.IsGroupRow(i) Then
    '        '    Dim view2 As GridView = GridView1.GetDetailView(i, 0)

    '        '    MsgBox("Tem " & view2.RowCount)

    '        'End If

    '    Next

    '    'DataRow row gridView1.GetDataRow(rowHandle);
    '    '    DataRow[] childRows =row.GetChildRows(row.Table.ChildRelations[0]);

    '    'foreach (int row in gridView1.GetSelectedRows())
    '    '    {
    '    '        if (!gridView1.IsGroupRow(row))
    '    '        {

    '    '            GridView detailView = gridView1.GetDetailView(row,0) as GridView;
    '    '            for (int i = 0; i < detailView.DataRowCount; i++)
    '    '            {
    '    '                DataRow row = detailView.GetDataRow(i);
    '    '            }

    '    '        }
    '    '    }

    'End Sub

End Module

'Imports System.Windows.Forms

'Module mLayout
'    Public Sub LookF4(bt As DevExpress.XtraEditors.ButtonEdit)

'        xTxtLayout(bt)

'        With bt.Properties.Buttons(0)
'            .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
'            .Image = My.Resources.F4
'            .Shortcut = New DevExpress.Utils.KeyShortcut(Keys.F4)
'        End With

'        bt.MaximumSize = New System.Drawing.Size(bt.Size.Width, 20)

'    End Sub

'    Public Sub xTxtLayout(bt As DevExpress.XtraEditors.ButtonEdit)
'        With bt.Properties.LookAndFeel
'            .SkinName = "Office 2013"
'            .Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
'            .UseDefaultLookAndFeel = False
'            .UseWindowsXPTheme = False
'        End With
'    End Sub



'    '-------------------------- nova estrutura -----------------------------


'    Private MaxHeight As Integer = 20
'    Private ValHeight As Integer = 20

'    Public Sub LookBaseF4(bt As DevExpress.XtraEditors.ButtonEdit, Imagem As System.Drawing.Image, Optional MaxLength As Integer = 0)

'        LookBaseXtraEditors(bt.Properties.LookAndFeel)

'        'PROPRIEDADES PROPRIAS PARA ESTE CONTROL
'        With bt.Properties.Buttons(0)
'            .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
'            .Image = Imagem 'My.Resources.F4
'            .Shortcut = New DevExpress.Utils.KeyShortcut(Keys.F4)
'        End With

'        PropBaseXtraEditors(bt.Properties, MaxLength)

'        bt.MaximumSize = New System.Drawing.Size(bt.Size.Width, MaxHeight)
'        bt.Size = New System.Drawing.Size(bt.Size.Width, ValHeight)

'    End Sub

'    Public Sub LookBaseTxt(txt As DevExpress.XtraEditors.TextEdit, Optional MaxLength As Integer = 0)

'        LookBaseXtraEditors(txt.Properties.LookAndFeel)
'        PropBaseXtraEditors(txt.Properties, MaxLength)

'        txt.MaximumSize = New System.Drawing.Size(txt.Size.Width, MaxHeight)
'        txt.Size = New System.Drawing.Size(txt.Size.Width, ValHeight)

'    End Sub

'    Public Sub PropBaseXtraEditors(ObjProperties As Object, Optional MaxLength As Integer = 0)
'        Try
'            With ObjProperties
'                .AutoHeight = False
'                .Appearance.Options.UseForeColor = True
'                .Appearance.Options.UseFont = True
'                .MaxLength = MaxLength
'            End With
'        Catch ex As Exception

'        End Try

'    End Sub

'    Public Sub LookBaseXtraEditors(objLookAndFeel As Object)
'        Try
'            With objLookAndFeel
'                .SkinName = "Office 2013"
'                .Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
'                .UseDefaultLookAndFeel = False
'                .UseWindowsXPTheme = False
'            End With
'        Catch ex As Exception

'        End Try
'    End Sub

'End Module
