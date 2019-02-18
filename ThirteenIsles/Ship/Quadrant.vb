Public Class Quadrant
    Public Sub New(ByVal _ship As Ship, ByVal _facing As Directions, ByVal _maxCrew As Integer, ByVal _maxGuns As Integer)
        Ship = _ship
        Facing = _facing
        CrewMax = _maxCrew
        GunsMax = _maxGuns
    End Sub

    Private Ship As Ship
    Private Facing As Directions
    Private GunsMax As Integer
    Private Sections As New List(Of Section)

    Private ReadOnly Property Crews As List(Of Crew)
        Get
            Dim total As New List(Of Crew)
            For Each Section In Sections
                total.AddRange(CType(Section, ShipAssignable).Crews)
            Next
            Return total
        End Get
    End Property
    Private CrewMax As Integer
    Public Function Addable(ByVal crew As Crew) As Boolean
        If Crews.Count + 1 > CrewMax Then Return False Else Return True
    End Function

    Private DamageTaken As Integer
    Private DamageMax As Integer
    Private ReadOnly Property DamagePercentage As Integer
        Get
            If DamageTaken = 0 Then Return 0
            If DamageMax = 0 Then Return 0
            Return DamageTaken / DamageMax * 100
        End Get
    End Property

    Public Function ConsoleReportBrief() As String
        Dim total As String = vbTabb(Facing.ToString & ":", 12)
        total &= Crews.Count & "/" & CrewMax
        total &= "  "
        total &= ProgressBar(10, DamagePercentage)
        Return total
    End Function
    Public Function ConsoleReport(ByVal ind As Integer) As String
        Dim total As String = vbIndent(ind) & Facing.ToString & vbCrLf
        total &= vbIndent(ind + 1)
    End Function
End Class
