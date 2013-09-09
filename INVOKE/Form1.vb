Public Class Form1

    'Dim s As String = "Provider=SQLOLEDB.1;User ID=sa;Password=saPrimlp8;Initial Catalog=PRIGCP090812;Data Source=MICROR10\PRIMLP8"
    Dim u As String = "JoaoLopes"
    Dim p As String = "1"
    Dim f As String = "FASTIL"
    Dim k As String = "KLICK"
    Dim j As String = "JUALTEX"

    Public MotorFA As Interop.ErpBS800.ErpBS
    Public ObjConfApl As Interop.StdPlatBS800.StdBSConfApl
    Public PlataformaFA As Interop.StdPlatBS800.StdPlatBS

    Public MotorKL As Interop.ErpBS800.ErpBS
    Public PlataformaKL As Interop.StdPlatBS800.StdPlatBS

    Public MotorJU As Interop.ErpBS800.ErpBS
    Public PlataformaJU As Interop.StdPlatBS800.StdPlatBS

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim x As New mcr_DadosComerciais.cDadosComerciais


        MotorFA = New Interop.ErpBS800.ErpBS
        MotorFA.AbreEmpresaTrabalho(1, f, u, p)

        ObjConfApl = New Interop.StdPlatBS800.StdBSConfApl
        ObjConfApl.AbvtApl = "GCP"
        ObjConfApl.Instancia = MotorFA.DSO.Instancia
        ObjConfApl.Utilizador = u
        ObjConfApl.PwdUtilizador = p
        PlataformaFA = New Interop.StdPlatBS800.StdPlatBS
        PlataformaFA.AbrePlataformaEmpresaIntegrador(f, Nothing, ObjConfApl, 1)

        MotorKL = New Interop.ErpBS800.ErpBS
        MotorKL.AbreEmpresaTrabalho(1, k, u, p)

        ObjConfApl = New Interop.StdPlatBS800.StdBSConfApl
        ObjConfApl.AbvtApl = "GCP"
        ObjConfApl.Instancia = MotorKL.DSO.Instancia
        ObjConfApl.Utilizador = u
        ObjConfApl.PwdUtilizador = p
        PlataformaKL = New Interop.StdPlatBS800.StdPlatBS
        PlataformaKL.AbrePlataformaEmpresaIntegrador(k, Nothing, ObjConfApl, 1)


        MotorJU = New Interop.ErpBS800.ErpBS
        MotorJU.AbreEmpresaTrabalho(1, j, u, p)

        ObjConfApl = New Interop.StdPlatBS800.StdBSConfApl
        ObjConfApl.AbvtApl = "GCP"
        ObjConfApl.Instancia = MotorJU.DSO.Instancia
        ObjConfApl.Utilizador = u
        ObjConfApl.PwdUtilizador = p
        PlataformaJU = New Interop.StdPlatBS800.StdPlatBS
        PlataformaJU.AbrePlataformaEmpresaIntegrador(j, Nothing, ObjConfApl, 1)


        'x.AbreDadosComerciais(MotorFA, PlataformaFA, Nothing, Nothing, Nothing, Nothing, u, p, "FASTIL")
        x.AbreDadosComerciais(MotorFA, PlataformaFA, MotorKL, PlataformaKL, MotorJU, PlataformaJU, u, p, f, "00091")
        x = Nothing

        End


        'x.AbreDadosComerciais(f, k, j, u, p, "FASTIL")
        'x = Nothing

        End
    End Sub

End Class
