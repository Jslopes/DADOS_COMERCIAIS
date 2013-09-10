'<ComClass(cPontoVenda.ClassId, cPontoVenda.InterfaceId, cPontoVenda.EventsId)> _
<ComClass()> _
Public Class cDadosComerciais

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "cc055f48-12b9-4399-b76e-de95ae823263"
    Public Const InterfaceId As String = "67699043-5907-417c-a5e9-8817573b5939"
    Public Const EventsId As String = "885da4a4-815d-4ec2-9811-d45e6c9305f2"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    'Public Sub AbreDadosComerciais(M_FA As Object, P_FA As Object, _
    '                           M_KL As Object, P_KL As Object, _
    '                           M_JU As Object, P_JU As Object, _
    '                           User As String, Psw As String, EmpresaQueExecuta As String)

    Public Sub AbreDadosComerciais(mObj_FA As Object, pObj_FA As Object, _
                                   mObj_KL As Object, pObj_KL As Object, _
                                   mObj_JU As Object, pObj_JU As Object, _
                                   User As String, Password As String, EmpresaQueExecuta As String, Optional Cliente As String = "")

        'sUtilizador = User
        'sPassword = Psw
        'Create a connection object. 

        EmpresaGeral = EmpresaQueExecuta
        ClienteGeral = Cliente

        MotorFA = mObj_FA
        PlataformaFA = pObj_FA

        MotorKL = mObj_KL
        PlataformaKL = pObj_KL

        MotorJU = mObj_JU
        PlataformaJU = pObj_JU

        Dim StrConectFA As String = PlataformaFA.BaseDados.DaConnectionString(PlataformaFA.BaseDados.DaNomeBDdaEmpresa(PlataformaFA.Contexto.Empresa.CodEmp).ToString, "Default").ToString
        Dim StrConectKL As String = PlataformaKL.BaseDados.DaConnectionString(PlataformaKL.BaseDados.DaNomeBDdaEmpresa(PlataformaKL.Contexto.Empresa.CodEmp).ToString, "Default").ToString
        Dim StrConectJU As String = PlataformaJU.BaseDados.DaConnectionString(PlataformaJU.BaseDados.DaNomeBDdaEmpresa(PlataformaJU.Contexto.Empresa.CodEmp).ToString, "Default").ToString




        Dim f As New frmDadosComerciais
        f.GetDados(StrConectFA, StrConectKL, StrConectJU)
        f.ShowDialog()

        'PlataformaFA.FechaPlataforma()
        'MotorFA.FechaEmpresaTrabalho()

        MotorFA = Nothing
        PlataformaFA = Nothing
        MotorKL = Nothing
        PlataformaKL = Nothing
        MotorJU = Nothing
        PlataformaJU = Nothing

        Exit Sub


TERMINAR:
        MsgBox("Não foi possivel executar o painel de dados comerciais de cliente." & vbCrLf & "Contate o fornecedor de software ou valide todos os parâmetros.", _
               MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Painel Comercial")
        Exit Sub
ERRO:

    End Sub

    Private motor As Interop.ErpBS800.ErpBS = Nothing
    Private plataforma As Interop.StdPlatBS800.StdPlatBS = Nothing
    Private plataformapub As Interop.StdPlatBS800.StdBSInterfPub = Nothing

    Public Sub AbrePainelComercial(Aplicacao As Object)

        motor = Aplicacao.BSO
        plataformapub = Aplicacao.PlataformaPRIMAVERA

        MsgBox("Passo 1 - OK")

        Dim ObjConfApl = New Interop.StdPlatBS800.StdBSConfApl

        ObjConfApl.AbvtApl = "GCP"
        ObjConfApl.Instancia = motor.DSO.Instancia
        ObjConfApl.Utilizador = Aplicacao.Utilizador.Utilizador
        ObjConfApl.PwdUtilizador = Aplicacao.Utilizador.Password

        MsgBox("Passo 2 - OK")

        plataforma = New Interop.StdPlatBS800.StdPlatBS()

        plataforma.AbrePlataformaEmpresaIntegrador(Aplicacao.Empresa.CodEmp, Nothing, ObjConfApl, 1)

        MsgBox("Passo 3 - OK")

        Dim f As New frmDadosComerciais
        f.ShowDialog()

    End Sub




    '    Private motor As Interop.ErpBS800.ErpBS = Nothing
    '    Private plataforma As Interop.StdPlatBS800.StdPlatBS = Nothing
    '    Private plataformapub As Interop.StdPlatBS800.StdBSInterfPub = Nothing

    'public void ErpConnect(dynamic Aplicacao)
    '{
    'motor = Aplicacao.BSO;
    'plataformapub = Aplicacao.PlataformaPRIMAVERA;

    'var ObjConfApl = new Interop.StdPlatBS800.StdBSConfApl();
    'ObjConfApl.AbvtApl = "GCP";
    'ObjConfApl.Instancia = motor.DSO.Instancia;
    'ObjConfApl.Utilizador = Aplicacao.Utilizador.Utilizador;
    'ObjConfApl.PwdUtilizador = Aplicacao.Utilizador.Password;

    'plataforma = new Interop.StdPlatBS800.StdPlatBS();

    'plataforma.AbrePlataformaEmpresaIntegrador(Aplicacao.Empresa.CodEmp, null, ObjConfApl, EnumTipoPlataforma.tpProfissional);

    '}

End Class


