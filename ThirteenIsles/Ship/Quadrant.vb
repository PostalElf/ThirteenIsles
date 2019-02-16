Public Class Quadrant
    Public Sub New(ByVal _ship As Ship, ByVal _facing As Directions, ByVal _maxCrew As Integer, ByVal _maxGuns As Integer)
        Ship = _ship
        Facing = _facing
        CrewMax = _maxCrew
        GunsMax = _maxGuns
    End Sub

    Private Ship As Ship
    Private Facing As Directions
    Private Guns As New List(Of Gun)
    Private GunsMax As Integer

    Private Crews As New List(Of Crew)
    Private CrewMax As Integer
    Public Function Add(ByVal Crew As Crew) As String
        If Crews.Count + 1 > CrewMax Then Return "Insufficient space."

        Crews.Add(Crew)
        Return Nothing
    End Function
    Public Function Remove(ByVal Crew As Crew) As String
        If Crews.Contains(Crew) = False Then Return "Invalid crew."

        Crews.Remove(Crew)
        Return Nothing
    End Function

    Public Function ConsoleReport(ByVal id As Integer) As String
        Dim total As String = vbIndent(id) & Facing.ToString & ":" & vbCrLf
        total &= vbIndent(id + 1) & "Crew: " & Crews.Count & "/" & CrewMax & vbCrLf
        total &= vbIndent(id + 1) & "Guns: " & Guns.Count & "/" & GunsMax & vbCrLf
        Return total
    End Function
End Class
