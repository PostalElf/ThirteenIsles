Public MustInherit Class Section
    Implements ShipAssignable
    Protected Name As String
    Private Quadrant As Quadrant
    Public MustOverride ReadOnly Property JobDescription As String
    Public MustOverride Function GetSection(ByVal param As String()) As Boolean

    Private Property Crews As New List(Of Crew) Implements ShipAssignable.Crews
    Private Sub Add(ByVal crew As Crew) Implements ShipAssignable.Add
        If Crews.Contains(crew) Then Exit Sub
        Crews.Add(crew)
    End Sub
    Private Function Addable(ByVal crew As Crew) As Boolean Implements ShipAssignable.Addable
        Return Quadrant.Addable(crew)
    End Function
    Private Sub Remove(ByVal crew As Crew) Implements ShipAssignable.Remove
        If Crews.Contains(crew) = False Then Exit Sub
        Crews.Remove(crew)
    End Sub
End Class
