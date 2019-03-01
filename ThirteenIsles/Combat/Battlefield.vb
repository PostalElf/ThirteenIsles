Public Class Battlefield
    Public Sub New()
        For Each d In [Enum].GetValues(GetType(Directions))
            Quadrants.Add(d, New List(Of ShipCombat))
        Next
    End Sub

    Private Player As ShipCombat
    Private Quadrants As New Dictionary(Of Directions, List(Of ShipCombat))
    Public Sub Rotate(ByVal turnLeft As Boolean)
        Dim value As Integer = 0
        If turnLeft = True Then value = -1 Else value = +1

        Dim newQuadrants As New Dictionary(Of Directions, List(Of ShipCombat))
        For Each d In [Enum].GetValues(GetType(Directions))
            Dim tempList As List(Of ShipCombat) = Quadrants(d)
            d += value
            If d <= 0 Then d = 4
            If d >= 5 Then d = 1
            newQuadrants.Add(d, tempList)
        Next
    End Sub
End Class
