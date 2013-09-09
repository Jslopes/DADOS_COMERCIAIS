Imports System.Reflection

Public NotInheritable Class frmSobre

    Private Sub frmSobre_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '' Set the title of the form.
        'Dim ApplicationTitle As String = "Sobre, Artigos Pendentes de Entrega"

        'Me.Text = ApplicationTitle
        '' Initialize all of the text displayed on the About Box.
        '' TODO: Customize the application's assembly information in the "Application" pane of the project 
        ''    properties dialog (under the "Project" menu).
        'Me.LabelProductName.Text = "Artigos Pendentes de Entrega"
        'Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        'Me.LabelCopyright.Text = "Copyright ©  2012"
        'Me.LabelCompanyName.Text = "Microrégio"
        'Me.TextBoxDescription.Text = "Artigos Pendentes de Entrega"

        Dim aAssembly As [Assembly] = System.Reflection.Assembly.GetExecutingAssembly


        Dim ApplicationTitle As String = "Sobre, Pontos de Venda."
        Me.Text = ApplicationTitle

        Try
            Me.LabelVersion.Text = Trim(System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(",")(1).Replace("Version=", ""))

            Dim aTitle As AssemblyTitleAttribute = AssemblyTitleAttribute.GetCustomAttribute(aAssembly, GetType(AssemblyTitleAttribute))
            Dim aCopyRight As AssemblyCopyrightAttribute = AssemblyCopyrightAttribute.GetCustomAttribute(aAssembly, GetType(AssemblyCopyrightAttribute))
            Dim aCompany As AssemblyCompanyAttribute = AssemblyCompanyAttribute.GetCustomAttribute(aAssembly, GetType(AssemblyCompanyAttribute))

            Me.LabelProductName.Text = aTitle.Title
            Me.LabelCopyright.Text = aCopyRight.Copyright
            Me.LabelCompanyName.Text = aCompany.Company
            Me.TextBoxDescription.Text = "Pontos de Venda"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
