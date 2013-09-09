Module mCalculo
    Public Function PrecoFecho(FchPreco As Double, dMedida As Double, DvlPreco As Double, Crs1Preco As Double, Crs2Preco As Double) As Double
        Dim dPreco As Double = 0
        Try
            'O limite minimo da medida para calculo do preço é 12 cm
            dMedida = IIf(CDbl(dMedida) < 12, 12, CDbl(dMedida))
            'medida vem em centimetros - passar para metros * 100
            dPreco = (FchPreco * (dMedida / 100)) + DvlPreco + Crs1Preco + Crs2Preco
            Return FormatNumber(dPreco, 3)
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Module
