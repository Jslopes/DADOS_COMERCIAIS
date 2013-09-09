Imports System.Windows.Forms

Module mLayout

    Private MaxHeight As Integer = 20
    Private ValHeight As Integer = 20

    Public Sub LookBaseF4(bt As DevExpress.XtraEditors.ButtonEdit, Imagem As System.Drawing.Image, Optional MaxLength As Integer = 0)

        'PROPRIEDADES PROPRIAS PARA ESTE CONTROL
        With bt.Properties.Buttons(0)
            .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            .Image = Imagem 'My.Resources.F4
            .Shortcut = New DevExpress.Utils.KeyShortcut(Keys.F4)
        End With

        LookBaseXtraEditors(bt.Properties.LookAndFeel)
        PropBaseXtraEditors(bt.Properties, MaxLength)

        bt.MaximumSize = New System.Drawing.Size(bt.Size.Width, MaxHeight)
        bt.Size = New System.Drawing.Size(bt.Size.Width, ValHeight)

    End Sub

    Public Sub LookBaseTxt(txt As DevExpress.XtraEditors.TextEdit, Optional MaxLength As Integer = 0)

        LookBaseXtraEditors(txt.Properties.LookAndFeel)
        PropBaseXtraEditors(txt.Properties, MaxLength)

        txt.MaximumSize = New System.Drawing.Size(txt.Size.Width, MaxHeight)
        txt.Size = New System.Drawing.Size(txt.Size.Width, ValHeight)

    End Sub

    Public Sub PropBaseXtraEditors(ObjProperties As Object, Optional MaxLength As Integer = 0)
        Try
            With ObjProperties
                .AutoHeight = False
                .Appearance.Options.UseForeColor = True
                .Appearance.Options.UseFont = True
                .MaxLength = MaxLength
            End With
        Catch ex As Exception

        End Try

    End Sub

    Public Sub LookBaseXtraEditors(objLookAndFeel As Object)
        'Propriedades do objeto
        Try
            With objLookAndFeel
                .SkinName = "VS2010" '"Office 2013"
                .Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
                .UseDefaultLookAndFeel = False
                .UseWindowsXPTheme = False
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
