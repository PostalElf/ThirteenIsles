Public Class Item
    Public Shared Function Generate(ByVal itemname As String, ByVal qty As Integer, Optional ByVal modifier As String = "") As Item
        Dim item As New Item
        With item
            .Category = GetCategory(itemname)
            If .Category = Nothing Then Return Nothing
            .Name = itemname
            .Qty = qty
            If modifier <> "" Then .Modifier = modifier
        End With
        Return item
    End Function

    Public Name As String
    Public Category As String
    Public Modifier As String
    Public Qty As Integer

    Public Shared Function GetCategory(ByVal itemName As String) As String
        Select Case itemName
            Case "Magesteel", "Coldiron" : Return "Metal"
            Case "Bloodcedar", "Ironoak" : Return "Wood"
            Case "Jewellery", "Silk", "Furs" : Return "Luxuries"
            Case "Beer", "Whiskey", "Wine" : Return "Drink"
            Case "Powderkeg", "Elixir", "Batteries" : Return "Consumables"
            Case "Rubedo", "Albedo", "Nigredo", "Citrinitas" : Return "Reagents"
            Case Else : Return Nothing
        End Select
    End Function

    Public Shared Operator =(ByVal i1 As Item, ByVal i2 As Item)
        If i1.Name = i2.Name AndAlso i1.Modifier = i2.Modifier Then Return True Else Return False
    End Operator
    Public Shared Operator <>(ByVal i1 As Item, ByVal i2 As Item)
        Return Not (i1 = i2)
    End Operator
End Class
